using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Portal.BIZ;
using Portal.Provider;
using Portal.Utility;

namespace WebPortal
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable tbl_sysSettings = ClsSystem.GetSystemSetting().Tables[0];
            string companyname = tbl_sysSettings.AsEnumerable().FirstOrDefault(r => DataRowExtensions.Field<string>(r, "ConfigurationName") == "Company Name")?["Value"].ToString();

            Page.Title = companyname + " Clinic Portal";

            error_display.Visible = false;
            if (!IsPostBack)
            {
                UserName.Focus();
            }
        }

        [WebMethod]
        public static string[] DetectBrowser()
        {
            System.Web.HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;

            string[] browserDetail = new string[3];
            browserDetail[0] = browser.Browser.ToLower();
            browserDetail[1] = browser.Version;
            browserDetail[2] = browser.Type;

            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Alert", "alert('" + HttpUtility.JavaScriptStringEncode(s) + "');", true);        
            return browserDetail;
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {

            int counter = 0;
            int counterP = 0;
            string v_err = string.Empty;
            string userid = string.Empty;

            DataSet ds = new DataSet();
            ds = ClsSystem.GetEmails();

            try
            {
                if (Membership.ValidateUser(UserName.Text, Password.Text))
                {
                    //userid = ClsSecurityManage.GetUserIdByUserName(UserName.Text);
                    v_err = ClsSecurity.DoLogin(UserName.Text, hdnFingerPrint.Value);
                    if (string.IsNullOrEmpty(v_err))
                    {
                        if (BUSessionUtility.BUSessionContainer.IsTrustedBrowser)
                        {
                            CheckPasswordIsExpired();
                        }
                        else
                        {
                            if (BUSessionUtility.BUSessionContainer.TwoWayAuthActivied && !string.IsNullOrEmpty(BUSessionUtility.BUSessionContainer.PhoneNumber))
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "two_way_oauth", "two_way_oauth('" + BUSessionUtility.BUSessionContainer.USER_ID + "', '" + BUSessionUtility.BUSessionContainer.Email + "', '" + BUSessionUtility.BUSessionContainer.PhoneNumber + "');", true);
                            }
                            else if (BUSessionUtility.BUSessionContainer.TwoWayAuthActivied && string.IsNullOrEmpty(BUSessionUtility.BUSessionContainer.PhoneNumber))
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "fetch_user_phone_number", "fetch_user_phone_number('" + BUSessionUtility.BUSessionContainer.USER_ID + "', '" + BUSessionUtility.BUSessionContainer.Email + "');", true);
                            }
                            else if (!BUSessionUtility.BUSessionContainer.TwoWayAuthActivied)
                            {
                                CheckPasswordIsExpired();
                            }
                        }
                    }
                }
                else
                {
                    if (PortalMembershipProvider.pfailureType == "password")
                    {
                        if (ViewState["CounterP"] == null)
                        {
                            counterP = 1;
                            ViewState["CounterP"] = counter;
                            lbl_invalid_password.Visible = true;
                            lbl_invalid_password.Attributes.Add("style", "display:inline-block;");
                            lbl_invalid_password.Text = PortalMembershipProvider.vCustomErrorMsg;
                        }
                        else
                        {
                            if ((int)ViewState["CounterP"] >= 2)
                            {
                                userid = ClsSecurityManage.GetUserIdByUserName(UserName.Text);
                                ViewState["CounterP"] = null;
                                lbl_invalid_password.Visible = true;
                                lbl_invalid_password.Attributes.Add("style", "display:inline-block;");

                                string company = ds.Tables[0].Rows[1]["Value"].ToString();
                                string result;
                                result = ClsSecurity.SendPasswordResetUrl(userid, company, true);

                                if (result == "sucess")
                                {
                                    lbl_invalid_password.Text = "A Reset Password link has been sent to your email address on record.";
                                }
                                else
                                {
                                    lbl_invalid_password.Text = "Email sending failed.";
                                }
                            }
                            else
                            {
                                counterP = (int)ViewState["CounterP"] + 1;
                                ViewState["CounterP"] = counterP;

                                lbl_invalid_password.Visible = true;
                                lbl_invalid_password.Attributes.Add("style", "display:inline-block;");
                                lbl_invalid_password.Text = PortalMembershipProvider.vCustomErrorMsg;
                            }
                        }
                         
                    }
                    else if (PortalMembershipProvider.pfailureType == "UserID_error")
                    {
                        if (ViewState["Counter"] == null)
                        {
                            counter = 1;
                            ViewState["Counter"] = counter;

                            lbl_invalid_username.Visible = true;
                            lbl_invalid_username.Attributes.Add("style", "display:inline-block;");
                            lbl_invalid_username.Text = PortalMembershipProvider.vCustomErrorMsg;
                        }
                        else
                        {
                            if ((int)ViewState["Counter"] >= 2)
                            {
                                ViewState["Counter"] = null;
                                lbl_invalid_username.Visible = true;
                                lbl_invalid_username.Attributes.Add("style", "display:inline-block;");
                                lbl_invalid_username.Text = "Please verify your Username with your system Administrator";
                            }
                            else
                            {
                                counter = (int)ViewState["Counter"] + 1;
                                ViewState["Counter"] = counter;

                                lbl_invalid_username.Visible = true;
                                lbl_invalid_username.Attributes.Add("style", "display:inline-block;");
                                lbl_invalid_username.Text = PortalMembershipProvider.vCustomErrorMsg;
                            }
                        }
                    }
                    else if (PortalMembershipProvider.pfailureType == "lockedout" || PortalMembershipProvider.pfailureType == "inactive")
                    {
                        lbl_invalid_username.Visible = true;
                        lbl_invalid_username.Attributes.Add("style", "display:inline-block;");
                        lbl_invalid_username.Text = PortalMembershipProvider.vCustomErrorMsg;
                    }
                }



            }
            catch (Exception exx)
            {
                string Type = "Error";
                string system = string.Empty;
                string program = "Login Screen";
                string Severity = "Medium";
                string Message = exx.Message;
                string User_id = BUSessionUtility.BUSessionContainer.USER_ID;
                //FailureText.Text = "Please verify your User ID with your system Administrator";
                ClsSecurityManage.CreateErrorLog(Type, system, program, Severity, Message, User_id);
            }
        }
        protected void CheckPasswordIsExpired()
        {
            int date_to_expire = ClsSystem.getPasswordExpiredDate(BUSessionUtility.BUSessionContainer.USER_ID);
            if (date_to_expire > 7)
            {
                /*bool isMobile = Utils.Utils.ifBrowserIsMobile();
                if (isMobile)
                {
                    FormsAuthentication.SetAuthCookie(UserName.Text, true);
                    Response.Redirect("~/hld_reprocessing_log.aspx");
                }
                else
                {*/
                    FormsAuthentication.RedirectFromLoginPage("", true);
                
            }
            else
            {
                if (date_to_expire < 0)
                {
                    BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG = "1";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "show_password_expired_message", "show_password_expired_message();", true);
                }
                else
                {
                    string days = string.Empty;
                    if (date_to_expire == 0)
                    {
                        days = "today";
                    }
                    else
                    {
                        days = date_to_expire + " days";
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "show_password_expire_warning", "show_password_expire_warning('" + days + "');", true);
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static bool ChangePasswordFlagUpdate()
        {
            bool result = false;
            try
            {
                BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG = "1";
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        [WebMethod]
        public static string VerifyOTP(int UserID, string otp, bool remember, string browser_fingerprint)
        {
            string result = string.Empty;
            try
            {
                if (HttpContext.Current.Session[UserID + "_otp"] != null && DateTime.Parse(HttpContext.Current.Session[UserID + "_otpExpiry"].ToString()) > DateTime.UtcNow)
                {
                    if (otp == HttpContext.Current.Session[UserID + "_otp"].ToString())
                    {
                        if (remember)
                        {
                            ClsSecurity.AddUserTrustedBrowser(UserID, browser_fingerprint);
                        }
                        HttpContext.Current.Session.Remove(UserID + "_otp");
                        HttpContext.Current.Session.Remove(UserID + "_otpExpiry");
                        result = "Success";
                    }
                    else
                    {
                        result = "Invalid Verification Code";
                    }
                }
                else
                {
                    HttpContext.Current.Session.Remove(UserID + "_otp");
                    HttpContext.Current.Session.Remove(UserID + "_otpExpiry");
                    result = "Verification Code Expired";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        private static string GenerateRandomOTP(int iOTPLength)
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;
            }
            return sOTP;
        }

        public static string _SendSMS(string destinationNumber, string otp)
        {
            // Web Client / REST
            string response = string.Empty;
            try
            {
                dynamic body = new ExpandoObject();
                string product_name = ConfigurationManager.AppSettings["Product"].ToString();
                body.MessageBody = "Your Verification Code for the " + BUSessionUtility.BUSessionContainer.CompanyName + " " + product_name + " is " + otp + ". This Verification Code will expire in 5 minutes.";

                var url = string.Format("https://secure.smsgateway.ca/services/message.svc/{0}/{1}",
                "DdK3Z4404l3dqWU58hOR83q2e3PkigHy", destinationNumber);

                using (var wClient = new System.Net.WebClient())
                {
                    wClient.Encoding = Encoding.UTF8;
                    wClient.Headers.Add("content-type", "application/json");

                    response = wClient.UploadString(url, Newtonsoft.Json.JsonConvert.SerializeObject(body));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        [WebMethod]
        public static string SendSMS(int UserID, string mobile, string send_method, string email)
        {
            string result = string.Empty;
            try
            {
                //send sms
                string otp = GenerateRandomOTP(8);
                if (send_method == "tel")
                {
                    string response = _SendSMS(mobile, otp);
                    JObject json = JObject.Parse(response);
                    result = json["SendMessageWithReferenceResult"].ToString();
                    if (json["SendMessageWithReferenceResult"].ToString() == "Message queued successfully")
                    {
                        ClsSecurity.UpdateUserPhoneNumber(UserID, mobile);
                        BUSessionUtility.BUSessionContainer.PhoneNumber = mobile;
                        HttpContext.Current.Session[UserID + "_otp"] = otp;
                        HttpContext.Current.Session[UserID + "_otpExpiry"] = DateTime.UtcNow.AddMinutes(5);
                    }
                }
                else
                {
                    string file_path = System.Web.HttpContext.Current.Server.MapPath("Email_Format_OTP.txt");
                    string file_content = File.ReadAllText(file_path);
                    string body = string.Format(file_content, otp, BUSessionUtility.BUSessionContainer.CompanyName);
                    string Product = ConfigurationManager.AppSettings["Product"].ToString();
                    if (ClsSecurity.SendSimpleMail(email, BUSessionUtility.BUSessionContainer.CompanyName + " " + Product, "Verification Code for Login to " + BUSessionUtility.BUSessionContainer.CompanyName + " Clean Hands Portal", body))
                    {
                        result = "Message queued successfully";
                        HttpContext.Current.Session[UserID + "_otp"] = otp;
                        HttpContext.Current.Session[UserID + "_otpExpiry"] = DateTime.UtcNow.AddMinutes(5);
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                result = ex.Message;
            }
            return result;
        }
        [WebMethod]
        public static string SendOTP(int UserID, string method, string method_value)
        {
            string result = string.Empty;
            try
            {
                if (method == "sms")
                {
                    //send sms
                    string otp = GenerateRandomOTP(8);
                    string response = _SendSMS(method_value, otp);
                    JObject json = JObject.Parse(response);
                    result = json["SendMessageWithReferenceResult"].ToString();
                    if (json["SendMessageWithReferenceResult"].ToString() == "Message queued successfully")
                    {
                        HttpContext.Current.Session[UserID + "_otp"] = otp;
                        HttpContext.Current.Session[UserID + "_otpExpiry"] = DateTime.UtcNow.AddMinutes(5);
                    }
                }
                else
                {
                    //send email
                    string otp = GenerateRandomOTP(8);
                    string file_path = System.Web.HttpContext.Current.Server.MapPath("Email_Format_OTP.txt");
                    string file_content = File.ReadAllText(file_path);
                    string product_name = ConfigurationManager.AppSettings["Product"].ToString();
                    string email_content = BUSessionUtility.BUSessionContainer.CompanyName + " " + product_name;
                    string body = string.Format(file_content, otp, email_content);
                    string subject = "Your Verification Code for the " + email_content;

                    List<string> emails = new List<string>();
                    emails.Add(method_value);

                    if (ClsCommon.SendSimpleMail(emails, subject, body, email_content, null, null))
                    {
                        result = "Message queued successfully";
                        HttpContext.Current.Session[UserID + "_otp"] = otp;
                        HttpContext.Current.Session[UserID + "_otpExpiry"] = DateTime.UtcNow.AddMinutes(5);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        protected void BtnHiddnLoginClick(object sender, EventArgs e)
        {
            /*bool isMobile = Utils.Utils.ifBrowserIsMobile();
            if (isMobile)
            {
                FormsAuthentication.SetAuthCookie(UserName.Text, true);
                Response.Redirect("~/hld_reprocessing_log.aspx");
            }
            else
            {*/
                FormsAuthentication.RedirectFromLoginPage("", true);
            /*}*/
        }

        protected void BtnHiddnCheckExpired(object sender, EventArgs e)
        {
            CheckPasswordIsExpired();
        }
    }
}