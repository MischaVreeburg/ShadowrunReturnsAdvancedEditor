Imports System
Imports System.Reflection.Assembly
Imports System.IO

Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.
    Public WithEvents currentDomain As AppDomain = AppDomain.CurrentDomain
    Public WindowSRRItemEditor As New WindowSRRItemEditor

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.
    Public Sub App_Startup(ByVal Sender As Object, ByVal E As StartupEventArgs)



        WindowSRRItemEditor.Show()

    End Sub
    

    Private Shared Function app_domain_AssemblyResolve(
 sender As Object, args As ResolveEventArgs) As System.Reflection.Assembly Handles currentDomain.AssemblyResolve
        '.AppDomain.AssemblyResolve()

        Dim aaa As String = args.Name
        Dim _asm As String() = args.Name.Split(",".ToCharArray())
        Dim currDir As String = Directory.GetCurrentDirectory
        Dim ParentDir As String = Directory.GetParent(currDir).FullName

        Dim _asmname As String = ParentDir & "\Shadowrun_Data\Managed\" + _asm(0) + ".dll"
        Dim myassembly As System.Reflection.Assembly = Nothing
        Try
            myassembly = System.Reflection.Assembly.LoadFrom(_asmname)
        Catch ex As Exception
        End Try
        Return myassembly
    End Function

End Class
