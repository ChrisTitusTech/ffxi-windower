-- Original: Motenten / Modified: Arislan

-------------------------------------------------------------------------------------------------------------------
--  Keybinds
-------------------------------------------------------------------------------------------------------------------

--  Modes:      [ F9 ]              Cycle Offense Modes
--              [ CTRL+F9 ]         Cycle Hybrid Modes
--              [ WIN+F9 ]          Cycle Weapon Skill Modes
--              [ F10 ]             Emergency -PDT Mode
--              [ ALT+F10 ]         Toggle Kiting Mode
--              [ F11 ]             Emergency -MDT Mode
--              [ F12 ]             Update Current Gear / Report Current Status
--              [ CTRL+F12 ]        Cycle Idle Modes
--              [ ALT+F12 ]         Cancel Emergency -PDT/-MDT Mode
--              [ WIN+A ]           AttackMode: Capped/Uncapped WS Modifier
--              [ WIN+C ]           Toggle Capacity Points Mode
--
--
--              (Global-Binds.lua contains additional non-job-related keybinds)

-------------------------------------------------------------------------------------------------------------------
-- Setup functions for this job.  Generally should not be modified.
-------------------------------------------------------------------------------------------------------------------

-- Initialization function for this job file.
function get_sets()
    mote_include_version = 2

    -- Load and initialize the include file.
    include('Mote-Include.lua')
    res = require 'resources'
end

-- Setup vars that are user-independent.
function job_setup()
    -- Add TH state
    include('Mote-TreasureHunter')
    state.TreasureMode:options('None','Tag')
    
    -- Initialize other states
    state.Buff = {}
    
    -- Add auto-hasso timer
    auto_hasso_timer = os.clock()

    no_swap_gear = S{"Warp Ring", "Dim. Ring (Dem)", "Dim. Ring (Holla)", "Dim. Ring (Mea)",
              "Trizek Ring", "Echad Ring", "Facility Ring", "Capacity Ring"}
    wyv_breath_spells = S{'Dia', 'Poison', 'Blaze Spikes', 'Protect', 'Sprout Smack', 'Head Butt', 'Cocoon',
        'Barfira', 'Barblizzara', 'Baraera', 'Barstonra', 'Barthundra', 'Barwatera'}
    wyv_elem_breath = S{'Flame Breath', 'Frost Breath', 'Sand Breath', 'Hydro Breath', 'Gust Breath', 'Lightning Breath'}

    lockstyleset = 4

end

-------------------------------------------------------------------------------------------------------------------
-- User setup functions for this job.  Recommend that these be overridden in a sidecar file.
-------------------------------------------------------------------------------------------------------------------

