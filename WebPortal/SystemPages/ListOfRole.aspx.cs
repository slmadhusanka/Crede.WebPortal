using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BIZ;
using Portal.Model;
using Portal.Provider;
using Portal.Utility;

namespace WebPortal.SystemPages
{
    public partial class ListOfRole : System.Web.UI.Page
    {
        public static bool IsEditAllowed;
        public static bool IsDeleteAllowed;
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
                List<ModulePermission> lstModulePermission = ClsSystem.GetPermission(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "Role");
                if (lstModulePermission != null && lstModulePermission.Count > 0)
                {
                    var Role = lstModulePermission.Where(x => x.ModuleKey == "Role").FirstOrDefault();
                    if (!((Role != null) ? Role.IsActive : false))
                        Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
                    else
                    {
                        var RoleAdd = lstModulePermission.Where(x => x.ModuleKey == "RoleAdd").FirstOrDefault();
                        btnAddNew.Visible = (RoleAdd != null) ? RoleAdd.IsActive : false;

                        var RoleEdit = lstModulePermission.Where(x => x.ModuleKey == "RoleEdit").FirstOrDefault();
                        hdnIsEditAllowed.Value = (RoleEdit != null) ? Convert.ToString(RoleEdit.IsActive) : "false";

                        var RoleDelete = lstModulePermission.Where(x => x.ModuleKey == "RoleDelete").FirstOrDefault();
                        hdnIsDeleteAllowed.Value = (RoleDelete != null) ? Convert.ToString(RoleDelete.IsActive) : "false";
                    }
                }
                IsEditAllowed = Convert.ToBoolean(hdnIsEditAllowed.Value);
                IsDeleteAllowed = Convert.ToBoolean(hdnIsDeleteAllowed.Value);
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
                List<Role> LIST_OBJ_Role = new List<Role>();
                LIST_OBJ_Role = ClsSystem.GetAllRoleList();
                //DataView dv = new DataView();
                if (LIST_OBJ_Role.Count != 0)
                {
                    BUSessionUtility.BUSessionContainer.OBJ_CLASS1 = LIST_OBJ_Role;
                    gvRole.DataSource = LIST_OBJ_Role;
                    gvRole.DataBind();
                }
                else
                {
                    gvRole.DataSource = string.Empty.ToList();
                    gvRole.DataBind();
                }
                MakeAccessible(gvRole);
            }
            catch (Exception ex)
            {
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

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SystemPages/AddRole.aspx");
        }

        protected void gvRole_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
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
                        if (cell.ContainingField.AccessibleHeaderText == "Edit")
                            cell.Visible = IsEditAllowed;

                        if (cell.ContainingField.AccessibleHeaderText == "Select")
                            cell.Visible = IsDeleteAllowed;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //delete operation
        [WebMethod]
        public static int DeleteRole(string id)
        {
            int returnVal = 0;
            try
            {
                if (!string.IsNullOrEmpty(id) && !string.IsNullOrWhiteSpace(id))
                {
                    DataSet ds = ClsSystem.GetRoleWiseUserList(Convert.ToInt32(id));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        returnVal = 2;
                    }
                    else
                    {
                        if (ClsSystem.DeleteRole(Convert.ToInt32(id)))
                        {
                            returnVal = 1;
                        }
                    }
                }
                else
                {
                    returnVal = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnVal;
        }

        protected void gvRole_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            try
            {
                string RoleCode = gvRole.Rows[e.NewSelectedIndex].Cells[0].Text;
                if (!string.IsNullOrEmpty(RoleCode))
                {
                    DataSet ds = ClsSystem.GetRoleWiseUserList(Convert.ToInt32(RoleCode));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Session["RoleWiseUserList"] = null;
                        lnkHidden.NavigateUrl = "~/Account/ReassignUserRole.aspx?c=" + RoleCode + "&d=Reassign User Role Information and try again to delete role."; ;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ScriptKey", "ShowUserList();", true);
                        Session["RoleWiseUserList"] = ds;
                    }
                    else
                    {
                        if (!ClsSystem.DeleteRole(Convert.ToInt32(RoleCode)))
                        {
                            error_alert.Visible = true;
                            ErrorMessage.Text = "Role cannot be deleted because there are existing user records for this Role";
                        }
                    }
                }
                else
                {
                    error_alert.Visible = true;
                    ErrorMessage.Text = "Role Code is Empty!";
                }
                LoadGrid();
            }
            catch (Exception ex)
            {
                string Type = "Error";
                string system = string.Empty;
                string Role = "Role Deletion";
                string Severity = "Medium";
                string Message = ex.Message;
                string User_id = BUSessionUtility.BUSessionContainer.USER_ID;
                ClsSecurityManage.CreateErrorLog(Type, system, Role, Severity, Message, User_id);
                error_alert.Visible = true;
                ErrorMessage.Text = Message;
                throw ex;
            }
        }

        protected void gvRole_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                Session["RoleData"] = null;
                if (!string.IsNullOrEmpty(gvRole.Rows[e.NewEditIndex].Cells[0].Text))
                {
                    Role objRole = new Role();
                    objRole.RoleCode = Convert.ToInt32(gvRole.Rows[e.NewEditIndex].Cells[0].Text);
                    objRole.Description = gvRole.Rows[e.NewEditIndex].Cells[1].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
                    objRole.IsActive = Convert.ToBoolean(gvRole.Rows[e.NewEditIndex].Cells[2].Text);

                    Session["RoleData"] = objRole;
                    Response.Redirect("~/SystemPages/UpdateRole.aspx");
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
    }
}