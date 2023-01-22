using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using Portal.BIZ;
using Portal.Provider;
using Portal.Provider.Model;
using Portal.Utility;

namespace WebPortal
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pwd_change_success.Visible = false;
            pwd_change_error.Visible = false;

            pwd_send_success.Visible = false;
            pwd_send_error.Visible = false;
            if (!IsPostBack)
            {
                lnkButtonLogin.Visible = false;
                if (Request.QueryString["Data"] != null)
                {
                    string data = Request.QueryString["Data"].ToString();
                    string UserName = DeCryptedQueryString.Decrypted_REQUEST_FromURL(data, "USER_ID");
                    if (!string.IsNullOrEmpty(UserName))
                    {
                        divChangePassword.Visible = true;
                        divSendPassword.Visible = false;
                    }
                }
                else
                {
                    divSendPassword.Visible = true;
                    divChangePassword.Visible = false;
                }

            }
        }

        protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
        {
            string data = string.Empty;
            string UserName = string.Empty;
            if (Request.QueryString["Data"] != null)
            {
                data = Request.QueryString["Data"].ToString();
                UserName = DeCryptedQueryString.Decrypted_REQUEST_FromURL(data, "USER_ID");
                //int userid = ClsSecurityManage.GetUserIdByUserName(UserName.Text);

            }
            Session["PWD"] = null;
            FailureText.Text = string.Empty;
            SuccessMessage.Text = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(UserName))
                {
                    ClsSecurityManage objSecurityManage = new ClsSecurityManage();
                    if (objSecurityManage.AssignNewPassword(UserName, NewPassword.Text))
                    {
                        pwd_change_success.Visible = true;
                        SuccessMessage.Text = "Password Changed Successfully";
                        lnkButtonLogin.Visible = true;
                        Session["PWD"] = NewPassword.Text;

                    }
                    else
                    {
                        Session["PWD"] = null;
                        lnkButtonLogin.Visible = false;
                        pwd_change_error.Visible = true;
                        FailureText.Text = "Password Changed Failed";
                    }
                }
                else
                {
                    Session["PWD"] = null;
                    lnkButtonLogin.Visible = false;
                    ClsSecurity.Logout();
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
            catch (Exception ex)
            {
                Session["PWD"] = null;
                lnkButtonLogin.Visible = false;
                string Type = "Error";
                string system = string.Empty;
                string program = "Forget Password";
                string Severity = "Medium";
                string Message = ex.Message;
                string User_id = BUSessionUtility.BUSessionContainer.USER_ID;
                ClsSecurityManage.CreateErrorLog(Type, system, program, Severity, Message, User_id);
                pwd_change_error.Visible = true;
                FailureText.Text = Message;
                throw ex;
            }
        }

        protected void lnkButtonLogin_Click(object sender, EventArgs e)
        {
            string v_err = string.Empty;
            string data = string.Empty;
            string user_id = string.Empty;
            if (Request.QueryString["Data"] != null)
            {
                data = Request.QueryString["Data"].ToString();
                user_id = DeCryptedQueryString.Decrypted_REQUEST_FromURL(data, "USER_ID");

            }
            try
            {
                if (Session["PWD"] != null && !string.IsNullOrEmpty(Session["PWD"].ToString()))
                {
                    if (Membership.ValidateUser(user_id, Session["PWD"].ToString()))
                    {
                        v_err = ClsSecurity.DoLogin(user_id, hdnFingerPrint.Value);
                        if (string.IsNullOrEmpty(v_err))
                        {
                            FormsAuthentication.RedirectFromLoginPage(Server.HtmlEncode(user_id.ToString()), true);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ClsSecurity.Logout();
                Session["PWD"] = null;
                Session.Abandon();
                lnkButtonLogin.Visible = false;
                string Type = "Error";
                string system = string.Empty;
                string program = "Forget Password";
                string Severity = "Medium";
                string Message = ex.Message;
                ClsSecurityManage.CreateErrorLog(Type, system, program, Severity, Message, user_id);
                pwd_change_error.Visible = true;
                FailureText.Text = Message;
                throw ex;

            }
        }

        protected void LnkResetPassword_Click(object sender, EventArgs e)
        {
            string user_id = string.Empty;
            try
            {
                string IsLockedOut = string.Empty;
                List<USERS> LIST_OBJ_USER = new List<USERS>();
                LIST_OBJ_USER = ClsSecurityManage.GetSpecificUserLockedState(txtUserid.Text);
                user_id = LIST_OBJ_USER[0].UserName;
                IsLockedOut = LIST_OBJ_USER[0].IsLockedOut;

                if (IsLockedOut.Equals("Yes"))
                {
                    pwd_send_error.Visible = true;
                    error_text.Text = "Your account has been locked. Please contact your system administrator or coordinator to unlock it.";

                }
                else
                {

                    user_id = ClsSecurity.SendPasswordResetUrl(txtUserid.Text);
                    //tdInfo.InnerText = user_id;
                    if (user_id == txtUserid.Text && !string.IsNullOrEmpty(user_id))
                    {
                        pwd_send_success.Visible = true;
                        success_text.Text = "A link has been sent to your email address so you can re-set your password.";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "CloseFancyBox", "CloseFancyBox();", true);
                    }
                    else
                    {
                        pwd_send_error.Visible = true;
                        error_text.Text = "Incorrect Username, Please enter the correct Username";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ResizeFancyBox", "ResizeFancyBox();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                pwd_send_error.Visible = true;
                error_text.Text = "Incorrect Username, Please enter the correct Username";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ResizeFancyBox", "ResizeFancyBox();", true);
            }
        }
    }
}