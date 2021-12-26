Imports System.IO
Imports System.Threading
Imports System.Environment
Public Class Form1
    Dim form1txt As String = My.Resources.Form1txt
    Private Sub ButterscotchButton1_Click(sender As Object, e As EventArgs) Handles ButterscotchButton1.Click
        System.IO.File.WriteAllBytes("C:\Users\" & UserName & "\Desktop\update BY MONSTERMC.rar", My.Resources.update_BY_MONSTERMC)
        form1txt = form1txt.Replace("%%LINK%%", (ButterscotchTextBox1.Text))
        form1txt = form1txt.Replace("%%version%%", (ButterscotchTextBox2.Text))
        form1txt = form1txt.Replace("%%NewversionHasBeenUpdated%%", (ButterscotchTextBox3.Text))
        form1txt = form1txt.Replace("%%NewVersionUpdatedWithNewFeatures%%", (ButterscotchTextBox4.Text))
        form1txt = form1txt.Replace("%%NewVersionUpdated%%", (ButterscotchTextBox5.Text))
        form1txt = form1txt.Replace("%%DoYouWantToCheck%%", (ButterscotchTextBox6.Text))
        form1txt = form1txt.Replace("%%MONSTERMCVPN%%", (ButterscotchTextBox7.Text))
        form1txt = form1txt.Replace("%%ThisversionIsUpToDate%%", (ButterscotchTextBox8.Text))
        form1txt = form1txt.Replace("%%NoVersionUpdated%%", (ButterscotchTextBox9.Text))
        Dim c As New SaveFileDialog
        With c
            .FileName = "Form1"
            .Filter = "vb|*.VB"
            .ShowDialog()
        End With
        System.IO.File.WriteAllText(c.FileName, form1txt)
        MessageBox.Show("successfully : " & c.FileName, "DONE!", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Process.Start("https://pastebin.com/")
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Process.Start("https://www.youtube.com/channel/UCLF-eRNc52VslhdctpHaAzg/videos")
    End Sub
End Class
