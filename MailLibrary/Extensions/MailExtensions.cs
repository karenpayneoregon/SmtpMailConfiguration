using System.Net.Mail;

namespace MailLibrary.Extensions
{
    /// <summary>
    /// Helper extensions for creating email messages
    /// </summary>
    public static class MailExtensions
    {
        /// <summary>
        /// Add alternate view for html
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <remarks>
        /// Value is shorter line in code that sends emails
        /// </remarks>
        public static void HTmlView(this AlternateViewCollection sender, string message)
        {
            sender.Add(AlternateView.CreateAlternateViewFromString(message, null, "text/html"));
        }
        /// <summary>
        /// Add alternate view for plain text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <remarks>
        /// Value is shorter line in code that sends emails
        /// </remarks>
        public static void PlainTextView(this AlternateViewCollection sender, string message)
        {
            sender.Add(AlternateView.CreateAlternateViewFromString(message, null, "text/plain"));
        }
    }
}
