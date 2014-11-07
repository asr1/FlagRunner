Public Class Status
    Public Shared Function isConnected(i As PlayerIndex) As Boolean
        Return GamePad.GetState(i).IsConnected
    End Function
End Class
