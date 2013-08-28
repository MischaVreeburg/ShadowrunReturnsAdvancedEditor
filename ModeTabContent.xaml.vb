Imports isogame

Public Class ModeTabContent
    Private _CurrentModeDefObj As ModeDef
    Private _BackupModeDefObj As ModeDef = Nothing
    Private _BackupModeDefId As String
    Public WithEvents dataProvider As ObjectDataProvider = TryCast(Me.Resources("dataProvider"), ObjectDataProvider)
    Private ManualEdit As Boolean

    Public Property CurrentModeDefObj As ModeDef
        Get
            Return _CurrentModeDefObj
        End Get
        Set(value As ModeDef)
            _CurrentModeDefObj = value
        End Set
    End Property

    Public Property BackupModeDefId As String
        Get
            Return _BackupModeDefId
        End Get
        Set(value As String)
            _BackupModeDefId = value
        End Set
    End Property

    Public Property BackupModeDefObj As ModeDef
        Get
            Return _BackupModeDefObj
        End Get
        Set(value As ModeDef)
            _BackupModeDefObj = value
        End Set
    End Property

    Public Sub New(ByRef CurrModeDefObject As ModeDef)

        ' This call is required by the designer.
        InitializeComponent()
        CurrentModeDefObj = CurrModeDefObject
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub TextBoxId_LostKeyboardFocus(sender As Object, e As KeyboardFocusChangedEventArgs) Handles TextBoxId.LostKeyboardFocus
        ManualEdit = True
        BackupModeDefId = TextBoxId.Text
    End Sub

    Private Sub TextBoxId_TextChanged(sender As Object, e As TextChangedEventArgs) Handles TextBoxId.TextChanged
        If ManualEdit Then
            BackupModeDefObj = CurrentModeDefObj
            BackupModeDefId = TextBoxId.Text
            ManualEdit = False
        End If
    End Sub

    Private Sub buttonAddPreRequisiteStrings_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddPreRequisiteStrings.Click
        Dim PreReqBuilder As New Prerquesite_String_Builder
        PreReqBuilder.ShowDialog()
        If PreReqBuilder.Cancelled Then
            Return
        End If

        If CType(dataProvider.Data, ModeDef).prereqStrings Is Nothing Then
            Return
        End If

        CType(dataProvider.Data, ModeDef).prereqStrings.Add(PreReqBuilder.Choice)
        dataProvider.Refresh()
        dataProvider.ObjectInstance = New ModeDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentModeDefObject
        dataProvider.Refresh()
    End Sub

    Private Sub buttonRemovePreRequisiteStrings_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemovePreRequisiteStrings.Click
        CType(dataProvider.Data, ModeDef).prereqStrings.Remove(listBoxPrereqStrings.SelectedValue.ToString())
        dataProvider.Refresh()
        dataProvider.ObjectInstance = New ModeDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentModeDefObject
        dataProvider.Refresh()
    End Sub

    Private Sub buttonAddAbilities_Click(sender As Object, e As RoutedEventArgs) Handles buttonAddAbilities.Click

        Dim LSF As New ListBoxSelectionFiller
        LSF.ComboBoxListBoxFilling.DataContext = WindowSRRItemEditor.AbilityList
        LSF.ComboBoxListBoxFilling.DisplayMemberPath = "id"
        LSF.ComboBoxListBoxFilling.SelectedValuePath = "id"

        LSF.label.Content = "Enter a new ability"

        LSF.ShowDialog()
        If LSF.Canceled Then
            Return
        End If
        CType(dataProvider.Data, ModeDef).abilities.Add(LSF.Choice)
        dataProvider.Refresh()
        dataProvider.ObjectInstance = New ModeDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentModeDefObject
        dataProvider.Refresh()

    End Sub

    Private Sub buttonRemoveAbilities_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemoveAbilities.Click

        CType(dataProvider.Data, ModeDef).abilities.Remove(listBoxAbilities.SelectedValue.ToString())
        dataProvider.Refresh()
        dataProvider.ObjectInstance = New ModeDef
        dataProvider.ObjectInstance = WindowSRRItemEditor.CurrentModeDefObject
        dataProvider.Refresh()

    End Sub

    Private Sub ModeTabContent_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        TextBoxUIRepIcon.DataContext = WindowSRRItemEditor.IconList

        dataProvider = TryCast(Me.Resources("dataProvider"), ObjectDataProvider)
        If Not (dataProvider Is Nothing) Then
            dataProvider.ObjectInstance = CurrentModeDefObj
        End If
    End Sub

    Private Sub listBoxAbilities_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles listBoxAbilities.MouseDoubleClick
        If listBoxAbilities.SelectedItem Is Nothing Then
            Return
        End If
        WindowSRRItemEditor.CurrentAbilityDefObject = WindowSRRItemEditor.AbilityList.FirstOrDefault(Function(C As isogame.AbilityDef) C.id = listBoxAbilities.SelectedValue.ToString())
        CType(CType(Me.Parent, TabItem).Parent, TabControl).SelectedIndex = CType(CType(Me.Parent, TabItem).Parent, TabControl).Items.IndexOf(CType(CType(Me.Parent, TabItem).Parent, TabControl).FindName("AbilitiesTab"))

    End Sub
End Class
