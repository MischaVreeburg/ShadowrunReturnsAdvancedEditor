Imports System.IO
Imports Microsoft.VisualBasic.CompilerServices
Imports isogame
Imports ProtoBuf
Imports UnityEngine
Imports System.Collections
Imports System.Web.Script.Serialization
Imports System.Reflection
Imports Microsoft.VisualBasic.FileIO

Public Module Module1

    Public Function FileSelector(ByVal Extension As String, ByVal MsgString As String) As String
        Try
            Dim FileName As String = ""
            Dim FD As System.Windows.Forms.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()

            FD.Title = "Open File Dialog"
            FD.InitialDirectory = My.Settings.LastUsedLocation

            FD.Filter = MsgString & " (*." & Extension & ")|*." & Extension & "|All files (*.*)|*.*"
            FD.FilterIndex = 2
            FD.RestoreDirectory = True

            If FD.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                FileName = FD.FileName
            Else
                FileName = String.Empty
            End If

            If File.Exists(FileName) Then
                My.Settings.LastUsedLocation = IO.Path.GetDirectoryName(FileName) & "\"
                My.Settings.Save()
            End If

            Return FileName

        Catch Ex As Exception
            MsgBox("FileSelector: " & Ex.Message & vbCrLf & Ex.StackTrace,
                   vbExclamation, "Err. no. " & Err.Number)
            Dim FileName As String = String.Empty
            Return FileName
        End Try

    End Function




    'Public Sub testResourceExtraction()

    '    Dim GameObj As GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube)
    '    Dim Animators As Object() = Resources.LoadAll("C:\Program Files (x86)\Shadowrun Returns\Shadowrun_Data", GetType(Animator))
    '    Dim textures As Object() = Resources.LoadAll("C:\Program Files (x86)\Shadowrun Returns\Shadowrun_Data", GetType(Texture2D))
    '    Dim Animations As Object() = Resources.LoadAll("C:\Program Files (x86)\Shadowrun Returns\Shadowrun_Data", GetType(Animation))
    '    Dim GameObjects As Object() = Resources.LoadAll("C:\Program Files (x86)\Shadowrun Returns\Shadowrun_Data", GetType(GameObject))
    '    Dim TextAssets As Object() = Resources.LoadAll("C:\Program Files (x86)\Shadowrun Returns\Shadowrun_Data", GetType(TextAsset))

    '    Dim str As String
    '    str = "adf"



    'End Sub


    Public Sub loadSave()
