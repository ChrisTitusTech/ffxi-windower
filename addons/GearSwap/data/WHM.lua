-- Called when this job file is unloaded (eg: job change)
function get_sets()
    -- Load and initialize the include file.
    mote_include_version = 2
	include('Mote-Include.lua')
    -- Additional local binds
    include('Global-Binds.lua')
    sets = {}  -- Initialize sets table
    sets.precast = {}  -- Initialize precast table
    sets.midcast = {}  -- Initialize midcast table
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

function file_unload()
    -- First call the global binds_on_unload if it exists
    if binds_on_unload then
        binds_on_unload()
    end
    
    -- Remove all our custom keybinds
    send_command('unbind !h')
    send_command('unbind ^[')
    
    -- Only unload healbot if it was loaded
    windower.send_command('lua u healbot')
end

function init_gear_sets()    
    -- Add weapon sets at the start of init_gear_sets
    sets.weapons = {}
    sets.weapons.Default = {
        main="Queller Rod",
        sub="Sors Shield",
    }
    sets.weapons.DualWield = {
        main="Queller Rod",
        sub="Ames", 
    }
    
    -- Engaged sets
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

    -- Idle sets (based on engaged, keeping only Refresh/Regen items)
    sets.idle = set_combine(sets.engaged, {
        ammo="Homiliary",
        ear1="Moonshade Earring",
        ear2="Ethereal Earring", -- Refresh
        body="Ebers Bliaut +2", -- Refresh
        hands="Ebers Mitts +2",
        legs="Ebers Pant. +2",
        ring1="Chirich Ring",
    })

    sets.resting = sets.idle
    -- FastCast
    sets.precast.FC = {
        ammo="Incantor Stone",
        head="Ebers Cap +1",
        neck="Clr. Torque +1",
        ear1="Loquac. Earring",
        ear2="Orison Earring",
        body="Piety Bliaut +1",
        hands="Ebers Mitts +2",
        ring1="Prolix Ring",
        ring2="Moonbeam Ring",
        back="Alaunus's Cape",
        waist="Witful Belt",
        legs="Ebers Pant. +2",
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
        hands="Ebers Mitts +2",
        ring1="Sirona's Ring",
        ring2="Ephedra Ring",
        back="Alaunus's Cape",
        waist="Witful Belt",
        legs="Ebers Pant. +2",
        feet="Theo. Duckbills +1"
    }

    -- Cure potency with Afflatus Solace
    sets.midcast.CureSolace = set_combine(sets.midcast.Cure, {
        body="Ebers Bliaut +2",  -- Enhances Afflatus Solace
    })

    -- Enhancing Magic
    sets.midcast['Enhancing Magic'] = {
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

    -- Regen
    sets.midcast.Regen = set_combine(sets.midcast['Enhancing Magic'], {
        -- head="Inyanga Tiara",
        body="Piety Bliaut +1",
        hands="Ebers Mitts +2",
        legs="Theo. Pant. +1",
    })

    -- Nuking set for solo play
    sets.midcast['Divine Magic'] = {
        legs="Theo Pant. +1",
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
            equip(sets.midcast.Cure)  -- Using regular Cure set for Curaga
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
    update_combat_weapon()
    if player.status == 'Engaged' then
        equip(sets.engaged)
    else
        equip(sets.idle)
    end
end

function status_change(new,old)
    update_combat_weapon()
    if new == 'Engaged' then
        equip(sets.engaged)
    else
        equip(sets.idle)
    end
end

-- Define self_commands
function job_self_command(commandArgs, eventArgs)  
    if commandArgs[1] == 'hbtoggle' then
        state.HealBot:toggle()
        if state.HealBot.value then
            send_command('hb on')
        else
            send_command('hb off')
        end
    end
end

-- Add this new function after init_gear_sets
function update_combat_weapon()
    -- Get the current sub job
    local sub_job = player.sub_job
    
    -- Choose appropriate weapon set
    if sub_job == 'NIN' or sub_job == 'DNC' then
        equip(sets.weapons.DualWield)
    else
        equip(sets.weapons.Default)
    end
end
