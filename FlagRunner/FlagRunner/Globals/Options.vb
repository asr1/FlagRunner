Public Enum DisplayHealth
    None
    Bar
    Number
End Enum

Public Enum ResolutionSize
    Corridors = 2
    Tight = 5
    Close = 7
    Balanced = 10
    Vast = 12
    Spacious = 16
    Open = 24
End Enum

Public Class Options
    Private Shared HealthBarOption As DisplayHealth = DisplayHealth.Bar 'Toggle for health bar display (none, bar, num)
    Private Shared DebugMode As Boolean = False
    Private Shared Resolution As ResolutionSize = ResolutionSize.Balanced


    Public Shared Function GetHealthBarOption() As DisplayHealth
        Return HealthBarOption
    End Function

    Public Shared Sub SetHealthBarOption(HealthOp As DisplayHealth)
        HealthBarOption = HealthOp
    End Sub

    Public Shared Function GetDebugMode() As Boolean
        Return DebugMode
    End Function

    Public Shared Sub SetResolution(newRes As ResolutionSize)
        Resolution = newRes
    End Sub

    Public Shared Function GetResolution() As ResolutionSize
        Return Resolution
    End Function

End Class
