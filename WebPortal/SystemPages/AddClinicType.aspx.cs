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
    public partial class AddFacilityType : System.Web.UI.Page
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
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "FacilityTypeAdd"))
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

                    if (!string.IsNullOrEmpty(txtFacilityDesc.Text.Trim()))
                    {
                        ClsSystem.CreateFacilityType(txtFacilityDesc.Text.Trim(), chkIsActive.Checked.ToString(), txtDescriptionShort.Text.Trim(), txtDescriptionLong.Text.Trim());
                        //ClsCommon.setRecordUpdateStatusInDb("FacilityType", "FacilityTypeCode", "Added", "");
                        success_alert.Visible = true;
                        SuccessMessage.Text = "Clinic Type Saved Successfully";
                        txtFacilityDesc.Text = string.Empty;
                        Response.Redirect("~/SystemPages/ListOfClinicType.aspx");
                    }

                }

            
            }
            catch (Exception ex)
            {
                string Type = "Error";
                string system = string.Empty;
                string program = "Adding Facility Type";
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