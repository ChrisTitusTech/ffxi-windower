require 'pack'
require 'lists'
require 'strings'
res = require 'resources'


_addon.name = 'Sparky'
_addon.version = '1'
_addon.author = 'Byrth'
_addon.commands = {'sparky'}

item = 12385 -- Default to Acheron's Shield
number_to_sell = nil
current_sparks = nil
print_code = false
start_buying = false
appraise = false
start_selling = true
last_seq = nil

option_strings = {
    [12385]={str=string.char(9,0,0x29,0),cost=2755}, -- Acheron's Shield
    [12302]={str=string.char(8,0,0x24,0),cost=473}, -- Darksteel Buckler
    [16834]={str=string.char(4,0,0xE,0),cost=60}, -- Brass Spear
    [17081]={str=string.char(4,0,0x15,0),cost=60}, -- Brass Rod
    [16407]={str=string.char(4,0,0,0),cost=60}, -- Brass Baghnakhs
    [12680]={str=string.char(5,0,0x1F,0),cost=141}, -- Chain Mittens
    [12936]={str=string.char(5,0,0x21,0),cost=129}, -- Greaves
    [12299]={str=string.char(3,0,0x3F,0),cost=50,id=12299}, -- Aspis
    [16704]={str=string.char(3,0,0xC,0),cost=50}, -- Butterfly Axe
    [16390]={str=string.char(3,0,0x1,0),cost=50}, -- Bronze Knuckles
    [16900]={str=string.char(3,0,0x12,0),cost=50}, -- Wakizashi
    [16960]={str=string.char(4,0,0x12,0),cost=68}, -- Uchigatana
    [16419]={str=string.char(7,0,2,0),cost=416}, -- Patas
    [16406]={str=string.char(5,0,1,0),cost=144}, -- Baghnakhs
    [16470]={str=string.char(9,0,2,0),cost=300}, -- Gully
    [13871]={str=string.char(6,0,0x43,0),cost=302}, -- Iron Visor
    [13783]={str=string.char(6,0,0x44,0),cost=464}, -- Iron Scale Mail
    [12938]={str=string.char(7,0,0x32,0),cost=322}, -- Sollerets
    [16644]={str=string.char(6,0,0x0D,0),cost=540}, -- Mythril Axe
}

