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
End Class
