﻿Public Class Trident
    Inherits Weapon

    Public Sub New()
        GFX = Textures.Trident
    End Sub

    Public Overrides Function getSourceRect(dir As Direction) As Rectangle
        Return New Rectangle(8, 9, 15, 21)
    End Function

    Public Overrides Sub Attack(dir As Direction, player As Player)

    End Sub

End Class