purge_items = S{
	5945, --Prize Powder
    3297, --Flame Geode
    3298, --Snow Geode
    3299, --Breeze Geode
    3300, --Soil Geode
    3301, --Thunder Geode
    3302, --Aqua Geode
    3303, --Light Geode
    3304, --Shadow Geode

    3520, --Ifritite
    3521, --Shivite
    3522, --Garudite
    3523, --Titanite
    3524, --Ramuite
    3525, --Leviatite
    3526, --Carbite
    3527, --Fenrite

    12385, --Acheron shield

    626, --Black Pepper
    643, --Iron Ore
    645, --Darksteel Ore
    685, --Khroma Ore
    3920, --Vanadium Ore
    1469, --Wootz Ore
    3928, --Velkk Necklace
    20772, --Voay Sword -1
    638, --Sage
    4386, --King Truffle
    623, --Bay Leaves
    2112, --Vanilla
    2155, --Lesser Chigoe
    4402, --Red Terrapin
    1771, --Dragon Bone
    1695, --Habaneros
    700, --Mahogany Log
    690, --Elm Log
    3993, --Grove Cuttings
    4478, --Three-eyed fish
    5965, --Isleracea
    1522, --Fresh Marjoram
    635, --Win. Tea Leaves
    5474, --Ca Cuong
    881, --Crab Shell
    1193, --H.Q. Crab Shell
    612, --Kazham Peppers
    614, --Mhaura Garlic
    4424, --Melon Juice
    1236, --Cactus Stems
    5138, --Black Ghost
    895, --Ram Horn
    4304, --Grimmonite
    4020, --Titanium Ore
    897, --Scorpion Claw
    835, --Flax Flower
    4473, --Crescent Fish
    1590, --Holy Basil
    5984, --Gnatbane
    4566, --Deathball
    2763, --Swamp Ore
    4384, --Black Sole


    2290, -- Imperial Cermet

    4199, -- Strength Potion
    4201, -- Dexterity Potion
    4203, -- Vitality Potion
    4205, -- Agility Potion
    4207, -- Intelligence Potion
    4209, -- Mind Potion
    4211, -- Charisma Potion

    4167, -- Cracker
    4168, -- Twinkle Shower
    4183, -- Konron Hassen
    4169, -- Little Comet
    4184, -- Kongou Inaho
    4185, -- Meifu Goma
    4186, -- Ariborn
    4215, -- Popstar
    4216, -- Brilliant Snow
    4217, -- Sparkling Hand
    4218, -- Air Rider
    4250, -- Crackler
    4251, -- Festive Fan
    4252, -- Summer Fan
    4253, -- Spirit Masque
    4256, -- Ouka Ranman
    4257, -- Papillion
    5360, -- Muteppo
    5361, -- Datechochin
    5441, -- Angelwing
    5725, -- Goshikitenge
    5769, -- Popper
    5881, -- Shisai Kaboku
    5882, -- Marine Bliss
    5883, -- Falling Star
    6457, -- Flarelet
    5884, -- Rengedama
    5936, -- Mog Missile
    5532, -- Ichinin. Koma
    6268, -- Komanezumi
    5937, -- Bubble Breeze


    4096, -- Fire crystal
    4097, -- Ice Crystal
    4098, -- Wind crystal
    4099, -- Earth crystal
    4100, -- Lightning Crystal
    4101, -- Water Crystal
    4102, -- Light Crystal
    4103, -- Dark Crystal
    17088, -- Ash Staff
    16465, -- Bronze Knife
    17034, -- Bronze Mace
    16565,--Spatha
    19224,--Musketoon

    4370, -- Honey
    3941, -- Chapuli Wing
    3932, -- Chapuli Horn
    2150, -- Colibri Feather
    925, -- Giant Stinger
    --4358, --hare meat
    856, --rabbit hide
    926,  --lizard tail
    852,  --lizard skin
    4362, -- lizard egg
--        853,  --raptor skin
--        3930, --twitherym wing
    3936, -- Acuex Poison
    2506, -- Ladybug wing
    5566, -- Date
    5908, -- Butterpear
    2417, -- Aht Urghan Brass



    2784, -- Fire Fewell
    2785, -- Ice Fewell
    2786, -- Wind Fewell
    2787, -- Earth Fewell
    2788, -- Lightning Fewell
    2789, -- Water Fewell
    2790, -- Light Fewell
    2791, -- Dark Fewell

--        750, --Silver Beastcoin
    3541, -- Seasoning Stone
    922, -- Bat Wing
    891, -- Bat Fang

    858, -- Wolf Hide
    1691,--Giant Scale
    868, -- Pugil Scales

--        955, -- Golem Shard
    1689, -- Recollection of Guilt
    1687, -- Recollection of Fear
    1688, -- Recollection of Pain

    769, -- Red Rock
    770, -- Blue Rock
    771, -- Yellow Rock
    772, -- Green Rock
    773, -- Translucent Rock
    774, -- Purple Rock
    775, -- Black Rock
    776, -- White Rock

    4114, -- Potion +2
    4118, -- Hi-Potion +2
    4119, -- Hi-Potion +3
    4122, -- X-Potion +2
    4123, -- X-Potion +2
    4454, -- Emperor Fish

    698, -- Ash Log
    951, -- Wijnruit
    4504, -- Acorn
    4767, -- Scroll of Stone
    4388, -- Eggplant
    4363, -- Faerie Apple
    5966, --Ulbuconut
    620, -- Tarutaru Rice
--        690, -- Elm Log
    693, -- Walnut Log
    691, -- Maple Log
    727, -- Dogwood Log
    923, -- Dryad Root
    941, -- Red Rose
    1524, -- Fresh Mugwort
    2213, -- Pine Nuts
    5661, -- Walnut
    4491, -- Watermelon
    642, -- Zinc Ore
    1650, -- Kopparnickle Ore
    900, -- Fish Bones
    882, -- Sheep Tooth
--        644, -- Mythril Ore
    1984, -- Snapping Mole
    4468, -- Pamamas
    5964, -- Felicifruit
    1982, -- King Locust
    613, -- Faded Crystal
    582, -- Meteorite
    894, -- Beetle Jaw
    1654, -- Igneous Rock
    2417, -- Aht Urhgan Brass
    768, -- Flint Stone
    641, -- Tin Ore
    2305, -- Auric Sand
    5235, -- Napa
    5650, -- Nopales
--        1236, -- Cactus Stems
--        17396, -- Little Worm
    4899, -- Earth Spirit Pact
    3934, -- Matamata Shell
    639, -- Chestnut
--        736, -- Silver Ore
    1845, -- Red Moko Grass
    1983, -- Mushroom Locust
    834, -- Saruta Cotton
    833, -- Moko Grass
    1981, -- Skull Locust
    4365, -- Rolanberry
    880, -- Bone Chip
    948, -- Carnation
--        643, -- Iron Ore
    775, -- Black Rock
    4445, -- Yagudo Cherry
    1228, -- Darksteel Nugget
    17296, -- Pebble
--        640, -- Copper Ore
    2960, -- Water Lily
    3942, -- Locust Elutriator

    4018, -- Guatambu Log
    3926, -- Urunday Log
    694, -- Chestnut Log
    4273, -- Kitron
    4274, -- Persikos
    4461, -- Bastore Bream
    4474, -- Gigant Squid
    4479, -- Bhefhel Marlin
    740, -- Phrygian Ore
    896, -- Scorpion Shell
    1618, -- Uragnite Shell
    1309, -- Gold Leaf
    1473, -- H.Q. Scp. Shell
    1622, -- Bugard Tusk
    21191, -- Voay Staff -1
    8709, -- Raaz Tusk
    2765, -- Fool's Gold Ore
    2427, -- Wivre Maul
    5604, -- El. Pachira Fruit
    4399, -- Bluetail
    702, -- Ebony Log
    3929, -- Velkk Mask
    903, -- Dragon Talon
    4446, -- Pumpkin Pie
    4288, -- Zebra Eel
    5950, -- Mackerel
    885, -- Turtle Shell
    1237, -- Tree Cuttings
    1446, -- Lacquer Tree Log
    1616, -- Antlion Jaw
    2496, -- Anc. Beast Horn
    2408, -- Flocon-de-mer
    3915, -- Saffron Blossom
    722, -- Divine Log
    4138, -- Super Ether +2
    4477, -- Gavial Fish
    4475, -- Sea Zombie

    784, -- Jadeite
    785, -- Emerald
    786, -- Ruby
    787, -- Diamond
    788, -- Peridot
    789, -- Topaz
    790, -- Garnet
    791, -- Aquamarine
    792, -- Pearl
    793, -- Black Pearl
    794, -- Sapphire
    795, -- Lapis Lazuli
    796, -- Light Opal
    797, -- Painite
    798, -- Turquoise
    799, -- Onyx
    800, -- Amethyst
    801, -- Chrysoberyl
    802, -- Moonstone
    803, -- Sunstone
    804, -- Spinel
    805, -- Zircon
    806, -- Tourmaline
    807, -- Sardonyx
    808, -- Goshenite
    809, -- Clear Topaz
    810, -- Fluorite
    811, -- Ametrine
    812, -- Deathstone
    813, -- Angelstone
    814, -- Amber
    815, -- Sphene

    4401, -- Moat Carp
    4428, -- Dark Bass
    5952, -- Ruddy Seema
    4464, -- Pipira
    5960, -- Ulbukan Lobster
    5948, -- Black Prawn
    4514, -- Quus
    5463, -- Yayinbaligi
    5959, -- Dragonfish
    4429, -- Black Eel
    4469, -- Giant Catfish
    4484, -- Shall Shell
    4515, -- Copper Frog
    4579, -- Elshimo Newt
    5121, -- Moorish Idol
    5536, -- Yorchete
    5651, -- Burdock
    5662, -- Dragon Fruit
    5951, -- Bloodblotch
    5128, -- Cone Calamary
    5961, -- Contortopus
    6145, -- Dwarf Remora
    5954, -- Barnacle
    5469, -- Brass Loach
    --4480, -- Gugru Tuna
    3965, -- Adoulinian Kelp
    4427, -- Gold Carp
    5963, -- Senroh Sardine
    4472, -- Crayfish


    924, -- Fiend Blood

    842, -- Giant Bird Feather
    14242, -- Rusty Subligar
    14117, -- Rusty Leggings
    688, -- Arrowwood Log
    4443, -- Cobalt Jellyfish
    90, -- Rusty Bucket
    861, -- Tiger Hide
    884, -- Black Tiger Fang
    846, -- Insect Wing
    912, -- Beehive Chip
    839, -- Crawler Cocoon
    4373, -- Woozyshroom
    4375, -- Danceshroom
    4374, -- Sleepshroom
    4570, -- Bird Egg
    847, -- Bird Feather
    5569, -- Puk Egg
    2148, -- Puk Wing
    2229, -- Chimera Blood
    }

