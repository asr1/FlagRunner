'This is a mazescreen with no lighting.

Public Class DarkMaze
    Inherits MazeScreen

    Public Sub New()
        Name = "DarkMaze"
    End Sub

    Public Sub createLights()
        Dim light As New Light2D
        light.Texture = Textures.LightTexture
        light.Range = 15
        light.Color = Color.White
        light.Intensity = 1
        light.Angle = 250
        light.Position = New Vector2(100, -48)
        light.IsOn = True

        Globals.KrypEng.Lights.Add(light)
    End Sub

    'I think this is a light blocker. That means
    'We want one on every wall,
    Public Sub CreateHulls()
        Dim hull = ShadowHull.CreateRectangle(New Vector2(1, 1))
        hull.Position.X = 0
        hull.Position.Y = 5
        Globals.KrypEng.Hulls.Add(hull)
    End Sub

    Public Overrides Sub Draw()

        'Possible way to fix coordinates.
        '   Globals.KrypEng.SpriteBatchCompatablityEnabled = True


        'Other way to fix coordinates.
        Dim World As Matrix = Matrix.Identity
        'Dim View As Matrix = Matrix.CreateTranslation(New Vector3(0, 0, 0) * -1)
        'Dim Projection As Matrix = Matrix.CreateOrthographic(Globals.GameSize.Y * Globals.Graphics.GraphicsDevice.Viewport.AspectRatio, Globals.GameSize.Y, 0, 1)
        Dim View As Matrix = Matrix.CreateTranslation(New Vector3(Globals.Graphics.PreferredBackBufferWidth / 2, -Globals.Graphics.PreferredBackBufferHeight / 2, 0) * -1.0F)
        Dim Projection As Matrix = Matrix.CreateOrthographic(Globals.Graphics.PreferredBackBufferWidth, Globals.Graphics.PreferredBackBufferHeight, 0, 1)


        Globals.KrypEng.AmbientColor = Color.Black

        Globals.KrypEng.Matrix = World * View * Projection
        Globals.KrypEng.Bluriness = 1
        Globals.KrypEng.LightMapPrepare()



        createLights()
        ' CreateHulls()
        MyBase.Draw()


        Globals.KrypEng.Draw(Globals.GameTime)
        For Each l As Light2D In Globals.KrypEng.Lights
            Globals.KrypEng.RenderHelper.BufferAddBoundOutline(l.Bounds)
        Next


        'Globals.Graphics.GraphicsDevice.Clear(Color.White)
        'Globals.Graphics.GraphicsDevice.SetRenderTarget(Nothing)
        'Globals.Graphics.GraphicsDevice.Present()


        '     Globals.SpriteBatch.Begin()
        'Globals.SpriteBatch.Draw(light, New Rectangle(0, 0, 10, 10), Color.White)
        '   Globals.SpriteBatch.End()
    End Sub

End Class
