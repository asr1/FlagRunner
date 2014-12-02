Public Class LightGun
    Inherits Weapon

    Dim numShots As Integer = 0 'tracks number of current light balls extended
    Dim lights(3) As Light2D 'All 4 of the lightballs out
    Public Sub New()
        GFX = Textures.LightGun
        Damage = 0.05
    End Sub


    Public Overrides Function getSourceRect(dir As Direction) As Rectangle
        Select Case dir
            Case Direction.Left
                Return New Rectangle(20, 2, 12, 7)
            Case Direction.Right
                Return New Rectangle(2, 2, 12, 7)
            Case Direction.Down
                Return New Rectangle(20, 10, 7, 12)
            Case Direction.Up
                Return New Rectangle(4, 11, 7, 11)
        End Select
    End Function

    Private Function CheckCollision() As Boolean
        Return False
    End Function


    'Shoot a ball of light, up to 4 times, then start overwriting
    'TODO we need to actually draw here
    Public Overrides Sub Attack(dir As Direction, player As Player)


        Dim AniTime As Double = 0
        Dim DrawTime As Double = 100  'The amount of delay between drawn updates

        lights(numShots Mod 4) = New Light2D
        Dim StopPos As Integer
        With lights(numShots Mod 4)
            .X = player.getAvatarPosition.X
            .Y = player.getAvatarPosition.Y
            .Texture = Textures.LightTexture
            .Range = 15
            .Color = Color.White
            .Intensity = 1
            .Angle = 250
            .IsOn = True
            'First select chooses our absolute stop.
            Select Case dir
                Case Direction.Down
                    StopPos = MathHelper.Min(Globals.GameSize.Y, .Y + 3) '* MazeScreen.TileSize)
                Case Direction.Up
                    StopPos = MathHelper.Max(0, .Y - 3)
                Case Direction.Left
                    StopPos = MathHelper.Max(0, .X - 3)
                Case Direction.Right
                    StopPos = Math.Min(Globals.GameSize.X, .X + 3)
            End Select

            '''''''''''''DEBUG
            StopPos += 100


            If MapBase.TileList(.X, .Y).isBlocked = False Then
                Select Case dir
                    Case Direction.Down
                        While (.Y < StopPos) AndAlso MapBase.TileList(.X, .Y).isBlocked = False


                            ''Begin draw outside Draw() sub
                            'Globals.Graphics.GraphicsDevice.SetRenderTarget(Nothing)
                            'Globals.SpriteBatch.Begin()
                            ''Redraw the background
                            'Globals.SpriteBatch.Draw(Globals.BackBuffer, New Rectangle(0, 0, Globals.Graphics.GraphicsDevice.Viewport.Width, Globals.Graphics.GraphicsDevice.Viewport.Height), Color.White)



                            AniTime += Globals.GameTime.ElapsedGameTime.Milliseconds
                            If AniTime > DrawTime Then
                                .Y += 1
                                UpdateLights()
                                DebugDrawLights()
                                AniTime = 0
                            End If
                            ''Draw outside the draw() sub
                            'Globals.SpriteBatch.End()
                            'Globals.Graphics.GraphicsDevice.Present()


                        End While
                    Case Direction.Up
                        While (.Y > StopPos) AndAlso MapBase.TileList(.X, .Y).isBlocked = False
                            ''Begin draw outside Draw() sub
                            ''          Globals.Graphics.GraphicsDevice.SetRenderTarget(Nothing)
                            'Globals.SpriteBatch.Begin()
                            ''Redraw the background
                            'Globals.SpriteBatch.Draw(Globals.BackBuffer, New Rectangle(0, 0, Globals.Graphics.GraphicsDevice.Viewport.Width, Globals.Graphics.GraphicsDevice.Viewport.Height), Color.White)


                            AniTime += Globals.GameTime.ElapsedGameTime.Milliseconds
                            If AniTime > DrawTime Then
                                .Y -= 1

                                UpdateLights()
                                DebugDrawLights()
                                AniTime = 0
                            End If

                            ''Draw outside the draw() sub
                            'Globals.SpriteBatch.End()
                            'Globals.Graphics.GraphicsDevice.Present()


                        End While
                    Case Direction.Left
                        While (.X > StopPos) AndAlso MapBase.TileList(.X, .Y).isBlocked = False
                            ''Begin draw outside Draw() sub
                            'Globals.Graphics.GraphicsDevice.SetRenderTarget(Nothing)
                            'Globals.SpriteBatch.Begin()
                            ''Redraw the background
                            'Globals.SpriteBatch.Draw(Globals.BackBuffer, New Rectangle(0, 0, Globals.Graphics.GraphicsDevice.Viewport.Width, Globals.Graphics.GraphicsDevice.Viewport.Height), Color.White)


                            AniTime += Globals.GameTime.ElapsedGameTime.Milliseconds
                            If AniTime > DrawTime Then
                                .X -= 1

                                UpdateLights()
                                DebugDrawLights()
                                AniTime = 0
                            End If

                            ''Draw outside the draw() sub
                            'Globals.SpriteBatch.End()
                            'Globals.Graphics.GraphicsDevice.Present()


                        End While
                    Case Direction.Right
                        While (.X < StopPos) AndAlso MapBase.TileList(.X, .Y).isBlocked = False
                            ''Begin draw outside Draw() sub
                            'Globals.Graphics.GraphicsDevice.SetRenderTarget(Nothing)
                            'Globals.SpriteBatch.Begin()
                            ''Redraw the background
                            'Globals.SpriteBatch.Draw(Globals.BackBuffer, New Rectangle(0, 0, Globals.Graphics.GraphicsDevice.Viewport.Width, Globals.Graphics.GraphicsDevice.Viewport.Height), Color.White)


                            AniTime += Globals.GameTime.ElapsedGameTime.Milliseconds
                            If AniTime > DrawTime Then
                                .X += 1

                                UpdateLights()
                                DebugDrawLights()
                                AniTime = 0
                            End If
                            ''Draw outside the draw() sub
                            'Globals.SpriteBatch.End()
                            'Globals.Graphics.GraphicsDevice.Present()


                        End While
                End Select
            End If
            .X *= MazeScreen.TileSize
            .Y *= MazeScreen.TileSize
        End With
        numShots += 1


        UpdateLights()
    End Sub

    Private Shared Sub DebugDrawLights()

        Globals.KrypEng.Draw(Globals.GameTime)
        For Each l As Light2D In Globals.KrypEng.Lights
            Globals.KrypEng.RenderHelper.BufferAddBoundOutline(l.Bounds)
        Next
        For Each screen As BaseScreen In ScreenManager.Screens
            If screen.Name = "MazeScreen" Then
                screen.Update()
                screen.Draw()
            End If
        Next
    End Sub

    Public Shared Sub UpdateLights()
        Globals.KrypEng.Lights.Clear()
        For Each Player As Player In MazeScreen.ConnectedPlayers
            If Player IsNot Nothing Then
                If TypeOf (Player.getMainWeapon) Is LightGun Then
                    For Each light As Light2D In DirectCast(Player.getMainWeapon, LightGun).lights
                        If Not light Is Nothing Then
                            Globals.KrypEng.Lights.Add(light)
                        End If
                    Next
                End If
            End If
        Next
    End Sub
End Class