sparks_npcs = L{'Eternal Flame','Rolandienne','Isakoth','Fhelm Jobeizat'}

items_to_sell = L{}
appraised = S{}
buying = nil

windower.register_event('outgoing chunk',function(id,org,mod,inj)
    local seq = org:unpack('H',3)
    if not last_seq then last_seq = seq end
    if not inj and seq ~= last_seq then
        last_seq = seq
        if start_selling and items_to_sell:length()>0 then
            local item_tab = items_to_sell:remove(1)
            windower.packets.inject_outgoing(0x84,string.char(0x84,0x06,0,0)..'I':pack(item_tab.count)..'H':pack(item_tab.id)..string.char(item_tab.slot,0))
            windower.packets.inject_outgoing(0x85,string.char(0x85,0x04,0,0)..'I':pack(1))
        elseif buying then
            buy_one()
        end
    end
    if not inj and id == 0x05B then
        local name = (windower.ffxi.get_mob_by_id(org:unpack('I',5)) or {}).name
        if name == 'Ardrick' then
            return org:sub(1,8)..string.char(1,0,60,0)..org:sub(13)
        end
    end
end)

windower.register_event('addon command',function(...)
    local commands = {...}
    local first = table.remove(commands,1):lower()
    if first then
        if first == 'buy' then
            local second = table.remove(commands,1)
            second = tonumber(second)
            for i,v in pairs(windower.ffxi.get_mob_array()) do
                if v.name and sparks_npcs:contains(v.name) and math.sqrt(v.distance)<10 and windower.ffxi.get_player().status == 0 then
                    windower.packets.inject_outgoing(0x1A,string.char(0x1A,0xE,0,0)..'IHHHHIII':pack(v.id,v.index,0,0,0,0,0,0))
                    start_buying = tonumber(second) and second or true
                    break
                end
            end
        elseif first == 'sell' then
            local second = table.remove(commands,1)
            if tonumber(second) then
                number_to_sell = tonumber(second)
            else
                number_to_sell = nil
            end
            if not res.items[item] then
                print('Sparky: Item cannot be sold because id is not in the resources')
            elseif res.items[item].flags['No NPC Sale'] then
                print('Sparky: Item cannot be sold because it is unsellable')
            else
                items_to_sell = L{}
                local inv = windower.ffxi.get_items(0)
                for i,v in ipairs(inv) do
                    if v.id and v.id == item then
                        items_to_sell:append(v)
                        if number_to_sell and number_to_sell == items_to_sell:length() then break end
                    end
                end
                if items_to_sell:length() > 0 then
                    windower.packets.inject_outgoing(0x84,string.char(0x84,0x06,0,0)..'I':pack(items_to_sell[1].count)..'H':pack(item)..string.char(items_to_sell[1].slot,0))
                    appraise = true
                end
            end
        elseif first == 'item' then
            local pot_item = table.concat(commands,' '):lower()
            if tonumber(pot_item) and res.items[tonumber(pot_item)] then
                item = tonumber(pot_item)
                pot_item = nil
                print('Sparky: Item is now '..res.items[item].en)
            else
                counter = 0
                for i,v in pairs(res.items) do
                    if v.en and v.en:lower() == pot_item then
                        item = i
                        pot_item = nil
                        print('Sparky: Item is now '..res.items[item].en)
                        break
                    end
                    counter = counter + 1
                    if counter%1000 == 0 then
                        coroutine.sleep(0.04) -- Sleep 1 frame every 1000 items searched to avoid lagging people out
                    end
                end
            end
            if pot_item then
                print('Sparky: Item not found. Item is still '..res.items[item].en)
            end
        elseif first == 'purge' then
            if not res.items[item] then
                print('Sparky: Item cannot be sold because id is not in the resources')
            elseif res.items[item].flags['No NPC Sale'] then
                print('Sparky: Item cannot be sold because it is unsellable')
            else
                items_to_sell = L{}
                appraised = S{}
                local inv = windower.ffxi.get_items(0)
                for i,v in ipairs(inv) do
                    if v.id and purge_items:contains(v.id) then
                        items_to_sell:append(v)
                        if number_to_sell and number_to_sell == items_to_sell:length() then break end
                    end
                end

                for _,v in ipairs(items_to_sell) do
                    if not appraised:contains(v.id) then
                        windower.packets.inject_outgoing(0x84,string.char(0x84,0x06,0,0)..'I':pack(v.count)..'H':pack(v.id)..string.char(v.slot,0))
                        appraised:append(v.id)
                    end
                end
            end
        elseif first == 'print' then
            print_code = not print_code
            print('Sparky: Print is now set to '..tostring(print_code))
        end
    end
end)

