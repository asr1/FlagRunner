Public Class CreditScreen
        Inherits BaseScreen

        Public Sub New()
            Name = "CreditScreen"
            State = ScreenState.Active
        End Sub

        Public Overrides Sub Draw()
        Globals.SpriteBatch.Begin()
        Globals.SpriteBatch.Draw(Textures.BlackGradient, New Rectangle(0, 0, Globals.GameSize.X - 2, Globals.GameSize.Y), New Rectangle(0, 0, 64, 1), Color.MintCream)

        'TODO
        'mgaut72 (The java maze guy)


        'Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "XNA Tutorials", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("XNA Tutorials").X / 2 - 35, 30), Color.White)
        'Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "Aardaerimus D'Aritonysss", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("Aardaerimus D'Aritonysss").X / 2, 60), Color.White, 0, New Vector2(0, 0), 0.75, SpriteEffects.None, 0)

        'Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "Maze Tiles", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("XNA Tutorials").X / 2 - 35, 90), Color.White)
        'Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "http://crawl.develz.org/wordpress/downloads", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("http://crawl.develz.org/wordpress/downloads").X / 2, 120), Color.White, 0, New Vector2(0, 0), 0.75, SpriteEffects.None, 0)

        'Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "Maze Algorithm", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("XNA Tutorials").X / 2 - 35, 150), Color.White)
        'Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "mgaut72", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("mgaut72").X / 2 - 40, 180), Color.White, 0, New Vector2(0, 0), 0.75, SpriteEffects.None, 0)

        'Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "Game By", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("XNA Tutorials").X / 2 - 35, 210), Color.White)
        'Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "Alex Rinehart", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("Alex Rinehart").X / 2 - 40, 240), Color.White, 0, New Vector2(0, 0), 0.75, SpriteEffects.None, 0)

        'Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "Special Thanks", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("XNA Tutorials").X / 2 - 35, 270), Color.White)
        'Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "Zachary Hodgerson", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("Zachary Hodgerson").X / 2 - 20, 300), Color.White, 0, New Vector2(0, 0), 0.75, SpriteEffects.None, 0)


        Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "XNA Tutorials", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("XNA Tutorials").X / 2, 30), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "Aardaerimus D'Aritonysss", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("XNA Tutorials").X / 2 - ((Fonts.Georgia_16.MeasureString("Aardaerimus D'Aritonysss").X - Fonts.Georgia_16.MeasureString("XNA Tutorials").X) / 2), 60), Color.White, 0, New Vector2(0, 0), 0.75, SpriteEffects.None, 0)

        Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "Maze Tiles", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("XNA Tutorials").X / 2, 90), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "http://crawl.develz.org/wordpress/downloads", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("http://crawl.develz.org/wordpress/downloads").X / 2, 120), Color.White, 0, New Vector2(0, 0), 0.75, SpriteEffects.None, 0)

        Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "Maze Algorithm", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("XNA Tutorials").X / 2, 150), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "mgaut72", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("mgaut72").X / 2, 180), Color.White, 0, New Vector2(0, 0), 0.75, SpriteEffects.None, 0)

        Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "Game By", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("XNA Tutorials").X / 2, 210), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "Alex Rinehart", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("Alex Rinehart").X / 2, 240), Color.White, 0, New Vector2(0, 0), 0.75, SpriteEffects.None, 0)

        Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "Special Thanks", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("XNA Tutorials").X / 2, 270), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "Zachary Hodgerson", New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16.MeasureString("Zachary Hodgerson").X / 2, 300), Color.White, 0, New Vector2(0, 0), 0.75, SpriteEffects.None, 0)


        Globals.SpriteBatch.DrawString(Fonts.Georgia_16, "Back", New Vector2(10, Globals.GameSize.Y - Fonts.Georgia_16.MeasureString("Back").Y - 10), Color.Red)
        Globals.SpriteBatch.End()
        End Sub

        Public Overrides Sub HandleInput()
            'Back is the only thing "highlighted"
        If (Input.KeyPressed(Keys.Enter) Or Input.ButtonPressed(Buttons.A, PlayerIndex.One) Or Input.ButtonPressed(Buttons.A, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.A, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.B, PlayerIndex.Four)) Then
            ScreenManager.UnloadScreen("CreditScreen")
            ScreenManager.AddScreen(New TitleScreen)
            ScreenManager.AddScreen(New MainMenu)
        End If



        End Sub

End Class
