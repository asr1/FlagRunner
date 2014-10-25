Public Enum DisplayHealth
    None
    Bar
    Number
End Enum

Public Class Options
    Private Shared HealthBarOption As DisplayHealth = DisplayHealth.Bar 'Toggle for health bar display (none, bar, num)
    Private Shared DebugMode As Boolean = False
    Private Shared Resolution As Integer = 10


    Public Shared Function GetHealthBarOption() As DisplayHealth
        Return HealthBarOption
    End Function

    Public Shared Sub SetHealthBarOption(HealthOp As DisplayHealth)
        HealthBarOption = HealthOp
    End Sub

    Public Shared Function GetDebugMode() As Boolean
        Return DebugMode
    End Function

    Public Shared Sub DecreaseResolution()
        If Resolution > 3 Then
            Resolution -= 1
        End If
    End Sub

    Public Shared Sub IncreaseResoltuion()
        If Resolution < 16 Then
            Resolution += 1
        End If
    End Sub

    Public Shared Function GetResolution() As Integer
        Return Resolution
    End Function

End Class
