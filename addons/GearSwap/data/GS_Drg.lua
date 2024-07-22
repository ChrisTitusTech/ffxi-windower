--Basic DRG GS file, tested and debugged. 

function get_sets()

sets.JA = {}

sets.JA['Spirit Surge'] = {body="Pteroslaver Mail +1"}
sets.JA['Call Wyvern'] = {body="Pteroslaver Mail +1"}
sets.JA['Ancient Cirlce'] = {legs="Vishap Brais +1"}
sets.JA['Jump'] = {body="Vishap Mail +1",feet="Vishap Greaves"}
sets.JA['Spirit Link'] = {head="Vishap Armet",hands="Lancer's Vambraces +2"}
sets.JA['High Jump'] = {body="Vishap Mail +1",feet="Vishap Greaves"}
sets.JA['Deep Breathing'] = {head="Pteroslaver Armet +1"}
sets.JA['Angon'] = {ammo="Angon",hands="Pteroslaver Finger Gauntlets +1"}
sets.JA['Spirit Jump'] = {body="Lancer's Plackart +2"}
sets.JA['Soul Jump'] = {body="Lancer's Plackart +2"}

sets. TP = {}
sets.TP.index = {'Reg','Acc','DT'}
TP_Index = 1


sets.TP.Reg = {ammo="Ginsen",
			head="Otomi Helm",neck="Asperity Necklace",ear1="Brutal Earring",ear2="Tripudio Earring",
			body="Xaddi Mail",hands="Xaddi Gauntlets",ring1="Rajas Ring",ring2="K'ayre Ring",
			back="Updraft Mantle",waist="Windbuffet Belt +1",legs="Xaddi Cuisses",feet="Ejekamal Boots"}
			
sets.TP.Acc = {ammo="Ginsen",
			head="Yaoyotl Helm",neck="Iqabi Necklace",ear1="Brutal Earring",ear2="Tripudio Earring",
			body="Xaddi Mail",hands="Xaddi Gauntlets",ring1="Patricius Ring",ring2="Beeline Ring",
			back="Updraft Mantle",waist="Olensi Belt",legs="Xaddi Cuisses",feet="Whirlpool Greaves"}
			
sets.TP.DT = {ammo="Ginsen",head="Ighwa Cap",
			body="Mikinaak Breastplate",neck="Twilight Torque",hands="Umuthi Gloves",ring1="Defending Ring",
			ring2="Patricius Ring",back="Mollusca Mantle",waist="Flume Belt",legs="Xaddi Cuisses",feet="Cizin Greaves +1"}
			



sets.Star = {}
sets.Star.index = {'Reg','Acc'}
Star_Index = 1

sets.Star.Reg = {ammo="Ginsen",
				body="Xaddi Mail",neck="Light Gorget",ear1="Brutal Earring",ear2="Moonshade Earring",
				hands="Mikinaak Gauntlets",ring1="Ifrit Ring",ring2="Ifrit Ring",back="Buquwik Cape",
				waist="Light Belt",legs="Scuffler's Cosciales",feet="Ejekamal Boots"}

sets.Star.Acc = set_combine(sets.Star.Reg,{ring1="Rajas Ring",legs="Mikinaak Cuisses",back="Updraft Mantle"})

sets.Star.GH = set_combine(sets.Star.Reg,{head="Gavialis Helm"})
sets.Star.R = set_combine(sets.Star.Reg,{neck="Ygnas's Resolve +1"})

sets.Drake = {}
sets.Drake.index = {'Reg','Acc'}
Drake_Index = 1

sets.Drake.Reg = {ammo="Ginsen",
				body="Xaddi Mail",neck="Light Gorget",ear1="Brutal Earring",ear2="Moonshade Earring",
				hands="Mikinaak Gauntlets",ring1="Ifrit Ring",ring2="Ifrit Ring",back="Rancorous Mantle",
				waist="Light Belt",legs="Scuffler's Cosciales",feet="Ejekamal Boots"}

