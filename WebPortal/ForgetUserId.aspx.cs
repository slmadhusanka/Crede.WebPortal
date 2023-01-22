using System;
using System.Collections.Generic;
using System.Web.UI;
using Portal.BIZ;
using Portal.Provider;
using Portal.Provider.Model;

namespace WebPortal
{
    public partial class ForgetUserId : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            email_success.Visible = false;
            email_error.Visible = false;
        }

        protected void LnkResetPassword_Click(object sender, EventArgs e)
        {
            string UserId = string.Empty;
            try
            {
                string email = string.Empty;
                string IsLockedOut = string.Empty;
                List<USERS> LIST_OBJ_USER = new List<USERS>();
                LIST_OBJ_USER = ClsSecurityManage.GetSpecificUserEmail(txtEmailId.Text);
                email = LIST_OBJ_USER[0].Email;
                IsLockedOut = LIST_OBJ_USER[0].IsLockedOut;

                if (IsLockedOut.Equals("Yes"))
                {
                    email_error.Visible = true;
                    error_text.Text = "Your account has been locked. Please contact your system administrator or coordinator to unlock it.";
                    
                }
                else
                {
                    UserId = ClsSecurity.SendUserId(txtEmailId.Text.Trim());

                    if (!string.IsNullOrEmpty(UserId))
                    {
                        email_success.Visible = true;
                        success_text.Text = "your username has been sent to your email address.";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "CloseFancyBox", "CloseFancyBox();", true);
                    }
                    else
                    {
                        email_error.Visible = true;
                        error_text.Text = "Incorrect email address, please enter the correct email address.";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ResizeFancyBox", "ResizeFancyBox();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                email_error.Visible = true;
                error_text.Text = "Incorrect email address, please enter the correct email address.";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ResizeFancyBox", "ResizeFancyBox();", true);
            }
        }
    }
}