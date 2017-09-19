Public Class frmSnake


    Private Const intGrow As Integer = 3
    Private Const intWidth As Integer = 8

    Private cSnake As clsSnake
    Private cMovement As clsMovement

    Private blnMoving As Boolean = False
    Private blnExpanding As Boolean = False

    Private rectFood As Rectangle

    Private intScore As Integer

    Public Sub Feed()

        Dim pntFood As Point

        Do

            pntFood = Randomize()

            If Not (cSnake Is Nothing) Then

                If Not cSnake.FoodPlacedOnSnake(pntFood) Then Exit Do

            Else

                Exit Do

            End If

        Loop

        rectFood.Location = pntFood

    End Sub

    Private Sub Die()

        DisplayMessage("Press Enter to play or Escape to quit.")

        Initialize()

    End Sub


    Private Sub Initialize()

        intScore = 0

        rectFood = New Rectangle(0, 0, intWidth, intWidth)

        Feed()

        Dim pntStart As New Point(CInt(picGame.ClientSize.Width / 2 / intWidth + 0.5) * intWidth, CInt(picGame.ClientSize.Height / 2 / intWidth + 0.5) * intWidth)

        cSnake = New clsSnake(pntStart, intWidth, 1)

        cMovement = New clsMovement(intWidth, cSnake.Head.Loc, clsMovement.intDirection.Right)

        blnExpanding = True

    End Sub

    Private Sub UpdateUI()

        Static iGrow As Integer = intGrow

        Static intAddSeg As Integer

        If Not blnMoving Then Exit Sub

        cMovement.Move(picGame.ClientRectangle)


        If cSnake.FoodPlacedOnSnake(cMovement.Location) Then

            iGrow = 0
            intAddSeg = 0

            Die()

            Return


        ElseIf rectFood.Contains(cMovement.Location) Then

            iGrow += intGrow

            blnExpanding = True

            Feed()

            intScore += 5

            Text = "Score: " + intScore.ToString

        End If

        If blnExpanding Then

            If iGrow < intGrow Then iGrow = intGrow

            If intAddSeg >= iGrow Then

                blnExpanding = False

                intAddSeg = 0
                iGrow = 0

                cSnake.Move(cMovement.Location)

            Else

                cSnake.Eat(cMovement.Location)

                intAddSeg += 1

            End If

        Else

            cSnake.Move(cMovement.Location)

        End If

    End Sub

    Private Sub DisplayMessage(ByVal strMsg As String)

        lblMessage.Text = strMsg
        lblMessage.Visible = True
        blnMoving = False

        tmrGame.Enabled = False

    End Sub

    Public Function Randomize() As Point

        Dim rnd As New Random(Now.Second)

        Dim intScreenWidth As Integer = ((ClientRectangle.Width \ intWidth) - 2) * intWidth
        Dim intScreenHeight As Integer = ((ClientRectangle.Height \ intWidth) - 2) * intWidth

        Dim intX As Integer = rnd.Next(0, intScreenWidth)
        Dim intY As Integer = rnd.Next(0, intScreenHeight)

        intX = (intX \ intWidth) * intWidth
        intY = (intY \ intWidth) * intWidth

        Return New Point(intX, intY)

    End Function

    Private Sub HideMessage()

        Me.Text = "Score: " + intScore.ToString

        lblMessage.Visible = False
        blnMoving = True
        tmrGame.Enabled = True

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Initialize()

    End Sub

    Private Sub picGame_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picGame.Paint

        If Not blnMoving Then

            e.Graphics.Clear(picGame.BackColor)

            Exit Sub

        End If

        e.Graphics.FillEllipse(Brushes.White, rectFood)

        Dim segCurrent As clsSegment


        For Each segCurrent In cSnake.NumberOfSegments

            e.Graphics.FillRectangle(Brushes.White, segCurrent.Rect)

        Next

    End Sub

    Private Sub tmrGame_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrGame.Tick

        UpdateUI()

        picGame.Invalidate()

    End Sub

    Private Sub Form1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp

        Select Case e.KeyCode

            Case Keys.Enter

                HideMessage()

            Case Keys.Escape

                If blnMoving Then

                    DisplayMessage("Press Enter to continue or Escape to quit.")

                Else

                    Me.Close()

                End If

        End Select

    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode

            Case Keys.Right

                cMovement.Direction = clsMovement.intDirection.Right

            Case Keys.Down

                cMovement.Direction = clsMovement.intDirection.Down

            Case Keys.Left

                cMovement.Direction = clsMovement.intDirection.Left

            Case Keys.Up

                cMovement.Direction = clsMovement.intDirection.Up

        End Select

    End Sub

End Class
