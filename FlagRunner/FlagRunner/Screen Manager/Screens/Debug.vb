'Just a simple display debug menu
'Shouldn't do this, but can have universal input collected here, we'll have it
'Always be lurking
Public Class Debug
    Inherits BaseScreen

    Public Screens As String = "" 'This is a list of active screens
    Public focusScreen As String = "" 'Initializing to prevent error where f1 would cause a crash if somehow there were no screens

    Private fps As Integer 'Frames per second
    Private fpsCounter As Integer
    Private fpsTimer As Double
    Private fpsText As String = ""

    Private BGRect As Rectangle

    Public Sub New()
        Name = "Debug"
        state = ScreenState.Hidden 'We don't want this to be seen
        GrabFocus = False
    End Sub

    Public Overrides Sub HandleInput()
        MyBase.HandleInput()
        'Global key commands.

        'Toggle visibility on F1
        If Input.KeyPressed(Keys.F1) Then
            If state = ScreenState.Active Then
                state = ScreenState.Hidden
            ElseIf state = ScreenState.Hidden Then
                state = ScreenState.Active
            End If
        End If


        'debug sound info. Having it on the omnipresernt Debug screen means it can be toggled from anywhere
        'Right now set to Z
        If Input.KeyPressed(Keys.Z) Then
            If SoundManager.SoundOn Then
                SoundManager.Pause()
            ElseIf SoundManager.soundOn = False Then
                SoundManager.ResumeMusic()
            End If
        End If

        'Add next global command here

    End Sub

    Public Overrides Sub Update()
        MyBase.Update()

        'Truncate the last comma and space
        If Screens.Length > 1 Then
            Screens = Screens.Substring(0, Screens.Length - 2)
        End If

        Dim txtWidth As Integer = 0
        Dim txtHeight As Integer = 0

        If Fonts.Centaur_10.MeasureString(Screens).X > txtWidth Then
            txtWidth = Fonts.Centaur_10.MeasureString(Screens).X
        End If
        If Fonts.Centaur_10.MeasureString(focusScreen).X > txtWidth Then
            txtWidth = Fonts.Centaur_10.MeasureString(focusScreen).X
        End If

        txtHeight = Fonts.Centaur_10.MeasureString(fpsText).Y * 3
        BGRect = New Rectangle(0, 0, txtWidth + 20, txtHeight + 20)

    End Sub

    Public Overrides Sub Draw()
        MyBase.Draw()

        'Count the frames per second
        If Globals.GameTime.TotalGameTime.TotalMilliseconds > fpsTimer Then
            fps = fpsCounter
            fpsTimer = Globals.GameTime.TotalGameTime.TotalMilliseconds + 1000
            fpsCounter = 1
            fpsText = "FPS: " & fps
        Else
            fpsCounter += 1
        End If


        Globals.SpriteBatch.Begin()
        Globals.SpriteBatch.DrawString(Fonts.Centaur_10, fpsText, New Vector2(10, 10), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Centaur_10, Screens, New Vector2(10, 22), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Centaur_10, focusScreen, New Vector2(10, 34), Color.White)
        Globals.SpriteBatch.End()

    End Sub

End Class
