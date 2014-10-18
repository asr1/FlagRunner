Public Enum Direction
    None
    Down
    Left
    Right
    Up
End Enum

'There is no player update function; it would cause dependency issues.
'Player updates are handled in the Mazescreen Class.

'Each character that is controlled.
Public Class Player
    Private Shared playerNum = 1 'The number of players
    Private UniquePlayerNum As Integer 'The unique identifier for this specific player



    'Used to toggle if a player is created
    Public NeedsUpdating As Boolean = False

    'Map coordinates
    Public AvatarPosition As Vector2

    'Avatar offset for smooth walking
    Public AvatarOffset As Vector2 = New Vector2(0, 0)

    'Movement
    Public AvatarMoving As Boolean = False
    Public BaseSpeed As Integer = 3
    Public MoveSpeed As Integer 'This updates based on status effects
    Public MoveDir As Direction = Direction.None
    Public LastDir As Direction = Direction.Down
    Private AvatarFrame As Integer = 0

    'Source Rectangle
    Private SRect As Rectangle

    'Colision
    Public HitBox As Rectangle

    'TODO find a way to mark player number so that we know which player to respawn. Possibly just have  a respawn function
    Public Sub New()
        'Set attributes
        MoveSpeed = BaseSpeed
        UniquePlayerNum = playerNum
        HitBox = New Rectangle(Me.AvatarPosition.X * MazeScreen.TileSize, Me.AvatarPosition.Y * MazeScreen.TileSize, MazeScreen.TileSize, MazeScreen.TileSize)



        'Position them appropriately
        If playerNum = 1 Then
            AvatarPosition = New Vector2(1, 1)
        ElseIf playerNum = 2 Then
            AvatarPosition = New Vector2(MazeScreen.getMapSize.X - 1, 1)
        ElseIf playerNum = 3 Then
            AvatarPosition = New Vector2(1, MazeScreen.getMapSize.Y - 1)
        ElseIf playerNum = 4 Then
            AvatarPosition = New Vector2(MazeScreen.getMapSize.X - 1, MazeScreen.getMapSize.Y - 1)
        End If
        NeedsUpdating = True
        playerNum += 1
    End Sub


    'Getter for UniquePlayerNum
    Public Function GetPlayerID() As Integer
        Return UniquePlayerNum
    End Function

    ''A new function used for respawning that takes in the player's number
    ''To ensure they are created appropriately
    'Public Sub New(Pnum As Integer)
    '    playerNum = Pnum

    'End Sub

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

    'Returns true if the unique player identifiers are the same, false otherwise.
    Public Overrides Function Equals(obj As Object) As Boolean
        'If it's null....
        If IsNothing(obj) Then
            Return False
        End If

        'Or not a player
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


End Class
