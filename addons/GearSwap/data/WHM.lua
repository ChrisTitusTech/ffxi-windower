-- Called when this job file is unloaded (eg: job change)
function get_sets()
    -- Load and initialize the include file.
    mote_include_version = 2
	include('Mote-Include.lua')
    -- Additional local binds
    include('Global-Binds.lua')
end

function user_setup()
    -- Add state tracking for healbot
    state = state or {}
    state.HealBot = M(false, 'HealBot')
    
    -- Load healbot first
    send_command('lua l healbot')
    
    -- Create a keybind for toggling healbot - try both methods
    send_command('bind !h gs c hbtoggle')
    
    windower.add_to_chat(158,'WHM Lua file loaded, HealBot toggle bound to Alt+H')
    
    -- Set macro book 1, page 1 for WHM spells
    set_macro_page(1, 1)
end

function job_setup()
    state.Buff['Afflatus Solace'] = buffactive['Afflatus Solace'] or false
    state.Buff['Afflatus Misery'] = buffactive['Afflatus Misery'] or false
    state.Buff['Sublimation: Activated'] = buffactive['Sublimation: Activated'] or false
    
    -- Add weapon options and set based on subjob
    state.WeaponSet = M{['description']='Weapon Set', 'Default', 'DualWield'}
    if player.sub_job == 'NIN' or player.sub_job == 'DNC' then
        state.WeaponSet:set('DualWield')
    else
        state.WeaponSet:set('Default')
    end
end

function file_unload()
    send_command('unbind !h')
    send_command('unbind ^[')
    send_command('lua u healbot')
end

function init_gear_sets()    
    -- Weapon sets
    sets.weapons = {}
    sets.weapons.Default = {
        main={ name="Queller Rod", augments={'Healing magic skill +15','"Cure" potency +10%','"Cure" spellcasting time -7%',}},
        sub="Sors Shield",
    }
    sets.weapons.DualWield = {
        main={ name="Queller Rod", augments={'Healing magic skill +15','"Cure" potency +10%','"Cure" spellcasting time -7%',}},
        sub="Ames",
        left_ear="Suppanomimi",
    }
    
    -- Base engaged set that other sets will build from
    sets.engaged = {
        ammo="Hasty Pinion +1",
        head="Aya. Zucchetto +2",
        body="Ayanmo Corazza +2",
        hands="Aya. Manopolas +2",
        legs="Aya. Cosciales +2",
        feet="Aya. Gambieras +2",
        neck="Asperity Necklace",
        waist="Windbuffet Belt",
        left_ear="Brutal Earring",
        right_ear="Cessance Earring",
        left_ring="Chirich Ring",
        right_ring="Rajas Ring",
        back={ name="Alaunus's Cape", augments={'MND+10','Eva.+20 /Mag. Eva.+20','MND+10','Enmity-10','Damage taken-5%',}},
    }

    -- Simplified idle set
    sets.idle = set_combine(sets.engaged, sets.weapons[state.WeaponSet.value], {
        ammo="Homiliary",
        ear1="Moonshade Earring",
        ear2="Ethereal Earring",
        body="Ebers Bliaut +2",
        hands="Ebers Mitts +2",
        legs="Ebers Pant. +2"
    })

    sets.resting = sets.idle

    -- Simplified FastCast set
    sets.precast.FC = {
        ammo="Incantor Stone",
        neck="Clr. Torque +1",
        ear1="Loquac. Earring",
        ear2="Orison Earring",
        hands="Gende. Gages +1",
        ring1="Prolix Ring",
        ring2="Moonbeam Ring",
        back="Swith Cape",
        waist="Witful Belt",
        legs="Aya. Cosciales +2",
        feet="Theo. Duckbills +1"
    }

    -- Cure precast
    sets.precast.FC.Cure = set_combine(sets.precast.FC, {
        legs="Ebers Pant. +2",
    })

    -- Cure potency
    sets.midcast.Cure = {
        ammo="Staunch Tathlum",
        head="Ebers Cap +1",
        neck="Clr. Torque +1",
        ear1="Roundel Earring",
        ear2="Orison Earring",
        body="Ebers Bliaut +2",
        hands="Telchine Gloves",
        ring1="Sirona's Ring",
        ring2="Ephedra Ring",
        back={ name="Alaunus's Cape", augments={'MND+10','Eva.+20 /Mag. Eva.+20','MND+10','Enmity-10','Damage taken-5%',}},
        waist="Witful Belt",
        legs="Ebers Pant. +2",
        feet="Theo. Duckbills +1"
    }

    sets.midcast.CureSolace = set_combine(sets.midcast.Cure, {
        body="Ebers Bliaut +2"
    })
    
    sets.midcast['Enhancing Magic'] = {
        ammo="Staunch Tathlum",
        head="Ebers Cap +1",
        neck="Clr. Torque +1",
        ear1="Roundel Earring",
        ear2="Orison Earring",
        body="Piety Bliaut +1",
        hands="Ebers Mitts +2",
        ring1="Sirona's Ring",
        ring2="Ephedra Ring",
        back={ name="Alaunus's Cape", augments={'MND+10','Eva.+20 /Mag. Eva.+20','MND+10','Enmity-10','Damage taken-5%',}},
        waist="Witful Belt",
        legs="Piety Pantaloons",
        feet="Theo. Duckbills +1"
    }

    -- Regen set
    sets.midcast.Regen = set_combine(sets.midcast['Enhancing Magic'], {
        body="Piety Bliaut +1",
        hands="Ebers Mitts +2",
        legs="Theo. Pant. +1"
    })

    -- Nuking set for solo play
    sets.midcast['Divine Magic'] = {
        legs="Theo. Pant. +1",
        hands="Fanatic Gloves"
    }

    -- Optional: Specific set for Holy
    sets.midcast['Holy'] = sets.midcast['Divine Magic']
    sets.midcast['Holy II'] = sets.midcast['Divine Magic']
