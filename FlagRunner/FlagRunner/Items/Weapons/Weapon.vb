Public MustInherit Class Weapon
    Inherits Item
    Public Shared FireTime As Integer 'The amount of time between a trigger press and a shot fired
    Public Shared RelaodTime As Integer 'The amount of time between expending a full clip and being able to fire again
    Public Shared AmmoCapacity As Integer 'The amount of ammo in one full clip
    Public Shared RemainingAmmo As Integer 'The amount of ammo until next reload, we never run out.
    Public Shared IsMelee As Boolean 'If false, is ranged weapon
    Public Shared TwoHanded As Boolean 'If false, one handed
    Public Shared MaxRange As Boolean 'How far will a projectile go?
    Public Shared Damage As Double 'How much damage will getting hit cost me?
    Public Shared SoundEffect As String 'TODO


    Public MustOverride Function getSourceRect(dir As Direction) As Rectangle

    Public MustOverride Sub Attack(dir As Direction, player As Player)

End Class
