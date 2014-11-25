Public Enum Direction
    None
    Down
    Left
    Right
    Up
End Enum


'Okay so what we're going to do is have a boolean or possibly a state enum 
'And then in the mazescreen draw sub we're going to update our position/drawing appropriately.
'It's possible there is a better way, but we would have to solve the errors with our backbuffer
'Re: the screen turning purple when we try to draw.

'There is no player update function; it would cause dependency issues.
'Player updates are handled in the Mazescreen Class.

'Each character that is controlled.
Public Class Player
    Private Shared playerNum = 1 'The number of players
    Private UniquePlayerNum As Integer 'The unique identifier for this specific player
    Private PlayerColor As Color 'The color of the given player
    Private Shared MaxHealth As Double = Options.getHealth 'The maximum health a player has
    Private Health As Double 'The current health a player has
    Private isAlive As Boolean = True
    Public VictoryPoints As Integer = 0

    'Weapons
    'Set these equal to fists or something
    Private MainWeapon As Weapon = Nothing
    Private SecondaryWeapon As Weapon = Nothing

    Private Const PUNCH_DAMAGE = 1

    'Used to toggle if a player is created
    Public NeedsUpdating As Boolean = False



    'Map coordinates
    Private AvatarPosition As Vector2
    Private StartPosition As Vector2

    'Avatar offset for smooth walking
    Public AvatarOffset As Vector2 = New Vector2(0, 0)

    'Movement
    Public AvatarMoving As Boolean = False
    Public Const BaseSpeed As Double = 3
    Public MoveSpeed As Double 'This updates based on status effects
    Public MoveDir As Direction = Direction.None
    Public LastDir As Direction = Direction.Down
    Private AvatarFrame As Integer = 0

    'Source Rectangle
    Private SRect As Rectangle

    'Colision
    Public HitBox As Rectangle

    'Used for nightlight mode. We get a delay of a couple frames 
    'Before we start taking damage.
    Public FramesInLight As Integer = 0

    'Getter for max health (This is basically a const)
    Public Shared Function getMaxHealth() As Integer
        Return MaxHealth
    End Function


    Public Sub New()
        'Set attributes
        MoveSpeed = BaseSpeed
        Health = MaxHealth
        UniquePlayerNum = playerNum
        HitBox = New Rectangle(Me.AvatarPosition.X * MazeScreen.TileSize, Me.AvatarPosition.Y * MazeScreen.TileSize, MazeScreen.TileSize, MazeScreen.TileSize)

        If Game1.GetGameMode = GameMode.Nightlight Then
            MainWeapon = New LightGun
        End If

        'Position them appropriately
        If playerNum = 1 Then
            AvatarPosition = New Vector2(1, 1)
            PlayerColor = Color.Blue
        ElseIf playerNum = 2 Then
            AvatarPosition = New Vector2(MazeScreen.getMapSize.X - 1, 1)
            PlayerColor = Color.Red
        ElseIf playerNum = 3 Then
            AvatarPosition = New Vector2(1, MazeScreen.getMapSize.Y - 1)
            PlayerColor = Color.Green
        ElseIf playerNum = 4 Then
            AvatarPosition = New Vector2(MazeScreen.getMapSize.X - 1, MazeScreen.getMapSize.Y - 1)
            PlayerColor = Color.Orange
        End If
        StartPosition = AvatarPosition
        NeedsUpdating = True
        playerNum += 1
    End Sub


    ''''''''''''''''''''''''''''''''''''''''''Begin Getters and Setters
    'Getter for UniquePlayerNum
    Public Function GetPlayerID() As Integer
        Return UniquePlayerNum
    End Function

    'Getter for current player health
    Public Function GetHealth() As Integer
        Return Health
    End Function

    'Getter for Position
    Public Function getAvatarPosition() As Vector2
        Return AvatarPosition
    End Function

    'Setter for position
    Public Sub setAvatarPosition(Position As Vector2)
        AvatarPosition = Position
    End Sub

    'Getter for main weapon
    Public Function getMainWeapon() As Weapon
        Return MainWeapon
    End Function

    'Getter for secondary Weapon
    Public Function getSecondaryWeapon() As Weapon
        Return SecondaryWeapon
    End Function

    'Setter for main weapon
    Public Sub setMainWeapon(weapon As Weapon)
        MainWeapon = weapon
    End Sub

    'Setter for secondary weapons
    Public Sub setSecWeap(weapon As Weapon)
        SecondaryWeapon = weapon
    End Sub

    'Getter for isAlive                                                                
    Public Function Living() As Boolean
        Return isAlive
    End Function

    'Getter for Basespeed
    Public Function GetSpeed() As Double
        Return MoveSpeed
    End Function

    'Setter for Basespeed
    Public Sub SetSpeed(newSpeed As Double)
        MoveSpeed = newSpeed
    End Sub

    Public Sub IncreaseVicPoints()
        VictoryPoints += 1
    End Sub

    ''''''''''''''''''''''''''''''''''''''''End Getters and Setters

    'Used if we exit a game prematurely
    'So that we don't break things with our
    'New()
    'Basically pretending we're starting a new game.
    Public Shared Sub reset()
        playerNum = 1
        For Each Player As Player In MazeScreen.ConnectedPlayers
            If Player IsNot Nothing Then
                Player.isAlive = True
                Player.VictoryPoints = 0
            End If
        Next
    End Sub

    'Todo
    Public Sub PickUpItem(item As Item, position As Vector2)
        'Basically we need a case statement here
        'For every type of itme in the game.

        'Then, after implementing effects,
        'We remove the item
        MapBase.TileList(position.X, position.Y).Item = Nothing
    End Sub

    'TODO Drop old weapon
    Public Sub PickUpWeapon(weapon As Weapon, position As Vector2)
        'We're not holding anything
        If Me.MainWeapon Is Nothing Then
            Me.MainWeapon = weapon
            MapBase.TileList(position.X, position.Y).Item = Nothing

            'We're holindg one weapon
            'And that weapon is not the same as the weapon we're already holding
        ElseIf Me.SecondaryWeapon Is Nothing And Not weapon.GetType Is MainWeapon.GetType Then
            'Pick up this weapon in the primary slot, 
            'And switch our primary weapon to secondary.
            Me.SecondaryWeapon = weapon
            Me.SwapWeapons()
            MapBase.TileList(position.X, position.Y).Item = Nothing

            'We have two weapons
            'Check for duplicate
        ElseIf Not Me.SecondaryWeapon Is Nothing AndAlso Not Me.SecondaryWeapon.GetType Is weapon.GetType Then
            'Replace the primary weapon
            DropWeapon(weapon, position)
            Me.MainWeapon = weapon
        End If
    End Sub

    Private Shared Sub DropWeapon(weapon As Weapon, position As Vector2)
        MapBase.TileList(position.X, position.Y).Item = weapon
    End Sub


    'A function for respawining based on gametype
    Private Sub respawn()
        'Only one life, no respawn
        If Game1.GetGameMode = GameMode.LastManStanding Or Game1.GetGameMode = GameMode.Nightlight Then
            isAlive = False
        Else 'We need to respawn
            'Probably TODO
            AvatarPosition = StartPosition
            MainWeapon = Nothing
            SecondaryWeapon = Nothing
            MoveSpeed = BaseSpeed
            Health = MaxHealth
        End If
    End Sub


    'Moves the player
    Public Sub Move(Dir As Direction)
        Me.MoveDir = Dir
        HitBox = New Rectangle(Me.AvatarPosition.X * MazeScreen.TileSize, Me.AvatarPosition.Y * MazeScreen.TileSize, MazeScreen.TileSize, MazeScreen.TileSize)

        Select Case Dir
            Case Direction.Down
                AvatarOffset.Y -= MoveSpeed
                If AvatarOffset.Y <= -MazeScreen.TileSize Then
                    AvatarPosition.Y += 1 'Actual movement?
                    AvatarOffset.Y = 0
                End If
            Case Direction.Left
                AvatarOffset.X += MoveSpeed
                If AvatarOffset.X >= MazeScreen.TileSize Then
                    AvatarPosition.X -= 1 'Acutal Movement?
                    AvatarOffset.X = 0
                End If

            Case Direction.Right
                AvatarOffset.X -= MoveSpeed
                If AvatarOffset.X <= -MazeScreen.TileSize Then
                    AvatarPosition.X += 1 'Acutal Movement?
                    AvatarOffset.X = 0
                End If
            Case Direction.Up
                AvatarOffset.Y += MoveSpeed
                If AvatarOffset.Y >= MazeScreen.TileSize Then
                    AvatarPosition.Y -= 1 'Actual movement?
                    AvatarOffset.Y = 0
                End If
        End Select

        If AvatarOffset.X <> 0 Then
            AvatarFrame = Math.Floor(Math.Abs(AvatarOffset.X) / 32 * 4)
        ElseIf AvatarOffset.Y <> 0 Then
            AvatarFrame = Math.Floor(Math.Abs(AvatarOffset.Y) / 32 * 4)
        Else
            AvatarFrame = 0
        End If

        If MoveDir <> Direction.None Then
            LastDir = Dir
        End If
    End Sub


    'Sets the status of the avatar as moving
    Public Sub MoveAvatar(Dir As Direction, AvX As Integer, AvY As Integer)
        'If I'm not walking into a wall or another person, go right ahead

        'Grab the hitbox, test the new one, then restore the old hitbox
        Dim Temp As Rectangle = Me.HitBox
        Me.HitBox = New Rectangle(AvX * MazeScreen.TileSize, AvY * MazeScreen.TileSize, MazeScreen.TileSize, MazeScreen.TileSize)
        If MapBase.TileList(AvX, AvY).isBlocked = False And DetectCollision(Me) = False Then
            AvatarMoving = True
            MoveDir = Dir
        End If
        Me.HitBox = Temp
    End Sub

    'Switch the weapons the player is holding.
    Public Sub SwapWeapons()
        Dim temp As Weapon = MainWeapon
        MainWeapon = SecondaryWeapon
        SecondaryWeapon = temp
    End Sub

    'Detects a collision with another player
    'Returns true if there is a collision, else false.
    'So our collision detection works UNLESS both players are running at another, or 
    'Both are moving simulataneously.
    'Could remedy, could leave in. Let's see how it plays for a whle. (Fix in moveAvatar sub)
    Private Function DetectCollision(player As Player) As Boolean
        For Each p As Player In MazeScreen.ConnectedPlayers
            If Not IsNothing(p) Then
                'Don't test for collision with ourself
                If Not p.Equals(player) Then
                    If player.HitBox.Intersects(p.HitBox) Then
                        Return True
                    End If
                End If
            End If
        Next
        'We iterated through every player without finding a collision
        Return False
    End Function

    'Detects a collision with another player
    'Returns the player with which a collision occurs.
    'Should only be called after detect collision
    Private Function FindCollision(player As Player) As Player
        For Each p As Player In MazeScreen.ConnectedPlayers
            If Not IsNothing(p) Then
                'Don't test for collision with ourself
                If Not p.Equals(player) Then
                    If player.HitBox.Intersects(p.HitBox) Then
                        Return p
                    End If
                End If
            End If
        Next
        'We iterated through every player without finding a collision
        'We should never get here
        Return Nothing
    End Function

    'Returns true if the unique player identifiers are the same, false otherwise.
    Public Overrides Function Equals(obj As Object) As Boolean
        'If it's null....
        If IsNothing(obj) Then
            Return False
        End If

        '...Or not a player, return false
        If Not TypeOf (obj) Is Player Then
            Return False
        End If

        'Else test for equality
        Return Me.UniquePlayerNum = DirectCast(obj, Player).GetPlayerID

    End Function

    'Based on direction, return specific sprite
    Public Function FetchAvatarSrc(dir As Direction) As Rectangle
        Select Case dir
            Case Direction.Down
                SRect = New Rectangle(32 * AvatarFrame, 0, 32, 47)
            Case Direction.Left
                SRect = New Rectangle(32 * AvatarFrame, 47, 32, 47)
            Case Direction.Right
                SRect = New Rectangle(32 * AvatarFrame, 94, 32, 47)
            Case Direction.Up
                SRect = New Rectangle(32 * AvatarFrame, 142, 32, 47)
        End Select
        Return SRect
    End Function

    'Decrease hit points by the amount specified,  
    'Going no lower than zero
    Public Sub DecreaseHealth(i As Double)
        Me.Health = Math.Max(0, Me.Health - i)
        If Me.Health = 0 Then
            Me.respawn()
        End If
    End Sub


    'Present issue: Sprite draws in wrong spot on full screen. Please investigate.
    Public Sub Punch()
        Globals.Graphics.GraphicsDevice.SetRenderTarget(Nothing)
        Globals.SpriteBatch.Begin()
        'Redraw the background
        Globals.SpriteBatch.Draw(Globals.BackBuffer, New Rectangle(0, 0, Globals.Graphics.GraphicsDevice.Viewport.Width, Globals.Graphics.GraphicsDevice.Viewport.Height), Color.White)

        Select Case Me.LastDir
            Case Direction.Down
                Me.HitBox.Y += 4
                'Draw this animation
                Globals.SpriteBatch.Draw(Textures.Pirate, New Rectangle(Me.AvatarPosition.X * MazeScreen.TileSize * MazeScreen.ScaleFactor.X, ((Me.AvatarPosition.Y * MazeScreen.TileSize) + 1) * MazeScreen.ScaleFactor.Y, MazeScreen.TileSize, MazeScreen.TileSize), Me.PlayerColor)
                If DetectCollision(Me) Then
                    FindCollision(Me).DecreaseHealth(PUNCH_DAMAGE)
                End If
                Me.HitBox.Y -= 4

            Case Direction.Left
                Me.HitBox.X -= 4
                Globals.SpriteBatch.Draw(Textures.Pirate, New Rectangle(((Me.AvatarPosition.X * MazeScreen.TileSize) - 4) * MazeScreen.ScaleFactor.X, (Me.AvatarPosition.Y * MazeScreen.TileSize) * MazeScreen.ScaleFactor.Y, MazeScreen.TileSize, MazeScreen.TileSize), Me.PlayerColor)
                If DetectCollision(Me) Then
                    FindCollision(Me).DecreaseHealth(PUNCH_DAMAGE)
                End If
                Me.HitBox.X += 4

            Case Direction.Right
                Me.HitBox.X += 4
                Globals.SpriteBatch.Draw(Textures.Pirate, New Rectangle(((Me.AvatarPosition.X * MazeScreen.TileSize)) * MazeScreen.ScaleFactor.X, ((Me.AvatarPosition.Y * MazeScreen.TileSize) + 4) * MazeScreen.ScaleFactor.Y, MazeScreen.TileSize, MazeScreen.TileSize), Me.PlayerColor)
                If DetectCollision(Me) Then
                    FindCollision(Me).DecreaseHealth(PUNCH_DAMAGE)
                End If
                Me.HitBox.X -= 4

            Case Direction.Up
                Me.HitBox.Y -= 4
                Globals.SpriteBatch.Draw(Textures.Pirate, New Rectangle(((Me.AvatarPosition.X * MazeScreen.TileSize) + 4) * MazeScreen.ScaleFactor.X, (Me.AvatarPosition.Y * MazeScreen.TileSize) * MazeScreen.ScaleFactor.Y, MazeScreen.TileSize, MazeScreen.TileSize), Me.PlayerColor)
                If DetectCollision(Me) Then
                    FindCollision(Me).DecreaseHealth(PUNCH_DAMAGE)
                End If
                Me.HitBox.Y -= 4
        End Select

        Globals.SpriteBatch.End()
        Globals.Graphics.GraphicsDevice.Present()

        'TODO
        'Also play sound for swing, sound for hit
    End Sub

End Class
