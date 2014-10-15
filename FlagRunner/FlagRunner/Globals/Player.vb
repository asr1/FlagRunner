Public Enum Direction
    None
    Down
    Left
    Right
    Up
End Enum
Public Class Player
    Private Shared playerNum = 1

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



    'TODO find a safer way to ensure no out of bounds.
    'TODO always start in 1,1 or the equivalent; clear that square
    'TODO find a way to mark player number so that we know which player to respawn. Possibly just have  a respawn function
    'Also investigate the findOpen() function.
    Public Sub New()
        MoveSpeed = BaseSpeed
        If playerNum = 1 Then
            'AvatarPosition = MazeScreen.FindOpen(0, 0)
            AvatarPosition = New Vector2(1, 1)
        ElseIf playerNum = 2 Then
            'AvatarPosition = MazeScreen.FindOpen(MazeScreen.getMapSize.X - 5, 0)
            AvatarPosition = New Vector2(MazeScreen.getMapSize.X - 1, 1)
        ElseIf playerNum = 3 Then
            'AvatarPosition = MazeScreen.FindOpen(0, MazeScreen.getMapSize.Y - 5)
            AvatarPosition = New Vector2(0, MazeScreen.getMapSize.Y - 1)
        ElseIf playerNum = 4 Then
            'AvatarPosition = MazeScreen.FindOpen(MazeScreen.getMapSize.X - 5, MazeScreen.getMapSize.Y - 5)
            AvatarPosition = New Vector2(MazeScreen.getMapSize.X - 1, MazeScreen.getMapSize.Y - 1)
        End If
        playerNum += 1
    End Sub

    ''A new function used for respawning that takes in the player's number
    ''To ensure they are created appropriately
    'Public Sub New(Pnum As Integer)
    '    playerNum = Pnum

    'End Sub

    Public Sub Move(Dir As Direction)
        Me.MoveDir = Dir

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
        If MapBase.TileList(AvX, AvY).isBlocked = False Then
            AvatarMoving = True
            MoveDir = Dir
        End If
    End Sub

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