function user_setup()
    state.OffenseMode:options('Normal', 'Acc', 'STP')
    state.WeaponskillMode:options('Normal', 'Acc')
    state.HybridMode:options('Normal', 'DT')
    state.IdleMode:options('Normal', 'DT')

    state.AttackMode = M{['description']='Attack', 'Capped', 'Uncapped'}
    -- state.CP = M(false, "Capacity Points Mode")

    -- Additional local binds
    include('Global-Binds.lua') -- OK to remove this line

    send_command('bind ^` input /ja "Call Wyvern" <me>')
    send_command('bind !` input /ja "Spirit Link" <me>')
    send_command('bind @` input /ja "Dismiss" <me>')
    send_command('bind @a gs c cycle AttackMode')

    if player.sub_job == 'WAR' then
        send_command('bind !w input /ja "Defender" <me>')
    elseif player.sub_job == 'SAM' then
        send_command('bind !w input /ja "Hasso" <me>')
    end

    send_command('bind @w gs c toggle WeaponLock')
    send_command('bind @c gs c toggle CP')

    if player.sub_job == 'WAR' then
        send_command('bind ^numpad/ input /ja "Berserk" <me>')
        send_command('bind ^numpad* input /ja "Warcry" <me>')
        send_command('bind ^numpad- input /ja "Aggressor" <me>')
    elseif player.sub_job == 'SAM' then
        send_command('bind ^numpad/ input /ja "Meditate" <me>')
        send_command('bind ^numpad* input /ja "Sekkanoki" <me>')
        send_command('bind ^numpad- input /ja "Third Eye" <me>')
    end

    send_command('bind ^numpad7 input /ws "Camlann\'s Torment" <t>')
    send_command('bind ^numpad8 input /ws "Drakesbane" <t>')
    send_command('bind ^numpad4 input /ws "Stardiver" <t>')
    send_command('bind ^numpad5 input /ws "Geirskogul" <t>')
    send_command('bind ^numpad6 input /ws "Impulse Drive" <t>')
    send_command('bind ^numpad1 input /ws "Sonic Thrust" <t>')
    send_command('bind ^numpad2 input /ws "Leg Sweep" <t>')

    send_command('bind ^numpad0 input /ja "Jump" <t>')
    send_command('bind ^numpad. input /ja "High Jump" <t>')
    send_command('bind ^numpad+ input /ja "Spirit Jump" <t>')
    send_command('bind ^numpadenter input /ja "Soul Jump" <t>')
    send_command('bind ^numlock input /ja "Super Jump" <t>')

    select_default_macro_book()
    set_lockstyle()

    state.Auto_Kite = M(false, 'Auto_Kite')
    moving = false

    -- Add TH toggle
    send_command('bind f9 gs c cycle TreasureMode')
    
    -- Add auto-hasso toggle
    send_command('bind !h gs c auto_hasso')
    
    -- Add to your setup message
    add_to_chat(158,'F9: Toggle Treasure Hunter')
    add_to_chat(158,'Alt-h: Auto Hasso')
end

function user_unload()
    send_command('unbind ^`')
    send_command('unbind !`')
    send_command('unbind !w')
    send_command('unbind @`')
    send_command('unbind @a')
    -- send_command('unbind @c')
    send_command('unbind ^numpad/')
    send_command('unbind ^numpad*')
    send_command('unbind ^numpad-')
    send_command('unbind ^numpad7')
    send_command('unbind ^numpad8')
    send_command('unbind ^numpad4')
    send_command('unbind ^numpad5')
    send_command('unbind ^numpad6')
    send_command('unbind ^numpad1')
    send_command('unbind ^numpad2')
    send_command('unbind ^numpad0')
    send_command('unbind ^numpad.')
    send_command('unbind ^numpad+')
    send_command('unbind ^numpadenter')
    send_command('unbind ^numlock')

    send_command('unbind #`')
    send_command('unbind #1')
    send_command('unbind #2')
    send_command('unbind #3')
    send_command('unbind #4')
    send_command('unbind #5')
    send_command('unbind #6')
    send_command('unbind #7')
    send_command('unbind #8')
    send_command('unbind #9')
    send_command('unbind #0')
end

