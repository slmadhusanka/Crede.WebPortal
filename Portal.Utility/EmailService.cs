using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Utility
{
    public class EmailService
    {
        /// <summary>
        /// Basic send mail function. Just call and mail
        /// <remarks>Can not handle a list of recivers - only one in each of the fields To, CC, BCC</remarks>
        /// </summary>
        /// <param name="ToDisplayName"></param>
        /// <param name="ToAdr"></param>
        /// <param name="FromDisplayName"></param>
        /// <param name="FromAdr"></param>
        /// <param name="CcDisplayName"></param>
        /// <param name="CcAdr"></param>
        /// <param name="BccAdr"></param>
        /// <param name="Subject"></param>
        /// <param name="BodyText"></param>
        /// <param name="AttachmentFileName"></param>
        /// <param name="VerboseLevel">Default logging errors and warnings.</param>
        public static void BasicSendMail(string ToDisplayName, string ToAdr
            , string FromDisplayName, string FromAdr
            , string CcDisplayName, string CcAdr
            , string BccAdr
            , string Subject, string BodyText, string AttachmentFileName, string spoofingEmailAdress)
        {
            MailMessage mes = new MailMessage();
            char[] sepr = new char[] { ',', ';' };
            string _fromadd = "";
            string _fromdisp = "";
            string[] _toadd = new string[50];
            string[] _todisp = new string[50];
            string[] _ccadd = new string[50];
            string[] _ccdisp = new string[50];
            string[] _bccadd = new string[50];
            string[] _bccdisp = new string[50];
            string[] _attachFile = new string[50];
            #region FROM
            /*string _fromdisp = !String.IsNullOrEmpty(FromDisplayName) ? FromDisplayName : FromAdr;
			mes.From = new MailAddress(FromAdr, _fromdisp);*/
            if (!String.IsNullOrEmpty(FromAdr))
                _fromadd = FromAdr;
            if (!String.IsNullOrEmpty(FromDisplayName))
                _fromdisp = FromDisplayName;
            mes.From = new MailAddress(_fromadd); //new MailAddress(_fromadd,_fromdisp);
            #endregion
            #region TO
            /*string _todisp = !String.IsNullOrEmpty(ToDisplayName) ? ToDisplayName : ToAdr;
			mes.To.Add(new MailAddress(ToAdr, _todisp));*/
            if (!String.IsNullOrEmpty(ToAdr))
            {
                _toadd = ToAdr.Split(sepr);
            }
            if (!String.IsNullOrEmpty(ToDisplayName))
            {
                _todisp = ToDisplayName.Split(sepr);
            }
            for (int i = 0; i < _toadd.Length; i++)
            {
                if (!String.IsNullOrEmpty(_toadd[i]))
                    mes.To.Add(new MailAddress(_toadd[i]));
            }

            #endregion
            #region CC
            /*if (!String.IsNullOrEmpty(CcAdr))
			{
				string _ccdisp = !String.IsNullOrEmpty(CcDisplayName) ? CcDisplayName : CcAdr;
				mes.CC.Add(new MailAddress(CcAdr, _ccdisp));
			}*/
            if (!String.IsNullOrEmpty(CcAdr))
            {
                _ccadd = CcAdr.Split(sepr);
            }
            if (!String.IsNullOrEmpty(CcDisplayName))
            {
                _ccdisp = CcDisplayName.Split(sepr);
            }
            //for (int i = 0; i < _ccadd.Length; i++)
            //{
            //    if (!String.IsNullOrEmpty(_ccadd[i]))
            //        mes.CC.Add(new MailAddress(_ccadd[i], _ccdisp[i]));
            //}
            #endregion
            #region BCC
            /*if (!String.IsNullOrEmpty(BccAdr))
				mes.Bcc.Add(new MailAddress(BccAdr, BccAdr));*/
            if (!String.IsNullOrEmpty(BccAdr))
            {
                _bccadd = BccAdr.Split(sepr);
            }
            /*if (!String.IsNullOrEmpty(BccDisplayName))
            {
                _bccdisp = BccDisplayName.Split(sepr);
            }*/
            //for (int i = 0; i < _bccadd.Length; i++)
            //{
            //    if (!String.IsNullOrEmpty(_bccadd[i]))
            //        mes.Bcc.Add(new MailAddress(_bccadd[i]));
            //}
            #endregion
            #region Subject & Body
            mes.Subject = Subject;
            //mes.SubjectEncoding = System.Text.Encoding.UTF8;
            mes.IsBodyHtml = true; //System.Web.Mail.MailFormat.Html;
            mes.Body = BodyText;
            mes.BodyEncoding = System.Text.Encoding.UTF8;
            #endregion

            #region Atachment
            if (!String.IsNullOrEmpty(AttachmentFileName))
            {
                _attachFile = AttachmentFileName.Split(sepr);
            }
            for (int i = 0; i < _attachFile.Length; i++)
            {
                if (!String.IsNullOrEmpty(_attachFile[i]))
                    mes.Attachments.Add(new Attachment(_attachFile[i]));

            }
            #endregion

            string LS_MailServerIP = ConfigurationManager.AppSettings["MailServerIP"].ToString();
            string LS_MailServerSenderAddress = ConfigurationManager.AppSettings["system_email_sender"].ToString();
            string system_email_sender_password = ConfigurationManager.AppSettings["system_email_sender_password"].ToString();


            try
            {

                SendMailByGmail(ToDisplayName,
                    ToAdr
            , FromDisplayName, FromAdr
            , CcDisplayName, CcAdr
            , BccAdr
            , Subject, BodyText, AttachmentFileName, spoofingEmailAdress);
            }

            catch (System.Exception ex)
            {
                string errmes = "LM_EMail.BasicSendMail() error. Mail post attemt: " +
                    "\nToDisplayName=" + ToDisplayName +
                    "\nToAdr=" + ToAdr +
                    "\nFromDisplayName=" + FromDisplayName +
                    "\nFromAdr=" + FromAdr +
                    "\nCcDisplayName=" + CcDisplayName +
                    "\nCcAdr=" + CcAdr +
                    "\nBccAdr=" + BccAdr +
                    "\nSubject=" + Subject +
                    "\nBodyText=" + BodyText +
                    "\nAttachmentFileName=" + AttachmentFileName +
                    "\nErrormessage:" + ex.Message +
                    "\nSource:" + ex.Source +
                    "\nStack:" + ex.StackTrace +
                    "\nInnerMessage:" + ex.InnerException != null ? ex.InnerException.Message : "No inner exception exist";

                throw (new System.Exception(errmes));
            }
            finally
            {

            }

        }


        public static void SendMailByGmail(string ToDisplayName, string ToAdr
            , string FromDisplayName, string FromAdr
            , string CcDisplayName, string CcAdr
            , string BccAdr
            , string Subject, string BodyText, string AttachmentFileName, string spoofingEmailAdress)
        {

            string LS_MailServerIP = ConfigurationManager.AppSettings["MailServerIP"].ToString();
            string LS_MailServerSenderAddress = ConfigurationManager.AppSettings["system_email_sender"].ToString();
            string system_email_sender_password = ConfigurationManager.AppSettings["system_email_sender_password"].ToString();

            MailMessage MailObj = new MailMessage();
            MailObj.To.Add(ToAdr);
            //MailObj.From = "GmailAccount@gmail.com";
            MailObj.From = new MailAddress(LS_MailServerSenderAddress, FromDisplayName);
            //MailObj.Cc = "cc@cc.com";

            MailObj.IsBodyHtml = true;

            MailObj.Priority = MailPriority.Normal;
            // string sAttach = @"c:\yourpic.jpg";
            if (!string.IsNullOrEmpty(AttachmentFileName))
                MailObj.Attachments.Add(new Attachment(AttachmentFileName));
            MailObj.Subject = Subject;
            MailObj.Body = BodyText;
            SmtpClient smtpcli = new SmtpClient(LS_MailServerIP, 587); //use this PORT! and "smtp.gmail.com"
            smtpcli.EnableSsl = true;
            smtpcli.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpcli.Credentials = new NetworkCredential(LS_MailServerSenderAddress, system_email_sender_password);
            try
            {
                smtpcli.Send(MailObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
