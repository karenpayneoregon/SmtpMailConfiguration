using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmtpMailConfiguration;

namespace smtpUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        protected string GmailConfiguration = "mailSettings/smtp_1";
        protected string ComcastConfiguration = "system.net/mailSettings/smtp";

        [TestMethod]
        public void GMailFrom()
        {
            var mc = new MailConfiguration(GmailConfiguration);
            Assert.IsTrue(mc.FromAddress == "someone@gmail.com", 
                "Wrong from address for gmail");
        }
        [TestMethod]
        public void GMailUserName()
        {
            var mc = new MailConfiguration(GmailConfiguration);
            Assert.IsTrue(mc.UserName == "MssGMail",
                "Wrong user name for gmail");

        }
        [TestMethod]
        public void GMailHost()
        {
            var mc = new MailConfiguration(GmailConfiguration);
            Assert.IsTrue(mc.Host == "smtp.gmail.com",
                "Wrong host for gmail");
        }
        [TestMethod]
        public void GMailPort()
        {
            var mc = new MailConfiguration(GmailConfiguration);
            Assert.IsTrue(mc.Port == 587,
                "Wrong port for gmail");
        }
        [TestMethod]
        public void GMailEnableSsl()
        {
            var mc = new MailConfiguration(GmailConfiguration);
            Assert.IsTrue(mc.EnableSsl,
                "Wrong EnableSsl for gmail");
        }
        [TestMethod]
        public void GMailDefaultCredentials()
        {
            var mc = new MailConfiguration(GmailConfiguration);
            Assert.IsTrue(mc.DefaultCredentials == false,
                "Wrong DefaultCredentials for gmail");
        }
        [TestMethod]
        public void GMailPickupFolderExists()
        {
            var mc = new MailConfiguration(GmailConfiguration);
            Assert.IsTrue(mc.PickupFolderExists(),
                "gmail pickup folder does not exists");
        }
        [TestMethod]

        public void ComcastPickupFolderExists()
        {
            var mc = new MailConfiguration(ComcastConfiguration);
            Assert.IsTrue(mc.PickupFolderExists(),
                "Comcast pickup folder does not exists");
        }
        [TestMethod]
        public void ComcastFrom()
        {
            var mc = new MailConfiguration(ComcastConfiguration);
            Assert.IsTrue(mc.FromAddress == "Someone@comcast.net",
                "Wrong from address for Comcast");
        }
        [TestMethod]
        public void ComcastUserName()
        {
            var mc = new MailConfiguration(ComcastConfiguration);
            Assert.IsTrue(mc.UserName == "MissComcast",
                "Wrong user name for Comcast");

        }
        [TestMethod]
        public void ComcastHost()
        {
            var mc = new MailConfiguration(ComcastConfiguration);
            Assert.IsTrue(mc.Host == "smtp.comcast.net",
                "Wrong host for Comcast");
        }
        [TestMethod]
        public void ComcastPort()
        {
            var mc = new MailConfiguration(ComcastConfiguration);
            Assert.IsTrue(mc.Port == 587,
                "Wrong port for Comcast");
        }
        [TestMethod]
        public void ComcastEnableSsl()
        {
            var mc = new MailConfiguration(ComcastConfiguration);
            Assert.IsTrue(mc.EnableSsl,
                "Wrong EnableSsl for Comcast");
        }
        [TestMethod]
        public void ComcastDefaultCredentials()
        {
            var mc = new MailConfiguration(ComcastConfiguration);
            Assert.IsTrue(mc.DefaultCredentials,
                "Wrong DefaultCredentials for Comcast");
        }
    }
}
