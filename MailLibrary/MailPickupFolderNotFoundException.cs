using System;
using static System.String;

namespace MailLibrary
{
    /// <inheritdoc />
    /// <summary>
    /// Specific exception for when a pickup folder was specified for sending smtp email in
    /// test (not production) in app.config but does not physically exists when running a test.
    /// </summary>
    public class MailPickupFolderNotFoundException : Exception
    {
        /// <summary>
        /// Standard contructor
        /// </summary>
        public MailPickupFolderNotFoundException() { }
        /// <summary>
        /// Overloaded constructor, accepts user message
        /// </summary>
        /// <param name="message"></param>
        public MailPickupFolderNotFoundException(string message) : base(Concat("Folder does not exist: ", message)) { }
        /// <summary>
        /// Overloaded contructor to pass messasge and inner exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public MailPickupFolderNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
