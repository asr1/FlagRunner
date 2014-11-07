Public Enum GameMode
    CTF 'capture the flag 
    Deathmatch 'First to x kills wins. 
    LastManStanding 'Last one alive wins
    Nightlight 'All is dark. Light sources hurt you.
End Enum

Public Class Game1
    Inherits Microsoft.Xna.Framework.Game

    Public KrypEng As New KryptonEngine(Me, "KryptonEffect")


    Public Shared isPaused As Boolean = False
    Private Shared isOver As Boolean = False
    Public Shared ShouldExit As Boolean = False 'If we get an exit signal.

    'Screen size needs to be tilesize * size of Tilelist
    Public Const GAME_SIZE_X As Integer = 1224, GAME_SIZE_Y As Integer = 1224
    Private ScreenManager As ScreenManager
    Private Shared GameMode As GameMode

    Public Shared Function GetGameMode() As GameMode
        Return GameMode
    End Function

    Public Shared Sub SetGameMode(newMode As GameMode)
        GameMode = newMode
    End Sub


    Public Sub New()
        Globals.Graphics = New GraphicsDeviceManager(Me)
        Content.RootDirectory = "Content"
    End Sub

    Protected Overrides Sub Initialize()
        Me.IsMouseVisible = False 'We don't want the mouse
        Window.AllowUserResizing = True 'Errr, we may regret this?
        Globals.KrypEng = Me.KrypEng
        Globals.KrypEng.Initialize()

        Globals.GameSize = New Vector2(GAME_SIZE_X, GAME_SIZE_Y)
        Globals.Graphics.PreferredBackBufferWidth = Globals.GameSize.X
        Globals.Graphics.PreferredBackBufferHeight = Globals.GameSize.Y
        Globals.Graphics.ApplyChanges()

        Globals.BackBuffer = New RenderTarget2D(Globals.Graphics.GraphicsDevice, Globals.GameSize.X, Globals.GameSize.Y)
        ' Globals.BackBuffer = New RenderTarget2D(Globals.Graphics.GraphicsDevice, Globals.GameSize.X, Globals.GameSize.Y, False, SurfaceFormat.Color, RenderTargetUsage.PreserveContents)
        MyBase.Initialize()
    End Sub

    'Begin pause
    Private Shared Sub BeginPause()
        isPaused = True
        SoundManager.Pause()

        'Stop any vibrartion
        GamePad.SetVibration(PlayerIndex.One, 0, 0)
        GamePad.SetVibration(PlayerIndex.Two, 0, 0)
        GamePad.SetVibration(PlayerIndex.Three, 0, 0)
        GamePad.SetVibration(PlayerIndex.Four, 0, 0)



    End Sub

    Private Sub Reset()
        GameMode = Nothing
        Globals.KrypEng.Lights.Clear()
    End Sub

    'End pause
    Private Shared Sub EndPause()
        SoundManager.Play()
        isPaused = False
    End Sub

    'Check for pause action. If start, pause. 
    'If already paused, unpause. Anyone can unpause.
    Public Shared Sub CheckForPause()
        If Input.ButtonPressed(Buttons.Start, PlayerIndex.One) Or Input.ButtonPressed(Buttons.Start, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.Start, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.Start, PlayerIndex.Four) Then
            If isPaused = False Then
                BeginPause()
            Else
                EndPause()
            End If
        End If
    End Sub

    'LoadContent will be called once per game and is the place to load
    'all of your content.
    Protected Overrides Sub LoadContent()
        ' Create a new SpriteBatch, which can be used to draw textures.
        Globals.SpriteBatch = New SpriteBatch(GraphicsDevice)
        Globals.Content = Me.Content

        'loading fonts, textures and soudns
        Fonts.load()
        Textures.Load()
        Sounds.load()

        'Load Weapons
        Options.InitializeItems()

        'Add default screens
        ScreenManager = New ScreenManager
        ScreenManager.AddScreen(New TitleScreen)
        ScreenManager.AddScreen(New MainMenu)


    End Sub

    ' UnloadContent will be called once per game and is the place to unload
    ' all content.
    Protected Overrides Sub UnloadContent()
        ' TODO: Unload any non ContentManager content here
        'Could delete this entire sub.
    End Sub

    'Allows the game to run logic such as updating the world,
    'checking for collisions, gathering input, and playing audio.
    Protected Overrides Sub Update(ByVal gameTime As GameTime)
        'Input
        Input.Update()

        ' Allows the game to exit
        If ShouldExit = True Then
            Me.Exit()
        End If

        If Utilities.CheckForWin = True And MazeScreen.Initialized = True Then
            isPaused = True
        End If

        'TODO: Fix pausing

        'Don't update this frame if we're paused.
        If isPaused Then

            'Exit this game and start a new one.
            If Input.ButtonDown(Buttons.X, PlayerIndex.One) And Input.ButtonDown(Buttons.LeftTrigger, PlayerIndex.One) Or Input.ButtonDown(Buttons.X, PlayerIndex.Two) And Input.ButtonDown(Buttons.LeftTrigger, PlayerIndex.Two) Or Input.ButtonDown(Buttons.X, PlayerIndex.Three) And Input.ButtonDown(Buttons.LeftTrigger, PlayerIndex.Three) Or Input.ButtonDown(Buttons.X, PlayerIndex.Four) And Input.ButtonDown(Buttons.LeftTrigger, PlayerIndex.Four) Then
                ScreenManager.UnloadScreen("MazeScreen")
                ScreenManager.AddScreen(New TitleScreen)
                ScreenManager.AddScreen(New MainMenu)
                Player.reset()
                EndPause()
                Reset()
            End If


            'Draw outside the draw() sub
            '  Globals.Graphics.GraphicsDevice.SetRenderTarget(Nothing)
            Globals.SpriteBatch.Begin()
            'Redraw the background
            Globals.SpriteBatch.Draw(Globals.BackBuffer, New Rectangle(0, 0, Globals.Graphics.GraphicsDevice.Viewport.Width, Globals.Graphics.GraphicsDevice.Viewport.Height), Color.White)

            'Pause will behave the same as exit
            If isOver = True Then
                Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "Victory by " & Utilities.GetWinner, New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("Victory By Player 1").X, Globals.Graphics.GraphicsDevice.Viewport.Height / 2), Color.White)
            Else
                Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "PAUSED", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("PAUSED").X, Globals.Graphics.GraphicsDevice.Viewport.Height / 2), Color.White)
            End If
            Globals.SpriteBatch.DrawString(Fonts.Centaur_10, "Press L and X to exit", New Vector2(Globals.GameSize.X / 2 - Fonts.Centaur_10.MeasureString("Press L and X to exit").X, Globals.Graphics.GraphicsDevice.Viewport.Height / 100 * 55), Color.White)

            Globals.SpriteBatch.End()
            Globals.Graphics.GraphicsDevice.Present()

            CheckForPause()
            ' PauseScreen.HandlePauseScreen()
            Return
        End If

        'Update logic
        MyBase.Update(gameTime)
        Globals.WindowFocused = Me.IsActive
        Globals.GameTime = gameTime

        'update screens
        ScreenManager.Update()

        'Add screen here. Todo?


        'Skip once.
        If MazeScreen.Initialized = True Then
            isOver = Utilities.CheckForWin
        End If
    End Sub

    'This is called when the game should draw itself.
    Protected Overrides Sub Draw(ByVal gameTime As GameTime)
        Globals.Graphics.GraphicsDevice.SetRenderTarget(Globals.BackBuffer)
        GraphicsDevice.Clear(Color.Black)
        MyBase.Draw(gameTime)

        ScreenManager.draw()
        Globals.Graphics.GraphicsDevice.SetRenderTarget(Nothing)

        'Draw backbuffer to screen
        Globals.SpriteBatch.Begin()
        Globals.SpriteBatch.Draw(Globals.BackBuffer, New Rectangle(0, 0, Globals.Graphics.GraphicsDevice.Viewport.Width, Globals.Graphics.GraphicsDevice.Viewport.Height), Color.White)
        Globals.SpriteBatch.End()



    End Sub

End Class
