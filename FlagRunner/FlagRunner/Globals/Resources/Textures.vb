'Loads all textures into project
Public Class Textures
    Public Shared BlackGradient As Texture2D
    Public Shared Cobble As Texture2D
    Public Shared Wall As Texture2D
    Public Shared CrackedWall As Texture2D
    Public Shared BaseTile As Texture2D
    Public Shared StatusLight As Texture2D
    Public Shared HealthBar As Texture2D
    Public Shared Trident As Texture2D
    Public Shared LightTexture As Texture2D
    Public Shared LightGun As Texture2D
    Public Shared AButton As Texture2D

    'Avatars
    Public Shared Pirate As Texture2D

    Public Shared Sub Load()
        BlackGradient = Globals.Content.Load(Of Texture2D)("GFX/blackgradient")
        Cobble = Globals.Content.Load(Of Texture2D)("GFX/Tiles/Cobble")
        Wall = Globals.Content.Load(Of Texture2D)("GFX/Tiles/Wall")
        CrackedWall = Globals.Content.Load(Of Texture2D)("GFX/Tiles/CrackedWall")
        Pirate = Globals.Content.Load(Of Texture2D)("GFX/Avatars/Pirate")
        BaseTile = Globals.Content.Load(Of Texture2D)("GFX/Tiles/Base")
        StatusLight = Globals.Content.Load(Of Texture2D)("GFX/Connected")
        HealthBar = Globals.Content.Load(Of Texture2D)("GFX/Healthbar")
        Trident = Globals.Content.Load(Of Texture2D)("GFX/Items/Weapons/Trident")
        LightTexture = LightTextureBuilder.CreatePointLight(Globals.Graphics.GraphicsDevice, 512)
        LightGun = Globals.Content.Load(Of Texture2D)("GFX/Items/Weapons/Lightgun")
        AButton = Globals.Content.Load(Of Texture2D)("GFX/Xbox360_Button_A")
    End Sub

    Public Shared Function GetHealthBarSource() As Rectangle
        Return New Rectangle(64, 0, 64, 64)
    End Function
End Class


