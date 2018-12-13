Public Class Form1
    Private Const iHeight As Integer = 100
    Private CurHeight As Int16 = 100
    Private bnlCollepse As Boolean = False

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        bnlCollepse = True
        Timer1.Enabled = True

        'If (CurHeight > 0) Then
        '    Me.Height = Me.Height - 5
        '    GroupBox1.Height = GroupBox1.Height - 5
        '    CurHeight -= 5
        'End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        bnlCollepse = False
        Timer1.Enabled = True

        'If (CurHeight < iHeight) Then
        '    Me.Height = Me.Height + 5
        '    GroupBox1.Height = GroupBox1.Height + 5
        '    CurHeight += 5
        'End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If (bnlCollepse) Then
            If (CurHeight > 0) Then
                Me.Height = Me.Height - 5
                GroupBox1.Height = GroupBox1.Height - 5
                CurHeight -= 5
            Else
                Timer1.Enabled = False
            End If
        Else
            If (CurHeight < iHeight) Then
                Me.Height = Me.Height + 5
                GroupBox1.Height = GroupBox1.Height + 5
                CurHeight += 5
            Else
                Timer1.Enabled = False
            End If
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        'If (RadioButton1.Checked) Then
        '    MessageBox.Show("buy")
        '    bnlCollepse = True
        '    Timer1.Enabled = True
        'End If
        ControlBoundsAnimator1.Continue()
    End Sub

    Private Sub RadioButton2_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If (RadioButton2.Checked) Then
            MessageBox.Show("sell")
            bnlCollepse = False
            Timer1.Enabled = True
        End If
    End Sub
End Class
