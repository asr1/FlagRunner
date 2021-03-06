﻿Public Enum MenuItems
    CTF
    Deathmatch
    Nightlight
    LastManStanding
    Options
    Credits
    Quit
End Enum


Public Class MainMenu
    Inherits BaseScreen

    Private Entries As New List(Of MenuEntry)
    Private MenuSelect As MenuItems = MenuItems.Deathmatch 'Selected item in menu

    Private MenuSize As New Vector2(250, 160)
    Private MenuPos As New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16_Bold.MeasureString("FLAG RUNNER").X, Globals.GameSize.Y / 3)


    Public Sub New()
        Name = "MainMenu"
        State = ScreenState.Active

        AddEntry("Capture The Flag", False)
        AddEntry("Deathmatch", True)
        AddEntry("Nightlight", True)
        AddEntry("Last Man Standing", False)
        AddEntry("Options", True)
        AddEntry("Credits", True)
        AddEntry("Quit", True)
    End Sub

    Public Sub AddEntry(Text As String, Enabled As Boolean)
        Dim Entry As MenuEntry
        Entry = New MenuEntry
        With Entry
            .text = Text
            .Enabled = Enabled
        End With
        Entries.Add(Entry)
    End Sub



    ''TEMPORARY TEST KONAMI CODE
    'Public Shared KonamiCode() As Buttons = {Buttons.DPadUp, Buttons.DPadUp, Buttons.DPadDown, Buttons.DPadDown, Buttons.DPadLeft, Buttons.DPadRight, Buttons.B}
    'Public Shared KonamiCounter As Integer = 0
    'Public Shared KonamiStatus As Boolean = False
    'Public Shared temp As Buttons



    Public Overrides Sub HandleInput()

        ''If there was a button pressed
        'If Input.HasPlayerOneInputChanged() = True Then
        '    'And that button was the next in the code
        '    If (Input.ButtonPressed(KonamiCode(KonamiCounter), PlayerIndex.One)) Then
        '        'Increase our code count
        '        KonamiCounter += 1
        '    Else 'Unless we pressed the wrong button
        '        KonamiCounter = 0
        '    End If
        '    'Don't overflow. Also check if we finished
        '    If KonamiCounter = KonamiCode.Length Then
        '        KonamiStatus = True
        '        KonamiCounter = 0
        '    End If
        'End If



        'Menu Up
        If Input.KeyPressed(Keys.Up) Or Input.KeyPressed(Keys.W) Or Input.ButtonPressed(Buttons.DPadUp, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickUp, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickUp, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.DPadUp, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.LeftThumbstickUp, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.DPadUp, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.LeftThumbstickUp, PlayerIndex.Four) Or Input.ButtonPressed(Buttons.DPadUp, PlayerIndex.Four) Then
            MenuSelect -= 1
            If MenuSelect < 0 Then 'Enable wrap-around
                MenuSelect = Entries.Count - 1
            End If
            'Skip disabled
            Do Until Entries(MenuSelect).Enabled = True
                MenuSelect -= 1
                If MenuSelect < 0 Then
                    MenuSelect = Entries.Count - 1
                End If
            Loop
        End If

        'Menu Down
        If Input.KeyPressed(Keys.Down) Or Input.KeyPressed(Keys.S) Or Input.ButtonPressed(Buttons.DPadDown, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickDown, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickDown, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.DPadDown, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.LeftThumbstickDown, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.DPadDown, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.LeftThumbstickDown, PlayerIndex.Four) Or Input.ButtonPressed(Buttons.DPadDown, PlayerIndex.Four) Then
            MenuSelect += 1
            If MenuSelect > Entries.Count - 1 Then
                MenuSelect = 0
            End If
            Do Until Entries(MenuSelect).Enabled = True
                MenuSelect += 1
                If MenuSelect > Entries.Count - 1 Then
                    MenuSelect = 0
                End If
            Loop
        End If


        'TODO DEBUG
        'Invoke Selected menu Item when selected
        If Input.KeyPressed(Keys.Enter) Or Input.ButtonPressed(Buttons.A, PlayerIndex.One) Or Input.ButtonPressed(Buttons.A, PlayerIndex.One) Or Input.ButtonPressed(Buttons.A, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.A, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.A, PlayerIndex.Four) Then
            Select Case MenuSelect
                Case MenuItems.CTF
                    Game1.SetGameMode(GameMode.CTF)
                    ScreenManager.UnloadScreen("TitleScreen")
                    ScreenManager.UnloadScreen("MainMenu")
                    ScreenManager.AddScreen(New MazeScreen)
                Case MenuItems.Deathmatch
                    Game1.SetGameMode(GameMode.Deathmatch)
                    ScreenManager.UnloadScreen("TitleScreen")
                    ScreenManager.UnloadScreen("MainMenu")
                    ScreenManager.AddScreen(New MazeScreen)
                Case MenuItems.Nightlight
                    Game1.SetGameMode(GameMode.Nightlight)
                    ScreenManager.UnloadScreen("TitleScreen")
                    ScreenManager.UnloadScreen("MainMenu")
                    ScreenManager.AddScreen(New DarkMaze)
                Case MenuItems.LastManStanding
                Case MenuItems.Options
                    ScreenManager.UnloadScreen("TitleScreen")
                    ScreenManager.UnloadScreen("MainMenu")
                    ScreenManager.AddScreen(New OptionScreen)
                Case MenuItems.Credits
                    ScreenManager.UnloadScreen("TitleScreen")
                    ScreenManager.UnloadScreen("MainMenu")
                    ScreenManager.AddScreen(New CreditScreen)
                Case MenuItems.Quit
                    Game1.ShouldExit = True
            End Select
        End If
    End Sub

    Public Overrides Sub Draw()
        Globals.SpriteBatch.Begin()
        'Draw Menu Items (like a bullet select or something)
        For Each Entry In Entries
            'TODO ??
        Next



        'Globals.SpriteBatch.DrawString(Fonts.Georgia_16, KonamiCounter, New Vector2(90, 80), Color.Black)
        'If KonamiStatus = True Then
        '    Globals.SpriteBatch.Draw(Textures.BlackGradient, New Rectangle(0, 0, 500, 500), Color.Pink)
        '    KonamiStatus = False
        'End If

        'Print helper text
        If MenuSelect = MenuItems.Deathmatch Then
            Globals.SpriteBatch.DrawString(Fonts.Centaur_10, "The first player to amass " & Options.getVictoryPoints & " kills is the victor.", New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Centaur_10.MeasureString("The first player to amass " & Options.getVictoryPoints & " kills is the victor.").X / 2, MenuPos.Y - 20), Color.White, 0, New Vector2(0, 0), 1.3, SpriteEffects.None, 0)
        ElseIf MenuSelect = MenuItems.Options Then
            Globals.SpriteBatch.DrawString(Fonts.Centaur_10, "Options", New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Centaur_10.MeasureString("Options").X / 2, MenuPos.Y - 20), Color.White, 0, New Vector2(0, 0), 1.3, SpriteEffects.None, 0)
        ElseIf MenuSelect = MenuItems.Credits Then
            Globals.SpriteBatch.DrawString(Fonts.Centaur_10, "Credits", New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Centaur_10.MeasureString("Credits").X / 2, MenuPos.Y - 20), Color.White, 0, New Vector2(0, 0), 1.3, SpriteEffects.None, 0)
        ElseIf MenuSelect = MenuItems.Quit Then
            Globals.SpriteBatch.DrawString(Fonts.Centaur_10, "Quit", New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Centaur_10.MeasureString("Quit").X / 2, MenuPos.Y - 20), Color.White, 0, New Vector2(0, 0), 1.3, SpriteEffects.None, 0)
        ElseIf MenuSelect = MenuItems.CTF Then
            Globals.SpriteBatch.DrawString(Fonts.Centaur_10, "The first player to return another player's flag " & Options.getVictoryPoints & " times is the victor.", New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Centaur_10.MeasureString("The first player to return another player's flag " & Options.getVictoryPoints & " times is the victor.").X / 2, MenuPos.Y - 20), Color.White, 0, New Vector2(0, 0), 1.3, SpriteEffects.None, 0)
        ElseIf MenuSelect = MenuItems.Nightlight Then
            Globals.SpriteBatch.DrawString(Fonts.Centaur_10, "The light is your guide, and the only thing that can hurt you. Last one standing wins.", New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Centaur_10.MeasureString("The light is your guide, and the only thing that can hurt you. Last one standing wins.").X / 2, MenuPos.Y - 20), Color.White, 0, New Vector2(0, 0), 1.3, SpriteEffects.None, 0)
        ElseIf MenuSelect = MenuItems.LastManStanding Then
            Globals.SpriteBatch.DrawString(Fonts.Centaur_10, "One life. Last one standing wins.", New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Centaur_10.MeasureString("One life. Last one standing wins.").X / 2, MenuPos.Y - 20), Color.White, 0, New Vector2(0, 0), 1.3, SpriteEffects.None, 0)

        End If

        'Draw actual menu options
        Dim MenuY As Integer = MenuPos.Y + 20
        For X = 0 To Entries.Count - 1
            If X = MenuSelect Then
                'Selected
                Globals.SpriteBatch.DrawString(Fonts.Georgia_16, Entries.Item(X).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Georgia_16.MeasureString(Entries.Item(X).Text).X / 2, MenuY), Color.Red)
            ElseIf Entries.Item(X).Enabled = True Then
                'Enabled, not selected
                Globals.SpriteBatch.DrawString(Fonts.Georgia_16, Entries.Item(X).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Georgia_16.MeasureString(Entries.Item(X).Text).X / 2, MenuY), Color.White)
            Else 'Disabled
                Globals.SpriteBatch.DrawString(Fonts.Georgia_16, Entries.Item(X).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Georgia_16.MeasureString(Entries.Item(X).Text).X / 2, MenuY), Color.Gray)
            End If
            MenuY += 30 'Move down so we don't overwrite ourself
        Next
        Globals.SpriteBatch.End()
    End Sub

End Class
