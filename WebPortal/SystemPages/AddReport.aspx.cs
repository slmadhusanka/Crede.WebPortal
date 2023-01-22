using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BIZ;
using Portal.Provider;
using Portal.Provider.Model;
using Portal.Utility;

namespace WebPortal.SystemPages
{
    public partial class AddReport : System.Web.UI.Page
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
                ClsCommon.setFocusCurrentTabControl(Master, "liMobileDeviceMantenance");
            }
        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "ReportListAdd"))
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(Page.IsValid)
                {

                    SuccessMessage.Text = string.Empty;
                    ErrorMessage.Text = string.Empty;
                    Report objReport = new Report();
                    objReport.ReportCode = txtReportCode.Text.Trim();
                    objReport.ReportName = txtReportName.Text.Trim();
                    objReport.ReportDescription = (!string.IsNullOrEmpty(txtReportDescription.Text.Trim())) ? txtReportDescription.Text.Trim() : null;
                    objReport.ReportServerLocation = txtReportServerLocation.Text.Trim();
                    objReport.ReportCategory = txtReportCategory.Text;
                    objReport.ReportSubCategory = (!string.IsNullOrEmpty(txtReportSubCategory.Text.Trim())) ? txtReportSubCategory.Text.Trim() : null;
                    objReport.IsActive = chkIsActive.Checked;
                    if (!string.IsNullOrEmpty(txtSortOrder.Text.Trim()))
                        objReport.SortOrder = Convert.ToInt32(txtSortOrder.Text.Trim());
                    objReport.IsSendEnable = chkIsSebdEnable.Checked;
                    objReport.ReportKey = txtReportKey.Text;

                    if (ClsSystem.AddUpdateReport(objReport, "Add"))
                    {
                        //ClsCommon.setRecordUpdateStatusInDb("ReportsAndRoles", "ReportCode", "Added", "");
                        Response.Redirect("~/SystemPages/ListOfReport.aspx");

                    }
                    else
                    {
                        error_alert.Visible = true;
                        ErrorMessage.Text = "Error in add report";
                    }
                }

             
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                string Type = "Error";
                string system = string.Empty;
                string program = "Adding Report";
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
            Response.Redirect("~/SystemPages/ListOfReport.aspx");
        }
    }
}