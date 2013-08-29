Imports System.IO
Imports Microsoft.VisualBasic.CompilerServices
Imports ProtoBuf
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports isogame
Imports System.Data
Imports System.Linq
Imports System.Data.DataSetExtensions
Imports System.Text.RegularExpressions
Imports Xceed.Wpf.Toolkit
Imports Microsoft.VisualBasic
Imports System.Reflection.Assembly
Imports ShadowrunReturnsItemEditor.WindowSRRItemEditor


Public Class ItemTabContent

    Public WithEvents RangeModTable As New DataTable
    Public Loading As Boolean = False
    Public ManualSelection As Boolean = False
    Public AddingDependancies As Boolean = False
    Public CurrentTabname As String

    Public TreeViewContentPack As TreeView


    Friend Sub FillTab()
        Loading = True
        'Integer items
        If CurrentItemDefObject IsNot Nothing Then
            TextBoxAIPriority.Value = WindowSRRItemEditor.CurrentItemDefObject.ai_priority
            TextBoxAdditionalTargets.Value = WindowSRRItemEditor.CurrentItemDefObject.additionalTargets
            TextBoxAmmoReloadAPCost.Value = WindowSRRItemEditor.CurrentItemDefObject.ammoReloadAPCost
            TextBoxApCost.Value = WindowSRRItemEditor.CurrentItemDefObject.apCost
            TextBoxBaseAPDamage.Value = WindowSRRItemEditor.CurrentItemDefObject.baseAPDamage
            TextBoxBaseHPDamage.Value = WindowSRRItemEditor.CurrentItemDefObject.baseHPDamage
            TextBoxCooldown.Value = WindowSRRItemEditor.CurrentItemDefObject.cooldown
            TextBoxDeckingBody.Value = WindowSRRItemEditor.CurrentItemDefObject.decking_body
            TextBoxCredentials.Value = WindowSRRItemEditor.CurrentItemDefObject.credentials
            TextBoxDeckingEspLimit.Value = WindowSRRItemEditor.CurrentItemDefObject.decking_esp_limit
            TextBoxDeckingEvasion.Value = WindowSRRItemEditor.CurrentItemDefObject.decking_evasion
            TextBoxDeckingHardening.Value = WindowSRRItemEditor.CurrentItemDefObject.decking_hardening
            TextBoxDeckingMaxAp.Value = WindowSRRItemEditor.CurrentItemDefObject.decking_max_ap
            TextBoxDeckingMaxIp.Value = WindowSRRItemEditor.CurrentItemDefObject.decking_max_ip
            TextBoxDeckingProgramLimit.Value = WindowSRRItemEditor.CurrentItemDefObject.decking_program_limit
            TextBoxEffectDuration.Value = WindowSRRItemEditor.CurrentItemDefObject.effectDuration
            TextBoxEffectRadius.Value = WindowSRRItemEditor.CurrentItemDefObject.effectRadius
            TextBoxForceRating.Value = WindowSRRItemEditor.CurrentItemDefObject.forceRating
            TextBoxMaxAmmo.Value = WindowSRRItemEditor.CurrentItemDefObject.maxAmmo
            TextBoxNoiseLevel.Value = WindowSRRItemEditor.CurrentItemDefObject.noiseLevel
            TextBoxNoiseRounds.Value = WindowSRRItemEditor.CurrentItemDefObject.noiseRounds
            TextBoxReactionsPerUse.Value = WindowSRRItemEditor.CurrentItemDefObject.reactions_per_use
            TextBoxSpreadAngle.Value = WindowSRRItemEditor.CurrentItemDefObject.spreadAngle
            TextBoxStoreCost.Value = WindowSRRItemEditor.CurrentItemDefObject.store_cost

            'string items
            If WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects IsNot Nothing Then
                TextBoxASEDurationFxScript.Text = WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.durationFxScript
                TextBoxASEFxScript.Text = WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.fxScript
                TextBoxASEStackingCategory.Text = WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.stackingCategory
                If WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep IsNot Nothing Then
                    TextBoxASEUIRepName.Text = WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep.name
                    TextBoxASEUIRepDescription.Text = WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep.description
                    TextBoxASEUIRepIcon.Text = WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep.icon
                    TextBoxASEUIRepThumbnail.Text = WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep.thumbnail
                Else
                    TextBoxASEUIRepName.Text = ""
                    TextBoxASEUIRepDescription.Text = ""
                    TextBoxASEUIRepIcon.Text = ""
                    TextBoxASEUIRepThumbnail.Text = ""
                End If
            Else
                TextBoxASEDurationFxScript.Text = ""
                TextBoxASEFxScript.Text = ""
                TextBoxASEStackingCategory.Text = ""
                TextBoxASEUIRepName.Text = ""
                TextBoxASEUIRepDescription.Text = ""
                TextBoxASEUIRepIcon.Text = ""
                TextBoxASEUIRepThumbnail.Text = ""
            End If

            If WindowSRRItemEditor.CurrentItemDefObject.fxrep IsNot Nothing Then
                TextBoxActionFxName.Text = WindowSRRItemEditor.CurrentItemDefObject.fxrep.actionFxName
                TextBoxPostActionFxName.Text = WindowSRRItemEditor.CurrentItemDefObject.fxrep.postActionFxName
                TextBoxPreActionFxName.Text = WindowSRRItemEditor.CurrentItemDefObject.fxrep.preActionFxName
                TextBoxHitReactionFxName.Text = WindowSRRItemEditor.CurrentItemDefObject.fxrep.hitReactionFxName
                TextBoxMissReactionFxName.Text = WindowSRRItemEditor.CurrentItemDefObject.fxrep.missReactionFxName
            Else
                TextBoxActionFxName.Text = ""
                TextBoxPostActionFxName.Text = ""
                TextBoxPreActionFxName.Text = ""
                TextBoxHitReactionFxName.Text = ""
                TextBoxMissReactionFxName.Text = ""
            End If

            If WindowSRRItemEditor.CurrentItemDefObject.character_prefab_id IsNot Nothing Then
                TextBoxCharacterPrefabId.Text = WindowSRRItemEditor.CurrentItemDefObject.character_prefab_id
            Else
                TextBoxCharacterPrefabId.Text = ""
            End If

            If WindowSRRItemEditor.CurrentItemDefObject.character_sheet_id IsNot Nothing Then
                TextBoxCharacterSheetId.Text = WindowSRRItemEditor.CurrentItemDefObject.character_sheet_id
            Else
                TextBoxCharacterSheetId.Text = ""
            End If

            If WindowSRRItemEditor.CurrentItemDefObject.character_ui_name IsNot Nothing Then
                TextBoxCharacterUIName.Text = WindowSRRItemEditor.CurrentItemDefObject.character_ui_name
            Else
                TextBoxCharacterUIName.Text = ""
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.cooldown_category IsNot Nothing Then
                TextBoxCooldownCategory.Text = WindowSRRItemEditor.CurrentItemDefObject.cooldown_category
            Else
                TextBoxCooldownCategory.Text = ""
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.decking_default_weapon IsNot Nothing Then
                TextBoxDeckingDefaultWeapon.Text = WindowSRRItemEditor.CurrentItemDefObject.decking_default_weapon
            Else
                TextBoxDeckingDefaultWeapon.Text = ""
            End If

            If WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects IsNot Nothing Then
                TextBoxESEDurationFxScript.Text = WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.durationFxScript
                TextBoxESEFxScript.Text = WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.fxScript
                TextBoxESEStackingCategory.Text = WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.stackingCategory
                If WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep IsNot Nothing Then
                    TextBoxESEUIRepDescription.Text = WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep.description
                    TextBoxESEUIRepIcon.Text = WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep.icon
                    TextBoxESEUIRepName.Text = WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep.name
                    TextBoxESEUIRepThumbnail.Text = WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep.thumbnail
                Else
                    TextBoxESEUIRepDescription.Text = ""
                    TextBoxESEUIRepIcon.Text = ""
                    TextBoxESEUIRepName.Text = ""
                    TextBoxESEUIRepThumbnail.Text = ""
                End If
            Else
                TextBoxESEDurationFxScript.Text = ""
                TextBoxESEFxScript.Text = ""
                TextBoxESEStackingCategory.Text = ""
                TextBoxESEUIRepDescription.Text = ""
                TextBoxESEUIRepIcon.Text = ""
                TextBoxESEUIRepName.Text = ""
                TextBoxESEUIRepThumbnail.Text = ""
            End If

            If WindowSRRItemEditor.CurrentItemDefObject.equipPrefabName IsNot Nothing Then
                TextBoxEquipPrefabName.Text = WindowSRRItemEditor.CurrentItemDefObject.equipPrefabName
            Else
                TextBoxEquipPrefabName.Text = ""
            End If

            If WindowSRRItemEditor.CurrentItemDefObject.gear_bundle IsNot Nothing Then
                TextBoxGearBundle.Text = WindowSRRItemEditor.CurrentItemDefObject.gear_bundle
            Else
                TextBoxGearBundle.Text = ""
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.gear_prefab IsNot Nothing Then
                TextBoxGearPrefab.Text = WindowSRRItemEditor.CurrentItemDefObject.gear_prefab
            Else
                TextBoxGearPrefab.Text = ""
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.id IsNot Nothing Then
                TextBoxId.Text = WindowSRRItemEditor.CurrentItemDefObject.id
            Else
                TextBoxId.Text = ""
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.outfit_texture IsNot Nothing Then
                TextBoxOutfitTexture.Text = WindowSRRItemEditor.CurrentItemDefObject.outfit_texture
            Else
                TextBoxOutfitTexture.Text = ""

            End If
            If WindowSRRItemEditor.CurrentItemDefObject.sorting_group IsNot Nothing Then
                TextBoxSortingGroup.Text = WindowSRRItemEditor.CurrentItemDefObject.sorting_group
            Else
                TextBoxSortingGroup.Text = ""

            End If
            If WindowSRRItemEditor.CurrentItemDefObject.uirep IsNot Nothing Then
                TextBoxUIRepDescription.Text = WindowSRRItemEditor.CurrentItemDefObject.uirep.description
                TextBoxUIRepIcon.Text = WindowSRRItemEditor.CurrentItemDefObject.uirep.icon
                TextBoxUIRepName.Text = WindowSRRItemEditor.CurrentItemDefObject.uirep.name
                TextBoxUIRepThumbnail.Text = WindowSRRItemEditor.CurrentItemDefObject.uirep.thumbnail
            Else
                TextBoxUIRepDescription.Text = ""
                TextBoxUIRepIcon.Text = ""
                TextBoxUIRepName.Text = ""
                TextBoxUIRepThumbnail.Text = ""
            End If

            'Dropdown list choices
            'If WindowSRRItemEditor.CurrentItemDefObject.anim_type IsNot Nothing Then
            comboBoxAnimType.SelectedIndex = comboBoxAnimType.Items.IndexOf([Enum].GetName(GetType(AnimType), WindowSRRItemEditor.CurrentItemDefObject.anim_type))
            ' End If
            ' If WindowSRRItemEditor.CurrentItemDefObject.coreAttribute IsNot Nothing Then

            comboBoxCoreAttribute.SelectedIndex = comboBoxCoreAttribute.Items.IndexOf([Enum].GetName(GetType(isogame.Attribute), WindowSRRItemEditor.CurrentItemDefObject.coreAttribute))

            comboBoxCoreSkill.SelectedIndex = comboBoxCoreSkill.Items.IndexOf([Enum].GetName(GetType(isogame.Skill), WindowSRRItemEditor.CurrentItemDefObject.coreSkill))

            comboBoxCoreSpecialization.SelectedIndex = comboBoxCoreSpecialization.Items.IndexOf([Enum].GetName(GetType(isogame.Specialization), WindowSRRItemEditor.CurrentItemDefObject.coreSpecialization))

            comboBoxItemType.SelectedIndex = comboBoxItemType.Items.IndexOf([Enum].GetName(GetType(isogame.ItemType), WindowSRRItemEditor.CurrentItemDefObject.type))

            ComboBoxCyberwareType.SelectedIndex = ComboBoxCyberwareType.Items.IndexOf([Enum].GetName(GetType(isogame.CyberwareType), WindowSRRItemEditor.CurrentItemDefObject.cyberware_type))

            ComboBoxIntendedUser.SelectedIndex = ComboBoxIntendedUser.Items.IndexOf([Enum].GetName(GetType(isogame.IntendedUser), WindowSRRItemEditor.CurrentItemDefObject.intended_user))

            'Boolean choices
            If WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects IsNot Nothing Then
                If WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.is_buff Then
                    ComboBoxASEIsBuff.SelectedIndex = 0
                Else
                    ComboBoxASEIsBuff.SelectedIndex = 1
                End If
                If WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.is_debuff Then
                    ComboBoxASEIsDebuff.SelectedIndex = 0
                Else
                    ComboBoxASEIsDebuff.SelectedIndex = 1
                End If
                If WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.is_totem Then
                    ComboBoxASEIsTotem.SelectedIndex = 0
                Else
                    ComboBoxASEIsTotem.SelectedIndex = 1
                End If
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.affectsDecker Then
                ComboBoxAffectsDecker.SelectedIndex = 0
            Else
                ComboBoxAffectsDecker.SelectedIndex = 1
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.affectsEnemy Then

                ComboBoxAffectsEnemy.SelectedIndex = 0
            Else
                ComboBoxAffectsEnemy.SelectedIndex = 1
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.affectsFriendly Then
                ComboBoxAffectsFriendly.SelectedIndex = 0
            Else
                ComboBoxAffectsFriendly.SelectedIndex = 1
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.canTargetActor Then
                ComboBoxCanTargetActor.SelectedIndex = 0
            Else
                ComboBoxCanTargetActor.SelectedIndex = 1
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.canTargetOccupiedGridPoint Then
                ComboBoxCanTargetOccupiedGridPoint.SelectedIndex = 0
            Else
                ComboBoxCanTargetOccupiedGridPoint.SelectedIndex = 1
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.canTargetUnoccupiedGridPoint Then
                ComboBoxCanTargetUnoccupiedGridPoint.SelectedIndex = 0
            Else
                ComboBoxCanTargetUnoccupiedGridPoint.SelectedIndex = 1
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.canTargetSelf Then
                ComboBoxCanTargetSelf.SelectedIndex = 0
            Else
                ComboBoxCanTargetSelf.SelectedIndex = 1
            End If

            If WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects IsNot Nothing Then
                If WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.is_buff Then
                    ComboBoxESEIsBuff.SelectedIndex = 0
                Else
                    ComboBoxESEIsBuff.SelectedIndex = 1
                End If
                If WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.is_debuff Then
                    ComboBoxESEIsDebuff.SelectedIndex = 0
                Else
                    ComboBoxESEIsDebuff.SelectedIndex = 1
                End If
                If WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.is_totem Then
                    ComboBoxESEIsTotem.SelectedIndex = 0
                Else
                    ComboBoxESEIsTotem.SelectedIndex = 1
                End If
            End If

            If WindowSRRItemEditor.CurrentItemDefObject.gear_covers_arms Then
                ComboBoxGearCoversArms.SelectedIndex = 0
            Else
                ComboBoxGearCoversArms.SelectedIndex = 1
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.gear_covers_face Then
                ComboBoxGearCoversFace.SelectedIndex = 0
            Else
                ComboBoxGearCoversFace.SelectedIndex = 1
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.gear_covers_hair Then
                ComboBoxGearCoversHair.SelectedIndex = 0
            Else
                ComboBoxGearCoversHair.SelectedIndex = 1
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.isBuff Then
                ComboBoxIsBuff.SelectedIndex = 0
            Else
                ComboBoxIsBuff.SelectedIndex = 1
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.isDebuff Then
                ComboBoxIsDebuff.SelectedIndex = 0
            Else
                ComboBoxIsDebuff.SelectedIndex = 1
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.isMagic Then
                ComboBoxIsMagic.SelectedIndex = 0
            Else
                ComboBoxIsMagic.SelectedIndex = 1
            End If
            If WindowSRRItemEditor.CurrentItemDefObject.effectOnTile Then
                ComboBoxEffectOnTile.SelectedIndex = 0
            Else
                ComboBoxEffectOnTile.SelectedIndex = 1
            End If

            'lists
            listBoxASEStatusConditions.Items.Clear()
            If WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects IsNot Nothing Then
                For Each StatCon As isogame.StatusCondition In WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.statusConditions
                    listBoxASEStatusConditions.Items.Add([Enum].GetName(GetType(isogame.StatusCondition), StatCon))
                Next
            End If

            listBoxAbilityModes.Items.Clear()
            For Each AbilityMode As String In WindowSRRItemEditor.CurrentItemDefObject.abilityModes
                listBoxAbilityModes.Items.Add(AbilityMode)
            Next

            listBoxESEStatusConditions.Items.Clear()
            If WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects IsNot Nothing Then
                For Each StatCon As isogame.StatusCondition In WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.statusConditions
                    listBoxESEStatusConditions.Items.Add([Enum].GetName(GetType(isogame.StatusCondition), StatCon))
                Next
            End If

            listBoxEffectModTable.Items.Clear()
            For Each EffectMod As Single In WindowSRRItemEditor.CurrentItemDefObject.effectModTable
                listBoxEffectModTable.Items.Add(EffectMod.ToString())
            Next

            listBoxEquipmentSheetId.Items.Clear()
            For Each EquipSheetID As String In WindowSRRItemEditor.CurrentItemDefObject.equipment_sheet_id
                listBoxEquipmentSheetId.Items.Add(EquipSheetID.ToString())
            Next

            listBoxModelessAbilities.Items.Clear()
            For Each ModelessAbility As String In WindowSRRItemEditor.CurrentItemDefObject.modelessAbilities
                listBoxModelessAbilities.Items.Add(ModelessAbility.ToString())
            Next

            listBoxPrereqStrings.Items.Clear()
            For Each PrereqString As String In WindowSRRItemEditor.CurrentItemDefObject.prereqStrings
                listBoxPrereqStrings.Items.Add(PrereqString.ToString())
            Next

            If RangeModTable.Rows IsNot Nothing Then
                RangeModTable.Rows.Clear()
                RangeModTable = New DataTable
            End If

            Dim Keys(1) As DataColumn
            Dim Row As DataRow
            Dim Column As New DataColumn

            If Not RangeModTable.Columns.Contains("Range") Then
                Column = New DataColumn()
                Column.DataType = System.Type.GetType("System.Int32")
                Column.ColumnName = "Range"
                Column.AutoIncrement = True
                Column.Caption = "Range"
                Column.ReadOnly = True
                RangeModTable.Columns.Add(Column)
                ' Keys(0) = Column
                'RangeModTable.PrimaryKey = Keys
            End If
            If Not RangeModTable.Columns.Contains("Modification") Then
                Column = New DataColumn()
                Column.DataType = System.Type.GetType("System.Single")
                Column.ColumnName = "Modification"
                Column.AutoIncrement = False
                Column.Caption = "Modification"
                Column.ReadOnly = False
                RangeModTable.Columns.Add(Column)
            End If

            For Each RangeMod As Single In WindowSRRItemEditor.CurrentItemDefObject.rangeModTable
                Row = RangeModTable.NewRow()
                Row("Modification") = RangeMod
                RangeModTable.Rows.Add(Row)
            Next
            DataGridRangeModTable.ItemsSource = RangeModTable.DefaultView
            'DataGridRangeModTable.Columns(0).Width = 70
            'DataGridRangeModTable.Columns(1).Width = 100
            textBlockMaxRange.Text = CStr(RangeModTable.Rows.Count)

            'complicated lists

            ListViewASEStatMods.Items.Clear()
            If WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects IsNot Nothing Then
                For Each Statmod As isogame.StatMod In WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.statMods
                    ListViewASEStatMods.Items.Add(Statmod)
                Next
            End If

            ListViewDrainBucketFailureEntries.Items.Clear()
            ListViewDrainBucketSuccesEntries.Items.Clear()
            If WindowSRRItemEditor.CurrentItemDefObject.drainBucket IsNot Nothing Then
                For Each FailureEntry As isogame.ResultBucket.Entry In WindowSRRItemEditor.CurrentItemDefObject.drainBucket.failureEntries
                    ListViewDrainBucketFailureEntries.Items.Add(FailureEntry)
                Next
                For Each SuccessEntry As isogame.ResultBucket.Entry In WindowSRRItemEditor.CurrentItemDefObject.drainBucket.successEntries
                    ListViewDrainBucketSuccesEntries.Items.Add(SuccessEntry)
                Next
            End If

            ListViewESEStatMods.Items.Clear()
            If WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects IsNot Nothing Then
                For Each Statmod As isogame.StatMod In WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.statMods
                    ListViewESEStatMods.Items.Add(Statmod)
                Next
            End If
        End If
        Loading = False
    End Sub

    Friend Sub StoreChangesItems()
        Dim index As Integer = WindowSRRItemEditor.ItemList.IndexOf(WindowSRRItemEditor.CurrentItemDefObject)

        If WindowSRRItemEditor.CurrentItemDefObject Is Nothing Then
            WindowSRRItemEditor.CurrentItemDefObject = New ItemDef
            WindowSRRItemEditor.ItemList.Add(WindowSRRItemEditor.CurrentItemDefObject)
            Dim IndexOfAddedItem As Integer = WindowSRRItemEditor.ItemList.IndexOf(WindowSRRItemEditor.CurrentItemDefObject)



            If TreeviewItemFinder(TreeViewContentPack, CreateTreeViewFolderItem("NewObjects", "NewObject")) Is Nothing Then
                Dim TVINewObjects As TreeViewItem = CreateTreeViewFolderItem("NewObjects", "NewObject")
                TreeViewContentPack.Items.Add(TVINewObjects)
            End If
            If TreeviewItemFinder(TreeviewItemFinder(TreeViewContentPack, CreateTreeViewFolderItem("NewObjects", "NewObject")), CreateTreeViewFolderItem("items")) Is Nothing Then
                Dim TVINewItemsFolder As TreeViewItem = CreateTreeViewFolderItem("items")
                TreeviewItemFinder(TreeViewContentPack, CreateTreeViewFolderItem("NewObjects", "NewObject")).Items.Add(TVINewItemsFolder)
            End If
            TreeviewItemFinder(TreeviewItemFinder(TreeViewContentPack, CreateTreeViewFolderItem("NewObjects", "NewObject")), CreateTreeViewFolderItem("items")).Items.Add(CreateTreeViewItem(TextBoxId.Text, IndexOfAddedItem.ToString()))

        End If


        If TextBoxId.Text = "" Then
            'TODO usermessage, need id
            Return
        End If

        'Integer items
        If TextBoxAIPriority.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.ai_priority = CInt(TextBoxAIPriority.Value)
        End If
        If TextBoxAdditionalTargets.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.additionalTargets = CInt(TextBoxAdditionalTargets.Value)
        End If
        If TextBoxAmmoReloadAPCost.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.ammoReloadAPCost = CInt(TextBoxAmmoReloadAPCost.Value)
        End If
        If TextBoxApCost.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.apCost = CInt(TextBoxApCost.Value)
        End If
        If TextBoxCooldown.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.cooldown = CInt(TextBoxCooldown.Value)
        End If
        If TextBoxDeckingBody.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.decking_body = CInt(TextBoxDeckingBody.Value)
        End If
        If TextBoxCredentials.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.credentials = CInt(TextBoxCredentials.Value)
        End If
        If TextBoxDeckingEspLimit.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.decking_esp_limit = CInt(TextBoxDeckingEspLimit.Value)
        End If
        If TextBoxDeckingEvasion.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.decking_evasion = CInt(TextBoxDeckingEvasion.Value)
        End If
        If TextBoxDeckingHardening.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.decking_hardening = CInt(TextBoxDeckingHardening.Value)
        End If
        If TextBoxDeckingMaxAp.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.decking_max_ap = CInt(TextBoxDeckingMaxAp.Value)
        End If
        If TextBoxDeckingMaxIp.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.decking_max_ip = CInt(TextBoxDeckingMaxIp.Value)
        End If
        If TextBoxDeckingProgramLimit.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.decking_program_limit = CInt(TextBoxDeckingProgramLimit.Value)
        End If
        If TextBoxEffectDuration.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.effectDuration = CInt(TextBoxEffectDuration.Value)
        End If
        If TextBoxEffectRadius.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.effectRadius = CInt(TextBoxEffectRadius.Value)
        End If
        If TextBoxForceRating.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.forceRating = CInt(TextBoxForceRating.Value)
        End If
        If TextBoxMaxAmmo.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.maxAmmo = CInt(TextBoxMaxAmmo.Value)
        End If
        If TextBoxNoiseRounds.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.noiseRounds = CInt(TextBoxNoiseRounds.Value)
        End If
        If TextBoxReactionsPerUse.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.reactions_per_use = CInt(TextBoxReactionsPerUse.Value)
        End If
        If TextBoxStoreCost.Value <> 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.store_cost = CInt(TextBoxStoreCost.Value)
        End If

        If TextBoxBaseAPDamage.Value <> 0.0 Then
            WindowSRRItemEditor.CurrentItemDefObject.baseAPDamage = CSng(TextBoxBaseAPDamage.Value)
        End If
        If TextBoxBaseHPDamage.Value <> 0.0 Then
            WindowSRRItemEditor.CurrentItemDefObject.baseHPDamage = CSng(TextBoxBaseHPDamage.Value)
        End If
        If TextBoxNoiseLevel.Value <> 0.0 Then
            WindowSRRItemEditor.CurrentItemDefObject.noiseLevel = CSng(TextBoxNoiseLevel.Value)
        End If
        If TextBoxSpreadAngle.Value <> 0.0 Then
            WindowSRRItemEditor.CurrentItemDefObject.spreadAngle = CSng(TextBoxSpreadAngle.Value)
        End If

        'string items
        If WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects IsNot Nothing Then
            WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.durationFxScript = TextBoxASEDurationFxScript.Text
            WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.fxScript = TextBoxASEFxScript.Text
            WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.stackingCategory = TextBoxASEStackingCategory.Text
            If WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep IsNot Nothing Then
                WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep.name = TextBoxASEUIRepName.Text
                WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep.description = TextBoxASEUIRepDescription.Text
                WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep.icon = TextBoxASEUIRepIcon.Text
                WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep.thumbnail = TextBoxASEUIRepThumbnail.Text
            Else
                If TextBoxASEUIRepName.Text <> "" OrElse TextBoxASEUIRepDescription.Text <> "" OrElse TextBoxASEUIRepIcon.Text <> "" OrElse TextBoxASEUIRepThumbnail.Text <> "" Then
                    WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep = New isogame.UIRep()
                    WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep.name = TextBoxASEUIRepName.Text
                    WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep.description = TextBoxASEUIRepDescription.Text
                    WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep.icon = TextBoxASEUIRepIcon.Text
                    WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep.thumbnail = TextBoxASEUIRepThumbnail.Text
                End If
            End If
        Else
            If TextBoxASEDurationFxScript.Text <> "" OrElse TextBoxASEFxScript.Text <> "" OrElse TextBoxASEStackingCategory.Text <> "" Then
                WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects = New isogame.StatusEffects
                WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.durationFxScript = TextBoxASEDurationFxScript.Text
                WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.fxScript = TextBoxASEFxScript.Text
                WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.stackingCategory = TextBoxASEStackingCategory.Text
                If TextBoxASEUIRepName.Text <> "" OrElse TextBoxASEUIRepDescription.Text <> "" OrElse TextBoxASEUIRepIcon.Text <> "" OrElse TextBoxASEUIRepThumbnail.Text <> "" Then
                    WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep = New isogame.UIRep()
                    WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep.name = TextBoxASEUIRepName.Text
                    WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep.description = TextBoxASEUIRepDescription.Text
                    WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep.icon = TextBoxASEUIRepIcon.Text
                    WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.uirep.thumbnail = TextBoxASEUIRepThumbnail.Text
                End If
            End If
        End If

        If WindowSRRItemEditor.CurrentItemDefObject.fxrep IsNot Nothing Then
            WindowSRRItemEditor.CurrentItemDefObject.fxrep.actionFxName = TextBoxActionFxName.Text
            WindowSRRItemEditor.CurrentItemDefObject.fxrep.postActionFxName = TextBoxPostActionFxName.Text
            WindowSRRItemEditor.CurrentItemDefObject.fxrep.preActionFxName = TextBoxPreActionFxName.Text
            WindowSRRItemEditor.CurrentItemDefObject.fxrep.hitReactionFxName = TextBoxHitReactionFxName.Text
            WindowSRRItemEditor.CurrentItemDefObject.fxrep.missReactionFxName = TextBoxMissReactionFxName.Text
        Else
            If TextBoxActionFxName.Text <> "" OrElse TextBoxPostActionFxName.Text <> "" OrElse TextBoxPreActionFxName.Text <> "" OrElse TextBoxHitReactionFxName.Text <> "" OrElse TextBoxMissReactionFxName.Text <> "" Then
                WindowSRRItemEditor.CurrentItemDefObject.fxrep = New isogame.FxRep()
                WindowSRRItemEditor.CurrentItemDefObject.fxrep.actionFxName = TextBoxActionFxName.Text
                WindowSRRItemEditor.CurrentItemDefObject.fxrep.postActionFxName = TextBoxPostActionFxName.Text
                WindowSRRItemEditor.CurrentItemDefObject.fxrep.preActionFxName = TextBoxPreActionFxName.Text
                WindowSRRItemEditor.CurrentItemDefObject.fxrep.hitReactionFxName = TextBoxHitReactionFxName.Text
                WindowSRRItemEditor.CurrentItemDefObject.fxrep.missReactionFxName = TextBoxMissReactionFxName.Text
            End If
        End If

        If TextBoxCharacterPrefabId.Text <> "" Then
            WindowSRRItemEditor.CurrentItemDefObject.character_prefab_id = TextBoxCharacterPrefabId.Text
        End If

        If TextBoxCharacterSheetId.Text <> "" Then
            WindowSRRItemEditor.CurrentItemDefObject.character_sheet_id = TextBoxCharacterSheetId.Text
        End If

        If TextBoxCharacterUIName.Text <> "" Then
            WindowSRRItemEditor.CurrentItemDefObject.character_ui_name = TextBoxCharacterUIName.Text
        End If

        If TextBoxCooldownCategory.Text <> "" Then
            WindowSRRItemEditor.CurrentItemDefObject.cooldown_category = TextBoxCooldownCategory.Text
        End If

        If TextBoxDeckingDefaultWeapon.Text <> "" Then
            WindowSRRItemEditor.CurrentItemDefObject.decking_default_weapon = TextBoxDeckingDefaultWeapon.Text
        End If

        If WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects IsNot Nothing Then
            WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.durationFxScript = TextBoxESEDurationFxScript.Text
            WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.fxScript = TextBoxESEFxScript.Text
            WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.stackingCategory = TextBoxESEStackingCategory.Text
            If WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep IsNot Nothing Then
                WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep.name = TextBoxESEUIRepName.Text
                WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep.description = TextBoxESEUIRepDescription.Text
                WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep.icon = TextBoxESEUIRepIcon.Text
                WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep.thumbnail = TextBoxESEUIRepThumbnail.Text
            Else
                If TextBoxESEUIRepName.Text <> "" OrElse TextBoxESEUIRepDescription.Text <> "" OrElse TextBoxESEUIRepIcon.Text <> "" OrElse TextBoxESEUIRepThumbnail.Text <> "" Then
                    WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep = New isogame.UIRep()
                    WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep.name = TextBoxESEUIRepName.Text
                    WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep.description = TextBoxESEUIRepDescription.Text
                    WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep.icon = TextBoxESEUIRepIcon.Text
                    WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep.thumbnail = TextBoxESEUIRepThumbnail.Text
                End If
            End If
        Else
            If TextBoxESEDurationFxScript.Text <> "" OrElse TextBoxESEFxScript.Text <> "" OrElse TextBoxESEStackingCategory.Text <> "" Then
                WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects = New isogame.StatusEffects
                WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.durationFxScript = TextBoxESEDurationFxScript.Text
                WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.fxScript = TextBoxESEFxScript.Text
                WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.stackingCategory = TextBoxESEStackingCategory.Text
                If TextBoxESEUIRepName.Text <> "" OrElse TextBoxESEUIRepDescription.Text <> "" OrElse TextBoxESEUIRepIcon.Text <> "" OrElse TextBoxESEUIRepThumbnail.Text <> "" Then
                    WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep = New isogame.UIRep()
                    WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep.name = TextBoxESEUIRepName.Text
                    WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep.description = TextBoxESEUIRepDescription.Text
                    WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep.icon = TextBoxESEUIRepIcon.Text
                    WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.uirep.thumbnail = TextBoxESEUIRepThumbnail.Text
                End If
            End If
        End If

        If TextBoxEquipPrefabName.Text <> "" Then
            WindowSRRItemEditor.CurrentItemDefObject.equipPrefabName = TextBoxEquipPrefabName.Text
        End If

        If TextBoxGearBundle.Text <> "" Then
            WindowSRRItemEditor.CurrentItemDefObject.gear_bundle = TextBoxGearBundle.Text
        End If

        If TextBoxGearPrefab.Text <> "" Then
            WindowSRRItemEditor.CurrentItemDefObject.gear_prefab = TextBoxGearPrefab.Text
        End If

        If TextBoxId.Text <> "" Then
            WindowSRRItemEditor.CurrentItemDefObject.id = TextBoxId.Text
        End If

        If TextBoxOutfitTexture.Text <> "" Then
            WindowSRRItemEditor.CurrentItemDefObject.outfit_texture = TextBoxOutfitTexture.Text
        End If

        If TextBoxSortingGroup.Text <> "" Then
            WindowSRRItemEditor.CurrentItemDefObject.sorting_group = TextBoxSortingGroup.Text
        End If

        If WindowSRRItemEditor.CurrentItemDefObject.uirep IsNot Nothing Then
            WindowSRRItemEditor.CurrentItemDefObject.uirep.name = TextBoxUIRepName.Text
            WindowSRRItemEditor.CurrentItemDefObject.uirep.description = TextBoxUIRepDescription.Text
            WindowSRRItemEditor.CurrentItemDefObject.uirep.icon = TextBoxUIRepIcon.Text
            WindowSRRItemEditor.CurrentItemDefObject.uirep.thumbnail = TextBoxUIRepThumbnail.Text
        Else
            If TextBoxUIRepName.Text <> "" OrElse TextBoxUIRepDescription.Text <> "" OrElse TextBoxUIRepIcon.Text <> "" OrElse TextBoxUIRepThumbnail.Text <> "" Then
                WindowSRRItemEditor.CurrentItemDefObject.uirep = New isogame.UIRep()
                WindowSRRItemEditor.CurrentItemDefObject.uirep.name = TextBoxUIRepName.Text
                WindowSRRItemEditor.CurrentItemDefObject.uirep.description = TextBoxUIRepDescription.Text
                WindowSRRItemEditor.CurrentItemDefObject.uirep.icon = TextBoxUIRepIcon.Text
                WindowSRRItemEditor.CurrentItemDefObject.uirep.thumbnail = TextBoxUIRepThumbnail.Text
            End If
        End If

        'Dropdown list choices
        If comboBoxAnimType.SelectedIndex <> -1 Then
            WindowSRRItemEditor.CurrentItemDefObject.anim_type = DirectCast([Enum].Parse(GetType(AnimType), comboBoxAnimType.SelectedItem.ToString()), AnimType)
        End If
        If comboBoxCoreAttribute.SelectedIndex <> -1 Then
            WindowSRRItemEditor.CurrentItemDefObject.coreAttribute = DirectCast([Enum].Parse(GetType(isogame.Attribute), comboBoxCoreAttribute.SelectedItem.ToString()), isogame.Attribute)
        End If
        If comboBoxCoreSkill.SelectedIndex <> -1 Then
            WindowSRRItemEditor.CurrentItemDefObject.coreSkill = DirectCast([Enum].Parse(GetType(isogame.Skill), comboBoxCoreSkill.SelectedItem.ToString()), isogame.Skill)
        End If
        If comboBoxCoreSpecialization.SelectedIndex <> -1 Then
            WindowSRRItemEditor.CurrentItemDefObject.coreSpecialization = DirectCast([Enum].Parse(GetType(isogame.Specialization), comboBoxCoreSpecialization.SelectedItem.ToString()), isogame.Specialization)
        End If
        If comboBoxItemType.SelectedIndex <> -1 Then
            WindowSRRItemEditor.CurrentItemDefObject.type = DirectCast([Enum].Parse(GetType(isogame.ItemType), comboBoxItemType.SelectedItem.ToString()), isogame.ItemType)
        End If
        If ComboBoxCyberwareType.SelectedIndex <> -1 Then
            WindowSRRItemEditor.CurrentItemDefObject.cyberware_type = DirectCast([Enum].Parse(GetType(isogame.CyberwareType), ComboBoxCyberwareType.SelectedItem.ToString()), isogame.CyberwareType)
        End If
        If ComboBoxIntendedUser.SelectedIndex <> -1 Then
            WindowSRRItemEditor.CurrentItemDefObject.intended_user = DirectCast([Enum].Parse(GetType(isogame.IntendedUser), ComboBoxIntendedUser.SelectedIndex.ToString()), isogame.IntendedUser)
        End If

        'Boolean choices
        If WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects IsNot Nothing Then
            WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.is_buff = CBool(ComboBoxASEIsBuff.SelectedItem.ToString().Contains("True"))
            WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.is_debuff = CBool(ComboBoxASEIsDebuff.SelectedItem.ToString().Contains("True"))
            WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.is_totem = CBool(ComboBoxASEIsTotem.SelectedItem.ToString().Contains("True"))
        Else
            If ComboBoxASEIsBuff.SelectedIndex <> -1 OrElse ComboBoxASEIsBuff.SelectedIndex <> -1 OrElse ComboBoxASEIsBuff.SelectedIndex <> -1 Then
                WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects = New isogame.StatusEffects()
                WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.is_buff = CBool(ComboBoxASEIsBuff.SelectedItem.ToString().Contains("True"))
                WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.is_debuff = CBool(ComboBoxASEIsDebuff.SelectedItem.ToString().Contains("True"))
                WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.is_totem = CBool(ComboBoxASEIsTotem.SelectedItem.ToString().Contains("True"))
            End If
        End If

        WindowSRRItemEditor.CurrentItemDefObject.affectsDecker = CBool(ComboBoxAffectsDecker.SelectedItem.ToString().Contains("True"))
        WindowSRRItemEditor.CurrentItemDefObject.affectsEnemy = CBool(ComboBoxAffectsEnemy.SelectedItem.ToString().Contains("True"))
        WindowSRRItemEditor.CurrentItemDefObject.affectsFriendly = CBool(ComboBoxAffectsFriendly.SelectedItem.ToString().Contains("True"))
        WindowSRRItemEditor.CurrentItemDefObject.canTargetActor = CBool(ComboBoxCanTargetActor.SelectedItem.ToString().Contains("True"))
        WindowSRRItemEditor.CurrentItemDefObject.canTargetOccupiedGridPoint = CBool(ComboBoxCanTargetOccupiedGridPoint.SelectedItem.ToString().Contains("True"))
        WindowSRRItemEditor.CurrentItemDefObject.canTargetUnoccupiedGridPoint = CBool(ComboBoxCanTargetUnoccupiedGridPoint.SelectedItem.ToString().Contains("True"))
        WindowSRRItemEditor.CurrentItemDefObject.canTargetSelf = CBool(ComboBoxCanTargetSelf.SelectedItem.ToString().Contains("True"))

        If WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects IsNot Nothing Then
            WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.is_buff = CBool(ComboBoxESEIsBuff.SelectedItem.ToString().Contains("True"))
            WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.is_debuff = CBool(ComboBoxESEIsDebuff.SelectedItem.ToString().Contains("True"))
            WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.is_totem = CBool(ComboBoxESEIsTotem.SelectedItem.ToString().Contains("True"))
        Else
            If ComboBoxESEIsBuff.SelectedIndex <> -1 OrElse ComboBoxESEIsBuff.SelectedIndex <> -1 OrElse ComboBoxESEIsBuff.SelectedIndex <> -1 Then
                WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects = New isogame.StatusEffects()
                WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.is_buff = CBool(ComboBoxESEIsBuff.SelectedItem.ToString().Contains("True"))
                WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.is_debuff = CBool(ComboBoxESEIsDebuff.SelectedItem.ToString().Contains("True"))
                WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.is_totem = CBool(ComboBoxESEIsTotem.SelectedItem.ToString().Contains("True"))
            End If
        End If

        WindowSRRItemEditor.CurrentItemDefObject.gear_covers_arms = CBool(ComboBoxGearCoversArms.SelectedItem.ToString().Contains("True"))
        WindowSRRItemEditor.CurrentItemDefObject.gear_covers_face = CBool(ComboBoxGearCoversFace.SelectedItem.ToString().Contains("True"))
        WindowSRRItemEditor.CurrentItemDefObject.gear_covers_hair = CBool(ComboBoxGearCoversHair.SelectedItem.ToString().Contains("True"))
        WindowSRRItemEditor.CurrentItemDefObject.isBuff = CBool(ComboBoxIsBuff.SelectedItem.ToString().Contains("True"))
        WindowSRRItemEditor.CurrentItemDefObject.isDebuff = CBool(ComboBoxIsDebuff.SelectedItem.ToString().Contains("True"))
        WindowSRRItemEditor.CurrentItemDefObject.isMagic = CBool(ComboBoxIsMagic.SelectedItem.ToString().Contains("True"))
        WindowSRRItemEditor.CurrentItemDefObject.effectOnTile = CBool(ComboBoxEffectOnTile.SelectedItem.ToString().Contains("True"))

        'lists

        WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.statusConditions.Clear()
        For Each StatCon As String In listBoxASEStatusConditions.Items
            WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.statusConditions.Add(DirectCast([Enum].Parse(GetType(isogame.StatusCondition), StatCon.ToString()), isogame.StatusCondition))
        Next

        WindowSRRItemEditor.CurrentItemDefObject.abilityModes.Clear()
        For Each AbilityMode As String In listBoxAbilityModes.Items
            WindowSRRItemEditor.CurrentItemDefObject.abilityModes.Add(AbilityMode)
        Next

        WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.statusConditions.Clear()
        For Each StatCon As String In listBoxESEStatusConditions.Items
            WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.statusConditions.Add(DirectCast([Enum].Parse(GetType(isogame.StatusCondition), StatCon.ToString()), isogame.StatusCondition))
        Next

        WindowSRRItemEditor.CurrentItemDefObject.effectModTable.Clear()
        For Each EffectMod As String In listBoxEffectModTable.Items
            WindowSRRItemEditor.CurrentItemDefObject.effectModTable.Add(CSng(EffectMod))
        Next

        WindowSRRItemEditor.CurrentItemDefObject.equipment_sheet_id.Clear()
        For Each EquipSheetID As String In listBoxEquipmentSheetId.Items
            WindowSRRItemEditor.CurrentItemDefObject.equipment_sheet_id.Add(EquipSheetID)
        Next

        WindowSRRItemEditor.CurrentItemDefObject.modelessAbilities.Clear()
        For Each ModelessAbility As String In listBoxModelessAbilities.Items
            WindowSRRItemEditor.CurrentItemDefObject.modelessAbilities.Add(ModelessAbility)
        Next

        WindowSRRItemEditor.CurrentItemDefObject.prereqStrings.Clear()
        For Each PrereqString As String In listBoxPrereqStrings.Items
            WindowSRRItemEditor.CurrentItemDefObject.prereqStrings.Add(PrereqString)
        Next

        WindowSRRItemEditor.CurrentItemDefObject.rangeModTable.Clear()

        For Each Row As DataRow In RangeModTable.Rows
            WindowSRRItemEditor.CurrentItemDefObject.rangeModTable.Add(CSng(Row("Modification")))
        Next

        'complicated lists
        If WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects IsNot Nothing Then
            WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.statMods.Clear()
            For Each StatmodItem As StatMod In ListViewASEStatMods.Items
                WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.statMods.Add(StatmodItem)
            Next
        ElseIf ListViewASEStatMods.Items.Count > 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects = New isogame.StatusEffects()
            For Each StatmodItem As StatMod In ListViewASEStatMods.Items
                WindowSRRItemEditor.CurrentItemDefObject.activationStatusEffects.statMods.Add(StatmodItem)
            Next
        End If

        If WindowSRRItemEditor.CurrentItemDefObject.drainBucket IsNot Nothing Then
            For Each FailureEntry As isogame.ResultBucket.Entry In ListViewDrainBucketFailureEntries.Items
                WindowSRRItemEditor.CurrentItemDefObject.drainBucket.failureEntries.Add(FailureEntry)
            Next
            For Each SuccessEntry As isogame.ResultBucket.Entry In ListViewDrainBucketSuccesEntries.Items
                WindowSRRItemEditor.CurrentItemDefObject.drainBucket.successEntries.Add(SuccessEntry)
            Next
        ElseIf ListViewDrainBucketFailureEntries.Items.Count > 0 OrElse ListViewDrainBucketSuccesEntries.Items.Count > 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.drainBucket = New isogame.ResultBucket()
            For Each FailureEntry As isogame.ResultBucket.Entry In ListViewDrainBucketFailureEntries.Items
                WindowSRRItemEditor.CurrentItemDefObject.drainBucket.failureEntries.Add(FailureEntry)
            Next
            For Each SuccessEntry As isogame.ResultBucket.Entry In ListViewDrainBucketSuccesEntries.Items
                WindowSRRItemEditor.CurrentItemDefObject.drainBucket.successEntries.Add(SuccessEntry)
            Next
        End If


        If WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects IsNot Nothing Then
            WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.statMods.Clear()
            For Each StatmodItem As StatMod In ListViewESEStatMods.Items
                WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.statMods.Add(StatmodItem)
            Next
        ElseIf ListViewESEStatMods.Items.Count > 0 Then
            WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects = New isogame.StatusEffects()
            For Each StatmodItem As StatMod In ListViewESEStatMods.Items
                WindowSRRItemEditor.CurrentItemDefObject.equippedStatusEffects.statMods.Add(StatmodItem)
            Next
        End If

    End Sub

    Private Sub buttonAddPreRequisiteStrings_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddPreRequisiteStrings.Click

        Dim PreReqBuilder As New Prerquesite_String_Builder
        PreReqBuilder.ShowDialog()
        If PreReqBuilder.Cancelled Then
            Return
        End If
        listBoxPrereqStrings.Items.Add(PreReqBuilder.Choice)

    End Sub

    Private Sub buttonRemovePreRequisiteStrings_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemovePreRequisiteStrings.Click
        listBoxPrereqStrings.Items.Remove(listBoxPrereqStrings.SelectedItem)
    End Sub

    Private Sub buttonAddEffectModifiers_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddEffectModifiers.Click

        Dim LSSF As New ListBoxSingleSelectionFiller
        LSSF.label.Content = "Enter a new Effect Modifier for Area Of Effects(AOE)"

        LSSF.ShowDialog()
        If LSSF.Canceled Then
            Return
        End If
        listBoxEffectModTable.Items.Add(LSSF.Choice)
    End Sub
    Private Sub buttonRemoveEffectModifiers_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveEffectModifiers.Click
        listBoxEffectModTable.Items.Remove(listBoxEffectModTable.SelectedItem)

    End Sub

    Private Sub buttonAddAbilityModes_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddAbilityModes.Click

        Dim LSF As New ListBoxSelectionFiller
        LSF.ComboBoxListBoxFilling.DataContext = WindowSRRItemEditor.ModesNamesList

        LSF.label.Content = "Enter a new ability mode"

        LSF.ShowDialog()
        If LSF.Canceled Then
            Return
        End If
        listBoxAbilityModes.Items.Add(LSF.Choice)

    End Sub

    Private Sub buttonRemoveAbilityModes_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveAbilityModes.Click
        listBoxAbilityModes.Items.Remove(listBoxAbilityModes.SelectedItem)

    End Sub

    Private Sub buttonAddModelessAbilities_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddModelessAbilities.Click

        Dim LSF As New ListBoxSelectionFiller
        If comboBoxItemType.SelectedItem IsNot Nothing AndAlso DirectCast([Enum].Parse(GetType(isogame.ItemType), comboBoxItemType.SelectedItem.ToString()), isogame.ItemType) = ItemType.ItemType_AttackSpell Then

            Dim source As ICollectionView = CollectionViewSource.GetDefaultView(WindowSRRItemEditor.AbilityList)
            source.Filter = Function(p) CType(p, AbilityDef).toHitFunction <> "locationSpellToHit" AndAlso CType(p, AbilityDef).toHitFunction <> "locationWeaponToHit"

            LSF.ComboBoxListBoxFilling.DataContext = source
        Else
            LSF.ComboBoxListBoxFilling.DataContext = WindowSRRItemEditor.AbilityList
        End If


        LSF.ComboBoxListBoxFilling.DisplayMemberPath = "id"
        LSF.ComboBoxListBoxFilling.SelectedValuePath = "id"

        LSF.label.Content = "Enter a new modeless ability"

        LSF.ShowDialog()
        If LSF.Canceled Then
            Return
        End If
        If LSF.Choice IsNot Nothing Then
            listBoxModelessAbilities.Items.Add(LSF.Choice)
        End If

    End Sub

    Private Sub buttonRemoveModelessAbilities_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveModelessAbilities.Click
        listBoxModelessAbilities.Items.Remove(listBoxModelessAbilities.SelectedItem)

    End Sub

    Private Sub buttonAddFailureEntries_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddFailureEntries.Click
        Dim RBE As ResultBucketEditor
        If WindowSRRItemEditor.CurrentItemDefObject.drainBucket IsNot Nothing Then
            RBE = New ResultBucketEditor(WindowSRRItemEditor.CurrentItemDefObject.drainBucket, "failure")
        Else
            RBE = New ResultBucketEditor(New ResultBucket, "failure")
        End If
        RBE.label.Content = "Edit the failure Entries"
        RBE.ShowDialog()
        If RBE.Cancelled = False Then
            ListViewDrainBucketFailureEntries.Items.Clear()
            ' ListViewDrainBucketSuccesEntries.Items.Clear()
            'If WindowSRRItemEditor.CurrentItemDefObject.drainBucket IsNot Nothing Then
            For Each FailureEntry As isogame.ResultBucket.Entry In RBE.Choice.failureEntries
                ListViewDrainBucketFailureEntries.Items.Add(FailureEntry)
            Next
            'For Each SuccessEntry As isogame.ResultBucket.Entry In WindowSRRItemEditor.CurrentItemDefObject.drainBucket.successEntries
            'ListViewDrainBucketSuccesEntries.Items.Add(SuccessEntry)
            ' Next
            ' End If
        End If
        'Dim LSF As New ResultBucketSelectionFiller

        'LSF.label.Content = "Enter a new failure Entry"

        'LSF.ShowDialog()
        'If LSF.Canceled Then
        '    Return
        'End If
        'ListViewDrainBucketFailureEntries.Items.Add(LSF.Choice)

    End Sub

    Private Sub buttonRemoveFailureEntries_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveFailureEntries.Click
        ListViewDrainBucketFailureEntries.Items.Remove(ListViewDrainBucketFailureEntries.SelectedItem)
    End Sub

    Private Sub buttonAddSuccessEntries_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddSuccessEntries.Click

        Dim RBE As ResultBucketEditor
        If WindowSRRItemEditor.CurrentItemDefObject.drainBucket IsNot Nothing Then
            RBE = New ResultBucketEditor(WindowSRRItemEditor.CurrentItemDefObject.drainBucket, "success")
        Else
            RBE = New ResultBucketEditor(New ResultBucket, "success")
        End If
        RBE.label.Content = "Edit the success Entries"
        RBE.ShowDialog()
        If RBE.Cancelled = False Then
            ListViewDrainBucketSuccesEntries.Items.Clear()
            For Each SuccessEntry As isogame.ResultBucket.Entry In RBE.Choice.successEntries
                ListViewDrainBucketSuccesEntries.Items.Add(SuccessEntry)
            Next
            'For Each SuccessEntry As isogame.ResultBucket.Entry In WindowSRRItemEditor.CurrentItemDefObject.drainBucket.successEntries
            'ListViewDrainBucketSuccesEntries.Items.Add(SuccessEntry)
            ' Next
            ' End If
        End If

    End Sub

    Private Sub buttonRemoveSuccessEntries_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveSuccessEntries.Click
        ListViewDrainBucketSuccesEntries.Items.Remove(ListViewDrainBucketSuccesEntries.SelectedItem)

    End Sub

    Private Sub buttonAddEquipmentSheetIds_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddEquipmentSheetIds.Click

        Dim LSF As New ListBoxSelectionFiller
        LSF.ComboBoxListBoxFilling.DataContext = WindowSRRItemEditor.EquipmentSheetIdList
        LSF.ComboBoxListBoxFilling.IsEditable = True
        LSF.label.Content = "Enter a new equipment sheet id"

        LSF.ShowDialog()
        If LSF.Canceled Then
            Return
        End If
        listBoxAbilityModes.Items.Add(LSF.Choice)

    End Sub

    Private Sub buttonRemoveEquipmentSheetIds_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveEquipmentSheetIds.Click
        listBoxEquipmentSheetId.Items.Remove(listBoxEquipmentSheetId.SelectedItem)

    End Sub

    Private Sub buttonAddASEStatMods_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddASEStatMods.Click

        Dim LSF As New StatModSelectionFiller

        LSF.label.Content = "Enter a new activationStatusEffects status modification"

        LSF.ShowDialog()
        If LSF.Canceled Then
            Return
        End If
        ListViewASEStatMods.Items.Add(LSF.Choice)

    End Sub

    Private Sub buttonRemoveASEStatMods_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveASEStatMods.Click
        ListViewASEStatMods.Items.Remove(ListViewASEStatMods.SelectedItem)
    End Sub

    Private Sub buttonAddASEStatusConditions_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddASEStatusConditions.Click

        Dim LSF As New ListBoxSelectionFiller
        LSF.ComboBoxListBoxFilling.DataContext = New ObservableCollection(Of String)(New List(Of String)(System.Enum.GetNames(GetType(isogame.StatusCondition))))

        LSF.label.Content = "Enter a new StatusCondition"

        LSF.ShowDialog()
        If LSF.Canceled Then
            Return
        End If
        listBoxASEStatusConditions.Items.Add(LSF.Choice)

    End Sub

    Private Sub buttonRemoveASEStatusConditions_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveASEStatusConditions.Click
        listBoxASEStatusConditions.Items.Remove(listBoxASEStatusConditions.SelectedItem)

    End Sub
    Private Sub buttonAddESEStatMods_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddESEStatMods.Click


        Dim LSF As New StatModSelectionFiller

        LSF.label.Content = "Enter a new equippedStatusEffects status modification"

        LSF.ShowDialog()
        If LSF.Canceled Then
            Return
        End If
        ListViewESEStatMods.Items.Add(LSF.Choice)

    End Sub

    Private Sub buttonRemoveESEStatMods_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveESEStatMods.Click
        ListViewESEStatMods.Items.Remove(ListViewESEStatMods.SelectedItem)
    End Sub
    Private Sub buttonAddESEStatusConditions_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddESEStatusConditions.Click

        Dim LSF As New ListBoxSelectionFiller
        LSF.ComboBoxListBoxFilling.DataContext = New ObservableCollection(Of String)(New List(Of String)(System.Enum.GetNames(GetType(isogame.StatusCondition))))

        LSF.label.Content = "Enter a new StatusCondition"

        LSF.ShowDialog()
        If LSF.Canceled Then
            Return
        End If
        listBoxESEStatusConditions.Items.Add(LSF.Choice)

    End Sub
    Private Sub buttonRemoveESEStatusConditions_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveESEStatusConditions.Click
        listBoxESEStatusConditions.Items.Remove(listBoxESEStatusConditions.SelectedItem)

    End Sub
    Private Sub RangeModTable_RowChanged(sender As Object, e As DataRowChangeEventArgs) Handles RangeModTable.RowChanged
        If Loading Then
            Return
        End If
    End Sub

    Private Sub RangeModTable_RowDeleted(sender As Object, e As DataRowChangeEventArgs) Handles RangeModTable.RowDeleted
        If Loading Then
            Return
        End If

        UpdateRanges()

        textBlockMaxRange.Text = CStr(RangeModTable.Rows.Count)
    End Sub

    Private Sub UpdateRanges()

        RangeModTable.Columns(0).ReadOnly = False
        RangeModTable.Columns(0).Unique = False
        For i As Integer = 0 To RangeModTable.Rows.Count - 1
            RangeModTable.Rows(i)(0) = i
        Next

        RangeModTable.Columns(0).ReadOnly = True
        RangeModTable.Columns(0).Unique = True
    End Sub

    Private Sub RangeModTable_TableNewRow(sender As Object, e As DataTableNewRowEventArgs) Handles RangeModTable.TableNewRow
        If Loading Then
            Return
        End If
        textBlockMaxRange.Text = CStr(RangeModTable.Rows.Count + 1)
    End Sub

    Private Sub buttonAddRangeModifiers_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddRangeModifiers.Click
        Try
            Dim Inputval As Single = CSng(InputBox("Enter a new range modifier"))

            Dim row As DataRow = RangeModTable.NewRow()
            row("Modification") = Inputval
            If DataGridRangeModTable.SelectedIndex <> -1 Then
                RangeModTable.Rows.InsertAt(row, DataGridRangeModTable.SelectedIndex)
                UpdateRanges()
            Else
                RangeModTable.Rows.Add(row)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub buttonRemoveRangeModifiers_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveRangeModifiers.Click

        RangeModTable.Rows.RemoveAt(DataGridRangeModTable.SelectedIndex)
    End Sub
    Private Sub TextBoxLostKeyboardFocus(sender As Object, e As KeyboardFocusChangedEventArgs) Handles TextBoxUIRepIcon.LostKeyboardFocus, TextBoxEquipPrefabName.LostKeyboardFocus, TextBoxCooldownCategory.LostKeyboardFocus, TextBoxPreActionFxName.LostKeyboardFocus, TextBoxActionFxName.LostKeyboardFocus, TextBoxPostActionFxName.LostKeyboardFocus, TextBoxHitReactionFxName.LostKeyboardFocus, TextBoxMissReactionFxName.LostKeyboardFocus, TextBoxCharacterPrefabId.LostKeyboardFocus, TextBoxASEUIRepIcon.LostKeyboardFocus, TextBoxASEStackingCategory.LostKeyboardFocus, TextBoxASEFxScript.LostKeyboardFocus, TextBoxASEDurationFxScript.LostKeyboardFocus, TextBoxESEUIRepIcon.LostKeyboardFocus, TextBoxESEStackingCategory.LostKeyboardFocus, TextBoxESEFxScript.LostKeyboardFocus, TextBoxESEDurationFxScript.LostKeyboardFocus, TextBoxGearBundle.LostKeyboardFocus, TextBoxOutfitTexture.LostKeyboardFocus, TextBoxGearPrefab.LostKeyboardFocus, TextBoxSortingGroup.LostKeyboardFocus, TextBoxCharacterSheetId.LostKeyboardFocus, TextBoxCharacterUIName.LostKeyboardFocus


        If CType(sender, ComboBox).Text <> "" Then

            If CType(CType(sender, ComboBox).DataContext, ObservableCollection(Of String)).FirstOrDefault(Function(S As String) S = CType(sender, ComboBox).Text) Is Nothing Then
                CType(CType(sender, ComboBox).DataContext, ObservableCollection(Of String)).Add(CType(sender, ComboBox).Text)
            End If
            CType(sender, ComboBox).SelectedIndex = CType(CType(sender, ComboBox).DataContext, ObservableCollection(Of String)).IndexOf(CType(sender, ComboBox).Text)
        End If

    End Sub
    Private Sub TextBoxSelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles TextBoxUIRepIcon.SelectionChanged, TextBoxEquipPrefabName.SelectionChanged, TextBoxCooldownCategory.SelectionChanged, TextBoxPreActionFxName.SelectionChanged, TextBoxActionFxName.SelectionChanged, TextBoxPostActionFxName.SelectionChanged, TextBoxHitReactionFxName.SelectionChanged, TextBoxMissReactionFxName.SelectionChanged, TextBoxCharacterPrefabId.SelectionChanged, TextBoxASEUIRepIcon.SelectionChanged, TextBoxASEStackingCategory.SelectionChanged, TextBoxASEFxScript.SelectionChanged, TextBoxASEDurationFxScript.SelectionChanged, TextBoxESEUIRepIcon.SelectionChanged, TextBoxESEStackingCategory.SelectionChanged, TextBoxESEFxScript.SelectionChanged, TextBoxESEDurationFxScript.SelectionChanged, TextBoxGearBundle.SelectionChanged, TextBoxOutfitTexture.SelectionChanged, TextBoxGearPrefab.SelectionChanged, TextBoxSortingGroup.SelectionChanged, TextBoxCharacterSheetId.SelectionChanged, TextBoxCharacterUIName.SelectionChanged

        If CType(sender, ComboBox).SelectedItem Is Nothing Then
            Return
        End If

        If CType(sender, ComboBox).Text <> CType(sender, ComboBox).SelectedItem.ToString() Then
            CType(sender, ComboBox).Text = CType(sender, ComboBox).SelectedItem.ToString()
        End If

    End Sub


    Private Sub ItemTabContent_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ''Fill enums in combobox items
        For Each AnimType As String In [Enum].GetNames(GetType(isogame.AnimType))
            comboBoxAnimType.Items.Add(AnimType.ToString())
        Next
        For Each Attribute As String In [Enum].GetNames(GetType(isogame.Attribute))
            comboBoxCoreAttribute.Items.Add(Attribute.ToString())
        Next
        For Each Skill As String In [Enum].GetNames(GetType(isogame.Skill))
            comboBoxCoreSkill.Items.Add(Skill.ToString())
        Next
        For Each Specialization As String In [Enum].GetNames(GetType(isogame.Specialization))
            comboBoxCoreSpecialization.Items.Add(Specialization.ToString())
        Next
        For Each ItemType As String In [Enum].GetNames(GetType(isogame.ItemType))
            comboBoxItemType.Items.Add(ItemType.ToString())
        Next
        For Each CyberwareType As String In [Enum].GetNames(GetType(isogame.CyberwareType))
            ComboBoxCyberwareType.Items.Add(CyberwareType.ToString())
        Next
        For Each IntendedUser As String In [Enum].GetNames(GetType(isogame.IntendedUser))
            ComboBoxIntendedUser.Items.Add(IntendedUser.ToString())
        Next

        TextBoxUIRepIcon.DataContext = WindowSRRItemEditor.IconList
        TextBoxEquipPrefabName.DataContext = WindowSRRItemEditor.EquipmentPrefabList
        TextBoxCooldownCategory.DataContext = WindowSRRItemEditor.CooldownCategoryList
        TextBoxPreActionFxName.DataContext = WindowSRRItemEditor.FxNamesList
        TextBoxActionFxName.DataContext = WindowSRRItemEditor.FxNamesList
        TextBoxPostActionFxName.DataContext = WindowSRRItemEditor.FxNamesList
        TextBoxHitReactionFxName.DataContext = WindowSRRItemEditor.FxNamesList
        TextBoxMissReactionFxName.DataContext = WindowSRRItemEditor.FxNamesList
        TextBoxCharacterPrefabId.DataContext = WindowSRRItemEditor.CharacterPrefabIdList
        TextBoxASEUIRepIcon.DataContext = WindowSRRItemEditor.IconList
        TextBoxASEStackingCategory.DataContext = WindowSRRItemEditor.StackingCategoriesList
        TextBoxASEFxScript.DataContext = WindowSRRItemEditor.FXScriptList
        TextBoxASEDurationFxScript.DataContext = WindowSRRItemEditor.DurationFxScriptList
        TextBoxESEUIRepIcon.DataContext = WindowSRRItemEditor.IconList
        TextBoxESEStackingCategory.DataContext = WindowSRRItemEditor.StackingCategoriesList
        TextBoxESEFxScript.DataContext = WindowSRRItemEditor.FXScriptList
        TextBoxESEDurationFxScript.DataContext = WindowSRRItemEditor.DurationFxScriptList
        TextBoxGearBundle.DataContext = WindowSRRItemEditor.GearBundleList
        TextBoxOutfitTexture.DataContext = WindowSRRItemEditor.OutfitTextureList
        TextBoxGearPrefab.DataContext = WindowSRRItemEditor.GearPrefabList
        TextBoxSortingGroup.DataContext = WindowSRRItemEditor.SortingGroupList
        TextBoxCharacterSheetId.DataContext = WindowSRRItemEditor.CharacterSheetIdList
        TextBoxCharacterUIName.DataContext = WindowSRRItemEditor.CharacterUINameList
        TextBoxDeckingDefaultWeapon.DataContext = WindowSRRItemEditor.ItemList
        TextBoxDeckingDefaultWeapon.DisplayMemberPath = "id"
        TextBoxDeckingDefaultWeapon.SelectedValuePath = "id"


    End Sub

    Private Sub listBoxModelessAbilities_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles listBoxModelessAbilities.MouseDoubleClick
        If listBoxModelessAbilities.SelectedItem Is Nothing Then
            Return
        End If
        CurrentAbilityDefObject = AbilityList.FirstOrDefault(Function(C As isogame.AbilityDef) C.id = listBoxModelessAbilities.SelectedValue.ToString())
        Dim tabControl As TabControl = CType(Me.Parent, TabControl)
        tabControl.SelectedIndex = tabControl.Items.IndexOf(tabControl.FindName("AbilitiesTab"))

    End Sub


    Private Sub listBoxAbilityModes_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles listBoxAbilityModes.MouseDoubleClick
        If listBoxAbilityModes.SelectedItem Is Nothing Then
            Return
        End If
        Dim str As String = listBoxAbilityModes.SelectedValue.ToString().ToLower()
        Dim in_ As Integer = ModesNamesList.IndexOf(str)
        CurrentModeIdString = ModesNamesList.FirstOrDefault(Function(C As String) C = str)
        If CurrentModeIdString IsNot Nothing Then
            CurrentModeDefObject = ModesList.Item(ModesNamesList.IndexOf(CurrentModeIdString))
            Dim tabControl As TabControl = CType(Me.Parent, TabControl)
            tabControl.SelectedIndex = tabControl.Items.IndexOf(tabControl.FindName("ModesTab"))
        End If

    End Sub

    Private Sub comboBoxItemType_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboBoxItemType.SelectionChanged
        If DirectCast([Enum].Parse(GetType(isogame.ItemType), comboBoxItemType.SelectedItem.ToString()), isogame.ItemType) = ItemType.ItemType_AttackSpell Then
            ComboBoxCanTargetOccupiedGridPoint.IsEnabled = False
            ComboBoxCanTargetUnoccupiedGridPoint.IsEnabled = False
        Else
            ComboBoxCanTargetOccupiedGridPoint.IsEnabled = True
            ComboBoxCanTargetUnoccupiedGridPoint.IsEnabled = True
        End If
    End Sub
End Class
