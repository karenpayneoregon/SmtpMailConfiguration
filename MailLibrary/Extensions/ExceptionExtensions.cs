﻿using System;

namespace MailLibrary.Extensions
{
    /// <summary>
    /// Helper methods for logging exceptions
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Get InnerException if there is one as text.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string GetExceptionMessages(this Exception e, string result = "")
        {
            if (e == null) return string.Empty;

            if (string.IsNullOrWhiteSpace(result)) result = e.Message;

            if (e.InnerException != null)
            {
                result += $": InnerException: {GetExceptionMessages(e.InnerException)}";
            }

            return result;
        }
    }
}
