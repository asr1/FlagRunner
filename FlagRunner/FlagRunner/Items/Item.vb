Public MustInherit Class Item
    Public GFX As Texture2D
    Private RelFreq As Frequency = Frequency.Med ' The relative frequency with which this item is selected

    Public Function getRelFreq() As Frequency
        Return RelFreq
    End Function

    Public Sub setRelFreq(newFreq As Frequency)
        RelFreq = newFreq
    End Sub
End Class
