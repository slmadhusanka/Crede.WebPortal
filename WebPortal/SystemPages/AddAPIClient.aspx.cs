using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BIZ;
using Portal.Utility;

namespace WebPortal.SystemPages
{
    public partial class AddAPIClient : System.Web.UI.Page
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
                txtScope.Text = "WebApi.Read";
                hdnIsSaved.Value = "";
            }
        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "AuditTypeAdd"))
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

        protected void UpdateUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    SuccessMessage.Text = string.Empty;
                    ErrorMessage.Text = string.Empty;
                    string ClientId = GenerateClientId();
                    string ClientSecret = Membership.GeneratePassword(12, 5);

                    var ClientSecretHash = "";
                    // Create a SHA256   
                    using (SHA256 shA256 = SHA256.Create())
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(ClientSecret);
                        ClientSecretHash = Convert.ToBase64String(((HashAlgorithm)shA256).ComputeHash(bytes));
                    }

                    bool result = ClsSystem.CreateAPIClient(ClientId, ClientSecretHash, txtName.Text, txtScope.Text, cbIsActive.Checked);
                    if (result == true)
                    {
                        hdnIsSaved.Value = "true";
                        hdnClientId.Value = ClientId;
                        hdnClientSecret.Value = ClientSecret;
                    }
                    else
                        ErrorMessage.Text = ConfigurationManager.AppSettings["ErrorMsg"].ToString();
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

        private string GenerateClientId()
        {
            var randmNum1 = new Random();
            string ClientId = Convert.ToString(randmNum1.Next(10000000, 99999999));

            int cnt = 0;
            cnt = ClsSystem.CheckClientId(ClientId);
            ClientId = cnt > 0 ? ClientId = GenerateClientId() : ClientId;

            return ClientId;
        }
    }
}