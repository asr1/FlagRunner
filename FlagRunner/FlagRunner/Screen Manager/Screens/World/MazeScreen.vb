﻿Public Class MazeScreen
    Inherits BaseScreen

    Public Shared Map As MapBase
    Private Shared MapWidth As Integer = 50
    Private Shared MapHeight As Integer = 50
    Public Shared TileSize As Integer = 24


    'Current Coordinates
    Public MapX As Integer = 0
    Public MapY As Integer = 0

    Private MoveTime As Double = 0

    Public Player1 As Player
    Public Player2 As Player
    Public Player3 As Player
    Public Player4 As Player


    Public Shared Function getMapSize() As Vector2
        Return New Vector2(MapWidth, MapHeight)
    End Function

    Public Sub New()
        Name = "MazeScreen"
        Map = New MapBase(MapWidth, MapHeight, New Vector2(0, 0), New Vector2(5, 5))
        If Status.Player1 Then
            Player1 = New Player
        End If
        If Status.Player2 Then
            Player2 = New Player
        End If
        If Status.Player3 Then
            Player3 = New Player
        End If
        If Status.Player4 Then
            Player4 = New Player
        End If
    End Sub

    Public Overrides Sub HandleInput()
        'Player 1
        If Status.Player1 Then
            If Player1.AvatarOffset.X = 0 And Player1.AvatarOffset.Y = 0 Then 'And Player1.AvatarMoving = False Then
                If Input.ButtonDown(Buttons.LeftThumbstickDown, PlayerIndex.One) Then
                    Player1.MoveAvatar(Direction.Down, Player1.AvatarPosition.X, Player1.AvatarPosition.Y + 1)
                    Player1.LastDir = Direction.Down
                ElseIf Input.ButtonDown(Buttons.LeftThumbstickUp, PlayerIndex.One) Then
                    Player1.MoveAvatar(Direction.Up, Player1.AvatarPosition.X, Player1.AvatarPosition.Y - 1)
                    Player1.LastDir = Direction.Up
                ElseIf Input.ButtonDown(Buttons.LeftThumbstickLeft, PlayerIndex.One) Then
                    Player1.MoveAvatar(Direction.Left, Player1.AvatarPosition.X - 1, Player1.AvatarPosition.Y)
                    Player1.LastDir = Direction.Left
                ElseIf Input.ButtonDown(Buttons.LeftThumbstickRight, PlayerIndex.One) Then
                    Player1.MoveAvatar(Direction.Right, Player1.AvatarPosition.X + 1, Player1.AvatarPosition.Y)
                    Player1.LastDir = Direction.Right
                End If
            Else
                Player1.MoveDir = Direction.None
            End If
        End If


        'Player 2
        If Status.Player2 Then
            If Player2.AvatarOffset.X = 0 And Player2.AvatarOffset.Y = 0 Then 'And Player2.AvatarMoving = False Then
                If Input.ButtonDown(Buttons.LeftThumbstickDown, PlayerIndex.Two) Then
                    Player2.MoveAvatar(Direction.Down, Player2.AvatarPosition.X, Player2.AvatarPosition.Y + 1)
                    Player2.LastDir = Direction.Down
                ElseIf Input.ButtonDown(Buttons.LeftThumbstickUp, PlayerIndex.Two) Then
                    Player2.MoveAvatar(Direction.Up, Player2.AvatarPosition.X, Player2.AvatarPosition.Y - 1)
                    Player2.LastDir = Direction.Up
                ElseIf Input.ButtonDown(Buttons.LeftThumbstickLeft, PlayerIndex.Two) Then
                    Player2.MoveAvatar(Direction.Left, Player2.AvatarPosition.X - 1, Player2.AvatarPosition.Y)
                    Player2.LastDir = Direction.Left
                ElseIf Input.ButtonDown(Buttons.LeftThumbstickRight, PlayerIndex.Two) Then
                    Player2.MoveAvatar(Direction.Right, Player2.AvatarPosition.X + 1, Player2.AvatarPosition.Y)
                    Player2.LastDir = Direction.Right
                End If
            Else
                Player2.MoveDir = Direction.None
            End If
        End If

        'Player 3
        If Status.Player3 Then
            If Player3.AvatarOffset.X = 0 And Player3.AvatarOffset.Y = 0 Then 'And Player3.AvatarMoving = False Then
                If Input.ButtonDown(Buttons.LeftThumbstickDown, PlayerIndex.Three) Then
                    Player3.MoveAvatar(Direction.Down, Player3.AvatarPosition.X, Player3.AvatarPosition.Y + 1)
                    Player3.LastDir = Direction.Down
                ElseIf Input.ButtonDown(Buttons.LeftThumbstickUp, PlayerIndex.Three) Then
                    Player3.MoveAvatar(Direction.Up, Player3.AvatarPosition.X, Player3.AvatarPosition.Y - 1)
                    Player3.LastDir = Direction.Up
                ElseIf Input.ButtonDown(Buttons.LeftThumbstickLeft, PlayerIndex.Three) Then
                    Player3.MoveAvatar(Direction.Left, Player3.AvatarPosition.X - 1, Player3.AvatarPosition.Y)
                    Player3.LastDir = Direction.Left
                ElseIf Input.ButtonDown(Buttons.LeftThumbstickRight, PlayerIndex.Three) Then
                    Player3.MoveAvatar(Direction.Right, Player3.AvatarPosition.X + 1, Player3.AvatarPosition.Y)
                    Player3.LastDir = Direction.Right
                End If
            Else
                Player3.MoveDir = Direction.None
            End If
        End If

        'Player 4
        If Status.Player4 Then
            If Player4.AvatarOffset.X = 0 And Player4.AvatarOffset.Y = 0 Then 'And Player4.AvatarMoving = False Then
                If Input.ButtonDown(Buttons.LeftThumbstickDown, PlayerIndex.Four) Then
                    Player4.MoveAvatar(Direction.Down, Player4.AvatarPosition.X, Player4.AvatarPosition.Y + 1)
                    Player4.LastDir = Direction.Down
                ElseIf Input.ButtonDown(Buttons.LeftThumbstickUp, PlayerIndex.Four) Then
                    Player4.MoveAvatar(Direction.Up, Player4.AvatarPosition.X, Player4.AvatarPosition.Y - 1)
                    Player4.LastDir = Direction.Up
                ElseIf Input.ButtonDown(Buttons.LeftThumbstickLeft, PlayerIndex.Four) Then
                    Player4.MoveAvatar(Direction.Left, Player4.AvatarPosition.X - 1, Player4.AvatarPosition.Y)
                    Player4.LastDir = Direction.Left
                ElseIf Input.ButtonDown(Buttons.LeftThumbstickRight, PlayerIndex.Four) Then
                    Player4.MoveAvatar(Direction.Right, Player4.AvatarPosition.X + 1, Player4.AvatarPosition.Y)
                    Player4.LastDir = Direction.Right
                End If
            Else
                Player4.MoveDir = Direction.None
            End If
        End If
    End Sub

    Public Overrides Sub Update()
        'Update Tiles
        MapBase.UpdateTiles(MazeScreen.getMapSize().X, MazeScreen.getMapSize().Y)

        'character movement updates
        MoveTime += Globals.GameTime.ElapsedGameTime.TotalMilliseconds

        If MoveTime > 15 Then
            'Player 1
            If Status.Player1 Then
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
            If Status.Player2 Then
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
            If Status.Player3 Then
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
            If Status.Player4 Then
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

                If X >= 0 And X <= MapWidth And Y >= 0 And Y <= MapHeight Then
                    Globals.SpriteBatch.Draw(MapBase.TileList(X, Y).TileGFX, New Rectangle(DrawX * TileSize, DrawY * TileSize, TileSize, TileSize), New Rectangle(0, 0, 31, 31), Color.White)
                    'DEBUG view coordinates on tile
                    'If DrawX Mod 10 = 0 And DrawY Mod 2 = 0 Then
                    'Globals.SpriteBatch.DrawString(Fonts.Centaur_10, "x: " & X & vbCrLf & "y: " & Y, New Vector2(DrawX * TileSize, DrawY * TileSize), Color.White)
                    'End If

                End If
            Next
        Next 'End maze

        'Draw bases with the right colors and properties.
        If Status.Player1 Then
            Globals.SpriteBatch.Draw(Textures.BaseTile, New Rectangle(TileSize, TileSize, TileSize, TileSize), Color.Blue)
            MapBase.TileList(1, 1).TerrainType = TileType.Base
        End If
        If Status.Player2 Then
            Globals.SpriteBatch.Draw(Textures.BaseTile, New Rectangle((MazeScreen.getMapSize.X - 1) * TileSize, TileSize, TileSize, TileSize), Color.Red)
            MapBase.TileList(MazeScreen.getMapSize.X - 1, 1).TerrainType = TileType.Base
        End If
        If Status.Player3 Then
            Globals.SpriteBatch.Draw(Textures.BaseTile, New Rectangle(TileSize, (MazeScreen.getMapSize.Y - 1) * TileSize, TileSize, TileSize), Color.Green)
            MapBase.TileList(1, MazeScreen.getMapSize.Y - 1).TerrainType = TileType.Base
        End If
        If Status.Player4 Then
            Globals.SpriteBatch.Draw(Textures.BaseTile, New Rectangle((MazeScreen.getMapSize.X - 1) * TileSize, (MazeScreen.getMapSize.Y - 1) * TileSize, TileSize, TileSize), Color.Orange)
            MapBase.TileList(MazeScreen.getMapSize.X - 1, MazeScreen.getMapSize.Y - 1).TerrainType = TileType.Base
        End If

        'Avatars
        If Status.Player1 Then
            Globals.SpriteBatch.Draw(Textures.Pirate, New Rectangle(Player1.AvatarPosition.X * TileSize, Player1.AvatarPosition.Y * TileSize, TileSize, TileSize), Player1.FetchAvatarSrc(Player1.LastDir), Color.Blue)
        End If
        If Status.Player2 Then
            Globals.SpriteBatch.Draw(Textures.Pirate, New Rectangle(Player2.AvatarPosition.X * TileSize, Player2.AvatarPosition.Y * TileSize, TileSize, TileSize), Player2.FetchAvatarSrc(Player2.LastDir), Color.Red)
        End If
        If Status.Player3 Then
            Globals.SpriteBatch.Draw(Textures.Pirate, New Rectangle(Player3.AvatarPosition.X * TileSize, Player3.AvatarPosition.Y * TileSize, TileSize, TileSize), Player3.FetchAvatarSrc(Player3.LastDir), Color.Green)
        End If
        If Status.Player4 Then
            Globals.SpriteBatch.Draw(Textures.Pirate, New Rectangle(Player4.AvatarPosition.X * TileSize, Player4.AvatarPosition.Y * TileSize, TileSize, TileSize), Player4.FetchAvatarSrc(Player4.LastDir), Color.Orange)
        End If
        Globals.SpriteBatch.End()
    End Sub

End Class
