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
    public partial class ListOfUnitType1 : System.Web.UI.Page
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
                List<ModulePermission> lstModulePermission = ClsSystem.GetPermission(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "UnitType1");
                if (lstModulePermission != null && lstModulePermission.Count > 0)
                {
                    var UnitType1 = lstModulePermission.Where(x => x.ModuleKey == "UnitType1").FirstOrDefault();
                    if (!((UnitType1 != null) ? UnitType1.IsActive : false))
                        Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
                    else
                    {
                        //var UnitType1Add = lstModulePermission.Where(x => x.ModuleKey == "UnitType1Add").FirstOrDefault();
                        //btnAddNew.Visible = (UnitType1Add != null) ? UnitType1Add.IsActive : false;

                        //var UnitType1Edit = lstModulePermission.Where(x => x.ModuleKey == "UnitType1Edit").FirstOrDefault();
                        //hdnIsEditAllowed.Value = (UnitType1Edit != null) ? Convert.ToString(UnitType1Edit.IsActive) : "false";

                        //var UnitType1Delete = lstModulePermission.Where(x => x.ModuleKey == "UnitType1Delete").FirstOrDefault();
                        //hdnIsDeleteAllowed.Value = (UnitType1Delete != null) ? Convert.ToString(UnitType1Delete.IsActive) : "false";
                    }
                }

                hdnIsDeleteAllowed.Value = "true";
                hdnIsEditAllowed.Value = "true";
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
                List<UnitType1> LIST_OBJ_UnitType1 = new List<UnitType1>();
                LIST_OBJ_UnitType1 = ClsSystem.GetAllUnitType1List();
                //DataView dv = new DataView();

                if (LIST_OBJ_UnitType1.Count != 0)
                {
                    BUSessionUtility.BUSessionContainer.OBJ_CLASS1 = LIST_OBJ_UnitType1;
                    gvUnitTyp1.DataSource = LIST_OBJ_UnitType1;
                    gvUnitTyp1.DataBind();
                }
                else
                {
                    gvUnitTyp1.DataSource = string.Empty.ToList();
                    gvUnitTyp1.DataBind();
                }
                MakeAccessible(gvUnitTyp1);
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

        protected void gvUnitTyp1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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
        public static bool DeleteUnitType1(string id)
        {
            bool returnVal = false;
            try
            {
                returnVal = ClsSystem.DeleteUnitType1(id);
            }
            catch (Exception ex)
            {
                returnVal = false;
                throw ex;
            }
            return returnVal;
        }

        protected void gvUnitTyp1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            #region Set data in Session for editing (Author: Grishma)
            try
            {
                Session["UnitType1Data"] = null;

                if (!string.IsNullOrEmpty(gvUnitTyp1.Rows[e.NewEditIndex].Cells[0].Text))
                {
                    UnitType1 objUnitType1 = new UnitType1();
                    objUnitType1.UnitType1Code = string.IsNullOrEmpty(gvUnitTyp1.Rows[e.NewEditIndex].Cells[0].Text) ? "" : gvUnitTyp1.Rows[e.NewEditIndex].Cells[0].Text;
                    objUnitType1.Description = gvUnitTyp1.Rows[e.NewEditIndex].Cells[1].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");

                    if (gvUnitTyp1.Rows[e.NewEditIndex].Cells[2].Text == "Yes")
                        objUnitType1.IsActive = "True";
                    else
                        objUnitType1.IsActive = "False";
                    //objUnitType1.IsActive = gvUnitTyp1.Rows[e.NewEditIndex].Cells[2].Text;
                    objUnitType1.DescriptionShort =
                        Utils.Utils.RemoveNbsp(gvUnitTyp1.Rows[e.NewEditIndex].Cells[3].Text);

                    Session["UnitType1Data"] = objUnitType1;
                    Response.Redirect("~/SystemPages/UpdateEquipmentType1.aspx");
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

        protected void gvUnitTyp1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            if (BUSessionUtility.BUSessionContainer.OBJ_CLASS1 != null)
            {
                List<UnitType1> LIST_OBJ_UnitType1 = new List<UnitType1>();
                LIST_OBJ_UnitType1 = (List<UnitType1>)BUSessionUtility.BUSessionContainer.OBJ_CLASS1;
                gvUnitTyp1.PageIndex = e.NewPageIndex;
                gvUnitTyp1.DataSource = LIST_OBJ_UnitType1;
                gvUnitTyp1.DataBind();
            }
        }
    }
}