function buy_one()
    if buying and buying.number > 0 then
        buying.number = buying.number - 1
        windower.packets.inject_outgoing(0x5B,buying.buy_packet)
    end
    if buying and buying.number <= 0 then
        windower.packets.inject_outgoing(0x5B,buying.end_packet)
        buying = nil
    end
end

windower.register_event('incoming chunk',function(id,org)
    if id == 0x034 and sparks_npcs:contains(windower.ffxi.get_mob_by_id(org:unpack('I',5)).name) and option_strings[item] and start_buying then
        local current_sparks = org:unpack('I',13)
        local inv_stats = windower.ffxi.get_bag_info(0)
        buying = {
            number = math.min(math.floor(current_sparks/option_strings[item].cost),inv_stats.max - inv_stats.count),
            buy_packet = string.char(0x5B,0xA,0,0)..org:sub(5,8)..option_strings[item].str..org:sub(0x29,0x2A)..string.char(1,0)..org:sub(0x2B,0x2E),
            end_packet = string.char(0x5B,0xA,0,0)..org:sub(5,8)..string.char(1,0,0,0)..org:sub(0x29,0x2A)..string.char(0,0)..org:sub(0x2B,0x2E)
            }
        if type(start_buying) == 'number' then
            buying.number = math.min(buying.number,start_buying)
        end
        buy_one()
        start_buying = false
        return true
    elseif id == 0x03D and appraise then
        appraise = false
        start_selling = true
    elseif id == 0x00A then
        appraised = S{}
    end
end)