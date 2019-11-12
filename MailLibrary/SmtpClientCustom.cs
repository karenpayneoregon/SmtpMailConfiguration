using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailLibrary
{
    public class SmtpClientCustom : SmtpClient
    {
        /// <inheritdoc />
        /// <summary>
        /// Required to instantiate base class constructor 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public SmtpClientCustom(string host, int port) : base(host, port) {}

        /// <summary>
        /// Override event to ignore second argument, set CC and BCC collections
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userToken"></param>
        /// <remarks>
        /// Malformed Message will cause an exception to be thrown
        /// </remarks>
        public new void SendAsync(MailMessage message, object userToken)
        {

            MailMessage = message;

            CarbonCopyCollection = MailMessage.CC;
            BlindCarbonCopyCollection = MailMessage.Bcc;

            base.SendAsync(message, message);

        }

        /// <summary>
        /// MailMessage describing: from, to, cc, bcc
        /// </summary>
        public MailMessage MailMessage { get; set; }
        /// <summary>
        /// Determine if there is a MailMessage
        /// </summary>
        public bool HasMessage => MailMessage != null;

        public bool HasCarbons => MailMessage.CC.Count != null;
        public bool HasBlindCarbons => MailMessage.Bcc.Count != null;

        public MailAddressCollection CarbonCopyCollection { get; set; }
        public MailAddressCollection BlindCarbonCopyCollection { get; set; }

    }
}
