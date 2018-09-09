using System.IO;
using System.Net.Mail;

namespace MailLibrary
{
    /// <summary>
    /// Container for use with sending email messages which gets contents of a file
    /// from a varbinary column. The Attachment property permits using for sending email
    /// messages.
    /// </summary>
    public class AttachmentByteArray
    {
        /// <summary>
        /// Represents the name for the byte array in FileContent
        /// </summary>
        public string Filename { get; set; }
        /// <summary>
        /// Represents a file in a byte array
        /// </summary>
        public byte[] FileContent { get; set; }
        /// <summary>
        /// Creates a smtp mail attachment from a byte array returned from a row in
        /// a table.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Since not using mime types this is not passed to the Attachment, 
        /// only a file name.
        /// 
        /// MemoryStream is safe to leave undisposed, gac handle this.
        /// 
        /// Two variables are used to create symbols for the gac to work with.
        /// </remarks>
        public Attachment Attachment
        {
            get
            {
                var stream = new MemoryStream(FileContent);
                var attachment = new Attachment(stream, Filename);
                return attachment;
            }
        }


    }
}