sets.Drake.Acc = set_combine(sets.Drake.Reg,{ring1="Rajas Ring",back="Updraft Mantle",legs="Mikinaak Cuisses"})

sets.Drake.GH = set_combine(sets.Drake.Reg,{head="Gavialis Helm"})
sets.Drake.R = set_combine(sets.Drake.Reg,{neck="Ygnas Resolve +1"})

sets.Caml = {}
sets.Caml.index = {'Reg','Acc'}
Caml_Index = 1

sets.Caml.Reg = {ammo="Ginsen",
				head="Otomi Helm",neck="Light Gorget",ear1="Brutal Earring",ear2="Moonshade Earring",
				body="Phorcys Korazin",hands="Mikinaak Gauntlets",ring1="Ifrit Ring",ring2="Ifrit Ring",
				back="Buquwik Cape",waist="Light Belt",legs="Scuffler's Cosciales",feet="Ejekamal Boots"}

sets.Caml.Acc = set_combine(sets.Caml.Reg,{head="Yaoyotl Helm",body="Xaddi Mail",ring1="Rajas Ring",
				back="Updraft Mantle",legs="Mikinaak Cuisses"})

sets.Caml.GH = set_combine(sets.Caml.Reg,{head="Gavialis Helm"})		
sets.Caml.R = set_combine(sets.Caml.Reg,{neck="Ygnas's Resolve +1"})		
				
sets.Sonic = {}
sets.Sonic.index = {'Reg','Acc'}
Sonic_Index = 1

sets.Sonic.Reg = {ammo="Ginsen",
				head="Otomi Helm",neck="Light Gorget",ear1="Brutal Earring",ear2="Moonshade Earring",
				body="Phorcys Korazin",hands="Mikinaak Gauntlets",ring1="Ifrit Ring",ring2="Ifrit Ring",
				back="Buquwik Cape",waist="Light Belt",legs="Scuffler's Cosciales",feet="Ejekamal Boots"}

sets.Sonic.Acc = set_combine(sets.Sonic.Reg,{head="Yaoyotl Helm",body="Xaddi Mail",ring1="Rajas Ring",
				back="Updraft Mantle",legs="Mikinaak Cuisses"})

sets.Sonic.GH = set_combine(sets.Sonic.Reg,{head="Gavialis Helm"})
sets.Sonic.R = set_combine(sets.Sonic.Reg,{neck="Ygnas's Resolve +1"})
				
end

function precast(spell,act)

	if spell.type == 'JobAbility' then
		if sets.JA[spell.english] then
			equip(sets.JA[spell.english])
		end
	end

	if spell.english == "Stardiver" then
	equip(sets.Star[sets.Star.index[Star_Index]])
	end

	if spell.english == "Drakesbane" then
	equip(sets.Drake[sets.Drake.index[Drake_Index]])
	end

	if spell.english == "Sonic Thrust" then
	equip(sets.Sonic[sets.Sonic.index[Sonic_Index]])
	end

	if spell.english == "Camlann's Torment" then
	equip(sets.Caml[sets.Caml.index[Caml_Index]])
	end

	if buffactive['Reive Mark'] and spell.english == "Stardiver" then
	equip(sets.Star.R)
	end

	if buffactive['Reive Mark'] and spell.english == "Drakesbane" then
	equip(sets.Drake.R)
	end

	if buffactive['Reive Mark'] and spell.english == "Sonic Thrust" then
	equip(sets.Sonic.R)
	end

	if buffactive['Reive Mark'] and spell.english == "Camlann's Torment" then
	equip(sets.Caml.R)
	end

	if spell.english == "Stardiver" and (world.day == "Earthsday" or world.day == "Darksday" or world.day == "Lightsday") then
	equip(sets.Star.GH)
	end

	if spell.english == "Drakesbane" and (world.day == "Firesday" or world.day == "Lightsday") then
	equip(sets.Drake.GH)
	end

	if spell.english == "Sonic Thrust" and (world.day == "Lightsday" or world.day == "Earthsday") then
	equip(sets.Sonic.GH)
	end

	if spell.english == "Camlann's Torment" and (world.day == "Lightsday" or world.day == "Windsday" or world.day == "Lightiningday") then
	equip(sets.Caml.GH)
	end

