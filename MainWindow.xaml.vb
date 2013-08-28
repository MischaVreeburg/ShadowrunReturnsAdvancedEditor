Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data
Imports System.Data.DataSetExtensions
Imports System.IO
Imports System.Linq
Imports System.Reflection.Assembly
Imports System.Text.RegularExpressions
Imports isogame
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports ProtoBuf
Imports Xceed.Wpf.Toolkit

Public Class ContentPackMetaData
    Implements INotifyPropertyChanged, IEqualityComparer(Of ContentPackMetaData), IEquatable(Of ContentPackMetaData), IComparable(Of ContentPackMetaData)

#Region "Fields"
    Private _Location As String
    Private _Compressed As Boolean
#End Region
#Region "Fields"
    Public Property Location As String
        Get
            Return _Location
        End Get
        Set(value As String)
            _Location = value
        End Set
    End Property
    Public Property Compressed As Boolean
        Get
            Return _Compressed
        End Get
        Set(value As Boolean)
            _Compressed = value
        End Set
    End Property

#End Region
#Region "Methods"
    Sub New()

    End Sub

    Private Sub OnPropertyChanged(ByVal PropertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(PropertyName))
    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Shared Operator <>(ByVal A As ContentPackMetaData, ByVal B As ContentPackMetaData) As Boolean
        Dim Found As Boolean = False
        If A.Location = B.Location Then
            Found = True
        End If
        Return Not Found
    End Operator

    Public Shared Operator =(ByVal A As ContentPackMetaData, ByVal B As ContentPackMetaData) As Boolean
        Dim Found As Boolean = False
        If A.Location = B.Location Then
            Found = True
        End If
        Return Found
    End Operator

    Public Function Equals2(A As ContentPackMetaData, B As ContentPackMetaData) As Boolean Implements IEqualityComparer(Of ContentPackMetaData).Equals
        Dim Found As Boolean = False
        If A.Location = B.Location Then
            Found = True
        End If
        Return Found
    End Function

    Public Function GetHashCode2(obj As ContentPackMetaData) As Integer Implements IEqualityComparer(Of ContentPackMetaData).GetHashCode
        Return obj.Location.GetHashCode()
    End Function

    Public Overloads Function Equals(other As ContentPackMetaData) As Boolean Implements IEquatable(Of ContentPackMetaData).Equals
        If other Is Nothing Then Return False
        If Me.Location = other.Location Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        If obj Is Nothing Then Return False

        Dim ContentPackMetaData As ContentPackMetaData = TryCast(obj, ContentPackMetaData)
        If ContentPackMetaData Is Nothing Then
            Return False
        Else
            Return Equals(ContentPackMetaData)
        End If
    End Function

    Public Overloads Function CompareTo(other As ContentPackMetaData) As Integer Implements IComparable(Of ContentPackMetaData).CompareTo

        ' If other is not a valid object reference, this instance is greater. 
        If other Is Nothing Then Return 1

        Return Me.Location.CompareTo(other.Location)
    End Function


#End Region

End Class

Class WindowSRRItemEditor

