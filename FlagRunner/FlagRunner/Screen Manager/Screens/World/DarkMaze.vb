'This is a mazescreen with no lighting.

Public Class DarkMaze
    Inherits MazeScreen

    Public Sub New()
        Name = "DarkMaze"
    End Sub

    Public Sub createLights()
        Dim light As New Light2D
        light.Texture = Textures.LightTexture
        light.Range = 10
        light.Color = Color.Yellow
        light.Intensity = 1
        light.Angle = 250
        light.Position = New Vector2(0, 0)
        light.IsOn = True

        Globals.KrypEng.Lights.Add(light)
    End Sub

    Public Sub CreateHulls()
        Dim hull = ShadowHull.CreateRectangle(New Vector2(1, 1))
        hull.Position.X = 10
        hull.Position.Y = 10

        Globals.KrypEng.Hulls.Add(hull)
    End Sub

    Public Overrides Sub Draw()
  

        Dim World As Matrix = Matrix.Identity
        Dim View As Matrix = Matrix.CreateTranslation(New Vector3(0, 0, 0) * -1)
        Dim Projection As Matrix = Matrix.CreateOrthographic(Globals.GameSize.Y * Globals.Graphics.GraphicsDevice.Viewport.AspectRatio, Globals.GameSize.Y, 0, 1)

        Globals.KrypEng.Matrix = World * View * Projection
        Globals.KrypEng.Bluriness = 3
        Globals.KrypEng.LightMapPrepare()


        createLights()
        '   CreateHulls()
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
