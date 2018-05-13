' Add a reference in the project to System.Configuration
Imports System.Configuration

Public Class MailConfiguration
    Private smtpSection As Net.Configuration.SmtpSection = (TryCast(ConfigurationManager.GetSection("system.net/mailSettings/smtp"), Net.Configuration.SmtpSection))

    ''' <summary> 
    ''' Email address for the system 
    ''' </summary> 
    Public ReadOnly Property FromAddress() As String
        Get
            Return smtpSection.From
        End Get
    End Property
    ''' <summary> 
    ''' Gets the name or IP address of the host used for SMTP transactions. 
    ''' </summary> 
    Public ReadOnly Property Host() As String
        Get
            Return smtpSection.Network.Host
        End Get
    End Property
    ''' <summary> 
    ''' Gets the port used for SMTP transactions 
    ''' </summary> 
    ''' <remarks>default host is 25</remarks> 
    Public ReadOnly Property Port() As Integer
        Get
            Return smtpSection.Network.Port
        End Get
    End Property
    Public Overrides Function ToString() As String
        Return "From: [" & FromAddress & "] Host: [" & Host & "] Port: [" & Port & "]"
    End Function
End Class

