Public Enum OptionItems
    HealthBar
    Back
End Enum

Public Class OptionScreen
    Inherits BaseScreen

    'TODO
    '(resolution, volume, % breakable walls, item drop frequency, relative item drop frequency, health bar display)

    Private MenuSize As New Vector2(250, 160)
    Private MenuPos As New Vector2(Globals.GameSize.X / 2, Globals.GameSize.Y / 3)
    Private MenuEntries As New List(Of MenuEntry) 'A list of main vertical elements
    Private HealthBarEntries As New List(Of MenuEntry) 'A horizontal list of Elements

    Private menuSelect As OptionItems = OptionItems.HealthBar

    Dim HealthSelect As DisplayHealth = Options.GetHealthBarOption

    Public Sub New()
        Name = "OptionScreen"
        State = ScreenState.Active

        'Add main menu items
        AddEntry("Health Bar", MenuEntries, True)
        AddEntry("Back", MenuEntries, True)

        'Add sub menu items
        'First Healthbar
        For i As Integer = 0 To Utilities.MaxEnum(GetType(DisplayHealth))
            AddEntry([Enum].GetName(GetType(DisplayHealth), i), HealthBarEntries, True)
        Next
    End Sub

    'Subroutine to create a menu item and add it to a given menu or submenu
    Public Sub AddEntry(Text As String, list As List(Of MenuEntry), Enabled As Boolean)
        Dim Entry As MenuEntry
        Entry = New MenuEntry
        With Entry
            .Text = Text
            .Enabled = Enabled
        End With
        list.Add(Entry)
    End Sub

    Public Overrides Sub HandleInput()

        'Menu Up
        If Input.KeyPressed(Keys.Up) Or Input.KeyPressed(Keys.W) Or Input.ButtonPressed(Buttons.DPadUp, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickUp, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickUp, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.LeftThumbstickUp, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.LeftThumbstickUp, PlayerIndex.Four) Then
            menuSelect -= 1
            If menuSelect < 0 Then 'Enable wrap-around
                menuSelect = MenuEntries.Count - 1
            End If
            'Skip disabled
            Do Until MenuEntries(menuSelect).Enabled = True
                menuSelect -= 1
                If menuSelect < 0 Then
                    menuSelect = MenuEntries.Count - 1
                End If
            Loop
        End If

        'Menu Down
        If Input.KeyPressed(Keys.Down) Or Input.KeyPressed(Keys.S) Or Input.ButtonPressed(Buttons.DPadDown, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickDown, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickDown, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.LeftThumbstickDown, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.LeftThumbstickDown, PlayerIndex.Four) Then
            menuSelect += 1
            If menuSelect > MenuEntries.Count - 1 Then
                menuSelect = 0
            End If
            Do Until MenuEntries(menuSelect).Enabled = True
                menuSelect += 1
                If menuSelect > MenuEntries.Count - 1 Then
                    menuSelect = 0
                End If
            Loop
        End If

        'Invoke Selected menu Item when selected
        'Note that the only thing we can "select" here is back
        If Input.KeyPressed(Keys.Enter) Or Input.ButtonPressed(Buttons.A, PlayerIndex.One) Or Input.ButtonPressed(Buttons.A, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.A, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.A, PlayerIndex.Four) Then
            Select Case menuSelect
                Case OptionItems.Back
                    ScreenManager.UnloadScreen("OptionScreen")
                    ScreenManager.AddScreen(New TitleScreen)
                    ScreenManager.AddScreen(New MainMenu)
            End Select
        End If

        'But if we hit right or left, we'll need to cycle through the individual array option.
        'Start with left
        If Input.KeyPressed(Keys.Left) Or Input.ButtonPressed(Buttons.LeftThumbstickLeft, PlayerIndex.One) Or Input.ButtonPressed(Buttons.DPadLeft, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickLeft, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.DPadLeft, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.LeftThumbstickLeft, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.DPadLeft, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.LeftThumbstickLeft, PlayerIndex.Four) Or Input.ButtonPressed(Buttons.DPadLeft, PlayerIndex.Four) Then
            Select Case menuSelect
                Case OptionItems.HealthBar
                    'Enable WrapAround
                    HealthSelect -= 1
                    If HealthSelect < 0 Then
                        HealthSelect = Utilities.MaxEnum(GetType(DisplayHealth))
                    End If
                    Options.SetHealthBarOption(HealthSelect)
            End Select
        End If

        'Then Right
        If Input.KeyPressed(Keys.Right) Or Input.ButtonPressed(Buttons.LeftThumbstickRight, PlayerIndex.One) Or Input.ButtonPressed(Buttons.DPadRight, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickRight, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.DPadRight, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.LeftThumbstickRight, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.DPadRight, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.LeftThumbstickRight, PlayerIndex.Four) Or Input.ButtonPressed(Buttons.DPadRight, PlayerIndex.Four) Then
            Select Case menuSelect
                'If we're going right on healthbar
                Case OptionItems.HealthBar
                    'Enable wrap around
                    HealthSelect += 1
                    If HealthSelect > Utilities.MaxEnum(GetType(DisplayHealth)) Then
                        HealthSelect = 0
                    End If
                    'Set the option appropriately
                    Options.SetHealthBarOption(HealthSelect)
            End Select
        End If

    End Sub


    Public Overrides Sub Draw()
        Globals.SpriteBatch.Begin()
        'Starrt with our beloved texture
        Globals.SpriteBatch.Draw(Textures.BlackGradient, New Rectangle(0, 0, Globals.GameSize.X - 2, Globals.GameSize.Y), New Rectangle(0, 0, 64, 1), Color.MintCream)


        'TODO: Add arrows around option choices

        'Draw the menu
        Dim MenuY As Integer = 30
        For x = 0 To MenuEntries.Count - 1
            If x = menuSelect Then
                Globals.SpriteBatch.Draw(Textures.HealthBar, New Rectangle(MenuPos.X + MenuSize.X / 2 - 110, MenuY, 330, 30), Textures.GetHealthBarSource, Color.Orange)
            End If
            'Either way, draw the menu words
            Globals.SpriteBatch.DrawString(Fonts.Georgia_16, MenuEntries.Item(x).Text, New Vector2(MenuPos.X + MenuSize.X / 2 - Fonts.Georgia_16.MeasureString(MenuEntries.Item(x).Text).X / 2, MenuY), Color.White)
            MenuY += 30 'Move down so we don't overwrite ourself
        Next

        'And manually draw submenu. Investigate a way to loop through this? 'Note: No better way to do thsi
        'If we make a superItem class, we lose the readability. Do it like this.
        Globals.SpriteBatch.DrawString(Fonts.Georgia_16, [Enum].GetName(GetType(DisplayHealth), Options.GetHealthBarOption), New Vector2(MenuPos.X + MenuSize.X / 2 + 80, 30), Color.White)

        Globals.SpriteBatch.End()
    End Sub


End Class
