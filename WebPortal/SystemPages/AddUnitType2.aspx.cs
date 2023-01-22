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
    public partial class AddUnitType2 : System.Web.UI.Page
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
                ClsCommon.setFocusCurrentTabControl(Master, "liTableMaintainance");
            }

        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "UnitType2Add"))
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

        protected void UpdateUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    SuccessMessage.Text = string.Empty;
                    ErrorMessage.Text = string.Empty;

                    if (!string.IsNullOrEmpty(txtUnitTyp2Desc.Text))
                    {
                        ClsSystem.CreateUnitType2(txtUnitTyp2Desc.Text.Trim().Replace("'", "''"), chkIsActive.Checked.ToString(), txtDescriptionShort.Text.Trim().Replace("'", "''"));
                        //ClsCommon.setRecordUpdateStatusInDb("UnitType2", "UnitType2Code", "Added", "");
                        success_alert.Visible = true;
                        SuccessMessage.Text = "Unit Type 2 Saved Successfully";
                        txtUnitTyp2Desc.Text = string.Empty;
                        Response.Redirect("~/SystemPages/ListOfUnitType2.aspx");
                    }

                }

               
            }
            catch (Exception ex)
            {
                string Type = "Error";
                string system = string.Empty;
                string program = "Adding Unit Type 2";
                string Severity = "Medium";
                string Message = ex.Message;
                string User_id = BUSessionUtility.BUSessionContainer.USER_ID;
                ClsSecurityManage.CreateErrorLog(Type, system, program, Severity, Message, User_id);
                error_alert.Visible = true;
                ErrorMessage.Text = Message;
                throw ex;
            }
        }
    }
}