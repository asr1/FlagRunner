Public Enum Frequency
    None = 0
    Low = 2
    Medium = 8
    High = 12
End Enum

Partial Public Class Options
    Private Shared AllItems As New List(Of Item) 'A list containing every item in play.
    Private Shared TotalFrequency As Frequency = Frequency.High 'The frequency with which items will be generated

    Public Shared Sub InitializeItems()
        Dim t As Trident = New Trident
        AllItems.Add(t)
    End Sub

    Public Shared Function getTotalFrequency() As Frequency
        Return TotalFrequency
    End Function

    Public Shared Sub setTotalFrequency(newFreq As Frequency)
        TotalFrequency = newFreq
    End Sub

    'Returns an item based on frequency selected
    'This is called when populating tiles initially
    'If a random number 0,100 is less than totalFrequency.
    'Each item has an equal chance of being selected
    'But the chance of being selected is equal to
    'The relative frequency /100. Otherwise it will reroll.
    Public Shared Function GetItem() As Item
        'Loop until we get a value
        While True
            Dim Rand As Random = New Random
            'Pick a random item
            Dim item As Item = AllItems(Rand.Next(0, AllItems.Count - 1))
            'Then see if we pick it. 
            Dim i As Integer = Rand.Next(0, 100)
            If i < item.getRelFreq Then
                Return item
            End If
        End While
        'This will never be reached
        Return Nothing
    End Function

End Class
