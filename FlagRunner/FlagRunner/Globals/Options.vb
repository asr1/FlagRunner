Public Enum DisplayHealth
    None
    Bar
    Number
End Enum

Public Class Options
    Private Shared HealthBarOption As DisplayHealth = DisplayHealth.Bar 'Toggle for health bar display (none, bar, num)
    Private Shared DebugMode As Boolean = False



    Public Shared Function GetHealthBarOption() As DisplayHealth
        Return HealthBarOption
    End Function

    Public Shared Sub SetHealthBarOption(HealthOp As DisplayHealth)
        HealthBarOption = HealthOp
    End Sub

    Public Shared Function GetDebugMode() As Boolean
        Return DebugMode
    End Function

End Class
