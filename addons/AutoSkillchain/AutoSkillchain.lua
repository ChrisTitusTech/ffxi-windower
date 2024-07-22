_addon.name = 'AutoSkillchain'
_addon.author = 'Ameilia'
_addon.commands = {'autosc','asc','autoskillchain'}
_addon.version = '1.1'
_addon.lastUpdate = '2020.08.28'

require('luau')
require('lor/lor_utils')
files = require('files')
_libs.lor.req('all')
_libs.lor.debug = false

local rarr = string.char(129,168)
local bags = {[0]='inventory',[8]='wardrobe',[10]='wardrobe2',[11]='wardrobe3',[12]='wardrobe4',[13]='wardrobe5',[14]='wardrobe6',[15]='wardrobe7',[16]='wardrobe8'}
local enabled = false
local wsDelay = 4
local useAutoRA = false
local araDelay = 0
local chains

local player = windower.ffxi.get_player()
local selected_chain_index = 1
local chain_index = 1
local activeChain
local chain_name
local distance_msg_enabled = true

local use_keybinds = true
local keybind = 'k'

local max_distances = {['melee'] = 4.95, ['ranged'] = 21.99}
local mode = 'melee'
local aftermath = false

local pet_moves = require('pet_moves')
local aftermath_weapons = require('aftermaths')

