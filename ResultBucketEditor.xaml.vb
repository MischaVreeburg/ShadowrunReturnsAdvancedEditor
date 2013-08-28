Imports isogame
Imports System.Windows.Controls.Primitives
Imports Xceed.Wpf.Toolkit

Partial Public Class ResultBucketEditor

    Public Choice As ResultBucket
    Private EditingList As List(Of ResultBucket.Entry)
    Private EntryType As String
    Public Cancelled As Boolean = False
    Private ManualChange As Boolean = True

    Private Sub ResultBucketEditor_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        If EditingList.Count > 1 Then
            For i As Integer = 1 To EditingList.Count - 1
                Dim NewDock As New DockPanel With {.Name = "Entry_" & i.ToString()}
                StackPanelResultBucketEntries.Children.Add(NewDock)
                CType(StackPanelResultBucketEntries.Children(i), DockPanel).Children.Add(New RadioButton With {.Margin = New Thickness(0, 0, 5, 0), .VerticalAlignment = VerticalAlignment.Center})
                CType(StackPanelResultBucketEntries.Children(i), DockPanel).Children.Add(New Slider With {.Name = "Slider_" & i.ToString(), .Width = 100, .LargeChange = 0.1, .Maximum = 1, .SmallChange = 0.01, .TickFrequency = 0.1, .TickPlacement = TickPlacement.BottomRight})
                CType(StackPanelResultBucketEntries.Children(i), DockPanel).Children.Add(New TextBlock With {.Width = 30, .Margin = New Thickness(0, 2, 0, 0), .Text = "0", .TextAlignment = TextAlignment.Center})
                CType(StackPanelResultBucketEntries.Children(i), DockPanel).Children.Add(New CheckBox With {.Name = "Checkbox_" & i.ToString(), .Margin = New Thickness(5, 0, 0, 0), .VerticalAlignment = VerticalAlignment.Center, .IsChecked = False})
                CType(StackPanelResultBucketEntries.Children(i), DockPanel).Children.Add(New SingleUpDown With {.Width = 100, .Margin = New Thickness(5, 0, 0, 0), .Value = 0})
                RegisterControl("Entry_" & i.ToString(), NewDock)
                RegisterControl("Slider_" & i.ToString(), CType(NewDock.Children(1), Slider))
                RegisterControl("Checkbox_" & i.ToString(), CType(NewDock.Children(3), CheckBox))
                AddHandler CType(NewDock.Children(1), Slider).ValueChanged, AddressOf Slider_ValueChanged
                AddHandler CType(NewDock.Children(3), CheckBox).Checked, AddressOf Checkbox_Checked
                AddHandler CType(NewDock.Children(3), CheckBox).Unchecked, AddressOf Checkbox_Unchecked

            Next
        ElseIf EditingList.Count = 0 Then
            StackPanelResultBucketEntries.Children.Clear()
        End If
        ManualChange = False
        Dim Counter As Integer = 0
        For Each Entry As ResultBucket.Entry In EditingList
            CType(CType(StackPanelResultBucketEntries.Children(Counter), DockPanel).Children(1), Slider).Value = Entry.weight
            CType(CType(StackPanelResultBucketEntries.Children(Counter), DockPanel).Children(2), TextBlock).Text = CStr(Entry.weight)
            CType(CType(StackPanelResultBucketEntries.Children(Counter), DockPanel).Children(4), SingleUpDown).Value = Entry.value
            Counter += 1
        Next

        If StackPanelResultBucketEntries.Children.Count = 1 Then
            CType(CType(StackPanelResultBucketEntries.Children(0), DockPanel).Children(1), Slider).IsEnabled = False
            CType(CType(StackPanelResultBucketEntries.Children(0), DockPanel).Children(3), CheckBox).IsChecked = True
            CType(CType(StackPanelResultBucketEntries.Children(0), DockPanel).Children(3), CheckBox).IsEnabled = False
        End If
        ManualChange = True
    End Sub

    Private Sub buttonAdd_Click(sender As Object, e As RoutedEventArgs) Handles buttonAdd.Click
        ManualChange = False
        Dim i As Integer = StackPanelResultBucketEntries.Children.Count
        Dim NewDock As New DockPanel With {.Name = "Entry_" & i.ToString()}
        StackPanelResultBucketEntries.Children.Add(NewDock)
        CType(StackPanelResultBucketEntries.Children(i), DockPanel).Children.Add(New RadioButton With {.Margin = New Thickness(0, 0, 5, 0), .VerticalAlignment = VerticalAlignment.Center})
        CType(StackPanelResultBucketEntries.Children(i), DockPanel).Children.Add(New Slider With {.Name = "Slider_" & i.ToString(), .Width = 100, .LargeChange = 0.1, .Maximum = 1, .SmallChange = 0.01, .TickFrequency = 0.1, .TickPlacement = TickPlacement.BottomRight})
        CType(StackPanelResultBucketEntries.Children(i), DockPanel).Children.Add(New TextBlock With {.Width = 30, .Margin = New Thickness(0, 2, 0, 0), .Text = "0", .TextAlignment = TextAlignment.Center})
        CType(StackPanelResultBucketEntries.Children(i), DockPanel).Children.Add(New CheckBox With {.Name = "Checkbox_" & i.ToString(), .Margin = New Thickness(5, 0, 0, 0), .VerticalAlignment = VerticalAlignment.Center, .IsChecked = False})
        CType(StackPanelResultBucketEntries.Children(i), DockPanel).Children.Add(New SingleUpDown With {.Width = 100, .Margin = New Thickness(5, 0, 0, 0), .Value = 0})
        RegisterControl("Entry_" & i.ToString(), NewDock)
        RegisterControl("Slider_" & i.ToString(), CType(NewDock.Children(1), Slider))
        RegisterControl("Checkbox_" & i.ToString(), CType(NewDock.Children(3), CheckBox))
        AddHandler CType(NewDock.Children(1), Slider).ValueChanged, AddressOf Slider_ValueChanged
        AddHandler CType(NewDock.Children(3), CheckBox).Checked, AddressOf Checkbox_Checked
        AddHandler CType(NewDock.Children(3), CheckBox).Unchecked, AddressOf Checkbox_Unchecked

        If StackPanelResultBucketEntries.Children.Count = 1 Then
            CType(CType(StackPanelResultBucketEntries.Children(0), DockPanel).Children(1), Slider).IsEnabled = False
            CType(CType(StackPanelResultBucketEntries.Children(0), DockPanel).Children(3), CheckBox).IsChecked = True
            CType(CType(StackPanelResultBucketEntries.Children(0), DockPanel).Children(3), CheckBox).IsEnabled = False
            CType(CType(StackPanelResultBucketEntries.Children(0), DockPanel).Children(1), Slider).Value = 1
            CType(CType(StackPanelResultBucketEntries.Children(0), DockPanel).Children(2), TextBlock).Text = 1.ToString()

        ElseIf StackPanelResultBucketEntries.Children.Count > 1 Then
            For j As Integer = 0 To StackPanelResultBucketEntries.Children.Count - 1
                ' CType(CType(StackPanelResultBucketEntries.Children(j), DockPanel).Children(1), Slider).IsEnabled = False
                '  CType(CType(StackPanelResultBucketEntries.Children(j), DockPanel).Children(2), CheckBox).IsChecked = True
                CType(CType(StackPanelResultBucketEntries.Children(j), DockPanel).Children(3), CheckBox).IsEnabled = True
            Next
        End If

        ManualChange = True

    End Sub

    Private Sub buttonRemove_Click(sender As Object, e As RoutedEventArgs) Handles buttonRemove.Click
        ManualChange = False
        For Each Child As UIElement In StackPanelResultBucketEntries.Children
            If CType(CType(Child, DockPanel).Children(0), RadioButton).IsChecked Then
                StackPanelResultBucketEntries.Children.Remove(Child)
                Return
            End If
        Next

        If StackPanelResultBucketEntries.Children.Count = 1 Then
            CType(CType(StackPanelResultBucketEntries.Children(0), DockPanel).Children(1), Slider).IsEnabled = False
            CType(CType(StackPanelResultBucketEntries.Children(0), DockPanel).Children(3), CheckBox).IsChecked = True
            CType(CType(StackPanelResultBucketEntries.Children(0), DockPanel).Children(3), CheckBox).IsEnabled = False
        End If
        ManualChange = True

    End Sub

    Private Sub buttonOk_Click(sender As Object, e As RoutedEventArgs) Handles buttonOk.Click
        Cancelled = False
        EditingList.Clear()
        For Each Child As UIElement In StackPanelResultBucketEntries.Children
            EditingList.Add(New ResultBucket.Entry() With {.weight = CSng(CType(CType(Child, DockPanel).Children(1), Slider).Value), .value = CSng(CType(CType(Child, DockPanel).Children(4), SingleUpDown).Value)})
        Next
        Me.Close()
    End Sub

    Private Sub buttonCancel_Click(sender As Object, e As RoutedEventArgs) Handles buttonCancel.Click
        Cancelled = True

        Me.Close()
    End Sub

    Public Sub New(ResultBucket As ResultBucket, EntryType As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Choice = ResultBucket

        If EntryType = "failure" Then
            EditingList = Choice.failureEntries
        ElseIf EntryType = "success" Then
            EditingList = Choice.successEntries
        End If
        Me.EntryType = EntryType

    End Sub
    
    Public Sub RegisterControl(Of T)(ByVal Name As String, ByVal ControlElement As T)

        If (CType(Me.FindName(Name), T) IsNot Nothing) Then
            Me.UnregisterName(Name)
        End If
        Me.RegisterName(Name, ControlElement)

    End Sub

    Public Sub RegisterControl(Of T)(ByVal Name As String, ByVal OldName As String, ByVal ControlElement As T)

        If (CType(Me.FindName(OldName), T) IsNot Nothing) Then
            Me.UnregisterName(OldName)
        End If
        If (CType(Me.FindName(Name), T) IsNot Nothing) Then
            Me.UnregisterName(Name)
        End If
        Me.RegisterName(Name, ControlElement)

    End Sub

    Private Sub Slider_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double)) Handles Slider_0.ValueChanged
        Dim IndexOfFirstChanger As Integer
        Dim NumOfLockedItems As Integer
        Dim TotalValueOfCheckedItems As Single
        Dim newValue As Single = CSng(e.NewValue)

        If ManualChange Then
            IndexOfFirstChanger = StackPanelResultBucketEntries.Children.IndexOf(CType(CType(sender, Slider).Parent, DockPanel))
            For Each Child As UIElement In StackPanelResultBucketEntries.Children
                If CType(CType(Child, DockPanel).Children(3), CheckBox).IsChecked Then
                    NumOfLockedItems += 1
                    TotalValueOfCheckedItems += CSng(CType(CType(Child, DockPanel).Children(1), Slider).Value)
                End If
            Next
            ManualChange = False
            If e.NewValue > (1 - TotalValueOfCheckedItems) Then
                CType(sender, Slider).Value = (1 - TotalValueOfCheckedItems)
                newValue = (1 - TotalValueOfCheckedItems)
            End If
            Dim changeAmount As Double = newValue - e.OldValue
            Dim RestAmount As Double = 0
            Dim RestAmount2 As Double = 0
            For i As Integer = 0 To StackPanelResultBucketEntries.Children.Count - 1
                '  For Each Child As UIElement In StackPanelResultBucketEntries.Children
                Dim Child As UIElement = StackPanelResultBucketEntries.Children(i)
                If StackPanelResultBucketEntries.Children.IndexOf(Child) <> IndexOfFirstChanger Then
                    If CType(CType(Child, DockPanel).Children(3), CheckBox).IsChecked = False Then
                        If CDbl(changeAmount / (StackPanelResultBucketEntries.Children.Count - NumOfLockedItems - 1)) > CType(CType(Child, DockPanel).Children(1), Slider).Value Then
                            RestAmount += CDbl(changeAmount / (StackPanelResultBucketEntries.Children.Count - NumOfLockedItems - 1)) + CType(CType(Child, DockPanel).Children(1), Slider).Value
                            CType(CType(Child, DockPanel).Children(1), Slider).Value = 0
                        Else
                            CType(CType(Child, DockPanel).Children(1), Slider).Value -= CDbl(changeAmount / (StackPanelResultBucketEntries.Children.Count - NumOfLockedItems - 1))
                        End If
                    End If
                End If
            Next
            If RestAmount > 0 Then
                Dim countItemsWithRemainingValue As Integer = 0
                For Each Child As UIElement In StackPanelResultBucketEntries.Children
                    If StackPanelResultBucketEntries.Children.IndexOf(Child) <> IndexOfFirstChanger Then
                        If CType(CType(Child, DockPanel).Children(1), Slider).Value > 0 Then
                            countItemsWithRemainingValue += 1
                        End If
                    End If
                Next
                If countItemsWithRemainingValue > 0 Then
                    For Each Child As UIElement In StackPanelResultBucketEntries.Children
                        If StackPanelResultBucketEntries.Children.IndexOf(Child) <> IndexOfFirstChanger Then
                            If CType(CType(Child, DockPanel).Children(3), CheckBox).IsChecked = False Then
                                If CType(CType(Child, DockPanel).Children(1), Slider).Value > 0 Then
                                    If -CDbl(RestAmount / (countItemsWithRemainingValue)) > CType(CType(Child, DockPanel).Children(1), Slider).Value Then
                                        RestAmount2 += CDbl(RestAmount / (countItemsWithRemainingValue)) + CType(CType(Child, DockPanel).Children(1), Slider).Value
                                        CType(CType(Child, DockPanel).Children(1), Slider).Value = 0
                                    Else
                                        CType(CType(Child, DockPanel).Children(1), Slider).Value -= CDbl(RestAmount / (countItemsWithRemainingValue))
                                    End If
                                End If
                            End If

                        End If
                    Next
                Else
                    CType(CType(StackPanelResultBucketEntries.Children(IndexOfFirstChanger), DockPanel).Children(1), Slider).Value += RestAmount
                End If
                If RestAmount2 > 0 Then
                    'remove amount from all that are not 0, not locked and not the first changer.
                    ' there are no others or still a rest amount2 than add to first changer.
                    CType(CType(StackPanelResultBucketEntries.Children(IndexOfFirstChanger), DockPanel).Children(1), Slider).Value += RestAmount2
                End If
            End If
            ManualChange = True
        End If


        CType(CType(CType(sender, Slider).Parent, DockPanel).Children(2), TextBlock).Text = CStr(CType(sender, Slider).Value)

    End Sub

    Private Sub Checkbox_Checked(sender As Object, e As RoutedEventArgs) Handles Checkbox_0.Checked
        CType(CType(CType(sender, CheckBox).Parent, DockPanel).Children(1), Slider).IsEnabled = False
    End Sub

    Private Sub Checkbox_Unchecked(sender As Object, e As RoutedEventArgs) Handles Checkbox_0.Unchecked
        CType(CType(CType(sender, CheckBox).Parent, DockPanel).Children(1), Slider).IsEnabled = True
    End Sub
End Class
