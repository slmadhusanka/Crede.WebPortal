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
using Portal.Provider;
using Portal.Utility;

namespace WebPortal.SystemPages
{
    public partial class ListOfAPIClients : System.Web.UI.Page
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
                ClsCommon.setFocusCurrentTabControl(Master, "liMobileDeviceMantenance");
                LoadGrid();
            }
        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                List<ModulePermission> lstModulePermission = ClsSystem.GetPermission(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "Note");
                if (lstModulePermission != null && lstModulePermission.Count > 0)
                {
                    var APIClient = lstModulePermission.Where(x => x.ModuleKey == "Note").FirstOrDefault();
                    if (!((APIClient != null) ? APIClient.IsActive : false))
                        Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
                    else
                    {
                        var APIClientAdd = lstModulePermission.Where(x => x.ModuleKey == "NoteAdd").FirstOrDefault();
                        btnAddNew.Visible = (APIClientAdd != null) ? APIClientAdd.IsActive : false;
                        
                        var APIClientDelete = lstModulePermission.Where(x => x.ModuleKey == "NoteDelete").FirstOrDefault();
                        hdnIsDeleteAllowed.Value = (APIClientDelete != null) ? Convert.ToString(APIClientDelete.IsActive) : "false";
                    }
                }
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

        public void LoadGrid()
        {
            try
            {
                List<APIClient> LIST_OBJ_APIClient = new List<APIClient>();
                LIST_OBJ_APIClient = ClsSystem.GetAllAPIClients();

                if (LIST_OBJ_APIClient.Count != 0)
                {
                    BUSessionUtility.BUSessionContainer.OBJ_CLASS1 = LIST_OBJ_APIClient;
                    gvAPIClient.DataSource = LIST_OBJ_APIClient;
                    gvAPIClient.DataBind();
                }
                else
                {
                    gvAPIClient.DataSource = string.Empty.ToList();
                    gvAPIClient.DataBind();
                }
                MakeAccessible(gvAPIClient);
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

        protected void gvAPIClient_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.Header)
            {
                foreach (System.Web.UI.WebControls.DataControlFieldCell cell in e.Row.Cells)
                {
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
                }
            }
        }

        //Delete Operation
        [WebMethod]
        public static bool DeleteClient(string id)
        {
            bool returnVal = false;
            try
            {
                if (!string.IsNullOrEmpty(id) && !string.IsNullOrWhiteSpace(id))
                {
                    returnVal = ClsSystem.DeleteAPIClient(id);
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
    }
}