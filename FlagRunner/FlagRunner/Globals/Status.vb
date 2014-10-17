Public Class Status
    Public Shared Player1 As Boolean = GamePad.GetState(PlayerIndex.One).IsConnected
    Public Shared Player2 As Boolean = GamePad.GetState(PlayerIndex.Two).IsConnected
    Public Shared Player3 As Boolean = GamePad.GetState(PlayerIndex.Three).IsConnected
    Public Shared Player4 As Boolean = GamePad.GetState(PlayerIndex.Four).IsConnected
End Class
