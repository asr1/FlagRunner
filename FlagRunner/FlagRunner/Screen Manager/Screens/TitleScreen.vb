Public Class TitleScreen
    Inherits BaseScreen

    'RGB value, change each time on startup
    Private r As Integer
    Private g As Integer
    Private b As Integer
    Private rnd As Random = New Random
    Private curColor As Color

    Public Sub New()
        Name = "TitleScreen"
        State = ScreenState.Active
        r = rnd.Next(60, 225)
        g = rnd.Next(60, 225)
        b = rnd.Next(60, 225)
        curColor = New Color(r, g, b)

    End Sub


    Public Overrides Sub Draw()
        MyBase.Draw()

        'Pixel arty magic
        'Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone)
        Globals.SpriteBatch.Begin()


        'TODO make background less gray so disabled is more readable
        Globals.SpriteBatch.Draw(Textures.BlackGradient, New Rectangle(0, 0, Globals.GameSize.X - 2, Globals.GameSize.Y), New Rectangle(0, 0, 64, 1), Color.MintCream)
        'Left offset
        Globals.SpriteBatch.DrawString(Fonts.Georgia_16_Bold, "FLAG RUNNER", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16_Bold.MeasureString("FLAG RUNNER").X, Globals.GameSize.Y / 5), Color.Black, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        'Center main
        Globals.SpriteBatch.DrawString(Fonts.Georgia_16_Bold, "FLAG RUNNER", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16_Bold.MeasureString("FLAG RUNNER").X, Globals.GameSize.Y / 5), curColor, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        'Right offset
        Globals.SpriteBatch.DrawString(Fonts.Georgia_16_Bold, "FLAG RUNNER", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16_Bold.MeasureString("FLAG RUNNER").X + 2, Globals.GameSize.Y / 5), Color.Black, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)

        'Draw Connected symbols
        'For each controller, check if it's connencted, and then color appropriately
        Dim StatusColor As Color = Color.White
        Dim Index As PlayerIndex = PlayerIndex.One
        For i As Integer = 0 To 3
            If GamePad.GetState(Index).IsConnected Then
                StatusColor = Color.Green
            Else
                StatusColor = Color.White
            End If
            'Actual drawing of connection orb
            Globals.SpriteBatch.Draw(Textures.StatusLight, New Rectangle(Globals.GameSize.X / 2 - Fonts.Georgia_16_Bold.MeasureString("FLAG RUNNER").X / 2 + 40 * i - 20, Globals.GameSize.Y / 4 + 20, 32, 32), StatusColor)
            Index += 1
        Next

        Globals.SpriteBatch.End()
    End Sub


End Class
