'Imports System.Collections

'Public Enum directionT
'    North
'    East
'    South
'    West
'End Enum

'Public Class MazeSolver
'    'The actual maze
'    Dim Maze(,) As Char
'    'Tracing our location in the maze
'    Dim Path As ArrayList

'    Dim Rows As Integer
'    Dim Cols As Integer

'    'The pt is the starting point, goal is our present out point
'    Public Shared Function SolveMaze(pt As Vector2, Goal As Integer) As Boolean
'        If OutsideMaze(pt, Goal) = True Then
'            Return True
'        End If
'        If IsMarked(pt) = True Then
'            Return False
'        End If
'        MarkSquare(pt)

'        For i As Integer = 0 To 3 Step 1
'            Dim dir As directionT = i
'            If Not WallExists(pt, dir) Then
'                If SolveMaze(AdjacentPoint(pt, dir), Goal) Then
'                    Return True
'                End If
'            End If
'        Next
'        UnMarkSquare(pt)
'        Return False
'    End Function

'    'Returns true if point is equal to the goal
'    'Goal refers to which player's starting position we are trying to find
'    'The goals are treated as the "exit" to the maze.
'    Private Shared Function OutsideMaze(pt As Vector2, Goal As Integer) As Boolean
'        Select Case Goal
'            Case 1
'                If pt = New Vector2(1, 1) Then
'                    Return True
'                End If
'            Case 2
'                If pt = New Vector2(MazeScreen.getMapSize.X - 1, MazeScreen.getMapSize.Y - 1) Then
'                    Return True
'                End If
'            Case 3
'                If pt = New Vector2(1, MazeScreen.getMapSize.Y - 1) Then
'                    Return True
'                End If
'            Case 4
'                If pt = New Vector2(MazeScreen.getMapSize.X - 1, 1) Then
'                    Return True
'                End If
'        End Select
'        Return False
'    End Function

'    'Returns true if there is a wall in the direction indicated from pt
'    Private Shared Function WallExists(pt As Vector2, dir As directionT) As Boolean
'        Select Case dir
'            Case directionT.North
'                Return MapBase.TileList(pt.X, pt.Y - 1).isBlocked
'            Case directionT.South
'                Return MapBase.TileList(pt.X, pt.Y + 1).isBlocked
'            Case directionT.East
'                Return MapBase.TileList(pt.X + 1, pt.Y).isBlocked
'            Case directionT.West
'                Return MapBase.TileList(pt.X - 1, pt.Y).isBlocked
'        End Select
'        Return False
'    End Function

'    'Returns the point in the direction indicated from pt
'    Private Shared Function AdjacentPoint(pt As Vector2, dir As Direction) As Vector2
'        Select Case dir
'            Case directionT.North
'                Return New Vector2(pt.X, pt.Y - 1)
'            Case directionT.South
'                Return New Vector2(pt.X, pt.Y + 1)
'            Case directionT.East
'                Return New Vector2(pt.X + 1, pt.Y)
'            Case directionT.West
'                Return New Vector2(pt.X - 1, pt.Y)
'        End Select
'    End Function

'    'Marks a square as having been traversed
'    Private Shared Sub MarkSquare(pt As Vector2)
'        MapBase.TileList(pt.X, pt.Y).isMarked = True
'    End Sub

'    'Unmarks a square as having been traversed.
'    Private Shared Sub UnMarkSquare(pt As Vector2)
'        MapBase.TileList(pt.X, pt.Y).isMarked = False
'    End Sub

'    'Checks the status of a square as having been traversed
'    Private Shared Function IsMarked(pt As Vector2) As Boolean
'        Return MapBase.TileList(pt.X, pt.Y).isMarked
'    End Function

'End Class