-- Define sets and vars used by this job file.
function init_gear_sets()

    ------------------------------------------------------------------------------------------------
    ---------------------------------------- Precast Sets ------------------------------------------
    ------------------------------------------------------------------------------------------------

    sets.precast.JA['Spirit Surge'] = {
        -- body="Ptero. Mail +3"
    }

    sets.precast.JA['Call Wyvern'] = {
        -- body="Ptero. Mail +3"
    }

    sets.precast.JA['Ancient Circle'] = {
        -- legs="Vishap Brais +3"
    }

    sets.precast.JA['Spirit Link'] = {
        head="Vishap Armet",
        -- hands="Pel. Vambraces +1",
        -- feet="Ptero. Greaves +3",
        -- ear1="Pratik Earring",
    }

    sets.precast.JA['Steady Wing'] = {
        -- legs="Vishap Brais +3",
        -- feet="Ptero. Greaves +3",
        -- neck="Chanoix's Gorget",
        -- ear1="Lancer's Earring",
        --ear2="Anastasi Earring",
        back="Updraft Mantle",
    }

    sets.precast.JA['Jump'] = {
        ammo="Hasty Pinion +1",
        head="Flam. Zucchetto +2",
        hands="Sulev. Gauntlets +2",
        legs="Sulev. Cuisses +2",
        feet="Flam. Gambieras +2",
        neck="Dgn. Collar +1",
        ear1="Brutal Earring",
        ear2="Cessance Earring",
        ring1="Chirich Ring",
        ring2="Moonbeam Ring",
        back={ name="Brigantia's Mantle", augments={'STR+20','Accuracy+20 Attack+20','"Dbl.Atk."+10','Damage taken-5%',}},
        waist="Ioskeha Belt",
    }

    sets.precast.JA['High Jump'] = sets.precast.JA['Jump']
    sets.precast.JA['Spirit Jump'] = sets.precast.JA['Jump']
    sets.precast.JA['Soul Jump'] = set_combine(sets.precast.JA['Jump'], {
        legs="Sulev. Cuisses +2"
    })
    sets.precast.JA['Super Jump'] = {}

    sets.precast.JA['Angon'] = {
        ammo="Angon", 
        -- hands="Ptero. Fin. G. +3"
    }

    -- Fast cast sets for spells
    sets.precast.FC = {
        ammo="Sapience Orb", --2
        --head="Carmine Mask +1", --14
        --body="Sacro Breastplate", --10
        --hands="Leyline Gloves", --8
        legs="Aya. Cosciales +2", --6
        -- feet="Carmine Greaves +1", --8
        --neck="Orunmila's Torque", --5
        ear1="Loquacious Earring", --2
        -- ear2="Enchntr. Earring +1", --2
        -- ring2="Weather. Ring +1", --6(4)
    }

    ------------------------------------------------------------------------------------------------
    ------------------------------------- Weapon Skill Sets ----------------------------------------
    ------------------------------------------------------------------------------------------------

    sets.precast.WS = {
        head="Flam. Zucchetto +2",
        body="Flamma Korazin +2",
        hands="Sulev. Gauntlets +2",
        legs="Sulev. Cuisses +2",
        feet="Sulev. Leggings +2",
        neck="Fotia Gorget",
        ear1="Moonshade Earring",
        ear2="Mache Earring",
        back={ name="Brigantia's Mantle", augments={'STR+20','Accuracy+20 Attack+20','"Dbl.Atk."+10','Damage taken-5%',}},
        waist="Fotia Belt",
    }

    sets.precast.WS.Acc = set_combine(sets.precast.WS, {
        head="Sulevia's Mask +2",
        hands="Sulev. Gauntlets +2",
        feet="Flam. Gambieras +2",
        ear1="Cessance Earring",
    })

    sets.precast.WS['Camlann\'s Torment'] = set_combine(sets.precast.WS, {
        neck="Dgn. Collar +1",
        waist="Ioskeha Belt",
    })

    sets.precast.WS['Camlann\'s Torment'].Acc = set_combine(sets.precast.WS['Camlann\'s Torment'], {})

    sets.precast.WS['Drakesbane'] = set_combine(sets.precast.WS, {
        head="Flam. Zucchetto +2",
        legs="Sulev. Cuisses +2",
        feet="Flam. Gambieras +2",
        ear1="Brutal Earring",
        ear2="Cessance Earring",
        ring1="Chirich Ring",
        waist="Ioskeha Belt",
        })

    sets.precast.WS['Geirskogul'] = set_combine(sets.precast.WS, {
        ear2="Mache Earring",
        back={ name="Brigantia's Mantle", augments={'STR+20','Accuracy+20 Attack+20','"Dbl.Atk."+10','Damage taken-5%',}},
    })

    sets.precast.WS['Geirskogul'].Acc = set_combine(sets.precast.WS['Geirskogul'], {})

    sets.precast.WS['Impulse Drive'] = set_combine(sets.precast.WS['Camlann\'s Torment'], {
        neck="Dgn. Collar +1",
        ear2="Moonshade Earring",
        back={ name="Brigantia's Mantle", augments={'STR+20','Accuracy+20 Attack+20','"Dbl.Atk."+10','Damage taken-5%',}},
        waist="Ioskeha Belt",
    })

    sets.precast.WS['Impulse Drive'].Acc = set_combine(sets.precast.WS['Impulse Drive'], {
        waist="Ioskeha Belt",
    })

    sets.precast.WS['Impulse Drive'].HighTP = set_combine(sets.precast.WS['Impulse Drive'], {
        head="Flam. Zucchetto +2",
        body="Flamma Korazin +2",
        --ear2="Ishvara Earring",
    })

    sets.precast.WS['Sonic Thrust'] = sets.precast.WS['Camlann\'s Torment']
    sets.precast.WS['Sonic Thrust'].Acc = sets.precast.WS['Camlann\'s Torment'].Acc

    sets.precast.WS['Stardiver'] = set_combine(sets.precast.WS, {
        neck="Fotia Gorget",
        ear1="Cessance Earring",
        ear2="Brutal Earring",
        ring1="Chirich Ring",
        waist="Fotia Belt",
    })

    sets.precast.WS['Stardiver'].Acc = set_combine(sets.precast.WS['Stardiver'], {
        -- head="Ptero. Armet +3",
        -- feet="Ptero. Greaves +3",
    })

    sets.precast.WS['Raiden Thrust'] = set_combine(sets.precast.WS, {
        -- ammo="Ghastly Tathlum +1",
        --body="Carm. Sc. Mail +1",
        --hands="Carmine Fin. Ga. +1",
        --ear1="Crematio Earring",
        ear2="Friomisi Earring",
        -- ring1="Shiva Ring +1",
        -- back="Argocham. Mantle",
    })

    sets.precast.WS['Thunder Thrust'] = sets.precast.WS['Raiden Thrust']

    sets.precast.WS['Leg Sweep'] = set_combine(sets.precast.WS, {
        body="Flamma Korazin +2",
        feet="Flam. Gambieras +2",
        ear1="Cessance Earring",
    })

    ------------------------------------------------------------------------------------------------
    ---------------------------------------- Midcast Sets ------------------------------------------
    ------------------------------------------------------------------------------------------------

    sets.midcast.HealingBreath = {
        neck="Dgn. Collar +1",
        back="Updraft Mantle",
        waist="Glassblower's Belt",
    }

    sets.midcast.ElementalBreath = {
        back="Updraft Mantle",
        waist="Glassblower's Belt",
    }

    ------------------------------------------------------------------------------------------------
    ----------------------------------------- Idle Sets --------------------------------------------
    ------------------------------------------------------------------------------------------------

    sets.idle = {
        main={ name="Gae Derg +1", augments={'Path: A',}},
        sub="Eletta Grip",
        ammo="Hasty Pinion +1",
        head="Flam. Zucchetto +2",
        body="Flamma Korazin +2",
        hands="Sulev. Gauntlets +2",
        legs="Sulev. Cuisses +2",
        feet="Flam. Gambieras +2",
        neck="Dgn. Collar +1",
        waist="Ioskeha Belt",
        left_ear="Brutal Earring",
        right_ear="Cessance Earring",
        left_ring="Chirich Ring",
        right_ring="Moonbeam Ring",
        back={ name="Brigantia's Mantle", augments={'STR+20','Accuracy+20 Attack+20','"Dbl.Atk."+10','Damage taken-5%',}},
        }

    sets.idle.DT = {
        ammo="Staunch Tathlum", -- Good
        head="Sulevia's Mask +2", -- Good
        body="Sulevia's Plate. +2", -- Good
        hands="Sulev. Gauntlets +2", -- Good
        legs="Sulev. Cuisses +2", -- Good
        feet="Flam. Gambieras +2",
        neck="Dgn. Collar +1",
        left_ear="Cessance Earring",
        right_ear={ name="Odnowa Earring +1", augments={'Path: A',}}, -- Good
        left_ring="Vocane Ring", -- Good
        right_ring="Moonbeam Ring", -- Good
        back={ name="Brigantia's Mantle", augments={'STR+20','Accuracy+20 Attack+20','"Dbl.Atk."+10','Damage taken-5%',}},
    }

    sets.idle.Pet = set_combine(sets.idle, {
        -- body="Vishap Mail +3",
        -- hands="Ptero. Fin. G. +3",
        -- feet="Ptero. Greaves +3",
        neck="Dgn. Collar +1",
        -- ear1="Enmerkar Earring",
        -- ear2="Anastasi Earring",
        waist="Ioskeha Belt",
    })

    sets.idle.Town = set_combine(sets.idle, {
  
        })

    sets.idle.Weak = sets.idle.DT


    ------------------------------------------------------------------------------------------------
    ---------------------------------------- Defense Sets ------------------------------------------
    ------------------------------------------------------------------------------------------------

    sets.defense.PDT = sets.idle.DT
    sets.defense.MDT = sets.idle.DT

    ------------------------------------------------------------------------------------------------
    ---------------------------------------- Engaged Sets ------------------------------------------
    ------------------------------------------------------------------------------------------------

    sets.engaged = {
        main={ name="Gae Derg +1", augments={'Path: A',}},
        sub="Eletta Grip",
        ammo="Hasty Pinion +1",
        head="Flam. Zucchetto +2",
        body="Flamma Korazin +2",
        hands="Sulev. Gauntlets +2",
        legs="Sulev. Cuisses +2",
        feet="Flam. Gambieras +2",
        neck="Dgn. Collar +1",
        waist="Ioskeha Belt",
        left_ear="Brutal Earring",
        right_ear="Cessance Earring",
        left_ring="Chirich Ring",
        right_ring="Moonbeam Ring",
        back={ name="Brigantia's Mantle", augments={'STR+20','Accuracy+20 Attack+20','"Dbl.Atk."+10','Damage taken-5%',}},
    }

    sets.engaged.Acc = set_combine(sets.engaged, {
        ear2="Mache Earring",
        waist="Ioskeha Belt",
        legs="Sulev. Cuisses +2",
    })

    sets.engaged.STP = set_combine(sets.engaged, {
        legs="Sulev. Cuisses +2",
        ear2="Cessance Earring",
        back={ name="Brigantia's Mantle", augments={'STR+20','Accuracy+20 Attack+20','"Dbl.Atk."+10','Damage taken-5%',}},
    })

    -- Update the hybrid sets to match
    sets.engaged.DT = set_combine(sets.engaged, sets.engaged.Hybrid)
    sets.engaged.Acc.DT = set_combine(sets.engaged.Acc, sets.engaged.Hybrid)
    sets.engaged.STP.DT = set_combine(sets.engaged.STP, sets.engaged.Hybrid)

    ------------------------------------------------------------------------------------------------
    ---------------------------------------- Hybrid Sets -------------------------------------------
    ------------------------------------------------------------------------------------------------

    sets.engaged.Hybrid = {
        ring1="Vocane Ring",
        ring2="Moonbeam Ring",
        }

    ------------------------------------------------------------------------------------------------
    ---------------------------------------- Special Sets ------------------------------------------
    ------------------------------------------------------------------------------------------------

    sets.buff.Doom = {
        -- neck="Nicander's Necklace", --20
        -- ring1={name="Eshmun's Ring", bag="wardrobe3"}, --20
        -- ring2={name="Eshmun's Ring", bag="wardrobe4"}, --20
        -- waist="Gishdubar Sash", --10
        }

    -- Add TH set
    sets.TreasureHunter = set_combine(sets.engaged, {
        ammo="Per. Lucky Egg",
    })

