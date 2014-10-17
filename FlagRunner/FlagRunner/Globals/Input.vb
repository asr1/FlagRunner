'This class manages keyboard AND XBOX controller input
'Basically it cleans up our functions a bit. It's all technically
'Already implemented, but this makes it a bit neater.
Public Class Input
    'Keyboard
    Shared CurrentKeyState As KeyboardState
    Shared LastKeyState As KeyboardState

    'Xbox 360 controller 1
    Shared CurrentButtonState1 As GamePadState
    Shared LastButtonState1 As GamePadState

    'Xbox 360 controller 2
    Shared CurrentButtonState2 As GamePadState
    Shared lastButtonState2 As GamePadState

    'Xbox 360 controller 3
    Shared CurrentButtonState3 As GamePadState
    Shared lastButtonState3 As GamePadState

    'Xbox 360 controller 4
    Shared CurrentButtonState4 As GamePadState
    Shared lastButtonState4 As GamePadState

    Public Shared Sub Update()
        'Current store to old
        LastKeyState = CurrentKeyState
        LastButtonState1 = CurrentButtonState1
        lastButtonState2 = CurrentButtonState2
        lastButtonState3 = CurrentButtonState3
        lastButtonState4 = CurrentButtonState4

        'new store to current
        CurrentKeyState = Keyboard.GetState
        CurrentButtonState1 = GamePad.GetState(PlayerIndex.One)
        CurrentButtonState2 = GamePad.GetState(PlayerIndex.Two)
        CurrentButtonState3 = GamePad.GetState(PlayerIndex.Three)
        CurrentButtonState4 = GamePad.GetState(PlayerIndex.Four)
    End Sub


    'Holding a key down
    Public Shared Function KeyDown(key As Keys) As Boolean
        Return CurrentKeyState.IsKeyDown(key)
    End Function

    'Holding a button down
    Public Shared Function ButtonDown(button As Buttons, controller As PlayerIndex) As Boolean
        Select Case controller
            Case PlayerIndex.One
                Return CurrentButtonState1.IsButtonDown(button)
            Case PlayerIndex.Two
                Return CurrentButtonState2.IsButtonDown(button)
            Case PlayerIndex.Three
                Return CurrentButtonState3.IsButtonDown(button)
            Case PlayerIndex.Four
                Return CurrentButtonState4.IsButtonDown(button)
        End Select
        Return False
    End Function

    'Press a key
    Public Shared Function KeyPressed(key As Keys) As Boolean
        If CurrentKeyState.IsKeyDown(key) And LastKeyState.IsKeyUp(key) Then
            Return True
        End If
        Return False
    End Function

    'Press a button
    Public Shared Function ButtonPressed(button As Buttons, controller As PlayerIndex) As Boolean
        Select Case controller
            Case PlayerIndex.One
                Return CurrentButtonState1.IsButtonDown(button) And LastButtonState1.IsButtonUp(button)
            Case PlayerIndex.Two
                Return CurrentButtonState2.IsButtonDown(button) And lastButtonState2.IsButtonUp(button)
            Case PlayerIndex.Three
                Return CurrentButtonState3.IsButtonDown(button) And lastButtonState3.IsButtonUp(button)
            Case PlayerIndex.Four
                Return CurrentButtonState4.IsButtonDown(button) And lastButtonState4.IsButtonUp(button)
        End Select
        Return False
    End Function

End Class
