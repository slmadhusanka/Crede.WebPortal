using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Portal.BIZ;
using Portal.BIZ.HelperModel;
using Portal.Provider;
using Portal.Provider.Model;
using Portal.Utility;

namespace WebPortal.Account
{
    public partial class Register : System.Web.UI.Page
    {
        ClsSecurityManage objSecurityManage = new ClsSecurityManage();
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
                ClearFields();
                BindCombo();
                genaratePassword();
            }

            //

            if (ddlRegion.SelectedIndex > 0)
            {
                //    ClsCommon.BindComboWithSQL(ddlFacility, @"SELECT  CASE 
                //      WHEN IsActive = '1'
                //         THEN [Description] 
                //      ELSE [Description] + ' (Not Active)'
                //END AS Description,FacilityCode FROM  DimFacility WHERE RegionCode='" + ddlRegion.Text + "' ORDER BY Description", true, "", "Description", "FacilityCode");

                //Bind Facility dropdown
                DataTable table = ClsSystem.GetFacilityByRegion(Convert.ToInt32(ddlRegion.Text));
                table.AsEnumerable().Where(r => r.Field<bool>("IsActive") == false).ToList().ForEach(row => row.Delete());
                table.DefaultView.Sort = "Description ASC";
                table = table.DefaultView.ToTable();
                table.AcceptChanges();

                ddlLabs.DataSource = table;
                ddlLabs.DataTextField = "Description";
                ddlLabs.DataValueField = "FacilityCode";
                ddlLabs.DataBind();

            }
            else
            {
                ddlLabs.Items.Clear();
                //ddlFacility.Items.Insert(0, new ListItem("---Select---", ""));

            }

