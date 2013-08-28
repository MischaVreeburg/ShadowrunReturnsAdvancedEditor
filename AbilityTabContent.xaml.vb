Imports isogame
Imports System.Collections.ObjectModel


Public Class AbilityTabContent
    Private _CurrAbilityDefObj As AbilityDef
    Private _BackupAbilityDefObj As AbilityDef = Nothing
    Public WithEvents dataProvider As ObjectDataProvider = TryCast(Me.Resources("dataProvider"), ObjectDataProvider)
    Private ManualEdit As Boolean


    Public Sub FillTab()

    End Sub

    Public Property CurrentAbilityDefObj As AbilityDef
        Get
            Return _CurrAbilityDefObj
        End Get
        Set(value As AbilityDef)
            _CurrAbilityDefObj = value
        End Set
    End Property

    Public Property BackupAbilityDefObj As AbilityDef
        Get
            Return _BackupAbilityDefObj
        End Get
        Set(value As AbilityDef)
            _BackupAbilityDefObj = value
        End Set
    End Property

    Public Sub New(ByRef AbilityDefObj As AbilityDef)

        ' This call is required by the designer.
        InitializeComponent()
        CurrentAbilityDefObj = AbilityDefObj
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub TextBoxId_LostKeyboardFocus(sender As Object, e As KeyboardFocusChangedEventArgs) Handles TextBoxId.LostKeyboardFocus
        ManualEdit = True
    End Sub

    Private Sub TextBoxId_TextChanged(sender As Object, e As TextChangedEventArgs) Handles TextBoxId.TextChanged
        If ManualEdit Then
            BackupAbilityDefObj = CurrentAbilityDefObj
            ManualEdit = False
        End If
    End Sub

    Private Sub AbilityTabContent_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ComboboxAnimAction.DataContext = New ObservableCollection(Of String)(New List(Of String)(System.Enum.GetNames(GetType(isogame.AnimAction))))
        comboBoxAnimModifier.DataContext=New ObservableCollection(Of String)(New List(Of String)(System.Enum.GetNames(GetType(isogame.AnimModifier))))
        comboBoxDamageType.DataContext = New ObservableCollection(Of String)(New List(Of String)(System.Enum.GetNames(GetType(isogame.VulnerabilityType))))
        ComboBoxIntendedUser.DataContext = New ObservableCollection(Of String)(New List(Of String)(System.Enum.GetNames(GetType(isogame.IntendedUser))))

        TextBoxUIRepIcon.DataContext = WindowSRRItemEditor.IconList
        TextBoxCooldownCategory.DataContext = WindowSRRItemEditor.CooldownCategoryList
        TextBoxPreActionFxName.DataContext = WindowSRRItemEditor.FxNamesList
        TextBoxActionFxName.DataContext = WindowSRRItemEditor.FxNamesList
        TextBoxPostActionFxName.DataContext = WindowSRRItemEditor.FxNamesList
        TextBoxHitReactionFxName.DataContext = WindowSRRItemEditor.FxNamesList
        TextBoxMissReactionFxName.DataContext = WindowSRRItemEditor.FxNamesList
        TextBoxToHitFunction.DataContext = WindowSRRItemEditor.ToHistFunctionList
        TextBoxdamageFunction.DataContext = WindowSRRItemEditor.DamageFunctionList
        TextBoxASEUIRepIcon.DataContext = WindowSRRItemEditor.IconList
        TextBoxASEStackingCategory.DataContext = WindowSRRItemEditor.StackingCategoriesList
        TextBoxASEFxScript.DataContext = WindowSRRItemEditor.FXScriptList
        TextBoxASEDurationFxScript.DataContext = WindowSRRItemEditor.DurationFxScriptList

  
        dataProvider = TryCast(Me.Resources("dataProvider"), ObjectDataProvider)
        If Not (dataProvider Is Nothing) Then
            dataProvider.ObjectInstance = CurrentAbilityDefObj
        End If

    End Sub

    Private Sub buttonAddPreRequisiteStrings_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddPreRequisiteStrings.Click
        Dim PreReqBuilder As New Prerquesite_String_Builder
        PreReqBuilder.ShowDialog()
        If PreReqBuilder.Cancelled Then
            Return
        End If

        If CType(dataProvider.Data, AbilityDef).prereqStrings Is Nothing Then
            Return
        End If

        CType(dataProvider.Data, AbilityDef).prereqStrings.Add(PreReqBuilder.Choice)
        dataProvider.Refresh()
        dataProvider.ObjectInstance = New AbilityDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentAbilityDefObject
        dataProvider.Refresh()
        
    End Sub

    Private Sub buttonRemovePreRequisiteStrings_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemovePreRequisiteStrings.Click
        CType(dataProvider.Data, AbilityDef).prereqStrings.Remove(listBoxPrereqStrings.SelectedValue.ToString())
        dataProvider.Refresh()
        dataProvider.ObjectInstance = New AbilityDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentAbilityDefObject
        dataProvider.Refresh()

    End Sub
    Private Sub buttonAddFailureEntriesDamageBucket_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddFailureEntriesDamageBucket.Click

        If CType(dataProvider.Data, AbilityDef).damageBucket Is Nothing Then
            CType(dataProvider.Data, AbilityDef).damageBucket = New ResultBucket()
        End If


        Dim RBE As ResultBucketEditor
        If CType(dataProvider.Data, AbilityDef).damageBucket IsNot Nothing Then
            RBE = New ResultBucketEditor(CType(dataProvider.Data, AbilityDef).damageBucket, "failure")
        Else
            RBE = New ResultBucketEditor(New ResultBucket, "failure")
        End If
        RBE.label.Content = "Edit the failure Entries"
        RBE.ShowDialog()
        'If RBE.Cancelled = False Then
        '    CType(dataProvider.Data, AbilityDef).damageBucket.failureEntries.Clear()
        '    For Each failureEntry As isogame.ResultBucket.Entry In RBE.Choice.failureEntries
        '        CType(dataProvider.Data, AbilityDef).damageBucket.failureEntries.Add(failureEntry)
        '    Next
        'End If

        dataProvider.Refresh()
        dataProvider.ObjectInstance = New AbilityDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentAbilityDefObject
        dataProvider.Refresh()

    End Sub

    Private Sub buttonRemoveFailureEntriesDamageBucket_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveFailureEntriesDamageBucket.Click
        CType(dataProvider.Data, AbilityDef).damageBucket.failureEntries.Remove(CType(ListViewDamageBucketFailureEntries.SelectedItem, ResultBucket.Entry))
        dataProvider.Refresh()
        dataProvider.ObjectInstance = New AbilityDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentAbilityDefObject
        dataProvider.Refresh()
    End Sub

    Private Sub buttonAddSuccessEntriesDamageBucket_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddSuccessEntriesDamageBucket.Click

        If CType(dataProvider.Data, AbilityDef).damageBucket Is Nothing Then
            CType(dataProvider.Data, AbilityDef).damageBucket = New ResultBucket()
        End If


        Dim RBE As ResultBucketEditor
        If CType(dataProvider.Data, AbilityDef).damageBucket IsNot Nothing Then
            RBE = New ResultBucketEditor(CType(dataProvider.Data, AbilityDef).damageBucket, "success")
        Else
            RBE = New ResultBucketEditor(New ResultBucket, "success")
        End If
        RBE.label.Content = "Edit the success Entries"
        RBE.ShowDialog()
        'If RBE.Cancelled = False Then
        '    CType(dataProvider.Data, AbilityDef).damageBucket.successEntries.Clear()
        '    For Each successEntry As isogame.ResultBucket.Entry In RBE.Choice.successEntries
        '        CType(dataProvider.Data, AbilityDef).damageBucket.successEntries.Add(successEntry)
        '    Next
        'End If

        dataProvider.Refresh()
        dataProvider.ObjectInstance = New AbilityDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentAbilityDefObject
        dataProvider.Refresh()


    End Sub

    Private Sub buttonRemoveSuccessEntriesDamageBucket_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveSuccessEntriesDamageBucket.Click
        CType(dataProvider.Data, AbilityDef).damageBucket.successEntries.Remove(CType(ListViewDamageBucketSuccesEntries.SelectedItem, ResultBucket.Entry))
        dataProvider.Refresh()
        dataProvider.ObjectInstance = New AbilityDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentAbilityDefObject
        dataProvider.Refresh()

    End Sub

    Private Sub buttonAddFailureEntriesDrainBucket_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddFailureEntriesDrainBucket.Click


        If CType(dataProvider.Data, AbilityDef).drainBucket Is Nothing Then
            CType(dataProvider.Data, AbilityDef).drainBucket = New ResultBucket()
        End If


        Dim RBE As ResultBucketEditor
        If CType(dataProvider.Data, AbilityDef).drainBucket IsNot Nothing Then
            RBE = New ResultBucketEditor(CType(dataProvider.Data, AbilityDef).drainBucket, "failure")
        Else
            RBE = New ResultBucketEditor(New ResultBucket, "failure")
        End If
        RBE.label.Content = "Edit the failure Entries"
        RBE.ShowDialog()
        'If RBE.Cancelled = False Then
        '    CType(dataProvider.Data, AbilityDef).drainBucket.failureEntries.Clear()
        '    For Each failureEntry As isogame.ResultBucket.Entry In RBE.Choice.failureEntries
        '        CType(dataProvider.Data, AbilityDef).drainBucket.failureEntries.Add(failureEntry)
        '    Next
        'End If

        dataProvider.Refresh()
        dataProvider.ObjectInstance = New AbilityDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentAbilityDefObject
        dataProvider.Refresh()


    End Sub

    Private Sub buttonRemoveFailureEntriesDrainBucket_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveFailureEntriesDrainBucket.Click
        CType(dataProvider.Data, AbilityDef).drainBucket.failureEntries.Remove(CType(ListViewDrainBucketFailureEntries.SelectedItem, ResultBucket.Entry))
        dataProvider.Refresh()
        dataProvider.ObjectInstance = New AbilityDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentAbilityDefObject
        dataProvider.Refresh()

    End Sub

    Private Sub buttonAddSuccessEntriesDrainBucket_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddSuccessEntriesDrainBucket.Click

        If CType(dataProvider.Data, AbilityDef).drainBucket Is Nothing Then
            CType(dataProvider.Data, AbilityDef).drainBucket = New ResultBucket()
        End If


        Dim RBE As ResultBucketEditor
        If CType(dataProvider.Data, AbilityDef).drainBucket IsNot Nothing Then
            RBE = New ResultBucketEditor(CType(dataProvider.Data, AbilityDef).drainBucket, "success")
        Else
            RBE = New ResultBucketEditor(New ResultBucket, "success")
        End If
        RBE.label.Content = "Edit the success Entries"
        RBE.ShowDialog()
        'If RBE.Cancelled = False Then
        '    CType(dataProvider.Data, AbilityDef).drainBucket.successEntries.Clear()
        '    For Each successEntry As isogame.ResultBucket.Entry In RBE.Choice.successEntries
        '        CType(dataProvider.Data, AbilityDef).drainBucket.successEntries.Add(successEntry)
        '    Next
        'End If

        dataProvider.Refresh()
        dataProvider.ObjectInstance = New AbilityDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentAbilityDefObject
        dataProvider.Refresh()

    End Sub

    Private Sub buttonRemoveSuccessEntriesDrainBucket_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveSuccessEntriesDrainBucket.Click
        CType(dataProvider.Data, AbilityDef).drainBucket.successEntries.Remove(CType(ListViewDrainBucketSuccesEntriess.SelectedItem, ResultBucket.Entry))
        dataProvider.Refresh()
        dataProvider.ObjectInstance = New AbilityDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentAbilityDefObject
        dataProvider.Refresh()

    End Sub

    Private Sub buttonAddEffectModifiers_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddEffectModifiers.Click
        Dim LSSF As New ListBoxSingleSelectionFiller
        LSSF.label.Content = "Enter a new Effect Modifier for Area Of Effects(AOE)"

        LSSF.ShowDialog()
        If LSSF.Canceled Then
            Return
        End If
        If CType(dataProvider.Data, AbilityDef).effectModTable Is Nothing Then
            Return
        End If

        CType(dataProvider.Data, AbilityDef).effectModTable.Add(LSSF.Choice)
        dataProvider.Refresh()
        dataProvider.ObjectInstance = New AbilityDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentAbilityDefObject
        dataProvider.Refresh()
    End Sub

    Private Sub buttonRemoveEffectModifiers_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveEffectModifiers.Click
        CType(dataProvider.Data, AbilityDef).effectModTable.Remove(CSng(listBoxEffectModTable.SelectedItem))
        dataProvider.Refresh()
        dataProvider.ObjectInstance = New AbilityDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentAbilityDefObject
        dataProvider.Refresh()
    End Sub

    Private Sub buttonAddASEStatMods_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddASEStatMods.Click
        Dim LSF As New StatModSelectionFiller

        LSF.label.Content = "Enter a new activationStatusEffects status modification"

        LSF.ShowDialog()
        If LSF.Canceled Then
            Return
        End If

        If CType(dataProvider.Data, AbilityDef).activationStatusEffects Is Nothing Then
            CType(dataProvider.Data, AbilityDef).activationStatusEffects = New StatusEffects()
        End If

        CType(dataProvider.Data, AbilityDef).activationStatusEffects.statMods.Add(LSF.Choice)
        dataProvider.Refresh()
        dataProvider.ObjectInstance = New AbilityDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentAbilityDefObject
        dataProvider.Refresh()
    End Sub

    Private Sub buttonRemoveASEStatMods_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveASEStatMods.Click
        CType(dataProvider.Data, AbilityDef).activationStatusEffects.statMods.Remove(CType(ListViewASEStatMods.SelectedItem, StatMod))
        dataProvider.Refresh()
        dataProvider.ObjectInstance = New AbilityDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentAbilityDefObject
        dataProvider.Refresh()
    End Sub

    Private Sub buttonAddASEStatusConditions_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddASEStatusConditions.Click
        Dim LSF As New ListBoxSelectionFiller
        LSF.ComboBoxListBoxFilling.DataContext = New ObservableCollection(Of String)(New List(Of String)(System.Enum.GetNames(GetType(isogame.StatusCondition))))

        LSF.label.Content = "Enter a new StatusCondition"

        LSF.ShowDialog()
        If LSF.Canceled Then
            Return
        End If
        If CType(dataProvider.Data, AbilityDef).activationStatusEffects Is Nothing Then
            CType(dataProvider.Data, AbilityDef).activationStatusEffects = New StatusEffects()
        End If

        CType(dataProvider.Data, AbilityDef).activationStatusEffects.statusConditions.Add(DirectCast([Enum].Parse(GetType(isogame.StatusCondition), LSF.Choice.ToString()), isogame.StatusCondition))
        dataProvider.Refresh()
        dataProvider.ObjectInstance = New AbilityDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentAbilityDefObject
        dataProvider.Refresh()

    End Sub

    Private Sub buttonRemoveASEStatusConditions_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveASEStatusConditions.Click
        CType(dataProvider.Data, AbilityDef).activationStatusEffects.statusConditions.Remove(DirectCast([Enum].Parse(GetType(isogame.StatusCondition), listBoxASEStatusConditions.SelectedValue.ToString()), isogame.StatusCondition))
        dataProvider.Refresh()
        dataProvider.ObjectInstance = New AbilityDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentAbilityDefObject
        dataProvider.Refresh()
    End Sub



    Private Sub TextBoxLostKeyboardFocus(sender As Object, e As KeyboardFocusChangedEventArgs) Handles TextBoxUIRepIcon.LostKeyboardFocus, TextBoxCooldownCategory.LostKeyboardFocus, TextBoxPreActionFxName.LostKeyboardFocus, TextBoxActionFxName.LostKeyboardFocus, TextBoxPostActionFxName.LostKeyboardFocus, TextBoxHitReactionFxName.LostKeyboardFocus, TextBoxMissReactionFxName.LostKeyboardFocus, TextBoxASEUIRepIcon.LostKeyboardFocus, TextBoxASEStackingCategory.LostKeyboardFocus, TextBoxASEFxScript.LostKeyboardFocus, TextBoxASEDurationFxScript.LostKeyboardFocus


        If CType(sender, ComboBox).Text <> "" Then

            If CType(CType(sender, ComboBox).DataContext, ObservableCollection(Of String)).FirstOrDefault(Function(S As String) S = CType(sender, ComboBox).Text) Is Nothing Then
                CType(CType(sender, ComboBox).DataContext, ObservableCollection(Of String)).Add(CType(sender, ComboBox).Text)
            End If
            CType(sender, ComboBox).SelectedIndex = CType(CType(sender, ComboBox).DataContext, ObservableCollection(Of String)).IndexOf(CType(sender, ComboBox).Text)
        End If

    End Sub

    Private Sub TextBoxSelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles TextBoxUIRepIcon.SelectionChanged, TextBoxCooldownCategory.SelectionChanged, TextBoxPreActionFxName.SelectionChanged, TextBoxActionFxName.SelectionChanged, TextBoxPostActionFxName.SelectionChanged, TextBoxHitReactionFxName.SelectionChanged, TextBoxMissReactionFxName.SelectionChanged, TextBoxASEUIRepIcon.SelectionChanged, TextBoxASEStackingCategory.SelectionChanged, TextBoxASEFxScript.SelectionChanged, TextBoxASEDurationFxScript.SelectionChanged

        If CType(sender, ComboBox).SelectedItem Is Nothing Then
            Return
        End If

        If CType(sender, ComboBox).Text <> CType(sender, ComboBox).SelectedItem.ToString() Then
            CType(sender, ComboBox).Text = CType(sender, ComboBox).SelectedItem.ToString()
        End If

    End Sub


