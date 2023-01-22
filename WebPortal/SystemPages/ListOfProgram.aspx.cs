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
    public partial class ListOfProgram : System.Web.UI.Page
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
                if (Session["ismobiletab"] != null)
                {
                    if (Session["ismobiletab"].ToString().Equals("true"))
                    {
                        ClsCommon.setFocusCurrentTabControl(Master, "liMobileDeviceMantenance");
                    }
                    else
                    {
                        ClsCommon.setFocusCurrentTabControl(Master, "liTableMaintainance");
                    }
                }
                else
                {
                    ClsCommon.setFocusCurrentTabControl(Master, "liTableMaintainance");
                }

                LoadGrid();
            }
        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                List<ModulePermission> lstModulePermission = ClsSystem.GetPermission(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "Program");
                if (lstModulePermission != null && lstModulePermission.Count > 0)
                {
                    var Program = lstModulePermission.Where(x => x.ModuleKey == "Program").FirstOrDefault();
                    if (!((Program != null) ? Program.IsActive : false))
                        Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
                    else
                    {
                        //var ProgramAdd = lstModulePermission.Where(x => x.ModuleKey == "ProgramAdd").FirstOrDefault();
                        //btnAddNew.Visible = (ProgramAdd != null) ? ProgramAdd.IsActive : false;

                        //var ProgramEdit = lstModulePermission.Where(x => x.ModuleKey == "ProgramEdit").FirstOrDefault();
                        //hdnIsEditAllowed.Value = (ProgramEdit != null) ? Convert.ToString(ProgramEdit.IsActive) : "false";

                        //var ProgramDelete = lstModulePermission.Where(x => x.ModuleKey == "ProgramDelete").FirstOrDefault();
                        //hdnIsDeleteAllowed.Value = (ProgramDelete != null) ? Convert.ToString(ProgramDelete.IsActive) : "false";
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
                List<Program> LIST_OBJ_Program = new List<Program>();
                LIST_OBJ_Program = ClsSystem.GetAllProgramList();

                if (LIST_OBJ_Program.Count != 0)
                {
                    BUSessionUtility.BUSessionContainer.OBJ_CLASS1 = LIST_OBJ_Program;
                    gvProgram.DataSource = LIST_OBJ_Program;
                    gvProgram.DataBind();
                }
                else
                {
                    gvProgram.DataSource = string.Empty.ToList();
                    gvProgram.DataBind();
                }
                MakeAccessible(gvProgram);
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

        protected void gvProgram_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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
        public static bool DeleteProgram(string id)
        {
            bool returnVal = false;
            try
            {
                returnVal = ClsSystem.DeleteProgram(id);
            }
            catch (Exception ex)
            {
                returnVal = false;
                throw ex;
            }
            return returnVal;
        }

        protected void gvProgram_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            #region Set data in Session for editing (Author: Grishma)
            try
            {
                Session["ProgramData"] = null;

                if (!string.IsNullOrEmpty(gvProgram.Rows[e.NewEditIndex].Cells[0].Text))
                {
                    Program objProgram = new Program();

                    objProgram.Program_Code = gvProgram.Rows[e.NewEditIndex].Cells[0].Text;
                    objProgram.Program_Desc = gvProgram.Rows[e.NewEditIndex].Cells[1].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
                    objProgram.Program_Type_Code = gvProgram.Rows[e.NewEditIndex].Cells[2].Text;
                    //objProgram.IsActive = gvProgram.Rows[e.NewEditIndex].Cells[4].Text;
                    if (gvProgram.Rows[e.NewEditIndex].Cells[4].Text == "Yes")
                        objProgram.IsActive = "True";
                    else
                        objProgram.IsActive = "False";
                    objProgram.DescriptionShort = gvProgram.Rows[e.NewEditIndex].Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");

                    Session["ProgramData"] = objProgram;
                    Response.Redirect("~/SystemPages/UpdateProgram.aspx");
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