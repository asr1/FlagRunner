Public Class Sounds
    Public Shared Track1 As Song
    Public Shared Track2 As Song
    Public Shared Track3 As Song
    Public Shared Track4 As Song
    Public Shared Track5 As Song
    Public Shared Track6 As Song
    Public Shared Track7 As Song
    Public Shared Track8 As Song
    Public Shared Track9 As Song

    Public Shared Sub load()
        Track1 = Globals.Content.Load(Of Song)("Songs/Lohengrin")
        Track2 = Globals.Content.Load(Of Song)("Songs/Meistersinger")
        Track3 = Globals.Content.Load(Of Song)("Songs/Rienzi")
        Track4 = Globals.Content.Load(Of Song)("Songs/Siegfried")
        Track5 = Globals.Content.Load(Of Song)("Songs/tannhauser")
        Track6 = Globals.Content.Load(Of Song)("Songs/Valkyries")
        Track7 = Globals.Content.Load(Of Song)("Songs/1812")
        Track8 = Globals.Content.Load(Of Song)("Songs/Athens")
        Track9 = Globals.Content.Load(Of Song)("Songs/Sabre")
    End Sub

End Class
