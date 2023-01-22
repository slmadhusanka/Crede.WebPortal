using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BIZ;
using Portal.Model;
using Portal.Provider;
using Portal.Utility;

namespace WebPortal.SystemPages
{
    public partial class UpdateFacilityType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG == "1")
            {
                Response.Redirect("~/Account/ChangePassword.aspx");
            }

            success_alert.Visible = false;
            error_alert.Visible = false;
            try
            {
                if (!IsPostBack)
                {
                    CheckPermission();

                    #region Set data for editing (Author: Grishma)

                    if (Session["FacilityTypeData"] != null)
                    {
                        ClsCommon.setFocusCurrentTabControl(Master, "liTableMaintainance");

                        FacilityType objFacilityType = (FacilityType)(Session["FacilityTypeData"]);
                        lblFacilityCode.Text = objFacilityType.FacilityTypeCode;
                        txtFacilityDesc.Text = objFacilityType.Description;
                        txtDescriptionShort.Text = objFacilityType.DescriptionShort;
                        txtDescriptionLong.Text = objFacilityType.DescriptionLong;
                        chkIsActive.Checked = Convert.ToBoolean(objFacilityType.IsActive);
                    }
                    else
                    {
                        Response.Redirect("~/SystemPages/ListOfClinicType.aspx");
                    }
                    #endregion

                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "FacilityTypeEdit"))
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

                    if (!string.IsNullOrEmpty(lblFacilityCode.Text) && !string.IsNullOrEmpty(txtFacilityDesc.Text.Trim()))
                    {
                        ClsSystem.UpdateFacilityType(lblFacilityCode.Text, txtFacilityDesc.Text.Trim(), chkIsActive.Checked.ToString(), txtDescriptionShort.Text.Trim(), txtDescriptionLong.Text.Trim());
                        //ClsCommon.setRecordUpdateStatusInDb("FacilityType", "FacilityTypCode", "Edited", lblFacilityCode.Text.Trim());
                        success_alert.Visible = true;
                        SuccessMessage.Text = "Clinic Type Updated Successfully";
                        Session["FacilityTypeData"] = null;
                        Response.Redirect("~/SystemPages/ListOfClinicType.aspx");
                        lblFacilityCode.Text = string.Empty;
                        txtFacilityDesc.Text = string.Empty;
                    }
                    else
                    {
                        error_alert.Visible = true;
                        ErrorMessage.Text = "The Facility type code is not selected for update";
                    }

                }

            
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                //ErrorMessage.Text = ex.Message;
                string Type = "Error";
                string system = string.Empty;
                string program = "Editing Faciltiy";
                string Severity = "Medium";
                string Message = ex.Message;
                string User_id = BUSessionUtility.BUSessionContainer.USER_ID;
                ClsSecurityManage.CreateErrorLog(Type, system, program, Severity, Message, User_id);
                error_alert.Visible = true;
                ErrorMessage.Text = Message;
                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["FacilityTypeData"] = null;
            Response.Redirect("~/SystemPages/ListOfClinicType.aspx");
        }
    }
}