using System;
using System.Configuration;
using System.Net.Configuration;
using System.Reflection;

/// <summary>
/// Demonstrates reading setting for sending email messages
/// via smtp from an app.config file, works for web.config file also.
/// </summary>
public class MailConfiguration
{
    /// <summary>
    /// SMTP Server for SmtpClient
    /// </summary>
    public string HostServer { get; set; }
    /// <summary>
    /// Specified port e.g. 587 for Comcast
    /// </summary>
    public int Port { get; set; }
    /// <summary>
    /// Who is sending message
    /// </summary>
    public string From { get; set; }
    /// <summary>
    /// User name for credentials
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// Password for credentials
    /// </summary>
    public string Password { get; set; }
    public bool DefaultCredentials { get; set; }
    /// <summary>
    /// Set properties for this class
    /// </summary>
    public MailConfiguration()
    {
        Configuration config;
        MailSettingsSectionGroup mailSettings;

        config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
        mailSettings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
        if (mailSettings != null)
        {

            HostServer = mailSettings.Smtp.Network.Host;
            UserName = mailSettings.Smtp.Network.UserName;
            Password = mailSettings.Smtp.Network.Password;
            From = mailSettings.Smtp.From;
            DefaultCredentials = mailSettings.Smtp.Network.DefaultCredentials;

            //  if port is not numeric an exception is thrown
            try
            {
                Port = mailSettings.Smtp.Network.Port;
            }
            catch (Exception)
            {
                Port = 0;
            }
        }
    }
}