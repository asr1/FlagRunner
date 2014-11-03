Public Class MazeScreen
    Inherits BaseScreen


    'The ratio of expected screen size to actual. Might need to adjsut if issues on other screens (like check min/max for positive/negative ratio)
    Public Shared ScaleFactor As Vector2 = New Vector2(Globals.Graphics.GraphicsDevice.Viewport.Width / Globals.GameSize.X, Globals.Graphics.GraphicsDevice.Viewport.Height / Globals.GameSize.Y)
    Public Shared Map As MapBase
    Private Shared MapWidth As Integer = 50
    Private Shared MapHeight As Integer = 50
    Public Const TileSize As Integer = 24


    'Current Coordinates
    Public MapX As Integer = 0
    Public MapY As Integer = 0

    Private MoveTime As Double = 0

    Public Shared Player1 As Player
    Public Shared Player2 As Player
    Public Shared Player3 As Player
    Public Shared Player4 As Player


    Public Shared ConnectedPlayers(0 To 3) As Player

    Public Shared Function getMapSize() As Vector2
        Return New Vector2(MapWidth, MapHeight)
    End Function

    Public Sub New()
        Name = "MazeScreen"
        Map = New MapBase(MapWidth, MapHeight, New Vector2(0, 0), New Vector2(5, 5))
        If Status.isConnected(PlayerIndex.One) Then
            Player1 = New Player
        End If
        If Status.isConnected(PlayerIndex.Two) Then
            Player2 = New Player
        End If
        If Status.isConnected(PlayerIndex.Three) Then
            Player3 = New Player
        End If
        If Status.isConnected(PlayerIndex.Four) Then
            Player4 = New Player
        End If
    End Sub

    Public Overrides Sub HandleInput()
        'Player 1
        If Status.isConnected(PlayerIndex.One) Then
            'movement
            If Player1.AvatarOffset.X = 0 And Player1.AvatarOffset.Y = 0 Then 'And Player1.AvatarMoving = False Then
                If Input.ButtonDown(Buttons.LeftThumbstickDown, PlayerIndex.One) Then
                    Player1.MoveAvatar(Direction.Down, Player1.getAvatarPosition.X, Player1.getAvatarPosition.Y + 1)
                    Player1.LastDir = Direction.Down
                ElseIf Input.ButtonDown(Buttons.LeftThumbstickUp, PlayerIndex.One) Then
                    Player1.MoveAvatar(Direction.Up, Player1.getAvatarPosition.X, Player1.getAvatarPosition.Y - 1)
                    Player1.LastDir = Direction.Up
                ElseIf Input.ButtonDown(Buttons.LeftThumbstickLeft, PlayerIndex.One) Then
                    Player1.MoveAvatar(Direction.Left, Player1.getAvatarPosition.X - 1, Player1.getAvatarPosition.Y)
                    Player1.LastDir = Direction.Left
                ElseIf Input.ButtonDown(Buttons.LeftThumbstickRight, PlayerIndex.One) Then
                    Player1.MoveAvatar(Direction.Right, Player1.getAvatarPosition.X + 1, Player1.getAvatarPosition.Y)
                    Player1.LastDir = Direction.Right
                End If
            Else
                Player1.MoveDir = Direction.None
            End If 'End Movement
            'Attack
            If Input.ButtonPressed(Buttons.RightTrigger, PlayerIndex.One) Then
                If Player1.getMainWeapon Is Nothing Then
                    Player1.Punch()
                Else
                    Player1.getMainWeapon.Attack(Player1.LastDir, Player1)
                End If
            End If

        End If

            'Player 2
            If Status.isConnected(PlayerIndex.Two) Then
                If Player2.AvatarOffset.X = 0 And Player2.AvatarOffset.Y = 0 Then 'And Player2.AvatarMoving = False Then
                    If Input.ButtonDown(Buttons.LeftThumbstickDown, PlayerIndex.Two) Then
                        Player2.MoveAvatar(Direction.Down, Player2.getAvatarPosition.X, Player2.getAvatarPosition.Y + 1)
                        Player2.LastDir = Direction.Down
                    ElseIf Input.ButtonDown(Buttons.LeftThumbstickUp, PlayerIndex.Two) Then
                        Player2.MoveAvatar(Direction.Up, Player2.getAvatarPosition.X, Player2.getAvatarPosition.Y - 1)
                        Player2.LastDir = Direction.Up
                    ElseIf Input.ButtonDown(Buttons.LeftThumbstickLeft, PlayerIndex.Two) Then
                        Player2.MoveAvatar(Direction.Left, Player2.getAvatarPosition.X - 1, Player2.getAvatarPosition.Y)
                        Player2.LastDir = Direction.Left
                    ElseIf Input.ButtonDown(Buttons.LeftThumbstickRight, PlayerIndex.Two) Then
                        Player2.MoveAvatar(Direction.Right, Player2.getAvatarPosition.X + 1, Player2.getAvatarPosition.Y)
                        Player2.LastDir = Direction.Right
                    End If
                Else
                    Player2.MoveDir = Direction.None
                End If
                If Input.ButtonPressed(Buttons.RightTrigger, PlayerIndex.Two) Then
                    Player2.Punch()
                End If
            End If

            'Player 3
            If Status.isConnected(PlayerIndex.Three) Then
                If Player3.AvatarOffset.X = 0 And Player3.AvatarOffset.Y = 0 Then 'And Player3.AvatarMoving = False Then
                    If Input.ButtonDown(Buttons.LeftThumbstickDown, PlayerIndex.Three) Then
                        Player3.MoveAvatar(Direction.Down, Player3.getAvatarPosition.X, Player3.getAvatarPosition.Y + 1)
                        Player3.LastDir = Direction.Down
                    ElseIf Input.ButtonDown(Buttons.LeftThumbstickUp, PlayerIndex.Three) Then
                        Player3.MoveAvatar(Direction.Up, Player3.getAvatarPosition.X, Player3.getAvatarPosition.Y - 1)
                        Player3.LastDir = Direction.Up
                    ElseIf Input.ButtonDown(Buttons.LeftThumbstickLeft, PlayerIndex.Three) Then
                        Player3.MoveAvatar(Direction.Left, Player3.getAvatarPosition.X - 1, Player3.getAvatarPosition.Y)
                        Player3.LastDir = Direction.Left
                    ElseIf Input.ButtonDown(Buttons.LeftThumbstickRight, PlayerIndex.Three) Then
                        Player3.MoveAvatar(Direction.Right, Player3.getAvatarPosition.X + 1, Player3.getAvatarPosition.Y)
                        Player3.LastDir = Direction.Right
                    End If
                Else
                    Player3.MoveDir = Direction.None
                End If
                If Input.ButtonPressed(Buttons.RightTrigger, PlayerIndex.Three) Then
                    Player3.Punch()
                End If
            End If

            'Player 4
            If Status.isConnected(PlayerIndex.Four) Then
                If Player4.AvatarOffset.X = 0 And Player4.AvatarOffset.Y = 0 Then 'And Player4.AvatarMoving = False Then
                    If Input.ButtonDown(Buttons.LeftThumbstickDown, PlayerIndex.Four) Then
                        Player4.MoveAvatar(Direction.Down, Player4.getAvatarPosition.X, Player4.getAvatarPosition.Y + 1)
                        Player4.LastDir = Direction.Down
                    ElseIf Input.ButtonDown(Buttons.LeftThumbstickUp, PlayerIndex.Four) Then
                        Player4.MoveAvatar(Direction.Up, Player4.getAvatarPosition.X, Player4.getAvatarPosition.Y - 1)
                        Player4.LastDir = Direction.Up
                    ElseIf Input.ButtonDown(Buttons.LeftThumbstickLeft, PlayerIndex.Four) Then
                        Player4.MoveAvatar(Direction.Left, Player4.getAvatarPosition.X - 1, Player4.getAvatarPosition.Y)
                        Player4.LastDir = Direction.Left
                    ElseIf Input.ButtonDown(Buttons.LeftThumbstickRight, PlayerIndex.Four) Then
                        Player4.MoveAvatar(Direction.Right, Player4.getAvatarPosition.X + 1, Player4.getAvatarPosition.Y)
                        Player4.LastDir = Direction.Right
                    End If
                Else
                    Player4.MoveDir = Direction.None
                End If
                If Input.ButtonPressed(Buttons.RightTrigger, PlayerIndex.Four) Then
                    Player4.Punch()
                End If
            End If
    End Sub

    Public Overrides Sub Update()
        'Update Tiles
        MapBase.UpdateTiles(MazeScreen.getMapSize().X, MazeScreen.getMapSize().Y)

        'Anyone pause?
        Game1.CheckForPause()

        'Updates player information
        If Status.isConnected(PlayerIndex.One) Then
            'Update the hit box
            Player1.HitBox = New Rectangle(Player1.getAvatarPosition.X * TileSize, Player1.getAvatarPosition.Y * TileSize, MazeScreen.TileSize, MazeScreen.TileSize)

            'If they were just created, add them to the list.
            If Player1.NeedsUpdating = True Then
                'Updates the list of players

                'Add the player to the list
                ConnectedPlayers(0) = Player1
                Player1.NeedsUpdating = False
            End If
        End If

        'Player 2
        If Status.isConnected(PlayerIndex.Two) Then
            'Update the hit box
            Player2.HitBox = New Rectangle(Player2.getAvatarPosition.X * TileSize, Player2.getAvatarPosition.Y * TileSize, MazeScreen.TileSize, MazeScreen.TileSize)

            'If they were just created, add them to the list.
            If Player2.NeedsUpdating = True Then
                'Updates the list of players

                'Add the player to the list
                ConnectedPlayers(1) = Player2
                Player2.NeedsUpdating = False
            End If
        End If

        'Player 3
        If Status.isConnected(PlayerIndex.Three) Then
            'Update the hit box
            Player3.HitBox = New Rectangle(Player3.getAvatarPosition.X * TileSize, Player3.getAvatarPosition.Y * TileSize, MazeScreen.TileSize, MazeScreen.TileSize)

            'If they were just created, add them to the list.
            If Player3.NeedsUpdating = True Then
                'Updates the list of players

                'Add the player to the list
                ConnectedPlayers(2) = Player3
                Player3.NeedsUpdating = False
            End If
        End If

        'Player 4
        If Status.isConnected(PlayerIndex.Four) Then
            'Update the hit box
            Player4.HitBox = New Rectangle(Player4.getAvatarPosition.X * TileSize, Player4.getAvatarPosition.Y * TileSize, MazeScreen.TileSize, MazeScreen.TileSize)

            'If they were just created, add them to the list.
            If Player4.NeedsUpdating = True Then
                'Updates the list of players

                'Add the player to the list
                ConnectedPlayers(3) = Player4
                Player4.NeedsUpdating = False
            End If
        End If


    'character movement updates
        MoveTime += Globals.GameTime.ElapsedGameTime.TotalMilliseconds

        If MoveTime > 15 Then
    'Player 1
            If Status.isConnected(PlayerIndex.One) Then
                If Player1.AvatarMoving = True Then
                    If Player1.MoveDir = Direction.None And (Player1.AvatarOffset.X <> 0 Or Player1.AvatarOffset.Y <> 0) Then
    'finish move cycle before accepting new inputs
                        Player1.Move(Player1.LastDir)
                    Else 'If not between movements, accept new
                        Player1.Move(Player1.MoveDir)
                    End If

    'Between movements
                    If Player1.AvatarOffset.X = 0 And Player1.AvatarOffset.Y = 0 Then
                        Player1.AvatarMoving = False
                    End If
                End If
            End If

    'Player 2
            If Status.isConnected(PlayerIndex.Two) Then
                If Player2.AvatarMoving = True Then
                    If Player2.MoveDir = Direction.None And (Player2.AvatarOffset.X <> 0 Or Player2.AvatarOffset.Y <> 0) Then
    'finish move cycle before accepting new inputs
                        Player2.Move(Player2.LastDir)
                    Else 'If not between movements, accept new
                        Player2.Move(Player2.MoveDir)
                    End If

    'Between movements
                    If Player2.AvatarOffset.X = 0 And Player2.AvatarOffset.Y = 0 Then
                        Player2.AvatarMoving = False
                    End If
                End If
            End If

    'Player 3
            If Status.isConnected(PlayerIndex.Three) Then
                If Player3.AvatarMoving = True Then
                    If Player3.MoveDir = Direction.None And (Player3.AvatarOffset.X <> 0 Or Player3.AvatarOffset.Y <> 0) Then
    'finish move cycle before accepting new inputs
                        Player3.Move(Player3.LastDir)
                    Else 'If not between movements, accept new
                        Player3.Move(Player3.MoveDir)
                    End If

    'Between movements
                    If Player3.AvatarOffset.X = 0 And Player3.AvatarOffset.Y = 0 Then
                        Player3.AvatarMoving = False
                    End If
                End If
            End If

    'Player 4
            If Status.isConnected(PlayerIndex.Four) Then
                If Player4.AvatarMoving = True Then
                    If Player4.MoveDir = Direction.None And (Player4.AvatarOffset.X <> 0 Or Player4.AvatarOffset.Y <> 0) Then
    'finish move cycle before accepting new inputs
                        Player4.Move(Player4.LastDir)
                    Else 'If not between movements, accept new
                        Player4.Move(Player4.MoveDir)
                    End If

    'Between movements
                    If Player4.AvatarOffset.X = 0 And Player4.AvatarOffset.Y = 0 Then
                        Player4.AvatarMoving = False
                    End If
                End If

                MoveTime = 0 'reset time to reset cycle
            End If
        End If
    'End character movement updates
    End Sub

    'returns the first open square near the given coordinates.
    'TODO: Fix or remove this code
    Public Shared Function FindOpen(X As Integer, Y As Integer) As Vector2
        While MapBase.TileList(X, Y).isBlocked = True 'Loop until we find one
            If MapBase.TileList(X, Y).isBlocked = False Then
                Return New Vector2(X, Y)
            Else
                X += 1
            End If
            If MapBase.TileList(X, Y).isBlocked = False Then
                Return New Vector2(X, Y)
            Else
                X -= 1
                Y += 1
                If MapBase.TileList(X, Y).isBlocked = False Then
                    Return New Vector2(X, Y)
                Else
                    X += 1
                End If
            End If

        End While
        Return New Vector2(X, Y)
    End Function

    Public Overrides Sub Draw()
        MyBase.Draw()
        Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone)

        'Draw Maze Layer (Players will walk "on" this)
        For DrawX As Integer = -1 To MapWidth 'Start drawing to the left of the screen, avoiding clipping
            For DrawY As Integer = -1 To MapHeight
                Dim X As Integer = DrawX + MapX
                Dim Y As Integer = DrawY + MapY

                'Draw each time
                If X >= 0 And X <= MapWidth And Y >= 0 And Y <= MapHeight Then
                    Globals.SpriteBatch.Draw(MapBase.TileList(X, Y).TileGFX, New Rectangle(DrawX * TileSize, DrawY * TileSize, TileSize, TileSize), New Rectangle(0, 0, 31, 31), Color.White)
                    If Not MapBase.TileList(X, Y).Item Is Nothing Then
                        Globals.SpriteBatch.Draw(MapBase.TileList(X, Y).Item.GFX, New Rectangle(DrawX * TileSize, DrawY * TileSize, 32, 32), New Rectangle(0, 0, 31, 31), Color.White)
                    End If

                    'DEBUG view coordinates on tile
                    If Options.GetDebugMode = True Then
                        If DrawX Mod 10 = 0 And DrawY Mod 2 = 0 Then
                            Globals.SpriteBatch.DrawString(Fonts.Centaur_10, "x: " & X & vbCrLf & "y: " & Y, New Vector2(DrawX * TileSize, DrawY * TileSize), Color.White)
                        End If
                    End If
                End If
            Next
        Next 'End maze

        'Debug hit boxes
        If Options.GetDebugMode = True Then
            Globals.SpriteBatch.Draw(Textures.BlackGradient, Player1.HitBox, Color.Blue)
            Globals.SpriteBatch.Draw(Textures.BlackGradient, Player2.HitBox, Color.Red)
        End If

        'Draw bases with the right colors and properties.
        If Status.isConnected(PlayerIndex.One) Then
            Globals.SpriteBatch.Draw(Textures.BaseTile, New Rectangle(TileSize, TileSize, TileSize, TileSize), Color.Blue)
            MapBase.TileList(1, 1).TerrainType = TileType.Base
        End If
        If Status.isConnected(PlayerIndex.Two) Then
            Globals.SpriteBatch.Draw(Textures.BaseTile, New Rectangle((MazeScreen.getMapSize.X - 1) * TileSize, TileSize, TileSize, TileSize), Color.Red)
            MapBase.TileList(MazeScreen.getMapSize.X - 1, 1).TerrainType = TileType.Base
        End If
        If Status.isConnected(PlayerIndex.Three) Then
            Globals.SpriteBatch.Draw(Textures.BaseTile, New Rectangle(TileSize, (MazeScreen.getMapSize.Y - 1) * TileSize, TileSize, TileSize), Color.Green)
            MapBase.TileList(1, MazeScreen.getMapSize.Y - 1).TerrainType = TileType.Base
        End If
        If Status.isConnected(PlayerIndex.Four) Then
            Globals.SpriteBatch.Draw(Textures.BaseTile, New Rectangle((MazeScreen.getMapSize.X - 1) * TileSize, (MazeScreen.getMapSize.Y - 1) * TileSize, TileSize, TileSize), Color.Orange)
            MapBase.TileList(MazeScreen.getMapSize.X - 1, MazeScreen.getMapSize.Y - 1).TerrainType = TileType.Base
        End If


        'Avatars and health bars
        'Player 1
        If Status.isConnected(PlayerIndex.One) Then
            Globals.SpriteBatch.Draw(Textures.Pirate, New Rectangle(Player1.getAvatarPosition.X * TileSize, Player1.getAvatarPosition.Y * TileSize, TileSize, TileSize), Player1.FetchAvatarSrc(Player1.LastDir), Color.Blue)
            If Options.GetHealthBarOption = DisplayHealth.Number Then
                Globals.SpriteBatch.DrawString(Fonts.Georgia_16, Player1.GetHealth, New Vector2(Player1.getAvatarPosition.X * TileSize, (Player1.getAvatarPosition.Y * TileSize) - TileSize), Color.White)
            ElseIf Options.GetHealthBarOption = DisplayHealth.Bar Then
                'Health bar frame.
                Globals.SpriteBatch.Draw(Textures.HealthBar, New Rectangle(Player1.getAvatarPosition.X * TileSize, Player1.getAvatarPosition.Y * TileSize - 10, TileSize, 10), Textures.GetHealthBarSource, Color.White)
                'Healthbar fill
                Globals.SpriteBatch.Draw(Textures.BlackGradient, New Rectangle((Player1.getAvatarPosition.X * TileSize + 1), Player1.getAvatarPosition.Y * TileSize - 10, TileSize * Player1.GetHealth / Player.getMaxHealth, 9), Color.Green)
            End If
            If Not Player1.getMainWeapon Is Nothing Then
                Globals.SpriteBatch.Draw(Player1.getMainWeapon.GFX, New Rectangle(Player1.getAvatarPosition.X * TileSize + 0.0 * TileSize, Player1.getAvatarPosition.Y * TileSize + 10, 7, 12), Player1.getMainWeapon.getSourceRect(Player1.LastDir), Color.White, 0, New Vector2(0, 0), SpriteEffects.None, 0)
            End If
        End If

        'Player 2
        If Status.isConnected(PlayerIndex.Two) Then
            Globals.SpriteBatch.Draw(Textures.Pirate, New Rectangle(Player2.getAvatarPosition.X * TileSize, Player2.getAvatarPosition.Y * TileSize, TileSize, TileSize), Player2.FetchAvatarSrc(Player2.LastDir), Color.Red)

            If Options.GetHealthBarOption = DisplayHealth.Number Then
                Globals.SpriteBatch.DrawString(Fonts.Georgia_16, Player2.GetHealth, New Vector2(Player2.getAvatarPosition.X * TileSize, Player2.getAvatarPosition.Y * TileSize - TileSize), Color.White)
            ElseIf Options.GetHealthBarOption = DisplayHealth.Bar Then
                'Health bar frame
                Globals.SpriteBatch.Draw(Textures.HealthBar, New Rectangle(Player2.getAvatarPosition.X * TileSize, Player2.getAvatarPosition.Y * TileSize - 10, TileSize, 10), Textures.GetHealthBarSource, Color.White)
                'Healthbar fill
                Globals.SpriteBatch.Draw(Textures.BlackGradient, New Rectangle((Player2.getAvatarPosition.X * TileSize + 1), Player2.getAvatarPosition.Y * TileSize - 10, TileSize * Player2.GetHealth / Player.getMaxHealth, 9), Color.Green)
            End If
        End If

        If Status.isConnected(PlayerIndex.Three) Then
            Globals.SpriteBatch.Draw(Textures.Pirate, New Rectangle(Player3.getAvatarPosition.X * TileSize, Player3.getAvatarPosition.Y * TileSize, TileSize, TileSize), Player3.FetchAvatarSrc(Player3.LastDir), Color.Green)

            If Options.GetHealthBarOption = DisplayHealth.Number Then
                Globals.SpriteBatch.DrawString(Fonts.Georgia_16, Player3.GetHealth, New Vector2(Player3.getAvatarPosition.X * TileSize, Player3.getAvatarPosition.Y * TileSize - TileSize), Color.White)
            ElseIf Options.GetHealthBarOption = DisplayHealth.Bar Then
                'Health bar Frame
                Globals.SpriteBatch.Draw(Textures.HealthBar, New Rectangle(Player3.getAvatarPosition.X * TileSize, Player3.getAvatarPosition.Y * TileSize - 10, TileSize, 10), Textures.GetHealthBarSource, Color.White)
                'Health bar fill
                Globals.SpriteBatch.Draw(Textures.BlackGradient, New Rectangle(Player3.getAvatarPosition.X * TileSize + 1, Player3.getAvatarPosition.Y * TileSize - 10, TileSize * Player3.GetHealth / Player.getMaxHealth, 9), Color.Green)
            End If
        End If

        If Status.isConnected(PlayerIndex.Four) Then
            Globals.SpriteBatch.Draw(Textures.Pirate, New Rectangle(Player4.getAvatarPosition.X * TileSize, Player4.getAvatarPosition.Y * TileSize, TileSize, TileSize), Player4.FetchAvatarSrc(Player4.LastDir), Color.Orange)

            If Options.GetHealthBarOption = DisplayHealth.Number Then
                Globals.SpriteBatch.DrawString(Fonts.Georgia_16, Player4.GetHealth, New Vector2(Player4.getAvatarPosition.X * TileSize, Player4.getAvatarPosition.Y * TileSize - TileSize), Color.White)
            ElseIf Options.GetHealthBarOption = DisplayHealth.Bar Then
                'Health bar Frame
                Globals.SpriteBatch.Draw(Textures.HealthBar, New Rectangle(Player4.getAvatarPosition.X * TileSize, Player4.getAvatarPosition.Y * TileSize - 10, TileSize, 10), Textures.GetHealthBarSource, Color.White)
                'Health bar fill
                Globals.SpriteBatch.Draw(Textures.BlackGradient, New Rectangle(Player4.getAvatarPosition.X * TileSize + 1, Player4.getAvatarPosition.Y * TileSize - 10, TileSize * Player4.GetHealth / Player.getMaxHealth, 9), Color.Green)
            End If

        End If

        Globals.SpriteBatch.End()
    End Sub

End Class
