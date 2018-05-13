using System;
using System.Configuration;
using System.IO;
using System.Net.Configuration;


/// <summary>
/// Responsible for retrieving settings for use when sending Smtp email messages in the app
/// and also in unit test. web.config must be setup properly. MSDN documentation:  <see cref="System.Net.Configuration.SmtpSection"/>
/// </summary>
/// <remarks>
/// - variable smtpSection provides the ability to read elements from app.config or web.config
/// - MailConfigurationTest provides a unit test for testing this class.
/// </remarks>
public class MailConfiguration
{
    private readonly SmtpSection _smtpSection = (ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection);
    
    public MailConfiguration()
    {

    }
    /// <summary>
    /// Email address for the system
    /// </summary>
    public string FromAddress => _smtpSection.From;
    /// <summary>
    /// Used for testing in tangent with PickupFolderExists
    /// </summary>
    public string PickupFolder
    {
        get
        {
            string mailDrop = _smtpSection.SpecifiedPickupDirectory.PickupDirectoryLocation;

            if (mailDrop != null)
            {
                mailDrop = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    _smtpSection.SpecifiedPickupDirectory.PickupDirectoryLocation);
            }

            return mailDrop;
        }
    }
    /// <summary>
    /// Determine if pickup folder exists
    /// </summary>
    /// <returns></returns>
    public bool PickupFolderExists()
    {
        return Directory.Exists(PickupFolder);
    }
    /// <summary>
    /// Gets the name or IP address of the host used for SMTP transactions.
    /// </summary>
    public string Host => _smtpSection.Network.Host;

    /// <summary>
    /// Gets the port used for SMTP transactions
    /// </summary>
    /// <remarks>default host is 25</remarks>
    public int Port => _smtpSection.Network.Port;

    /// <summary>
    /// Gets a value that specifies the amount of time after 
    /// which a synchronous Send call times out.
    /// </summary>
    public int TimeOut => 2000;

    /// <summary>
    /// Allows, for debugging to review from address, host and port properties
    /// </summary>
    /// <returns>A strin with main properties</returns>
    public override string ToString() => $"From: [ { FromAddress} ] Host: [{Host}] Port: [{Port}] Pickup: {System.IO.Directory.Exists(PickupFolder)}";
}

