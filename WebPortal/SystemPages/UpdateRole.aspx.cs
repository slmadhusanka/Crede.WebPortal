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
    public partial class UpdateRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG == "1")
            {
                Response.Redirect("~/Account/ChangePassword.aspx");
            }
            success_alert.Visible = false;
            error_alert.Visible = false;
            try
            {
                if (!IsPostBack)
                {
                    CheckPermission();
                    ClsCommon.setFocusCurrentTabControl(Master, "liMobileDeviceMantenance");
                    if (Session["RoleData"] != null)
                    {
                        Role objRole = (Role)(Session["RoleData"]);
                        hdnRoleCode.Value = Convert.ToString(objRole.RoleCode);
                        txtRoleDescription.Text = objRole.Description;
                        chkIsActive.Checked = objRole.IsActive;
                    }
                    else
                    {
                        Response.Redirect("~/SystemPages/ListOfRole.aspx");
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    SuccessMessage.Text = string.Empty;
                    ErrorMessage.Text = string.Empty;
                    if (!string.IsNullOrEmpty(hdnRoleCode.Value))
                    {
                        if (chkIsActive.Checked == false)
                        {
                            DataSet ds = ClsSystem.GetRoleWiseUserList(Convert.ToInt32(hdnRoleCode.Value));
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                Session["RoleWiseUserList"] = null;
                                lnkHidden.NavigateUrl = "~/Account/ReassignUserRole.aspx?c=" + hdnRoleCode.Value + "&d=Reassign User Role Information and try again to deactivate role.";
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ScriptKey", "ShowUserList();", true);
                                Session["RoleWiseUserList"] = ds;
                            }
                            else
                            {
                                SaveData();
                            }
                        }
                        else
                        {
                            SaveData();
                        }
                    }
                    else
                    {
                        error_alert.Visible = true;
                        ErrorMessage.Text = "Role Code is not Selected for Update.";
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
                string program = "Role Updation";
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
            Session["RoleWiseUserList"] = null;
            Session["RoleData"] = null;
            Response.Redirect("~/SystemPages/ListOfRole.aspx");
        }

        private void SaveData()
        {
            Role objRole = new Role();
            objRole.RoleCode = Convert.ToInt32(hdnRoleCode.Value);
            objRole.Description = txtRoleDescription.Text.Trim();
            objRole.IsActive = chkIsActive.Checked;

            if (ClsSystem.AddUpdateRole(objRole, "Edit", 0))
            {
                //ClsCommon.setRecordUpdateStatusInDb("Role", "RoleCode", "Edited", hdnRoleCode.Value);
                Session["RoleWiseUserList"] = null;
                Session["RoleData"] = null;
                Response.Redirect("~/SystemPages/ListOfRole.aspx");
            }
            else
            {
                error_alert.Visible = true;
                ErrorMessage.Text = "Role Code is not Updated.";
            }
        }
    }
}