using SmtpMailConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;
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

            /*
             * Using non-existing addresses for testing purposes
             */
            mail.ReplyToList.Add(new MailAddress("jane@gmail.com"));
            mail.ReplyToList.Add(new MailAddress("kevin@comcast.net"));

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
        public void ExampleSendMaskNames(string pConfig, int identifier, MailFriendly pFromAddress, MailFriendly pToAddress, [CallerMemberName]string name = "")
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
        public void UsePickupFolderExample(string pConfig, int identifier, MailFriendly pFromAddress, MailFriendly pToAddress, bool userPickupFolder = true, [CallerMemberName]string name = "")
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
            mail.IsBodyHtml = true;

            mail.AlternateViews.PlainTextView(data.TextMessage);
            mail.AlternateViews.HTmlView(data.HtmlMessage);

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
                smtp.SendCompleted += OED_SendCompleted;

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
                                    WriteToLogFile("General SmtpException", $"{generalException.GetExceptionMessages()}, Status code: {((SmtpException)generalException).StatusCode}");
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

        public XmlDocument EmailXmlDocument()
        {
            var emailBody = new XmlDocument();
            emailBody.Load("EmployerNotificationEmailTemplate.xml");

            return emailBody;
        }
        public string EmailBodyFromXml()
        {
            // ReSharper disable once PossibleNullReferenceException
            return EmailXmlDocument().SelectNodes("/email/body")[0].InnerText;
        }

        public void OED_1(string pConfig, string pSendToo, int identifier, [CallerMemberName] string name = "")
        {

            var bodyText = EmailBodyFromXml();
            bodyText = bodyText.Replace("##E_RESPONSE_URL##", "https://testuisides.org");
            bodyText = bodyText.Replace("##PIN##","5555");
            bodyText = bodyText.Replace("##EMPLOYER_NAME##","ABC Inc");
            bodyText = bodyText.Replace("##CLAIMANT_SSN##", "555-55-5555");
            bodyText = bodyText.Replace("##RESPONSE_DUE_DATE##",DateTime.Now.AddDays(30).ToString("D"));
            var mc = new MailConfiguration(pConfig);
            var mail = new MailMessage
            {
                From = new MailAddress(mc.FromAddress),
                Subject = "Testing email for S I D E S",
                IsBodyHtml = true
            };

            mail.To.Add(pSendToo);

            mail.Bcc.Add("karen.1.payne@oregon.gov");
            mail.Bcc.Add("Lisa.A.SMITH-BURHAM@oregon.gov");

            mail.Bcc.Add("Bill.RICKMAN@oregon.gov");


            mail.Body = "This is a <strong>test</strong> on generating an email activity log/sniffer with CC and BCC.<br><span style='color:green'>No response needed</span>";

            int? port = null;
            var smtp = new SmtpClient(mc.Host, 444)
            {
                Credentials = new NetworkCredential(mc.UserName, mc.Password),
                EnableSsl = mc.EnableSsl
            };

            smtp.SendCompleted += OED_SendCompleted;

            smtp.SendCompleted += (s, e) =>
            {
                smtp.Dispose();
                mail.Dispose();
            };

            try
            {
                smtp.SendAsync(mail, mail);
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
                                WriteToLogFile("General SmtpException", $"{generalException.GetExceptionMessages()}, Status code: {((SmtpException)generalException).StatusCode}");
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
        public void ExampleSendCustomClient(string pConfig, string pSendToo, int identifier, [CallerMemberName]string name = "")
        {
            var ops = new DataOperations();
            var data = ops.Read(identifier);

            var mc = new MailConfiguration(pConfig);
            var mail = new MailMessage
            {
                From = new MailAddress(mc.FromAddress),
                Subject = "Testing email for S I D E S"
            };

            mail.To.Add(pSendToo);
            mail.CC.Add("Lisa.A.SMITH-BURHAM@oregon.gov");
            mail.CC.Add("Bill.RICKMAN@oregon.gov");

            mail.Bcc.Add("karen.1.payne@oregon.gov");

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

            var smtp = new SmtpClientCustom(mc.Host, mc.Port)
            {
                Credentials = new NetworkCredential(mc.UserName, mc.Password),
                EnableSsl = mc.EnableSsl
            };

            smtp.SendCompleted += OED_SendCompleted;

            smtp.SendCompleted += (s, e) =>
            {
                smtp.Dispose();
                mail.Dispose();
            };

            try
            {
                smtp.SendAsync(mail,mail);
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
                                WriteToLogFile("General SmtpException", $"{generalException.GetExceptionMessages()}, Status code: {((SmtpException)generalException).StatusCode}");
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
        /// <summary>
        /// Send email with a callback, not no using statement is used as 
        /// doing so would circumvent the callback.
        /// </summary>
        /// <param name="pConfigurationSection"></param>
        /// <param name="pSendToo"></param>
        /// <returns></returns>
        public async Task ExampleSend3Async(string pConfigurationSection, string pSendToo, [CallerMemberName]string name = "")
        {
            var ops = new DataOperations();
            var data = ops.Read(5);

            var mc = new MailConfiguration(pConfigurationSection);

            var mail = new MailMessage
            {
                Subject = $"Called from: {name}",
                From = new MailAddress(mc.FromAddress)
            };

            mail.To.Add(pSendToo);
            mail.Priority = MailPriority.High;
            mail.IsBodyHtml = true;

            mail.AlternateViews.PlainTextView(data.TextMessage);
            mail.AlternateViews.HTmlView(data.HtmlMessage);

            //send the message
            var smtp = new SmtpClient(mc.Host, mc.Port)
            {
                Credentials = new NetworkCredential(mc.UserName, mc.Password),
                EnableSsl = mc.EnableSsl
            };


            smtp.SendCompleted += OED_SendCompleted;

            smtp.SendCompleted += (s, e) =>
            {
                Console.WriteLine("Disposing");
                smtp.Dispose();
                mail.Dispose();
            };

            Console.WriteLine("Sending");
            // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
            await smtp.SendMailAsync(mail).ConfigureAwait(false);

        }
        #region For part 3 of this series - to be written shortly
        /// <summary>
        /// Example which sends all files in a specific folder
        /// </summary>
        /// <param name="pConfig">appropriate <see cref="MailConfiguration"/> item</param>
        /// <param name="pSendToo">Valid email address to send message too</param>
        /// <param name="identifier">SQL-Server table key</param>
        /// <param name="name">Represents who called this method</param>
        /// <remarks>
        /// For a real application
        /// - make sure the folder exists and there are files if this is a busness requirement.
        /// </remarks>
        public void SendMultipleAttachementsFromDisk(string pConfig, string pSendToo, int identifier, [CallerMemberName]string name = "")
        {
            var files = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files1"));
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

            foreach (var file in files)
            {
                mail.Attachments.Add(new Attachment(file));
            }

            using (var smtp = new SmtpClient(mc.Host, mc.Port))
            {
                smtp.Credentials = new NetworkCredential(mc.UserName, mc.Password);
                smtp.EnableSsl = mc.EnableSsl;
                smtp.Send(mail);
            }

        }
        /// <summary>
        /// Add attachments from a folder were each file in the folder is added without
        /// any conditions.
        /// </summary>
        /// <param name="pConfig">appropriate <see cref="MailConfiguration"/> item</param>
        /// <param name="pSendToo">Valid email address to send message too</param>
        /// <param name="identifier">SQL-Server table key</param>
        /// <param name="name">Represents who called this method</param>
        public void SendMultipleAttachementsFromByeArray(string pConfig, string pSendToo, int identifier, [CallerMemberName]string name = "")
        {
            var files = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files1"));
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
            mail.Attachments.AddFilesFromStream(files);

            using (var smtp = new SmtpClient(mc.Host, mc.Port))
            {
                smtp.Credentials = new NetworkCredential(mc.UserName, mc.Password);
                smtp.EnableSsl = mc.EnableSsl;
                smtp.Send(mail);
            }
        }
        /// <summary>
        /// Embed an image into an email message. In this sample the image is setup
        /// within this method but could have been passed in as a parameter.
        /// </summary>
        /// <param name="pConfig">appropriate <see cref="MailConfiguration"/> item</param>
        /// <param name="pSendToo">Valid email address to send message too</param>
        /// <param name="name">Represents who called this method</param>
        public void EmbedImageFromDisk(string pConfig, string pSendToo, [CallerMemberName]string name = "")
        {
            var mc = new MailConfiguration(pConfig);
            var mail = new MailMessage
            {
                From = new MailAddress(mc.FromAddress),
                Subject = $"Sent from test: '{name}'"
            };

            var plainMessage = AlternateView.CreateAlternateViewFromString(
                "This email desires html",
                null, "text/plain");

            /*
            *  This is the identifier for embeding an image into the email message.
            *  A variable is used because the identifier is needed into two areas,
            *  first in the AlternateView for HTML and secondly for the LinkedResource.
            */
            var imageIdentifier = "Miata";

            var htmlMessage = AlternateView.CreateAlternateViewFromString(
                $"<p>This is what I'm purchasing in <b>2019</b> to go along with my 2016 model.</p><img src=cid:{imageIdentifier}><p>Karen</p>",
                null, "text/html");

            var fileName = $"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images1")}\\2017Miata.jpg";
            var miataImage = new LinkedResource(fileName, "image/jpeg") { ContentId = imageIdentifier };
            mail.AlternateViews.Add(plainMessage);
            mail.AlternateViews.Add(htmlMessage);
            htmlMessage.LinkedResources.Add(miataImage);

            mail.To.Add(pSendToo);
            mail.IsBodyHtml = true;

            using (var smtp = new SmtpClient(mc.Host, mc.Port))
            {
                smtp.Credentials = new NetworkCredential(mc.UserName, mc.Password);
                smtp.EnableSsl = mc.EnableSsl;
                smtp.Send(mail);
            }
        }
        #endregion

        #region Logging 

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

        #endregion
        /// <summary>
        /// Callback for sending an email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArguments"></param>
        private void OED_SendCompleted(object sender, AsyncCompletedEventArgs eventArguments)
        {
            var emailMessageContainer = new EmailSniffer();

            if (eventArguments.UserState is MailMessage mailMessage)
            {

                emailMessageContainer.Inspect((SmtpClient)sender, eventArguments);

            }
            else
            {
                emailMessageContainer.Inspect();
            }
        }
        private void Smtp_SendCompleted1(object sender, AsyncCompletedEventArgs eventArguments)
        {
            var emailMessageContainer = new EmailSniffer();

            if (eventArguments.UserState is MailMessage mailMessage)
            {

                emailMessageContainer.Inspect((SmtpClient)sender, eventArguments);

            }
            else
            {
                emailMessageContainer.Inspect();
            }

        }
    }

}
