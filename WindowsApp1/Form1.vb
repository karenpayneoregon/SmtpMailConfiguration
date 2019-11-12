Imports System.IO

Public Class Form1
    Private Sub WriteToFileButton_Click(sender As Object, e As EventArgs) Handles WriteToFileButton.Click

        Dim fileName As String =
                If(String.IsNullOrWhiteSpace(My.Settings.FileName1),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SomeFile.txt"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AnotherFile.txt"))

        Using writer = New StreamWriter(fileName, CBool(IIf(File.Exists(fileName), True, False)))
            writer.WriteLine("one")
            writer.WriteLine("two")
        End Using
    End Sub

End Class
