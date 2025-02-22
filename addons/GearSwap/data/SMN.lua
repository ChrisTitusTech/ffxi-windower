-------------------------------------------------------------------------------------------------------------------
-- Setup functions for this job.  Generally should not be modified.
-------------------------------------------------------------------------------------------------------------------

-- Initialization function for this job file.
function get_sets()
    mote_include_version = 2
    include('Mote-Include.lua')
end

-- Setup vars that are user-independent.
function job_setup()
    state.Buff = {}
end

-- Setup vars that are user-dependent.
function user_setup()
    -- Initialize state
    state.WeaponSet = M{['description']='Weapon Set', 'Queller', 'Wind', 'Scepter'}
    
    -- Bind weapon cycle command
    send_command('bind @w gs c cycle WeaponSet')
end

-- Called when this job file is unloaded (eg: job change)
function file_unload()
    send_command('unbind @w')
end

-------------------------------------------------------------------------------------------------------------------
-- Job-specific hooks that are called to process player actions at specific points in time.
-------------------------------------------------------------------------------------------------------------------

function init_gear_sets()
    -- Weapon Sets
    sets.weapons = {}
    sets.weapons.Queller = {
        main="Queller Rod",
        sub="Vox Grip"
    }
    sets.weapons.Wind = {
        main={ name="Vayuvata III", augments={'Wind Affinity: Avatar perp. cost+6','Wind Affin.: "Blood Pact" delay +12',}},
        sub="Vox Grip"
    }
    sets.weapons.Scepter = {
        main="Scepter Staff"
    }

    -- Normal/Idle set focusing on refresh and PDT
    sets.idle = {
        ammo="Sancus Sachet",
        head="Tali'ah Turban +1",
        body="Tali'ah Manteel +2",
        hands="Tali'ah Gages +1",
        legs="Tali'ah Sera. +1",
        feet="Con. Pigaches",
        neck="Caller's Pendant",
        waist="Lucidity Sash",
        left_ear="Moonshade Earring",
        right_ear="Ethereal Earring",
        left_ring="Varar Ring",
        right_ring="Evoker's Ring",
        back="Campestres's Cape"
    }

    -- Precast set for fast cast
    sets.precast = {}
    sets.precast.FC = {
        ammo="Sancus Sachet",
        head="Tali'ah Turban +1",
        body="Tali'ah Manteel +2",
        hands="Tali'ah Gages +1",
        legs="Tali'ah Sera. +1",
        feet="Con. Pigaches",
        neck="Sanctity Necklace",
        waist="Witful Belt",
        left_ear="Loquac. Earring",
        right_ear="Ethereal Earring",
        left_ring="Prolix Ring",
        right_ring="Kishar Ring",
        back="Campestres's Cape"
    }

    -- BP Timer Reduction
    sets.precast.BP = {
        ammo="Sancus Sachet",
        head="Tali'ah Turban +1",
        body="Tali'ah Manteel +2",
        hands="Tali'ah Gages +1",
        legs="Tali'ah Sera. +1",
        feet="Con. Pigaches",
        neck="Caller's Pendant",
        waist="Lucidity Sash",
        left_ear="Moonshade Earring",
        right_ear="Ethereal Earring",
        left_ring="Varar Ring",
        right_ring="Evoker's Ring",
        back="Campestres's Cape"
    }

    -- Midcast for BP damage
    sets.midcast = {}
    sets.midcast.BP = {
        ammo="Sancus Sachet",
        head="Tali'ah Turban +1",
        body="Tali'ah Manteel +2",
        hands="Tali'ah Gages +1",
        legs="Tali'ah Sera. +1",
        feet="Con. Pigaches",
        neck="Sanctity Necklace",
        waist="Lucidity Sash",
        left_ear="Moonshade Earring",
        right_ear="Ethereal Earring",
        left_ring="Varar Ring",
        right_ring="Evoker's Ring",
        back="Campestres's Cape"
    }

    -- Avatar's Favor
    sets.midcast.Favor = {
        ammo="Sancus Sachet",
        head="Tali'ah Turban +1",
        body="Tali'ah Manteel +2",
        hands="Tali'ah Gages +1",
        legs="Tali'ah Sera. +1",
        feet="Con. Pigaches",
        neck="Caller's Pendant",
        waist="Lucidity Sash",
        left_ear="Ethereal Earring",
        right_ear="Loquac. Earring",
        left_ring="Evoker's Ring",
        right_ring="Varar Ring",
        back="Campestres's Cape"
    }

    -- Summoning Skill
    sets.midcast.Pet = {
        ammo="Sancus Sachet",
        head="Tali'ah Turban +1",
        body="Tali'ah Manteel +2",
        hands="Tali'ah Gages +1",
        legs="Tali'ah Sera. +1",
        feet="Con. Pigaches",
        neck="Caller's Pendant",
        waist="Lucidity Sash",
        left_ear="Ethereal Earring",
        right_ear="Loquac. Earring",
        left_ring="Evoker's Ring",
        right_ring="Varar Ring",
        back="Campestres's Cape"
    }
end

-- Function to handle cycling weapons
function job_self_command(cmdParams, eventArgs)
    if cmdParams[1] == 'cycle' and cmdParams[2] == 'WeaponSet' then
        state.WeaponSet:cycle()
        add_to_chat(158,'Weapon Set: '..state.WeaponSet.value)
        equip(sets.weapons[state.WeaponSet.value])
    end
end

function precast(spell)
    equip(sets.weapons[state.WeaponSet.value])
    
    if spell.type == "BloodPactRage" or spell.type == "BloodPactWard" then
        equip(sets.precast.BP)
    else
        equip(sets.precast.FC)
    end
end

function midcast(spell)
    equip(sets.weapons[state.WeaponSet.value])
    
    if spell.type == "BloodPactRage" or spell.type == "BloodPactWard" then
        equip(sets.midcast.BP)
    elseif spell.type == "SummonerPact" then
        equip(sets.midcast.Pet)
    end
end

function aftercast(spell)
    equip(sets.weapons[state.WeaponSet.value])
    
    if pet.isvalid then
        equip(sets.midcast.Favor)
    else
        equip(sets.idle)
    end
end

function status_change(new,old)
    equip(sets.weapons[state.WeaponSet.value])
    
    if new == 'Idle' then
        equip(sets.idle)
    end
end

function pet_change(pet,gain)
    equip(sets.weapons[state.WeaponSet.value])
    
    if gain then
        equip(sets.midcast.Favor)
    else
        equip(sets.idle)
    end
end

function customize_melee_set(meleeSet)
    return set_combine(meleeSet, sets.weapons[state.WeaponSet.value])
end