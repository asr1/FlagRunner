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

Public Enum Breakability
    None = 0
    Few = 5
    Sparse = 10
    Seldom = 20
    Intermittent = 30
    Uncommon = 40
    Common = 50
    Frequent = 60
    Most = 80
    All = 100
End Enum

Public Enum Health
    Crippled = 2
    Weak = 5
    Standard = 10
    Tough = 20
    Vigorous = 35
    Durable = 50
End Enum

Public Enum VPoints
    One = 1
    Three = 3
    Five = 5
    Ten = 10
    Fifteen = 15
    Twenty = 20
End Enum

Public Class Options
    Private Shared HealthBarOption As DisplayHealth = DisplayHealth.Bar 'Toggle for health bar display (none, bar, num)
    Private Shared DebugMode As Boolean = False
    Private Shared Resolution As ResolutionSize = ResolutionSize.Balanced
    Private Shared BreakabilityConstant As Breakability = Breakability.Few
    Private Shared Health As Health = Health.Standard
    Private Shared VictoryPoints As VPoints = VPoints.Five

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

    Public Shared Function GetBreakability() As Breakability
        Return BreakabilityConstant
    End Function

    Public Shared Sub SetBreakability(newBreak As Breakability)
        BreakabilityConstant = newBreak
    End Sub

    Public Shared Sub SetHealth(newHealth As Health)
        Health = newHealth
    End Sub

    Public Shared Function getHealth() As Health
        Return Health
    End Function

    Public Shared Sub SetVictoryPoitns(newPoint As VPoints)
        VictoryPoints = newPoint
    End Sub

    Public Shared Function getVictoryPoints() As VPoints
        Return VictoryPoints
    End Function
End Class
