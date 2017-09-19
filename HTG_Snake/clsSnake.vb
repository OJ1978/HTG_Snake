Public Class clsSnake

    Private Const intMaxLength As Integer = 1024
    Private Const intDefaultLength As Integer = 4
    Private Const intDefaultWidth As Integer = 8

    Private qSegments As Queue
    Private intWidth As Integer

    Public Property NumberOfSegments() As clsSegment()

        Get

            Dim cSegments(qSegments.Count - 1) As clsSegment

            qSegments.CopyTo(cSegments, 0)

            Return cSegments

        End Get

        Set(value As clsSegment())

        End Set

    End Property

    Public Property Head() As clsSegment

        Get

            Return DirectCast(qSegments.Peek, clsSegment).CloneSegment

        End Get

        Set(value As clsSegment)

        End Set

    End Property

    Private Sub InitializeSnake(ByVal pntLoc As Point, ByVal iWidth As Integer, ByVal iLength As Integer)

        intWidth = iWidth

        Dim pLoc As Point = pntLoc

        Dim i As Integer

        For i = 1 To iLength

            Eat(pLoc)

            pLoc.X -= intWidth

        Next

    End Sub

    Public Sub New()

        MyBase.New()

        InitializeSnake(New Point(intDefaultLength * intDefaultWidth, 0), intDefaultWidth, intDefaultLength)

    End Sub

    Public Sub New(ByVal pntStart As Point, ByVal iWidth As Integer, ByVal iLength As Integer)

        MyBase.New()

        InitializeSnake(pntStart, iWidth, iLength)

    End Sub

    Public Sub Eat(ByVal pntLoc As Point)

        Dim cHead As New clsSegment(pntLoc, intWidth)

        If (qSegments Is Nothing) Then

            qSegments = New Queue(intMaxLength)

        ElseIf (qSegments.Count = intMaxLength) Then

            Move(pntLoc)

            Exit Sub

        End If

        qSegments.Enqueue(cHead)

    End Sub

    Public Sub Clear()

        qSegments.Clear()

    End Sub

    Public Sub Move(ByVal pntLoc As Point)

        Dim cHead As New clsSegment(pntLoc, intWidth)

        qSegments.Enqueue(cHead)

        qSegments.Dequeue()

    End Sub


    Public Function FoodPlacedOnSnake(ByVal pntLoc As Point) As Boolean

        Dim ieSegments As IEnumerator = qSegments.GetEnumerator

        While ieSegments.MoveNext

            If DirectCast(ieSegments.Current, clsSegment).Rect.Contains(pntLoc) Then Return True

        End While

    End Function

End Class
