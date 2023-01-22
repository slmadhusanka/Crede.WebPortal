using Newtonsoft.Json;
using Portal.BIZ.HelperModel;
using Portal.DAL;
using Portal.Provider;
using Portal.Provider.Model;
using Portal.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Portal.BIZ
{
    public class ClsSecurity
    {
        /// <summary>
        /// Author  : Tanvir Ehsan
        /// Date    : Monday,August 29,2011.
        /// Purpose : Do all the works(Removing Identity...) for Loging out an User.
        /// </summary>
        public static void Logout()
        {
            try
            {
                ///Create Empty Roles
                string[] roles = { "", "" };
                ///Create Empty Identity
                System.Security.Principal.GenericIdentity identity = new System.Security.Principal.GenericIdentity("", "");
                ///Creating Empty Pricipal
                System.Security.Principal.GenericPrincipal pricipal = new System.Security.Principal.GenericPrincipal(identity, roles);
                ///Assigning Empty Principal
                System.Threading.Thread.CurrentPrincipal = pricipal;
                HttpContext.Current.User = pricipal;
                ///Removing all session values. 
                BUSessionUtility.BUSessionContainer.DO_NULL = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        public static string DoLogin(string UserName, string browser_fingerprint)
        {
            string returnVal = null;
            try
            {
                //Before login it is set to logout first. So that it could be ensured that all data for
                //any user is reset 
                USERS OBJ = new USERS();
                Logout();

                string[] vROLE_PERM_ALL;
                // pFUNCTION_GROUP_ID will have to implement later for diffrent type of User: Admin,User, System User
                //Validate from Membership Provider
                bool vVALIDATE = true; //Membership.ValidateUser(pUSER_ID, pPASSWORD);
                if (vVALIDATE)
                {
                    //Getting the Roles from Role Provider
                    vROLE_PERM_ALL = Roles.GetRolesForUser(UserName);
                    //Initialize the Forms Authentication according to configuration settings.
                    FormsAuthentication.Initialize();
                    //Creating Forms Authentication ticket. This ticket will work through out 
                    //the browser life time
                    FormsAuthenticationTicket vFTK = new FormsAuthenticationTicket(UserName, true, 120);
                    //Creating an Identity obejct with the ticket
                    secIdentity vID = secIdentity.PortalIdentity(vFTK);
                    //Setting up the Pricipal with the Identity and Roles                
                    secPrincipal.PortalPrincipal(vID, vROLE_PERM_ALL);

                    //UpdateRowSource Last Successful Login
                    UpdateUserLastSuccessfullLogin(UserName);

                    // Get roles
                    var roleSet = ClsSystem.GetAllRoleList();

                    //Fetch values(Ex. )  for the logged in User.
                    //
                    // Get CompanyName
                    DataTable tbl_sysSettings = ClsSystem.GetSystemSetting().Tables[0];
                    string companyname = tbl_sysSettings.AsEnumerable().Where(r => r.Field<string>("ConfigurationName") == "Company Name").FirstOrDefault()["Value"].ToString();
                    string sessionTimeOut = tbl_sysSettings.AsEnumerable().Where(r => r.Field<string>("ConfigurationName") == "Session TimeOut").FirstOrDefault()["Value"].ToString();

                    //and keep the necessary into the session
                    OBJ = ClsSecurityManage.GetSpecificUser(UserName);
                    BUSessionUtility.BUSessionContainer.USER_ID = OBJ.User_ID;
                    BUSessionUtility.BUSessionContainer.LAST_LOGIN_DT = OBJ.LastLoginDate;
                    BUSessionUtility.BUSessionContainer.FirstName = OBJ.FirstName;
                    BUSessionUtility.BUSessionContainer.LastName = OBJ.LastName;
                    BUSessionUtility.BUSessionContainer.UserName = OBJ.UserName;
                    BUSessionUtility.BUSessionContainer.LAST_LOGIN_DT = OBJ.LastLoginDate;
                    BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG = OBJ.Force_Password_Changed_Flag;
                    BUSessionUtility.BUSessionContainer.Email = OBJ.Email;
                    BUSessionUtility.BUSessionContainer.LastPasswordChangedDate = OBJ.LastPasswordChangedDate;
                    BUSessionUtility.BUSessionContainer.LastPasswordChangedDate = OBJ.CreationDate;
                    BUSessionUtility.BUSessionContainer.ROLES_FOR_USER = OBJ.RoleCode;

                    BUSessionUtility.BUSessionContainer.TwoWayAuthActivied = OBJ.TwoWayAuthActivied;
                    BUSessionUtility.BUSessionContainer.PhoneNumber = OBJ.PhoneNumber;
                    BUSessionUtility.BUSessionContainer.CompanyName = companyname;
                    //check trusted browser
                    BUSessionUtility.BUSessionContainer.IsTrustedBrowser = CheckIfTrustedBrowser(Convert.ToInt32(OBJ.User_ID), browser_fingerprint);
                    BUSessionUtility.BUSessionContainer.ROLE = roleSet.Where(o => o.RoleCode == Convert.ToInt32(OBJ.RoleCode)).Select(o => o.Description).FirstOrDefault();
                    BUSessionUtility.BUSessionContainer.SessionTimeOut = sessionTimeOut;


                    returnVal = string.Empty;


                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(new List<LogType>
                        {
                            new LogType(){ Key = "User_ID", Value = OBJ.User_ID },
                            new LogType(){ Key = "UserName", Value = OBJ.UserName },
                            new LogType(){ Key = "Email", Value = OBJ.Email },
                            new LogType(){ Key = "RoleID", Value = OBJ.RoleCode },
                            new LogType(){ Key = "Role", Value = BUSessionUtility.BUSessionContainer.ROLE },
                        }),
                        Action = LogAction.Edit.Value,
                        Module = "USER_LOGGED",
                        ModuleID = Convert.ToInt32(OBJ.User_ID),
                        TableName = LogTable.Users.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName = BUSessionUtility.BUSessionContainer.UserName,
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    returnVal = PortalMembershipProvider.pfailureType;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnVal;
        }
        public static void UpdateUserLastSuccessfullLogin(string UserName)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            string SP_NAME = "update_users_login";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            string DaysLeftToforcePassword = ConfigurationManager.AppSettings["DaysLeftToforcePassword"].ToString();
            int recAdded = 0;
            try
            {
                objList.Add(new DSSQLParam("UserName", UserName, false));
                objList.Add(new DSSQLParam("DaysLeftToforcePassword", DaysLeftToforcePassword, false));
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SP_NAME, CommandType.StoredProcedure, objList);

                if (recAdded > 0)
                {

                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    objDbCommand.Transaction.Rollback();

                }

            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
            }
            finally
            {
                objDbCommand.Connection.Close();

            }

        }
        public static string SendPasswordResetUrl(string user_id, string Company, bool forattmpt)
        {
            string ToDisplayName = string.Empty;
            string ToAdr = string.Empty;
            string FromDisplayName = string.Empty;
            string FromAdr = string.Empty;
            string CcDisplayName = string.Empty;
            string CcAdr = string.Empty;
            string BccAdr = string.Empty;
            string Subject = string.Empty;
            string BodyText = string.Empty;
            string AttachmentFileName = string.Empty;
            string updated_record_id = string.Empty;
            string result = string.Empty;
            USERS obj_USERS = new USERS();

            string strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            string WebSiteUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
            string forget_password_url = WebSiteUrl + ConfigurationManager.AppSettings["ForgetPasswordUrl"].ToString();

            if (ConfigurationManager.AppSettings["isEmail"].ToString() == "0")
            {
                // forget_password_url = forget_password_url + "?Data=" + EncryptedURL(obj_USERS.User_ID);
                //return forget_password_url;
                return string.Empty;

            }
            try
            {
                obj_USERS = ClsSecurityManage.GetSpecificUser(user_id);
                string pr = Guid.NewGuid().ToString();
                forget_password_url = forget_password_url + "?Data=" + EncryptedURL(obj_USERS.User_ID) + "&pr=" + pr;
                //record password reset request
                //get password reset link expire time
                DateTime passwordExpireDate = DateTime.Now.AddMinutes(10);
                DataTable tbl_sysSettings = ClsSystem.GetSystemSetting().Tables[0];
                if (tbl_sysSettings != null && tbl_sysSettings.Rows.Count > 0)
                {
                    string passwordExpireMinutes = tbl_sysSettings.AsEnumerable().Where(r => r.Field<string>("ConfigurationName") == "PasswordResetLinkExpire").FirstOrDefault()["Value"].ToString();
                    if (!string.IsNullOrEmpty(passwordExpireMinutes))
                    {
                        passwordExpireDate = DateTime.Now.AddMinutes(Convert.ToInt32(passwordExpireMinutes));
                    }
                }
                ClsSecurityManage.AddPasswrodResetRequest(Convert.ToInt32(user_id), pr, DateTime.Now, passwordExpireDate, false);
                ToDisplayName = user_id;
                ToAdr = obj_USERS.Email;
                FromDisplayName = string.Empty;
                FromAdr = ConfigurationManager.AppSettings["system_email_sender"].ToString();
                CcDisplayName = string.Empty;
                CcAdr = string.Empty;
                BccAdr = string.Empty;

                string Product = ConfigurationManager.AppSettings["Product"].ToString();

                string emailFormatTextFile = string.Empty;
                if (forattmpt)
                {

                    Subject = "Password reset to " + Company + " " + Product;
                    emailFormatTextFile = ConfigurationManager.AppSettings["Email_Format_ForgetPassword_Four_Attament_Fail"].ToString();
                }
                else
                {
                    Subject = "Request a password reset to " + Company + " " + Product;
                    emailFormatTextFile = ConfigurationManager.AppSettings["Email_Format_ForgetPassword"].ToString();
                }
                string emailFormat = File.ReadAllText(emailFormatTextFile);
                emailFormat = emailFormat.Replace("<Company>", Company);
                emailFormat = emailFormat.Replace("<UserId>", obj_USERS.UserName);
                emailFormat = emailFormat.Replace("<URL>", "<a href=" + "\"" + forget_password_url + "\"" + " >Reset Password</a>");

                //string company_name = Company //ConfigurationManager.AppSettings["Company_Name"].ToString();

                //                StringBuilder BodyTextN = new StringBuilder();

                //                BodyText = @"<html> <body> A request to reset password to Username <b>"+ obj_USERS.UserName + "</b> " + company_name + @" account has been received. <br/>
                //If you requested " + company_name + @" password reset, please click on the link:
                //<br/>
                //<a href=" +"\""+forget_password_url+"\""+" >Reset Password</a> </body> </html>";  
                AttachmentFileName = string.Empty;

                string spoofingEmailAdress = ConfigurationManager.AppSettings["NotificationEmailSpoofing"].ToString();

                EmailService.SendMailByGmail(ToDisplayName, ToAdr, FromDisplayName, FromAdr, CcDisplayName, CcAdr, BccAdr, Subject, emailFormat, AttachmentFileName, spoofingEmailAdress);
                result = "sucess";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static string SendPasswordResetUrl(string UserName)
        {
            string return_val = string.Empty;
            string ToDisplayName = string.Empty;
            string ToAdr = string.Empty;
            string FromDisplayName = string.Empty;
            string FromAdr = string.Empty;
            string CcDisplayName = string.Empty;
            string CcAdr = string.Empty;
            string BccAdr = string.Empty;
            string Subject = string.Empty;
            string BodyText = string.Empty;
            string AttachmentFileName = string.Empty;
            string updated_record_id = string.Empty;
            USERS obj_USERS = new USERS();
            string forget_password_url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/ForgetPassword.aspx";

            if (ConfigurationManager.AppSettings["isEmail"].ToString() == "0")
            {
                // forget_password_url = forget_password_url + "?Data=" + EncryptedURL(obj_USERS.User_ID);
                //return forget_password_url;
                return string.Empty;

            }
            try
            {
                obj_USERS = ClsSecurityManage.GetSpecificUser(UserName);
                if (obj_USERS.IsLockedOut == "False")
                {
                    forget_password_url = forget_password_url + "?Data=" + EncryptedURL(obj_USERS.User_ID);
                    ToDisplayName = UserName;
                    ToAdr = obj_USERS.Email;
                    FromDisplayName = string.Empty;
                    FromAdr = ConfigurationManager.AppSettings["system_email_sender"].ToString();
                    CcDisplayName = string.Empty;
                    CcAdr = string.Empty;
                    BccAdr = string.Empty;
                    string company_name = ConfigurationManager.AppSettings["Company_Name"].ToString(); //BUSessionUtility.BUSessionContainer.CompanyName;
                    Subject = "Request a password reset to " + company_name + " account";
                    StringBuilder BodyTextN = new StringBuilder();

                    BodyText = @"<html> <body> A request to reset your password for your " + company_name + @" account has been received. <br/>
If you requested " + company_name + @" password reset, please click on the link:
<br/>
<a href=" + "\"" + forget_password_url + "\"" + " >Reset Password</a> <br/><br/>If you did not request a password reset, please ignore this email. Your account is still secure.</body> </html>";
                    AttachmentFileName = string.Empty;

                    string spoofingEmailAdress = ConfigurationManager.AppSettings["NotificationEmailSpoofing"].ToString();

                    EmailService.SendMailByGmail(ToDisplayName, ToAdr, FromDisplayName, FromAdr, CcDisplayName, CcAdr, BccAdr, Subject, BodyText.ToString(), AttachmentFileName, spoofingEmailAdress);



                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(new LogType
                        {
                            Key = ToAdr,
                            Value = BodyText.ToString()
                        }),

                        Action = LogAction.EmailSend.Value,
                        Module = "SendPasswordResetUrl",
                        ModuleID = 0,
                        TableName = LogTable.Users.Value,

                        UserID = Convert.ToInt32(obj_USERS.User_ID),
                        UserName = obj_USERS.UserName,
                        Email = obj_USERS.Email,
                        UserRole = "",
                        UserRoleID = Convert.ToInt32(obj_USERS.RoleCode),
                    });


                    return obj_USERS.UserName;

                }
                else
                {
                    return_val = "Locked";
                }
            }
            catch (Exception ex)
            {
                return_val = string.Empty;
                throw ex;
                // return ex.Message;
            }
            return return_val;
        }
        private static string EncryptedURL(string USER_ID)
        {
            if (string.IsNullOrEmpty(USER_ID))
                return string.Empty;
            string vData = USER_ID;
            EncryptedQueryString QueryString = new EncryptedQueryString();
            if (!string.IsNullOrEmpty(vData))
            {
                QueryString.Add("USER_ID", USER_ID);
            }
            return QueryString.ToString();
        }

        public static bool SendReport(string filename, string filenameForEmail, string To, string Cc, string BCc, string subject, string body)
        {
            string ToDisplayName = string.Empty;
            string ToAdr = To;
            string FromDisplayName = string.Empty;
            string FromAdr = string.Empty;
            string CcDisplayName = string.Empty;
            string CcAdr = Cc;
            string BccAdr = BCc;
            string Subject = subject;
            string BodyText = string.Empty;
            string AttachmentFileName = string.Empty;

            bool result = false;

            if (Convert.ToString(ConfigurationManager.AppSettings["isEmail"]) == "0")
            {
                return false;
            }
            try
            {
                FromAdr = Convert.ToString(ConfigurationManager.AppSettings["system_email_sender"]);
                BodyText = @"<html> <body>" + body + "</body> </html>";
                AttachmentFileName = filename;

                string AuthenticatedSMTP = Convert.ToString(ConfigurationManager.AppSettings["AuthenticatedSMTP"]);
                string server = Convert.ToString(ConfigurationManager.AppSettings["MailServerIP"]);

                if (string.IsNullOrEmpty(FromAdr) || string.IsNullOrEmpty(server))
                {
                    return false;
                }

                MailMessage MailObj = new MailMessage();
                MailObj.To.Add(ToAdr);
                MailObj.From = new MailAddress(FromAdr);
                MailObj.IsBodyHtml = true;

                MailObj.Subject = Subject;
                MailObj.Body = BodyText;

                if (!string.IsNullOrEmpty(AttachmentFileName))
                {
                    Attachment attachment = new Attachment(AttachmentFileName);
                    //System.Net.Mime.ContentType contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                    //attachment.ContentType = contentType;
                    attachment.ContentDisposition.FileName = filenameForEmail;
                    MailObj.Attachments.Add(attachment);
                }

                if (AuthenticatedSMTP == "No")
                {
                    SmtpClient mSmtpClient = new SmtpClient(server);
                    mSmtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                    mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    try
                    {
                        mSmtpClient.Send(MailObj);
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        result = false;
                        throw ex;
                    }
                }
                else
                {
                    //EmailService.BasicSendMail(ToDisplayName, ToAdr, FromDisplayName, FromAdr, CcDisplayName, CcAdr, BccAdr, Subject, BodyText.ToString(), AttachmentFileName);                    

                    string system_email_sender_password = Convert.ToString(ConfigurationManager.AppSettings["system_email_sender_password"]);

                    if (string.IsNullOrEmpty(system_email_sender_password))
                    {
                        return false;
                    }

                    MailObj.Priority = MailPriority.Normal;
                    SmtpClient smtpcli = new SmtpClient(server, 587);
                    smtpcli.EnableSsl = true;
                    smtpcli.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpcli.Credentials = new NetworkCredential(FromAdr, system_email_sender_password);
                    try
                    {
                        smtpcli.Send(MailObj);
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        result = false;
                        throw ex;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
                // return ex.Message;
            }
        }

        public static string SendUserId(string emailid)
        {
            string ToDisplayName = string.Empty;
            string ToAdr = string.Empty;
            string FromDisplayName = string.Empty;
            string FromAdr = string.Empty;
            string CcDisplayName = string.Empty;
            string CcAdr = string.Empty;
            string BccAdr = string.Empty;
            string Subject = string.Empty;
            string BodyText = string.Empty;
            string AttachmentFileName = string.Empty;
            string updated_record_id = string.Empty;
            USERS obj_USERS = new USERS();

            if (ConfigurationManager.AppSettings["isEmail"].ToString() == "0")
            {
                return string.Empty;
            }
            try
            {
                obj_USERS = ClsSecurityManage.GetUserId(emailid);
                if (string.IsNullOrEmpty(obj_USERS.User_ID) || string.IsNullOrWhiteSpace(obj_USERS.User_ID))
                {
                    return obj_USERS.User_ID;
                }
                else
                {
                    ToDisplayName = emailid;
                    ToAdr = emailid;
                    FromDisplayName = string.Empty;
                    FromAdr = ConfigurationManager.AppSettings["system_email_sender"].ToString();
                    CcDisplayName = string.Empty;
                    CcAdr = string.Empty;
                    BccAdr = string.Empty;
                    string company_name = ConfigurationManager.AppSettings["Company_Name"].ToString(); //BUSessionUtility.BUSessionContainer.CompanyName;
                    string WebSiteUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

                    Subject = "Request a Username for " + company_name + " account";
                    StringBuilder BodyTextN = new StringBuilder();

                    BodyText = @"<html> <body> A request to send your Username for your " + company_name + @" account has been received. <br/>
                                 Your Username is " + obj_USERS.UserName + "." + "<br/>Click <a href=" + "\"" + WebSiteUrl + "\"" + " >here</a> to login. </body> </html>";

                    AttachmentFileName = string.Empty;
                    string spoofingEmailAdress = ConfigurationManager.AppSettings["NotificationEmailSpoofing"].ToString();

                    EmailService.BasicSendMail(ToDisplayName, ToAdr, FromDisplayName, FromAdr, CcDisplayName, CcAdr, BccAdr, Subject, BodyText.ToString(), AttachmentFileName, spoofingEmailAdress);
                    return obj_USERS.User_ID;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
                throw ex;
            }
        }
        public static bool SendSimpleMail(string email, string displayName, string subject, string body)
        {
            bool result = false;
            try
            {
                string FromAdr = ConfigurationManager.AppSettings["system_email_sender"].ToString();
                string spoofingEmailAdress = ConfigurationManager.AppSettings["NotificationEmailSpoofing"].ToString();
                EmailService.BasicSendMail(displayName, email, string.Empty, FromAdr, string.Empty, string.Empty, string.Empty, subject, body.ToString(), string.Empty, spoofingEmailAdress);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        public static bool UpdateUserPhoneNumber(int UserID, string PhoneNumber)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            string SP_NAME = "USP_UPDATE_USER_PHONE";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            bool result = false;
            try
            {
                objList.Add(new DSSQLParam("UserID", UserID, false));
                objList.Add(new DSSQLParam("PhoneNumber", PhoneNumber, false));
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SP_NAME, CommandType.StoredProcedure, objList);

                if (recAdded > 0)
                {
                    objDbCommand.Transaction.Commit();
                    result = true;
                }
                else
                {
                    objDbCommand.Transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objDbCommand.Connection.Close();
            }
            return result;
        }
        public static bool AddUserTrustedBrowser(int UserID, string browser_fingerprint)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            string SP_NAME = "USP_ADD_TRUSTED_BROWSER";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            bool result = false;
            try
            {
                objList.Add(new DSSQLParam("UserID", UserID, false));
                objList.Add(new DSSQLParam("browser_fingerprint", browser_fingerprint, false));
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SP_NAME, CommandType.StoredProcedure, objList);

                if (recAdded > 0)
                {
                    objDbCommand.Transaction.Commit();
                    result = true;
                }
                else
                {
                    objDbCommand.Transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objDbCommand.Connection.Close();
            }
            return result;
        }

        public static bool CheckIfTrustedBrowser(int UserID, string browser_fingerprint)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            string SP_NAME = "USP_CHECK_TRUSTED_BROWSER";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            bool result = false;
            try
            {
                objList.Add(new DSSQLParam("UserID", UserID, false));
                objList.Add(new DSSQLParam("browser_fingerprint", browser_fingerprint, false));
                DataTable table = objCDataAccess.ExecuteDataTable(objDbCommand, SP_NAME, CommandType.StoredProcedure, objList);
                objDbCommand.Transaction.Commit();
                if (table != null && table.Rows.Count > 0)
                {
                    int count = Convert.ToInt32(table.Rows[0]["id"].ToString());
                    if (count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }
            return result;
        }
    }
}
