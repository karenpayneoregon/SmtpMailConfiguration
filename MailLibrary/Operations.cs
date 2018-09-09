using SmtpMailConfiguration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Threading;
using LoggingLibrary;
using MailLibrary.BaseClasses;
using MailLibrary.Extensions;

namespace MailLibrary
{
    public class Operations
    {
        private bool _writeToLog;
        private FileInfo _LogInfo;

        /// <summary>
        /// Init class for deciding to log or not.
        /// </summary>
        /// <param name="pUseLogging">True to log, false not to log to file</param>
        /// <param name="pFileName">Log file name</param>
        public Operations(bool pUseLogging = false, string pFileName = "")
        {
            if (pUseLogging && !string.IsNullOrWhiteSpace(pFileName))
            {
                _writeToLog = true;
                _LogInfo = new FileInfo(pFileName);
            }
            
        }
        //
        /// <summary>
        /// Uses settings from unit test app.config to configuring/setting up
        /// the MailConfiguration, MailMessage (from address) and properties
        /// for SmtpClient object. subject and message are static.
        /// 
        /// Message properties
        /// - Subject adds the caller name e.g. the test method calling this method.
        /// - Plain and HTML message comes from SQL-Server table
        /// </summary>
        /// <param name="pConfig">appropriate <see cref="MailConfiguration"/> item</param>
        /// <param name="pSendToo">Valid email address to send message too</param>
        /// <param name="identifier">SQL-Server table key</param>
        /// <param name="name">Represents who called this method</param>
        public void ExampleSend1(string pConfig, string pSendToo, int identifier, [CallerMemberName]string name = "")
        {
            var ops = new DataOperations();
            var data = ops.Read(identifier);

            var mc = new MailConfiguration(pConfig);
            var mail = new MailMessage
            {
                From = new MailAddress(mc.FromAddress),
                Subject = $"Sent from test: '{name}'"
            };

            mail.To.Add(pSendToo);
            mail.IsBodyHtml = true;
            
            mail.AlternateViews.PlainTextView(data.TextMessage);
            mail.AlternateViews.HTmlView(data.HtmlMessage);

            using (var smtp = new SmtpClient(mc.Host, mc.Port))
            {
                smtp.Credentials = new NetworkCredential(mc.UserName, mc.Password);
                smtp.EnableSsl = mc.EnableSsl;
                smtp.Send(mail);
            }

        }
        //
        /// <summary>
        /// Uses settings from unit test app.config to configuring/setting up
        /// the MailConfiguration, MailMessage (from address) and properties
        /// for SmtpClient object. subject and message are static.
        /// 
        /// Message properties
        /// - Subject adds the caller name e.g. the test method calling this method.
        /// - Plain and HTML message comes from SQL-Server table
        /// </summary>
        /// <param name="pConfig">appropriate <see cref="MailConfiguration"/> item</param>
        /// <param name="pSendToo">Valid email address to send message too</param>
        /// <param name="identifier">SQL-Server table key</param>
        /// <param name="name">Represents who called this method</param>
        public void ExampleSendWithRepyToList(string pConfig, string pSendToo, int identifier, [CallerMemberName]string name = "")
        {
            var ops = new DataOperations();
            var data = ops.Read(identifier);

            var mc = new MailConfiguration(pConfig);
            var mail = new MailMessage
            {
                From = new MailAddress(mc.FromAddress),
                Subject = $"Sent from test: '{name}'"
            };

            mail.To.Add(pSendToo);

            mail.ReplyToList.Add(new MailAddress("oregon@gmail.com"));
            mail.ReplyToList.Add(new MailAddress("kevin@comcast.net"));

            var plainMessage = AlternateView.CreateAlternateViewFromString(
                data.TextMessage,
                null, "text/plain");

            var htmlMessage = AlternateView.CreateAlternateViewFromString(
                data.HtmlMessage,
                null, "text/html");

            mail.IsBodyHtml = true;

            mail.AlternateViews.Add(plainMessage);
            mail.AlternateViews.Add(htmlMessage);

            using (var smtp = new SmtpClient(mc.Host, mc.Port))
            {
                smtp.Credentials = new NetworkCredential(mc.UserName, mc.Password);
                smtp.EnableSsl = mc.EnableSsl;
                smtp.Send(mail);
            }

        }
        /// <summary>
        /// Since MailAddress.To is a collection you can add many addresses for sending.
        /// For testing this means you need multiple email addresses. It's easier in a
        /// company were they can create multiple test addresses while for personal use
        /// you need to create them and then be able to monitor them in testing. 
        /// </summary>
        private void SendMultiples()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Helper method to create a MailAddress with friendly name for FROM or TO address
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>MailAddress</returns>
        /// <remarks>
        /// Could be a in line function to if only used in one method for C# 7.
        /// </remarks>
        private MailAddress CreateFriendltAddress(MailFriendly sender) => new MailAddress(sender.Address, sender.Display);
        /// <summary>
        /// Example for showing friendly names in an email
        /// </summary>
        /// <param name="pConfig"></param>
        /// <param name="identifier"></param>
        /// <param name="pFromAddress"></param>
        /// <param name="pToAddress"></param>
        /// <param name="name">Method name as default with an empty string</param>
        public void ExampleSendMaskNames(string pConfig, int identifier, MailFriendly pFromAddress, MailFriendly pToAddress,  [CallerMemberName]string name = "")
        {
            var ops = new DataOperations();
            var data = ops.Read(identifier);

            var mc = new MailConfiguration(pConfig);
            var mail = new MailMessage
            {
                From = CreateFriendltAddress(pFromAddress), 
                Subject = $"Sent from test: '{name}'"
            };

            mail.To.Add(CreateFriendltAddress(pToAddress));
            
            var plainMessage = AlternateView.CreateAlternateViewFromString(
                data.TextMessage,
                null, "text/plain");

            var htmlMessage = AlternateView.CreateAlternateViewFromString(
                data.HtmlMessage,
                null, "text/html");

            mail.IsBodyHtml = true;

            mail.AlternateViews.Add(plainMessage);
            mail.AlternateViews.Add(htmlMessage);

            using (var smtp = new SmtpClient(mc.Host, mc.Port))
            {
                smtp.Credentials = new NetworkCredential(mc.UserName, mc.Password);
                smtp.EnableSsl = mc.EnableSsl;
                smtp.Send(mail);
            }

        }