end

function midcast(spell,act)

	if spell.type == 'JobAbility' then
		if sets.JA[spell.english] then
			equip(sets.JA[spell.english])
		end
	end

	if spell.english == "Stardiver" then
	equip(sets.Star[sets.Star.index[Star_Index]])
	end

	if spell.english == "Drakesbane" then
	equip(sets.Drake[sets.Drake.index[Drake_Index]])
	end

	if spell.english == "Sonic Thrust" then
	equip(sets.Sonic[sets.Sonic.index[Sonic_Index]])
	end

	if spell.english == "Camlann's Torment" then
	equip(sets.Caml[sets.Caml.index[Caml_Index]])
	end

	if buffactive['Reive Mark'] and spell.english == "Stardiver" then
	equip(sets.Star.R)
	end

	if buffactive['Reive Mark'] and spell.english == "Drakesbane" then
	equip(sets.Drake.R)
	end

	if buffactive['Reive Mark'] and spell.english == "Sonic Thrust" then
	equip(sets.Sonic.R)
	end

	if buffactive['Reive Mark'] and spell.english == "Camlann's Torment" then
	equip(sets.Caml.R)
	end

	if spell.english == "Stardiver" and (world.day == "Earthsday" or world.day == "Darksday" or world.day == "Lightsday") then
	equip(sets.Star.GH)
	end

	if spell.english == "Drakesbane" and (world.day == "Firesday" or world.day == "Lightsday") then
	equip(sets.Drake.GH)
	end

	if spell.english == "Sonic Thrust" and (world.day == "Lightsday" or world.day == "Earthsday") then
	equip(sets.Sonic.GH)
	end

	if spell.english == "Camlann's Torment" and (world.day == "Lightsday" or world.day == "Windsday" or world.day == "Lightiningday") then
	equip(sets.Caml.GH)
	end
end

function aftercast(spell,act)
	if player.status == 'Engaged' then
		equip(sets.TP[sets.TP.index[TP_Index]])
	end
	
	if player.status == 'Engaged' and buffactive['Reive Mark'] then
                equip{neck="Ygnas's Resolve +1"}
    end
end

function buff_change(new,old)

if player.status == 'Engaged' and buffactive['Reive Mark'] then
                equip{neck="Ygnas's Resolve +1"}
        end

end

function status_change(new,old)

if player.status == 'Engaged' then
		equip(sets.TP[sets.TP.index[TP_Index]])
	end
	
if player.status == 'Engaged' and buffactive['Reive Mark'] then
                equip{neck="Ygnas's Resolve +1"}
    end
end



function self_command(command)

	if command == 'toggle TP set' then	
		TP_Index = TP_Index +1
		if TP_Index > #sets.TP.index then TP_Index = 1 end
		send_command('@ input /echo >>> TP set changed to: '..sets.TP.index[TP_Index]..' ')
		equip(sets.TP[sets.TP.index[TP_Index]])

	elseif command == 'toggle WS set' then
		Star_Index = Star_Index +1
		if Star_Index > #sets.Star.index then Star_Index = 1 end
		Drake_Index = Drake_Index +1
		if Drake_Index > #sets.Drake.index then Drake_Index = 1 end
		Sonic_Index = Sonic_Index +1
		if Sonic_Index > #sets.Sonic.index then Sonic_Index = 1 end
		Caml_Index = Caml_Index +1
		if Caml_Index > #sets.Caml.index then Caml_Index = 1 end
		send_command('@ input /echo >>> WS sets changed to '..sets.Star.index[Star_Index]..' ')
	end
		
	end