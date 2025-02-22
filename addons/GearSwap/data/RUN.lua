-------------------------------------------------------------------------------------------------------------------
-- Initialization function that defines sets and variables to be used.
-------------------------------------------------------------------------------------------------------------------
--  RUN.lua 
--  author: Orestes
--  6/10/2014 

-- Initialization function for this job file.
function get_sets()
	-- Load and initialize the include file.
    mote_include_version = 2
	include('Mote-Include.lua')
    -- Additional local binds
    include('Global-Binds.lua')
    state.Runes = {"Tellus","Unda","Flabra","Ignis","Gelus","Sulpor","Lux","Tenebrae"}
    
    -- Initialize the timer
    auto_rune_timer = os.clock()
    auto_hasso_timer = os.clock()
    send_command('wait 1; gs c check_auto')
end

-- Setup vars that are user-independent.
function job_setup()
    state.Buff.Battuta = buffactive.battuta or false
    state.Buff.Vallation = buffactive.vallation or false
    state.Buff.Valiance = buffactive.valiance or false
    state.Buff.Swordplay = buffactive.swordplay or false
    state.Buff.Hasso = buffactive.hasso or false
    
    -- Add auto-rune timer
    auto_rune_timer = os.clock()
    auto_hasso_timer = os.clock()
    
    -- Change AutoCheck to use modes
    state.AutoCheck = M{['description']='Auto Check', 'On', 'Off'}
end