        /// <summary>
        /// Example for showing friendly names in an email
        /// </summary>
        /// <param name="pConfig"></param>
        /// <param name="identifier"></param>
        /// <param name="pFromAddress"></param>
        /// <param name="pToAddress"></param>
        /// <param name="userPickupFolder">Toggle between sending live or sending to file</param>
        /// <param name="name">Method name as default with an empty string</param>
        public void UsePickupFolderExample(string pConfig, int identifier, MailFriendly pFromAddress, MailFriendly pToAddress,bool userPickupFolder = true, [CallerMemberName]string name = "")
        {
            var ops = new DataOperations();
            var data = ops.Read(identifier);

            var mc = new MailConfiguration(pConfig);
            var mail = new MailMessage
            {
                From = CreateFriendltAddress(pFromAddress), 
                Subject = $"Sent from test: '{name}'"
            };

            mail.To.Add(CreateFriendltAddress(pToAddress));

            var plainMessage = AlternateView.CreateAlternateViewFromString(
                data.TextMessage,
                null, "text/plain");

            var htmlMessage = AlternateView.CreateAlternateViewFromString(
                data.HtmlMessage,
                null, "text/html");

            mail.IsBodyHtml = true;

            mail.AlternateViews.Add(plainMessage);
            mail.AlternateViews.Add(htmlMessage);

            using (var smtp = new SmtpClient(mc.Host, mc.Port))
            {
                smtp.Credentials = new NetworkCredential(mc.UserName, mc.Password);

                if (userPickupFolder)
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtp.PickupDirectoryLocation = mc.PickupFolder;
                }

                smtp.EnableSsl = !userPickupFolder;
                smtp.Send(mail);
            }

        }

