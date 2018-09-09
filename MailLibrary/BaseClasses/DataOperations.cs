using System;
using System.Data.SqlClient;

namespace MailLibrary.BaseClasses
{
    public class DataOperations : BaseSqlServerConnections
    {
        public DataOperations()
        {
            DefaultCatalog = "EmailTesting";
        }
        /// <summary>
        /// Return an existing record for email purposes. There is
        /// no exception handling to assert if the record requested exists
        /// as the intent is to work with primary keys that exists.
        /// </summary>
        /// <param name="pIdentifier"></param>
        /// <returns></returns>
        public CannedMessages Read(int pIdentifier)
        {
            mHasException = false;
            var result = new CannedMessages();

            using (var cn = new SqlConnection() {ConnectionString = ConnectionString})
            {
                using (var cmd = new SqlCommand() {Connection = cn})
                {
                    cmd.CommandText = $"SELECT [Description],HtmlMessage,TextMessage FROM EmailTesting.dbo.CannedMessages WHERE id = {pIdentifier}";
                    try
                    {
                        cn.Open();
                        var reader = cmd.ExecuteReader();
                        reader.Read();
                        result.Description = reader.GetString(0);
                        result.HtmlMessage = reader.GetString(1);
                        result.TextMessage = reader.GetString(2);
                    }
                    catch (Exception e)
                    {
                        mHasException = true;
                        mLastException = e;
                    }
                }
            }

            return result;
        }
    }
}
