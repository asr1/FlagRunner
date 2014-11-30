Public Class MazeScreen
    Inherits BaseScreen

    'The ratio of expected screen size to actual. Might need to adjsut if issues on other screens (like check min/max for positive/negative ratio)
    Public Shared ScaleFactor As Vector2

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

    Public Shared Initialized As Boolean = False


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
        If Status.isConnected(PlayerIndex.One) AndAlso Player1.Living Then
            'movement
            'Speed
            If Input.ButtonDown(Buttons.B, PlayerIndex.One) Then
                Player1.SetSpeed(Player.BaseSpeed * 1.5)
            Else
                Player1.SetSpeed(Player.BaseSpeed)
            End If

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

            'Grab Item
            If Input.ButtonPressed(Buttons.A, PlayerIndex.One) Then
                'Emulate a with statement
                Dim FloorItem As Item = MapBase.TileList(Player1.getAvatarPosition.X, Player1.getAvatarPosition.Y).Item

                If Not FloorItem Is Nothing Then  ' There's something here
                    If TryCast(FloorItem, Weapon) Is Nothing Then 'We have an item
                        Player1.PickUpItem(FloorItem, Player1.getAvatarPosition)
                    Else 'We have a weapon
                        Player1.PickUpWeapon(FloorItem, Player1.getAvatarPosition)
                    End If
                End If
            End If
        End If

        'Player 2
        If Status.isConnected(PlayerIndex.Two) AndAlso Player2.Living Then
            'Running
            If Input.ButtonDown(Buttons.B, PlayerIndex.Two) Then
                Player2.SetSpeed(Player.BaseSpeed * 1.5)
            Else
                Player2.SetSpeed(Player.BaseSpeed)
            End If
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
            'Attack
            If Input.ButtonPressed(Buttons.RightTrigger, PlayerIndex.Two) Then
                If Player2.getMainWeapon Is Nothing Then
                    Player2.Punch()
                Else
                    Player2.getMainWeapon.Attack(Player2.LastDir, Player2)
                End If
            End If
        End If

        'Player 3
        If Status.isConnected(PlayerIndex.Three) AndAlso Player3.Living Then
            'Running
            If Input.ButtonDown(Buttons.B, PlayerIndex.Three) Then
                Player3.SetSpeed(Player.BaseSpeed * 1.5)
            Else
                Player3.SetSpeed(Player.BaseSpeed)
            End If
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
            'Attack
            If Input.ButtonPressed(Buttons.RightTrigger, PlayerIndex.Three) Then
                If Player3.getMainWeapon Is Nothing Then
                    Player3.Punch()
                Else
                    Player3.getMainWeapon.Attack(Player3.LastDir, Player3)
                End If
            End If
        End If

        'Player 4
        If Status.isConnected(PlayerIndex.Four) AndAlso Player4.Living Then
            'Running
            If Input.ButtonDown(Buttons.B, PlayerIndex.Four) Then
                Player4.SetSpeed(Player.BaseSpeed * 1.5)
            Else
                Player4.SetSpeed(Player.BaseSpeed)
            End If
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
            'Attack
            If Input.ButtonPressed(Buttons.RightTrigger, PlayerIndex.Four) Then
                If Player4.getMainWeapon Is Nothing Then
                    Player4.Punch()
                Else
                    Player4.getMainWeapon.Attack(Player4.LastDir, Player4)
                End If
            End If
        End If
    End Sub


    Private Sub UpdateHitbox(ByRef player As Player)
        player.HitBox = New Rectangle(player.getAvatarPosition.X * TileSize, player.getAvatarPosition.Y * TileSize, MazeScreen.TileSize, MazeScreen.TileSize)
    End Sub

    Private Sub UpdateMovement(ByRef player As Player)
        If player.AvatarMoving = True Then
            If player.MoveDir = Direction.None And (player.AvatarOffset.X <> 0 Or player.AvatarOffset.Y <> 0) Then
                'finish move cycle before accepting new inputs
                player.Move(player.LastDir)
            Else 'If not between movements, accept new
                player.Move(player.MoveDir)
            End If

            'Between movements
            If player.AvatarOffset.X = 0 And player.AvatarOffset.Y = 0 Then
                player.AvatarMoving = False
            End If
        End If
    End Sub

    Public Overrides Sub Update()
        'Update scalefactor
        ScaleFactor = New Vector2(Globals.Graphics.GraphicsDevice.Viewport.Width / Globals.GameSize.X, Globals.Graphics.GraphicsDevice.Viewport.Height / Globals.GameSize.Y)

        'Update Tiles
        MapBase.UpdateTiles(MazeScreen.getMapSize().X, MazeScreen.getMapSize().Y)

        'Anyone pause?
        Game1.CheckForPause()

        'Updates player information


        'character movement updates
        MoveTime += Globals.GameTime.ElapsedGameTime.TotalMilliseconds

        If MoveTime > 15 Then

            'Player 1
            If Status.isConnected(PlayerIndex.One) AndAlso Player1.Living Then
                UpdateHitbox(Player1)

                'If they were just created, add them to the list.
                If Player1.NeedsUpdating = True Then
                    'Updates the list of players

                    'Add the player to the list
                    ConnectedPlayers(0) = Player1
                    Player1.NeedsUpdating = False
                End If

                UpdateMovement(Player1)
            End If

            'Player 2
            If Status.isConnected(PlayerIndex.Two) AndAlso Player2.Living Then
                UpdateHitbox(Player2)
                'If they were just created, add them to the list.
                If Player2.NeedsUpdating = True Then
                    'Updates the list of players

                    'Add the player to the list
                    ConnectedPlayers(1) = Player2
                    Player2.NeedsUpdating = False
                End If

                UpdateMovement(Player2)
            End If

            'Player 3
            If Status.isConnected(PlayerIndex.Three) AndAlso Player3.Living Then

                UpdateHitbox(Player3)

                'If they were just created, add them to the list.
                If Player3.NeedsUpdating = True Then
                    'Updates the list of players

                    'Add the player to the list
                    ConnectedPlayers(2) = Player3
                    Player3.NeedsUpdating = False
                End If

                UpdateMovement(Player3)
            End If

            'Player 4
            If Status.isConnected(PlayerIndex.Four) AndAlso Player4.Living Then

                UpdateHitbox(Player4)

                'If they were just created, add them to the list.
                If Player4.NeedsUpdating = True Then
                    'Updates the list of players

                    'Add the player to the list
                    ConnectedPlayers(3) = Player4
                    Player4.NeedsUpdating = False
                End If

                UpdateMovement(Player4)
            End If

            MoveTime = 0 'reset time to reset cycle
        End If
        'End character movement updates

        Initialized = True
    End Sub


    Private Sub DrawAvatar(ByVal player As Player)
        Globals.SpriteBatch.Draw(Textures.Pirate, New Rectangle(player.getAvatarPosition.X * TileSize, player.getAvatarPosition.Y * TileSize, TileSize, TileSize), player.FetchAvatarSrc(player.LastDir), Color.Blue)
    End Sub

    Private Sub DrawHealthBars(ByVal player As Player)
        If Options.GetHealthBarOption = DisplayHealth.Number Then
            Globals.SpriteBatch.DrawString(Fonts.Georgia_16, player.GetHealth, New Vector2(player.getAvatarPosition.X * TileSize, (player.getAvatarPosition.Y * TileSize) - TileSize), Color.White)
        ElseIf Options.GetHealthBarOption = DisplayHealth.Bar Then
            'Health bar frame.
            Globals.SpriteBatch.Draw(Textures.HealthBar, New Rectangle(player.getAvatarPosition.X * TileSize, player.getAvatarPosition.Y * TileSize - 10, TileSize, 10), Textures.GetHealthBarSource, Color.White)
            'Healthbar fill
            Globals.SpriteBatch.Draw(Textures.BlackGradient, New Rectangle((player.getAvatarPosition.X * TileSize + 1), player.getAvatarPosition.Y * TileSize - 10, TileSize * player.GetHealth / player.getMaxHealth, 9), Color.Green)
        End If
    End Sub

    Private Sub DrawItemButton(ByVal player As Player)
        If Not MapBase.TileList(player.getAvatarPosition.X, player.getAvatarPosition.Y).Item Is Nothing Then
            Globals.SpriteBatch.Draw(Textures.AButton, New Rectangle(player.getAvatarPosition.X * TileSize, player.getAvatarPosition.Y * (TileSize) + TileSize, 16, 16), Color.White)
        End If
    End Sub

    Private Sub DrawWeapon(ByVal player As Player)
        If Not player.getMainWeapon Is Nothing Then
            Globals.SpriteBatch.Draw(player.getMainWeapon.GFX, New Rectangle(player.getAvatarPosition.X * TileSize + 0.0 * TileSize, player.getAvatarPosition.Y * TileSize + 10, 7, 12), player.getMainWeapon.getSourceRect(Player1.LastDir), Color.White, 0, New Vector2(0, 0), SpriteEffects.None, 0)
        End If
    End Sub

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

        'Player 1
        If Status.isConnected(PlayerIndex.One) Then
            'Base
            Globals.SpriteBatch.Draw(Textures.BaseTile, New Rectangle(TileSize, TileSize, TileSize, TileSize), Color.Blue)
            MapBase.TileList(1, 1).TerrainType = TileType.Base

            DrawAvatar(Player1)
            DrawHealthBars(Player1)
            DrawItemButton(Player1)
            DrawWeapon(Player1)
        End If

        'Player 2
        If Status.isConnected(PlayerIndex.Two) Then
            'Base
            Globals.SpriteBatch.Draw(Textures.BaseTile, New Rectangle((MazeScreen.getMapSize.X - 1) * TileSize, TileSize, TileSize, TileSize), Color.Red)
            MapBase.TileList(MazeScreen.getMapSize.X - 1, 1).TerrainType = TileType.Base

            DrawAvatar(Player2)
            DrawHealthBars(Player2)
            DrawItemButton(Player2)
            DrawWeapon(Player2)
        End If

        If Status.isConnected(PlayerIndex.Three) Then
            'Base
            Globals.SpriteBatch.Draw(Textures.BaseTile, New Rectangle(TileSize, (MazeScreen.getMapSize.Y - 1) * TileSize, TileSize, TileSize), Color.Green)
            MapBase.TileList(1, MazeScreen.getMapSize.Y - 1).TerrainType = TileType.Base

            DrawAvatar(Player3)
            DrawHealthBars(Player3)
            DrawItemButton(Player3)
            DrawWeapon(Player3)
        End If

        If Status.isConnected(PlayerIndex.Four) Then
            'Base
            Globals.SpriteBatch.Draw(Textures.BaseTile, New Rectangle((MazeScreen.getMapSize.X - 1) * TileSize, (MazeScreen.getMapSize.Y - 1) * TileSize, TileSize, TileSize), Color.Orange)
            MapBase.TileList(MazeScreen.getMapSize.X - 1, MazeScreen.getMapSize.Y - 1).TerrainType = TileType.Base

            DrawAvatar(Player4)
            DrawHealthBars(Player4)
            DrawItemButton(Player4)
            DrawWeapon(Player4)
        End If

        Globals.SpriteBatch.End()
    End Sub

End Class
