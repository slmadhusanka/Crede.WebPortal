using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BIZ;
using Portal.Model;
using Portal.Provider;
using Portal.Provider.Model;
using Portal.Utility;

namespace WebPortal.Account
{
    public partial class ListOfUser : System.Web.UI.Page
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
                List<ModulePermission> lstModulePermission = ClsSystem.GetPermission(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "User");
                if (lstModulePermission != null && lstModulePermission.Count > 0)
                {
                    var User = lstModulePermission.Where(x => x.ModuleKey == "User").FirstOrDefault();
                    if (!((User != null) ? User.IsActive : false))
                        Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
                    else
                    {
                        var UserAdd = lstModulePermission.Where(x => x.ModuleKey == "UserAdd").FirstOrDefault();
                        btnAddNew.Visible = (UserAdd != null) ? UserAdd.IsActive : false;

                        var UserEdit = lstModulePermission.Where(x => x.ModuleKey == "UserEdit").FirstOrDefault();
                        hdnIsEditAllowed.Value = (UserEdit != null) ? Convert.ToString(UserEdit.IsActive) : "false";

                        var UserDelete = lstModulePermission.Where(x => x.ModuleKey == "UserDelete").FirstOrDefault();
                        hdnIsDeleteAllowed.Value = (UserDelete != null) ? Convert.ToString(UserDelete.IsActive) : "false";
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
                List<USERS> LIST_OBJ_USER = new List<USERS>();
                LIST_OBJ_USER = ClsSecurityManage.GetAllUserList();

                if (LIST_OBJ_USER.Count != 0)
                {
                    BUSessionUtility.BUSessionContainer.OBJ_CLASS1 = LIST_OBJ_USER;
                    gvUserSummary.DataSource = LIST_OBJ_USER;
                    gvUserSummary.DataBind();
                }
                MakeAccessible(gvUserSummary);
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

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void gvUserSummary_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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
        public static bool DeleteUser(string id)
        {
            bool returnVal = false;
            try
            {

                returnVal = Membership.DeleteUser(id, false);
                if (returnVal)
                {

                    ClsSystem.DeleteUserLab(Convert.ToInt32(id));
                }

            }
            catch (Exception ex)
            {
                returnVal = false;
                throw ex;
            }
            return returnVal;
        }

        private void ClearFields()
        {
            ErrorMessage.Text = string.Empty;

        }
        protected void gvUserSummary_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            #region Set data in Session for editing (Author: Grishma)
            try
            {
                Session["UserData"] = null;

                if (!string.IsNullOrEmpty(gvUserSummary.Rows[e.NewEditIndex].Cells[0].Text))
                {
                    USERS objUser = new USERS();

                    objUser.UserName = gvUserSummary.Rows[e.NewEditIndex].Cells[0].Text.Replace("&nbsp;", "");
                    objUser.User_ID = gvUserSummary.Rows[e.NewEditIndex].Cells[1].Text;
                    objUser.FirstName = gvUserSummary.Rows[e.NewEditIndex].Cells[2].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&nbsp;", "");
                    objUser.LastName = gvUserSummary.Rows[e.NewEditIndex].Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&nbsp;", "");
                    objUser.DisplayName = gvUserSummary.Rows[e.NewEditIndex].Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&nbsp;", "");
                    objUser.RoleCode = gvUserSummary.Rows[e.NewEditIndex].Cells[5].Text;
                    objUser.RegionCode = gvUserSummary.Rows[e.NewEditIndex].Cells[7].Text;
                    objUser.FacilityCode = gvUserSummary.Rows[e.NewEditIndex].Cells[9].Text;
                    objUser.UnitCode = gvUserSummary.Rows[e.NewEditIndex].Cells[11].Text;
                    objUser.Email = gvUserSummary.Rows[e.NewEditIndex].Cells[13].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&nbsp;", "");
                    objUser.PhoneNumber = gvUserSummary.Rows[e.NewEditIndex].Cells[14].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&nbsp;", "");
                    objUser.Occupation = Server.HtmlDecode(gvUserSummary.Rows[e.NewEditIndex].Cells[15].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&nbsp;", ""));


                    if (gvUserSummary.Rows[e.NewEditIndex].Cells[16].Text == "Yes")
                        objUser.IsLockedOut = "True";
                    else
                        objUser.IsLockedOut = "False";

                    if (gvUserSummary.Rows[e.NewEditIndex].Cells[17].Text == "Yes")
                        objUser.IsAuditor = "True";
                    else
                        objUser.IsAuditor = "False";


                    if (gvUserSummary.Rows[e.NewEditIndex].Cells[18].Text == "Yes")
                        objUser.IsActive = "True";
                    else
                        objUser.IsActive = "False";


                    Session["UserData"] = objUser;
                    Response.Redirect("~/Account/EditUser.aspx");
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
            #endregion
        }
    }
}