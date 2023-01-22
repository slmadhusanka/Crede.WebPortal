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
    public partial class UpdateReport : System.Web.UI.Page
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
                    ClsCommon.setFocusCurrentTabControl(Master, "liMobileDeviceMantenance");
                    if (Session["ReportData"] != null)
                    {
                        Report objReport = (Report)Session["ReportData"];
                        hdnReportID.Value = Convert.ToString(objReport.ReportID);
                        txtReportCode.Text = objReport.ReportCode;
                        txtReportName.Text = objReport.ReportName;
                        txtReportDescription.Text = (objReport.ReportDescription != "&nbsp;") ? objReport.ReportDescription : null;
                        txtReportServerLocation.Text = objReport.ReportServerLocation;
                        txtReportCategory.Text = objReport.ReportCategory;
                        txtReportSubCategory.Text = (objReport.ReportSubCategory != "&nbsp;") ? objReport.ReportSubCategory : null;
                        chkIsActive.Checked = objReport.IsActive;
                        txtSortOrder.Text = (objReport.SortOrder != 0) ? Convert.ToString(objReport.SortOrder) : null;
                        chkIsSendEnable.Checked = objReport.IsSendEnable;
                        txtReportKey.Text = objReport.ReportKey;
                    }
                    else
                    {
                        Response.Redirect("~/SystemPages/ListOfReport.aspx");
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                error_alert.Visible = true;
                ErrorMessage.Text = ex.Message;
                throw ex;
            }

        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "ReportListEdit"))
                    Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                error_alert.Visible = true;
                ErrorMessage.Text = ex.Message;
                throw ex;
            }
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SuccessMessage.Text = string.Empty;
                ErrorMessage.Text = string.Empty;

                if (!string.IsNullOrEmpty(hdnReportID.Value))
                {
                    Report objReport = new Report();
                    objReport.ReportID = Convert.ToInt32(hdnReportID.Value);
                    objReport.ReportCode = txtReportCode.Text.Trim();
                    objReport.ReportName = txtReportName.Text.Trim();
                    objReport.ReportDescription = (!string.IsNullOrEmpty(txtReportDescription.Text.Trim())) ? txtReportDescription.Text.Trim() : null;
                    objReport.ReportServerLocation = txtReportServerLocation.Text.Trim();
                    objReport.ReportCategory = txtReportCategory.Text.Trim();
                    objReport.ReportSubCategory = (!string.IsNullOrEmpty(txtReportSubCategory.Text.Trim())) ? txtReportSubCategory.Text.Trim() : null;
                    objReport.IsActive = chkIsActive.Checked;
                    if (!string.IsNullOrEmpty(txtSortOrder.Text))
                        objReport.SortOrder = Convert.ToInt32(txtSortOrder.Text.Trim());
                    objReport.IsSendEnable = chkIsSendEnable.Checked;
                    objReport.ReportKey = txtReportKey.Text.Trim();

                    if (ClsSystem.AddUpdateReport(objReport, "Edit"))
                    {
                        //ClsCommon.setRecordUpdateStatusInDb("Report", "ReportID", "Edited", hdnReportID.Value);
                        Session["ReportData"] = null;
                        Response.Redirect("~/SystemPages/ListOfReport.aspx");
                    }
                    else
                    {
                        error_alert.Visible = true;
                        ErrorMessage.Text = "Error in update report";
                    }
                }
                else
                {
                    error_alert.Visible = true;
                    ErrorMessage.Text = "Report ID is not Selected for Update.";
                }
            }
            catch (Exception ex)
            {
                string Type = "Error";
                string system = string.Empty;
                string program = "Report Updation";
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
            Session["ReportData"] = null;
            Response.Redirect("~/SystemPages/ListOfReport.aspx");
        }
    }
}