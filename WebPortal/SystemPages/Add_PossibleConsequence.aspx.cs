using Portal.BIZ;
using Portal.Provider;
using Portal.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal.SystemPages
{
    public partial class Add_PossibleConsequence : System.Web.UI.Page
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
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "ZoneAdd"))
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


        protected void UpdateRegionButton_Click(object sender, EventArgs e)
        {
            try
            {
                SuccessMessage.Text = string.Empty;
                ErrorMessage.Text = string.Empty;

                if (Page.IsValid)
                {
                    if (!string.IsNullOrEmpty(txtRegionDesc.Text.Trim()))
                    {
                        ClsSystem.CreatePossibleConsequence(txtRegionDesc.Text.Trim(), chkIsActive.Checked.ToString());
                        //ClsCommon.setRecordUpdateStatusInDb("Region", "RegionCode", "Added", "");
                        Response.Redirect("~/SystemPages/ListOfPossibleConsequence.aspx");
                        SuccessMessage.Text = "Possible Consequence Saved Successfully";
                        txtRegionDesc.Text = string.Empty;
                    }

                }


            }
            catch (Exception ex)
            {
                string Type = "Error";
                string system = string.Empty;
                string program = "Adding Lab";
                string Severity = "Medium";
                string Message = ex.Message;
                string User_id = BUSessionUtility.BUSessionContainer.USER_ID;
                ClsSecurityManage.CreateErrorLog(Type, system, program, Severity, Message, User_id);
                ErrorMessage.Text = ConfigurationManager.AppSettings["ErrorMsg"].ToString();
                throw ex;
            }
        }
    }
}