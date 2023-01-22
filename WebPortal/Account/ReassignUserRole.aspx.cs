using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BIZ;
using Portal.Utility;

namespace WebPortal.Account
{
    public partial class ReassignUserRole : System.Web.UI.Page
    {
        private DataSet dsRole;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG == "1")
                {
                    Response.Redirect("~/Account/ChangePassword.aspx");
                }
                success_alert.Visible = false;
                error_alert.Visible = false;
                if (!IsPostBack)
                {
                    CheckPermission();
                    lgText.InnerText = Request.QueryString["d"];
                    if (Session["RoleWiseUserList"] != null)
                    {
                        GetRoleDropDownData(Request.QueryString["c"]);
                        var ds = (DataSet)Session["RoleWiseUserList"];
                        lvUserList.DataSource = ds.Tables[0];
                        lvUserList.DataBind();

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "RoleEdit"))
                    Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
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

        private void GetRoleDropDownData(string roleCode)
        {
            dsRole = ClsSystem.GetAllProgram();
            DataRow[] dr = dsRole.Tables[0].Select("RoleCode = " + roleCode);
            if (dr != null)
                dsRole.Tables[0].Rows.Remove(dr[0]);
        }

        protected void lvUserList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    DropDownList ddlRole = (DropDownList)e.Item.FindControl("ddlRole");
                    ddlRole.DataSource = dsRole.Tables[0];
                    ddlRole.DataTextField = "Description";
                    ddlRole.DataValueField = "RoleCode";
                    ddlRole.DataBind();

                    ddlRole.Items.Insert(0, new ListItem("---Select---", ""));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var userID = "";

                string userXML = string.Empty;
                foreach (ListViewDataItem lv in lvUserList.Items)
                {
                    HiddenField hdnUserId = (HiddenField)lv.FindControl("hdnUserId");
                    DropDownList ddlRole = (DropDownList)lv.FindControl("ddlRole");
                    userID = hdnUserId.Value;
                    userXML += "<User><UserID>" + hdnUserId.Value + "</UserID><Role_Code>" + ddlRole.Text + "</Role_Code></User>";
                }
                if (!string.IsNullOrEmpty(userXML))
                {
                    userXML = "<root>" + userXML + "</root>";
                    if (ClsSystem.ReassignUserRole(userXML, userID))
                    {
                        success_alert.Visible = true;
                        SuccessMessage.Text = "Users reassigned successfully.";
                    }
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}