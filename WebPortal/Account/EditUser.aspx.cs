using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
    public partial class EditUser : System.Web.UI.Page
    {
        USERS OBJ_USERS = new USERS();
        ClsSecurityManage ObjSecurityManage = new ClsSecurityManage();
        public static string CurrentMail;
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

                    if (Session["UserData"] != null)
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

                        BindCombo();
                        ClearMessage();
                        
                        #region Set data for editing (Author: Grishma)

                        USERS objUser = (USERS)(Session["UserData"]);

                        hdnUserID.Value = objUser.User_ID;
                        txtUserFirstName.Text = objUser.FirstName;
                        txtUserLastName.Text = objUser.LastName;
                        txtUserName.Text = objUser.UserName;
                        txtDisplayName.Text = objUser.DisplayName;
                        ddlRoleName.Text = objUser.RoleCode;
                        Email.Text = objUser.Email;
                        txtPhoneNo.Text = objUser.PhoneNumber;

                        TxtOccupation.Text = objUser.Occupation;

                        CurrentMail = objUser.Email;
                        CheckBox1.Checked = Convert.ToBoolean(objUser.IsLockedOut);
                        chkAuditor.Checked = Convert.ToBoolean(objUser.IsAuditor);
                        chkIsActive.Checked = Convert.ToBoolean(objUser.IsActive);
                        ddlRegion.Text = (objUser.RegionCode != "&nbsp;") ? objUser.RegionCode : "";


                        if (!string.IsNullOrEmpty(ddlRegion.Text))
                        {
            //                ClsCommon.BindComboWithSQL(ddlFacility, @"SELECT  CASE 
            //      WHEN IsActive = '1'
            //         THEN [Description] 
            //      ELSE [Description] + ' (Not Active)'
            //END AS Description,FacilityCode FROM  DimFacility WHERE RegionCode='" + ddlRegion.Text + "' ORDER BY Description", true, "---Select---", "Description", "FacilityCode");


                            //Bind Facility dropdown
                            DataTable table = ClsSystem.GetFacilityByRegion(Convert.ToInt32( ddlRegion.Text));
                            table.AsEnumerable().Where(r => r.Field<bool>("IsActive") == false).ToList().ForEach(row => row.Delete());
                            table.DefaultView.Sort = "Description ASC";
                            table = table.DefaultView.ToTable();
                            table.AcceptChanges();

                            ddlLabs.DataSource = table;
                            ddlLabs.DataTextField = "Description";
                            ddlLabs.DataValueField = "FacilityCode";
                            ddlLabs.DataBind();


                            //  ddlFacility.Text = (objUser.FacilityCode != "&nbsp;") ? objUser.FacilityCode : "";
                            HdnLabValues.Value = (objUser.FacilityCode != "&nbsp;") ? objUser.FacilityCode : "";                         


                            if (!string.IsNullOrEmpty(ddlLabs.Text))
                            {
                                ClsCommon.BindComboWithSQL(ddlUnit, @"SELECT  CASE 
                  WHEN IsActive = '1'
                     THEN Convert(nvarchar, Description) + '-' + Convert(nvarchar, isnull(DescriptionLong,'')) 
                  ELSE Convert(nvarchar, Description) + '-' + Convert(nvarchar, isnull(DescriptionLong,'')) + ' (Not Active)'
            END AS Unit_Name_Description_Long,UnitCode FROM  DimUnit u where u.FacilityCode='" + ddlLabs.Text + "' ORDER BY Description", true, "---Select---", "Unit_Name_Description_Long", "UnitCode");

                                ddlUnit.SelectedValue = (objUser.UnitCode != "&nbsp;") ? objUser.UnitCode : "";
                            }
                            else
                            {
                                //ddlUnit.Items.Insert(0, new ListItem("---Select---", ""));
                            }
                        }
                        else
                        {
                           // ddlLab.Items.Insert(0, new ListItem("---Select---", ""));
                           // ddlUnit.Items.Insert(0, new ListItem("---Select---", ""));
                        }


                        #endregion
                    }
                    else
                    {
                        Response.Redirect("~/Account/ListOfUser.aspx");
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
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "UserEdit"))
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

        private void ClearMessage()
        {
            ErrorMessage.Text = string.Empty;
            SuccessMessage.Text = string.Empty;
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
                //ddlRegion.Items.Insert(0, new ListItem("---Select---", ""));
            }
            else
            {
                ddlRegion.Items.Clear();
                //ddlRegion.Items.Insert(0, new ListItem("---Select---", ""));
            }

            ddlLabs.Items.Clear();
            // ClsCommon.BindComboWithSQL(ddlUserName, @"SELECT  UserID,UserID FROM  USERS ", true, string.Empty, "UserID", "UserID");

            ClsCommon.BindComboWithSQL(ddlRoleName, @"SELECT RoleCode,Description FROM Role WHERE IsActive = 1 ORDER BY Description", true, string.Empty, "Description", "RoleCode");

        }

        private USERS setObjectProperties()
        {
            OBJ_USERS.User_ID = hdnUserID.Value;
            OBJ_USERS.FirstName = txtUserFirstName.Text;
            OBJ_USERS.LastName = txtUserLastName.Text;
            OBJ_USERS.UserName = txtUserName.Text;
            OBJ_USERS.Email = Email.Text;

            OBJ_USERS.Occupation = TxtOccupation.Text;

            OBJ_USERS.FacilityCode = "";
            OBJ_USERS.RoleCode = ddlRoleName.Text;
            OBJ_USERS.Password = NewPassword.Text;
            OBJ_USERS.UnitCode = ddlUnit.SelectedValue;
            OBJ_USERS.IsLockedOut = CheckBox1.Checked.ToString();
            OBJ_USERS.DisplayName = txtDisplayName.Text.Trim();
            OBJ_USERS.IsAuditor = chkAuditor.Checked.ToString();
            OBJ_USERS.IsActive = chkIsActive.Checked.ToString();
            OBJ_USERS.RegionCode = ddlRegion.Text;
            OBJ_USERS.PhoneNumber = txtPhoneNo.Text;
            OBJ_USERS.Lab = HdnLabValues.Value;
            return OBJ_USERS;
        }

        protected void UpdateUserButton_Click(object sender, EventArgs e)
        {
            MembershipCreateStatus status;
            ClearMessage();
            try
            {

                if (IsValid)
                {

                    if (CheckMailIsDuplicate())
                    {
                        error_alert.Visible = true;
                        ErrorMessage.Text = "Duplicate Email Address. Please choose another email address";
                        return;
                    }
                    else
                    {
                        OBJ_USERS = new USERS();
                        OBJ_USERS = setObjectProperties();
                        if (string.IsNullOrEmpty(NewPassword.Text))
                        {
                            ObjSecurityManage.UpdateUserAdmin(OBJ_USERS, out status, false);

                            // Audit Trail
                            SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                            {
                                Description = JsonConvert.SerializeObject(OBJ_USERS),

                                Action = LogAction.Edit.Value,
                                Module = "UpdateUserAdmin",
                                ModuleID = Convert.ToInt32(OBJ_USERS.User_ID),
                                TableName = LogTable.Users.Value,

                                UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                                UserName = $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                                Email = BUSessionUtility.BUSessionContainer.Email,
                                UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                                UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                            });
                        }
                        else
                        {
                            ObjSecurityManage.UpdateUserAdmin(OBJ_USERS, out status, true);

                            // Audit Trail
                            SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                            {
                                Description = JsonConvert.SerializeObject(OBJ_USERS),

                                Action = LogAction.Edit.Value,
                                Module = "UpdateUserAdmin",
                                ModuleID = Convert.ToInt32(OBJ_USERS.User_ID),
                                TableName = LogTable.Users.Value,

                                UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                                UserName = $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                                Email = BUSessionUtility.BUSessionContainer.Email,
                                UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                                UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                            });
                        }

                        if (MembershipCreateStatus.Success == status)
                        {
                            ClearFields();
                            //ddlUserName.Text = string.Empty;
                            Session["UserData"] = null;
                            Response.Redirect("~/Account/ListOfUser.aspx");
                        }
                    }

                }

               
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                ClearMessage();
                string Type = "Error";
                string system = string.Empty;
                string program = "Edit User Info";
                string Severity = "Medium";
                string Message = ex.Message;
                string User_id = BUSessionUtility.BUSessionContainer.USER_ID;
                ClsSecurityManage.CreateErrorLog(Type, system, program, Severity, Message, User_id);
                error_alert.Visible = true;
                ErrorMessage.Text = Message;
                throw ex;
            }
        }

        private bool CheckMailIsDuplicate()
        {
            bool returnValue = true;
            //check original mail and edited mails are not empty
            if ((!string.IsNullOrEmpty(CurrentMail) || !string.IsNullOrWhiteSpace(CurrentMail)) && (!string.IsNullOrEmpty(Email.Text) || !string.IsNullOrWhiteSpace(Email.Text)))
            {
                //check original mail and entered mail is same
                if (CurrentMail.Trim().ToLower().Equals(Email.Text.Trim().ToLower()))
                {
                    returnValue = false;
                }
                else
                {
                    if (ClsSecurityManage.isDuplicateEmail(Email.Text))
                    {
                        returnValue = false;
                    }
                    else
                    {
                        returnValue = true;
                    }
                }
            }
            return returnValue;
        }

        private void ClearLabel()
        {
            ErrorMessage.Text = string.Empty;
            SuccessMessage.Text = string.Empty;
        }
        protected void DeleteUserButton_Click(object sender, EventArgs e)
        {
            Session["UserData"] = null;
            Response.Redirect("~/Account/ListOfUser.aspx");
        }
       
        private void FetchDataByUserID(USERS OBJ)
        {
            txtUserFirstName.Text = OBJ.FirstName;
            txtUserLastName.Text = OBJ.LastName;
            Email.Text = OBJ.Email;

            TxtOccupation.Text = OBJ.Occupation;

           // ddlLab.Text = OBJ.FacilityCode;
            ddlRoleName.Text = OBJ.RoleCode;
            ddlUnit.SelectedValue = OBJ_USERS.UnitCode;
            CheckBox1.Checked = Convert.ToBoolean(OBJ_USERS.IsLockedOut);
            ddlRegion.Text = OBJ_USERS.RegionCode;
        }
        private void ClearFields()
        {
           
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        //protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // ddlUnit.Items.Clear();
        //    if (ddlLab.SelectedIndex > 0)
        //    {
        //        ClsCommon.BindComboWithSQL(ddlUnit, @"SELECT  CASE 
        //          WHEN IsActive = '1'
        //             THEN Convert(nvarchar, Description) + '-' + Convert(nvarchar, isnull(DescriptionLong,'')) 
        //          ELSE Convert(nvarchar, Description) + '-' + Convert(nvarchar, isnull(DescriptionLong,'')) + ' (Not Active)'
        //    END AS Unit_Name_Description_Long,UnitCode FROM  DimUnit u where u.FacilityCode='" + ddlLab.Text + "' ORDER BY Description", true, "---Select---", "Unit_Name_Description_Long", "UnitCode");
        //    }
        //    else
        //    {
        //        ddlUnit.Items.Clear();
        //        //ddlUnit.Items.Insert(0, new ListItem("---Select---", ""));
        //        ddlUnit.SelectedIndex = 1;
        //    }
        //}
        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRegion.SelectedIndex > 0)
            {
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
                //ddlFacility.Items.Insert(0, new ListItem("Wentworth-Halton", ""));

               // ddlFacility.SelectedIndex = 1;
                
            }

            ddlUnit.Items.Clear();
            //ddlUnit.Items.Insert(0, new ListItem("---Select---", ""));
        }
        [WebMethod]
        public static bool CheckUsernameAvailability(int user_id, string username)
        {
            bool available = false;
            try
            {
                available = ClsSystem.CheckUsernameIsTakenOnEdit(user_id, username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return available;
        }

        [WebMethod]
        public static bool CheckEmailAvailability(int user_id, string email)
        {
            bool available = false;
            try
            {
                available = ClsSecurityManage.isDuplicateEmailOnEdit(user_id, email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return available;
        }
    }
}
