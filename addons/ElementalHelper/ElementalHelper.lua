--Copyright (c) 2018, Ameilia
--All rights reserved.

--Redistribution and use in source and binary forms, with or without
--modification, are permitted provided that the following conditions are met:

--    * Redistributions of source code must retain the above copyright
--      notice, this list of conditions and the following disclaimer.
--    * Redistributions in binary form must reproduce the above copyright
--      notice, this list of conditions and the following disclaimer in the
--      documentation and/or other materials provided with the distribution.
--    * Neither the name of ElementalHelper nor the
--      names of its contributors may be used to endorse or promote products
--      derived from this software without specific prior written permission.

--THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
--ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
--WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
--DISCLAIMED. IN NO EVENT SHALL Ameilia BE LIABLE FOR ANY
--DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
--(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
--LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
--ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
--(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
--SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

require 'luau'

_addon.name = 'ElementalHelper'
_addon.version = '1.0'
_addon.author = 'Ameilia'
_addon.commands = {'eh','elementalhelper','elehelper'}

elements = {'Stone','Water','Aero','Fire','Blizzard','Thunder','Light','Dark'}
ancient = {'Quake','Flood','Tornado','Flare','Freeze','Burst'}
helices = {'Geo','Hydro','Anemo','Pyro','Cryo','Iono','Lumino','Nocto'}
storms = {'Sand','Rain','Wind','Fire','Hail','Thunder','Aurora','Void'}
gas = {'Stone','Water','Aero','Fira','Blizza','Thunda'}
ras = {'Stone','Wate','Ae','Fi','Blizza','Thunda'}
sc1 = {'Aero','Stone','Stone','Stone','Water','Water','Noctohelix','Blizzard'}
sc2 = {'Stone','Water','Aero','Fire','Blizzard','Thunder','Luminohelix','Noctohelix'}
shots = {'Earth','Water','Wind','Fire','Ice','Thunder','Light','Dark'}
brd = {'Earth','Water','Wind','Fire','Ice','Lightning','Light','Dark'}
runes = {'Tellus','Unda','Flabra','Ignis','Gelus','Sulpor','Lux','Tenebrae'}
nin = {'Doton','Suiton','Huton','Katon','Hyoton','Raiton'}
barelements = {'Barstonra','Barwatera','Baraera','Barfira','Barblizzara','Barthundra'}
bardebuffs = {'Barpetra','Barpoisonra','Barsilencera','Baramnesra','Barparalyzra','Barvira'}

eleIndex = 1

function handle_spell(spelltype, cmdParams)
	local target = '<t>'
    local tier = ''
	local spellstr = elements[eleIndex]

	if cmdParams[1] then
        tier = cmdParams[1]:upper()	
		if(tier == 'I') then
			tier = ''
		end
    end
	
	if(S{'Light','Dark'}:contains(spellstr)) then
		if(S{'n','nuke','ga','ra','ja','ancient','nin'}:contains(spelltype)) then
			windower.add_to_chat(50, 'ElementalHelper: No spell defined for '..spellstr..' '..spelltype..'.')
			return
		end
	end
	
	if(S{'ga','ja'}:contains(spelltype)) then
		spellstr = gas[eleIndex] .. spelltype
	elseif spelltype == 'ancient' then
		spellstr = ancient[eleIndex]
	elseif spelltype == 'ra' then
		spellstr = ras[eleIndex] .. spelltype
	elseif spelltype == 'helix' then
		spellstr = helices[eleIndex] .. spelltype
	elseif spelltype == 'storm' then
		spellstr = storms[eleIndex] .. spelltype
		target = '<stpt>'
	elseif S{'carol','threnody'}:contains(spelltype) then
		local brdspell = brd[eleIndex]
		if(spelltype == 'threnody') then
			target = '<t>'
			if brdspell == 'Lightning' then
				brdspell = 'Ltng.'
			end
		else
			target = '<stpc>'
		end
		spellstr = brdspell .. ' ' .. spelltype
	elseif spelltype == 'nin' then
		spellstr = nin[eleIndex] .. ':'
		if S{'','I','1'}:contains(tier) then
			tier = 'Ichi'
		elseif S{'II','2'}:contains(tier) then
			tier = 'Ni'
		elseif S{'III','3'}:contains(tier) then 
			tier = 'San'
		end
	elseif spelltype == 'en' then
		spellstr = 'En'..spellstr:lower()
		target = '<me>'
	elseif spelltype == 'sc1' then
		spellstr = sc1[eleIndex]
		windower.send_command('input /ja "Immanence" <me>;wait 1;input /p '..elements[eleIndex]..' Skillchain #1')
	elseif spelltype == 'sc2' then
		spellstr = sc2[eleIndex]
		windower.send_command('input /ja "Immanence" <me>;wait 1;input /p '..elements[eleIndex]..' Skillchain #2')
	elseif spelltype == 'bar' then
		spellstr = barelements[eleIndex]
		target = '<me>'
	elseif spelltype == 'bardebuff' then
		spellstr = bardebuffs[eleIndex]
		target = '<me>'		
	end
    	
	local spell = spellstr..' '..tier	
	windower.send_command('@input /ma "'..spell..'" '..target)
	
	if(S{'ja'}:contains(spelltype)) then
		windower.send_command('wait 4;input /p '..gas[eleIndex] .. spelltype..' away. '..shots[eleIndex]..' '..'Shot please.')
	end
end

function handle_ja(jatype)
	local target = '<t>'
	local jastr = ''
	
	if jatype == 'rune' then
		jastr = runes[eleIndex]
		target = '<me>'
	elseif jatype == 'shot' then 
		jastr = shots[eleIndex]..' '..'Shot'
	end
	
	windower.send_command('@input /ja "'..jastr..'" '..target)
end

windower.register_event('addon command',function(...)
    local args = {...}
    local first = table.remove(args,1):lower()

	if first then
		if first == 'cycle' then
			eleIndex = (eleIndex % #elements) + 1
			report_nuke()
			windower.send_command('ank setnuke '..elements[eleIndex])
			windower.send_command('sing setelement '..brd[eleIndex])
		elseif S{'storm','nuke','n','helix','ga','ja','ra','sc1','sc2','threnody','carol','ancient','nin','en','bar','bardebuff'}:contains(first) then
			handle_spell(first, args)
		elseif first == 'bars' then
			handle_spell('bar', args)
			coroutine.sleep(7)
			handle_spell('bardebuff',args)
		elseif S{'shot','rune'}:contains(first) then
			handle_ja(first)
		elseif S{'unload','reload'}:contains(first) then
			windower.send_command('lua %s %s':format(first, _addon.name))
		elseif first == 'set' then
			local found = false
			local el = args[1]:lower()
			for i,v in ipairs(elements) do
				if v:lower() == el then
					eleIndex = i
					found = true
				end
			end
			if found then
				report_nuke()
			else 
				windower.add_to_chat(39, 'Could not find Element '..args[1])
			end
		elseif first == 'help' then
			print_help()
		else
			windower.add_to_chat(39, 'Error: Unknown Command')
		end
	end
end)

windower.register_event('load', function()
	windower.add_to_chat(50, 'Welcome to ElementalHelper')
	report_nuke()
end)

function report_nuke()
	windower.add_to_chat(50, 'ElementalHelper Element: '..elements[eleIndex])
end

function print_help()
	windower.add_to_chat(50, 'ElementalHelper usage (//eh):')
	windower.add_to_chat(50, '   cycle - Cycles the element')
	windower.add_to_chat(50, '   set (element) - Sets the element directly')
	windower.add_to_chat(50, '   nuke/n (I,II,III,IV,V,VI) - Cast (element) (tier)')
	windower.add_to_chat(50, '   ancient (I,II) - Cast AM (tier)')
	windower.add_to_chat(50, '   ga (I,II,III) - Cast (element)ga (tier)')
	windower.add_to_chat(50, '   ja - Cast (element)ja')
	windower.add_to_chat(50, '   en (I, II) - cast En(element) (tier)')
	windower.add_to_chat(50, '   helix (I,II) - Cast (element)helix (tier)')
	windower.add_to_chat(50, '   storm (I,II) - Cast (element)storm (tier)')
	windower.add_to_chat(50, '   bar - Cast bar(element)ra')
	windower.add_to_chat(50, '   bardebuff - Cast bar(element-debuff)ra i.e. Barparalyzra for ice')
	windower.add_to_chat(50, '   bars - Cast bar(element)ra and bar(element-debuff)ra')
	windower.add_to_chat(50, '   nin (I,II,III) - Cast (element) ninjutsu (tier)')
	windower.add_to_chat(50, '   threnody (I,II) - Cast (element)threnody (tier)')
	windower.add_to_chat(50, '   carol (I,II) - Cast (element)carol (tier)')
	windower.add_to_chat(50, '   shot - use (element) quickdraw')
	windower.add_to_chat(50, '   rune - use (element) rune')
	windower.add_to_chat(50, '   sc1 - Open SCH tier1 skillchain for (element)')
	windower.add_to_chat(50, '   sc2 - Close SCH tier1 skillchain for (element)')
end
