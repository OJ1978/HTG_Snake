Public Class clsMovement

    Private pntLocation As Point
    Private intStep As Integer
    Private iDirection As intDirection

    Public ReadOnly Property Location() As Point

        Get

            Return pntLocation

        End Get

    End Property

    Public Property Direction() As intDirection

        Get

            Return iDirection

        End Get

        Set(ByVal Value As intDirection)

            iDirection = Value

        End Set

    End Property


    Public ReadOnly Property Increment() As Integer

        Get

            Return intStep

        End Get

    End Property

    Public Enum intDirection As Integer

        None = -1
        Left
        Down
        Right
        Up

    End Enum



    Public Sub New()

        intStep = 8

        pntLocation = New Point(0, 0)

        Direction = intDirection.Right

    End Sub

    Public Sub New(ByVal iStep As Integer, ByVal pStart As Point, ByVal dirNew As intDirection)

        iDirection = dirNew

        intStep = iStep

        pntLocation = pStart

    End Sub

    Public Function NextLoc(Optional ByVal dirNext As intDirection = intDirection.None) As Point

        Dim pntLoc As New Point(pntLocation.X, pntLocation.Y)

        If (dirNext = intDirection.None) Then dirNext = iDirection

        Select Case dirNext

            Case intDirection.Left

                pntLoc.X -= intStep

                Exit Select

            Case intDirection.Down

                pntLoc.Y += intStep

                Exit Select

            Case intDirection.Right

                pntLoc.X += intStep

                Exit Select

            Case intDirection.Up

                pntLoc.Y -= intStep

                Exit Select

        End Select

        Return pntLoc

    End Function

    Public Sub Move(Optional ByVal dirMove As intDirection = intDirection.None)

        If (dirMove = intDirection.None) Then dirMove = iDirection

        Select Case dirMove

            Case intDirection.Left

                pntLocation.X -= intStep

                Exit Select

            Case intDirection.Down

                pntLocation.Y += intStep

                Exit Select

            Case intDirection.Right

                pntLocation.X += intStep

                Exit Select

            Case intDirection.Up

                pntLocation.Y -= intStep

                Exit Select

        End Select

    End Sub

    Public Sub Move(ByVal rectBounds As Rectangle, Optional ByVal dirMove As intDirection = intDirection.None)

        Move(dirMove)

        If (pntLocation.X > rectBounds.Right) Then

            pntLocation.X = CInt(rectBounds.Left / intStep) * intStep

        ElseIf (pntLocation.X < rectBounds.Left) Then

            pntLocation.X = CInt(rectBounds.Right / intStep) * intStep

        ElseIf (pntLocation.Y > rectBounds.Bottom) Then

            pntLocation.Y = CInt(rectBounds.Top / intStep) * intStep

        ElseIf (pntLocation.Y < rectBounds.Top) Then

            pntLocation.Y = CInt(rectBounds.Bottom / intStep) * intStep

        End If

    End Sub

End Class

