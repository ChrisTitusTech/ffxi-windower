_addon.name = 'FFXIKeys'
_addon.author = 'Areint'
_addon.version = '1.0.4'
_addon.commands = {'keys'}

require('logger')
local packets = require('packets')
local settings = require('settings')
local targets = require('targets')
local res = require('resources')

local keyids = S{
	res.items:with('en', 'SP Gobbie Key').id,
	res.items:with('en', 'Dial Key #ANV').id,
	res.items:with('en','Dial Key #Fo').id,
	res.items:with('en','Dial Key #Ab').id
}

local key_id = require('resources').items:with('en', 'SP Gobbie Key').id
local anvkey_id = require('resources').items:with('en', 'Dial Key #ANV').id
local fokey_id = require('resources').items:with('en','Dial Key #Fo').id
local running = false
local player_id
local npc

--------------------------------------------------------------------------------
-- Validates game state before attempting to trade.
--
function run()
    running = false
    npc = nil

    -- Make sure we have player information
    if not player_id then
        log('Unable to get player information')
        return
    end

    -- Make sure we can get info from the game
    local info = windower.ffxi.get_info()
    if not info then
        log('Unable to get game info')
        return
    end

    -- Make sure the player is in a supported zone
    local data = targets[info.zone]
    if not data then
        log('Not in a valid zone')
        return
    end

    -- Make sure there is room in the players inventory
    local bag = windower.ffxi.get_items(0)
    if not bag or bag.count >= bag.max then
        log('Inventory is full')
        return
    end

    -- Make sure the npc is in range
    npc = windower.ffxi.get_mob_by_name(data.name)
    if not npc or npc.distance > settings.config.maxdistance then
        log('Not in range of npc')
        return
    end

    -- Find keys in the players inventory and trade one
    for index, item in pairs(bag) do
        if type(item) == 'table' and (keyids:contains(item.id)) then
            local pkt = packets.new('outgoing', 0x036)
            if not pkt then
                log('Unable to create outgoing packet')
                return
            end

            pkt['Target'] = npc.id
            pkt['Item Count 1'] = 1
            pkt['Item Index 1'] = index
            pkt['Target Index'] = npc.index
            pkt['Number of Items'] = 1

            packets.inject(pkt)
            running = true
            return
        end
    end

    log('No more keys')
end

--------------------------------------------------------------------------------
-- Handles addon commands.
--
-- param [in] cmd - The user command.
--
function handle_command(cmd)
    if not cmd then
        return
    end

    local lcmd = cmd:lower()
    if lcmd == 'start' then
        log('Starting')
        running = true
        run()

    elseif lcmd == 'stop' then
        log('Stopping')
        running = false

    elseif lcmd == 'printlinks' then
        log('Turning printing links ' .. (settings.config.printlinks and 'off' or 'on'))
        settings.config.printlinks = not settings.config.printlinks
        settings.save()

    elseif lcmd == 'openlinks' then
        log('Turning opening links ' .. (settings.config.openlinks and 'off' or 'on'))
        settings.config.openlinks = not settings.config.openlinks
        settings.save()

    end
end

--------------------------------------------------------------------------------
-- Handles addon load.  Gets the player id for the session.
--
function handle_load()
    local player = windower.ffxi.get_player()
    if not player then
        player_id = nil
    else
        player_id = player.id
    end

    settings.load()
end

--------------------------------------------------------------------------------
-- Parses incoming chunks.  Used to trigger additional trades.
--
-- param [in] id  - The packet id.
-- param [in] pkt - The packet data.
--
function handle_incoming(id, _, pkt, _, _)
    if running and id == 0x037 then
        local pkt = packets.parse('incoming', pkt)
        if pkt and pkt['Status'] == 0 and pkt['Player'] == player_id then
            run()
        end
    elseif running and npc and id == 0x02A then
        local pkt = packets.parse('incoming', pkt)
        if pkt and pkt['Player'] == npc.id and pkt['Player Index'] == npc.index then
            if settings.config.printlinks then
                log('https://www.ffxiah.com/item/' .. pkt['Param 1'] .. '/')
            end
            if settings.config.openlinks then
                windower.open_url('https://www.ffxiah.com/item/' .. pkt['Param 1'] .. '/')
            end
        end
    end
end

--------------------------------------------------------------------------------
windower.register_event('load', handle_load)
windower.register_event('addon command', handle_command)
windower.register_event('incoming chunk', handle_incoming)