end

-------------------------------------------------------------------------------------------------------------------
-- Job-specific hooks for standard casting events.
-------------------------------------------------------------------------------------------------------------------

function job_precast(spell, action, spellMap, eventArgs)
    -- Wyvern Commands
    if spell.name == 'Dismiss' and pet.hpp < 100 then
        eventArgs.cancel = true
        add_to_chat(50, 'Cancelling Dismiss! ' ..pet.name.. ' is below full HP! [ ' ..pet.hpp.. '% ]')
    elseif wyv_breath_spells:contains(spell.english) or (spell.skill == 'Ninjutsu' and player.hpp < 33) then
        equip(sets.precast.HealingBreath)
    end
    -- Jump Overrides
    --if pet.isvalid and player.main_job_level >= 77 and spell.name == "Jump" then
    --    eventArgs.cancel = true
    --    send_command('@input /ja "Spirit Jump" <t>')
    --end

    --if pet.isvalid and player.main_job_level >= 85 and spell.name == "High Jump" then
    --    eventArgs.cancel = true
    --    send_command('@input /ja "Soul Jump" <t>')
    --end
end

function job_post_precast(spell, action, spellMap, eventArgs)
    if spell.type == 'WeaponSkill' then
        if spell.english == 'Impulse Drive' and player.tp > 2000 then
           equip(sets.precast.WS['Impulse Drive'].HighTP)
        end
    end
