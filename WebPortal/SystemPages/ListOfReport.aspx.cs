using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BIZ;
using Portal.Model;
using Portal.Provider.Model;
using Portal.Utility;

namespace WebPortal.SystemPages
{
    public partial class ListOfReport : System.Web.UI.Page
    {
        public static bool IsDeleteAllowed;
        public static bool IsEditAllowed;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG == "1")
            {
                Response.Redirect("~/Account/ChangePassword.aspx");
            }
            error_alert.Visible = false;
            CheckPermission();
            if (!IsPostBack)
            {
                ClsCommon.setFocusCurrentTabControl(Master, "liMobileDeviceMantenance");
                LoadGrid();
            }
        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                List<ModulePermission> lstModulePermission = ClsSystem.GetPermission(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "ReportList");
                if (lstModulePermission != null && lstModulePermission.Count > 0)
                {
                    var ReportList = lstModulePermission.Where(x => x.ModuleKey == "ReportList").FirstOrDefault();
                    if (!((ReportList != null) ? ReportList.IsActive : false))
                        Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
                    else
                    {
                        var ReportListAdd = lstModulePermission.Where(x => x.ModuleKey == "ReportListAdd").FirstOrDefault();
                        btnAddNew.Visible = (ReportListAdd != null) ? ReportListAdd.IsActive : false;

                        var ReportListEdit = lstModulePermission.Where(x => x.ModuleKey == "ReportListEdit").FirstOrDefault();
                        hdnIsEditAllowed.Value = (ReportListEdit != null) ? Convert.ToString(ReportListEdit.IsActive) : "false";

                        var ReportListDelete = lstModulePermission.Where(x => x.ModuleKey == "ReportListDelete").FirstOrDefault();
                        hdnIsDeleteAllowed.Value = (ReportListDelete != null) ? Convert.ToString(ReportListDelete.IsActive) : "false";
                    }
                }
                IsDeleteAllowed = Convert.ToBoolean(hdnIsDeleteAllowed.Value);
                IsEditAllowed = Convert.ToBoolean(hdnIsEditAllowed.Value);
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

        private void LoadGrid()
        {
            try
            {
                List<Report> LIST_OBJ_Report = new List<Report>();
                LIST_OBJ_Report = ClsSystem.GetAllReportList();

                if (LIST_OBJ_Report.Count != 0)
                {
                    BUSessionUtility.BUSessionContainer.OBJ_CLASS1 = LIST_OBJ_Report;
                    gvReport.DataSource = LIST_OBJ_Report;
                    gvReport.DataBind();
                }
                else
                {
                    gvReport.DataSource = string.Empty.ToList();
                    gvReport.DataBind();
                }
                MakeAccessible(gvReport);
            }
            catch (Exception ex)
            {
                error_alert.Visible = true;
                ErrorMessage.Text = ex.Message;
                throw ex;
                //Response.Write("this" + ex.Message);
            }
        }
        private void MakeAccessible(GridView grid)
        {
            //This replaces <td> with <th> and adds the scope attribute 
            grid.UseAccessibleHeader = true;

            //This will add the <thead> and <tbody> elements
            grid.HeaderRow.TableSection = TableRowSection.TableHeader;

            //This adds the <tfoot> element. Remove if you don't have a footer row 
            //grid.HeaderRow.TableSection = TableRowSection.TableFooter;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SystemPages/AddReport.aspx");
        }

        protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.Header)
            {
                foreach (System.Web.UI.WebControls.DataControlFieldCell cell in e.Row.Cells)
                {
                    if (cell.ContainingField.AccessibleHeaderText == "Edit")
                        cell.Visible = Convert.ToBoolean(hdnIsEditAllowed.Value);

                    if (cell.ContainingField.AccessibleHeaderText == "Select")
                        cell.Visible = Convert.ToBoolean(hdnIsDeleteAllowed.Value);
                }
            }

            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                foreach (System.Web.UI.WebControls.DataControlFieldCell cell in e.Row.Cells)
                {
                    if (cell.ContainingField.AccessibleHeaderText == "Select")
                    {
                        cell.ContainingField.Visible = IsDeleteAllowed;
                    }
                    if (cell.ContainingField.AccessibleHeaderText == "Edit")
                    {
                        cell.ContainingField.Visible = IsEditAllowed;
                    }
                }
            }
        }

        //Delete Operation
        [WebMethod]
        public static bool DeleteReport(string id)
        {
            bool returnVal = false;
            try
            {
                if (!string.IsNullOrEmpty(id) && !string.IsNullOrWhiteSpace(id))
                {
                    returnVal = ClsSystem.DeleteReport(Convert.ToInt32(id));
                }
                else
                {
                    returnVal = false;
                }
            }
            catch (Exception ex)
            {
                returnVal = false;
                throw ex;
            }
            return returnVal;
        }

        protected void gvReport_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["ReportData"] = null;
            if (!string.IsNullOrEmpty(gvReport.Rows[e.NewEditIndex].Cells[0].Text))
            {
                Report objReport = new Report();
                objReport.ReportID = Convert.ToInt32(gvReport.Rows[e.NewEditIndex].Cells[0].Text);
                objReport.ReportCode = gvReport.Rows[e.NewEditIndex].Cells[1].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
                objReport.ReportName = gvReport.Rows[e.NewEditIndex].Cells[2].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
                objReport.ReportDescription = gvReport.Rows[e.NewEditIndex].Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
                objReport.ReportServerLocation = gvReport.Rows[e.NewEditIndex].Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
                objReport.ReportCategory = gvReport.Rows[e.NewEditIndex].Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
                objReport.ReportSubCategory = gvReport.Rows[e.NewEditIndex].Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
                objReport.IsActive = Convert.ToBoolean(gvReport.Rows[e.NewEditIndex].Cells[7].Text);
                objReport.SortOrder = Convert.ToInt32(gvReport.Rows[e.NewEditIndex].Cells[8].Text);
                objReport.IsSendEnable = Convert.ToBoolean(gvReport.Rows[e.NewEditIndex].Cells[9].Text);
                objReport.ReportKey = gvReport.Rows[e.NewEditIndex].Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");

                Session["ReportData"] = objReport;
                Response.Redirect("~/SystemPages/UpdateReport.aspx");
            }
        }
    }
}