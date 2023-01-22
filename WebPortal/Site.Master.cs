using Portal.BIZ;
using Portal.Model;
using Portal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string title = BUSessionUtility.BUSessionContainer.CompanyName + " Clinic Portal";
            Page.Title = title;

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {

                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
                return;
            }
            if (string.IsNullOrEmpty(BUSessionUtility.BUSessionContainer.USER_ID))
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
                return;
            }

            CheckPermission();

            lblname.Text = BUSessionUtility.BUSessionContainer.FirstName + " " + BUSessionUtility.BUSessionContainer.LastName + " (" + BUSessionUtility.BUSessionContainer.USER_ID + ")";
            SetVariableSessionTimeOut();
            CheckSessionsTimeout();
        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                List<ModulePermission> lstModulePermission = ClsSystem.GetMenuPermission(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER));
                if (lstModulePermission != null && lstModulePermission.Count > 0)
                {
                    // Solution Log
                    var SolutionTestinglog = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "SolutionTestingLog");
                    li_SolutionTestinglog.Visible = SolutionTestinglog?.IsActive ?? false;

                    // Reprocessing Log
                    var Reprocessinglog = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "ReprocessingLog");
                    li_Reprocessinglog.Visible = Reprocessinglog?.IsActive ?? false;

                    //logreports
                    var logreport = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "LogReports");
                    logreports.Visible = logreport?.IsActive ?? false;

                    var error_logreport = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "ErrorLog");
                    errorlogreport.Visible = error_logreport?.IsActive ?? false;

                    var assistance_logreport = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "NeedAssistanceLog");
                    assistancelogreport.Visible = assistance_logreport?.IsActive ?? false;

                    //System configuration
                    var SystemConfigurations = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "SystemConfiguration");
                    lisystemconfiguration.Visible = SystemConfigurations?.IsActive ?? false;

                    var Configurations = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "Configurations");
                    liConfiguration.Visible = Configurations?.IsActive ?? false;

                    var Permissions = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "Permission");
                    liPermission.Visible = Permissions?.IsActive ?? false;

                    var Roles = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "Role");
                    liRole.Visible = Roles?.IsActive ?? false;

                    //Labs & Equipment

                
                    var FacilityUnit = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "FacilityUnit");
                    li_header_information.Visible = FacilityUnit?.IsActive ?? false;

                    var Zone = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "Zone");
                    liZone.Visible = Zone?.IsActive ?? false;

                    var Facility = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "Facility");
                    liFacility.Visible = Facility?.IsActive ?? false;

                    var Unit = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "Unit");
                    liUnit.Visible = Unit?.IsActive ?? false;

                    
                    //Table Maintance
                    var table_Maintenance = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "SystemTables");
                    li_table_Maintenance.Visible = table_Maintenance?.IsActive ?? false;

                    var Users = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "User");
                    liUser.Visible = Users?.IsActive ?? false;

                    var Transducer = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "Transducer");
                    liTransducer.Visible = Transducer?.IsActive ?? false;

                    var LabType = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "FacilityType");
                    liLabType.Visible = LabType?.IsActive ?? false;

                    var EquType = lstModulePermission.FirstOrDefault(x => x.ModuleKey == "UnitType1");
                    liEquType.Visible = EquType?.IsActive ?? false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void CheckSessionsTimeout()
        {
            string msgSession = @" Your Session will expire in three minutes. Click on \'Extend Session\' to stay logged in.";
            //time to remind, 3 minutes before session ends0
            int int_MilliSecondsTimeReminder = (this.Session.Timeout - 3) * 60 * 1000;
            hdnReminderTime.Value = int_MilliSecondsTimeReminder.ToString();
            //time to redirect, 5 milliseconds before session ends
            int int_MilliSecondsTimeOut = (this.Session.Timeout * 60 * 1000) - 1000;
            hdnRedirectTime.Value = int_MilliSecondsTimeOut.ToString();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CheckSessionOut", "ss_init()", true);
        }
        private void SetVariableSessionTimeOut()
        {
            //if (ConfigurationManager.AppSettings.GetValues("TimeForVariableSessionTimeOut") != null)
            //{
            //    //Setting the Session.Timeout by reading from web.config file. The value in the web.config file for this key must be integer.
            //    this.Session.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings.GetValues("TimeForVariableSessionTimeOut")[0].ToString());
            //}
            //else
            //    this.Session.Timeout = 30;//Default TimeOut 20 minuite.

            if (!string.IsNullOrEmpty(BUSessionUtility.BUSessionContainer.SessionTimeOut))
            {
                //Setting the Session.Timeout by reading from web.config file. The value in the web.config file for this key must be integer.
                this.Session.Timeout = Convert.ToInt32(BUSessionUtility.BUSessionContainer.SessionTimeOut);
            }
            else
                this.Session.Timeout = 30;//Default TimeOut 20 minuite.


        }

        protected void lnlLogout_Click(object sender, EventArgs e)
        {
            ClsSecurity.Logout();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx");
        }
    }
}