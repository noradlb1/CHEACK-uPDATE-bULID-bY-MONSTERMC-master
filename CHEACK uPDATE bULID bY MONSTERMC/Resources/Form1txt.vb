Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        UpToDate()
        PictureBox1.Image = My.Resources.loading__2_
    End Sub
    '--------------------------------------------------------Check Update Credit By MONSTERMC-------------------------------------------------------'
    Sub UpToDate()
        Try
            PictureBox1.Image = My.Resources.loading__2_

            Dim version As String = "%%version%%"

            Dim down As New Net.WebClient

            Dim checkver As String = down.DownloadString("%%LINK%%")

            If Not checkver.Split("|")(0) = version Then

                PictureBox1.Image = My.Resources.check_g834a1a840_1280

                Label1.Text = "%%NewversionHasBeenUpdated%%"

                Label2.Text = "%%NewVersionUpdatedWithNewFeatures%%"

                If MsgBox("%%NewVersionUpdated%%" & Chr(13) & "%%DoYouWantToCheck%%", MsgBoxStyle.YesNo, "%%MONSTERMCVPN%%") = MsgBoxResult.Yes Then

                    Process.Start(checkver.Split("|")(1))

                End If
            Else
                PictureBox1.Image = My.Resources.loading__2_

                Label1.Text = "%%ThisversionIsUpToDate%%"

                Label2.Text = "%%NoVersionUpdated%%"
            End If

        Catch ex As Exception

            MsgBox("Your Connexion Low To Check For Updates", MsgBoxStyle.Exclamation)
            PictureBox1.Image = My.Resources.loading__2_

        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        UpToDate()
    End Sub

    Private Sub ButterscotchTheme1_Click(sender As Object, e As EventArgs) Handles ButterscotchTheme1.Click

    End Sub
End Class
