Public Class Fonts
    Public Shared Georgia_16 As SpriteFont
    Public Shared Centaur_10 As SpriteFont
    Public Shared Georgia_16_Bold As SpriteFont

    Public Shared Sub load()
        Georgia_16 = Globals.Content.Load(Of SpriteFont)("Fonts/Georgia_16")
        Centaur_10 = Globals.Content.Load(Of SpriteFont)("Fonts/Centaur_10")
        Georgia_16_Bold = Globals.Content.Load(Of SpriteFont)("Fonts/Georgia_16_Bold")
    End Sub

End Class