end

function job_pet_midcast(spell, action, spellMap, eventArgs)
    if spell.name:startswith('Healing Breath') or spell.name == 'Restoring Breath' then
        equip(sets.midcast.HealingBreath)
    elseif wyv_elem_breath:contains(spell.english) then
        equip(sets.midcast.ElementalBreath)
    end
end

-------------------------------------------------------------------------------------------------------------------
-- Job-specific hooks for non-casting events.
-------------------------------------------------------------------------------------------------------------------

function job_buff_change(buff,gain)
    -- If we gain or lose any haste buffs, adjust which gear set we target.
--    if buffactive['Reive Mark'] then
--        if gain then
--            equip(sets.Reive)
--            disable('neck')
--        else
--            enable('neck')
--        end
--    end

    if buff == "doom" then
        if gain then
            equip(sets.buff.Doom)
            send_command('@input /p Doomed.')
             disable('ring1','ring2','waist')
        else
            enable('ring1','ring2','waist')
            handle_equipping_gear(player.status)
        end
    end

    if buff == 'Hasso' and not gain and player.sub_job == 'SAM' and not areas.Cities:contains(world.area) then
        add_to_chat(167, 'Recasting Hasso!')
        send_command('input /ja "' .. buff .. '" <me>')
    end

end


-------------------------------------------------------------------------------------------------------------------
-- User code that supplements standard library decisions.
-------------------------------------------------------------------------------------------------------------------

