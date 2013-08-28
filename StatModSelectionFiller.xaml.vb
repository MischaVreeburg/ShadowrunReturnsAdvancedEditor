Imports System.Collections.ObjectModel
Imports Xceed.Wpf.Toolkit
Imports isogame

Public Class StatModSelectionFiller

    Public Canceled As Boolean
    Public Choice As New StatMod

    Private Sub buttonCancel_Click(sender As Object, e As RoutedEventArgs) Handles buttonCancel.Click
        Canceled = True
        Me.Close()
    End Sub

    Private Sub ListBoxSelectionFiller_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ComboBoxAttribute.DataContext = New ObservableCollection(Of String)(New List(Of String)(System.Enum.GetNames(GetType(isogame.Attribute))))

        ComboBoxSkill.DataContext = New ObservableCollection(Of String)(New List(Of String)(System.Enum.GetNames(GetType(isogame.Skill))))
        ComboBoxSpecialization.DataContext = New ObservableCollection(Of String)(New List(Of String)(System.Enum.GetNames(GetType(isogame.Specialization))))

        ComboBoxAttribute.SelectedValue = System.Enum.GetName(GetType(isogame.Attribute), Attribute.Attribute_None)
        ComboBoxSkill.SelectedValue = System.Enum.GetName(GetType(isogame.Skill), Skill.Skill_None)
        ComboBoxSkill.SelectedValue = System.Enum.GetName(GetType(isogame.Specialization), Specialization.Specialization_None)


    End Sub


    Private Sub buttonOk_Click(sender As Object, e As RoutedEventArgs) Handles buttonOk.Click
        If ComboBoxAttribute.SelectedItem Is Nothing Then
            Choice.attribute = isogame.Attribute.Attribute_None
        Else
            Choice.attribute = DirectCast([Enum].Parse(GetType(isogame.Attribute), ComboBoxAttribute.SelectedItem.ToString()), isogame.Attribute)
        End If

        If ComboBoxSkill.SelectedItem Is Nothing Then
            Choice.skill = isogame.Skill.Skill_None
        Else
            Choice.skill = DirectCast([Enum].Parse(GetType(isogame.Skill), ComboBoxSkill.SelectedItem.ToString()), isogame.Skill)
        End If

        If ComboBoxSpecialization.SelectedItem Is Nothing Then
            Choice.specialization = isogame.Specialization.Specialization_None
        Else
            Choice.specialization = DirectCast([Enum].Parse(GetType(isogame.Specialization), ComboBoxSpecialization.SelectedItem.ToString()), isogame.Specialization)
        End If

        If DirectCast([Enum].Parse(GetType(isogame.Attribute), ComboBoxAttribute.SelectedItem.ToString()), isogame.Attribute) = isogame.Attribute.Attribute_Magic_Essence Then
            Choice.intModValue = 0
            Choice.floatModValue = CSng(ComboBoxfloatModValue.Value)
        Else
            Choice.intModValue = CInt(ComboBoxintModValue.Value)
            Choice.floatModValue = 0.0F

        End If

        Me.Close()

    End Sub

    Private Sub ComboBoxAttribute_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ComboBoxAttribute.SelectionChanged
        If DirectCast([Enum].Parse(GetType(isogame.Attribute), ComboBoxAttribute.SelectedItem.ToString()), isogame.Attribute) <> isogame.Attribute.Attribute_None Then
            ComboBoxSkill.IsEnabled = False
            ComboBoxSpecialization.IsEnabled = False
            If DirectCast([Enum].Parse(GetType(isogame.Attribute), ComboBoxAttribute.SelectedItem.ToString()), isogame.Attribute) = isogame.Attribute.Attribute_Magic_Essence Then
                ComboBoxfloatModValue.IsEnabled = True
                ComboBoxintModValue.IsEnabled = False
            Else
                ComboBoxfloatModValue.IsEnabled = False
                ComboBoxintModValue.IsEnabled = True
            End If
        Else
            ComboBoxSkill.IsEnabled = True
            ComboBoxSpecialization.IsEnabled = True
        End If

    End Sub

    Private Sub ComboBoxSkill_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ComboBoxSkill.SelectionChanged
        If DirectCast([Enum].Parse(GetType(isogame.Skill), ComboBoxSkill.SelectedItem.ToString()), isogame.Skill) <> isogame.Skill.Skill_None Then
            ComboBoxAttribute.IsEnabled = False
            ComboBoxSpecialization.IsEnabled = False
        Else
            ComboBoxAttribute.IsEnabled = True
            ComboBoxSpecialization.IsEnabled = True
        End If
    End Sub

    Private Sub ComboBoxSpecialization_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ComboBoxSpecialization.SelectionChanged
        If DirectCast([Enum].Parse(GetType(isogame.Specialization), ComboBoxSpecialization.SelectedItem.ToString()), isogame.Specialization) <> isogame.Specialization.Specialization_None Then
            ComboBoxAttribute.IsEnabled = False
            ComboBoxSkill.IsEnabled = False
        Else
            ComboBoxAttribute.IsEnabled = True
            ComboBoxSkill.IsEnabled = True
        End If
    End Sub
End Class
