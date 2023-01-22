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

namespace WebPortal.ReportViewer
{
    public partial class AuthenticatedReport : System.Web.UI.Page
    {
        List<Report> lstReport = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG == "1")
            {
                Response.Redirect("~/Account/ChangePassword.aspx");
            }

            if (!IsPostBack)
            {


                try
                {
                    string curUserRoleCode = BUSessionUtility.BUSessionContainer.ROLES_FOR_USER;
                    divRoleID.InnerHtml = "Role: " + curUserRoleCode;

                    lstReport = ClsSecurityManage.GetAuthenticatedUserReports(Convert.ToInt32(curUserRoleCode));
                    ShowReports();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "Report"))
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

        private void ShowReports()
        {
            try
            {
                if (lstReport != null && lstReport.Count > 0)
                {
                    lvReportCategory.DataSource = lstReport.Select(x => new { x.ReportCategory }).Distinct().AsEnumerable().Select(r => new Report { ReportCategory = r.ReportCategory }).ToList(); ;
                    lvReportCategory.DataBind();
                }
                else
                {
                    lblnote.Text = "Note: If you do not see the list, please contact the application administrator.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lvReportCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    Report objReport = (Report)e.Item.DataItem;
                    ListView lvReports = (ListView)e.Item.FindControl("lvReports");
                    lvReports.DataSource = lstReport.Where(x => x.ReportCategory == objReport.ReportCategory).ToList();
                    lvReports.DataBind();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lvReportCategory_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ShowReport")
                {
                    Session["ShowReport"] = Convert.ToString(e.CommandArgument);
                    Response.Redirect("~/ReportViewer/ReportContainer.aspx");
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
    }
}