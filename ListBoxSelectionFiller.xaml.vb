
Imports System.Collections.ObjectModel
Imports System.ComponentModel
'Imports isogame

Public Class ListBoxSelectionFiller

    Public Canceled As Boolean
    Public Choice As String

    Private Sub buttonCancel_Click(sender As Object, e As RoutedEventArgs) Handles buttonCancel.Click
        Canceled = True
        Me.Close()
    End Sub

    Private Sub ListBoxSelectionFiller_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

    End Sub






    Private Sub TextBoxLostKeyboardFocus(sender As Object, e As KeyboardFocusChangedEventArgs) Handles ComboBoxListBoxFilling.LostKeyboardFocus

        If CType(sender, ComboBox).Text <> "" Then
            Dim teststr As String
            If TryCast(CType(sender, ComboBox).DataContext, ObservableCollection(Of String)) IsNot Nothing Then
                teststr = TryCast(CType(sender, ComboBox).DataContext, ObservableCollection(Of String)).FirstOrDefault(Function(S As String) S = CType(sender, ComboBox).Text)
                ' Dim p As New Predicate(Of Object)(Function(S As String) S = CType(sender, ComboBox).Text)
                '  TryCast(CType(sender, ComboBox).DataContext, ICollectionView).Filter = (Function(S As String) S = CType(sender, ComboBox).Text)
            ElseIf TryCast(CType(sender, ComboBox).DataContext, ObservableCollection(Of isogame.AbilityDef)) IsNot Nothing Then
                teststr = TryCast(CType(sender, ComboBox).DataContext, ObservableCollection(Of isogame.AbilityDef)).FirstOrDefault(Function(S As isogame.AbilityDef) S.id = CType(sender, ComboBox).Text).id
            Else

                teststr = CStr(CType(TryCast(CType(sender, ComboBox).DataContext, ICollectionView).CurrentItem, isogame.AbilityDef).id)
            End If

            If TryCast(CType(sender, ComboBox).DataContext, ObservableCollection(Of String)) IsNot Nothing Then
                If CType(CType(sender, ComboBox).DataContext, ObservableCollection(Of String)).FirstOrDefault(Function(S As String) S = CType(sender, ComboBox).Text) Is Nothing Then
                    CType(CType(sender, ComboBox).DataContext, ObservableCollection(Of String)).Add(CType(sender, ComboBox).Text)
                End If
                CType(sender, ComboBox).SelectedIndex = CType(CType(sender, ComboBox).DataContext, ObservableCollection(Of String)).IndexOf(CType(sender, ComboBox).Text)
            Else

            End If
        End If

    End Sub
    Private Sub TextBoxSelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ComboBoxListBoxFilling.SelectionChanged
        If CType(sender, ComboBox).SelectedItem Is Nothing Then
            Return
        End If


        'If CType(sender, ComboBox).Text <> CType(sender, ComboBox).SelectedItem.ToString() Then
        '    CType(sender, ComboBox).Text = CType(sender, ComboBox).SelectedItem.ToString()
        'End If

    End Sub


    Private Sub buttonOk_Click(sender As Object, e As RoutedEventArgs) Handles buttonOk.Click
        If ComboBoxListBoxFilling.SelectedItem Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        If TryCast(ComboBoxListBoxFilling.SelectedItem, isogame.AbilityDef) IsNot Nothing Then
            Choice = TryCast(ComboBoxListBoxFilling.SelectedItem, isogame.AbilityDef).id
        Else
            Choice = CStr(ComboBoxListBoxFilling.SelectedItem.ToString())
        End If

        Me.Close()
    End Sub
End Class
