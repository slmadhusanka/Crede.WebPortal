using Portal.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Portal.BIZ
{
    public class Logger
    {
        public static void log(Exception ex, string URL)
        {
            StringBuilder sbExceptionMessage = new StringBuilder();

            List<Exception> exceptionList = GetInnerExceptions(ex);

            if (exceptionList.Any())
            {
                foreach (Exception inex in exceptionList)
                {
                    sbExceptionMessage.Append("<p>DateTime : " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "</p>");
                    sbExceptionMessage.Append("<p>Exception Type : " + inex.GetType().Name + "</p>");
                    sbExceptionMessage.Append("</br></br>");
                    sbExceptionMessage.Append("<p>Message : <b>" + inex.Message + "</b></p>");
                    sbExceptionMessage.Append("<p>Stack trace : </p>");
                    string stack_trace = Regex.Replace(inex.StackTrace, @"\r\n?|\n", "<br />");
                    sbExceptionMessage.Append("<p>" + stack_trace + "</p>");
                    sbExceptionMessage.Append("</br></br>");
                }
            }

            string portal_name = ConfigurationManager.AppSettings["PortalName"].ToString();
            string delevoper_1_email = ConfigurationManager.AppSettings["Deleloper_1"].ToString();
            string FromAdr = ConfigurationManager.AppSettings["system_email_sender"].ToString();
            if (portal_name.Length == 0 || delevoper_1_email.Length == 0 || FromAdr.Length == 0)
            {
                return;
            }

            string Subject = "*** Error in " + portal_name + " ***";

            StringBuilder sbMailBody = new StringBuilder();
            sbMailBody.Append("<p>Hi developer</p>");
            sbMailBody.Append("<p>An exception has occurred in " + portal_name + "</p>");
            sbMailBody.Append("<p>Please try to fix the issue ASAP</p>");
            sbMailBody.Append("</br>");
            sbMailBody.AppendLine("<p>=====================================================</p>");
            sbMailBody.Append("</br>");
            sbMailBody.Append("<p>URL : " + URL + "</p>");
            sbMailBody.Append(sbExceptionMessage);

            string ToDisplayName = string.Empty;
            string ToAdr = delevoper_1_email;
            string FromDisplayName = string.Empty;

            string CcDisplayName = string.Empty;
            string CcAdr = string.Empty;
            string BccAdr = string.Empty;
            string BodyText = sbMailBody.ToString();
            string AttachmentFileName = string.Empty;
            try
            {
                string AuthenticatedSMTP = ConfigurationManager.AppSettings["AuthenticatedSMTP"].ToString();

                if (AuthenticatedSMTP == "No")
                {
                    MailMessage mMailMessage = new MailMessage();

                    string server = ConfigurationManager.AppSettings["MailServerIP"].ToString(); ;

                    mMailMessage.From = new MailAddress(FromAdr);
                    mMailMessage.IsBodyHtml = true;
                    mMailMessage.To.Add(new MailAddress(delevoper_1_email));
                    mMailMessage.Subject = Subject;
                    // Set the body of the mail message
                    mMailMessage.Body = BodyText;

                    SmtpClient mSmtpClient = new SmtpClient(server);
                    mSmtpClient.Port = 587;
                    mSmtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                    mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mSmtpClient.Send(mMailMessage);

                    mSmtpClient.Send(mMailMessage);
                }
                else
                {
                    string spoofingEmailAdress = ConfigurationManager.AppSettings["NotificationEmailSpoofing"].ToString();
                    EmailService.BasicSendMail(ToDisplayName, ToAdr, FromDisplayName, FromAdr, CcDisplayName, CcAdr, BccAdr, Subject, BodyText, AttachmentFileName, spoofingEmailAdress);
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public static List<Exception> GetInnerExceptions(Exception ex)
        {
            List<Exception> exceptions = new List<Exception>();
            IEnumerable<Exception> innerExceptions = Enumerable.Empty<Exception>();
            exceptions.Add(ex);

            Exception currentEx = ex;
            if (currentEx.InnerException != null)
            {
                innerExceptions = new Exception[] { ex.InnerException };
                foreach (var innerEx in innerExceptions)
                {
                    exceptions.Add(innerEx);
                }
            }

            // Reverse the order to the innermost is first
            exceptions.Reverse();

            return exceptions;
        }
    }
}
