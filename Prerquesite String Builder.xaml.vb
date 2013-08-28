Imports System.Collections.ObjectModel

Public Class Prerquesite_String_Builder

    Public Cancelled As Boolean = False
    Public Choice As String
    Private TargetList As New List(Of String)(New String() {"Player", "Item"})
    Private PlayerFlagList As New List(Of String)(New String() {"ATTRIBUTE", "SKILL", "SPECIALIZATION", "CONDITION", "FLAG"})
    Private ItemFlagList As New List(Of String)(New String() {"CORE_ATTRIBUTE", "CORE_SKILL", "CORE_SPECIALIZATION", "FLAG"})
    Private ComparerList1 As New List(Of String)(New String() {"<", "<=", ">", ">=", "==", "!="})
    Private ComparerList2 As New List(Of String)(New String() {"==", "!="})
    Private ComparerList3 As New List(Of String)(New String() {"EXISTS", "NOTEXISTS", "EQUIPPED", "NOT_EQUIPPED"})
    Private ComparableItemList As New List(Of String)(New String() {"WEAPON_CUR_AMMO", "WEAPON_MAX_AMMO", "REACH"})
    Private ComparablePlayerList As New List(Of String)(New String() {"AIMING", "ATTACKS_USED", "CAN_INTERACT", "CAN_PICKUP", "CAN_SUMMON_DRONES", "CAN_SUMMON_ESP", "CAN_SUMMON_SPIRITS", "CAN_TAKE_COVER", "CHANGED_FIRE_MODE", "DEFENSES_USED", "DODGES_USED", "DRONES_ACTIVE", "HAS_DATAJACK", "HEALTH_USED", "IN_COVER", "MASTER_DRONES_ACTIVE", "OFFPHASE_USED", "OVERWATCH", "REACH", "SLAVES_CAN_RETURN", "SPIRIT_POINT_NEARBY", "SPIRITS_ACTIVE", "SWAPPED_WEAPON", "TOOK_COVER", "WEAPON"})

    Private SelectedTarget As String = String.Empty
    Private Flag As String = String.Empty
    Private Comparable As String = String.Empty
    Private Comparer As String = String.Empty
    Private Value As String = String.Empty


    Private Sub buttonOk_Click(sender As Object, e As RoutedEventArgs) Handles buttonOk.Click
        Choice = ResultString.Text
        Me.Close()
    End Sub

    Private Sub buttonCancel_Click(sender As Object, e As RoutedEventArgs) Handles buttonCancel.Click
        Cancelled = True
        Me.Close()
    End Sub

    Private Sub Prerquesite_String_Builder_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        comboBoxTargetID.DataContext = TargetList
        comboBoxFlag.DataContext = PlayerFlagList
        comboBoxComparable.DataContext = New ObservableCollection(Of String)(New List(Of String)(System.Enum.GetNames(GetType(isogame.Attribute))))
        comboBoxComparer.DataContext = ComparerList1
        buttonOk.IsEnabled = False

    End Sub

    Private Sub comboBoxTargetID_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboBoxTargetID.SelectionChanged
        Select Case comboBoxTargetID.SelectedItem.ToString()
            Case "Player"
                comboBoxFlag.DataContext = PlayerFlagList
                SelectedTarget = comboBoxTargetID.SelectedItem.ToString()
                comboBoxComparable.SelectedIndex = -1
                comboBoxComparer.SelectedIndex = -1
                SpinControl.Value = Nothing
            Case "Item"
                comboBoxFlag.DataContext = ItemFlagList
                SelectedTarget = comboBoxTargetID.SelectedItem.ToString()
                comboBoxComparable.SelectedIndex = -1
                comboBoxComparer.SelectedIndex = -1
                SpinControl.Value = Nothing
            Case Else
                SelectedTarget = String.Empty
        End Select
        
        If Flag.Contains("CORE") Then
            ResultString.Text = SelectedTarget & "." & Flag & " " & Comparer & " " & Value
        Else
            ResultString.Text = SelectedTarget & "." & Flag & "." & Comparable & " " & Comparer & " " & Value
        End If
        If Flag.Contains("CORE") Then
            If SelectedTarget <> "" AndAlso Flag <> "" AndAlso Comparer <> "" AndAlso Value <> "" Then
                buttonOk.IsEnabled = True
            Else
                buttonOk.IsEnabled = False

            End If
        ElseIf comboBoxComparer.DataContext Is ComparerList3 Then
            If SelectedTarget <> "" AndAlso Flag <> "" AndAlso Comparable <> "" AndAlso Comparer <> "" Then
                buttonOk.IsEnabled = True
            Else
                buttonOk.IsEnabled = False
            End If
        Else
            If SelectedTarget <> "" AndAlso Flag <> "" AndAlso Comparable <> "" AndAlso Comparer <> "" AndAlso Value <> "" Then
                buttonOk.IsEnabled = True
            Else
                buttonOk.IsEnabled = False
            End If
        End If
    End Sub
    
    Private Sub comboBoxFlag_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboBoxFlag.SelectionChanged
        If comboBoxFlag.SelectedItem Is Nothing Then
            Flag = ""
            Return
        End If

        Select Case comboBoxFlag.SelectedItem.ToString()
            Case "ATTRIBUTE"
                comboBoxComparable.DataContext = New ObservableCollection(Of String)(New List(Of String)(System.Enum.GetNames(GetType(isogame.Attribute))))
                Flag = comboBoxFlag.SelectedItem.ToString()
                comboBoxComparer.SelectedIndex = -1
                SpinControl.Value = Nothing
            Case "SKILL"
                comboBoxComparable.DataContext = New ObservableCollection(Of String)(New List(Of String)(System.Enum.GetNames(GetType(isogame.Skill))))
                Flag = comboBoxFlag.SelectedItem.ToString()
                comboBoxComparer.SelectedIndex = -1
                SpinControl.Value = Nothing
            Case "SPECIALIZATION"
                comboBoxComparable.DataContext = New ObservableCollection(Of String)(New List(Of String)(System.Enum.GetNames(GetType(isogame.Specialization))))
                Flag = comboBoxFlag.SelectedItem.ToString()
                comboBoxComparer.SelectedIndex = -1
                SpinControl.Value = Nothing
            Case "CONDITION"
                comboBoxComparable.DataContext = New ObservableCollection(Of String)(New List(Of String)(System.Enum.GetNames(GetType(isogame.StatusCondition))))
                Flag = comboBoxFlag.SelectedItem.ToString()
                comboBoxComparer.SelectedIndex = -1
                SpinControl.Value = Nothing
            Case "CORE_ATTRIBUTE"
                comboBoxComparable.DataContext = New ObservableCollection(Of String)(New List(Of String))
                comboBoxComparer.DataContext = ComparerList1
                Flag = comboBoxFlag.SelectedItem.ToString()
                comboBoxComparer.SelectedIndex = -1
                SpinControl.Value = Nothing
            Case "CORE_SKILL"
                comboBoxComparable.DataContext = New ObservableCollection(Of String)(New List(Of String))
                comboBoxComparer.DataContext = ComparerList1
                Flag = comboBoxFlag.SelectedItem.ToString()
                comboBoxComparer.SelectedIndex = -1
                SpinControl.Value = Nothing
            Case "CORE_SPECIALIZATION"
                comboBoxComparable.DataContext = New ObservableCollection(Of String)(New List(Of String))
                comboBoxComparer.DataContext = ComparerList1
                Flag = comboBoxFlag.SelectedItem.ToString()
                comboBoxComparer.SelectedIndex = -1
                SpinControl.Value = Nothing
            Case "FLAG"
                If SelectedTarget = "Player" Then
                    comboBoxComparable.DataContext = ComparablePlayerList
                ElseIf SelectedTarget = "Item" Then
                    comboBoxComparable.DataContext = ComparableItemList
                End If
                Flag = comboBoxFlag.SelectedItem.ToString()
                comboBoxComparer.SelectedIndex = -1
                SpinControl.Value = Nothing
            Case Else
                Flag = String.Empty
        End Select

        If Flag.Contains("CORE") Then
            ResultString.Text = SelectedTarget & "." & Flag & " " & Comparer & " " & Value
        Else
            ResultString.Text = SelectedTarget & "." & Flag & "." & Comparable & " " & Comparer & " " & Value
        End If
        If Flag.Contains("CORE") Then
            If SelectedTarget <> "" AndAlso Flag <> "" AndAlso Comparer <> "" AndAlso Value <> "" Then
                buttonOk.IsEnabled = True
            Else
                buttonOk.IsEnabled = False

            End If
        ElseIf comboBoxComparer.DataContext Is ComparerList3 Then
            If SelectedTarget <> "" AndAlso Flag <> "" AndAlso Comparable <> "" AndAlso Comparer <> "" Then
                buttonOk.IsEnabled = True
            Else
                buttonOk.IsEnabled = False
            End If
        Else
            If SelectedTarget <> "" AndAlso Flag <> "" AndAlso Comparable <> "" AndAlso Comparer <> "" AndAlso Value <> "" Then
                buttonOk.IsEnabled = True
            Else
                buttonOk.IsEnabled = False
            End If
        End If
    End Sub

    Private Sub comboBoxComparable_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboBoxComparable.SelectionChanged
        If comboBoxComparable.SelectedIndex = -1 Then
            Comparable = ""
            Return
        End If
        Select Case comboBoxComparable.SelectedItem.ToString()
            Case "AIMING"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "ATTACKS_USED"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "CAN_INTERACT"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "CAN_PICKUP"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "CAN_SUMMON_DRONES"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "CAN_SUMMON_ESP"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "CAN_SUMMON_SPIRITS"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "CAN_TAKE_COVER"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "CHANGED_FIRE_MODE"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "DEFENSES_USED"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "DODGES_USED"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "HAS_DATAJACK"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "HEALTH_USED"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "IN_COVER"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "MASTER_DRONES_ACTIVE"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "OFFPHASE_USED"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "OVERWATCH"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "REACH"
                comboBoxComparer.DataContext = ComparerList1
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 999
                SpinControl.Value = Nothing
            Case "SLAVES_CAN_RETURN"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "SPIRIT_POINT_NEARBY"
                comboBoxComparer.DataContext = ComparerList3
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "SPIRITS_ACTIVE"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "SWAPPED_WEAPON"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "WEAPON"
                comboBoxComparer.DataContext = ComparerList3
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "TOOK_COVER"
                comboBoxComparer.DataContext = ComparerList2
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 1
                SpinControl.Value = Nothing
            Case "WEAPON_CUR_AMMO"
                comboBoxComparer.DataContext = ComparerList1
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 999
                SpinControl.Value = Nothing
            Case "WEAPON_MAX_AMMO"
                comboBoxComparer.DataContext = ComparerList1
                Comparable = comboBoxComparable.SelectedItem.ToString()
                SpinControl.Maximum = 999
            Case Else
                SpinControl.Value = Nothing
                If comboBoxComparable.SelectedItem.ToString().Contains("Condition") Then
                    comboBoxComparer.DataContext = ComparerList2
                    SpinControl.Maximum = 1
                End If
        End Select
        Comparable = comboBoxComparable.SelectedItem.ToString()

        If Flag.Contains("CORE") Then
            ResultString.Text = SelectedTarget & "." & Flag & " " & Comparer & " " & Value
        Else
            ResultString.Text = SelectedTarget & "." & Flag & "." & Comparable & " " & Comparer & " " & Value
        End If
        If Flag.Contains("CORE") Then
            If SelectedTarget <> "" AndAlso Flag <> "" AndAlso Comparer <> "" AndAlso Value <> "" Then
                buttonOk.IsEnabled = True
            Else
                buttonOk.IsEnabled = False

            End If
        ElseIf comboBoxComparer.DataContext Is ComparerList3 Then
            If SelectedTarget <> "" AndAlso Flag <> "" AndAlso Comparable <> "" AndAlso Comparer <> "" Then
                buttonOk.IsEnabled = True
            Else
                buttonOk.IsEnabled = False
            End If
        Else
            If SelectedTarget <> "" AndAlso Flag <> "" AndAlso Comparable <> "" AndAlso Comparer <> "" AndAlso Value <> "" Then
                buttonOk.IsEnabled = True
            Else
                buttonOk.IsEnabled = False
            End If
        End If
    End Sub

    Private Sub SpinControl_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Object)) Handles SpinControl.ValueChanged

        If SpinControl.Value Is Nothing Then
            Value = ""
            Return
        End If

        Value = CStr(SpinControl.Value)

        If Flag.Contains("CORE") Then
            ResultString.Text = SelectedTarget & "." & Flag & " " & Comparer & " " & Value
        ElseIf comboBoxComparer.DataContext Is ComparerList3 Then
            ResultString.Text = SelectedTarget & "." & Flag & "." & Comparable & " " & Comparer
        Else
            ResultString.Text = SelectedTarget & "." & Flag & "." & Comparable & " " & Comparer & " " & Value
        End If

        If Flag.Contains("CORE") Then
            If SelectedTarget <> "" AndAlso Flag <> "" AndAlso Comparer <> "" AndAlso Value <> "" Then
                buttonOk.IsEnabled = True
            Else
                buttonOk.IsEnabled = False

            End If
        ElseIf comboBoxComparer.DataContext Is ComparerList3 Then
            If SelectedTarget <> "" AndAlso Flag <> "" AndAlso Comparable <> "" AndAlso Comparer <> "" Then
                buttonOk.IsEnabled = True
            Else
                buttonOk.IsEnabled = False
            End If
        Else
            If SelectedTarget <> "" AndAlso Flag <> "" AndAlso Comparable <> "" AndAlso Comparer <> "" AndAlso Value <> "" Then
                buttonOk.IsEnabled = True
            Else
                buttonOk.IsEnabled = False
            End If
        End If
    End Sub

    Private Sub comboBoxComparer_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboBoxComparer.SelectionChanged
        If comboBoxComparer.SelectedIndex = -1 Then
            Comparer = ""
            Return
        End If
        Comparer = comboBoxComparer.SelectedItem.ToString()

        If Flag.Contains("CORE") Then
            ResultString.Text = SelectedTarget & "." & Flag & " " & Comparer & " " & Value
        Else
            ResultString.Text = SelectedTarget & "." & Flag & "." & Comparable & " " & Comparer & " " & Value
        End If
        If Flag.Contains("CORE") Then
            If SelectedTarget <> "" AndAlso Flag <> "" AndAlso Comparer <> "" AndAlso Value <> "" Then
                buttonOk.IsEnabled = True
            Else
                buttonOk.IsEnabled = False

            End If
        ElseIf comboBoxComparer.DataContext Is ComparerList3 Then
            If SelectedTarget <> "" AndAlso Flag <> "" AndAlso Comparable <> "" AndAlso Comparer <> "" Then
                buttonOk.IsEnabled = True
            Else
                buttonOk.IsEnabled = False
            End If
        Else
            If SelectedTarget <> "" AndAlso Flag <> "" AndAlso Comparable <> "" AndAlso Comparer <> "" AndAlso Value <> "" Then
                buttonOk.IsEnabled = True
            Else
                buttonOk.IsEnabled = False
            End If
        End If
    End Sub
End Class
