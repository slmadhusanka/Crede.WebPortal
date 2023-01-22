using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BIZ;
using Portal.Utility;

namespace WebPortal.SystemPages
{
    public partial class AddAuditType : System.Web.UI.Page
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
                //Convert.ToString(Guid.NewGuid());
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
                    bool result = ClsSystem.CreateAuditType(txtAuditType.Text.Trim(), (string.IsNullOrWhiteSpace(txtSortOredr.Text.Trim()) ? 0 : Convert.ToInt32(txtSortOredr.Text.Trim())), cbIsActive.Checked);
                    if (result == true)
                    {
                        Response.Redirect("~/SystemPages/ListOfAuditTypes.aspx");
                    }
                    else
                    {
                        error_alert.Visible = true;
                        ErrorMessage.Text = ConfigurationManager.AppSettings["ErrorMsg"].ToString();
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
    }
}