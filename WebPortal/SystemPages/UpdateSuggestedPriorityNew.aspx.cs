using Portal.BIZ;
using Portal.Model;
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
    public partial class UpdateSuggestedPriorityNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG == "1")
            {
                Response.Redirect("~/Account/ChangePassword.aspx");
            }
            success_alert.Visible = false;
            ErrorMessage.Visible = false;
            try
            {
                if (!IsPostBack)
                {
                    CheckPermission();

                    #region Set data for editing (Author: Grishma)
                    if (Session["RegionData"] != null)
                    {
                        ClsCommon.setFocusCurrentTabControl(Master, "liTableMaintainance");

                        SuggestedPriority objError = (SuggestedPriority)(Session["RegionData"]);
                        lblRegionCode.Text = Convert.ToString(objError.ID);
                        txtRegionDesc.Text = objError.Description;
                        chkIsActive.Checked = Convert.ToBoolean(objError.IsActive);

                    }
                    else
                    {
                        Response.Redirect("~/SystemPages/ListOfSuggestedPriorityNew.aspx");
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
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "ZoneEdit"))
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

                if (!string.IsNullOrEmpty(lblRegionCode.Text) && !string.IsNullOrEmpty(txtRegionDesc.Text.Trim()))
                {
                    ClsSystem.UpdateSuggestedPriorityType(lblRegionCode.Text, txtRegionDesc.Text.Trim(), chkIsActive.Checked.ToString());
                    //ClsCommon.setRecordUpdateStatusInDb("Region", "RegionCode", "Edited", lblRegionCode.Text);
                    SuccessMessage.Text = "Region Updated Successfully";
                    Session["RegionData"] = null;
                    Response.Redirect("~/SystemPages/ListOfSuggestedPriorityNew.aspx");
                    txtRegionDesc.Text = string.Empty;
                    lblRegionCode.Text = string.Empty;
                }
                else
                {
                    ErrorMessage.Text = "The Region Code is not Selected for Update";
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
                string program = "Editing Region";
                string Severity = "Medium";
                string Message = ex.Message;
                string User_id = BUSessionUtility.BUSessionContainer.USER_ID;
                ClsSecurityManage.CreateErrorLog(Type, system, program, Severity, Message, User_id);
                ErrorMessage.Text = ConfigurationManager.AppSettings["ErrorMsg"].ToString();
                throw ex;
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Session["RegionData"] = null;
            Response.Redirect("~/SystemPages/ListOfSuggestedPriorityNew.aspx");
        }
    }
}