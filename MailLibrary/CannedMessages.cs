namespace MailLibrary
{
    /// <summary>
    /// Represents a record from SQL-Server database table
    /// </summary>
    public class CannedMessages
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Description of record
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// HTML formatted for mail message
        /// </summary>
        public string HtmlMessage { get; set; }
        /// <summary>
        /// Plain text for mail message
        /// </summary>
        public string TextMessage { get; set; }

    }
}
