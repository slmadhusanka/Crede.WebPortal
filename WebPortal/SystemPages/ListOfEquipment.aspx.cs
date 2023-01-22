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
    public partial class ListOfUnitM : System.Web.UI.Page
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
                List<ModulePermission> lstModulePermission = ClsSystem.GetPermission(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "Unit");
                if (lstModulePermission != null && lstModulePermission.Count > 0)
                {
                    var Unit = lstModulePermission.Where(x => x.ModuleKey == "Unit").FirstOrDefault();
                    if (!((Unit != null) ? Unit.IsActive : false))
                        Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
                    else
                    {
                        var UnitAdd = lstModulePermission.Where(x => x.ModuleKey == "UnitAdd").FirstOrDefault();
                        //btnAddNew.Visible = (UnitAdd != null) ? UnitAdd.IsActive : false;

                        var UnitEdit = lstModulePermission.Where(x => x.ModuleKey == "UnitEdit").FirstOrDefault();
                        hdnIsEditAllowed.Value = (UnitEdit != null) ? Convert.ToString(UnitEdit.IsActive) : "false";

                        var UnitDelete = lstModulePermission.Where(x => x.ModuleKey == "UnitDelete").FirstOrDefault();
                        hdnIsDeleteAllowed.Value = (UnitDelete != null) ? Convert.ToString(UnitDelete.IsActive) : "false";
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
                List<DimUnit> LIST_OBJ_DimUnit = new List<DimUnit>();
                LIST_OBJ_DimUnit = ClsSystem.GetAllDimUnitList();

                if (LIST_OBJ_DimUnit.Count != 0)
                {
                    BUSessionUtility.BUSessionContainer.OBJ_CLASS1 = LIST_OBJ_DimUnit;
                    gvDimUnit.DataSource = LIST_OBJ_DimUnit;
                    gvDimUnit.DataBind();
                }
                else
                {
                    gvDimUnit.DataSource = string.Empty.ToList();
                    gvDimUnit.DataBind();
                }
                MakeAccessible(gvDimUnit);
            }
            catch (Exception ex)
            {
                // Response.Write("this" + ex.Message);
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

        protected void gvDimUnit_RowDataBound(object sender, GridViewRowEventArgs e)
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
        public static bool DeleteUnit(string id)
        {
            bool returnVal = false;
            try
            {
                returnVal = ClsSystem.DeleteDimUnit(id);
            }
            catch (Exception ex)
            {
                returnVal = false;
                throw ex;
            }
            return returnVal;
        }

        protected void gvDimUnit_RowEditing(object sender, GridViewEditEventArgs e)
        {
            #region Set data in Session for editing (Author: Grishma)
            try
            {
                Session["UnitData"] = null;

                if (!string.IsNullOrEmpty(gvDimUnit.Rows[e.NewEditIndex].Cells[0].Text))
                {
                    DimUnit objDimUnit = new DimUnit();
                    objDimUnit.UnitCode = Convert.ToInt32(gvDimUnit.Rows[e.NewEditIndex].Cells[0].Text);
                    objDimUnit.Description = gvDimUnit.Rows[e.NewEditIndex].Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");

                    objDimUnit.UnitType1Code = Convert.ToInt32(gvDimUnit.Rows[e.NewEditIndex].Cells[2].Text);
                    objDimUnit.UnitType2Code = Convert.ToInt32(gvDimUnit.Rows[e.NewEditIndex].Cells[4].Text);

                    objDimUnit.FacilityCode = Convert.ToInt32(gvDimUnit.Rows[e.NewEditIndex].Cells[6].Text);
                    objDimUnit.ProgramCode = Convert.ToInt32(gvDimUnit.Rows[e.NewEditIndex].Cells[8].Text);

                    objDimUnit.DescriptionLong = gvDimUnit.Rows[e.NewEditIndex].Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");

                    string orderId = gvDimUnit.Rows[e.NewEditIndex].Cells[12].Text;
                    objDimUnit.OrderID = ((!string.IsNullOrEmpty(orderId)) && orderId != "nbsp;") ? Convert.ToInt32(orderId) : 0;

                    objDimUnit.Beds = gvDimUnit.Rows[e.NewEditIndex].Cells[14].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");

                    if (gvDimUnit.Rows[e.NewEditIndex].Cells[15].Text == "Yes")
                        objDimUnit.IsActive = "True";
                    else
                        objDimUnit.IsActive = "False";
                    //objDimUnit.IsActive = gvDimUnit.Rows[e.NewEditIndex].Cells[14].Text;

                    objDimUnit.DescriptionShort = gvDimUnit.Rows[e.NewEditIndex].Cells[16].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");

                    //objDimUnit.UserID = Convert.ToInt32(gvDimUnit.Rows[e.NewEditIndex].Cells[11].Text);

                    Session["UnitData"] = objDimUnit;
                    Response.Redirect("~/SystemPages/UpdateEquipment.aspx");
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