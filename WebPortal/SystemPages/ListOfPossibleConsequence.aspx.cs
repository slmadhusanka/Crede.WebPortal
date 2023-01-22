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
    public partial class ListOfPossibleConsequence : System.Web.UI.Page
    {
        public static bool IsDeleteAllowed;
        public static bool IsEditAllowed;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG == "1")
            {
                Response.Redirect("~/Account/ChangePassword.aspx");
            }


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
                List<ModulePermission> lstModulePermission = ClsSystem.GetPermission(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "Zone");
                if (lstModulePermission != null && lstModulePermission.Count > 0)
                {
                    var Zone = lstModulePermission.Where(x => x.ModuleKey == "Zone").FirstOrDefault();
                    if (!((Zone != null) ? Zone.IsActive : false))
                        Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
                    else
                    {
                        var ZoneAdd = lstModulePermission.Where(x => x.ModuleKey == "ZoneAdd").FirstOrDefault();
                        btnAddNew.Visible = (ZoneAdd != null) ? ZoneAdd.IsActive : false;

                        var ZoneEdit = lstModulePermission.Where(x => x.ModuleKey == "ZoneEdit").FirstOrDefault();
                        hdnIsEditAllowed.Value = (ZoneEdit != null) ? Convert.ToString(ZoneEdit.IsActive) : "false";

                        var ZoneDelete = lstModulePermission.Where(x => x.ModuleKey == "ZoneDelete").FirstOrDefault();
                        hdnIsDeleteAllowed.Value = (ZoneDelete != null) ? Convert.ToString(ZoneDelete.IsActive) : "false";
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
                List<PossibleConsequence> LIST_OBJ_Regions = new List<PossibleConsequence>();
                LIST_OBJ_Regions = ClsSystem.GetAllPossibleConsequence();
                //DataView dv = new DataView();
                if (LIST_OBJ_Regions.Count != 0)
                {
                    BUSessionUtility.BUSessionContainer.OBJ_CLASS1 = LIST_OBJ_Regions;
                    gvPossibleConsequence.DataSource = LIST_OBJ_Regions;
                    gvPossibleConsequence.DataBind();
                }
                else
                {
                    gvPossibleConsequence.DataSource = string.Empty.ToList();
                    gvPossibleConsequence.DataBind();
                }

                MakeAccessible(gvPossibleConsequence);
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


        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SystemPages/Add_PossibleConsequence.aspx");
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void gvRegion_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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
        public static bool DeleteERROR(string id)
        {
            bool returnVal = false;
            try
            {
                returnVal = ClsSystem.DeleteSPossibleConsequence(id);
            }
            catch (Exception ex)
            {
                returnVal = false;
                throw ex;
            }
            return returnVal;
        }

        protected void gvRegion_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            #region Set data in Session for editing (Author: Grishma)
            try
            {
                Session["RegionData"] = null;

                if (!string.IsNullOrEmpty(gvPossibleConsequence.Rows[e.NewEditIndex].Cells[0].Text))
                {
                    PossibleConsequence objRegion = new PossibleConsequence();
                    objRegion.ID = gvPossibleConsequence.Rows[e.NewEditIndex].Cells[0].Text;
                    objRegion.Description = gvPossibleConsequence.Rows[e.NewEditIndex].Cells[1].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
                    if (gvPossibleConsequence.Rows[e.NewEditIndex].Cells[3].Text == "Yes")
                        objRegion.IsActive = "True";
                    else
                        objRegion.IsActive = "False";
                    // objRegion.IsActive = gvRegion.Rows[e.NewEditIndex].Cells[3].Text;

                    Session["RegionData"] = objRegion;
                    Response.Redirect("~/SystemPages/UpdatePossibleConsequence.aspx");
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