using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BIZ;
using Portal.Provider;
using Portal.Provider.Model;
using Portal.Utility;

namespace WebPortal.Account
{
    public partial class UpdateAccountSettings : System.Web.UI.Page
    {
        USERS OBJ_USERS = new USERS();
        ClsSecurityManage ObjSecurityManage = new ClsSecurityManage();
        Boolean IsLocked = false;
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
                ClsCommon.setFocusCurrentTabControl(Master, "liAccountSettings");

                OBJ_USERS = ClsSecurityManage.GetSpecificUser(BUSessionUtility.BUSessionContainer.UserName);
                BindCombo();

                txtUserName.Text = OBJ_USERS.UserName;
                txtUserFirstName.Text = OBJ_USERS.FirstName;


                txtUserLastName.Text = OBJ_USERS.LastName;

                ddlRoleName.Text = OBJ_USERS.RoleCode;

                ddlFacility.SelectedValue = OBJ_USERS.FacilityCode;
                ddlUnit.SelectedValue = OBJ_USERS.UnitCode;

                Email.Text = OBJ_USERS.Email;

            }

        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                var IsUserListAllowed = ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "User");
                btnGo.Visible = IsUserListAllowed;
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

        private void BindCombo()
        {
            ClsCommon.BindComboWithSQL(ddlRoleName, @"SELECT RoleCode,Description FROM Role WHERE IsActive = 1 ", true, string.Empty, "Description", "RoleCode");

            ClsCommon.BindComboWithSQL(ddlFacility, @"SELECT  FacilityCode, Description  FROM  DimFacility ORDER BY Description ", true, string.Empty, "Description", "FacilityCode");
            if (!string.IsNullOrEmpty(OBJ_USERS.FacilityCode))
                ClsCommon.BindComboWithSQL(ddlUnit, @"SELECT   UnitCode,Description,Convert(nvarchar, u.Description) + ' ( ' + Convert(nvarchar, isnull(u.DescriptionLong,'')) +' )' as Unit_Name_Description_Long FROM  DimUnit u where u.FacilityCode='" + OBJ_USERS.FacilityCode + "' ORDER BY Description ", true, "---Select---", "Unit_Name_Description_Long", "UnitCode");
            else
                ClsCommon.BindComboWithSQL(ddlUnit, @"SELECT  UnitCode, Description , Convert(nvarchar, Description) + ' ( ' + Convert(nvarchar, isnull(DescriptionLong,'')) +' )' as Unit_Name_Description_Long FROM  DimUnit  ORDER BY Description ", true, string.Empty, "Unit_Name_Description_Long", "UnitCode");
        }
        private USERS setObjectProperties()
        {
            OBJ_USERS.User_ID = BUSessionUtility.BUSessionContainer.USER_ID;
            OBJ_USERS.UserName = txtUserName.Text;
            OBJ_USERS.FirstName = txtUserFirstName.Text;
            OBJ_USERS.LastName = txtUserLastName.Text;
            OBJ_USERS.Email = Email.Text;
            OBJ_USERS.FacilityCode = ddlFacility.Text;
            OBJ_USERS.RoleCode = ddlRoleName.Text;
            OBJ_USERS.UnitCode = ddlUnit.Text;
            return OBJ_USERS;
        }
        protected void DeleteUserButton_Click(object sender, EventArgs e)
        {
            ErrorMessage.Text = string.Empty;
            SuccessMessage.Text = string.Empty;
           
            Response.Redirect("~/hld_reprocessing_log.aspx");
        }
        protected void UpdateUserButton_Click(object sender, EventArgs e)
        {
            MembershipCreateStatus status;
            try
            {
                OBJ_USERS = new USERS();
                OBJ_USERS = setObjectProperties();
                ObjSecurityManage.UpdateUserAccount(OBJ_USERS, out status);
                if (MembershipCreateStatus.Success == status)
                {
                    ClearFields();
                    success_alert.Visible = true;
                    SuccessMessage.Text = "User Updated Successfully";

                    if (OBJ_USERS.RoleCode == "1")
                        Response.Redirect("~/Account/UpdateAccountSettings.aspx");
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                string Type = "Error";
                string system = string.Empty;
                string program = "Update Account Settings";
                string Severity = "Medium";
                string Message = ex.Message;
                string User_id = BUSessionUtility.BUSessionContainer.USER_ID;
                ClsSecurityManage.CreateErrorLog(Type, system, program, Severity, Message, User_id);
                ErrorMessage.Text = ex.Message;// ConfigurationManager.AppSettings["ErrorMsg"].ToString();
                error_alert.Visible = true;
                throw ex;
            }
        }
        private void ClearLabel()
        {
            ErrorMessage.Text = string.Empty;
            SuccessMessage.Text = string.Empty;
        }
        private void ClearFields()
        {

        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/ListOfUser.aspx");
        }
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/ChangePassword.aspx");
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlUnit.Items.Clear();
            ClsCommon.BindComboWithSQL(ddlUnit, @"SELECT   UnitCode , Description , Convert(nvarchar, u.Description) + ' ( ' + Convert(nvarchar, isnull(u.DescriptionLong,'')) +' )' as Unit_Name_Description_Long FROM  DimUnit u where u.FacilityCode='" + ddlFacility.Text + "' ORDER BY Description", true, "---Select---", "Unit_Name_Description_Long", "UnitCode");
        }
    }
}