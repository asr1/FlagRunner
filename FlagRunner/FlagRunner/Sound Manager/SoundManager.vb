Public Class SoundManager

    Public Shared SoundOn As Boolean = True 'Tracks the stats of the music being played
    'An array of every song. Used for shuffling.
    Public Shared Songs() As Song = {Sounds.Track1, Sounds.Track2, Sounds.Track3, Sounds.Track4, Sounds.Track5, Sounds.Track6, Sounds.Track7, Sounds.Track8, Sounds.Track9}

    'Used to enable all sounds
        Public Shared Sub Play()
        Dim rand As Random = New Random
        'Shuffle music.
        Dim song As Song = Songs.GetValue(rand.Next(0, Songs.Length - 1))
            MediaPlayer.IsShuffled = True
            MediaPlayer.Play(song)
            soundOn = True
        End Sub

    Public Shared Sub SetVolume(vol As Double)
        MediaPlayer.Volume = MathHelper.Clamp(MediaPlayer.Volume + vol, 0.0F, 1.0F)
    End Sub

    Public Shared Function GetVolume() As Double
        Return MediaPlayer.Volume
    End Function
    'Resumes music at the last place it was paused
    Public Shared Sub ResumeMusic()
        MediaPlayer.Resume()
        SoundOn = True
    End Sub


    'Used to disable all sounds
    Public Shared Sub Pause()
        MediaPlayer.Pause()
        SoundOn = False
    End Sub


End Class
