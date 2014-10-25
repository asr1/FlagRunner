Public Enum ScreenState
    Active
    ShutDown
    Hidden
End Enum

Public Class ScreenManager
    Public Shared Screens As New List(Of BaseScreen) 'List of scren
    Private Shared NewScreens As New List(Of BaseScreen) 'Sscreen to be added
    'Use so we can remove w/o affecting index numbers
    Private debugScreen As New Debug()

    Public Sub New()
        AddScreen(debugScreen)

        'TODO: UNCOMMENT OUT THIS LINE BEFORE RELEASE
        ' SoundManager.Play()
    End Sub

    Public Sub Update()
        debugScreen.Screens = "Screens: "

        'generate list of dead screen for removal
        Dim RemoveScreens As New List(Of BaseScreen)

        'For loops for days

        For Each FoundScreen As BaseScreen In Screens
            If FoundScreen.State = ScreenState.ShutDown Then
                RemoveScreens.Add(FoundScreen)
            Else
                debugScreen.Screens += FoundScreen.Name + ", "
                FoundScreen.Focused = False
            End If
        Next

        'Now remove
        'Two lists so we don't delete one and go out of bounds
        For Each FoundScreen As BaseScreen In RemoveScreens
            Screens.Remove(FoundScreen)
        Next

        'Add new screens to manager list
        For Each FoundScreen As BaseScreen In NewScreens
            Screens.Add(FoundScreen)
        Next
        NewScreens.Clear()

        'Reset debug screen to top of list
        Screens.Remove(debugScreen)
        Screens.Add(debugScreen)

        'This time we don't need absolute values,
        'So we can get by with indices so we'll count backwards
        'So we can use only one list
        If Screens.Count > 0 Then
            For i = Screens.Count - 1 To 0 Step -1
                If Screens(i).GrabFocus Then
                    Screens(i).Focused = True
                    debugScreen.focusScreen = "Focused Screen: " & Screens(i).Name
                    Exit For 'We only want one focused screen
                End If
            Next
        End If

        'Handle input for focus screen
        For Each FoundScreen As BaseScreen In Screens
            If Globals.WindowFocused Then
                FoundScreen.HandleInput()
            End If
            FoundScreen.Update()
        Next
    End Sub

    Public Sub Draw()
        'Draw all the active screens
        For Each FoundScreen As BaseScreen In Screens
            If FoundScreen.State = ScreenState.Active Then
                FoundScreen.Draw()
            End If
        Next
    End Sub

    Public Shared Sub AddScreen(Screen As BaseScreen)
        NewScreens.Add(Screen)
    End Sub

    Public Shared Sub UnloadScreen(Screen As String)
        For Each FoundScreen As BaseScreen In Screens
            If FoundScreen.Name = Screen Then
                FoundScreen.Unload()
                Exit For
            End If
        Next
    End Sub

End Class