#Region "Editable Objects"

    Public ContentPackList As New ObservableCollection(Of isogame.ProjectDef)
    Public ContentPackLocationsList As New ObservableCollection(Of ContentPackMetaData)
    Public DataManifestList As New ObservableCollection(Of isogame.Manifest)
    Public Shared AbilityList As New ObservableCollection(Of isogame.AbilityDef)
    Public Shared ItemList As New ObservableCollection(Of isogame.ItemDef)
    Public Shared ModesList As New ObservableCollection(Of isogame.ModeDef)
    Public Shared ModesNamesList As New ObservableCollection(Of String)

    Private Shared _DurationFxScriptList As New ObservableCollection(Of String)(New String() {"GenericStatusHitReaction", "BurningStatusHitReaction", "LightningStatusHitReaction", "MatrixErosionStatusHitReaction"})

    Private Shared _EquipmentSheetIdList As New ObservableCollection(Of String)(New String() {"Spirit Abomination F$ Gear", "Spirit Air F$ Gear", "Spirit AIr F$ Gear", "Spirit Earth F$ Gear", "Spirit Fire F$ Gear", "Spirit Nature F$ Gear", "Spirit Toxic F$ Gear", "Spirit Water F$ Gear", "Class A Attack Drone Gear", "Class B Attack Drone Gear", "Class C Attack Drone Gear", "Class S Attack Drone Gear", "Class A Support Drone Gear", "Class B Support Drone Gear", "Class C Support Drone Gear", "Class S Support Drone Gear", "ESP Assassin F$ Gear", "ESP Attacker F$ Gear", "ESP Kamikaze F$ Gear", "ESP Shield F$ Gear", "Bug Spirit Pure Powers"})

    Private Shared _CharacterUINameList As New ObservableCollection(Of String)(New String() {"Apocalypse", "Wind Dancer", "Earthquake", "Inferno", "Primeval", "Pestilence", "Typhoon", "Steel Lynx Drone", "Strato-9 Drone", "Doberman Drone", "Guardian Drone", "Sundowner Drone", "Robo-Doc Drone", "Smoker Drone", "Wolfhound Drone", "Assassin Program", "Attack Program", "Exploder Program", "Shield Program", "Pure Insect Spirit"})

    Private Shared _DefDeckingWeaponList As New ObservableCollection(Of String)(New String() {})

    Private Shared _CharacterSheetIdList As New ObservableCollection(Of String)(New String() {"Spirit Abomination", "Spirit Air", "Spirit Earth", "Spirit Fire", "Spirit Nature", "Spirit Toxic", "Spirit Water", "DroneAttackA_SteelLynx", "DroneAttackB_Strato9", "DroneAttackC_Doberman", "DroneAttackS_Guardian", "DroneSupportA_Sundowner", "DroneSupportB_RoboDoc", "DroneSupportC_Smoker", "DroneSupportS_Wolfhound", "ESP Assassin", "ESP Attacker", "ESP Kamikaze", "ESP Shield", "Bug Spirit Pure"})

    Private Shared _CharacterPrefabIdList As New ObservableCollection(Of String)(New String() {"SpiritAbomination", "SpiritAir", "SpiritEarth", "SpiritFire", "SpiritNature", "SpiritToxic", "SpiritWater", "DroneWheeledCrawler", "DroneHoverCorp", "DroneWheeled", "DroneGuardian", "DroneHoverRoto", "DroneWheeledMedic", "DroneWheeledSmoker", "DroneHoverWolfhound", "SpiritESPAssassin", "SpiritESPAttacker", "SpiritESPKamikaze", "SpiritESPShield", "BugPureSpirit"})

    Private Shared _EquipmentPrefabList As New ObservableCollection(Of String)(New String() {"AK97Rifle", "TacticalAssaultRifle", "CorpTurret", "deck_conceptcar", "deck_dumptruck", "deck_ninja", "deck_cyber6", "RiggerRemote", "AresPredatorPistol", "RevolverPistol", "Broom", "TacticalShotgun", "StreetSweeperShotgun", "IngramSMG", "TacticalSMG", "Bat", "Crowbar", "Machete", "Rebar", "Axe", "Katana", "JadeSword", "BugGun", "TedSword"})

    Private Shared _StackingCategoriesList As New ObservableCollection(Of String)(New String() {"Acid", "AI", "Aim", "Air", "AP", "Armor", "Aura", "Body", "Bug", "Cover", "CyberwareMove", "Disarm", "DocWagon", "Dodge", "Fire", "Hellstorm", "HP", "Insect Totem", "Jazz", "Kamikaze", "KillingHands", "Lightning", "Mana", "MatrixCharge1", "MatrixCharge2", "Move", "Nitro", "Quickness", "Silence", "ToHitRed", "PitezelBuff"})

    Private Shared _FXScriptList As New ObservableCollection(Of String)(New String() {"AcidBurnStatus", "AcidVenomStatus", "AdeptDefenseBuffStatus", "AdeptOffenseBuffStatus", "AirBarrierStatus", "BurningStatus", "DocWagonBeaconStatus", "FireBarrierStatus", "FogAOE", "GenericSpellAOE", "GenericSpellStatus", "GuardStatus", "HeatWaveStatus", "HellstormStatus", "InsectBuffStatus", "KillerHandsStatus", "LightningBarrierStatus", "LightningStatus", "MatrixBarrierStatus", "MatrixErosionStatus", "ProtectStatus", "ShadowAOE"})

    Private Shared _IconList As New ObservableCollection(Of String)(New String() {"icon_accident", "icon_acidburst", "icon_acidstream1", "icon_acidstream2", "icon_aim", "icon_aim1", "icon_aim2", "icon_aim3", "icon_airbarrier1", "icon_airbarrier2", "icon_airbarrier3", "icon_airspirit", "icon_armor1", "icon_armor2", "icon_armor3", "icon_assassin", "icon_attacker", "icon_aura", "icon_auto", "icon_balllightning1", "icon_balllightning2", "icon_banish", "icon_barkskin", "icon_beartotem", "icon_bigwave", "icon_blackhammer", "icon_blaster", "icon_blaster2", "icon_blaster3", "icon_blindness", "icon_burst", "icon_cattotem", "icon_chainshot", "icon_chifocus", "icon_chionslaught", "icon_cleave", "icon_confusion", "icon_counterstrike", "icon_coyotetotem", "icon_cyber_arm_bunny", "icon_cyber_arm_tank", "icon_cyber_bonelacing_kevlar", "icon_cyber_bonelacing_plastic", "icon_cyber_datajack", "icon_cyber_dermalplatingMK1", "icon_cyber_dermalplatingMK1", "icon_cyber_dermalplatingMK1_alpha", "icon_cyber_eyevisionmag", "icon_cyber_eyevisionmag_alpha", "icon_cyber_leg", "icon_cyber_leg_alpha", "icon_cyber_wiredreflexes", "icon_deadeye", "icon_deck_fairlightexcaliber", "icon_deck_fuchivirtualx", "icon_deck_renrakukraftwork", "icon_deck_sony", "icon_deckertargeting", "icon_deckertargeting2", "icon_deckertargeting3", "icon_degrade", "icon_degrade2", "icon_degrade3", "icon_disarm", "icon_disarmranged", "icon_disarmspirit", "icon_disease", "icon_disintegrate1", "icon_disintegrate1", "icon_disintegrate1", "icon_disintegrate2", "icon_disintegrate3", "icon_dispel", "icon_dispelmagic", "icon_distraction1", "icon_distraction2", "icon_distraction3", "icon_docwagon", "icon_docwagongold", "icon_docwagonplatnum", "icon_doubletap", "icon_drone_activate", "icon_drone_doberman", "icon_drone_exit", "icon_drone_gaurdian", "icon_drone_gun", "icon_drone_holdposition", "icon_drone_mortar", "icon_drone_return", "icon_drone_robodoc", "icon_drone_smoker", "icon_drone_steellynx", "icon_drone_strato9", "icon_drone_sundowner", "icon_drone_wolfhound", "icon_dronerepairkit1", "icon_dronerepairkit2", "icon_dronerepairkit3", "icon_eagletotem", "icon_earthspirit", "icon_electrocurrent", "icon_erosion", "icon_erosion2", "icon_erosion3", "icon_exploder", "icon_fireball", "icon_fireball1", "icon_fireball2", "icon_firebarrier1", "icon_firebarrier2", "icon_firebarrier3", "icon_firespirit", "icon_firewall", "icon_firewall2", "icon_firewall3", "icon_flamethrower1", "icon_flamethrower2", "icon_flamethrower3", "icon_flash", "icon_fogB", "icon_frag", "icon_glue", "icon_grenade_aresconcussion", "icon_grenade_areshefrag", "icon_grenade_cavalierconcussion", "icon_grenade_cavalierfrag", "icon_grenade_fichetticoncussion", "icon_grenade_fichettifrag", "icon_grenade_phosphorus", "icon_grenade_smoke", "icon_guard", "icon_gun_ak98", "icon_gun_aresalphacombatgun", "icon_gun_arespredator", "icon_gun_benelliraffaello", "icon_gun_berettamodel70", "icon_gun_browningmaxpower", "icon_gun_buggun", "icon_gun_cavalierdeputy", "icon_gun_ceskablackscorpion", "icon_gun_coltm23", "icon_gun_coltmanhunter", "icon_gun_colttz110", "icon_gun_defiancet250", "icon_gun_enfieldas7", "icon_gun_fichettisecurity500", "icon_gun_fnhar", "icon_gun_hecklerkochg12a3z", "icon_gun_hk227", "icon_gun_ingramsmartgun", "icon_gun_mossbergcmdt", "icon_gun_remington990", "icon_gun_rugersuperwarhawk", "icon_gun_rugerthunderbolt", "icon_gun_sckmodel100", "icon_gun_semopalvz88v", "icon_gun_streetsweeper", "icon_gun_uzii111", "icon_handgun", "icon_haste1", "icon_haste2", "icon_haste3", "icon_haste4", "icon_headshot", "icon_heal1", "icon_heal2", "icon_heal3", "icon_heal4", "icon_heal5", "icon_heatwave", "icon_hellstormbarrier1", "icon_hellstormbarrier2", "icon_hellstormbarrier3", "icon_inferno", "icon_jazz", "icon_jetstream", "icon_kamikaze", "icon_killer", "icon_killer2", "icon_killer3", "icon_killerhands", "icon_killjoy", "icon_killjoy2", "icon_kneecap", "icon_lightningbarrier1", "icon_lightningbarrier2", "icon_lightningbarrier3", "icon_lightningbolt1", "icon_lightningbolt2", "icon_lightningbolt3", "icon_magic", "icon_magicresistance1", "icon_magicresistance2", "icon_magicresistance3", "icon_manaball1", "icon_manaball2", "icon_manaball3", "icon_manabolt1", "icon_manabolt2", "icon_manabolt3", "icon_manapunch", "icon_manastatic", "icon_medic", "icon_medic2", "icon_medic3", "icon_medkit", "icon_medkit2", "icon_medkit3", "icon_melee", "icon_melee_axe", "icon_melee_Baseballbat", "icon_melee_crowbar", "icon_melee_jadesword", "icon_melee_katanaforged", "icon_melee_katanaknockoff", "icon_melee_machette", "icon_melee_rebar", "icon_melee_teddysword", "icon_mindwipe", "icon_mysticarmor", "icon_naturespirit", "icon_nitro", "icon_outfit_adeptbase", "icon_outfit_adeptkunai", "icon_outfit_adeptninja", "icon_outfit_adeptstarter", "icon_outfit_adeptstealth", "icon_outfit_adeptstreetmonk", "Icon_outfit_backer_Bowling_Dashyoung", "icon_outfit_backer_dahlman_alnur", "icon_outfit_backer_doochin_stephenwatashima", "icon_outfit_backer_fiona", "icon_outfit_backer_fry_joshuamorgan", "icon_outfit_backer_guillermo_trollsuit", "icon_outfit_backer_herbert_okano", "icon_outfit_backer_krell_akumalshibara", "icon_outfit_backer_lawford_rayquandry", "icon_outfit_backer_mersman_nixie", "icon_outfit_backer_nagel_bryanlitelightner", "icon_outfit_backer_rc_greymitigator", "icon_outfit_backer_smith_malekcreid", "icon_outfit_backers_palsson_ashton", "icon_outfit_classlessstarter", "icon_outfit_deckerfab", "icon_outfit_deckerfedora", "icon_outfit_deckerprettyboy", "icon_outfit_deckerstarter", "icon_outfit_deckerstreet", "icon_outfit_disguise_corporate", "icon_outfit_disguise_cultistub", "icon_outfit_disguise_ganger", "icon_outfit_disguise_Janitor", "icon_outfit_disguise_lonestar", "icon_outfit_disguise_scientist", "icon_outfit_magecasual", "icon_outfit_magedark", "icon_outfit_mageredriding", "icon_outfit_mageslick", "icon_outfit_magestarter", "icon_outfit_magetraditional", "icon_outfit_riggerflightsuit", "icon_outfit_riggergolden", "icon_outfit_riggerhawaiian", "icon_outfit_riggerstarter", "icon_outfit_riggertoolbelt", "icon_outfit_riggertrench", "icon_outfit_samuraibunny", "Icon_outfit_samuraibunny", "icon_outfit_samuraibunny_maskless", "icon_outfit_samuraimilitary", "icon_outfit_samuraineo", "icon_outfit_samuraipunk", "icon_outfit_samuraistarter", "icon_outfit_shamanpendant", "icon_outfit_shamanskirted", "icon_outfit_shamanstarter", "icon_outfit_shamantotemcoat", "icon_outfit_shamanurban", "icon_painresistance", "icon_pebble", "icon_petrify", "icon_pommelstrike", "icon_power", "icon_powerbolt", "icon_powerbolt1", "icon_powerbolt2", "icon_powerbolt3", "icon_protect", "icon_quickstrike", "icon_quietbomb", "icon_raccoontotem", "icon_rifle", "icon_roundhousekick", "icon_shadow", "icon_shield", "icon_shield2", "icon_shield3", "icon_silence", "icon_single", "icon_slash", "icon_slo-mo", "icon_slow", "icon_slow1", "icon_slow2", "icon_slow3", "icon_smoke", "icon_snapshot", "icon_sniffer", "icon_sniffer2", "icon_sniffer3", "icon_spiritmelee", "icon_spiritranged", "icon_spiritranged", "icon_sprayandpray", "icon_stab", "icon_stonebody", "icon_stride", "icon_stunballB", "icon_stunbolt", "icon_summon_air_spirit", "icon_suppression", "icon_thrust", "icon_waterspirit", "icon_wideload", "icon_wind", "icon_wind", "icon_wound", "icon_concussion", "icon_wild_swing"})

    Private Shared _FxNamesList As New ObservableCollection(Of String)(New String() {"AcidBurstThrowAction", "AcidHitReaction", "AcidSpellPrepare", "AcidStreamThrowAction", "AdeptDefenseBuffHitReaction", "AdeptOffenseBuffHitReaction", "AdeptQuickstrikeAction", "AdeptSpellBuffAction", "BallLightningThrowAction", "BasiliskClawSwipeAction", "BasiliskPetrifyCastAction", "BasiliskPetrifyHitReaction", "BearTotemCastAction", "BluntWeaponSwingAction", "BoundSpiritSummonAction", "BugBombAction", "BugCrushAction", "BugMeleeAction", "BugPureSpitAction", "BugSpiritMeleeAction", "BugSpitAction", "BugVenomHitReaction", "CatTotemCastAction", "ClawSwipeHitReaction", "ConjureAirBarrierAction", "ConjureFireAction", "ConjureFireBarrierAction", "ConjureLightningBarrierAction", "CoyoteTotemCastAction", "CudgelHitReaction", "CudgelPunchAction", "DeckerJackinAction", "DeckerSpellCastAction", "DevourBiteAction", "DisarmAction", "DisarmHitReaction", "DisarmHit Reaction", "DiseaseBurstAction", "DocWagonAction", "DroneFireGrenadeFireAction", "DroneFlashGrenadeFireAction", "DroneFragGrenadeFireAction", "DroneMortarGrenadeFireAction", "DroneSmokeGrenadeFireAction", "EagleTotemCastAction", "ESPSummonAction", "FireArrowFireAction", "FireballHitReaction", "FireballThrowAction", "FireGrenadeThrowAction", "FireHitReaction", "FireSpellPrepare", "FlamethrowerThrowAction", "FlashGrenadeThrowAction", "FragGrenadeThrowAction", "FragGrenadeThrowAction", "GenericEmptyReaction", "GenericHitReaction", "GenericMatrixMissReaction", "GenericMissReaction", "GenericSpellBuffHitReaction", "GenericSpellCastAction", "GenericSpellDebuffHitReaction", "GenericSpellDeBuffHitReaction", "GenericSpellHitReaction", "GenericSpellMissReaction", "GenericSpellPrepare", "GenericSpellThrowAction", "GhoulFistPunchAction", "GunBurstFireAction", "GunBurstFireActionEmpty", "GunBurstHitReaction", "GunBurstMissReaction", "GunChainFireAction", "GunDoubleTapFireAction", "GunFullAutoFireAction", "GunHitReaction", "GunMissReaction", "GunReloadAction", "GunReloadReaction", "GunSingleFireAction", "HealSpellHitReaction", "HeatWaveCastAction", "HellhoundChompAction", "HellhoundFlameBreathAction", "HydroDaggerShootAction", "InsecticideShotgunFireAction", "InsecticideShotgunHitReaction", "InsecticideShotgunMissReaction", "InsectTotemCastAction", "InteractionAction", "InteractionAnimateAction", "InteractionReaction", "LightningBoltThrowAction", "LightningHitReaction", "LightningSpellPrepare", "lightningSpellPrepare", "MagicUnarmedPunchAction", "ManaBallThrowAction", "ManaBoltThrowAction", "MatrixBarrierCastAction", "MatrixBlasterThrowAction", "MatrixBuffHitReaction", "MatrixCastAction", "MatrixCastMissReaction", "MatrixDebuffHitReaction", "MatrixErosionPrepare", "MatrixErosionThrowAction", "MatrixHealReaction", "MatrixKillerThrowAction", "MatrixNodeHacking", "MatrixSpellHitReaction", "MatrixSpellMissReaction", "MatrixSpellPrepare", "MatrixSpellThrowAction", "MatrixStunThrowAction", "MedkitHitReaction", "MedkitUseAction", "PebbleCastAction", "PoisonDartsHitReaction", "PoisonDartsShootAction", "RacoonTotemCastAction", "RiggerJackinAction", "RiggerJackoutAction", "RockFistHitReaction", "RockFistPunchAction", "ShotgunBurstFireAction", "ShotgunFireAction", "ShotgunHitReaction", "ShotgunMissReaction", "SilencedGunBurstFireAction", "SilencedGunBurstFireActionEmpty", "SilencedGunChainFireAction", "SilencedGunDoubleTapFireAction", "SilencedGunSingleAction", "SilencedGunSingleFireAction", "SmokeGrenadeThrowAction", "SpiritRefreshAction", "SpiritRefreshReaction", "StunBallThrowAction", "StunBoltThrowAction", "StunSpellPrepare", "SwordCleaveAction", "SwordDoubleSwingAction", "SwordHitReaction", "SwordMissReaction", "SwordSwingAction", "SwordTripleSwingAction", "UnarmedHitReaction", "UnarmedMissReaction", "UnarmedPunchAction", "UnarmedRoundhouseAction", "WindBlastShootAction"})

    Private Shared _GearBundleList As New ObservableCollection(Of String)(New String() {"DisguiseBrotherhood", "DisguiseGanger", "DisguiseJanitor", "DisguiseLonestar", "DisguiseSalaryman", "DisguiseScientist", "outfitadeptcombatvest", "outfitadeptcombatvestalt", "outfitadeptkunai", "outfitadeptkunaialt", "outfitadeptninja", "outfitadeptninjaalt", "outfitadeptstarter", "outfitadeptstealth", "outfitadeptstealthalt", "outfitadeptstreetmonk", "outfitadeptstreetmonkalt", "outfitbacker", "outfitclasslessstarter", "outfitdeckerfab", "outfitdeckerfabalt", "outfitdeckerfedora", "outfitdeckerfedoraalt", "outfitdeckerjacket", "outfitdeckerjacketalt", "outfitdeckerstarter", "outfitdeckerstreet", "outfitdeckerstreetalt", "outfitmagecasual", "outfitmagecasualalt", "outfitmageDark", "outfitmageDarkalt", "outfitmageredridinghood", "outfitmageredridinghoodalt", "outfitmageslick", "outfitmageslickalt", "outfitmagestarter", "outfitmagetraditional", "outfitmagetraditionalalt", "outfitriggerflightsuit", "outfitriggerflightsuitalt", "outfitriggergolden", "outfitriggergoldenalt", "outfitriggerhawaiianshirt", "outfitriggerhawaiianshirtalt", "outfitriggerstarter", "outfitriggertoolbelt", "outfitriggertoolbeltalt", "outfitriggertrenchcoat", "outfitriggertrenchcoatalt", "outfitsamuraibunny", "outfitsamuraibunnyalt", "outfitsamuraimilitary", "outfitsamuraimilitaryalt", "outfitsamuraipunk", "outfitsamuraipunkalt", "outfitsamuraistarter", "outfitsamuraitrenchcoat", "outfitsamuraitrenchcoatalt", "outfitshamanpendant", "outfitshamanpendantalt", "outfitshamanskirted", "outfitshamanskirtedalt", "outfitshamanstarter", "outfitshamantotemcoat", "outfitshamantotemcoatalt", "outfitshamanurban", "outfitshamanurbanalt"})

    Private Shared _OutfitTextureList As New ObservableCollection(Of String)(New String() {"adept_combatvest", "adept_combatvest_alt", "adept_kunai", "adept_kunai_alt", "adept_ninja", "adept_ninja_alt", "adept_starter", "adept_stealth", "adept_stealth_alt", "adept_streetmonk", "adept_streetmonk_alt", "backer_bowling", "backer_dahlman", "backer_doochin", "backer_fiona", "backer_fry", "backer_guillermo", "backer_herbert", "backer_krell", "backer_lawford", "backer_mersmann", "backer_nagel", "backer_palsson", "backer_rc", "backer_smith", "cyber_1_arm", "cyber_2_arm", "decker_fab", "decker_fab_alt", "decker_fedora", "decker_fedora_alt", "decker_jacket", "decker_jacket_alt", "decker_starter", "decker_street", "decker_street_alt", "disguise_ganger", "disguise_janitor", "disguise_lonestar", "disguise_salaryman", "disguise_scientist", "disguise_ubmember", "gear_classless_starter", "mage_casual", "mage_casual_alt", "mage_dark", "mage_dark_alt", "mage_redridinghood", "mage_redridinghood_alt", "mage_slick", "mage_slick_alt", "mage_starter", "mage_traditional", "mage_traditional_alt", "rigger_flightsuit", "rigger_flightsuit_alt", "rigger_golden", "rigger_golden_alt", "rigger_hawaiian", "rigger_hawaiian_alt", "rigger_starter", "rigger_toolbelt", "rigger_toolbelt_alt", "rigger_trench", "rigger_trench_alt", "samurai_bunny", "samurai_bunny_alt", "samurai_military", "samurai_military_alt", "samurai_punk", "samurai_punk_alt", "samurai_starter", "samurai_trench", "samurai_trench_alt", "shaman_pendant", "shaman_pendant_alt", "shaman_skirted", "shaman_skirted_alt", "shaman_starter", "shaman_totemcoat", "shaman_totemcoat_alt", "shaman_urban", "shaman_urban_alt"})

    Private Shared _GearPrefabList As New ObservableCollection(Of String)(New String() {"gear_adept_combatvest", "gear_adept_combatvest_alt", "gear_adept_kunai", "gear_adept_kunai_alt", "gear_adept_ninja", "gear_adept_ninja_alt", "gear_adept_stealth", "gear_adept_stealth_alt", "gear_adept_streetmonk", "gear_adept_streetmonk_alt", "gear_backer_bowling", "gear_backer_dahlman", "gear_backer_fiona", "gear_backer_fry", "gear_backer_guillermo", "gear_backer_herbert", "gear_backer_krell", "gear_backer_lawford", "gear_backer_mersmann", "gear_backer_nagel", "gear_backer_palsson", "gear_backer_rc", "gear_backer_smith", "gear_classless_starter", "gear_decker_fab", "gear_decker_fab_alt", "gear_decker_fedora", "gear_decker_fedora_alt", "gear_decker_jacket", "gear_decker_jacket_alt", "gear_decker_starter", "gear_decker_street", "gear_decker_street_alt", "gear_disguise_ganger", "gear_disguise_lonestar", "gear_disguise_scientist", "gear_disguise_ubmember", "gear_mage_casual", "gear_mage_casual_alt", "gear_mage_dark", "gear_mage_dark_alt", "gear_mage_redridinghood", "gear_mage_redridinghood_alt", "gear_mage_slick", "gear_mage_slick_alt", "gear_mage_starter", "gear_mage_traditional", "gear_mage_traditional_alt", "gear_rigger_flightsuit", "gear_rigger_flightsuit_alt", "gear_rigger_golden", "gear_rigger_golden_alt", "gear_rigger_hawaiian", "gear_rigger_hawaiian_alt", "gear_rigger_starter", "gear_rigger_toolbelt", "gear_rigger_toolbelt_alt", "gear_rigger_trench", "gear_rigger_trench_alt", "gear_samurai_bunny", "gear_samurai_bunny_alt", "gear_samurai_military", "gear_samurai_military_alt", "gear_samurai_punk", "gear_samurai_punk_alt", "gear_samurai_starter", "gear_samurai_trench", "gear_samurai_trench_alt", "gear_shaman_pendant", "gear_shaman_pendant_alt", "gear_shaman_skirted", "gear_shaman_skirted_alt", "gear_shaman_starter", "gear_shaman_totemcoat", "gear_shaman_totemcoat_alt", "gear_shaman_urban", "gear_shaman_urban_alt"})

    Private Shared _SortingGroupList As New ObservableCollection(Of String)(New String() {"Backer", "Barriers", "Bound Spirits", "Chi Casting (Adept)", "Conjuring (Shaman)", "Critter Powers", "Cyberdecks", "CyberwareArms", "CyberwareBody", "CyberwareEyes", "CyberwareJack", "CyberwareLegs", "Disguises", "Drones", "Drugs", "ESPs", "Grenades", "Healing", "items", "Matrix Weapons", "Melee", "Outfits", "Pistols", "Player Outfits", "Programs", "Quest", "Rifles", "Shotguns", "SMGs", "Spellcasting (Mage)", "Spirit Powers", "Totems", "Turret Weapons"})

    Private Shared _PreReqList As New ObservableCollection(Of String)(New String() {"Item.CORE_ATTRIBUTE >= 3", "Item.CORE_ATTRIBUTE >= 4", "Item.CORE_SKILL < 5", "Item.CORE_SKILL > 4", "Item.CORE_SKILL > 6", "Item.CORE_SKILL > 7", "Item.CORE_SKILL > 8", "Item.CORE_SKILL > 9", "Item.CORE_SKILL >= 1", "Item.CORE_SKILL >= 3", "Item.CORE_SKILL >= 4", "Item.CORE_SKILL >= 6", "Item.CORE_SPECIALIZATION < 5", "Item.CORE_SPECIALIZATION > 0", "Item.CORE_SPECIALIZATION > 1", "Item.CORE_SPECIALIZATION > 2", "Item.CORE_SPECIALIZATION > 3", "Item.CORE_SPECIALIZATION > 4", "Item.CORE_SPECIALIZATION > 5", "Item.CORE_SPECIALIZATION > 7", "Item.CORE_SPECIALIZATION >= 1", "Item.CORE_SPECIALIZATION >= 3", "Item.CORE_SPECIALIZATION >= 4", "Item.CORE_SPECIALIZATION >= 5", "Item.CORE_SPECIALIZATION >= 7", "Item.CORE_SPECIALIZATION >= 8", "Item.FLAG.WEAPON_CUR_AMMO > 0", "Item.FLAG.WEAPON_CUR_AMMO > 1", "Item.FLAG.WEAPON_CUR_AMMO > 2", "Item.FLAG.WEAPON_CUR_AMMO > 3", "Item.FLAG.WEAPON_CUR_AMMO > 5", "Item.FLAG.WEAPON_CUR_AMMO > 7", "Item.FLAG.WEAPON_CUR_AMMO > 9", "Player.ATTRIBUTE.Attribute_Force < 3", "Player.ATTRIBUTE.Attribute_Force < 5", "Player.ATTRIBUTE.Attribute_Force > 2", "Player.ATTRIBUTE.Attribute_Force > 4", "Player.CONDITION.Condition_MatrixCharge1 == 0", "Player.CONDITION.Condition_MatrixCharge1 == 1", "Player.CONDITION.Condition_MatrixCharge2 == 0", "Player.CONDITION.Condition_MatrixCharge2 == 1", "Player.FLAG.CAN_SUMMON_DRONES == 1", "Player.FLAG.CAN_SUMMON_ESP == 1", "Player.FLAG.CAN_SUMMON_SPIRITS == 1", "Player.FLAG.DRONES_ACTIVE > 0", "Player.FLAG.HAS_DATAJACK == 1", "Player.FLAG.MASTER_DRONES_ACTIVE > 1", "Player.FLAG.SLAVES_CAN_RETURN > 0", "Player.FLAG.SPIRITS_ACTIVE < 2", "Player.FLAG.SPIRITS_ACTIVE > 0", "Player.SKILL.Skill_ChiCasting >= 1", "Player.SKILL.Skill_ChiCasting >= 10", "Player.SKILL.Skill_ChiCasting >= 2", "Player.SKILL.Skill_ChiCasting >= 3", "Player.SKILL.Skill_ChiCasting >= 4", "Player.SKILL.Skill_ChiCasting >= 5", "Player.SKILL.Skill_ChiCasting >= 6", "Player.SKILL.Skill_ChiCasting >= 7", "Player.SKILL.Skill_ChiCasting >= 8", "Player.SKILL.Skill_ChiCasting >= 9", "Player.SKILL.Skill_CloseCombat >= 1", "Player.SKILL.Skill_CloseCombat >= 3", "Player.SKILL.Skill_CloseCombat >= 4", "Player.SKILL.Skill_CloseCombat >= 5", "Player.SKILL.Skill_Conjuring >= 1", "Player.SKILL.Skill_Conjuring >= 2", "Player.SKILL.Skill_Conjuring >= 3", "Player.SKILL.Skill_Conjuring >= 4", "Player.SKILL.Skill_Conjuring >= 5", "Player.SKILL.Skill_Conjuring >= 6", "Player.SKILL.Skill_Conjuring >= 8", "Player.SKILL.Skill_Decking < 4", "Player.SKILL.Skill_Decking < 6", "Player.SKILL.Skill_Decking >= 1", "Player.SKILL.Skill_Decking >= 2", "Player.SKILL.Skill_Decking >= 3", "Player.SKILL.Skill_Decking >= 4", "Player.SKILL.Skill_Decking >= 5", "Player.SKILL.Skill_Decking >= 6", "Player.SKILL.Skill_Decking >= 7", "Player.SKILL.Skill_RangedCombat >= 1", "Player.SKILL.Skill_RangedCombat >= 3", "Player.SKILL.Skill_RangedCombat >= 4", "Player.SKILL.Skill_RangedCombat >= 5", "Player.SKILL.Skill_RangedCombat >= 6", "Player.SKILL.Skill_Spellcasting >= 1", "Player.SKILL.Skill_Spellcasting >= 2", "Player.SKILL.Skill_Spellcasting >= 3", "Player.SKILL.Skill_Spellcasting >= 4", "Player.SKILL.Skill_Spellcasting >= 5", "Player.SKILL.Skill_Spellcasting >= 6", "Player.SKILL.Skill_SpiritSummoning >= 1", "Player.SKILL.Skill_SpiritSummoning >= 2", "Player.SKILL.Skill_SpiritSummoning >= 3", "Player.SKILL.Skill_SpiritSummoning >= 4", "Player.SKILL.Skill_SpiritSummoning >= 5", "Player.SKILL.Skill_SpiritSummoning >= 6"})

    Private Shared _CooldownCategoryList As New ObservableCollection(Of String)

    'is list of gameEffect functions => assembly Assembly-CSharp, namespace: none (isogame.actionresult)
    Private Shared _DamageFunctionList As New ObservableCollection(Of String)(New String() {"buffTarget", "buffTargetNotSelf", "disarmDamage", "ExecuteActivateCounterstrike", "ExecuteActivateDrone", "ExecuteAlarmSupression", "ExecuteDroneHoldPosition", "ExecuteDroneRetrieve", "ExecuteESPSpawn", "ExecuteObjectInteraction", "ExecuteRiggerDroneControl", "ExecuteRiggerDroneEject", "ExecuteRiggerDroneSpawn", "ExecuteRiggerDroneSwap", "ExecuteSlaveReturn", "ExecuteSpiritAirSpawn", "ExecuteSpiritBanish", "ExecuteSpiritControl", "ExecuteSpiritRefresh", "ExecuteSpiritSpawn", "ExecuteWeaponReload", "ExecutPlayerInteraction", "healTarget", "magicHealTarget", "matrixDamage", "suicideDamage", "weaponDamage", "weaponDamageNotSelf"})
    
    'is list of gameEffect functions => assembly Assembly-CSharp, namespace: none (single)
    Private Shared _ToHistFunctionList As New ObservableCollection(Of String)(New String() {"actorCanConverse", "actorHasMagicBuffToHit", "actorIsMyAvailableDroneSlave", "actorIsMyDistantDroneSlave", "actorIsMyDistantSpiritSlave", "actorIsMyDroneSibling", "actorIsMySpiritSlave", "actorIsSelf", "actorMatrixAttackToHit", "actorMatrixESPAttackToHit", "actorSpellAttackToHit", "actorSpellNonSpiritAttackToHit", "actorWeaponAttackToHit", "autoHitActor", "autoHitActorInRange", "autoHitActorNotSelfInRange", "autoHitHealActorInRange", "autoHitHealDroneActorInRange", "autoHitLocationInRange", "autoHitMagicHealActorInRange", "autoHitUnconciousActorInRange", "autoHitUnoccupiedLocation", "autoMagicActorInRange", "autoMagicNonSpiritActorInRange", "autoTotemActorInRange", "locationCanActorInteract", "locationSpellToHit", "locationWeaponToHit"})

    Private Shared _FlagList As New ObservableCollection(Of String)(New String() {"AIMING", "ATTACKS_USED", "CAN_INTERACT", "CAN_PICKUP", "CAN_SUMMON_DRONES", "CAN_SUMMON_ESP", "CAN_SUMMON_SPIRITS", "CAN_TAKE_COVER", "CHANGED_FIRE_MODE", "DEFENSES_USED", "DODGES_USED", "DRONES_ACTIVE", "HAS_DATAJACK", "HEALTH_USED", "IN_COVER", "MASTER_DRONES_ACTIVE", "OFFPHASE_USED", "OVERWATCH", "REACH", "SLAVES_CAN_RETURN", "SPIRIT_POINT_NEARBY", "SPIRITS_ACTIVE", "SWAPPED_WEAPON", "TOOK_COVER", "WEAPON"})

    Private Shared _ItemTraitList As New ObservableCollection(Of String)(New String() {"ASSOCIATED_ATTRIBUTE", "ASSOCIATED_SKILL", "ASSOCIATED_SPECIALIZATION", "CORE_ATTRIBUTE", "CORE_SKILL", "CORE_SPECIALIZATION", "REACH", "WEAPON_CUR_AMMO", "WEAPON_MAX_AMMO"})




    Public Shared Property DurationFxScriptList As ObservableCollection(Of String)
        Get
            Return _DurationFxScriptList
        End Get
        Set(value As ObservableCollection(Of String))
            _DurationFxScriptList = value
        End Set
    End Property

    Public Shared Property CharacterPrefabIdList As ObservableCollection(Of String)
        Get
            Return _CharacterPrefabIdList
        End Get
        Set(value As ObservableCollection(Of String))
            _CharacterPrefabIdList = value
        End Set
    End Property
    Public Shared Property CharacterSheetIdList As ObservableCollection(Of String)
        Get
            Return _CharacterSheetIdList
        End Get
        Set(value As ObservableCollection(Of String))
            _CharacterSheetIdList = value
        End Set
    End Property

    Public Shared Property DamageFunctionList As ObservableCollection(Of String)
        Get
            Return _DamageFunctionList
        End Get
        Set(value As ObservableCollection(Of String))
            _DamageFunctionList = value
        End Set
    End Property

    Public Shared Property ToHistFunctionList As ObservableCollection(Of String)
        Get
            Return _ToHistFunctionList
        End Get
        Set(value As ObservableCollection(Of String))
            _ToHistFunctionList = value
        End Set
    End Property
    Public Shared Property CharacterUINameList As ObservableCollection(Of String)
        Get
            Return _CharacterUINameList
        End Get
        Set(value As ObservableCollection(Of String))
            _CharacterUINameList = value
        End Set
    End Property

    Public Shared Property CooldownCategoryList As ObservableCollection(Of String)
        Get
            Return _CooldownCategoryList
        End Get
        Set(value As ObservableCollection(Of String))
            _CooldownCategoryList = value
        End Set
    End Property

    Public Shared Property DefDeckingWeaponList As ObservableCollection(Of String)
        Get
            Return _DefDeckingWeaponList
        End Get
        Set(value As ObservableCollection(Of String))
            _DefDeckingWeaponList = value
        End Set
    End Property

    Public Shared Property EquipmentPrefabList As ObservableCollection(Of String)
        Get
            Return _EquipmentPrefabList
        End Get
        Set(value As ObservableCollection(Of String))
            _EquipmentPrefabList = value
        End Set
    End Property
    Public Shared Property EquipmentSheetIdList As ObservableCollection(Of String)
        Get
            Return _EquipmentSheetIdList
        End Get
        Set(value As ObservableCollection(Of String))
            _EquipmentSheetIdList = value
        End Set
    End Property

    Public Shared Property FlagList As ObservableCollection(Of String)
        Get
            Return _FlagList
        End Get
        Set(value As ObservableCollection(Of String))
            _FlagList = value
        End Set
    End Property

    Public Shared Property FxNamesList As ObservableCollection(Of String)
        Get
            Return _FxNamesList
        End Get
        Set(value As ObservableCollection(Of String))
            _FxNamesList = value
        End Set
    End Property


    Public Shared Property FXScriptList As ObservableCollection(Of String)
        Get
            Return _FXScriptList
        End Get
        Set(value As ObservableCollection(Of String))
            _FXScriptList = value
        End Set
    End Property

    Public Shared Property GearBundleList As ObservableCollection(Of String)
        Get
            Return _GearBundleList
        End Get
        Set(value As ObservableCollection(Of String))
            _GearBundleList = value
        End Set
    End Property

    Public Shared Property GearPrefabList As ObservableCollection(Of String)
        Get
            Return _GearPrefabList
        End Get
        Set(value As ObservableCollection(Of String))
            _GearPrefabList = value
        End Set
    End Property

    Public Shared Property IconList As ObservableCollection(Of String)
        Get
            Return _IconList
        End Get
        Set(value As ObservableCollection(Of String))
            _IconList = value
        End Set
    End Property


    Public Shared Property ItemTraitList As ObservableCollection(Of String)
        Get
            Return _ItemTraitList
        End Get
        Set(value As ObservableCollection(Of String))
            _ItemTraitList = value
        End Set
    End Property

    Public Shared Property OutfitTextureList As ObservableCollection(Of String)
        Get
            Return _OutfitTextureList
        End Get
        Set(value As ObservableCollection(Of String))
            _OutfitTextureList = value
        End Set
    End Property

    Public Shared Property PreReqList As ObservableCollection(Of String)
        Get
            Return _PreReqList
        End Get
        Set(value As ObservableCollection(Of String))
            _PreReqList = value
        End Set
    End Property

    Public Shared Property SortingGroupList As ObservableCollection(Of String)
        Get
            Return _SortingGroupList
        End Get
        Set(value As ObservableCollection(Of String))
            _SortingGroupList = value
        End Set
    End Property

    Public Shared Property StackingCategoriesList As ObservableCollection(Of String)
        Get
            Return _StackingCategoriesList
        End Get
        Set(value As ObservableCollection(Of String))
            _StackingCategoriesList = value
        End Set
    End Property

