﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace smtpUnitTests
{
    public class TestBase
    {
        #region Available configurations from app.config file
        protected string GmailConfiguration1 = "mailSettings/smtp_1";
        protected string GmailConfiguration2 = "mailSettings/smtp_2";
        protected string Configuration1 = "mailSettings/smtp_3";
        protected string ComcastConfiguration = "system.net/mailSettings/smtp";
        #endregion

        protected TestContext TestContextInstance;
        public TestContext TestContext
        {
            get => TestContextInstance;
            set => TestContextInstance = value;
        }

        /// <summary>
        /// Email address to send below message too.
        /// Replace with your address of choice.
        /// </summary>
        protected string SendMessageToAddress = "paynekaren@comcast.net";
        /// <summary>
        /// File name to log, log file is in bin\debug folder
        /// </summary>
        protected string LogfileName = "Demo.txt";


    }
}
