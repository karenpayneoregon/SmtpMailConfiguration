using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MailLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmtpMailConfiguration;

/*
 * https://myaccount.google.com/lesssecureapps
 */
namespace smtpUnitTests
{
    /// <summary>
    /// Test methods for confirming configurations and
    /// methods to send email message without assertions.
    /// </summary>
    /// <remarks>
    /// If using gmail for the host then make sure to read the following.
    /// https://myaccount.google.com/lesssecureapps
    /// </remarks>
    [TestClass]
    public class UnitTest1 :TestBase
    {
        /// <summary>
        /// Remove any files generated for test method UsePickupFolderExample
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            var mc = new MailConfiguration(GmailConfiguration2);
            if (TestContext.TestName == "UsePickupFolderExample")
            {
                Directory.GetFiles(mc.PickupFolder).ToList().ForEach(File.Delete);
            }
        }

        #region send email (there are no asserts)

        /// <summary>
        /// Send a canned email messages from database table, see comments in 
        /// <see cref="MailLibrary.Operations.ExampleSend1"/>
        /// </summary>
        /// <remarks>
        /// No logging.
        /// </remarks>
        [TestMethod]
        [TestTraits(Trait.SendingLiveSynchronous)]

        public void ExampleSend1_NoSendComplete_Good()
        {
            var ops = new Operations();

            ops.ExampleSend1(ComcastConfiguration, SendMessageToAddress, 1);
            ops.ExampleSend1(ComcastConfiguration, SendMessageToAddress, 2);
            ops.ExampleSend1(ComcastConfiguration, SendMessageToAddress, 2);

        }
        /// <summary>
        /// Example using ReplyToList which when the person whom rec'd the
        /// email selects "reply" the email address(es) in the ReplyToList
        /// will automatically be included in the reply.
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.SendingLiveSynchronous)]
        public void ExampleWithReplyTo()
        {

            var ops = new Operations();
            ops.ExampleSendWithRepyToList(ComcastConfiguration, SendMessageToAddress, 1);

        }
        /// <summary>
        /// Example for friendly names for FROM and TO addresses
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.SendingLiveSynchronous)]
        public void MailFriendlyExample()
        {
            var fromData = new MailFriendly() {Address = SendMessageToAddress, Display = "Karen",};
            var toData = new MailFriendly() { Address = SendMessageToAddress, Display = "Karen", };

            var ops = new Operations();
            ops.ExampleSendMaskNames(ComcastConfiguration, 5,fromData,toData);
        }

        [TestMethod]
        [TestTraits(Trait.SendingPickupFolder)]
        public void UsePickupFolderExample()
        {
            var fromData = new MailFriendly() { Address = SendMessageToAddress, Display = "Karen", };
            var toData = new MailFriendly() { Address = SendMessageToAddress, Display = "Karen", };

            var ops = new Operations();
            ops.UsePickupFolderExample(GmailConfiguration2, 5, fromData, toData);
        }
        /// <summary>
        /// Attempt to send email with a bad host and on failure write
        /// to a log file.
        /// </summary>
        /// <remarks>
        /// Logging is performed
        /// </remarks>
        [TestMethod]
        [TestTraits(Trait.SendingLiveSynchronous)]
        public void ExampleSend1_BadHost()
        {
            var ops = new Operations(true, LogfileName);
            ops.ExampleSend2(GmailConfiguration2, SendMessageToAddress, 4);
        }
        [TestMethod]
        public void ExampleSend1_CustomClient()
        {
            var ops = new Operations(true, LogfileName);
            ops.ExampleSendCustomClient(GmailConfiguration2, SendMessageToAddress, 4);
        }
        /// <summary>
        /// Send an email with callback. This is a successful send unless
        /// the configuration is not proper as the first parameter to
        /// ExampleSend3Async method.
        /// </summary>
        /// <returns>nothing</returns>
        /// <remarks>
        /// Logging is performed
        /// </remarks>
        /// <summary>
        /// - This test embeds a link.
        /// - This test method has issues when running from Test Explorer while
        ///   running from here zero issues. Consider this an issue with Test
        ///   Explorer and asynchronous calls. Has been tested outside of a unit
        ///   test also and works fine.
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.SendingLiveAsynchronous)]
        public async Task ExampleSend1_WithSendComplete_NoErrorsAsync()
        {
            var ops = new Operations(true, LogfileName);
            await ops.ExampleSend3Async(ComcastConfiguration, SendMessageToAddress).ConfigureAwait(false);
        }
        [TestMethod]
        [TestTraits(Trait.SendingLiveSynchronousAttachments)]
        public void SendingMultipleAttachmentsFromDisk()
        {
            var ops = new Operations();
            ops.SendMultipleAttachementsFromDisk(GmailConfiguration2, SendMessageToAddress, 1);
        }
        [TestMethod]
        [TestTraits(Trait.SendingLiveSynchronousEmbedImage)]
        public void SendMessageWithEmbededImage()
        {
            var ops = new Operations();
            ops.EmbedImageFromDisk(GmailConfiguration2, SendMessageToAddress);
        }
        [TestMethod]
        [TestTraits(Trait.SendingLiveSynchronousAttachments)]
        public void SendingMultipleAttachmentsByeArray()
        {
            var ops = new Operations();
            ops.SendMultipleAttachementsFromByeArray(ComcastConfiguration, SendMessageToAddress, 1);
        }

        #endregion

        #region configuration tests
        /// <summary>
        /// Test gmail from address in configuration file
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void GMailFrom1()
        {
            var mc = new MailConfiguration(GmailConfiguration1);
            Assert.IsTrue(mc.FromAddress == "someone@gmail.com",
                "Wrong from address for gmail 1");
        }
        /// <summary>
        /// Test gmail from address in configuration file
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void GMailFrom2()
        {
            var mc = new MailConfiguration(GmailConfiguration2);
            Assert.IsTrue(mc.FromAddress == "karenpayneoregon@gmail.com",
                "Wrong from address for gmail 2");
        }
        /// <summary>
        /// Test gmail user name in configuration file
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void GMailUserName1()
        {
            var mc = new MailConfiguration(GmailConfiguration1);
            Assert.IsTrue(mc.UserName == "MssGMail",
                "Wrong user name for gmail");

        }
        /// <summary>
        /// Test gmail user name in configuration file
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void GMailUserName2()
        {
            var mc = new MailConfiguration(GmailConfiguration2);
            Assert.IsTrue(mc.UserName == "karenpayneoregon@gmail.com",
                "Wrong user name for gmail 2");

        }
        /// <summary>
        /// Test gmail host in configuration file
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void GMailHost1()
        {
            var mc = new MailConfiguration(GmailConfiguration1);
            Assert.IsTrue(mc.Host == "smtp.gmail.com",
                "Wrong host for gmail 1");
        }
        /// <summary>
        /// Test gmail port in configuration file
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void GMailPort1()
        {
            var mc = new MailConfiguration(GmailConfiguration1);
            Assert.IsTrue(mc.Port == 587,
                "Wrong port for gmail");
        }
        /// <summary>
        /// Test gmail port in configuration file
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void GMailPort2()
        {
            var mc = new MailConfiguration(GmailConfiguration2);
            Assert.IsTrue(mc.Port == 587,
                "Wrong port for gmail 2");
        }
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void GMailEnableSsl1()
        {
            var mc = new MailConfiguration(GmailConfiguration1);
            Assert.IsTrue(mc.EnableSsl,
                "Wrong EnableSsl for gmail 1");
        }
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void GMailEnableSsl2()
        {
            var mc = new MailConfiguration(GmailConfiguration2);
            Assert.IsTrue(mc.EnableSsl,
                "Wrong EnableSsl for gmail 2");
        }
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void GMailDefaultCredentials1()
        {
            var mc = new MailConfiguration(GmailConfiguration1);
            Assert.IsTrue(mc.DefaultCredentials == false,
                "Wrong DefaultCredentials for gmail 1");
        }
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void GMailDefaultCredentials2()
        {
            var mc = new MailConfiguration(GmailConfiguration2);
            Assert.IsTrue(mc.DefaultCredentials == false,
                "Wrong DefaultCredentials for gmail 2");
        }
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void GMailPickupFolderExists1()
        {
            var mc = new MailConfiguration(GmailConfiguration1);
            Assert.IsTrue(mc.PickupFolderExists(),
                "gmail pickup folder does not exists");
        }
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void ComcastPickupFolderExists()
        {
            var mc = new MailConfiguration(ComcastConfiguration);
            Assert.IsTrue(mc.PickupFolderExists(),
                "Comcast pickup folder does not exists");
        }
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void ComcastFrom()
        {
            var mc = new MailConfiguration(ComcastConfiguration);
            Assert.IsTrue(mc.FromAddress == "Someone@comcast.net",
                "Wrong from address for Comcast");
        }
        [TestMethod]
        [Ignore]
        [TestTraits(Trait.Configuration)]
        public void ComcastUserName()
        {
            var mc = new MailConfiguration(ComcastConfiguration);
            Assert.IsTrue(mc.UserName == "MissComcast",
                "Wrong user name for Comcast");

        }
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void ComcastHost()
        {
            var mc = new MailConfiguration(ComcastConfiguration);
            Assert.IsTrue(mc.Host == "smtp.comcast.net",
                "Wrong host for Comcast");
        }
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void ComcastPort()
        {
            var mc = new MailConfiguration(ComcastConfiguration);
            Assert.IsTrue(mc.Port == 587,
                "Wrong port for Comcast");
        }
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void ComcastEnableSsl()
        {
            var mc = new MailConfiguration(ComcastConfiguration);
            Assert.IsTrue(mc.EnableSsl,
                "Wrong EnableSsl for Comcast");
        }
        [TestMethod]
        [TestTraits(Trait.Configuration)]
        public void ComcastDefaultCredentials()
        {
            var mc = new MailConfiguration(ComcastConfiguration);
            Assert.IsTrue(mc.DefaultCredentials,
                "Wrong DefaultCredentials for Comcast");
        }
        #endregion
    }
}