-- Handle equipping gear when engaging
function job_status_change(newStatus, oldStatus, eventArgs)
    if newStatus == 'Engaged' then
        -- Check for Hasso when engaging in combat
        if player.sub_job == 'SAM' and not buffactive['Hasso'] then
            send_command('wait 1; input /ja "Hasso" <me>')
        end
    end
end

-- This handles the actual gear swaps
function customize_melee_set(meleeSet)
    if state.TreasureMode.value == 'Tag' and not info.tagged_mobs[player.target.id] then
        meleeSet = set_combine(meleeSet, sets.TreasureHunter)
    end
    return meleeSet
end

function job_update(cmdParams, eventArgs)
    handle_equipping_gear(player.status)
end

function get_custom_wsmode(spell, action, spellMap)
    local wsmode
    if state.OffenseMode.value == 'MidAcc' or state.OffenseMode.value == 'HighAcc' then
        wsmode = 'Acc'
    end

    return wsmode
end

-- Function to display the current relevant user state when doing an update.
-- Set eventArgs.handled to true if display was handled, and you don't want the default info shown.
function display_current_job_state(eventArgs)
    local cf_msg = ''
    if state.CombatForm.has_value then
        cf_msg = ' (' ..state.CombatForm.value.. ')'
    end

    local m_msg = state.OffenseMode.value
    if state.HybridMode.value ~= 'Normal' then
        m_msg = m_msg .. '/' ..state.HybridMode.value
    end

    local am_msg = '(' ..string.sub(state.AttackMode.value,1,1).. ')'

    local ws_msg = state.WeaponskillMode.value

    local d_msg = 'None'
    if state.DefenseMode.value ~= 'None' then
        d_msg = state.DefenseMode.value .. state[state.DefenseMode.value .. 'DefenseMode'].value
    end

    local i_msg = state.IdleMode.value

    local msg = ''
    
    add_to_chat(002, '| ' ..string.char(31,210).. 'Melee' ..cf_msg.. ': ' ..string.char(31,001)..m_msg.. string.char(31,002)..  ' |'
        ..string.char(31,207).. ' WS' ..am_msg.. ': ' ..string.char(31,001)..ws_msg.. string.char(31,002)..  ' |'
        ..string.char(31,004).. ' Defense: ' ..string.char(31,001)..d_msg.. string.char(31,002)..  ' |'
        ..string.char(31,008).. ' Idle: ' ..string.char(31,001)..i_msg.. string.char(31,002)..  ' |'
        ..string.char(31,002)..msg)

    eventArgs.handled = true
