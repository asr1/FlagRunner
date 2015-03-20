Public Class Trident
    Inherits Weapon

    Public Sub New()
        GFX = Textures.Trident
        sound = Sounds.TridentNoise
        Damage = Damages.TRIDENT_DAMAGE
    End Sub



    Public Overrides Function getSourceRect(dir As Direction) As Rectangle
        Return New Rectangle(8, 9, 15, 21)
    End Function

    Public Overrides Sub Attack(dir As Direction, attacker As Player)

        Globals.Graphics.GraphicsDevice.SetRenderTarget(Nothing)
        Globals.SpriteBatch.Begin()
        'Redraw the background
        Globals.SpriteBatch.Draw(Globals.BackBuffer, New Rectangle(0, 0, Globals.Graphics.GraphicsDevice.Viewport.Width, Globals.Graphics.GraphicsDevice.Viewport.Height), Color.White)

        Select Case dir
            Case Direction.Down
                attacker.HitBox.Y += 4
                Globals.SpriteBatch.Draw(Textures.attackTrident, New Rectangle(attacker.AvatarPosition.X * MazeScreen.TileSize * MazeScreen.ScaleFactor.X, ((attacker.AvatarPosition.Y * MazeScreen.TileSize) + 1) * MazeScreen.ScaleFactor.Y, MazeScreen.TileSize, MazeScreen.TileSize), Color.White)
                If Utilities.DetectCollision(attacker) Then
                    If (Utilities.FindCollision(attacker).DecreaseHealth(Damage)) Then
                        attacker.IncreaseVicPoints()  'We got a kill
                    End If
                End If
                attacker.HitBox.Y -= 4

            Case Direction.Left
                attacker.HitBox.X -= 4
                Globals.SpriteBatch.Draw(Textures.attackTrident, New Rectangle(((attacker.AvatarPosition.X * MazeScreen.TileSize) - 4) * MazeScreen.ScaleFactor.X, (attacker.AvatarPosition.Y * MazeScreen.TileSize) * MazeScreen.ScaleFactor.Y, MazeScreen.TileSize, MazeScreen.TileSize), Color.White)
                If Utilities.DetectCollision(attacker) Then
                    If (Utilities.FindCollision(attacker).DecreaseHealth(Damage)) Then
                        attacker.IncreaseVicPoints()
                    End If
                End If
                attacker.HitBox.X += 4

            Case Direction.Right
                attacker.HitBox.X += 4
                Globals.SpriteBatch.Draw(Textures.attackTrident, New Rectangle(((attacker.AvatarPosition.X * MazeScreen.TileSize)) * MazeScreen.ScaleFactor.X, ((attacker.AvatarPosition.Y * MazeScreen.TileSize) + 4) * MazeScreen.ScaleFactor.Y, MazeScreen.TileSize, MazeScreen.TileSize), Color.White)
                If Utilities.DetectCollision(attacker) Then
                    If Utilities.FindCollision(attacker).DecreaseHealth(Damage) Then
                        attacker.IncreaseVicPoints()
                    End If
                End If
                attacker.HitBox.X -= 4

            Case Direction.Up
                attacker.HitBox.Y -= 4
                Globals.SpriteBatch.Draw(Textures.Pirate, New Rectangle(((attacker.AvatarPosition.X * MazeScreen.TileSize) + 4) * MazeScreen.ScaleFactor.X, (attacker.AvatarPosition.Y * MazeScreen.TileSize) * MazeScreen.ScaleFactor.Y, MazeScreen.TileSize, MazeScreen.TileSize), Color.White)
                If Utilities.DetectCollision(attacker) Then
                    If Utilities.FindCollision(attacker).DecreaseHealth(Damage) Then
                        attacker.IncreaseVicPoints()
                    End If
                End If
                attacker.HitBox.Y -= 4
        End Select

        Globals.SpriteBatch.End()
        Globals.Graphics.GraphicsDevice.Present()



    End Sub

End Class
