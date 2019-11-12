using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;



namespace WindowsFormsApp1
{

    /// <summary>
    /// https://codecanyon.net/item/fluentnet-mail-dynamic-templated-emailing-framework/4021407
    /// https://www.codeproject.com/Articles/640997/Fluent-interfaces-and-Method-Chaining-in-Csharp
    /// http://www.stefanoricciardi.com/2010/04/14/a-fluent-builder-in-c/
    /// </summary>
    public class PayneMail 
    {
        private MailBox _mailBox = new MailBox();
        public PayneMail From(string pEmailAddress, string pDisplayName)
        {
            _mailBox.FromEmailAddress = pEmailAddress;
            _mailBox.FromDisplayName = pDisplayName;
            return this;
        }
        public PayneMail To(string pEmailAddress, string pDisplayName)
        {
            _mailBox.ToEmailAddress = pEmailAddress;
            _mailBox.ToDisplayName = pDisplayName;
            return this;
        }
        public PayneMail Credentials(string pEmailAddress, string pPassword)
        {
            _mailBox.Credentials = new NetworkCredential(pEmailAddress,pPassword);
            return this;
        }
        public MailBox GetResult()
        {
            return _mailBox;
        }
    }

    public class MailBox
    {
        public SmtpClient Client { get; set; }
        public NetworkCredential Credentials { get; set; }
        public string FromEmailAddress { get; set; }
        public string FromDisplayName { get; set; }
        public string ToEmailAddress { get; set; }
        public string ToDisplayName { get; set; }
        public MailBox()
        {
            
        }
        public MailBox(string pEmailAddress)
        {
            
        }
    }
}
