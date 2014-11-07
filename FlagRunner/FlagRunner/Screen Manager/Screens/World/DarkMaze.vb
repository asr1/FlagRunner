'This is a mazescreen with no lighting.

Public Class DarkMaze
    Inherits MazeScreen

    Const LIGHT_DELAY_TIME As Integer = 2

    Public Sub New()
        'Using this name for compatability with pause screen.
        'It won't be an issue because onyl one mazescreen will ever
        'Be displayed at one time.
        Name = "Mazescreen"
    End Sub

    Dim DEBUGBOOL As Boolean = True
    Public Sub createLights()
        Dim light As New Light2D
        light.Texture = Textures.LightTexture
        light.Range = 150 '15
        light.Color = Color.White
        light.Intensity = 1
        light.Angle = 250
        light.Position = New Vector2(100, 100) '(100, -48)
        light.IsOn = True

        Globals.KrypEng.Lights.Add(light)

        DEBUGBOOL = False
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
        Globals.KrypEng.SpriteBatchCompatablityEnabled = True


        'Other way to fix coordinates.
        Dim World As Matrix = Matrix.Identity
        'Dim View As Matrix = Matrix.CreateTranslation(New Vector3(0, 0, 0) * -1)
        'Dim Projection As Matrix = Matrix.CreateOrthographic(Globals.GameSize.Y * Globals.Graphics.GraphicsDevice.Viewport.AspectRatio, Globals.GameSize.Y, 0, 1)
        Dim View As Matrix = Matrix.CreateTranslation(New Vector3(Globals.Graphics.PreferredBackBufferWidth / 2, -Globals.Graphics.PreferredBackBufferHeight / 2, 0) * -1.0F)
        Dim Projection As Matrix = Matrix.CreateOrthographic(Globals.Graphics.PreferredBackBufferWidth, Globals.Graphics.PreferredBackBufferHeight, 0, 1)


        Globals.KrypEng.AmbientColor = Color.Black
        Globals.KrypEng.CullMode = CullMode.None
        Globals.KrypEng.Matrix = World ' World * View * Projection
        Globals.KrypEng.Bluriness = 1
        Globals.KrypEng.LightMapPrepare()


        If DEBUGBOOL = True Then
            '        createLights()
        End If
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

    Private Function CheckLightCollision(Player As Player) As Boolean
        For Each light As Light2D In Globals.KrypEng.Lights
            'Covnert from boundingRect to a real rectangle so I can check intersection.
            If Player.HitBox.Intersects(New Rectangle(light.Bounds.Left, light.Bounds.Bottom, light.Bounds.Width, light.Bounds.Height)) Then
                Player.FramesInLight += 1
                'If we're in at least one light, get out of here
                Return True
            End If
        Next
        'We weren't in any lights.
        Player.FramesInLight = 0
        Return False
    End Function

    'Damages any player that is hit with light
    Private Sub CalcLightDamage()
        For Each Player As Player In ConnectedPlayers
            If Player IsNot Nothing Then
                'We get a few free frames to stand in the light before it hurts.
                If CheckLightCollision(Player) Then ' And Player.FramesInLight > LIGHT_DELAY_TIME Then
                    Player.DecreaseHealth(LightGun.Damage)
                End If
            End If
        Next
    End Sub
    Public Overrides Sub Update()
        MyBase.Update()
        'See if any player is getting hit with light
        CalcLightDamage()
    End Sub

End Class
