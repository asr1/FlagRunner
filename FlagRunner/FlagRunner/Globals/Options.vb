Public Enum DisplayHealth
    None
    Bar
    Number
End Enum

Public Class Options
    Public Shared HeathBarOption As DisplayHealth = DisplayHealth.Bar 'Toggle for health bar display (none, bar, num)
    Public Shared DebugMode As Boolean = False

End Class
