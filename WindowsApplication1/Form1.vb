Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim mc As New MailConfiguration
        Dim fromAddress As String = mc.FromAddress
        Console.WriteLine(fromAddress)



    End Sub
End Class