#End Region

#Region "Current Objects"

    Public Shared CurrentItemDefObject As New ItemDef
    Public Shared CurrentContentPack As New ProjectDef
    Public Shared CurrentManifest As New Manifest
    Public Shared CurrentAbilityDefObject As New AbilityDef
    Public Shared CurrentModeDefObject As New ModeDef
    Public Shared CurrentModeIdString As String

#End Region

#Region "TabContent Objects"

#End Region

    Friend ManualSelection As Boolean = False
    Friend AddingDependancies As Boolean = False
    Friend CurrentTabname As String

    Private ListExpandedTreeViewItems As New List(Of TreeViewItem)

    Private Sub WindowSRRItemEditor_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        'logging
        If Not Directory.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Log")) Then
            Directory.CreateDirectory(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Log"))
        End If
        Dim log As String = "Application started at " & DateTime.UtcNow & vbCrLf & "Stored SRR Location =" & My.Settings.SRRLocation
        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Log", "Log.txt"), log)

        If My.Settings.SRRLocation = "" Then
            log = "Detecting SRR Location:" & vbCrLf
            File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Log", "Log.txt"), log)
            'Find SRR Installation
            If Directory.Exists("C:\Program Files (x86)\Steam\SteamApps\common\Shadowrun Returns") Then
                My.Settings.SRRLocation = "C:\Program Files (x86)\Steam\SteamApps\common\Shadowrun Returns"
                log = "Found SRR Location:" & My.Settings.SRRLocation & vbCrLf
                File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Log", "Log.txt"), log)
            ElseIf Directory.Exists("C:\Program Files\Steam\SteamApps\common\Shadowrun Returns") Then
                My.Settings.SRRLocation = "C:\Program Files\Steam\SteamApps\common\Shadowrun Returns"
                log = "Found SRR Location:" & My.Settings.SRRLocation & vbCrLf
                File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Log", "Log.txt"), log)
            ElseIf Directory.Exists("C:\Program Files (x86)\Shadowrun Returns") Then
                My.Settings.SRRLocation = "C:\Program Files (x86)\Shadowrun Returns"
                log = "Found SRR Location:" & My.Settings.SRRLocation & vbCrLf
                File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Log", "Log.txt"), log)
            ElseIf Directory.Exists("C:\Program Files\Shadowrun Returns") Then
                My.Settings.SRRLocation = "C:\Program Files\Shadowrun Returns"
                log = "Found SRR Location:" & My.Settings.SRRLocation & vbCrLf
                File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Log", "Log.txt"), log)
            Else
                MsgBox("Can not find Shadowrun Returns installation, Please provide location.")
                Dim FolderBrowser As New Forms.FolderBrowserDialog()

                FolderBrowser.Description = "Select the folder where the Shadowrun Returns installation is located"
                FolderBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer
                FolderBrowser.ShowNewFolderButton = True

                If (FolderBrowser.ShowDialog() = Forms.DialogResult.OK) Then
                    My.Settings.SRRLocation = FolderBrowser.SelectedPath
                End If
                log = "Chosen SRR Location:" & My.Settings.SRRLocation & vbCrLf
                File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Log", "Log.txt"), log)
            End If

            If My.Settings.SRRLocation.Contains("Steam") Then
                '    My.Settings.SteamUGCLocation = FindSubFolders(New DirectoryInfo("C:\Program Files (x86)\Steam\userdata"), "ugc")
                My.Settings.SteamUGCLocation = FindSubFolders(New DirectoryInfo(Path.Combine(Directory.GetParent(My.Settings.SRRLocation).Parent.Parent.FullName, "userdata")), "ugc")

            End If
            My.Settings.Save()
        End If

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        DataContext = Me
        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Buttons"

    Private Sub buttonSave_Click(sender As Object, e As RoutedEventArgs) Handles buttonSave.Click
        Select Case CStr(CType(tabControl.SelectedItem, TabItem).Header)
            Case "Items"
                If CurrentItemDefObject Is Nothing Then
                    CurrentItemDefObject = New isogame.ItemDef
                    ItemList.Add(CurrentItemDefObject)
                End If
                SaveIntoContentPack(CType(CurrentItemDefObject, Object))
            Case "Abilities"
                SaveIntoContentPack(CType(CurrentAbilityDefObject, Object))
            Case "Modes"
                SaveIntoContentPack(CType(CurrentModeDefObject, Object))
        End Select

    End Sub

    'Private Sub buttonSaveAs_Click(sender As Object, e As RoutedEventArgs) Handles buttonSaveAs.Click

    '    Dim NewFile As String

    '    Dim dlg As New Microsoft.Win32.SaveFileDialog
    '    dlg.InitialDirectory = Path.GetDirectoryName(CStr(My.Settings.LastUsedLocation))
    '    dlg.FileName = Path.GetFileName(textBoxBytesFile.Text)
    '    dlg.DefaultExt = ".bytes" ' Default file extension
    '    dlg.Filter = "bytes Files (.bytes)|*.bytes" ' Filter files by extension

    '    ' Show open file dialog box
    '    Dim result? As Boolean = dlg.ShowDialog()

    '    ' Process open file dialog box results
    '    If result = True Then
    '        ' Open document
    '        NewFile = dlg.FileName

    '        My.Settings.LastUsedLocation = Path.GetDirectoryName(NewFile) & "\"
    '        My.Settings.Save()

    '        If tabControl.SelectedIndex = tabControl.Items.IndexOf(ItemsTab) Then
    '            StoreChangesItems()
    '            Using fs As FileStream = File.Create(NewFile)
    '                Serializer.Serialize(fs, CurrentItemDefObject)
    '            End Using
    '        End If
    '    End If

    'End Sub
#End Region

#Region "ContenPacks"

    Private Sub LoadContentPack(ByVal ContentPackFile As String, Optional Reload As Boolean = False)
        If ContentPackFile = "" Then
            Return
        End If
        If Reload Then
            ListExpandedTreeViewItems.Clear()
            For Each TVI As TreeViewItem In TreeViewContentPack.Items
                If TVI.IsExpanded Then
                    ListExpandedTreeViewItems.Add(TVI)
                    For Each TVI1 As TreeViewItem In TVI.Items
                        If TVI1.IsExpanded Then
                            ListExpandedTreeViewItems.Add(TVI1)
                            For Each TVI2 As TreeViewItem In TVI1.Items
                                If TVI2.IsExpanded Then
                                    ListExpandedTreeViewItems.Add(TVI2)
                                End If
                            Next
                        End If
                    Next
                End If
            Next

            ContentPackList.Clear()
            ContentPackLocationsList.Clear()
            TreeViewContentPack.Items.Clear()
            DataManifestList.Clear()
        End If
        Dim TempContentPackProjectDef As isogame.ProjectDef
        Using Fs As FileStream = File.Open(ContentPackFile, FileMode.Open)
            TempContentPackProjectDef = Serializer.Deserialize(Of isogame.ProjectDef)(Fs)
            If ContentPackList.FirstOrDefault(Function(C As isogame.ProjectDef) C.project_id = TempContentPackProjectDef.project_id) Is Nothing Then
                ContentPackList.Add(TempContentPackProjectDef)
                ContentPackLocationsList.Add(New ContentPackMetaData With {.Location = ContentPackFile, .Compressed = False})
            Else
                TempContentPackProjectDef = ContentPackList.FirstOrDefault(Function(C As isogame.ProjectDef) C.project_id = TempContentPackProjectDef.project_id)
            End If
            If Not AddingDependancies Then
                CurrentContentPack = TempContentPackProjectDef
                labelCurrentContentPack.Content = "Active Content Pack: " + TempContentPackProjectDef.project_name
            End If

        End Using
        AddDependancies(TempContentPackProjectDef)
        If Reload Then
            FillContentPackTreeView()
            For Each TVI As TreeViewItem In ListExpandedTreeViewItems
                TreeviewItemFinderRecursive(TreeViewContentPack, TVI).IsExpanded = True
            Next
            ListExpandedTreeViewItems.Clear()
        End If
    End Sub

    Private Sub AddDependancies(ContentPack As ProjectDef)
        AddingDependancies = True
        For Each PackRef As PackageRef In ContentPack.content_pack_dependencies
            Dim Str As String = PackRef.package_name

            If Str = "" Then
                Str = PackRef.package_id
                Dim DMS_Id As String
                Dim Seatlle_Id As String
                Dim ShadowrunCore_Id As String
                Dim TempContentPackProjectDef As PackageRef
                Using Fs As FileStream = File.Open(Path.Combine(My.Settings.SRRLocation, "Shadowrun_Data\StreamingAssets\ContentPacks\shadowrun_core\project.cpack.bytes"), FileMode.Open)
                    TempContentPackProjectDef = Serializer.Deserialize(Of isogame.PackageRef)(Fs)
                    ShadowrunCore_Id = TempContentPackProjectDef.package_id
                End Using
                Using Fs As FileStream = File.Open(Path.Combine(My.Settings.SRRLocation, "Shadowrun_Data\StreamingAssets\ContentPacks\seattle\project.cpack.bytes"), FileMode.Open)
                    TempContentPackProjectDef = Serializer.Deserialize(Of isogame.PackageRef)(Fs)
                    Seatlle_Id = TempContentPackProjectDef.package_id
                End Using
                Using Fs As FileStream = File.Open(Path.Combine(My.Settings.SRRLocation, "Shadowrun_Data\StreamingAssets\ContentPacks\dead_man_switch\project.cpack.bytes"), FileMode.Open)
                    TempContentPackProjectDef = Serializer.Deserialize(Of isogame.PackageRef)(Fs)
                    DMS_Id = TempContentPackProjectDef.package_id
                End Using
                If Str = ShadowrunCore_Id Then
                    LoadContentPack(Path.Combine(My.Settings.SRRLocation, "Shadowrun_Data\StreamingAssets\ContentPacks\shadowrun_core\project.cpack.bytes"))
                ElseIf Str = Seatlle_Id Then
                    LoadContentPack(Path.Combine(My.Settings.SRRLocation, "Shadowrun_Data\StreamingAssets\ContentPacks\seattle\project.cpack.bytes"))
                ElseIf Str = DMS_Id Then
                    LoadContentPack(Path.Combine(My.Settings.SRRLocation, "Shadowrun_Data\StreamingAssets\ContentPacks\dead_man_switch\project.cpack.bytes"))
                End If
            Else

                Dim UGCFolder As String = String.Empty
                If Str = "ShadowrunCore" Then
                    LoadContentPack(Path.Combine(My.Settings.SRRLocation, "Shadowrun_Data\StreamingAssets\ContentPacks\shadowrun_core\project.cpack.bytes"))
                ElseIf Str = "Seattle" Then
                    LoadContentPack(Path.Combine(My.Settings.SRRLocation, "Shadowrun_Data\StreamingAssets\ContentPacks\seattle\project.cpack.bytes"))
                ElseIf Str = "DeadMansSwitch" Then
                    LoadContentPack(Path.Combine(My.Settings.SRRLocation, "Shadowrun_Data\StreamingAssets\ContentPacks\dead_man_switch\project.cpack.bytes"))
                Else

                    If My.Settings.SRRLocation.Contains("Steam") Then
                        For Each Folderlocation As String In My.Settings.SteamUGCLocation 'todo Error message.
                            Dim TempUGCFolder As String = String.Empty
                            TempUGCFolder = FindSubFoldersSpecial(New DirectoryInfo(Folderlocation), PackRef.package_name)
                            If TempUGCFolder <> String.Empty Then
                                UGCFolder = TempUGCFolder
                                Exit For
                            End If
                            If UGCFolder = String.Empty Then
                                UGCFolder = FindSubFoldersSpecial(New DirectoryInfo(Path.Combine(My.Settings.SRRLocation, "Shadowrun_Data\StreamingAssets\ContentPacks")), PackRef.package_name)
                            End If
                        Next
                    Else
                        If PackRef.package_name <> "" Then
                            UGCFolder = FindSubFoldersSpecial(New DirectoryInfo(Path.Combine(My.Settings.SRRLocation, "Shadowrun_Data\StreamingAssets\ContentPacks")), PackRef.package_name)
                        End If
                    End If
                    LoadContentPack(RecursiveSearch(UGCFolder, "project.cpack.bytes"))
                End If
            End If
        Next
    End Sub

    Public Sub SaveIntoContentPack(ByRef Obj As Object)

        Dim ContentPackLocation As ContentPackMetaData = ContentPackLocationsList.Item(ContentPackList.IndexOf(ContentPackList.First(Function(C As ProjectDef) C.project_name = CurrentContentPack.project_name AndAlso C.project_id = CurrentContentPack.project_id AndAlso C.project_version = CurrentContentPack.project_version)))

        Dim Manifest As Manifest = DataManifestList.Item(ContentPackList.IndexOf(ContentPackList.First(Function(C As ProjectDef) C.project_name = CurrentContentPack.project_name AndAlso C.project_id = CurrentContentPack.project_id AndAlso C.project_version = CurrentContentPack.project_version)))
        Dim Location As String = Path.GetDirectoryName(ContentPackLocation.Location)

        Select Case Obj.GetType()
            Case GetType(isogame.ItemDef)
                CType(CType(tabControl.FindName("ItemsTab"), TabItem).Content, ItemTabContent).StoreChangesItems()
                Dim objItemDef As ItemDef = CType(Obj, ItemDef)
                If File.Exists(Path.Combine(Location, "data\items", ((objItemDef.id).ToLower + ".item.bytes"))) Then
                    Dim BackupPath = FileTester(Location + "\data\items\", (objItemDef.id).ToLower + ".item.bytes", ".bak")
                    File.Copy(Path.Combine(Location, "data\items", ((objItemDef.id).ToLower + ".item.bytes")), BackupPath)
                End If
                Using fs As FileStream = File.Create(Path.Combine(Location, "data\items", ((objItemDef.id).ToLower + ".item.bytes")))
                    Serializer.Serialize(fs, CurrentItemDefObject)
                End Using


                Dim PreviousEntry As Manifest.Entry = Manifest.entries.Find(Function(C As Manifest.Entry) C.digest_method = "" AndAlso C.name = "items/" + objItemDef.id.ToLower + ".item.bytes" AndAlso C.digest = "")
                If PreviousEntry Is Nothing Then
                    Manifest.entries.Add(New Manifest.Entry With {.digest_method = "", .name = "items/" + objItemDef.id.ToLower + ".item.bytes", .size = New FileInfo(Path.Combine(Location, "data\items\" + objItemDef.id.ToLower + ".item.bytes")).Length, .digest = ""})
                    Manifest.entries.Sort(New ManifestComparer)
                Else
                    PreviousEntry.size = New FileInfo(Path.Combine(Location, "data\items\" + objItemDef.id.ToLower + ".item.bytes")).Length
                End If
                Using fs As FileStream = File.Create(Path.Combine(Location, "data\manifest.mf.bytes"))
                    Serializer.Serialize(fs, Manifest)
                End Using

            Case GetType(isogame.AIObjective)
            Case GetType(isogame.AbilityDef)
                Dim objAbilityDef As AbilityDef = CType(Obj, AbilityDef)
                If objAbilityDef Is Nothing Then
                    Return
                End If
                If objAbilityDef.id Is Nothing Then
                    Return
                End If
                If File.Exists(Path.Combine(Location, "data\abilities", (objAbilityDef.id.ToLower + ".ab.bytes"))) Then
                    Dim BackupPath = FileTester(Location + "\data\abilities\", objAbilityDef.id.ToLower + ".ab.bytes", ".bak")
                    File.Copy(Path.Combine(Location, "data\abilities", (objAbilityDef.id.ToLower + ".ab.bytes")), BackupPath)
                End If
                Using fs As FileStream = File.Create(Path.Combine(Location, "data\abilities", (objAbilityDef.id.ToLower + ".ab.bytes")))
                    Serializer.Serialize(fs, CurrentAbilityDefObject)
                End Using


                Dim PreviousEntry As Manifest.Entry = Manifest.entries.Find(Function(C As Manifest.Entry) C.digest_method = "" AndAlso C.name = "abilities/" + objAbilityDef.id.ToLower + ".ab.bytes" And C.digest = "")
                If PreviousEntry Is Nothing Then
                    Manifest.entries.Add(New Manifest.Entry With {.digest_method = "", .name = "abilities/" + objAbilityDef.id.ToLower + ".ab.bytes", .size = New FileInfo(Path.Combine(Location, "data\abilities\" + objAbilityDef.id.ToLower + ".ab.bytes")).Length, .digest = ""})
                    Manifest.entries.Sort(New ManifestComparer)
                Else
                    PreviousEntry.size = New FileInfo(Path.Combine(Location, "data\abilities\" + objAbilityDef.id.ToLower + ".ab.bytes")).Length
                End If
                Using fs As FileStream = File.Create(Path.Combine(Location, "data\manifest.mf.bytes"))
                    Serializer.Serialize(fs, Manifest)
                End Using
            Case GetType(isogame.Ambience)
            Case GetType(isogame.CharacterInstance)
            Case GetType(isogame.MapDef)
            Case GetType(isogame.Manifest)
            Case GetType(isogame.ModeDef)
                Dim objModeDef As ModeDef = CType(Obj, ModeDef)
                Dim ModeIdString As String = ""
                If CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).BackupModeDefId IsNot Nothing AndAlso CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).BackupModeDefId <> "" Then
                    ModeIdString = CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).BackupModeDefId
                Else
                    ModeIdString = CurrentModeIdString
                End If

                If File.Exists(Path.Combine(Location, "data\modes", (ModeIdString.ToLower + ".mode.bytes"))) Then
                    Dim BackupPath = FileTester(Location + "\data\modes\", ModeIdString.ToLower + ".mode.bytes", ".bak")
                    File.Copy(Path.Combine(Location, "data\modes", (ModeIdString.ToLower + ".mode.bytes")), BackupPath)
                End If
                Using fs As FileStream = File.Create(Path.Combine(Location, "data\modes", (ModeIdString.ToLower + ".mode.bytes")))
                    Serializer.Serialize(fs, CurrentModeDefObject)
                End Using


                Dim PreviousEntry As Manifest.Entry = Manifest.entries.Find(Function(C As Manifest.Entry) C.digest_method = "" AndAlso C.name = "modes/" + ModeIdString.ToLower + ".mode.bytes" And C.digest = "")
                If PreviousEntry Is Nothing Then
                    Manifest.entries.Add(New Manifest.Entry With {.digest_method = "", .name = "modes/" + ModeIdString.ToLower + ".mode.bytes", .size = New FileInfo(Path.Combine(Location, "data\modes\" + ModeIdString.ToLower + ".mode.bytes")).Length, .digest = ""})
                    Manifest.entries.Sort(New ManifestComparer)
                Else
                    PreviousEntry.size = New FileInfo(Path.Combine(Location, "data\modes\" + ModeIdString.ToLower + ".mode.bytes")).Length
                End If
                Using fs As FileStream = File.Create(Path.Combine(Location, "data\manifest.mf.bytes"))
                    Serializer.Serialize(fs, Manifest)
                End Using
            Case GetType(isogame.ProjectDef)
            Case GetType(isogame.SceneDef)
            Case GetType(isogame.StoryDef)
            Case GetType(isogame.HiringSet)
            Case GetType(isogame.AnimationModifierList)
            Case GetType(isogame.CharacterPrefabLibrary)
            Case GetType(isogame.SoundLibrary)
            Case GetType(isogame.TsLibrary)
            Case GetType(isogame.Manifest)
            Case GetType(isogame.PropDef)
            Case GetType(isogame.DeckProgramDef)
            Case GetType(isogame.Conversation)
            Case GetType(isogame.SaveGame)
            Case GetType(isogame.SubMixGroup)

        End Select
        LoadContentPack(ContentPackLocation.Location, True)
    End Sub

#End Region

#Region "TreeView"

    Private Sub TreeViewContentPack_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs) Handles TreeViewContentPack.PreviewMouseDown

        If e.Source.GetType() Is GetType(TextBlock) Then
            If TryCast(CType(e.Source, TextBlock).Parent, StackPanel) IsNot Nothing Then
                If TryCast(TryCast(CType(CType(e.Source, TextBlock).Parent, StackPanel).Parent, TreeViewItem).Parent, TreeViewItem) IsNot Nothing Then
                    Select Case CStr(CType(CType(CType(CType(CType(CType(e.Source, TextBlock).Parent, StackPanel).Parent, TreeViewItem).Parent, TreeViewItem).Header, StackPanel).Children(1), TextBlock).Text)
                        Case "abilities"
                            If TryCast(CType(tabControl.FindName("AbilitiesTab"), TabItem).Content, AbilityTabContent) IsNot Nothing Then
                                If CType(CType(tabControl.FindName("AbilitiesTab"), TabItem).Content, AbilityTabContent).BackupAbilityDefObj IsNot Nothing Then
                                    SaveNewObject(Of AbilityDef)(AbilityList, "abilities", CType(CType(tabControl.FindName("AbilitiesTab"), TabItem).Content, AbilityTabContent).BackupAbilityDefObj, CType(CType(tabControl.FindName("AbilitiesTab"), TabItem).Content, AbilityTabContent).BackupAbilityDefObj.id)
                                End If
                            End If

                        Case "items"
                        Case "modes"
                            If TryCast(CType(tabControl.FindName("ModesTab"), TabItem).Content, ModeTabContent) IsNot Nothing Then
                                If CType(CType(tabControl.FindName("ModesTab"), TabItem).Content, ModeTabContent).BackupModeDefObj IsNot Nothing Then
                                    SaveNewObject(Of ModeDef)(ModesList, "modes", CType(CType(tabControl.FindName("ModesTab"), TabItem).Content, ModeTabContent).BackupModeDefObj, CType(CType(tabControl.FindName("ModesTab"), TabItem).Content, ModeTabContent).BackupModeDefId)

                                End If
                            End If
                    End Select
                End If
            End If
        End If

    End Sub

    Private Sub TreeViewContentPack_SelectedItemChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Object)) Handles TreeViewContentPack.SelectedItemChanged
        Dim SelectedTVI As TreeViewItem
        SelectedTVI = CType(TreeViewContentPack.SelectedItem, TreeViewItem)
        If SelectedTVI Is Nothing Then
            Return
        End If
        Dim IndexOfNewitem As Integer
        If Int32.TryParse(CType(SelectedTVI.Tag, String), IndexOfNewitem) Then
            If IndexOfNewitem > -1 Then
                Select Case CStr(CType(CType(CType(SelectedTVI.Parent, TreeViewItem).Header, StackPanel).Children(1), TextBlock).Text)
                    Case "abilities"
                        CurrentAbilityDefObject = AbilityList.Item(IndexOfNewitem)
                        tabControl.SelectedIndex = tabControl.Items.IndexOf(tabControl.FindName("AbilitiesTab"))
                    Case "items"
                        CType(CType(tabControl.FindName("ItemsTab"), TabItem).Content, ItemTabContent).StoreChangesItems()
                        CurrentItemDefObject = ItemList.Item(IndexOfNewitem)
                        CType(CType(tabControl.FindName("ItemsTab"), TabItem).Content, ItemTabContent).FillTab()
                        tabControl.SelectedIndex = tabControl.Items.IndexOf(tabControl.FindName("ItemsTab"))
                    Case "modes"
                        CurrentModeDefObject = ModesList.Item(IndexOfNewitem)
                        CurrentModeIdString = ModesNamesList.Item(IndexOfNewitem)
                        tabControl.SelectedIndex = tabControl.Items.IndexOf(tabControl.FindName("ModesTab"))

                End Select
            End If
        Else
            If Not CType(SelectedTVI.Tag, String).Contains("ContentPackIndex") AndAlso Not CType(SelectedTVI.Tag, String).Contains("Folder") AndAlso Not CType(SelectedTVI.Tag, String).Contains("NewObject") Then
                ManualSelection = True
                CType(CType(tabControl.FindName("ItemsTab"), TabItem).Content, ItemTabContent).StoreChangesItems()
                LoadBytesfiles(CType(SelectedTVI.Tag, String))
                ManualSelection = False
            End If
        End If

        If TryCast(SelectedTVI.Parent, TreeViewItem) IsNot Nothing Then
            Select Case CType(tabControl.SelectedItem, TabItem).Header.ToString()
                Case "Abilities"
                    If CStr(CType(CType(CType(SelectedTVI.Parent, TreeViewItem).Header, StackPanel).Children(1), TextBlock).Text) = "abilities" Then
                        CType(CType(tabControl.SelectedItem, TabItem).Content, AbilityTabContent).CurrentAbilityDefObj = CurrentAbilityDefObject

                        If Not (CType(CType(tabControl.SelectedItem, TabItem).Content, AbilityTabContent).dataProvider Is Nothing) Then
                            CType(CType(tabControl.SelectedItem, TabItem).Content, AbilityTabContent).dataProvider.ObjectInstance = CurrentAbilityDefObject
                        Else
                            CType(CType(tabControl.SelectedItem, TabItem).Content, AbilityTabContent).dataProvider = TryCast(CType(CType(tabControl.SelectedItem, TabItem).Content, AbilityTabContent).Resources("dataProvider"), ObjectDataProvider)
                            If Not (CType(CType(tabControl.SelectedItem, TabItem).Content, AbilityTabContent).dataProvider Is Nothing) Then
                                CType(CType(tabControl.SelectedItem, TabItem).Content, AbilityTabContent).dataProvider.ObjectInstance = CurrentAbilityDefObject
                            End If
                        End If
                        CType(CType(tabControl.SelectedItem, TabItem).Content, AbilityTabContent).dataProvider.Refresh()
                        CType(CType(tabControl.SelectedItem, TabItem).Content, AbilityTabContent).BackupAbilityDefObj = Nothing
                    End If
                Case "Items"
                Case "Modes"
                    If CStr(CType(CType(CType(SelectedTVI.Parent, TreeViewItem).Header, StackPanel).Children(1), TextBlock).Text) = "modes" Then
                        CurrentModeIdString = CStr(CType(CType(SelectedTVI.Header, StackPanel).Children(1), TextBlock).Text)

                        CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).CurrentModeDefObj = CurrentModeDefObject

                        If Not (CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).dataProvider Is Nothing) Then
                            CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).dataProvider.ObjectInstance = CurrentModeDefObject
                            CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).TextBoxId.Text = CurrentModeIdString
                        Else
                            CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).dataProvider = TryCast(CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).Resources("dataProvider"), ObjectDataProvider)
                            If Not (CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).dataProvider Is Nothing) Then
                                CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).dataProvider.ObjectInstance = CurrentModeDefObject
                                CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).TextBoxId.Text = CurrentModeIdString
                            End If
                        End If
                        CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).dataProvider.Refresh()
                        CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).BackupModeDefObj = Nothing
                        CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).BackupModeDefId = Nothing
                    End If
            End Select
        End If

    End Sub

    Private Sub FillContentPackTreeView()
        For Each ContentPack As isogame.ProjectDef In ContentPackList
            Dim CurrentContenPackIndex As Integer = ContentPackList.IndexOf(ContentPack)
            Dim TVIContentPack As TreeViewItem = CreateTreeViewFolderItem(ContentPack.project_name, "ContentPackIndex=" + CurrentContenPackIndex.ToString)
            TreeViewContentPack.Items.Add(TVIContentPack)
            LoadBytesfiles(Path.Combine(Path.GetDirectoryName(ContentPackLocationsList(CurrentContenPackIndex).Location), "data\manifest.mf.bytes"))
            Dim TVIArtDir As TreeViewItem = CreateTreeViewFolderItem("Art")
            Dim TVIDataDir As TreeViewItem = CreateTreeViewFolderItem("Data")
            TVIContentPack.Items.Add(TVIArtDir)
            TVIContentPack.Items.Add(TVIDataDir)
            For Each ManEntry As isogame.Manifest.Entry In DataManifestList(CurrentContenPackIndex).entries
                If Not ManEntry.name.Contains("readme") AndAlso Not ManEntry.name.Contains("epilogue") AndAlso Not ManEntry.name.Contains("ai_perception") AndAlso Not ManEntry.name.Contains("credentials") AndAlso Not ManEntry.name.Contains("hiring") Then
                    LoadBytesfiles(IO.Path.Combine(IO.Path.GetDirectoryName(ContentPackLocationsList(CurrentContenPackIndex).Location), "data", ManEntry.name), True)
                    If TreeviewItemFinder(TVIDataDir, CreateTreeViewFolderItem(ManEntry.name.Split(CChar("/"))(0))) IsNot Nothing Then
                        Dim ItemnameArr() As String = ManEntry.name.Split(CChar("/"))(1).Split(CChar("."))
                        Dim Itemname As String = String.Join("", ItemnameArr, 0, ItemnameArr.Count - 2)
                        TreeviewItemFinder(TVIDataDir, CreateTreeViewFolderItem(ManEntry.name.Split(CChar("/"))(0))).Items.Add(
                            CreateTreeViewItem(Itemname,
                                               IO.Path.Combine(IO.Path.GetDirectoryName(ContentPackLocationsList(CurrentContenPackIndex).Location),
                                                            "data", ManEntry.name
                                                            )))
                    Else
                        TVIDataDir.Items.Add(CreateTreeViewFolderItem(ManEntry.name.Split(CChar("/"))(0)))
                        Dim ItemnameArr() As String = ManEntry.name.Split(CChar("/"))(1).Split(CChar("."))
                        Dim Itemname As String = String.Join("", ItemnameArr, 0, ItemnameArr.Count - 2)
                        TreeviewItemFinder(TVIDataDir, CreateTreeViewFolderItem(ManEntry.name.Split(CChar("/"))(0))).Items.Add(CreateTreeViewItem(Itemname, IO.Path.Combine(IO.Path.GetDirectoryName(ContentPackLocationsList(CurrentContenPackIndex).Location), "data", ManEntry.name)))

                    End If
                End If
            Next
        Next
        CurrentItemDefObject = New ItemDef()
        CurrentAbilityDefObject = New AbilityDef()
        CurrentModeDefObject = New ModeDef()
        CurrentModeIdString = ""

    End Sub

    Public Shared Function TreeviewItemFinder(ByRef TreeViewItemToSearch As TreeViewItem, ByRef TreeviewItemToFind As TreeViewItem) As TreeViewItem
        Dim result As TreeViewItem = Nothing
        For Each objTreeviewItem As TreeViewItem In TreeViewItemToSearch.Items
            If CType(CType(objTreeviewItem.Header, StackPanel).Children(1), TextBlock).Text = CType(CType(TreeviewItemToFind.Header, StackPanel).Children(1), TextBlock).Text Then
                result = objTreeviewItem
            End If
        Next
        Return result
    End Function

    Public Shared Function TreeviewItemFinder(ByRef TreeViewToSearch As TreeView, ByRef TreeviewItemToFind As TreeViewItem) As TreeViewItem
        Dim result As TreeViewItem = Nothing
        For Each objTreeviewItem As TreeViewItem In TreeViewToSearch.Items
            If CType(CType(objTreeviewItem.Header, StackPanel).Children(1), TextBlock).Text = CType(CType(TreeviewItemToFind.Header, StackPanel).Children(1), TextBlock).Text Then
                result = objTreeviewItem
            End If
        Next
        Return result
    End Function

    Public Shared Function TreeviewItemFinderRecursive(ByRef TreeViewToSearch As TreeView, ByRef TreeviewItemToFind As TreeViewItem) As TreeViewItem
        Dim found As Boolean
        TreeviewItemFinderRecursive = New TreeViewItem()
        For Each objTreeviewItem As TreeViewItem In TreeViewToSearch.Items
            If CType(CType(objTreeviewItem.Header, StackPanel).Children(1), TextBlock).Text = CType(CType(TreeviewItemToFind.Header, StackPanel).Children(1), TextBlock).Text Then
                TreeviewItemFinderRecursive = objTreeviewItem
                found = True
            Else
                Dim TVI As TreeViewItem
                TVI = TreeviewItemFinderRecursive(objTreeviewItem, TreeviewItemToFind)
                If TVI IsNot Nothing Then
                    TreeviewItemFinderRecursive = TVI
                    found = True
                End If
            End If
            If found Then
                Exit For
            End If
        Next
        If found Then
            Return TreeviewItemFinderRecursive
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function TreeviewItemFinderRecursive(ByRef TreeViewToSearch As TreeViewItem, ByRef TreeviewItemToFind As TreeViewItem) As TreeViewItem
        Dim found As Boolean
        TreeviewItemFinderRecursive = New TreeViewItem()
        For Each objTreeviewItem As TreeViewItem In TreeViewToSearch.Items
            If CType(CType(objTreeviewItem.Header, StackPanel).Children(1), TextBlock).Text = CType(CType(TreeviewItemToFind.Header, StackPanel).Children(1), TextBlock).Text Then
                TreeviewItemFinderRecursive = objTreeviewItem
                found = True
            Else
                Dim TVI As TreeViewItem
                TVI = TreeviewItemFinderRecursive(objTreeviewItem, TreeviewItemToFind)
                If TVI IsNot Nothing Then
                    TreeviewItemFinderRecursive = TVI
                    found = True
                End If
            End If
            If found Then
                Exit For
            End If
        Next
        If found Then
            Return TreeviewItemFinderRecursive
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function CreateTreeViewFolderItem(ByVal DisplayText As String, Optional Tag As String = "Folder") As TreeViewItem
        Dim ItemTag As New TreeViewItem
        Dim Holder As New StackPanel
        Dim Image As New Windows.Controls.Image
        Dim TextBlock As New TextBlock
        ItemTag.Header = Holder
        Holder.Orientation = Controls.Orientation.Horizontal
        Dim CurrAssembly As System.Reflection.Assembly = System.Reflection.Assembly.LoadFrom(System.Windows.Forms.Application.ExecutablePath)
        Dim S As System.IO.Stream = CurrAssembly.GetManifestResourceStream("ShadowrunReturnsItemEditor.folder.png")
        Dim Bmp As New System.Windows.Media.Imaging.BitmapImage
        Bmp.BeginInit()
        Bmp.StreamSource = S
        Bmp.EndInit()
        Image.Source = Bmp
        If S IsNot Nothing Then S.Close()
        Holder.Children.Add(Image)
        Holder.Children.Add(TextBlock)
        ItemTag.Tag = Tag
        TextBlock.Text = DisplayText
        Return ItemTag
    End Function

    Public Shared Function CreateTreeViewItem(ByVal DisplayText As String, ByVal ItemTag As String) As TreeViewItem
        Dim TVI As New TreeViewItem
        Dim Holder As New StackPanel
        Dim Image As New Windows.Controls.Image
        Dim TextBlock As New TextBlock
        TVI.Header = Holder
        Holder.Orientation = Controls.Orientation.Horizontal
        Holder.Children.Add(Image)
        Holder.Children.Add(TextBlock)
        TVI.Tag = ItemTag
        TextBlock.Text = DisplayText
        Return TVI
    End Function

    Public Sub SaveNewObject(Of T As {New})(ByRef list As ObservableCollection(Of T), FolderName As String, CurrentObj As T, ObjName As String)
        Dim index As Integer = ItemList.IndexOf(CurrentItemDefObject)
        Dim IndexOfAddedItem As Integer

        If CurrentObj Is Nothing Then
            Dim temp As New T()
            CurrentObj = temp
            list.Add(CurrentObj)
        End If

        IndexOfAddedItem = list.IndexOf(CurrentObj)

        If TreeviewItemFinder(TreeViewContentPack, CreateTreeViewFolderItem("NewObjects", "NewObject")) Is Nothing Then
            Dim TVINewObjects As TreeViewItem = CreateTreeViewFolderItem("NewObjects", "NewObject")
            TreeViewContentPack.Items.Add(TVINewObjects)
        End If
        If TreeviewItemFinder(TreeviewItemFinder(TreeViewContentPack, CreateTreeViewFolderItem("NewObjects", "NewObject")), CreateTreeViewFolderItem(FolderName)) Is Nothing Then
            Dim TVINewItemsFolder As TreeViewItem = CreateTreeViewFolderItem(FolderName)
            TreeviewItemFinder(TreeViewContentPack, CreateTreeViewFolderItem("NewObjects", "NewObject")).Items.Add(TVINewItemsFolder)
        End If
        TreeviewItemFinder(TreeviewItemFinder(TreeViewContentPack, CreateTreeViewFolderItem("NewObjects", "NewObject")), CreateTreeViewFolderItem(FolderName)).Items.Add(CreateTreeViewItem(ObjName, IndexOfAddedItem.ToString()))
    End Sub

#End Region

#Region "TabControl"
    Private Sub tabControl_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs) Handles tabControl.PreviewMouseLeftButtonDown
        If e.Source.GetType() Is GetType(TabItem) Then
            If CType(e.Source, TabItem).Header.ToString() <> CurrentTabname Then
                Select Case CType(CType(CType(e.Source, TabItem).Parent, TabControl).SelectedItem, TabItem).Header.ToString()
                    Case "Items"
                        CType(CType(tabControl.SelectedItem, TabItem).Content, ItemTabContent).StoreChangesItems()
                    Case "Modes"
                        If CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).BackupModeDefObj IsNot Nothing Then
                            SaveNewObject(Of ModeDef)(ModesList, "modes", CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).BackupModeDefObj, CType(CType(tabControl.SelectedItem, TabItem).Content, ModeTabContent).BackupModeDefId)
                        End If
                    Case "Abilities"
                        If CType(CType(tabControl.SelectedItem, TabItem).Content, AbilityTabContent).BackupAbilityDefObj IsNot Nothing Then
                            SaveNewObject(Of AbilityDef)(AbilityList, "abilities", CType(CType(tabControl.SelectedItem, TabItem).Content, AbilityTabContent).BackupAbilityDefObj, CType(CType(tabControl.SelectedItem, TabItem).Content, AbilityTabContent).BackupAbilityDefObj.id)
                        End If
                End Select
            End If
        End If

    End Sub
    Private Sub tabControl_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles tabControl.SelectionChanged
        If (e.Source.GetType Is GetType(TabControl)) Then
            CurrentTabname = CType(CType(e.Source, TabControl).SelectedItem, TabItem).Header.ToString()
            Select Case CType(CType(e.Source, TabControl).SelectedItem, TabItem).Header.ToString()
                Case "Items"
                    CType(CType(e.Source, TabControl).SelectedItem, TabItem).Content = New ItemTabContent
                    CType(CType(CType(e.Source, TabControl).SelectedItem, TabItem).Content, ItemTabContent).FillTab()
                    CType(CType(CType(e.Source, TabControl).SelectedItem, TabItem).Content, ItemTabContent).TreeViewContentPack = TreeViewContentPack
                Case "Modes"
                    CType(CType(e.Source, TabControl).SelectedItem, TabItem).Content = New ModeTabContent(CurrentModeDefObject)
                    CType(CType(CType(e.Source, TabControl).SelectedItem, TabItem).Content, ModeTabContent).DataContext = CurrentModeDefObject
                    CType(CType(CType(e.Source, TabControl).SelectedItem, TabItem).Content, ModeTabContent).TextBoxId.Text = CurrentModeIdString
                Case "Abilities"
                    CType(CType(e.Source, TabControl).SelectedItem, TabItem).Content = New AbilityTabContent(CurrentAbilityDefObject)
                    CType(CType(CType(e.Source, TabControl).SelectedItem, TabItem).Content, AbilityTabContent).DataContext = CurrentAbilityDefObject
            End Select

        End If
        e.Handled = True

    End Sub

#End Region

#Region "Menu"
    Private Sub MenuItemAddDependancy_Click(sender As Object, e As RoutedEventArgs) Handles MenuItemAddDependancy.Click
        'testTxtimport()
        '   loadSave()
        'printTlib()
        'copydotDFiles()
        startParse()
    End Sub

    Private Sub MenuItemSetLocationOfSRR_Click(sender As Object, e As RoutedEventArgs) Handles MenuItemSetLocationOfSRR.Click
        Dim SRRLocation As String = InputBox("SRR is located at " + My.Settings.SRRLocation + ", Enter new location if you want to change the default location.")
        If Directory.Exists(SRRLocation) Then
            My.Settings.SRRLocation = SRRLocation
            My.Settings.Save()
        End If

    End Sub

    Private Sub MenuItemOpenContentPack_Click(sender As Object, e As RoutedEventArgs) Handles MenuItemOpenContentPack.Click
        Dim ContentPackLocation As String = FileSelector("cpack.bytes", "cpack.bytes file")
        If Path.GetFileName(ContentPackLocation) = "project.cpack.bytes" Then
            TreeViewContentPack.Items.Clear()
            ContentPackList.Clear()
            ContentPackLocationsList.Clear()
            DataManifestList.Clear()
            LoadBytesfiles(ContentPackLocation)
        End If
    End Sub
#End Region

#Region "Items Tab"

    Public Sub LoadItem(FileName As String)

        If Not File.Exists(FileName) Then
            Return
        End If
        Using Fs As FileStream = File.Open(FileName, FileMode.Open)

            WindowSRRItemEditor.CurrentItemDefObject = Serializer.Deserialize(Of isogame.ItemDef)(Fs)

            If ItemList.FirstOrDefault(Function(C As isogame.ItemDef) C.id = CurrentItemDefObject.id) Is Nothing Then
                ItemList.Add(CurrentItemDefObject)
            Else
                CurrentItemDefObject = ItemList.FirstOrDefault(Function(C As isogame.ItemDef) C.id = CurrentItemDefObject.id)
            End If
            If Not ManualSelection Then

                If CurrentItemDefObject.activationStatusEffects IsNot Nothing Then
                    If Not StackingCategoriesList.Contains(CurrentItemDefObject.activationStatusEffects.stackingCategory) AndAlso CurrentItemDefObject.activationStatusEffects.stackingCategory <> "" Then
                        StackingCategoriesList.Add(String.Copy(CurrentItemDefObject.activationStatusEffects.stackingCategory))
                        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListstackingCategory.txt"), CurrentItemDefObject.activationStatusEffects.stackingCategory + vbCrLf)
                    End If
                    If Not FXScriptList.Contains(CurrentItemDefObject.activationStatusEffects.fxScript) AndAlso CurrentItemDefObject.activationStatusEffects.fxScript <> "" Then
                        FXScriptList.Add(String.Copy(CurrentItemDefObject.activationStatusEffects.fxScript))
                        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListfxScript.txt"), CurrentItemDefObject.activationStatusEffects.fxScript + vbCrLf)
                    End If
                    If Not DurationFxScriptList.Contains(CurrentItemDefObject.activationStatusEffects.durationFxScript) AndAlso CurrentItemDefObject.activationStatusEffects.durationFxScript <> "" Then
                        DurationFxScriptList.Add(String.Copy(CurrentItemDefObject.activationStatusEffects.durationFxScript))
                        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListdurationFxScript.txt"), CurrentItemDefObject.activationStatusEffects.durationFxScript + vbCrLf)
                    End If
                    If CurrentItemDefObject.activationStatusEffects.uirep IsNot Nothing Then
                        If Not IconList.Contains(CurrentItemDefObject.activationStatusEffects.uirep.icon) AndAlso CurrentItemDefObject.activationStatusEffects.uirep.icon <> "" Then
                            IconList.Add(String.Copy(CurrentItemDefObject.activationStatusEffects.uirep.icon))
                            File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Listicon.txt"), CurrentItemDefObject.activationStatusEffects.uirep.icon + vbCrLf)
                        End If
                    End If
                End If
                If CurrentItemDefObject.equippedStatusEffects IsNot Nothing Then
                    If Not StackingCategoriesList.Contains(CurrentItemDefObject.equippedStatusEffects.stackingCategory) AndAlso CurrentItemDefObject.equippedStatusEffects.stackingCategory <> "" Then
                        StackingCategoriesList.Add(String.Copy(CurrentItemDefObject.equippedStatusEffects.stackingCategory))
                        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListstackingCategory.txt"), CurrentItemDefObject.equippedStatusEffects.stackingCategory + vbCrLf)
                    End If
                    If Not FXScriptList.Contains(CurrentItemDefObject.equippedStatusEffects.fxScript) AndAlso CurrentItemDefObject.equippedStatusEffects.fxScript <> "" Then
                        FXScriptList.Add(String.Copy(CurrentItemDefObject.equippedStatusEffects.fxScript))
                        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListfxScript.txt"), CurrentItemDefObject.equippedStatusEffects.fxScript + vbCrLf)
                    End If
                    If Not DurationFxScriptList.Contains(CurrentItemDefObject.equippedStatusEffects.durationFxScript) AndAlso CurrentItemDefObject.equippedStatusEffects.durationFxScript <> "" Then
                        DurationFxScriptList.Add(String.Copy(CurrentItemDefObject.equippedStatusEffects.durationFxScript))
                        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListdurationFxScript.txt"), CurrentItemDefObject.equippedStatusEffects.durationFxScript + vbCrLf)
                    End If
                    If CurrentItemDefObject.equippedStatusEffects.uirep IsNot Nothing Then
                        If Not IconList.Contains(CurrentItemDefObject.equippedStatusEffects.uirep.icon) AndAlso CurrentItemDefObject.equippedStatusEffects.uirep.icon <> "" Then
                            IconList.Add(String.Copy(CurrentItemDefObject.equippedStatusEffects.uirep.icon))
                            File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Listicon.txt"), CurrentItemDefObject.equippedStatusEffects.uirep.icon + vbCrLf)
                        End If
                    End If
                End If

                If CurrentItemDefObject.uirep IsNot Nothing Then
                    If Not IconList.Contains(CurrentItemDefObject.uirep.icon) AndAlso CurrentItemDefObject.uirep.icon <> "" Then
                        IconList.Add(String.Copy(CurrentItemDefObject.uirep.icon))
                        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Listicon.txt"), CurrentItemDefObject.uirep.icon + vbCrLf)
                    End If
                End If
                If CurrentItemDefObject.fxrep IsNot Nothing Then
                    If Not FxNamesList.Contains(CurrentItemDefObject.fxrep.actionFxName) AndAlso CurrentItemDefObject.fxrep.actionFxName <> "" Then
                        FxNamesList.Add(String.Copy(CurrentItemDefObject.fxrep.actionFxName))
                        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListFxName.txt"), CurrentItemDefObject.fxrep.actionFxName + vbCrLf)
                    End If
                    If Not FxNamesList.Contains(CurrentItemDefObject.fxrep.hitReactionFxName) AndAlso CurrentItemDefObject.fxrep.hitReactionFxName <> "" Then
                        FxNamesList.Add(String.Copy(CurrentItemDefObject.fxrep.hitReactionFxName))
                        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListFxName.txt"), CurrentItemDefObject.fxrep.hitReactionFxName + vbCrLf)
                    End If
                    If Not FxNamesList.Contains(CurrentItemDefObject.fxrep.missReactionFxName) AndAlso CurrentItemDefObject.fxrep.missReactionFxName <> "" Then
                        FxNamesList.Add(String.Copy(CurrentItemDefObject.fxrep.missReactionFxName))
                        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListFxName.txt"), CurrentItemDefObject.fxrep.missReactionFxName + vbCrLf)
                    End If
                    If Not FxNamesList.Contains(CurrentItemDefObject.fxrep.postActionFxName) AndAlso CurrentItemDefObject.fxrep.postActionFxName <> "" Then
                        FxNamesList.Add(String.Copy(CurrentItemDefObject.fxrep.postActionFxName))
                        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListFxName.txt"), CurrentItemDefObject.fxrep.postActionFxName + vbCrLf)
                    End If
                    If Not FxNamesList.Contains(CurrentItemDefObject.fxrep.preActionFxName) AndAlso CurrentItemDefObject.fxrep.preActionFxName <> "" Then
                        FxNamesList.Add(String.Copy(CurrentItemDefObject.fxrep.preActionFxName))
                        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListFxName.txt"), CurrentItemDefObject.fxrep.preActionFxName + vbCrLf)
                    End If
                End If
                If Not GearBundleList.Contains(CurrentItemDefObject.gear_bundle) AndAlso CurrentItemDefObject.gear_bundle <> "" Then
                    GearBundleList.Add(String.Copy(CurrentItemDefObject.gear_bundle))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Listgear_bundle.txt"), CurrentItemDefObject.gear_bundle + vbCrLf)
                End If
                If Not OutfitTextureList.Contains(CurrentItemDefObject.outfit_texture) AndAlso CurrentItemDefObject.outfit_texture <> "" Then
                    OutfitTextureList.Add(String.Copy(CurrentItemDefObject.outfit_texture))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Listoutfit_texture.txt"), CurrentItemDefObject.outfit_texture + vbCrLf)
                End If
                If Not GearPrefabList.Contains(CurrentItemDefObject.gear_prefab) AndAlso CurrentItemDefObject.gear_prefab <> "" Then
                    GearPrefabList.Add(String.Copy(CurrentItemDefObject.gear_prefab))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Listgear_prefab.txt"), CurrentItemDefObject.gear_prefab + vbCrLf)
                End If
                If Not SortingGroupList.Contains(CurrentItemDefObject.sorting_group) AndAlso CurrentItemDefObject.sorting_group <> "" Then
                    SortingGroupList.Add(String.Copy(CurrentItemDefObject.sorting_group))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Listsorting_group.txt"), CurrentItemDefObject.sorting_group + vbCrLf)
                End If
                If Not CooldownCategoryList.Contains(CurrentItemDefObject.cooldown_category) AndAlso CurrentItemDefObject.cooldown_category <> "" Then
                    CooldownCategoryList.Add(String.Copy(CurrentItemDefObject.cooldown_category))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListCooldowncategory.txt"), CurrentItemDefObject.cooldown_category + vbCrLf)
                End If
                If Not CharacterPrefabIdList.Contains(CurrentItemDefObject.character_prefab_id) AndAlso CurrentItemDefObject.character_prefab_id <> "" Then
                    CharacterPrefabIdList.Add(String.Copy(CurrentItemDefObject.character_prefab_id))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Listcharacter_prefab_id.txt"), CurrentItemDefObject.character_prefab_id + vbCrLf)
                End If

                If Not CharacterSheetIdList.Contains(CurrentItemDefObject.character_sheet_id) AndAlso CurrentItemDefObject.character_sheet_id <> "" Then
                    CharacterSheetIdList.Add(String.Copy(CurrentItemDefObject.character_sheet_id))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListCharacterSheetId.txt"), CurrentItemDefObject.character_sheet_id + vbCrLf)
                End If

                If Not EquipmentPrefabList.Contains(CurrentItemDefObject.equipPrefabName) AndAlso CurrentItemDefObject.equipPrefabName <> "" Then
                    EquipmentPrefabList.Add(String.Copy(CurrentItemDefObject.equipPrefabName))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListequipPrefabName.txt"), CurrentItemDefObject.equipPrefabName + vbCrLf)
                End If
                If Not CharacterUINameList.Contains(CurrentItemDefObject.character_ui_name) AndAlso CurrentItemDefObject.character_ui_name <> "" Then
                    CharacterUINameList.Add(String.Copy(CurrentItemDefObject.character_ui_name))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Listcharacter_ui_name.txt"), CurrentItemDefObject.character_ui_name + vbCrLf)
                End If

                If Not DefDeckingWeaponList.Contains(CurrentItemDefObject.decking_default_weapon) AndAlso CurrentItemDefObject.decking_default_weapon <> "" Then
                    DefDeckingWeaponList.Add(String.Copy(CurrentItemDefObject.decking_default_weapon))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Listdecking_default_weapon.txt"), CurrentItemDefObject.decking_default_weapon + vbCrLf)
                End If

                For Each PrereqString As String In CurrentItemDefObject.prereqStrings
                    If Not PreReqList.Contains(PrereqString) AndAlso PrereqString <> "" Then
                        PreReqList.Add(String.Copy(PrereqString))
                        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListprereqStrings.txt"), PrereqString + vbCrLf)
                    End If
                Next

                For Each EquipmentSheetId As String In CurrentItemDefObject.equipment_sheet_id
                    If Not EquipmentSheetIdList.Contains(EquipmentSheetId) AndAlso EquipmentSheetId <> "" Then
                        EquipmentSheetIdList.Add(String.Copy(EquipmentSheetId))
                        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListEquipmentSheetIds.txt"), EquipmentSheetId + vbCrLf)
                    End If
                Next
            End If
        End Using

    End Sub

#End Region

#Region "Abilities Tab"
    Private Sub LoadAbility(FileName As String)
        If Not File.Exists(FileName) Then
            Return
        End If
        Using Fs As FileStream = File.Open(FileName, FileMode.Open)

            WindowSRRItemEditor.CurrentAbilityDefObject = Serializer.Deserialize(Of isogame.AbilityDef)(Fs)

            If AbilityList.FirstOrDefault(Function(C As isogame.AbilityDef) C.id = CurrentAbilityDefObject.id) Is Nothing Then
                AbilityList.Add(CurrentAbilityDefObject)
            Else
                CurrentAbilityDefObject = AbilityList.FirstOrDefault(Function(C As isogame.AbilityDef) C.id = CurrentAbilityDefObject.id)
            End If
            Try
                If TryCast(TryCast(tabControl.SelectedItem, TabItem).Content, AbilityTabContent) IsNot Nothing Then
                    TryCast(TryCast(tabControl.SelectedItem, TabItem).Content, AbilityTabContent).DataContext = CurrentAbilityDefObject
                End If
            Catch
            End Try

        End Using
        If Not ManualSelection Then

            If CurrentAbilityDefObject.activationStatusEffects IsNot Nothing Then
                If Not StackingCategoriesList.Contains(CurrentAbilityDefObject.activationStatusEffects.stackingCategory) AndAlso CurrentAbilityDefObject.activationStatusEffects.stackingCategory <> "" Then
                    StackingCategoriesList.Add(String.Copy(CurrentAbilityDefObject.activationStatusEffects.stackingCategory))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListstackingCategory.txt"), CurrentAbilityDefObject.activationStatusEffects.stackingCategory + vbCrLf)
                End If
                If Not FXScriptList.Contains(CurrentAbilityDefObject.activationStatusEffects.fxScript) AndAlso CurrentAbilityDefObject.activationStatusEffects.fxScript <> "" Then
                    FXScriptList.Add(String.Copy(CurrentAbilityDefObject.activationStatusEffects.fxScript))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListfxScript.txt"), CurrentAbilityDefObject.activationStatusEffects.fxScript + vbCrLf)
                End If
                If Not DurationFxScriptList.Contains(CurrentAbilityDefObject.activationStatusEffects.durationFxScript) AndAlso CurrentAbilityDefObject.activationStatusEffects.durationFxScript <> "" Then
                    DurationFxScriptList.Add(String.Copy(CurrentAbilityDefObject.activationStatusEffects.durationFxScript))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListdurationFxScript.txt"), CurrentAbilityDefObject.activationStatusEffects.durationFxScript + vbCrLf)
                End If
                If CurrentAbilityDefObject.activationStatusEffects.uirep IsNot Nothing Then
                    If Not IconList.Contains(CurrentAbilityDefObject.activationStatusEffects.uirep.icon) AndAlso CurrentAbilityDefObject.activationStatusEffects.uirep.icon <> "" Then
                        IconList.Add(String.Copy(CurrentAbilityDefObject.activationStatusEffects.uirep.icon))
                        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListAbilityicon.txt"), CurrentAbilityDefObject.activationStatusEffects.uirep.icon + vbCrLf)
                    End If
                End If
            End If
            If CurrentAbilityDefObject.uirep IsNot Nothing Then
                If Not IconList.Contains(CurrentAbilityDefObject.uirep.icon) AndAlso CurrentAbilityDefObject.uirep.icon <> "" Then
                    IconList.Add(String.Copy(CurrentAbilityDefObject.uirep.icon))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListAbilityicon.txt"), CurrentAbilityDefObject.uirep.icon + vbCrLf)
                End If
            End If
            If CurrentAbilityDefObject.fxrep IsNot Nothing Then
                If Not FxNamesList.Contains(CurrentAbilityDefObject.fxrep.actionFxName) AndAlso CurrentAbilityDefObject.fxrep.actionFxName <> "" Then
                    FxNamesList.Add(String.Copy(CurrentAbilityDefObject.fxrep.actionFxName))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListAbilityFxName.txt"), CurrentAbilityDefObject.fxrep.actionFxName + vbCrLf)
                End If
                If Not FxNamesList.Contains(CurrentAbilityDefObject.fxrep.hitReactionFxName) AndAlso CurrentAbilityDefObject.fxrep.hitReactionFxName <> "" Then
                    FxNamesList.Add(String.Copy(CurrentAbilityDefObject.fxrep.hitReactionFxName))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListAbilityFxName.txt"), CurrentAbilityDefObject.fxrep.hitReactionFxName + vbCrLf)
                End If
                If Not FxNamesList.Contains(CurrentAbilityDefObject.fxrep.missReactionFxName) AndAlso CurrentAbilityDefObject.fxrep.missReactionFxName <> "" Then
                    FxNamesList.Add(String.Copy(CurrentAbilityDefObject.fxrep.missReactionFxName))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListAbilityFxName.txt"), CurrentAbilityDefObject.fxrep.missReactionFxName + vbCrLf)
                End If
                If Not FxNamesList.Contains(CurrentAbilityDefObject.fxrep.postActionFxName) AndAlso CurrentAbilityDefObject.fxrep.postActionFxName <> "" Then
                    FxNamesList.Add(String.Copy(CurrentAbilityDefObject.fxrep.postActionFxName))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListAbilityFxName.txt"), CurrentAbilityDefObject.fxrep.postActionFxName + vbCrLf)
                End If
                If Not FxNamesList.Contains(CurrentAbilityDefObject.fxrep.preActionFxName) AndAlso CurrentAbilityDefObject.fxrep.preActionFxName <> "" Then
                    FxNamesList.Add(String.Copy(CurrentAbilityDefObject.fxrep.preActionFxName))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListAbilityFxName.txt"), CurrentAbilityDefObject.fxrep.preActionFxName + vbCrLf)
                End If
            End If
            If Not CooldownCategoryList.Contains(CurrentAbilityDefObject.cooldown_category) AndAlso CurrentAbilityDefObject.cooldown_category <> "" Then
                CooldownCategoryList.Add(String.Copy(CurrentAbilityDefObject.cooldown_category))
                File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListCooldowncategory.txt"), CurrentAbilityDefObject.cooldown_category + vbCrLf)
            End If

            If Not DamageFunctionList.Contains(CurrentAbilityDefObject.damageFunction) AndAlso CurrentAbilityDefObject.damageFunction <> "" Then
                DamageFunctionList.Add(String.Copy(CurrentAbilityDefObject.damageFunction))
                File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListdamageFunction.txt"), CurrentAbilityDefObject.damageFunction + vbCrLf)
            End If
            If Not ToHistFunctionList.Contains(CurrentAbilityDefObject.toHitFunction) AndAlso CurrentAbilityDefObject.toHitFunction <> "" Then
                ToHistFunctionList.Add(String.Copy(CurrentAbilityDefObject.toHitFunction))
                File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListtoHitFunction.txt"), CurrentAbilityDefObject.toHitFunction + vbCrLf)
            End If

            For Each PrereqString As String In CurrentAbilityDefObject.prereqStrings
                If Not PreReqList.Contains(PrereqString) AndAlso PrereqString <> "" Then
                    PreReqList.Add(String.Copy(PrereqString))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListAbilityprereqStrings.txt"), PrereqString + vbCrLf)
                End If
            Next

        End If
    End Sub

#End Region

#Region "Modes Tab"
    Private Sub LoadMode(FileName As String)
        If Not File.Exists(FileName) Then
            Return
        End If
        Using Fs As FileStream = File.Open(FileName, FileMode.Open)

            WindowSRRItemEditor.CurrentModeDefObject = Serializer.Deserialize(Of isogame.ModeDef)(Fs)

            If ModesNamesList.IndexOf(Microsoft.VisualBasic.Left(Path.GetFileNameWithoutExtension(FileName), Path.GetFileNameWithoutExtension(FileName).Length - 5)) = -1 Then
                ModesList.Add(CurrentModeDefObject)
                ModesNamesList.Add(Microsoft.VisualBasic.Left(Path.GetFileNameWithoutExtension(FileName), Path.GetFileNameWithoutExtension(FileName).Length - 5))

            Else
                CurrentModeDefObject = ModesList.Item(ModesNamesList.IndexOf(Microsoft.VisualBasic.Left(Path.GetFileNameWithoutExtension(FileName), Path.GetFileNameWithoutExtension(FileName).Length - 5)))
            End If
            Try
                If TryCast(TryCast(tabControl.SelectedItem, TabItem).Content, ModeTabContent) IsNot Nothing Then
                    TryCast(TryCast(tabControl.SelectedItem, TabItem).Content, ModeTabContent).DataContext = CurrentModeDefObject

                End If
            Catch
            End Try

        End Using
        If Not ManualSelection Then

            If CurrentModeDefObject.uirep IsNot Nothing Then
                If Not IconList.Contains(CurrentModeDefObject.uirep.icon) AndAlso CurrentModeDefObject.uirep.icon <> "" Then
                    IconList.Add(String.Copy(CurrentModeDefObject.uirep.icon))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListModeicon.txt"), CurrentModeDefObject.uirep.icon + vbCrLf)
                End If
            End If

            For Each PrereqString As String In CurrentModeDefObject.prereqStrings
                If Not PreReqList.Contains(PrereqString) AndAlso PrereqString <> "" Then
                    PreReqList.Add(String.Copy(PrereqString))
                    File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListModeprereqStrings.txt"), PrereqString + vbCrLf)
                End If
            Next


        End If

    End Sub

#End Region

#Region "Load Functions"

    Public Sub LoadBytesfiles(ByVal FileName As String, Optional Silent As Boolean = False)
        Dim FileNameArray() As String
        FileNameArray = FileName.Split(CChar("."))

        Dim TypeSelector As String = FileNameArray(FileNameArray.GetUpperBound(0) - 1)
        Select Case TypeSelector
            Case "ab"
                LoadAbility(FileName)
            Case "ch_inst"
            Case "convo"
            Case "item"
                LoadItem(FileName)
                If Not Silent Then
                    '  FillItemTab()
                    If CType(tabControl.SelectedItem, TabItem).Header.ToString() = "Items" Then
                        CType(CType(tabControl.SelectedItem, TabItem).Content, ItemTabContent).FillTab()
                    End If
                End If
            Case "srm"
            Case "pflib"
            Case "srm"
            Case "mode"
                LoadMode(FileName)
            Case "pb"
            Case "srt"
            Case "pb"
            Case "story"
            Case "topic"
            Case "mf"
                LoadManifest(FileName)
            Case "cpack"
                LoadContentPack(FileName)
                AddingDependancies = False
                FillContentPackTreeView()
            Case "ambi"
            Case "ch_sht"
            Case "hiring"
            Case "pl"
            Case "ai"
            Case "alib"
            Case "blib"
            Case "credentials"
            Case "mlib"
            Case "slib"
            Case "pflib"
            Case "tlib"
            Case "submix"
            Case Else
        End Select

    End Sub

    Public Sub LoadTxtFiles(ByVal FileName As String, Optional Silent As Boolean = False)
        Dim FileNameArray() As String
        FileNameArray = FileName.Split(CChar("."))

        Dim TypeSelector As String = FileNameArray(FileNameArray.GetUpperBound(0) - 1)
        Select Case TypeSelector
            Case "ab"

                '        ObjectType = CType(parseProtoBufTextFormat(ProtoBufText, ObjectType), AbilityDef)

            Case "ch_inst"
            Case "convo"
            Case "item"
                LoadItem(FileName)
                If Not Silent Then
                    'FillItemTab()
                    If CType(tabControl.SelectedItem, TabItem).Header.ToString() = "Items" Then
                        'CType(CType(tabControl.SelectedItem, TabItem).Content, ItemsTabContent).FillTab()
                    End If
                End If
            Case "srm"
            Case "pflib"
            Case "srm"
            Case "mode"
                LoadMode(FileName)
            Case "pb"
            Case "srt"
            Case "pb"
            Case "story"
            Case "topic"
            Case "mf"
                LoadManifest(FileName)
            Case "cpack"
                LoadContentPack(FileName)
                AddingDependancies = False
                FillContentPackTreeView()
            Case "ambi"
            Case "ch_sht"
            Case "hiring"
            Case "pl"
            Case "ai"
            Case "alib"
            Case "blib"
            Case "credentials"
            Case "mlib"
            Case "slib"
            Case "pflib"
            Case "tlib"
            Case "submix"
            Case Else
        End Select

    End Sub

    Private Sub LoadManifest(ByVal ManifestFile As String)
        Using Fs As FileStream = File.Open(ManifestFile, FileMode.Open)
            Dim TempManifest As isogame.Manifest
            TempManifest = Serializer.Deserialize(Of isogame.Manifest)(Fs)
            If Not DataManifestList.Contains(TempManifest) Then
                DataManifestList.Add(TempManifest)
            End If
        End Using
    End Sub

#End Region

#Region "Misc Functions"
    Private Function FindSubFolders(ByVal dir As DirectoryInfo, ByVal DirName As String) As System.Collections.Specialized.StringCollection
        If FindSubFolders Is Nothing Then
            FindSubFolders = New System.Collections.Specialized.StringCollection
        End If
        Dim result As New System.Collections.Specialized.StringCollection
        For Each folder As DirectoryInfo In dir.GetDirectories()
            If folder.Name = DirName Then
                FindSubFolders.Add(folder.FullName)
            Else
                For Each location As String In FindSubFolders(folder, DirName)
                    FindSubFolders.Add(location)
                Next
            End If
        Next
        Return FindSubFolders
    End Function

    Private Function FindSubFoldersSpecial(ByVal dir As DirectoryInfo, ByVal DirName As String) As String
        Dim result As String = String.Empty
        For Each folder As DirectoryInfo In dir.GetDirectories()
            If folder.Name <> "art" AndAlso folder.Name <> "data" Then
                If folder.Name.ToLower().Contains(DirName.ToLower()) Then
                    result = folder.FullName
                    Exit For
                Else
                    result = FindSubFoldersSpecial(folder, DirName)
                End If
            End If
        Next
        Return result
    End Function

    Private Function RecursiveSearch(ByVal strPath As String, ByVal strPattern As String) As String
        If strPath = "" Then
            Return String.Empty
        End If

        Dim strFolders() As String = System.IO.Directory.GetDirectories(strPath)
        Dim strFiles() As String = System.IO.Directory.GetFiles(strPath, strPattern)
        Dim result As String = String.Empty

        'Add the files 
        For Each strFile As String In strFiles
            If New FileInfo(strFile).Name = strPattern Then
                result = New FileInfo(strFile).FullName
            End If
        Next

        If result = String.Empty Then
            'Look through the other folders 
            For Each strFolder As String In strFolders
                'Call the procedure again to perform the same operation 
                result = RecursiveSearch(strFolder, strPattern)
            Next
        End If
        Return result
    End Function

    Public Function FileTester(DirPath As String, FileName As String, Extension As String) As String
        Dim FilePath As String
        If File.Exists(DirPath & FileName & Extension) Then
            Dim key As String
            Dim m As Match = Regex.Match(FileName, _
                 "bytes\([1-9\-]+\)$", _
                 RegexOptions.None) '"bytes\([1-9\-]+\)\.bak$"
            ' If successful, write the group.
            If (m.Success) Then
                key = m.Groups(0).Value.Substring(6, m.Groups(0).Value.Length - 7)
                Dim removestring As String = "(" + key + ")"
                FileName = Microsoft.VisualBasic.Left(FileName, FileName.Length - removestring.Length)
            Else
                key = CStr(0)
            End If
            FilePath = FileTester(DirPath, FileName + "(" + CStr(CInt(key) + 1) + ")", Extension)
        Else
            FilePath = DirPath & FileName & Extension
        End If
        Return FilePath
    End Function

#End Region

#Region "Private Classes"

    Private Class ManifestComparer
        Inherits Comparer(Of Manifest.Entry)
        Implements IComparer(Of Manifest.Entry)
        Public Overrides Function Compare(x As Manifest.Entry, y As Manifest.Entry) As Integer Implements IComparer(Of Manifest.Entry).Compare

            If x.name.CompareTo(y.name) <> 0 Then
                Return x.name.CompareTo(y.name)
            ElseIf x.digest.CompareTo(y.digest) <> 0 Then
                Return x.digest.CompareTo(y.digest)
            ElseIf x.digest_method.CompareTo(y.digest_method) <> 0 Then
                Return x.digest_method.CompareTo(y.digest_method)
            Else
                Return 0
            End If
        End Function
    End Class

#End Region

End Class
