Public Class Status
    Public Shared Function isConnected(i As PlayerIndex) As Boolean
        Return GamePad.GetState(PlayerIndex.One).IsConnected
    End Function

End Class
