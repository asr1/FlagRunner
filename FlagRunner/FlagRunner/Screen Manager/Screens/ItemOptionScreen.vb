Public Enum ItemOptionsList
    TotalFrequency
End Enum
Public Class ItemOptionScreen
    Inherits BaseScreen
    'TODO
    '(relative item drop frequency)

    Private MenuSize As New Vector2(250, 160)
    Private MenuPos As New Vector2(Globals.GameSize.X / 3, Globals.GameSize.Y / 3)

    Private menuSelect As OptionItems = OptionItems.HealthBar

    'Menus
    Private ItemMenuEntries As New List(Of MenuEntry) 'A list of main vertical elements
    Private ItemFreqencyEntries As New List(Of MenuEntry) 'A horizontal list of Elements

    'Submenu selectors
    Dim ItemFrequencySelector As Frequency = Options.getTotalFrequency

    Public Sub New()
        Name = "ItemScreen"
        State = ScreenState.Active

        'Add main menu items
        OptionScreen.AddEntry("Item Frequency", ItemMenuEntries, True)


        'Add sub menu items
        For i As Integer = 0 To Utilities.MaxEnum(GetType(Frequency))
            OptionScreen.AddEntry([Enum].GetName(GetType(Frequency), i), ItemFreqencyEntries, True)
        Next
    End Sub



    Public Overrides Sub HandleInput()

        'Menu Up
        If Input.KeyPressed(Keys.Up) Or Input.KeyPressed(Keys.W) Or Input.ButtonPressed(Buttons.DPadUp, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickUp, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickUp, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.LeftThumbstickUp, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.LeftThumbstickUp, PlayerIndex.Four) Then
            menuSelect -= 1
            If menuSelect < 0 Then 'Enable wrap-around
                menuSelect = ItemMenuEntries.Count - 1
            End If
            'Skip disabled
            Do Until ItemMenuEntries(menuSelect).Enabled = True
                menuSelect -= 1
                If menuSelect < 0 Then
                    menuSelect = ItemMenuEntries.Count - 1
                End If
            Loop
        End If

        'Menu Down
        If Input.KeyPressed(Keys.Down) Or Input.KeyPressed(Keys.S) Or Input.ButtonPressed(Buttons.DPadDown, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickDown, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickDown, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.LeftThumbstickDown, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.LeftThumbstickDown, PlayerIndex.Four) Then
            menuSelect += 1
            If menuSelect > ItemMenuEntries.Count - 1 Then
                menuSelect = 0
            End If
            Do Until ItemMenuEntries(menuSelect).Enabled = True
                menuSelect += 1
                If menuSelect > ItemMenuEntries.Count - 1 Then
                    menuSelect = 0
                End If
            Loop
        End If

        'Invoke Selected menu Item when selected
        'Note that nothing can be invoked yet.
        If Input.KeyPressed(Keys.Enter) Or Input.ButtonPressed(Buttons.A, PlayerIndex.One) Or Input.ButtonPressed(Buttons.A, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.A, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.A, PlayerIndex.Four) Then
            'Nothing to invoke yet
        End If

        'But if we hit right or left, we'll need to cycle through the individual array option.
        'Start with left
        If Input.KeyPressed(Keys.Left) Or Input.ButtonPressed(Buttons.LeftThumbstickLeft, PlayerIndex.One) Or Input.ButtonPressed(Buttons.DPadLeft, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickLeft, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.DPadLeft, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.LeftThumbstickLeft, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.DPadLeft, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.LeftThumbstickLeft, PlayerIndex.Four) Or Input.ButtonPressed(Buttons.DPadLeft, PlayerIndex.Four) Then
            Select Case menuSelect
                'Overall Frequency
                Case ItemOptionsList.TotalFrequency
                    'Get the next smallest value or wrap around
                    ItemFrequencySelector = Utilities.NextSmallestEnum(GetType(Frequency), ItemFrequencySelector)
                    Options.setTotalFrequency(ItemFrequencySelector)
            End Select
        End If

        'Then Right
        If Input.KeyPressed(Keys.Right) Or Input.ButtonPressed(Buttons.LeftThumbstickRight, PlayerIndex.One) Or Input.ButtonPressed(Buttons.DPadRight, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickRight, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.DPadRight, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.LeftThumbstickRight, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.DPadRight, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.LeftThumbstickRight, PlayerIndex.Four) Or Input.ButtonPressed(Buttons.DPadRight, PlayerIndex.Four) Then
            Select Case menuSelect
                'Overall Frequency
                Case ItemOptionsList.TotalFrequency
                    'Get the next Largest value or wrap around
                    ItemFrequencySelector = Utilities.NextGreatestEnum(GetType(Frequency), ItemFrequencySelector)
                    Options.setTotalFrequency(ItemFrequencySelector)
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
        For x = 0 To ItemMenuEntries.Count - 1
            If x = menuSelect Then 'Our selection bar
                Globals.SpriteBatch.Draw(Textures.HealthBar, New Rectangle(MenuPos.X + MenuSize.X / 2 - 140, MenuY, 420, 30), Textures.GetHealthBarSource, Color.Orange)
            End If
            'Either way, draw the menu words
            Globals.SpriteBatch.DrawString(Fonts.Georgia_16, ItemMenuEntries.Item(x).Text, New Vector2(MenuPos.X + MenuSize.X / 2 - Fonts.Georgia_16.MeasureString(ItemMenuEntries.Item(x).Text).X / 2, MenuY), Color.White)
            MenuY += 30 'Move down so we don't overwrite ourself
        Next

        'And manually draw submenu.
        'Frequency
        Globals.SpriteBatch.DrawString(Fonts.Georgia_16, [Enum].GetName(GetType(Frequency), Options.getTotalFrequency), New Vector2(MenuPos.X + MenuSize.X / 2 + 100, 30), Color.White)
               Globals.SpriteBatch.End()
    End Sub

End Class
