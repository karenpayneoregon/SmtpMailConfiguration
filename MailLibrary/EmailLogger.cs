using System;
using System.Globalization;
using System.IO;
using System.Text;
using static System.IO.File;

namespace MailLibrary
{
    public class EmailLogger
    {
        public static void LogToFile(string text)
        {
            var sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            sb.Append(": ");
            sb.AppendLine(text);
            AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"EmailLog.txt"),sb.ToString());
        }
    }
}