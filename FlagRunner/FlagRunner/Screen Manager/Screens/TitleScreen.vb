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
        Globals.SpriteBatch.Draw(Textures.BlackGradient, New Rectangle(0, 0, Globals.GameSize.X - 2, Globals.GameSize.Y), New Rectangle(0, 0, 64, 1), Color.White)
        'Left offset
        Globals.SpriteBatch.DrawString(Fonts.Georgia_16_Bold, "FLAG RUNNER", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16_Bold.MeasureString("FLAG RUNNER").X, Globals.GameSize.Y / 5), Color.Black, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        'Center main
        Globals.SpriteBatch.DrawString(Fonts.Georgia_16_Bold, "FLAG RUNNER", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16_Bold.MeasureString("FLAG RUNNER").X, Globals.GameSize.Y / 5), curColor, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        'Right offset
        Globals.SpriteBatch.DrawString(Fonts.Georgia_16_Bold, "FLAG RUNNER", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16_Bold.MeasureString("FLAG RUNNER").X + 2, Globals.GameSize.Y / 5), Color.Black, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        Globals.SpriteBatch.End()
    End Sub


End Class
