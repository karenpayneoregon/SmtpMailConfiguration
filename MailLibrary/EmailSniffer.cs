using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;

namespace MailLibrary
{
    /// <summary>
    /// Call from a SmtpClient SendCompleted event where the SmtpClient has been invoked with
    /// SendAsync -
    ///   Parameter 1: MailMessage
    ///   Parameter 2: MailMessage
    ///
    /// Both parameters pass the same MailMessage.
    ///
    /// 
    /// </summary>
    public class EmailSniffer
    {
        public EmailSniffer()
        {
            Subject = "";
            From = "";

            ToList = new List<string>();
            CcList = new List<string>();
            BccList = new List<string>();

        }

        public void Inspect()
        {
            EmailLogger.LogToFile("");
        }

        public void Inspect(SmtpClient smtpClient, AsyncCompletedEventArgs args)
        {
            try
            {
                Host = smtpClient.Host;
                Port = smtpClient.Port.ToString();
            }
            catch (Exception) { 
                /*
                 * Do nothing we don't want to screw up here
                 */
            }

            if (args.Error != null)
            {
                Exception = args.Error;
            }

            var mailMessage = (MailMessage) args.UserState;

            Subject = mailMessage.Subject = string.IsNullOrWhiteSpace(mailMessage.Subject) ? "Missing subject" : mailMessage.Subject;
            From = string.IsNullOrWhiteSpace(mailMessage.From.Address) ? "Missing from address" : mailMessage.From.Address;

            ToList = mailMessage.To.Select(person => person.Address).ToList();
            CcList = mailMessage.CC.Select(person => person.Address).ToList();
            BccList = mailMessage.Bcc.Select(person => person.Address).ToList();

            AttachmentCount = mailMessage.Attachments.Count;

            LogDetails();

        }

        public string Host { get; set; }
        public string Port { get; set; }

        public bool Canceled { get; set; }

        public Exception Exception { get; set; }
        public string ErrorMessage => Exception == null ? "None" : Exception.Message;

        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public string Subject { get; set; }
        public string From { get; set; }
        public string FirstTo => ToList.FirstOrDefault();
        public List<string> ToList { get; set; }
        public List<string> CcList { get; set; }
        public List<string> BccList { get; set; }

        public int AttachmentCount { get; set; }

        public void LogDetails()
        {
            EmailLogger.LogToFile(ToString());
        }

        /// <summary>
        /// Get all properties for the message. Field names will go away
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"TO: [{string.Join(",", ToList.ToArray())}]," +
                   $"CC: [{string.Join(",", CcList.ToArray())}]," + 
                   $"BCC: [{string.Join(",", BccList.ToArray())}]," + 
                   $"Attachments: [{AttachmentCount}]," + 
                   $"Error: [{ErrorMessage}]," + 
                   $"Host: [{Host}]," + 
                   $"Port: [{Port}]";
        }
    }
}
