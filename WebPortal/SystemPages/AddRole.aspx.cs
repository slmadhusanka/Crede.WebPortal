using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BIZ;
using Portal.Model;
using Portal.Provider;
using Portal.Utility;

namespace WebPortal.SystemPages
{
    public partial class AddRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
                ClsCommon.setFocusCurrentTabControl(Master, "liMobileDeviceMantenance");
                FillTemplateRoleDropDown();
            }
        }

        private void FillTemplateRoleDropDown()
        {
            try
            {
                DataSet ds;
                ddlTemplateRole.Items.Clear();
                ddlTemplateRole.SelectedValue = null;
                ds = ClsSystem.GetAllRoles();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlTemplateRole.DataSource = ds.Tables[0];
                    ddlTemplateRole.DataTextField = "Description";
                    ddlTemplateRole.DataValueField = "RoleCode";
                    ddlTemplateRole.DataBind();
                }
                ddlTemplateRole.Items.Insert(0, new ListItem("---Select---", "0"));
            }
            catch (Exception ex)
            {
                error_alert.Visible = true;
                ErrorMessage.Text = ex.Message;
                throw ex;
            }
        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "RoleAdd"))
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (Page.IsValid)
                {
                    SuccessMessage.Text = string.Empty;
                    ErrorMessage.Text = string.Empty;
                    int templateRoleId = 0;

                    Role objRole = new Role();
                    objRole.Description = txtRoleDescription.Text.Trim();
                    objRole.IsActive = true;

                    if (ddlTemplateRole.Text != "0")
                    {
                        templateRoleId = Convert.ToInt32(ddlTemplateRole.Text);
                    }

                    if (ClsSystem.AddUpdateRole(objRole, "Add", templateRoleId))
                    {
                        //ClsCommon.setRecordUpdateStatusInDb("Role", "RoleCode", "Added", "");
                        Response.Redirect("~/SystemPages/ListOfRole.aspx");
                    }
                    else
                    {
                        error_alert.Visible = true;
                        ErrorMessage.Text = "Role add failed";
                    }
                }

                
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                string Type = "Error";
                string system = string.Empty;
                string program = "Adding Role";
                string Severity = "Medium";
                string Message = ex.Message;
                string User_id = BUSessionUtility.BUSessionContainer.USER_ID;
                ClsSecurityManage.CreateErrorLog(Type, system, program, Severity, Message, User_id);
                error_alert.Visible = true;
                ErrorMessage.Text = Message;
                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SystemPages/ListOfRole.aspx");
        }
    }
}