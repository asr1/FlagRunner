Public Enum Frequency
    None = 0
    Low = 5
    Medium = 30
    High = 70
End Enum

Partial Public Class Options
    Private Shared AllItems As List(Of Item) 'A list containing every item in play.
    Private Shared TotalFrequency As Frequency 'The frequency with which items will be generated

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
        Dim Rand As Random = New Random
        'Pick a random item
        Dim item As Item = AllItems(Rand.Next(0, AllItems.Count - 1))
        'Then see if we pick it. 
        Dim i As Integer = Rand.Next(0, 100)
        If i < item.getRelFreq Then
            Return item
        Else
            Return GetItem() 'Reroll
        End If
    End Function
End Class