End Class


Public Class AnimActionConverter
    Implements IValueConverter


    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Return System.Enum.GetName(GetType(isogame.AnimAction), value)
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return DirectCast([Enum].Parse(GetType(isogame.AnimAction), value.ToString()), isogame.AnimAction)
    End Function

End Class

Public Class AnimModifierConverter
    Implements IValueConverter


    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Return System.Enum.GetName(GetType(isogame.AnimModifier), value)
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return DirectCast([Enum].Parse(GetType(isogame.AnimModifier), value.ToString()), isogame.AnimModifier)
    End Function

End Class

Public Class DamageTypeConverter
    Implements IValueConverter


    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Return System.Enum.GetName(GetType(isogame.VulnerabilityType), value)
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return DirectCast([Enum].Parse(GetType(isogame.VulnerabilityType), value.ToString()), isogame.VulnerabilityType)
    End Function

End Class

Public Class BooleanConverter
    Implements IValueConverter


    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        If value Is Nothing Then
            Return New ComboBoxItem With {.Content = "False"}
        End If

        If CBool(value) Then
            Return 0
        Else
            Return 1
        End If

    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack

        Select Case CInt(value)
            Case 0
                Return True
            Case 1
                Return False
            Case Else
                Return False
        End Select
    End Function

End Class

Public Class IntendedUserConverter
    Implements IValueConverter


    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        If value Is Nothing Then
            Return System.Enum.GetName(GetType(isogame.IntendedUser), IntendedUser.IntendedUser_Player)
        End If
        Return System.Enum.GetName(GetType(isogame.IntendedUser), value)
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return DirectCast([Enum].Parse(GetType(isogame.IntendedUser), value.ToString()), isogame.IntendedUser)
    End Function

End Class

Public Class StatusConditionsConverter
    Implements IValueConverter


    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Return System.Enum.GetName(GetType(isogame.StatusCondition), value)
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return DirectCast([Enum].Parse(GetType(isogame.StatusCondition), value.ToString()), isogame.StatusCondition)
    End Function

End Class