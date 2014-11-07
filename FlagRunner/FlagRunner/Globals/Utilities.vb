Public Class Utilities
    'Function to return greatest value of enum
    Public Shared Function MaxEnum(ByVal EnumType As Type) As Long

        Dim max As Long = 0
        Dim gotInitialValue As Boolean = False
        For Each i In [Enum].GetValues(EnumType)
            If gotInitialValue = False Then
                gotInitialValue = True
                max = CLng(i)
            Else
                If CInt(i) > max Then
                    max = CLng(i)
                End If
            End If
        Next
        Return max
    End Function

    'Function to return smallest value of enum
    Public Shared Function MinEnum(ByVal EnumType As Type) As Long

        Dim Min As Long = Long.MaxValue
        Dim gotInitialValue As Boolean = False
        For Each i In [Enum].GetValues(EnumType)
            If gotInitialValue = False Then
                gotInitialValue = True
                Min = CLng(i)
            Else
                If CLng(i) < Min Then
                    Min = CLng(i)
                End If
            End If
        Next
        Return Min
    End Function

    'Returns the number one smaller than the one inputted in the given enum
    'OR if the number given is the smallest, returns the largest number in 
    'The given enum.
    'PRECONDITIONS: The enum must be sorted smallest to largest
    Public Shared Function NextSmallestEnum(ByVal EnumType As Type, Value As Integer) As Long

        Dim FirstElement As Boolean = True
        Dim prev As Long
        For Each i In [Enum].GetValues(EnumType)
            If CLng(i) < Value Then
                prev = CLng(i)
                FirstElement = False
            Else
                If CLng(i) = Value And FirstElement = True Then
                    Return MaxEnum(EnumType)
                Else
                    Return prev
                End If
            End If
        Next
        'If we get all the way to the end without finding anything
        'Shouldn't be reached
        Return MaxEnum(EnumType)
    End Function

    'Returns the number one greater than the one inputted in the given enum
    'OR if the number given is the biggest, returns the smallest number in 
    'The given enum.
    'PRECONDITIONS: The enum must be sorted smallest to largest
    Public Shared Function NextGreatestEnum(ByVal EnumType As Type, Value As Integer) As Long
        For Each i In [Enum].GetValues(EnumType)
            'If I pass in the highest value, return the lowest
            If Value = MaxEnum(EnumType) Then
                Return MinEnum(EnumType)
            End If
            'Else, iterate through each value in the enum
            If CLng(i) <= Value Then
                'Do nothing
            Else
                'Until we find one higher than ourself
                Return CLng(i)
            End If

        Next
        'If we get all the way to the end without finding anything
        'Shouldn't be reached
        Return MaxEnum(EnumType)
    End Function

    'Returns true if the game is over
    Public Shared Function CheckForWin() As Boolean
        If Game1.GetGameMode = GameMode.LastManStanding Or Game1.GetGameMode = GameMode.Nightlight Then
            Dim livingPlayers As Integer = 0
            For Each Player As Player In MazeScreen.ConnectedPlayers
                If Player IsNot Nothing Then
                    If Player.Living Then
                        livingPlayers += 1
                    End If
                End If
            Next
            If livingPlayers < 2 Then
                Return True
            End If
            Return False     'We've still got people playing.
        Else ' Other game modes
            For Each Player As Player In MazeScreen.ConnectedPlayers
                If Player IsNot Nothing Then
                    If Player.VictoryPoints = Options.getVictoryPoints Then
                        Return True
                    End If
                End If
            Next
            Return False 'No players have won.
        End If
    End Function

    'Returns the name of the player that won
    'Preconditions: there must be a victor
    Public Shared Function GetWinner() As String
        If CheckForWin() = True Then
            If Game1.GetGameMode = GameMode.LastManStanding Or Game1.GetGameMode = GameMode.Nightlight Then
                For Each Player As Player In MazeScreen.ConnectedPlayers
                    If Player IsNot Nothing Then
                        If Player.Living Then
                            'We know by this point that there is only one victor.
                            Return "Player " & Player.GetPlayerID
                        End If
                    End If
                Next
                'No return, never reached
            Else ' Other game modes
                For Each Player As Player In MazeScreen.ConnectedPlayers
                    If Player IsNot Nothing Then
                        If Player.VictoryPoints = Options.getVictoryPoints Then
                            Return "Player " & Player.GetPlayerID
                        End If
                    End If
                Next
            End If
        End If

        Return "" ' This is never reached.
    End Function

End Class