'        Dim FileName As String = "C:\Program Files (x86)\Steam\userdata\136575275\234650\remote\saves\6bee33a654c2454fa7be903f322e6296.sav"
'        Dim tempSaveGame As isogame.SaveGame

        Dim FileName As String = "C:\Program Files (x86)\Shadowrun Returns\Shadowrun_Data\StreamingAssets\ContentPacks\shadowrun_core\data\misc\trigger.tlib.bytes"
        Dim tempSaveGame As isogame.TsLibrary
        Using Fs As FileStream = File.Open(FileName, FileMode.Open)

            '    tempSaveGame = Serializer.Deserialize(Of isogame.SaveGame)(Fs)
            tempSaveGame = Serializer.Deserialize(Of isogame.TsLibrary)(Fs)

            'If AbilityList.FirstOrDefault(Function(C As isogame.AbilityDef) C.id = CurrentAbilityDefObject.id) Is Nothing Then
            '    AbilityList.Add(CurrentAbilityDefObject)
            'Else
            '    CurrentAbilityDefObject = AbilityList.FirstOrDefault(Function(C As isogame.AbilityDef) C.id = CurrentAbilityDefObject.id)
            'End If
            'Try
            '    If TryCast(TryCast(TabControl.SelectedItem, TabItem).Content, AbilityTabContent) IsNot Nothing Then
            '        TryCast(TryCast(TabControl.SelectedItem, TabItem).Content, AbilityTabContent).DataContext = CurrentAbilityDefObject
            '    End If
            'Catch
            'End Try

        End Using


        ' tempSaveGame.story_data.Item(0).party.Item(0).char_id = "test"

        For Each Tfunction As TsFunction In tempSaveGame.functions
            Dim str As String = Tfunction.name & ";" & Tfunction.hideInEditor.ToString() & ";" & Tfunction.params.Count.ToString() & ";"
            Dim str2 As String = ""
            For Each param As TsParam In Tfunction.params
                str2 = ""
                If param.defaultValue IsNot Nothing Then
                    If param.defaultValue.variableref_value IsNot Nothing Then
                        str2 += ";" & param.name.ToString & ";" & param.typeName.ToString & ";" & param.hint & ";" & param.defaultValue.ToString() & ";" & param.defaultValue.bool_value.ToString() & ";" & param.defaultValue.float_value.ToString() & ";" & param.defaultValue.int_value.ToString() & ";" & param.defaultValue.string_value & ";" & param.defaultValue.call_value.ToString() & ";" & param.defaultValue.call_value.functionName & ";" & param.defaultValue.variableref_value.ToString() & ";" & param.defaultValue.variableref_value.name & ";" & param.defaultValue.variableref_value.scope_name & ";" & param.defaultValue.variableref_value.typeName & ";" & param.defaultValue.variableref_value.parent_story.story_id & ";" & System.Enum.GetName(GetType(isogame.TsVariableScope), param.defaultValue.variableref_value.scope)
                    Else
                        If param.defaultValue.call_value IsNot Nothing Then

                            str2 += ";" & param.name.ToString & ";" & param.typeName.ToString & ";" & param.hint & ";" & param.defaultValue.ToString() & ";" & param.defaultValue.bool_value.ToString() & ";" & param.defaultValue.float_value.ToString() & ";" & param.defaultValue.int_value.ToString() & ";" & param.defaultValue.string_value & ";" & param.defaultValue.call_value.ToString() & ";" & param.defaultValue.call_value.functionName & ";" & "Nothing" & ";"

                            If param.defaultValue.call_value.args.Count > 0 Then
                                For Each arg As TsVariant In param.defaultValue.call_value.args
                                    Dim str3 = ""
                                    str3 = ";" & arg.int_value
                                Next
                            End If

                        Else
                            str2 += ";" & param.name.ToString & ";" & param.typeName.ToString & ";" & param.hint & ";" & param.defaultValue.ToString() & ";" & param.defaultValue.bool_value.ToString() & ";" & param.defaultValue.float_value.ToString() & ";" & param.defaultValue.int_value.ToString() & ";" & param.defaultValue.string_value & ";" & "Nothing" & ";"
                        End If

                    End If
                Else
                    str2 += ";" & param.name.ToString & ";" & param.typeName.ToString & ";" & param.hint & ";" & "Nothing" & ";"
                End If
                File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListTSFunctionIntriggersTLib.txt"), str + str2 + vbCrLf)
            Next
            If Tfunction.params.Count = 0 Then
                File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListTSFunctionIntriggersTLib.txt"), str + str2 + vbCrLf)
            End If
            File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListTSFunctionIntriggersTLib.txt"), vbCrLf)
        Next

        For Each TType As TsType In tempSaveGame.types
            Dim str As String = TType.name & ";" & TType.hideInEditor.ToString() & ";" & TType.baseTypeName & ";" & TType.group & ";" & TType.protobufPresetSource & ";" & TType.presets.Count.ToString()
            Dim str2 As String = ""
            For Each Preset As TsPresetValue In TType.presets
                str2 = ""
                If Preset.value.variableref_value IsNot Nothing Then
                    str2 += ";" & Preset.name & ";" & Preset.hint & ";" & Preset.value.ToString() & ";" & Preset.value.bool_value.ToString() & ";" & Preset.value.float_value.ToString() & ";" & Preset.value.int_value.ToString() & ";" & Preset.value.string_value & ";" & Preset.value.call_value.ToString() & ";" & Preset.value.call_value.functionName & ";" & Preset.value.variableref_value.ToString() & ";" & Preset.value.variableref_value.name & ";" & Preset.value.variableref_value.scope_name & ";" & Preset.value.variableref_value.typeName & ";" & Preset.value.variableref_value.parent_story.story_id & ";" & System.Enum.GetName(GetType(isogame.TsVariableScope), Preset.value.variableref_value.scope) & ";"
                Else
                    If Preset.value.call_value IsNot Nothing Then
                        str2 += ";" & Preset.name & ";" & Preset.hint & ";" & Preset.value.ToString() & ";" & Preset.value.bool_value.ToString() & ";" & Preset.value.float_value.ToString() & ";" & Preset.value.int_value.ToString() & ";" & Preset.value.string_value & ";" & Preset.value.call_value.ToString() & ";" & Preset.value.call_value.functionName & ";" & "Nothing" & ";"
                    Else
                        str2 += ";" & Preset.name & ";" & Preset.hint & ";" & Preset.value.ToString() & ";" & Preset.value.bool_value.ToString() & ";" & Preset.value.float_value.ToString() & ";" & Preset.value.int_value.ToString() & ";" & Preset.value.string_value & ";" & "Nothing" & ";"
                    End If
                End If


                File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListTSTypeIntriggersTLib.txt"), str + str2 + vbCrLf)
            Next
            If TType.presets.Count = 0 Then
                File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListTSTypeIntriggersTLib.txt"), str + str2 + vbCrLf)
            End If
            File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "ListTSTypeIntriggersTLib.txt"), vbCrLf)
        Next

    End Sub

    Public Sub printTlib()

        Dim FileName As String = "C:\Program Files (x86)\Steam\SteamApps\common\Shadowrun Returns\Shadowrun_Data\StreamingAssets\ContentPacks\shadowrun_core\data\misc\shadowruntriggers.tlib.bytes" '"C:\Program Files (x86)\Shadowrun Returns\Shadowrun_Data\StreamingAssets\ContentPacks\shadowrun_core\data\misc\trigger.tlib.bytes"
        Dim outfile As String = "ListShadowrunTriggersTLib.txt"  '"ListTriggersTLib.txt"

        Dim tlib As isogame.TsLibrary
        Using Fs As FileStream = File.Open(FileName, FileMode.Open)
            tlib = Serializer.Deserialize(Of isogame.TsLibrary)(Fs)
        End Using
   File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), outfile), "tlib.name = " & tlib.name + vbCrLf + vbCrLf + "---------------------------------------------" + vbCrLf + vbCrLf)
        For Each Tfunction As TsFunction In tlib.functions
            File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), outfile), printTsFunction(Tfunction) + vbCrLf + vbCrLf + "---------------------------------------------" + vbCrLf + vbCrLf)
        Next

        For Each TType As TsType In tlib.types
            File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), outfile), printTsType(TType) + vbCrLf + vbCrLf + "---------------------------------------------" + vbCrLf + vbCrLf)
        Next
    End Sub
    

    Public Function printTsFunction(ByVal Tfunction As TsFunction) As String
        printTsFunction = "TsFunction.name = " & Tfunction.name & vbCrLf
        printTsFunction += "TsFunction.group = " & Tfunction.group & vbCrLf
        printTsFunction += "TsFunction.grammarText = " & Tfunction.grammarText & vbCrLf
        printTsFunction += "TsFunction.tooltipText = " & Tfunction.tooltipText & vbCrLf
        printTsFunction += "TsFunction.returnTypeName = " & Tfunction.returnTypeName & vbCrLf
        printTsFunction += "TsFunction.hideInEditor = " & Tfunction.hideInEditor & vbCrLf
        If Tfunction.params IsNot Nothing Then
            If Tfunction.params.Count > 0 Then
                For i As Integer = 0 To Tfunction.params.Count - 1
                    printTsFunction += "TsType.presets_" & i.ToString() & " = " & vbCrLf & "{" & vbCrLf & printTsParam(Tfunction.params(i), 1) & "}" & vbCrLf
                Next
            End If
        End If
        Return printTsFunction
    End Function


    Public Function printTsType(ByVal Type As TsType) As String
        printTsType = "TsType.name = " & Type.name & vbCrLf
        printTsType += "TsType.group = " & Type.group & vbCrLf
        printTsType += "TsType.baseTypeName = " & Type.baseTypeName & vbCrLf
        printTsType += "TsType.group = " & Type.group & vbCrLf
        printTsType += "TsType.hideInEditor = " & Type.hideInEditor & vbCrLf
        printTsType += "TsType.protobufPresetSource = " & Type.protobufPresetSource & vbCrLf
        If Type.presets IsNot Nothing Then
            If Type.presets.Count > 0 Then
                For i As Integer = 0 To Type.presets.Count - 1
                    printTsType += "TsType.presets_" & i.ToString() & " = " & vbCrLf & "{" & vbCrLf & printTsPreset(Type.presets(i), 1) & "}" & vbCrLf
                Next
            End If
        End If
        Return printTsType
    End Function

    Public Function printTsPreset(ByVal Preset As TsPresetValue, IndentLevel As Integer) As String
        Dim indentstr As String = ""
        For i As Integer = 1 To IndentLevel
            indentstr += vbTab
        Next

        printTsPreset = indentstr & "TsPresetValue.name = " & Preset.name & vbCrLf
        printTsPreset += indentstr & "TsPresetValue.hint = " & Preset.hint & vbCrLf
        If Preset.value IsNot Nothing Then
            printTsPreset += indentstr & "TsPresetValue.value = " & vbCrLf & indentstr & "{" & vbCrLf & printTsVariant(Preset.value, IndentLevel + 1) & indentstr & "}" & vbCrLf
        End If
        Return printTsPreset
    End Function


    Public Function printTsParam(ByVal Param As TsParam, IndentLevel As Integer) As String
        Dim indentstr As String = ""
        For i As Integer = 1 To IndentLevel
            indentstr += vbTab
        Next

        printTsParam = indentstr & "TsParam.name = " & Param.name & vbCrLf
        printTsParam += indentstr & "TsParam.typeName = " & Param.typeName & vbCrLf
        printTsParam += indentstr & "TsParam.hint = " & Param.hint & vbCrLf
        If Param.defaultValue IsNot Nothing Then
            printTsParam += indentstr & "TsParam.defaultValue = " & vbCrLf & indentstr & "{" & vbCrLf & printTsVariant(Param.defaultValue, IndentLevel + 1) & indentstr & "}" & vbCrLf
        End If
        Return printTsParam
    End Function

    Public Function printTsVariant(ByVal TsVar As TsVariant, IndentLevel As Integer) As String
        Dim indentstr As String = ""
        For i As Integer = 1 To IndentLevel
            indentstr += vbTab
        Next

        printTsVariant = indentstr & "TsVariant.bool_value = " & TsVar.bool_value & vbCrLf
        printTsVariant += indentstr & "TsVariant.float_value = " & TsVar.float_value & vbCrLf
        printTsVariant += indentstr & "TsVariant.int_value = " & TsVar.int_value & vbCrLf
        printTsVariant += indentstr & "TsVariant.string_value = " & TsVar.string_value & vbCrLf
        If TsVar.variableref_value IsNot Nothing Then
            printTsVariant += indentstr & "TsVariant.variableref_value.name = " & TsVar.variableref_value.name & vbCrLf
            printTsVariant += indentstr & "TsVariant.variableref_value = " & vbCrLf & indentstr & "{" & vbCrLf & printTsVarRef(TsVar.variableref_value, IndentLevel + 1) & indentstr & "}" & vbCrLf
        End If
        If TsVar.call_value IsNot Nothing Then
            printTsVariant += indentstr & "TsVariant.call_value = " & vbCrLf & indentstr & "{" & vbCrLf & printTsCall(TsVar.call_value, IndentLevel + 1) & indentstr & "}" & vbCrLf
        End If
        Return printTsVariant
    End Function

    Public Function printTsCall(ByVal TsCal As TsCall, IndentLevel As Integer) As String
        Dim indentstr As String = ""
        For i As Integer = 1 To IndentLevel
            indentstr += vbTab
        Next

        printTsCall = indentstr & "TsCall.functionName = " & TsCal.functionName & vbCrLf
        If TsCal.args.Count > 0 Then
            For i As Integer = 0 To TsCal.args.Count - 1
                printTsCall += indentstr & "TsCall.args_" & i.ToString() & " = " & vbCrLf & indentstr & "{" & vbCrLf & printTsVariant(TsCal.args(i), IndentLevel + 1) & indentstr & "}" & vbCrLf
            Next
        End If
        Return printTsCall
    End Function

    Public Function printTsVarRef(ByVal TsVarRef As TsVariableRef, IndentLevel As Integer) As String
        Dim indentstr As String = ""
        For i As Integer = 1 To IndentLevel
            indentstr += vbTab
        Next

        printTsVarRef = indentstr & "TsVariableRef.name = " & TsVarRef.name & vbCrLf
        printTsVarRef += indentstr & "TsVariableRef.typeName = " & TsVarRef.typeName & vbCrLf
        printTsVarRef += indentstr & "TsVariableRef.scope_name = " & TsVarRef.scope_name & vbCrLf
        printTsVarRef += indentstr & "TsVariableRef.scope = " & System.Enum.GetName(GetType(isogame.TsVariableScope), TsVarRef.scope) & vbCrLf
        If TsVarRef.parent_story IsNot Nothing Then
            printTsVarRef += indentstr & "TsVariableRef.parent_story.account_id = " & TsVarRef.parent_story.account_id & vbCrLf
            printTsVarRef += indentstr & "TsVariableRef.parent_story.content_pack_id = " & TsVarRef.parent_story.content_pack_id & vbCrLf
            printTsVarRef += indentstr & "TsVariableRef.parent_story.story_id = " & TsVarRef.parent_story.story_id & vbCrLf
        End If
        Return printTsVarRef

    End Function

    'Public Sub GetManifests()
    '    Dim assetbundleman As New AssetBundleManager
    '    AssetBundleManager.GetBundleManifest()
    '    AssetBundleManager.LoadBundleAsync("C:\Program Files (x86)\Steam\SteamApps\common\Shadowrun Returns\Shadowrun_Data\StreamingAssets\win\items.assetbundle", AddressOf BundleLoaded)
    'End Sub

 Public Function parseProtoBufTextFormat(ByVal ProtoBufText As String(), ByVal Objecttype As Object) As Object

        'object type to pass into the code
        '   Dim objecttype As New AbilityDef

        Dim PropInfoList As New List(Of PropertyInfo)
        Dim ProtoBufText2 As New List(Of String)
        Dim child As New Object

        For Each line As String In ProtoBufText
            'if a line contains ":"  then we're dealing with a property
            'write value to property.
            Dim newValue As Object
            If line.Contains(":") Then
                'setting type info
                Dim PropInfo As PropertyInfo

                If PropInfoList.Count = 0 Then
                    PropInfo = Objecttype.GetType().GetProperty(line.Split(CChar(":"))(0).Replace(vbTab, ""))

                    If PropInfo.PropertyType = GetType(String) Then
                        newValue = line.Split(CChar(":"))(1).Substring(2).Replace(ControlChars.Quote, "").Trim()

                    ElseIf PropInfo.PropertyType.IsEnum Then
                        newValue = [Enum].Parse(PropInfo.PropertyType, line.Split(CChar(":"))(1).Substring(1))
                    Else
                        newValue = line.Split(CChar(":"))(1).Substring(1).Trim()
                    End If
                    PropInfo.SetValue(Objecttype, newValue)
                Else
                    ProtoBufText2.Add(line)
                End If

            ElseIf line.Contains("{") Then
                'Starting subobject
                PropInfoList.Add(Objecttype.GetType().GetProperty(line.Split(CChar("{"))(0).Replace(vbTab, "").Trim()))
                child = Objecttype.GetType().GetProperty(line.Split(CChar("{"))(0).Replace(vbTab, "").Trim()).GetValue(Objecttype, Nothing)
                If child Is Nothing Then
                    child = Activator.CreateInstance(Objecttype.GetType().GetProperty(line.Split(CChar("{"))(0).Replace(vbTab, "").Trim()).PropertyType)

                    Objecttype.GetType().GetProperty(line.Split(CChar("{"))(0).Replace(vbTab, "").Trim()).SetValue(Objecttype, child, Nothing)
                End If

            ElseIf line.Contains("}") Then
                If child IsNot Nothing Then
                    PropInfoList(PropInfoList.Count - 1).SetValue(Objecttype, parseProtoBufTextFormat(ProtoBufText2.ToArray(), child))
                    PropInfoList.RemoveAt(PropInfoList.Count - 1)
                End If
            End If

        Next

        Return Objecttype

    End Function

    Public Sub startParse()

        Dim filename As String = "C:\Users\Mischa Vreeburg\Documents\Projects\ShadowrunReturnsItemEditor\Txt resource files\Street Samurai Catalog v3g-18-3g\Street Samurai Catalog Data\data\abilities\conjure heal barrier.ab.txt"
        'object type to pass into the code
        Dim objecttype As New AbilityDef
        Dim ProtoBufText As String()
        ProtoBufText = File.ReadAllLines(filename)

        objecttype = CType(parseProtoBufTextFormat(ProtoBufText, objecttype), AbilityDef)

        Dim testId As String = objecttype.id

        Dim textFormatstring As String = ToProtoText(objecttype.GetType(), objecttype, 0)

        File.AppendAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "testfile.txt"), textFormatstring)

    End Sub


    Public Function ToProtoText(type As System.Type, deserialzedObject As Object, indentLevel As Integer) As String

        Dim properties As System.Reflection.PropertyInfo() = type.GetProperties(System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.Public)
        
        Dim protomemberNumber As Integer = 100

        Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()

        For Each current As System.Reflection.PropertyInfo In properties.OrderBy(Function(x As System.Reflection.PropertyInfo)
                                                                                     If x.GetCustomAttributes(True)(0) IsNot Nothing AndAlso TryCast(x.GetCustomAttributes(True)(0), ProtoMemberAttribute) IsNot Nothing Then
                                                                                         Return CType(x.GetCustomAttributes(True)(0), ProtoMemberAttribute).Tag
                                                                                     ElseIf x.GetCustomAttributes(True).Count > 1 Then
                                                                                         If x.GetCustomAttributes(True)(1) IsNot Nothing AndAlso TryCast(x.GetCustomAttributes(True)(1), ProtoMemberAttribute) IsNot Nothing Then
                                                                                             Return CType(x.GetCustomAttributes(True)(1), ProtoMemberAttribute).Tag
                                                                                         Else
                                                                                             protomemberNumber += 1
                                                                                             Return protomemberNumber
                                                                                         End If
                                                                                     Else
                                                                                         protomemberNumber += 1
                                                                                         Return protomemberNumber
                                                                                     End If
                                                                                 End Function)
            protomemberNumber += 1

            Dim ProtoMemberAttr As ProtoMemberAttribute
            Dim DefaultValue As Object
            Dim DefaultValuePresent As Boolean = False
            If current.GetCustomAttributes(True)(0) IsNot Nothing AndAlso TryCast(current.GetCustomAttributes(True)(0), ProtoMemberAttribute) IsNot Nothing Then
                ProtoMemberAttr = CType(current.GetCustomAttributes(True)(0), ProtoMemberAttribute)
                If current.GetCustomAttributes(True).Count > 1 Then
                    DefaultValue = current.GetCustomAttributes(True)(1)
                    DefaultValuePresent = True
                End If
            ElseIf current.GetCustomAttributes(True).Count > 1 Then
                If current.GetCustomAttributes(True)(1) IsNot Nothing AndAlso TryCast(current.GetCustomAttributes(True)(1), ProtoMemberAttribute) IsNot Nothing Then
                    ProtoMemberAttr = CType(current.GetCustomAttributes(True)(1), ProtoMemberAttribute)
                    DefaultValue = current.GetCustomAttributes(True)(0)
                    DefaultValuePresent = True
                End If
            End If

            Dim name As String = current.Name
            Dim obj As Object = If((deserialzedObject Is Nothing), Nothing, current.GetValue(deserialzedObject, Nothing))

            If DefaultValue Is Nothing OrElse TryCast(DefaultValue, System.ComponentModel.DefaultValueAttribute).Value IsNot Nothing Then
                If DefaultValue Is Nothing OrElse Not DirectCast(DefaultValue, System.ComponentModel.DefaultValueAttribute).Value.Equals(obj) Then

                    If obj IsNot Nothing AndAlso GetType(System.Collections.IEnumerable).IsAssignableFrom(current.PropertyType) AndAlso current.PropertyType IsNot GetType(String) Then
                        For Each current2 As Object In CType(obj, System.Collections.IEnumerable)
                            Dim type2 As System.Type = current2.GetType()
                            stringBuilder.Append(getProtoTextFromTypeAndValue(type2, indentLevel, name, current2))
                        Next
                    Else
                        stringBuilder.Append(getProtoTextFromTypeAndValue(current.PropertyType, indentLevel, name, obj))
                    End If
                End If
            Else
                If obj IsNot Nothing AndAlso GetType(System.Collections.IEnumerable).IsAssignableFrom(current.PropertyType) AndAlso current.PropertyType IsNot GetType(String) Then
                    For Each current2 As Object In CType(obj, System.Collections.IEnumerable)
                        Dim type2 As System.Type = current2.GetType()
                        stringBuilder.Append(getProtoTextFromTypeAndValue(type2, indentLevel, name, current2))
                    Next
                Else
                    stringBuilder.Append(getProtoTextFromTypeAndValue(current.PropertyType, indentLevel, name, obj))
                End If

            End If

        Next
            Return stringBuilder.ToString()
    End Function

    Private Function getProtoTextFromTypeAndValue(type As System.Type, indentLevel As Integer, propertyDisplayName As String, value As Object) As String
        Dim result As String
        If value IsNot Nothing AndAlso GetType(System.Collections.IEnumerable).IsAssignableFrom(type) AndAlso type IsNot GetType(String) Then
            Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
            For Each current As Object In CType(value, System.Collections.IEnumerable)
                Dim type2 As System.Type = current.GetType()
                stringBuilder.Append(getProtoTextFromTypeAndValue(type2, indentLevel, propertyDisplayName, current))
            Next
            result = stringBuilder.ToString()
        Else
            Dim obj As Object
            If type.IsClass AndAlso type IsNot GetType(String) AndAlso value IsNot Nothing Then
                obj = String.Format("{{{0}{2}{1}}}", System.Environment.NewLine, New String(CChar(" "), indentLevel * 2), ToProtoText(type, value, indentLevel + 1))
            Else
                If type Is GetType(String) Then
                    obj = String.Format("""{0}""", value)
                Else
                    obj = value
                End If
            End If
            If (obj Is Nothing OrElse (type Is GetType(String) AndAlso obj.ToString().Trim(New Char() {CChar("""")}) = "")) Then
                result = Nothing
            Else
                result = String.Format("{0}{1}: {2}{3}", New Object() {New String(CChar(" "), indentLevel * 2), propertyDisplayName, obj, System.Environment.NewLine})
            End If
        End If
        Return result
    End Function

End Module
