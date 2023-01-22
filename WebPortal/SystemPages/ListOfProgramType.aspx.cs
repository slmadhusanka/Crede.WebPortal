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
    public partial class ListOfProgramType : System.Web.UI.Page
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
                List<ModulePermission> lstModulePermission = ClsSystem.GetPermission(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "ProgramType");
                if (lstModulePermission != null && lstModulePermission.Count > 0)
                {
                    var ProgramType = lstModulePermission.Where(x => x.ModuleKey == "ProgramType").FirstOrDefault();
                    if (!((ProgramType != null) ? ProgramType.IsActive : false))
                        Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
                    else
                    {
                        //var ProgramTypeAdd = lstModulePermission.Where(x => x.ModuleKey == "ProgramTypeAdd").FirstOrDefault();
                       //btnAddNew.Visible = (ProgramTypeAdd != null) ? ProgramTypeAdd.IsActive : false;

                        //var ProgramTypeEdit = lstModulePermission.Where(x => x.ModuleKey == "ProgramTypeEdit").FirstOrDefault();
                        //hdnIsEditAllowed.Value = (ProgramTypeEdit != null) ? Convert.ToString(ProgramTypeEdit.IsActive) : "false";

                        //var ProgramTypeDelete = lstModulePermission.Where(x => x.ModuleKey == "ProgramTypeDelete").FirstOrDefault();
                        //hdnIsDeleteAllowed.Value = (ProgramTypeDelete != null) ? Convert.ToString(ProgramTypeDelete.IsActive) : "false";
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
                List<ProgramType> LIST_OBJ_Facility = new List<ProgramType>();
                LIST_OBJ_Facility = ClsSystem.GetAllProgramTypeList();
                //DataView dv = new DataView();

                if (LIST_OBJ_Facility.Count != 0)
                {
                    BUSessionUtility.BUSessionContainer.OBJ_CLASS1 = LIST_OBJ_Facility;
                    gvProgramType.DataSource = LIST_OBJ_Facility;
                    gvProgramType.DataBind();
                }
                else
                {
                    gvProgramType.DataSource = string.Empty.ToList();
                    gvProgramType.DataBind();
                }
                MakeAccessible(gvProgramType);
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

        protected void gvProgramType_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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
        public static bool DeleteProgramType(string id)
        {
            bool returnVal = false;
            try
            {
                returnVal = ClsSystem.DeleteProgramType(id);
            }
            catch (Exception ex)
            {
                returnVal = false;
                throw ex;
            }
            return returnVal;
        }

        protected void gvProgramType_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
             #region Set data in Session for editing (Author: Grishma)
            try
            {
                Session["ProgramTypeData"] = null;

                if (!string.IsNullOrEmpty(gvProgramType.Rows[e.NewEditIndex].Cells[0].Text))
                {
                    ProgramType objProgramType = new ProgramType();
                    objProgramType.ProgramTypeCode = gvProgramType.Rows[e.NewEditIndex].Cells[0].Text;
                    objProgramType.Description = gvProgramType.Rows[e.NewEditIndex].Cells[1].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
                    //objProgramType.IsActive = gvProgramType.Rows[e.NewEditIndex].Cells[2].Text;
                    if (gvProgramType.Rows[e.NewEditIndex].Cells[2].Text == "Yes")
                        objProgramType.IsActive = "True";
                    else
                        objProgramType.IsActive = "False";

                    objProgramType.DescriptionShort =
                        Utils.Utils.RemoveNbsp(gvProgramType.Rows[e.NewEditIndex].Cells[3].Text);

                    Session["ProgramTypeData"] = objProgramType;
                    Response.Redirect("~/SystemPages/UpdateProgramType.aspx");
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