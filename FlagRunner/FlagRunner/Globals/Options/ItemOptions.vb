Public Enum Frequency
    None = 0
    Low = 2
    Medium = 5
    High = 12
End Enum

Partial Public Class Options
    Private Shared AllItems As New List(Of Item) 'A list containing every item in play.
    Private Shared TotalFrequency As Frequency = Frequency.Medium 'The frequency with which items will be generated

    Public Shared Sub InitializeItems()
        AllItems.Add(New Trident)
    End Sub

    Public Shared Function getTotalFrequency() As Frequency
        Return TotalFrequency
    End Function

    Public Shared Sub setTotalFrequency(newFreq As Frequency)
        TotalFrequency = newFreq
    End Sub

    'Returns true if every item is set to have zero
    'Probability. Else, false.
    Private Shared Function CheckIfEmpty() As Boolean
        For Each i As Item In AllItems
            If Not i.getRelFreq = Frequency.None Then
                Return False
            End If
        Next
        Return True 'If we got through all of them without 
        'Finding a single one that wasn't none, they're all none.
    End Function


    'Returns an item based on frequency selected
    'This is called when populating tiles initially
    'If a random number 0,100 is less than totalFrequency.
    'Each item has an equal chance of being selected
    'But the chance of being selected is equal to
    'The relative frequency /100. Otherwise it will reroll.
    Public Shared Function GetItem() As Item
        'If the chance of all items is not zero,
        'But the chance of every item is, abort.
        'We could also handle this by setting the 
        'Overall frequency to zero if each item's relfreq
        'is equal to frequency.none
        If CheckIfEmpty() = True Then
            Return Nothing
        End If
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
