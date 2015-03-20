Public MustInherit Class Weapon
    Inherits Item
    Public Shared FireTime As Integer 'The amount of time between a trigger press and a shot fired
    Public Shared RelaodTime As Integer 'The amount of time between expending a full clip and being able to fire again
    Public Shared AmmoCapacity As Integer 'The amount of ammo in one full clip
    Public Shared RemainingAmmo As Integer 'The amount of ammo until next reload, we never run out.
    Public Shared IsMelee As Boolean 'If false, is ranged weapon
    Public Shared TwoHanded As Boolean 'If false, one handed
    Public Shared MaxRange As Boolean 'How far will a projectile go?
    Public Shared Damage As Double = 0 'How much damage will getting hit cost me?
    Public Shared sound As SoundEffect = Nothing 'TODO


    Public MustOverride Function getSourceRect(dir As Direction) As Rectangle

    'Dir: The direction the player is attacking in, player: the attacking player
    Public MustOverride Sub Attack(dir As Direction, attacker As Player)

    Public Sub playSound()
        If Not IsNothing(sound) Then
            sound.Play()
        End If
    End Sub

End Class
