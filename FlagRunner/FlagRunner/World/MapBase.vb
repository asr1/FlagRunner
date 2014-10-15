Public Class MapBase
    Public Shared TileList(0, 0) As Tile
    Public Const RESOLUTION As Integer = 10
    Dim Rand As Random = New Random

    Public Sub New(Width As Integer, Height As Integer, Start1 As Vector2, Start2 As Vector2)

        ReDim TileList(Width, Height)

        Initialize(Width, Height)
        GenerateMaze(0, Width, 0, Height)
        UpdateTiles(Width, Height)
        'Generate maps until we have a path from corner 1 to 4 and corner 2 to 3
        '  While Not MazeSolver.SolveMaze(New Vector2(1, 1), 4) = True And Not MazeSolver.SolveMaze(New Vector2(1, MazeScreen.getMapSize.X - 1), 3) = True
        'GenerateMaze(1, Width - 1, 1, Height - 1)
        '  UpdateTiles(Width, Height)
        ' End While

    End Sub

    'Iterate through entire map and set properties appropiately based on type
    Public Sub UpdateTiles(Width As Integer, Height As Integer)
        For x = 0 To Width
            For y = 0 To Height
                With TileList(x, y)
                    .isMarked = False 'ALL OF THEM ARE UNMARKED
                    Select Case .TerrainType
                        Case TileType.Cobble
                            .TileGFX = Textures.Cobble
                            .isBlocked = False
                        Case TileType.Wall
                            .TileGFX = Textures.Wall
                            .isBlocked = True
                        Case TileType.CrackedWall
                            .TileGFX = Textures.CrackedWall
                            .isBlocked = True
                        Case TileType.Base
                            .TileGFX = Textures.BaseTile
                            .isBlocked = False
                    End Select
                End With
            Next
        Next
    End Sub

    'Clear tiles and create new.
    Private Sub Initialize(Width As Integer, Height As Integer)
        Dim Row As Integer = Width ' * 2 + 1 'The effective number of rows
        Dim Col As Integer = Height ' * 2 + 1

        'Clear maze
        For x = 0 To Row - 1
            For y = 0 To Col - 1
                TileList(x, y) = New Tile
                TileList(x, y).TerrainType = TileType.Cobble
            Next
        Next

        'Make Outer Walls
        For x = 0 To Row
            TileList(x, 0).TerrainType = TileType.Wall
            TileList(x, Col).TerrainType = TileType.Wall
        Next

        For x = 0 To Col
            TileList(0, x).TerrainType = TileType.Wall
            TileList(Row, x).TerrainType = TileType.Wall
        Next

        'Draw bases
        TileList(1, 1).TerrainType = TileType.Base
        TileList(MazeScreen.getMapSize.X - 1, 1).TerrainType = TileType.Base

    End Sub

    'It's possible that references to right and left are actually to top and bottom, and vice-versa
    'By extension, it's not out of the question that divHorz() and divVert() are inversed 'Confirmed that these are inversed.
    Private Sub GenerateMaze(Left As Integer, Right As Integer, Top As Integer, Bottom As Integer)
        Dim Width As Integer = Right - Left
        Dim Height As Integer = Bottom - Top

        'Ensure we have room to split
        'And choose a direction
        If Width > RESOLUTION And Height > RESOLUTION Then
            If Width > Height Then
                DivideVert(Left, Right, Top, Bottom)
            ElseIf Height > Width Then
                DivideHorz(Left, Right, Top, Bottom)
            Else 'They're equal
                Dim Choice As Integer = Rand.Next(0, 2)
                If (Choice = 0) Then
                    DivideHorz(Left, Right, Top, Bottom)
                Else
                    DivideVert(Left, Right, Top, Bottom)
                End If

            End If
        ElseIf Width > RESOLUTION And Height <= RESOLUTION Then
            DivideVert(Left, Right, Top, Bottom)
        ElseIf Width <= RESOLUTION And Height > RESOLUTION Then
            DivideHorz(Left, Right, Top, Bottom)
        End If

    End Sub

    'If sections are being blocked off, the problem here
    'Also, this is horizontal
    Private Sub DivideVert(Left As Integer, Right As Integer, Top As Integer, Bottom As Integer)
        'Find a random, even point to divide at
        Dim divPos As Integer = Left + 2 + Rand.Next(0, (Right - Left - 1) / 2 - 1) * 2


        For x As Integer = Top To Bottom - 1
            'Have a 5% chance it's breakable
            Dim WallCheck As Integer = Rand.Next(0, 100)
            Dim WallType As TileType = TileType.Wall
            If WallCheck < 5 Then
                WallType = TileType.CrackedWall
            End If
            TileList(x, divPos).TerrainType = WallType
        Next

        'Grab an odd space for a walkway
        Dim WalkWay As Integer = Top + Rand.Next(0, (Bottom - Top) / 2 - 1) * 2 + 1

        TileList(WalkWay, divPos).TerrainType = TileType.Cobble

        'Recursion time
        GenerateMaze(Left, divPos, Top, Bottom)
        GenerateMaze(divPos, Right, Top, Bottom)

    End Sub


    Private Sub DivideHorz(Left As Integer, Right As Integer, Top As Integer, Bottom As Integer)
        'Find a random, even point to divide at
        Dim divPos As Integer = Top + 2 + Rand.Next(0, (Bottom - Top - 1) / 2 - 1) * 2
        If divPos Mod 2 = 1 Then
            divPos += 1
        End If

        'Fill in that maze 
        For x As Integer = Left To Right - 1
            'Have a 5% chance it's breakable
            Dim WallCheck As Integer = Rand.Next(0, 100)
            Dim WallType As TileType = TileType.Wall
            If WallCheck < 5 Then
                WallType = TileType.CrackedWall
            End If
            TileList(divPos, x).TerrainType = WallType
        Next

        'Make way for walking (odd integer)
        Dim WalkWay As Integer = Left + Rand.Next(0, (Right - Left) / 2 - 1) * 2 + 1

        TileList(divPos, WalkWay).TerrainType = TileType.Cobble

        'Now we have two new parts who both get this treatment
        'RECURSION!
        GenerateMaze(Left, Right, Top, divPos)
        GenerateMaze(Left, Right, divPos, Bottom)



    End Sub


    'Return a tile of the given type
    Private Function GetTileSource(TType As TileType) As Rectangle
        Return New Rectangle(0, 0, 31, 31)
    End Function


End Class
