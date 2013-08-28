Imports System.Collections.ObjectModel
Imports Xceed.Wpf.Toolkit
Imports isogame

Public Class ResultBucketSelectionFiller

    Public Canceled As Boolean
    Public Choice As New ResultBucket.Entry

    Private Sub buttonCancel_Click(sender As Object, e As RoutedEventArgs) Handles buttonCancel.Click
        Canceled = True
        Me.Close()
    End Sub

    Private Sub ListBoxSelectionFiller_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

    End Sub

    
    Private Sub buttonOk_Click(sender As Object, e As RoutedEventArgs) Handles buttonOk.Click
        Choice.value = CSng(ComboBoxResultBucketValue.Value)
        Choice.weight = CSng(ComboBoxResultBucketWeight.Value)

        Me.Close()
    End Sub
End Class
