Imports Xceed.Wpf.Toolkit
Public Class ListBoxSingleSelectionFiller

    Public Choice As Single
    Public Canceled As Boolean
    Public ListBoxSelectionFillerListSource As List(Of String)

    Private Sub buttonCancel_Click(sender As Object, e As RoutedEventArgs) Handles buttonCancel.Click
        Canceled = True
        Me.Close()
    End Sub

    Private Sub ListBoxSelectionFiller_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
      End Sub


    Private Sub ComboBoxListBoxFilling_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBoxListBoxFilling.KeyDown

    End Sub

    Private Sub ComboBoxListBoxFilling_LostKeyboardFocus(sender As Object, e As KeyboardFocusChangedEventArgs) Handles ComboBoxListBoxFilling.LostKeyboardFocus

    End Sub

    Private Sub buttonOk_Click(sender As Object, e As RoutedEventArgs) Handles buttonOk.Click
        Choice = CSng(ComboBoxListBoxFilling.Value)
        Me.Close()
    End Sub
End Class