-- Setup vars that are user-dependent.  Can override this function in a sidecar file.
function user_setup()
    include('Mote-TreasureHunter')
    state.TreasureMode:options('None','Tag')
    
    -- Initialize defense modes - these are set in Mote-Include already
    -- We just need to set the starting mode
    state.PhysicalDefenseMode:set('PDT')
    state.MagicalDefenseMode:set('MDT')
    state.DefenseMode:set('Magical')
    
    send_command('bind f9 gs c cycle TreasureMode')
    send_command('bind ^[ input /lockstyle on')
    send_command('bind ` input /ws "Dimidiation" <t>')
    send_command('bind ^` input /ws "Resolution" <t>')
    send_command('bind numpad0 input /ma "Foil" <me>')
    send_command('bind numpad. input /ma "Crusade" <me>')
    send_command('bind numpad3 input /ma "Refresh" <me>')
    send_command('bind numpad1 input /ma "Phalanx" <me>')
    send_command('bind !numpad0 input /ja "Swipe" <t>')
    send_command('bind !numpad. input /ja "Swordplay" <me>')
    send_command('bind !numpad3 input /ma "Temper" <me>')
    send_command('bind !numpad1 input /ja "Rayke" <t>')
    send_command('bind ^numpad0 input /ja "Pflug" <me>')
    send_command('bind ^numpad. input /ja "Elemental Sforzo" <me> ')
    send_command('bind ^numpad3 input /ma "Shell V" <me>')
    send_command('bind ^numpad1 input /ma "Protect IV" <me>')
    

    if player.sub_job == 'DRK' then
        send_command('bind ^numpad/ input /ja "Souleater" <me>')
        send_command('bind ^numpad* input /ja "Weapon Bash" <t>')
        send_command('bind ^numpad- input /ja "Last Resort" <me>')
    elseif player.sub_job == 'SAM' then
        send_command('bind ^numpad/ input /ja "Meditate" <me>')
        send_command('bind ^numpad* input /ja "Sekkanoki" <me>')
        send_command('bind ^numpad- input /ja "Third Eye" <me>')
    end
    
    select_default_macro_book()

    send_command('bind !` gs c auto_rune')
    send_command('bind !h gs c auto_hasso')
    send_command('bind !a gs c autocheck')
    add_to_chat(123,'RUN.lua loaded')
    add_to_chat(122,'Auto Check: ' .. state.AutoCheck.value)
    add_to_chat(158,[[
        Numpad Controls:
        0: Foil           Alt-0: Swipe          Ctrl-0: Pflug
        .: Crusade        Alt-.: Swordplay      Ctrl-.: Elemental Sforzo  
        3: Refresh        Alt-3: Temper         Ctrl-3: Shell V
        1: Phalanx        Alt-1: Rayke          Ctrl-1: Protect IV
        Alt-`: Auto Rune
        Alt-a: Toggle Auto Check
        
        Sub-job Controls (Ctrl):
        DRK: /: Souleater  *: Weapon Bash  -: Last Resort
        SAM: /: Meditate   *: Sekkanoki    -: Third Eye
             Alt-h: Auto Hasso
    ]])

    -- Add this line near the start of user_setup()
    state.OffenseMode:options('Normal', 'TP')
    
    -- Add this keybind (you can change F12 to another key if you prefer)
    send_command('bind F12 gs c cycle OffenseMode')
    
    -- Add this to your existing setup message
    add_to_chat(158,'F12: Cycle Offense Mode: ' .. state.OffenseMode.value)
    send_command('asc on')

    -- Add weapon options
    state.WeaponSet = M{['description']='Weapon Set', 'Epeolatry', 'Axe'}
    
    -- Add keybind for weapon swap (using Windows key + W, but you can change this)
    send_command('bind @w gs c cycle WeaponSet')
    
    -- Add to your existing setup message
    add_to_chat(158,'Win+W: Cycle Weapon Set: ' .. state.WeaponSet.value)
end


-- Called when this job file is unloaded (eg: job change)
function file_unload()
    if binds_on_unload then
    	binds_on_unload()
    end
    
    send_command('unbind ^[')
    send_command('unbind F12')
    send_command('unbind @w')
end

-- Define sets and vars used by this job file.
function init_gear_sets()
    --------------------------------------
    -- Start defining the sets
    --------------------------------------
    
    -- Precast sets to enhance JAs
    sets.precast.JA['Vivacious Pulse'] = { head="Erilaz Galea +1" }  
    sets.precast.JA.Swordplay = { hands="Futhark Mitons +1" }
    sets.precast.Effusion = {}

    -- Effusions
    sets.precast.Effusion.Lunge = { 

    }
    sets.precast.Effusion.Swipe = sets.precast.Effusion.Lunge
    sets.precast.Effusion.Gambit = { hands="Runeist Mitons" }
    sets.precast.Effusion.Rayke = { feet="Futhark Boots +1" }

    -- Wards
    sets.precast.Ward = {}
    sets.precast.Ward.Battuta = { head="Futhark Bandeau" }
    sets.precast.Ward.Pflug = { feet="Runeist Bottes" }
    sets.precast.Ward.Vallation = { body="Runeist Coat +1", legs="Futhark Trousers" }
    sets.precast.Ward.Valiance = sets.precast.Ward.Vallation

    -- Fast Cast
    sets.precast.FC = {
        ammo="Sapience orb",
        head="Runeist Bandeau +1",
        --neck="Orunmila's Torque",
        ear1="Loquacious Earring",
        --body="Vanir Cotehardie",
        --hands="Thaumas Gloves",
        ring1="Prolix Ring",
        legs="Aya. Cosciales +2",
    }
    sets.precast.FC['Enhancing Magic'] = set_combine(sets.precast.FC, {
        legs="Futhark Trousers"
    })

    -- Magic
    sets.midcast['Enhancing Magic'] = {
        neck="Colossus's Torque",
        --ear1="Augmenting Earring",
        --ear2="Andoaa Earring",
        --body="Manasa Chasuble",
        hands="Runeist Mitons",
        back="Merciful Cape",
        --waist="Olympus Sash",
        legs="Futhark Trousers"
    }
    
    -- Recast Timers for spells not otherwise specified
    sets.midcast.FastRecast = {
    	--head="Felistris Mask",
        body="Runeist Coat +1",
    	--hands="Qaaxo Mitaines",
        --waist="Ninurta's Sash",
        legs="Runeist Trousers",
        --feet="Iuitl Gaiters +1"
    }
    
    -- Waltz set (chr and vit)
    sets.precast.Waltz = {}
    
    
    
    --sets.Kiting = {feet="Hermes' Sandals"}
    
    -- Engaged sets
    sets.engaged = {
        head="Erilaz Galea +2",
        body="Turms Harness",
        hands="Turms Mittens",
        legs="Eri. Leg Guards +2",
        feet="Turms Leggings",
        neck={ name="Futhark Torque", augments={'Path: A',}},
        waist="Ioskeha Belt",
        left_ear={ name="Odnowa Earring +1", augments={'Path: A',}},
        right_ear="Cessance Earring",
        back={ name="Ogma's Cape", augments={'HP+60','Eva.+20 /Mag. Eva.+20','Mag. Evasion+10','"Store TP"+10','Parrying rate+5%',}},
    }

    sets.TreasureHunter = set_combine(sets.engaged, {
        ammo="Per. Lucky Egg",
    })

    sets.defense.PDT = {
        sub="Refined Grip +1",
        ammo="Staunch Tathlum",
        head="Erilaz Galea +2",
        body="Ayanmo Corazza +2",
        hands="Turms Mittens",
        waist="Flume Belt",
        left_ear={ name="Odnowa Earring +1", augments={'Path: A',}},
        right_ear="Ethereal Earring",
        left_ring="Moonbeam Ring",
        right_ring="Moonbeam Ring",
    }
    
    sets.engaged.PDT = set_combine(sets.defense.PDT, sets.engaged)

    sets.defense.MDT = {
        sub="Refined Grip +1",
        ammo="Staunch Tathlum",
        head="Erilaz Galea +2",
        left_ear={ name="Odnowa Earring +1", augments={'Path: A',}},
        left_ring="Moonbeam Ring",
        right_ring="Moonbeam Ring",
    }

    sets.engaged.MDT = set_combine(sets.defense.MDT, sets.engaged)

    sets.idle = sets.engaged
    sets.resting = sets.engaged
    	
    -- Weaponskill sets --
    sets.precast.WS = {
        ammo="Hagneia Stone",
        body="Ayanmo Corazza +2",
        hands="Aya. Manopolas +2",
        legs={ name="Samnuha Tights", augments={'STR+9','DEX+8','"Dbl.Atk."+2','"Triple Atk."+2',}},
        neck="Fotia Gorget",
        waist="Fotia Belt",
        left_ear="Brutal Earring",
        right_ear="Cessance Earring",
        right_ring="Chirich Ring",
        back={ name="Ogma's Cape", augments={'HP+60','Eva.+20 /Mag. Eva.+20','Mag. Evasion+10','"Store TP"+10','Parrying rate+5%',}},
    }

    sets.precast.WS.Resolution = set_combine(sets.precast.WS, {
        
    })
    
    sets.precast.WS.Dimidiation = set_combine(sets.precast.WS, {
        
    })

    sets.engaged.TP = {
        sub="Eletta Grip",
        ammo="Hagneia Stone",
        head="Adhemar Bonnet",
        body="Ayanmo Corazza +2",
        hands={ name="Herculean Gloves", augments={'Accuracy+25','"Triple Atk."+3','Attack+14',}},
        legs={ name="Samnuha Tights", augments={'STR+9','DEX+8','"Dbl.Atk."+2','"Triple Atk."+2',}},
        feet={ name="Herculean Boots", augments={'Accuracy+20','"Triple Atk."+3','AGI+8','Attack+10',}},
        neck="Asperity Necklace",
        waist="Ioskeha Belt",
        left_ear="Brutal Earring",
        right_ear="Cessance Earring",
        left_ring="Epona's Ring",
        right_ring="Chirich Ring",
        back={ name="Ogma's Cape", augments={'HP+60','Eva.+20 /Mag. Eva.+20','Mag. Evasion+10','"Store TP"+10','Parrying rate+5%',}},
    }

    sets.naked = {main=empty,sub=empty,range=empty,ammo=empty,
                head=empty,neck=empty,ear1=empty,ear2=empty,
                body=empty,hands=empty,ring1=empty,ring2=empty,
                back=empty,waist=empty,legs=empty,feet=empty}
    
    -- Add weapon sets near the start of init_gear_sets()
    sets.weapons = {}
    sets.weapons.Epeolatry = {
        main="Epeolatry",
    }
    sets.weapons.Axe = {
        main="Hepatizon Axe +1",
    }
end


-------------------------------------------------------------------------------------------------------------------
-- Job-specific hooks that are called to process player actions at specific points in time.
-------------------------------------------------------------------------------------------------------------------

-- Set eventArgs.handled to true if we don't want any automatic target handling to be done.
function job_pretarget(spell, action, spellMap, eventArgs)
    if state.Buff[spell.english] ~= nil then
        state.Buff[spell.english] = true
    end
end

function job_precast(spell, action, spellMap, eventArgs)
end
-- Run after the default precast() is done.
-- eventArgs is the same one used in job_precast, in case information needs to be persisted.
function job_post_precast(spell, action, spellMap, eventArgs)
	if spell.type:lower() == 'weaponskill' then
		--if state.Buff.Sengikori then
		--	equip(sets.buff.Sengikori)
		--end
	end
    if spell.name == 'Spectral Jig' and buffactive.sneak then
            -- If sneak is active when using, cancel before completion
            send_command('cancel 71')
    end
end


-- Set eventArgs.handled to true if we don't want any automatic gear equipping to be done.
function job_midcast(spell, action, spellMap, eventArgs)
	if spell.action_type == 'Magic' then
		equip(sets.midcast.FastRecast)
	end
end

-- Run after the default midcast() is done.
-- eventArgs is the same one used in job_midcast, in case information needs to be persisted.
function job_post_midcast(spell, action, spellMap, eventArgs)
    --if state.Buff['Third Eye'] then
    --    if state.DefenseMode == 'PDT' then
    --        equip(sets.thirdeye)
    --    end
    --end
end

-- Set eventArgs.handled to true if we don't want any automatic gear equipping to be done.
function job_aftercast(spell, action, spellMap, eventArgs)
	if state.Buff[spell.english] ~= nil then
		state.Buff[spell.english] = not spell.interrupted or buffactive[spell.english]
	end
end

function midcast(spell,action)
    if spell.english == 'Sneak' or spell.english == 'Spectral Jig' or spell.english:startswith('Monomi') and spell.target.type == 'SELF' then
        send_command('cancel 71')
    end
end

-- Modify the default idle set after it was constructed.
function customize_idle_set(idleSet)
	return idleSet
end

-- Modify the default melee set after it was constructed.
function customize_melee_set(meleeSet)
    -- First apply the weapon set
    meleeSet = set_combine(meleeSet, sets.weapons[state.WeaponSet.value])
    
    -- Then apply TP set if needed
    if state.OffenseMode.value == 'TP' then
        return set_combine(meleeSet, sets.engaged.TP)
    end
    return meleeSet
end

-------------------------------------------------------------------------------------------------------------------
-- Customization hooks for idle and melee sets, after they've been automatically constructed.
-------------------------------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------------------------------
-- General hooks for other events.
-------------------------------------------------------------------------------------------------------------------
function job_self_command(cmdParams, eventArgs)
    if cmdParams[1]:lower() == 'rune' then
        send_command('@input /ja '..state.Runes.value..' <me>')
    elseif cmdParams[1]:lower() == 'auto_rune' then
        auto_rune()
    elseif cmdParams[1]:lower() == 'auto_hasso' then
        auto_hasso()
    elseif cmdParams[1]:lower() == 'check_auto' then
        if state.AutoCheck.value == 'On' then
            if os.clock() - auto_rune_timer > 3 then
                auto_rune()
                auto_rune_timer = os.clock()
            end
            if os.clock() - auto_hasso_timer > 3 then
                auto_hasso()
                auto_hasso_timer = os.clock()
            end
            send_command('wait 3; gs c check_auto')
        end
    elseif cmdParams[1]:lower() == 'autocheck' then
        add_to_chat(122,'Auto Check: ' .. state.AutoCheck.value)
        if state.AutoCheck.value == 'Off' then
            state.AutoCheck:set('On')
            send_command('gs c check_auto')
        elseif state.AutoCheck.value == 'On' then
            state.AutoCheck:set('Off')
        end
    end
end
-- Called when a player gains or loses a buff.
-- buff == buff gained or lost
-- gain == true if the buff was gained, false if it was lost.
function job_buff_change(buff, gain)
    if state.Buff[buff] ~= nil then
        state.Buff[buff] = gain
        handle_equipping_gear(player.status)
    end
end

-------------------------------------------------------------------------------------------------------------------
-- User code that supplements self-commands.
-------------------------------------------------------------------------------------------------------------------

-- Called by the 'update' self-command, for common needs.
-- Set eventArgs.handled to true if we don't want automatic equipping of gear.
function job_update(cmdParams, eventArgs)
	--state.CombatForm = get_combat_form()
    --state.CombatWeapon = get_combat_weapon()
end

function status_change(new, old)
    send_command('input /lockstyle on')
    
    -- Check for Hasso when engaging in combat
    if new == 'Engaged' and player.sub_job == 'SAM' and not buffactive['Hasso'] then
        send_command('wait 1; input /ja "Hasso" <me>')
    end
end

-- Set eventArgs.handled to true if we don't want the automatic display to be run.
function display_current_job_state(eventArgs)

end

-------------------------------------------------------------------------------------------------------------------
-- Utility functions specific to this job.
-------------------------------------------------------------------------------------------------------------------

-- Select default macro book on initial load or subjob change.
function select_default_macro_book()
    -- Default macro set/book
    if player.sub_job == 'SAM' then
    	set_macro_page(8, 1)
    else
    	set_macro_page(8, 1)
    end
end

function auto_rune()
    if midaction() or player.status ~= 'Engaged' then return end
    
    if state.DefenseMode.value == 'Magical' then
        if not buffactive['Ignis'] then
            send_command('input /ja "Ignis" <me>')
        elseif not buffactive['Gelus'] then
            send_command('input /ja "Gelus" <me>')
        elseif not buffactive['Flabra'] then
            send_command('input /ja "Flabra" <me>')
        end
    else
        if not buffactive['Tellus'] then
            send_command('input /ja "Tellus" <me>')
        elseif not buffactive['Unda'] then
            send_command('input /ja "Unda" <me>')
        elseif not buffactive['Sulpor'] then
            send_command('input /ja "Sulpor" <me>')
        end
    end
end

function auto_hasso()   
    if player.status == 'Engaged' and player.sub_job == 'SAM' and not buffactive['Hasso'] and not midaction() then
        send_command('input /ja "Hasso" <me>')
    end
end