Public Enum GameMode
    CTF
    Deathmatch
End Enum

Public Class Game1
    Inherits Microsoft.Xna.Framework.Game

    'Screen size needs to be tilesize * size of TileList
    Public Const GAME_SIZE_X As Integer = 1224, GAME_SIZE_Y As Integer = 1224
    Private ScreenManager As ScreenManager
    Public Shared GameMode As GameMode

    Public Sub New()
        Globals.Graphics = New GraphicsDeviceManager(Me)
        Content.RootDirectory = "Content"
    End Sub

    Protected Overrides Sub Initialize()
        Me.IsMouseVisible = False 'We don't want the mouse
        Window.AllowUserResizing = True 'Errr, we may regret this?

        Globals.GameSize = New Vector2(GAME_SIZE_X, GAME_SIZE_Y)
        Globals.Graphics.PreferredBackBufferWidth = Globals.GameSize.X
        Globals.Graphics.PreferredBackBufferHeight = Globals.GameSize.Y
        Globals.Graphics.ApplyChanges()

        Globals.BackBuffer = New RenderTarget2D(Globals.Graphics.GraphicsDevice, Globals.GameSize.X, Globals.GameSize.Y, False, SurfaceFormat.Color, RenderTargetUsage.PreserveContents)
        MyBase.Initialize()
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
        ' Allows the game to exit
        If GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed Then
            Me.Exit()
        End If

        'Update logic
        MyBase.Update(gameTime)
        Globals.WindowFocused = Me.IsActive
        Globals.GameTime = gameTime

        'update screens
        ScreenManager.update()

        'Add screen here. Todo?

        'Input
        Input.update()
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
