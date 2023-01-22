using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BIZ;
using Portal.Provider;
using Portal.Utility;

namespace WebPortal.SystemPages
{
    public partial class AddProgram : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG == "1")
            {
                Response.Redirect("~/Account/ChangePassword.aspx");
            }
            success_alert.Visible = false;
            error_alert.Visible = false;
          
            if (!IsPostBack)
            {
                CheckPermission();
                if (Session["ismobiletab"] != null)
                {
                    if (Session["ismobiletab"].ToString().Equals("true"))
                    {
                        ClsCommon.setFocusCurrentTabControl(Master, "liMobileDeviceMantenance");
                    }
                    else
                    {
                        ClsCommon.setFocusCurrentTabControl(Master, "liTableMaintainance");
                    }
                }
                else
                {
                    ClsCommon.setFocusCurrentTabControl(Master, "liTableMaintainance");
                }
                BindCombo();
            }
        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "ProgramAdd"))
                    Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void BindCombo()
        {
            ClsCommon.BindComboWithSQL(ddlProgrammeType, @"SELECT ProgramTypeCode,Description FROM  ProgramType WHERE IsActive='True' ORDER BY Description", true, string.Empty, "Description", "ProgramTypeCode");
        }
        protected void UpdateUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    SuccessMessage.Text = string.Empty;
                    ErrorMessage.Text = string.Empty;

                    if (!string.IsNullOrEmpty(txtProgramDetails.Text) && !string.IsNullOrEmpty(ddlProgrammeType.Text))
                    {
                        ClsSystem.CreateProgram(txtProgramDetails.Text.Trim().Replace("'", "''"), ddlProgrammeType.Text, chkIsActive.Checked.ToString(), txtDescriptionShort.Text.Trim().Replace("'", "''"));
                        //ClsCommon.setRecordUpdateStatusInDb("Program", "ProgramCode", "Added","");

                        success_alert.Visible = true;
                        SuccessMessage.Text = "Program Saved Successfully";
                        txtProgramDetails.Text = string.Empty;
                        Response.Redirect("~/SystemPages/ListOfProgram.aspx");
                    }

                }

              
            }
            catch (Exception ex)
            {
                string Type = "Error";
                string system = string.Empty;
                string program = "Add Program";
                string Severity = "Medium";
                string Message = ex.Message;
                string User_id = BUSessionUtility.BUSessionContainer.USER_ID;
                ClsSecurityManage.CreateErrorLog(Type, system, program, Severity, Message, User_id);
                error_alert.Visible = true;
                ErrorMessage.Text = Message;
                throw ex;
            }
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            //txtProgramDetails.Text = string.Empty;
            Response.Redirect("~/SystemPages/ListOfProgram.aspx");

        }
    }
}