windower.register_event('addon command', function (command,...)
	command = command and command:lower() or 'help'
	local args = T{...}
	local arg_str = windower.convert_auto_trans(' ':join(args))
	local player = windower.ffxi.get_player()
	local job = player.main_job
	
	if S{'reload','unload'}:contains(command) then
		windower.send_command('lua %s %s':format(command, _addon.name))
	elseif S{'enable','on','start'}:contains(command) then
		enabled = true
		chain_index = 1
		print_status()
	elseif S{'disable','off','stop'}:contains(command) then
		enabled = false
		print_status()
	elseif command == 'toggle' then
		enabled = not enabled
		chain_index = 1
		print_status()
	elseif command == 'status' then
		print_status()
	elseif command == 'cycle_chain' then
		selected_chain_index = (selected_chain_index % #chains) + 1
		set_chain(selected_chain_index)
	elseif command == 'mode' then
		mode_str = args[1] and args[1]:lower()
		if (S{'melee','ranged'}:contains(mode_str)) then
			mode = args[1]
			atcc(50, 'AutoSkillChain mode set to ' .. mode)
		else 
			atcc(50, 'AutoSkillchain invalid mode: valid options are melee or ranged.')
		end
		refresh_from_file()
	elseif S{'aftermath','am3'}:contains(command) then
		local state = args[1]
		if S{'on','start'}:contains(state) and can_aftermath() then
			aftermath = true
			atcc(50, 'Autoskillchain Aftermath enabled')
		elseif S{'off','stop'}:contains(state) then
			aftermath = false
			atcc(50, 'Autoskillchain Aftermath disabled')
		else
			aftermath = false
			atcc(123, 'Autoskillchain Error: Equipped weapon is not configured for Aftermath handling.')
		end
	elseif command == 'refresh' then
		refresh_from_file()
	elseif command == 'autora' then
		local cmd = args[2] and args[2]:lower() or (useAutoRA and 'off' or 'on')
		if S{'on'}:contains(cmd) then
			useAutoRA = true
			atc('AutoSkillChain will now resume auto ranged attacks after WSing')
		elseif S{'off'}:contains(cmd) then
			useAutoRA = false
			atc('AutoSkillChain will no longer resume auto ranged attacks after WSing')
		else
			atc(123,'Error: invalid argument for AutoRA: '..cmd)
		end		
	elseif S{'help','--help'}:contains(command) then
		print_help()
	elseif command == 'info' then
		if not _libs.lor.exec then
			atc(3,'Unable to parse info.  Windower/addons/libs/lor/lor_exec.lua was unable to be loaded.')
			atc(3,'If you would like to use this function, please visit https://github.com/lorand-ffxi/lor_libs to download it.')
			return
		end
		local cmd = args[1]     --Take the first element as the command
		table.remove(args, 1)   --Remove the first from the list of args
		_libs.lor.exec.process_input(cmd, args)
	else
		atc('Error: Unknown command')
	end
end)

windower.register_event('load', function()
	if not _libs.lor then
		windower.add_to_chat(39,'ERROR: .../Windower/addons/libs/lor/ not found! Please download: https://github.com/lorand-ffxi/lor_libs')
	end
	atcc(262, 'Welcome to AutoSkillChain!')
	
	if(use_keybinds == true) then
		windower.send_command('unbind ^'..keybind) 
		windower.send_command('unbind !'..keybind)
		windower.send_command('unbind @'..keybind)
		windower.send_command('bind ^'..keybind..' asc toggle')
		windower.send_command('bind !'..keybind..' asc cycle_chain')
		windower.send_command('bind @'..keybind..' asc refresh')
	end
	
	autowsLastCheck = os.clock()
	coroutine.schedule(refresh_from_file, 2)
end)

windower.register_event('login', function()
	coroutine.schedule(refresh_from_file, 10)
end)

windower.register_event('status change', function()
	chain_index = 1
	distance_msg_enabled = true
end)


windower.register_event('zone change', function(new_id, old_id)
	autowsLastCheck = os.clock() + 15
end)


windower.register_event('job change', function()
	player = windower.ffxi.get_player()
	job = player.main_job
	enabled = false
	refresh_from_file()
	selected_chain_index = 1
	set_chain(selected_chain_index)
end)

windower.register_event('prerender', function()
	if enabled then
		local now = os.clock()
		if (now - autowsLastCheck) >= wsDelay and (now - araDelay >= 0) then
			local player = windower.ffxi.get_player()
			local mob = windower.ffxi.get_mob_by_target()
			local ws = activeChain['ws'][chain_index]
			if (player ~= nil) and (player.status == 1) and (mob ~= nil) then
				if aftermath == true and can_aftermath() and not aftermath_up() then
					if (player.vitals.tp > 2999) and distance_check(player, mob) then 
						perform_ws(aftermath_weapons[weapon_name()])
						chain_index = 1
						autowsLastCheck = now
					end
				else 
					if (player.vitals.tp > 999	or pet_moves['bst_ready']:contains(ws) or pet_moves['smn_pacts']:contains(ws)) and distance_check(player, mob) then
						handle_chain()
					end
				end
			end
		end
	end
end)

function distance_check(player, mob)
	if(mob.distance:sqrt() > max_distances[mode]) then
		if(distance_msg_enabled) then
			atcc(263, 'AutoSkillChain out of range, holding weapon skill '..activeChain['ws'][chain_index])
			distance_msg_enabled = false --don't spam the screen with the distance message.
		end
		return false
	end
	distance_msg_enabled = true
	return true
end

function set_chain(chindex)
	if not chains_defined() then
		no_chains()
		return
	end

	if chindex <= #chains then
		chain_index = 1
		activeChain = chains[chindex]
		chain_name = activeChain['name']
	end
	print_status()
end
	
function handle_chain()
	if activeChain == nil then
		no_chains()
		enabled = false
		return
	end
	
	local stop = false
	local now = os.clock()
	
	if chain_index == #activeChain['ws'] and not activeChain['repeat'] then
		stop = true
	end	
	
	if useAutoRA and (araDelay < 1) then
		araDelay = now + 1.2
	else
		local ws = activeChain['ws'][chain_index]
		perform_ws(ws)

		chain_index = (chain_index % #activeChain['ws']) + 1
		
		if stop then
			enabled = false
			atcc(17, 'Reached the end of the chain. Stopping AutoSC.')
		end
		araDelay = 0
		if useAutoRA then
			windower.send_command('wait 4;ara start')
		end
		autowsLastCheck = now
	end
end

function chains_defined() 
	return #chains > 0
end

function no_chains()
	if not chains_defined() then
		local player = windower.ffxi.get_player()
		local job = player.main_job
		local skill = weapon_type()
		atcc(263,'No skillchains defined for '..job..' '..skill)
	end
end

function weapon_type()
	local skill = 'Hand-to-Hand'
	local player = windower.ffxi.get_player()
	local i,bag,items

	if (player ~= nil) then
		items = windower.ffxi.get_items()
		if (mode:lower() == 'melee') then
			i = items.equipment.main
			bag = items.equipment.main_bag
		elseif (mode:lower() == 'ranged') then
			i = items.equipment.range
			bag = items.equipment.range_bag
		else 
			atcc(263,'Something went terribly wrong. You should not be here.')
			return 
		end
	end

	if i ~= 0 and items[bags[bag]][i].id ~= 0 then  --0 => nothing equipped
		skill = res.skills[res.items[items[bags[bag]][i].id].skill].en
	end
	return skill
end

function weapon_name()
	local wname = 'naked'
	local player = windower.ffxi.get_player()
	items = windower.ffxi.get_items()

	weapon = items.equipment.main
	bag = items.equipment.main_bag
	
	if mode:lower() == 'ranged' then
		weapon = items.equipment.range
		bag = items.equipment.range_bag
	end
	
	if weapon ~= 0 and items[bags[bag]][weapon].id ~= 0 then
		wname = res.items[items[bags[bag]][weapon].id].en
	end
	
	return wname
end

function can_aftermath()
	local w = weapon_name()
	return aftermath_weapons[w] ~= nil
end

function aftermath_up()
	return T(windower.ffxi.get_player().buffs):contains(272)
end

function perform_ws(ws)
		local input_str = 'input /ws "%s" <t>'
		
		if pet_moves['bst_ready']:contains(ws) then 
			input_str = 'input /pet "%s" <me>'
		elseif pet_moves['smn_pacts']:contains(ws) then 
			input_str = 'input /pet "%s" <t>'
		end
		
		windower.send_command(input_str:format(ws))
end

function refresh_from_file()
	player = windower.ffxi.get_player()
	player_mob_table = windower.ffxi.get_mob_by_index(player.index)
	job = player.main_job
	local skill = weapon_type()
	local pet_chains = {}
	
	if S{'BST','SMN'}:contains(job) and player_mob_table.pet_index and player_mob_table.pet_index ~= 0 then
		local pet_name = windower.ffxi.get_mob_by_index(player_mob_table.pet_index).name
		pet_name = pet_name:stripchars(' ')
		pet_chains = _libs.lor.settings.load('data/'..job..'/'..skill..'_'..pet_name..'.lua', {})
	end
	
	chains = _libs.lor.settings.load('data/'..job..'/'..skill..'.lua', {})
	if chains and pet_chains then 
		chains:extend(pet_chains)
	end
	set_chain(1)
end

function print_status()
	if not chains_defined() then
		no_chains()
		return
	end
	
	local power = enabled and 'ON' or 'OFF'
	local msg = '[AutoSC: %s mode: %s] %s ':format(power, mode, chain_name)
	if enabled then
		msg = msg..rarr..' '..activeChain['ws']:tostring()
	end
	
	if activeChain['repeat'] then
		msg = msg..' REPEATING'
	end

	atcc(50, msg)
end

function print_help()
	local help = T{
		['[on|off|toggle]'] = 'Enable / disable autoSkillchain',
		['autora [on|off]'] = 'Enable / disable the AutoRA addon',
		['[aftermath|am3] [on|off]'] = 'Enable / disable AM3 handling',
		['mode [melee|ranged]'] = 'Change distance calculation to melee/ranged mode',
		['CTRL-'..keybind] = 'Toggle autoSkillchain enabled/disabled',
		['ALT-'..keybind] = 'Cycle through defined chains',
		['Windows-'..keybind] = 'Refresh job/weapon type information',
	}

	local mwwidth = col_width(help:keys())
	atcc(262, 'AutoSkillChain commands:')
	for cmd,desc in opairs(help) do
		atc(cmd:rpad(' ', mwwidth):colorize(263), desc:colorize(1))
	end
end


-----------------------------------------------------------------------------------------------------------
--[[
Copyright © 2018, Ameilia
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
	* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
	* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
	* Neither the name of ffxiHealer nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL Lorand BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
--]]
-----------------------------------------------------------------------------------------------------------