end

function precast(spell)
    if spell.action_type == 'Magic' then
        if spell.skill == 'Healing Magic' then
            if spell.english:startswith('Cure') or spell.english:startswith('Curaga') then
                equip(sets.precast.FC.Cure)
            else
                equip(sets.precast.FC)
            end
        else
            equip(sets.precast.FC)
        end
    end
end

function midcast(spell)
    if spell.skill == 'Healing Magic' then
        if spell.english:startswith('Cure') then
            if buffactive['Afflatus Solace'] then
                equip(sets.midcast.CureSolace)
            else
                equip(sets.midcast.Cure)
            end
        elseif spell.english:startswith('Curaga') then
            equip(sets.midcast.Cure)
        elseif spell.english:startswith('Regen') then
            equip(sets.midcast.Regen)
        end
    elseif spell.skill == 'Enhancing Magic' then
        equip(sets.midcast['Enhancing Magic'])
    elseif spell.skill == 'Divine Magic' then
        equip(sets.midcast['Divine Magic'])
    end
end

function aftercast(spell)
    if player.status == 'Engaged' then
        equip(set_combine(sets.engaged, sets.weapons[state.WeaponSet.value]))
    else
        equip(sets.idle)
    end
end

function status_change(new,old)
    if new == 'Engaged' then
        equip(set_combine(sets.engaged, sets.weapons[state.WeaponSet.value]))
    else
        equip(sets.idle)
    end
end

-- Define self_commands
function job_self_command(commandArgs, eventArgs)  
    if commandArgs[1] == 'hbtoggle' then
        state.HealBot:toggle()
        send_command('hb ' .. (state.HealBot.value and 'on' or 'off'))
    end
end

-- Modify the default melee set after it was constructed.
function customize_melee_set(meleeSet)
    -- Apply the weapon set
    meleeSet = set_combine(meleeSet, sets.weapons[state.WeaponSet.value])
    return meleeSet
end

-- Called when your sub job changes
function sub_job_change(new, old)
    -- Update weapon set based on subjob
    if new == 'NIN' or new == 'DNC' then
        state.WeaponSet:set('DualWield')
    else
        state.WeaponSet:set('Default')
    end
    equip(sets.weapons[state.WeaponSet.value])
end