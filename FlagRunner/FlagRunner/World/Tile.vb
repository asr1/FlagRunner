Public Enum TileType
    Cobble 'Walkable background
    Wall 'Cannot be destroyed. Always exterior.
    CrackedWall 'Cracked wall can be destroyed with rocket launcher. 
    Base 'It's walkable, a spawn point, and a score point.
End Enum

Public Structure Tile
    Public Property TerrainType As TileType 'The terrain type
    Public Property TileGFX As Texture2D    'The image sprite
    Public Property SrcRect As Rectangle    'The source from whichh we get the tile
    Public Property Location As Vector2     'Where we will draw it on screen
    Public Property isMarked As Boolean   'Used for tracking map stats

    'Tile Actions
    Public Property isBlocked As Boolean

End Structure
