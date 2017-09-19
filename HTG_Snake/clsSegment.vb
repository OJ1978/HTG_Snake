Public Class clsSegment

    Private rectLoc As Rectangle

    Public ReadOnly Property Rect() As Rectangle

        Get

            Return rectLoc

        End Get

    End Property

    Public Property Loc() As Point

        Get

            Return rectLoc.Location

        End Get

        Set(ByVal Value As Point)

            rectLoc.Location = Value

        End Set

    End Property

    Public ReadOnly Property Size() As Size

        Get

            Return rectLoc.Size

        End Get

    End Property

    Public Sub New(ByVal pntLoc As Point, ByVal intWidth As Integer)

        rectLoc = New Rectangle(pntLoc, New Size(intWidth, intWidth))

    End Sub

    Public Function CloneSegment() As clsSegment

        Return New clsSegment(rectLoc.Location, rectLoc.Width)

    End Function

    Public Overrides Function ToString() As String

        Return Me.GetType.ToString + ": " + rectLoc.Location.ToString

    End Function

End Class
