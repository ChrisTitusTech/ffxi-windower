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
    send_command('lua load healbot')
    set_macro_page(1, 1)
end

function file_unload()
    if binds_on_unload then
    	binds_on_unload()
    end
    
    send_command('lua unload healbot')
end

function init_gear_sets()    
    -- Engaged sets
    sets.engaged = {
        ammo="Staunch Tathlum",
        head="Aya. Zucchetto +2",
        neck="Sanctity Necklace",
        ear1="Brutal Earring",
        ear2="Cessance Earring",
        body="Ayanmo Corazza +2",
        hands="Aya. Manopolas +2",
        ring1="Chirich Ring",
        ring2="Enlivened Ring",
        back="Alaunus's Cape",
        waist="Windbuffet Belt",
        legs="Aya. Cosciales +2",
        feet="Aya. Gambieras +2"
    }

    -- Idle sets (based on engaged, keeping only Refresh/Regen items)
    sets.idle = set_combine(sets.engaged, {
        main="Queller Rod",     -- Refresh
        sub="Sors Shield",      -- Regen
        ear1="Moonshade Earring",
        ear2="Ethereal Earring", -- Refresh
        body="Ebers Bliaut +1", -- Refresh
        ring1="Chirich Ring",
    })

    -- FastCast
    sets.precast.FC = {
        ammo="Incantor Stone",
        head="Ebers Cap",
        neck="Clr. Torque +1",
        ear1="Loquac. Earring",
        ear2="Orison Earring",
        body="Piety Bliaut +1",
        hands="Ebers Mitts +1",
        ring1="Prolix Ring",
        ring2="Moonbeam Ring",
        back="Alaunus's Cape",
        waist="Witful Belt",
        legs="Ebers Pant. +1",
        feet="Theo. Duckbills +1"
    }

    -- Cure precast
    sets.precast.FC.Cure = set_combine(sets.precast.FC, {
        legs="Ebers Pant. +1",
    })

    -- Cure potency
    sets.midcast.Cure = {
        main="Queller Rod",
        sub="Sors Shield",
        ammo="Staunch Tathlum",
        head="Ebers Cap",
        neck="Clr. Torque +1",
        ear1="Roundel Earring",
        ear2="Orison Earring",
        body="Ebers Bliaut +1",
        hands="Ebers Mitts +1",
        ring1="Sirona's Ring",
        ring2="Ephedra Ring",
        back="Alaunus's Cape",
        waist="Witful Belt",
        legs="Ebers Pant. +1",
        feet="Theo. Duckbills +1"
    }

    -- Enhancing Magic
    sets.midcast['Enhancing Magic'] = {
        main="Queller Rod",
        sub="Sors Shield",
        ammo="Staunch Tathlum",
        head="Ebers Cap",
        neck="Clr. Torque +1",
        ear1="Roundel Earring",
        ear2="Orison Earring",
        body="Piety Bliaut +1",
        hands="Ebers Mitts +1",
        ring1="Sirona's Ring",
        ring2="Ephedra Ring",
        back="Alaunus's Cape",
        waist="Witful Belt",
        legs="Piety Pantaloons",
        feet="Theo. Duckbills +1"
    }

    -- Regen
    sets.midcast.Regen = set_combine(sets.midcast['Enhancing Magic'], {
        -- head="Inyanga Tiara",
        body="Piety Bliaut +1",
        hands="Ebers Mitts +1",
        legs="Theo. Pant. +1",
    })
end

function precast(spell)
    if spell.action_type == 'Magic' then
        if spell.skill == 'Healing Magic' then
            if spell.english:startswith('Cure') then
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
            equip(sets.midcast.Cure)
        elseif spell.english:startswith('Regen') then
            equip(sets.midcast.Regen)
        end
    elseif spell.skill == 'Enhancing Magic' then
        equip(sets.midcast['Enhancing Magic'])
    end
end

function aftercast(spell)
    if player.status == 'Engaged' then
        equip(sets.engaged)
    else
        equip(sets.idle)
    end
end

function status_change(new,old)
    if new == 'Engaged' then
        equip(sets.engaged)
    else
        equip(sets.idle)
    end
end
