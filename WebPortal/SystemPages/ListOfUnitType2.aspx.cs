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
using Portal.Utility;

namespace WebPortal.SystemPages
{
    public partial class ListOfUnitType2 : System.Web.UI.Page
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
                ClsCommon.setFocusCurrentTabControl(Master, "liTableMaintainance");
                LoadGrid();
            }

        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                List<ModulePermission> lstModulePermission = ClsSystem.GetPermission(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "UnitType2");
                if (lstModulePermission != null && lstModulePermission.Count > 0)
                {
                    var UnitType2 = lstModulePermission.Where(x => x.ModuleKey == "UnitType2").FirstOrDefault();
                    if (!((UnitType2 != null) ? UnitType2.IsActive : false))
                        Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
                    else
                    {
                        var UnitType2Add = lstModulePermission.Where(x => x.ModuleKey == "UnitType2Add").FirstOrDefault();
                        btnAddNew.Visible = (UnitType2Add != null) ? UnitType2Add.IsActive : false;

                        var UnitType2Edit = lstModulePermission.Where(x => x.ModuleKey == "UnitType2Edit").FirstOrDefault();
                        hdnIsEditAllowed.Value = (UnitType2Edit != null) ? Convert.ToString(UnitType2Edit.IsActive) : "false";

                        var UnitType2Delete = lstModulePermission.Where(x => x.ModuleKey == "UnitType2Delete").FirstOrDefault();
                        hdnIsDeleteAllowed.Value = (UnitType2Delete != null) ? Convert.ToString(UnitType2Delete.IsActive) : "false";
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
                List<UnitType2> LIST_OBJ_UnitType2 = new List<UnitType2>();
                LIST_OBJ_UnitType2 = ClsSystem.GetAllUnitType2List();
                //DataView dv = new DataView();

                if (LIST_OBJ_UnitType2.Count != 0)
                {
                    BUSessionUtility.BUSessionContainer.OBJ_CLASS1 = LIST_OBJ_UnitType2;
                    gvUnitTyp2.DataSource = LIST_OBJ_UnitType2;
                    gvUnitTyp2.DataBind();
                }
                else
                {
                    gvUnitTyp2.DataSource = string.Empty.ToList();
                    gvUnitTyp2.DataBind();
                }
                MakeAccessible(gvUnitTyp2);
            }
            catch (Exception ex)
            {
                //Response.Write("this" + ex.Message);
                throw ex;
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

        protected void gvUnitTyp2_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.Header)
            {
                foreach (System.Web.UI.WebControls.DataControlFieldCell cell in e.Row.Cells)
                {
                    if (cell.ContainingField.AccessibleHeaderText == "Edit")
                        cell.Visible = IsEditAllowed;

                    if (cell.ContainingField.AccessibleHeaderText == "Select")
                        cell.Visible = IsDeleteAllowed;
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
        public static bool DeleteUnitType2(string id)
        {
            bool returnVal = false;
            try
            {
                returnVal = ClsSystem.DeleteUnitType2(id);
            }
            catch (Exception ex)
            {
                returnVal = false;
                throw ex;
            }
            return returnVal;
        }

        protected void gvUnitTyp2_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            //string UnitType2Code = gvUnitTyp2.Rows[e.NewEditIndex].Cells[0].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
            //string Description = gvUnitTyp2.Rows[e.NewEditIndex].Cells[1].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
            //string IsActive = gvUnitTyp2.Rows[e.NewEditIndex].Cells[2].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">"); 
            //if (!string.IsNullOrEmpty(UnitType2Code))
            //    Response.Redirect("~/SystemPages/UpdateUnitType2.aspx?UnitType2Code=" + UnitType2Code + "&Description=" + HttpUtility.UrlEncode(Description) + "&IsActive=" + IsActive);

            #region Set data in Session for editing (Author: Grishma)
            try
            {
                Session["UnitType2Data"] = null;

                if (!string.IsNullOrEmpty(gvUnitTyp2.Rows[e.NewEditIndex].Cells[0].Text))
                {
                    UnitType2 objUnitType2 = new UnitType2();
                    objUnitType2.UnitType2Code = gvUnitTyp2.Rows[e.NewEditIndex].Cells[0].Text;
                    objUnitType2.Description = gvUnitTyp2.Rows[e.NewEditIndex].Cells[1].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
                    if (gvUnitTyp2.Rows[e.NewEditIndex].Cells[2].Text == "Yes")
                        objUnitType2.IsActive = "True";
                    else
                        objUnitType2.IsActive = "False";
                    //objUnitType2.IsActive = gvUnitTyp2.Rows[e.NewEditIndex].Cells[2].Text;
                    objUnitType2.DescriptionShort = gvUnitTyp2.Rows[e.NewEditIndex].Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
                    Session["UnitType2Data"] = objUnitType2;
                    Response.Redirect("~/SystemPages/UpdateUnitType2.aspx");
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
        }
    }
}