'Loads all textures into project
Public Class Textures
    Public Shared BlackGradient As Texture2D
    Public Shared Cobble As Texture2D
    Public Shared Wall As Texture2D
    Public Shared CrackedWall As Texture2D
    Public Shared BaseTile As Texture2D

    'Avatars
    Public Shared Pirate As Texture2D


    Public Shared Sub Load()
        BlackGradient = Globals.Content.Load(Of Texture2D)("GFX/blackgradient")
        Cobble = Globals.Content.Load(Of Texture2D)("GFX/Cobble")
        Wall = Globals.Content.Load(Of Texture2D)("GFX/Wall")
        CrackedWall = Globals.Content.Load(Of Texture2D)("GFX/CrackedWall")
        Pirate = Globals.Content.Load(Of Texture2D)("GFX/Avatars/Pirate")
        BaseTile = Globals.Content.Load(Of Texture2D)("GFX/Base")
    End Sub
End Class