            ddlUnit.Items.Clear();
            //ddlUnit.Items.Insert(0, new ListItem("---Select---", ""));

        }
        
        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "UserAdd"))
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

        protected void DeleteUserButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/ListOfUser.aspx");
        }

        private void BindCombo()
        {
            DataSet ds;
            ds = ClsSystem.GetAllDimRegion();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlRegion.DataSource = ds.Tables[0];
                ddlRegion.DataTextField = "Description";
                ddlRegion.DataValueField = "RegionCode";
                ddlRegion.DataBind();
                ddlRegion.Items.Insert(0, new ListItem("---Select---", ""));
                ddlRegion.SelectedIndex = 1;
            }
            else
            {
                ddlRegion.Items.Clear();
                ddlRegion.Items.Insert(0, new ListItem("---Select---", ""));
            }

            ddlLabs.Items.Insert(0, new ListItem("---Select---", ""));
            ddlUnit.Items.Insert(0, new ListItem("---Select---", ""));

            ClsCommon.BindComboWithSQL(ddlRole, @"SELECT  RoleCode , Description  FROM  Role  WHERE IsActive = 1", true, "---Select---", "Description", "RoleCode");
            
        }

        private void ClearFields()
        {
            txtUserFirstName.Text = string.Empty;
            txtUserLastName.Text = string.Empty;
            Email.Text = string.Empty;
            //ddlFacility.Text = string.Empty;
            ddlRole.Text = string.Empty;
            NewPassword.Text = string.Empty;
            ConfirmNewPassword.Text = string.Empty;
            CheckBox1.Checked = false;
        }

        private bool validation()
        {
            return false;
        }

        protected void CreateUserButton_Click(object sender, EventArgs e)
        {
            string err = string.Empty;
            ErrorMessage.Text = string.Empty;
            SuccessMessage.Text = string.Empty;
            try
            {

                if (Page.IsValid)
                {
                    USERS user = new Portal.Provider.Model.USERS();
                    MembershipCreateStatus status;
                    user.Lab = HdnLabValues.Value;
                    user.FirstName = txtUserFirstName.Text;
                    user.LastName = txtUserLastName.Text;
                    if (ClsSecurityManage.isDuplicateEmail(Email.Text))
                    {
                        user.Email = Email.Text;
                    }
                    else
                    {
                        Email.Text = string.Empty;
                        error_alert.Visible = true;
                        ErrorMessage.Text = "Duplicate Email Address. Please choose another email address";
                        return;
                    }
                    string test = ddlLabs.SelectedValue;
                    user.UserName = txtUserName.Text;
                    user.Occupation = TxtOccupation.Text;

                    user.RoleCode = ddlRole.Text;
                    user.RegionCode = ddlRegion.Text;
                    user.FacilityCode = "";

                    user.Password = NewPassword.Text;
                    user.IsLockedOut = CheckBox1.Checked.ToString();
                    user.UnitCode = ddlUnit.SelectedValue;
                    user.DisplayName = txtDisplayName.Text;
                    user.IsAuditor = chkAuditor.Checked.ToString();
                    user.IsActive = chkIsActive.Checked.ToString();
                    user.Phone = txtPhoneNo.Text;
                    objSecurityManage.CreateCustomUser(user, out status);
                    if (MembershipCreateStatus.Success == status)
                    {
                        if (chksendmail.Checked)
                        {
                            string fpath = Server.MapPath("Email_Format_UserRegistor.txt");
                            ClsCommon.SendMailtoCustomerAfterUserCreation(user.Password, user.Email, fpath,user.UserName);

                            var userInserted = ClsSystem.GetUserByEmail(user.Email);

                            // Audit Trail
                            SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                            {
                                Description = JsonConvert.SerializeObject(userInserted),

                                Action = LogAction.Add.Value,
                                Module = "CreateCustomUser",
                                ModuleID = Convert.ToInt32(userInserted.User_ID),
                                TableName = LogTable.Users.Value,

                                UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                                UserName = $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                                Email = BUSessionUtility.BUSessionContainer.Email,
                                UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                                UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                            });
                        }
                        ClearFields();

                        ErrorMessage.Text = string.Empty;
                        success_alert.Visible = true;
                        SuccessMessage.Text = "User Created Successfully";
                        Response.Redirect("~/Account/ListOfUser.aspx");

                    }
                    else if (MembershipCreateStatus.DuplicateUserName == status)
                    {
                        error_alert.Visible = true;
                        ErrorMessage.Text = "User id Already Exist. Try with another User id!!";
                    }
                    else if (MembershipCreateStatus.ProviderError == status)
                    {
                        error_alert.Visible = true;
                        ErrorMessage.Text = "User id Already Exist Or Violated the Rules";
                    }

                }
               
            }
            catch (Exception ex)
            {
                ClearFields();
                error_alert.Visible = true;
                ErrorMessage.Text = ex.Message;// ConfigurationManager.AppSettings["ErrorMsg"].ToString();
                throw ex;
            }
        }

        //protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlFacility.SelectedIndex > 0)
        //    {
        //        ClsCommon.BindComboWithSQL(ddlUnit, @"SELECT  CASE 
        //          WHEN IsActive = '1'
        //             THEN Convert(nvarchar, Description) + '-' + Convert(nvarchar, isnull(DescriptionLong,'')) 
        //          ELSE Convert(nvarchar, Description) + '-' + Convert(nvarchar, isnull(DescriptionLong,'')) + ' (Not Active)'
        //    END AS Unit_Name_Description_Long,UnitCode FROM  DimUnit u where u.FacilityCode='" + ddlFacility.Text + "' ORDER BY Description", true, "---Select---", "Unit_Name_Description_Long", "UnitCode");
        //    }
        //    else
        //    {
        //        ddlUnit.Items.Clear();
        //        ddlUnit.Items.Insert(0, new ListItem("---Select---", ""));
        //       // ddlUnit.SelectedIndex = 0;
        //    }
        //}

        protected void genaratePassword()
        {
            int length_simple = 3;
            int length_capital = 3;
            int length_numbers = 2;
            int length_cahrs = 2;

            const string valid_simple = "abcdefghijklmnopqrstuvwxyz";
            const string valid_capital = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string valid_numbers = "0123456789";
            const string valid_chars = "!@#$%^&*?<>";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length_simple--)
            {
                res.Append(valid_simple[rnd.Next(valid_simple.Length)]);
            }
            while (0 < length_capital--)
            {
                res.Append(valid_capital[rnd.Next(valid_capital.Length)]);
            }
            while (0 < length_numbers--)
            {
                res.Append(valid_numbers[rnd.Next(valid_numbers.Length)]);
            }
            while (0 < length_cahrs--)
            {
                res.Append(valid_chars[rnd.Next(valid_chars.Length)]);
            }
            string password = "Pass" + res.ToString();


            NewPassword.Text = password;
            NewPassword.Attributes["type"] = "password";

            ConfirmNewPassword.Text = password;
            ConfirmNewPassword.Attributes["type"] = "password";
        }

        //protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlRegion.SelectedIndex > 0)
        //    {
        //        //    ClsCommon.BindComboWithSQL(ddlFacility, @"SELECT  CASE 
        //        //      WHEN IsActive = '1'
        //        //         THEN [Description] 
        //        //      ELSE [Description] + ' (Not Active)'
        //        //END AS Description,FacilityCode FROM  DimFacility WHERE RegionCode='" + ddlRegion.Text + "' ORDER BY Description", true, "", "Description", "FacilityCode");

        //        //Bind Facility dropdown
        //        DataTable table = ClsSystem.GetFacilityByRegion(Convert.ToInt32(ddlRegion.Text));
        //        table.AsEnumerable().Where(r => r.Field<bool>("IsActive") == false).ToList().ForEach(row => row.Delete());
        //        table.DefaultView.Sort = "Description ASC";
        //        table = table.DefaultView.ToTable();
        //        table.AcceptChanges();

        //        ddlFacility.DataSource = table;
        //        ddlFacility.DataTextField = "Description";
        //        ddlFacility.DataValueField = "FacilityCode";
        //        ddlFacility.DataBind();

        //    }
        //    else
        //    {
        //      //  ddlFacility.SelectedIndex = 0;
        //        ddlUnit.SelectedIndex = 0;

        //    }
        //}

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRegion.SelectedIndex > 0)
            {
                //    ClsCommon.BindComboWithSQL(ddlFacility, @"SELECT  CASE 
                //      WHEN IsActive = '1'
                //         THEN [Description] 
                //      ELSE [Description] + ' (Not Active)'
                //END AS Description,FacilityCode FROM  DimFacility WHERE RegionCode='" + ddlRegion.Text + "' ORDER BY Description", true, "", "Description", "FacilityCode");

                //Bind Facility dropdown
                DataTable table = ClsSystem.GetFacilityByRegion(Convert.ToInt32(ddlRegion.Text));
                table.AsEnumerable().Where(r => r.Field<bool>("IsActive") == false).ToList().ForEach(row => row.Delete());
                table.DefaultView.Sort = "Description ASC";
                table = table.DefaultView.ToTable();
                table.AcceptChanges();

                ddlLabs.DataSource = table;
                ddlLabs.DataTextField = "Description";
                ddlLabs.DataValueField = "FacilityCode";
                ddlLabs.DataBind();

            }
            else
            {
                ddlLabs.Items.Clear();
                //ddlFacility.Items.Insert(0, new ListItem("---Select---", ""));

            }

            ddlUnit.Items.Clear();
            //ddlUnit.Items.Insert(0, new ListItem("---Select---", ""));

        }

        [WebMethod]
        public static bool CheckUsernameAvailability(string username)
        {
            bool available = false;
            try
            {
                available = ClsSystem.CheckUsernameIsTaken(username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return available;
        }
        [WebMethod]
        public static bool CheckEmailAvailability(string email)
        {
            bool available = false;
            try
            {
                available = ClsSecurityManage.isDuplicateEmail(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return available;
        }
    }
}