Public MustInherit Class Weapon
    Public FireTime As Integer 'The amount of time between a trigger press and a shot fired
    Public RelaodTime As Integer 'The amount of time between expending a full clip and being able to fire again
    Public AmmoCapacity As Integer 'The amount of ammo in one full clip
    Public RemainingAmmo As Integer 'The amount of ammo until next reload, we never run out.
    Public IsMelee As Boolean 'If false, is ranged weapon
    Public TwoHanded As Boolean 'If false, one handed
    Public MaxRange As Boolean 'How far will a projectile go?
    Public Damage As Integer 'How much damage will getting hit cost me?
    Public SoundEffect As String 'TODO
End Class
