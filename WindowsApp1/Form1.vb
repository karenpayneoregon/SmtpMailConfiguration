Public Class Form1
    Dim _cts As Threading.CancellationTokenSource
    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            _cts = New Threading.CancellationTokenSource()

            Dim task01 As New Task(Of String)(Function() Calcular("I like ", _cts.Token)) : task01.Start()
            Dim cadena As String = Await task01
            MessageBox.Show(cadena)

        Catch ex As OperationCanceledException
            MessageBox.Show($"Cancelled: {ex.Message}, have no idea what you like")
        End Try
    End Sub
    Function Calcular(pText As String, pToken As Threading.CancellationToken) As String

        For i = 1 To 5
            Threading.Thread.Sleep(500)
            If pToken.IsCancellationRequested Then
                pToken.ThrowIfCancellationRequested()
            End If
        Next i

        Return $"{pText} bacon"

    End Function
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If _cts IsNot Nothing Then
            _cts.Cancel()
        End If
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If Debugger.IsAttached Then
            Button1.Enabled = False
            Button2.Enabled = False
            MessageBox.Show("You need to run from Windows Explorer")
        End If
    End Sub
End Class
