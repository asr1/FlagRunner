'Public Enum PauseItems
'    ResumeGame
'    QuitGame
'End Enum

'Public Class PauseScreen
'    Inherits BaseScreen

'    Private Shared Entries As New List(Of MenuEntry)
'    Private Shared PauseSelect As PauseItems = PauseItems.ResumeGame

'    Private Shared MenuSize As New Vector2(250, 160)
'    Private Shared MenuPos As New Vector2(Globals.GameSize.X / 2 - Fonts.Georgia_16_Bold.MeasureString("FLAG RUNNER").X, Globals.GameSize.Y / 3)

'    Public Sub New()
'        Name = "PauseScreen"
'        State = ScreenState.Active

'        AddEntry("Resume Game", True)
'        AddEntry("Quit Game", True)
'    End Sub

'    Public Sub AddEntry(Text As String, Enabled As Boolean)
'        Dim Entry As MenuEntry
'        Entry = New MenuEntry
'        With Entry
'            .text = Text
'            .Enabled = Enabled
'        End With
'        Entries.Add(Entry)
'    End Sub

'    'We moved everything into one update sub.
'    Public Shared Sub HandlePauseScreen()

'        'Menu Up
'        If Input.KeyPressed(Keys.Up) Or Input.KeyPressed(Keys.W) Or Input.ButtonPressed(Buttons.DPadUp, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickUp, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickUp, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.LeftThumbstickUp, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.LeftThumbstickUp, PlayerIndex.Four) Then
'            PauseSelect -= 1
'            If PauseSelect < 0 Then 'Enable wrap-around
'                PauseSelect = Entries.Count - 1
'            End If
'            'Skip disabled
'            Do Until Entries(PauseSelect).Enabled = True
'                PauseSelect -= 1
'                If PauseSelect < 0 Then
'                    PauseSelect = Entries.Count - 1
'                End If
'            Loop
'        End If

'        'Menu Down
'        If Input.KeyPressed(Keys.Down) Or Input.KeyPressed(Keys.S) Or Input.ButtonPressed(Buttons.DPadDown, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickDown, PlayerIndex.One) Or Input.ButtonPressed(Buttons.LeftThumbstickDown, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.LeftThumbstickDown, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.LeftThumbstickDown, PlayerIndex.Four) Then
'            PauseSelect += 1
'            If PauseSelect > Entries.Count - 1 Then
'                PauseSelect = 0
'            End If
'            Do Until Entries(PauseSelect).Enabled = True
'                PauseSelect += 1
'                If PauseSelect > Entries.Count - 1 Then
'                    PauseSelect = 0
'                End If
'            Loop
'        End If


'        'TODO DEBUG
'        'Invoke Selected menu Item when selected
'        If Input.KeyPressed(Keys.Enter) Or Input.ButtonPressed(Buttons.A, PlayerIndex.One) Or Input.ButtonPressed(Buttons.A, PlayerIndex.One) Or Input.ButtonPressed(Buttons.A, PlayerIndex.Two) Or Input.ButtonPressed(Buttons.A, PlayerIndex.Three) Or Input.ButtonPressed(Buttons.A, PlayerIndex.Four) Then
'            Select Case PauseSelect
'                Case PauseItems.ResumeGame
'                    ScreenManager.UnloadScreen("PauseScreen")
'                Case PauseItems.QuitGame
'                    ScreenManager.UnloadScreen("PauseScreen")
'                    ScreenManager.UnloadScreen("MazeScreen")
'                    ScreenManager.AddScreen(New TitleScreen)
'                    ScreenManager.AddScreen(New MainMenu)
'            End Select
'        End If

'        'This is where update ends and draw begins.
'        'I wish I didn't have to handle pausing this way.

'        Globals.SpriteBatch.Begin()

'        'Draw the menu
'        Dim MenuY As Integer = 30
'        For x = 0 To Entries.Count - 1
'            If x = PauseSelect Then 'Our selection bar
'                Globals.SpriteBatch.Draw(Textures.HealthBar, New Rectangle(MenuPos.X + MenuSize.X / 2 - 140, MenuY, 420, 30), Textures.GetHealthBarSource, Color.Orange)
'            End If
'            'Either way, draw the menu words
'            Globals.SpriteBatch.DrawString(Fonts.Georgia_16, Entries.Item(x).Text, New Vector2(MenuPos.X + MenuSize.X / 2 - Fonts.Georgia_16.MeasureString(Entries.Item(x).Text).X / 2, MenuY), Color.White)
'            MenuY += 30 'Move down so we don't overwrite ourself
'        Next
'        Globals.SpriteBatch.End()
'    End Sub
'End Class
