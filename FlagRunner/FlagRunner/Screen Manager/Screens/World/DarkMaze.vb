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
    End Sub

    Public Overrides Sub Draw()
        MyBase.Draw()
        Dim World As Matrix = Matrix.Identity
        Dim View As Matrix = Matrix.CreateTranslation(New Vector3(0, 0, 0) * -1)
        Dim Projection As Matrix = Matrix.CreateOrthographic(Globals.GameSize.Y * Globals.Graphics.GraphicsDevice.Viewport.AspectRatio, Globals.GameSize.Y, 0, 1)

        Globals.KrypEng.Matrix = World * View * Projection
        Globals.KrypEng.Bluriness = 3
        Globals.KrypEng.LightMapPrepare()

        Globals.Graphics.GraphicsDevice.Clear(Color.Black)



        Globals.SpriteBatch.Begin()
        'Globals.SpriteBatch.Draw(light, New Rectangle(0, 0, 10, 10), Color.White)
        Globals.SpriteBatch.End()
    End Sub

End Class