        /// <summary>
        /// Example for incorrect port used to send a email message.
        /// </summary>
        /// <param name="pConfig">appropriate <see cref="MailConfiguration"/> item</param>
        /// <param name="pSendToo">Valid email address to send message too</param>
        /// <param name="identifier">SQL-Server table key</param>
        /// <param name="name">Represents who called this method</param>
        public void ExampleSend2(string pConfig, string pSendToo, int identifier, [CallerMemberName]string name = "")
        {
            var ops = new DataOperations();
            var data = ops.Read(identifier);

            var mc = new MailConfiguration(pConfig);
            var mail = new MailMessage
            {
                From = new MailAddress(mc.FromAddress),
                Subject = $"Sent from test: '{name}'"
            };

            mail.To.Add(pSendToo);

            var plainMessage = AlternateView.CreateAlternateViewFromString(
                data.TextMessage, 
                null, 
                "text/plain");

            var htmlMessage = AlternateView.CreateAlternateViewFromString(
                data.HtmlMessage, 
                null, 
                "text/html");

            mail.IsBodyHtml = true;

            mail.AlternateViews.Add(plainMessage);
            mail.AlternateViews.Add(htmlMessage);

            using (var smtp = new SmtpClient("Bad host", mc.Port))
            {
                smtp.Credentials = new NetworkCredential(mc.UserName, mc.Password);
                smtp.EnableSsl = mc.EnableSsl;
                smtp.SendCompleted += Smtp_SendCompleted;

                try
                {
                    smtp.Send(mail);
                }
                catch (Exception generalException)
                {
                    switch (generalException)
                    {
                        case SmtpFailedRecipientsException _:
                            {
                                if (_writeToLog)
                                {
                                    WriteToLogFile("SmtpFailedRecipientsException", generalException.GetExceptionMessages());
                                }
                                break;
                            }

                        case SmtpException _:
                        {                           
                                if (_writeToLog)
                                {
                                    WriteToLogFile("General SmtpException", $"{generalException.GetExceptionMessages()}, Status code: {((SmtpException) generalException).StatusCode}");
                                }
                                break;
                            }

                        default:
                            if (_writeToLog)
                            {
                                Logger.Start(_LogInfo);
                                try
                                {
                                    // ReSharper disable once PossibleInvalidCastException
                                    WriteToLogFile("General Exception", $"{generalException.GetExceptionMessages()}, Status code: {((SmtpException)generalException).StatusCode}");
                                }
                                finally
                                {
                                    Logger.ShutDown();
                                }
                            }

                            break;
                    }
                }
            }
        }
        /// <summary>
        /// Send email with a callback, not no using statement is used as 
        /// doing so would circumvent the callback.
        /// </summary>
        /// <param name="pConfigurationSection"></param>
        /// <param name="pSendToo"></param>
        /// <returns></returns>
        public async Task ExampleSend3Async(string pConfigurationSection, string pSendToo)
        {
            var ops = new DataOperations();
            var data = ops.Read(5);

            var mc = new MailConfiguration(pConfigurationSection);

            var mail = new MailMessage
            {
                Subject = $"Async message: {data.Description}",
                From = new MailAddress(mc.FromAddress)
            };

            mail.To.Add(pSendToo);
            mail.Priority = MailPriority.High;


            var plainMessage = AlternateView.CreateAlternateViewFromString(data.TextMessage, null, "text/plain");
            var htmlMessage = AlternateView.CreateAlternateViewFromString(
                data.HtmlMessage, 
                null, 
                "text/html");

            mail.IsBodyHtml = true;

            mail.AlternateViews.Add(plainMessage);
            mail.AlternateViews.Add(htmlMessage);

            //send the message
            var smtp = new SmtpClient(mc.Host, mc.Port)
            {
                Credentials = new NetworkCredential(mc.UserName, mc.Password),
                EnableSsl = mc.EnableSsl
        };


            smtp.SendCompleted += Smtp_SendCompleted;

            smtp.SendCompleted += (s, e) => {
                smtp.Dispose();
                mail.Dispose();
            };

            await smtp.SendMailAsync(mail).ConfigureAwait(false);

        }
        #region For part 3 of this series - to be written shortly

        /// <summary>
        /// Send attachment from physical file on disk into email
        /// </summary>
        public void SendSingleAttachmentFromDisk()
        {
            throw new NotImplementedException();
        }
        public void SendMultipleAttachementsFromDisk()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Example for sending attachment via byte array into email
        /// </summary>
        public void SendSingleAttachmentFromMemory()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Embed image from physical file into email
        /// </summary>
        public void EmbedImageFromDisk()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Embed image from memory/array into email
        /// </summary>
        public void EmbedImageFromMemory()
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Central code for writing to log file
        /// </summary>
        /// <param name="pTitle"></param>
        /// <param name="pMessage"></param>
        private void WriteToLogFile(string pTitle, string pMessage)
        {
            Logger.Start(_LogInfo);

            try
            {
                var log = new Logger(pTitle);
                log.Log("", pMessage);
            }
            finally
            {
                Logger.ShutDown();
            }
        }
        /// <summary>
        /// Callback for sending an email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
            if (e.Cancelled == false && e.Error == null)
            {
                WriteToLogFile("Sent","Mail sent");
            }
            else
            {
                WriteToLogFile("Sent", "Mail not sent");
            }
        }

    }
}