end

-------------------------------------------------------------------------------------------------------------------
-- Utility functions specific to this job.
-------------------------------------------------------------------------------------------------------------------

function job_self_command(cmdParams, eventArgs)
    if cmdParams[1]:lower() == 'auto_hasso' then
        auto_hasso()
    end
end

function auto_hasso()   
    if player.status == 'Engaged' and player.sub_job == 'SAM' and not buffactive['Hasso'] and not midaction() and not areas.Cities:contains(world.area) then
        send_command('input /ja "Hasso" <me>')
    end
end

function gearinfo(cmdParams, eventArgs)
    if cmdParams[1] == 'gearinfo' then
        if type(cmdParams[4]) == 'string' then
            if cmdParams[4] == 'true' then
                moving = true
            elseif cmdParams[4] == 'false' then
                moving = false
            end
        end
        if not midaction() then
            job_update()
        end
    end
end

function check_gear()
    if no_swap_gear:contains(player.equipment.left_ring) then
        disable("ring1")
    else
        enable("ring1")
    end
    if no_swap_gear:contains(player.equipment.right_ring) then
        disable("ring2")
    else
        enable("ring2")
    end
end

windower.register_event('zone change',
    function()
        if no_swap_gear:contains(player.equipment.left_ring) then
            enable("ring1")
            equip(sets.idle)
        end
        if no_swap_gear:contains(player.equipment.right_ring) then
            enable("ring2")
            equip(sets.idle)
        end
    end
)

-- Select default macro book on initial load or subjob change.
function select_default_macro_book()
    -- Default macro set/book: (set, book)
    --if player.sub_job == 'SAM' then
        set_macro_page(2, 1)
    --else
        --set_macro_page(2, 8)
    --end
end

function set_lockstyle()
    send_command('wait 2; input /lockstyleset ' .. lockstyleset)
end

function status_change(new, old)
    -- Check for Hasso when engaging in combat (only outside of towns)
    if new == 'Engaged' and player.sub_job == 'SAM' and not buffactive['Hasso'] and not areas.Cities:contains(world.area) then
        send_command('wait 1; input /ja "Hasso" <me>')
    end
end