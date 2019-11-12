Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Net.Sockets

Public Class Form1
    Private WithEvents bgWorker As New BackgroundWorker
    Private mailOps As New MailOperations

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Button1.Enabled = False

        bgWorker.WorkerReportsProgress = True
        bgWorker.WorkerSupportsCancellation = True

        mailOps.FlushMailFolder()

        Dim mailList = mailOps.MailToList()
        bgWorker.RunWorkerAsync(mailList.Count)

    End Sub
    ''' <summary>
    ''' Process email messages one at a time
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub bgw_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgWorker.DoWork

        Dim numberOfMessagesToProcess As Integer = CInt(e.Argument)

        For index As Integer = 1 To mailOps.MailToList().Count - 1

            ' using this because otherwise things run rather faster for demonstration purposes
            ' of 50 records.
            Threading.Thread.Sleep(100)

            mailOps.SendSingleMessage(mailOps.MailToList()(index))
            bgWorker.ReportProgress(Convert.ToInt32((index / numberOfMessagesToProcess) * 100))
        Next
    End Sub
    ''' <summary>
    ''' Is fired off each time an iteration has completed in do work.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub bgw_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgWorker.ProgressChanged

        UpdateProgress(e.ProgressPercentage)

    End Sub
    ''' <summary>
    ''' Is fired off when all messages have been sent.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub bgw_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgWorker.RunWorkerCompleted

        ProgressBar1.Value = ProgressBar1.Maximum
        Button1.Enabled = True

    End Sub
    ''' <summary>
    ''' This can be done in the changed event but split out so 
    ''' a developer can use this in another form.
    ''' </summary>
    ''' <param name="percentDone">Percentage done of the entire operation</param>
    Public Sub UpdateProgress(percentDone As Integer)
        ProgressBar1.Value = ProgressBar1.Maximum
        ProgressBar1.Value = percentDone
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim test As New Tester
        test.Example1()
    End Sub
    Public Function GetLocalIPAddress() As String
        Dim host = Dns.GetHostEntry(Dns.GetHostName())
        For Each ip In host.AddressList
            If ip.AddressFamily = AddressFamily.InterNetwork Then
                Return ip.ToString()
            End If
        Next
        'Throw New Exception("No network adapters with an IPv4 address in the system!")
        Return ""
    End Function
    Public Sub DisplayLocalHostName()
        Try
            ' Get the local computer host name.
            Dim hostName As [String] = Dns.GetHostName()
            Dim tester = Dns.GetHostAddresses(hostName)


            Console.WriteLine($"Addy {Dns.GetHostAddresses(hostName)(0)}")

            Console.WriteLine(("Computer name :" + hostName))
        Catch e As SocketException
            Console.WriteLine("SocketException caught!!!")
            Console.WriteLine(("Source : " + e.Source))
            Console.WriteLine(("Message : " + e.Message))
        Catch e As Exception
            Console.WriteLine("Exception caught!!!")
            Console.WriteLine(("Source : " + e.Source))
            Console.WriteLine(("Message : " + e.Message))
        End Try
    End Sub
End Class
