using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BIZ;
using Portal.Provider;
using Portal.Utility;

namespace WebPortal.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearMessage();
                ClsCommon.setFocusCurrentTabControl(Master, "liAccountSettings");
            }

        }
        private void ClearMessage()
        {
            success_alert.Visible = false;
            error_alert.Visible = false;
            ErrorMessage.Text = string.Empty;
            SuccessMessage.Text = string.Empty;
        }
        protected void DeleteUserButton_Click(object sender, EventArgs e)
        {
            ErrorMessage.Text = string.Empty;
            SuccessMessage.Text = string.Empty;
            CurrentPassword.Text = string.Empty;
            NewPassword.Text = string.Empty;
            ConfirmNewPassword.Text = string.Empty;
            Response.Redirect("~/Account/UpdateAccountSettings.aspx");
        }
        protected void UpdateUserButton_Click(object sender, EventArgs e)
        {
            string user_id = BUSessionUtility.BUSessionContainer.USER_ID;
            ErrorMessage.Text = string.Empty;
            SuccessMessage.Text = string.Empty;
            try
            {
                if (Page.IsValid)
                {
                    if (string.Compare(CurrentPassword.Text, NewPassword.Text) == 0)
                    {
                        ClearMessage();
                        error_alert.Visible = true;
                        ErrorMessage.Text = "New Password cannot be same as Old Password";
                        CurrentPassword.Text = string.Empty;
                        NewPassword.Text = string.Empty;
                        ConfirmNewPassword.Text = string.Empty;

                        return;

                    }
                    if (!string.IsNullOrEmpty(user_id))
                    {
                        ClsSecurityManage objSecurityManage = new ClsSecurityManage();
                        if (objSecurityManage.CustomChangePassword(user_id, CurrentPassword.Text, NewPassword.Text))
                        {
                            ClearMessage();
                            success_alert.Visible = true;
                            SuccessMessage.Text = "Password Changed Successfully";
                            BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG = "0";
                            CurrentPassword.Text = string.Empty;
                            NewPassword.Text = string.Empty;
                            ConfirmNewPassword.Text = string.Empty;
                        }
                        else
                        {

                            ClearMessage();
                            error_alert.Visible = true;
                            ErrorMessage.Text = "Password Changed Failed";
                            CurrentPassword.Text = string.Empty;
                            NewPassword.Text = string.Empty;
                            ConfirmNewPassword.Text = string.Empty;
                            SuccessMessage.Visible = false;
                            ErrorMessage.Visible = true;
                        }
                    }
                    else
                    {
                        ClsSecurity.Logout();
                        Session.Abandon();
                        FormsAuthentication.SignOut();
                        FormsAuthentication.RedirectToLoginPage();
                    }

                }

              

            }

            catch (Exception ex)
            {
                CurrentPassword.Text = string.Empty;
                NewPassword.Text = string.Empty;
                ConfirmNewPassword.Text = string.Empty;
                ClearMessage();
                var Type = "Error";
                var system = string.Empty;
                var program = "Change Password";
                var Severity = "Medium";
                var Message = ex.Message;
                var User_id = BUSessionUtility.BUSessionContainer.USER_ID;
                ClsSecurityManage.CreateErrorLog(Type, system, program, Severity, Message, User_id);
                error_alert.Visible = true;
                ErrorMessage.Text = ex.Message;
                throw ex;
            }
        }
        protected void btncontinue_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Account/UpdateAccountSettings.aspx");
        }
    }
}