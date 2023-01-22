using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class UpdateSystemConfigurationM : System.Web.UI.Page
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
                    SetData();
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
        private void SetData()
        {
            try
            {
                SystemConfiguration objSystemConfiguration = ClsSystem.GetSystemConfiguration();
                if (objSystemConfiguration != null)
                {
                    lblsystemconfigid.Text = Convert.ToString(objSystemConfiguration.ConfigurationID);
                    txtauditduration.Text = Convert.ToString(objSystemConfiguration.AuditDuration);
                    txtadditionaltime.Text = objSystemConfiguration.AdditionalTime;
                    txtminobservation.Text = Convert.ToString(objSystemConfiguration.MinObservation);
                    txtobservationperhcw.Text = Convert.ToString(objSystemConfiguration.MinObservationPerHCW);
                    txtMaxobservationperhcw.Text = Convert.ToString(objSystemConfiguration.MaxObservationPerHCW);
                    txtresultimeduration.Text = objSystemConfiguration.ResultTimerDuration;
                    txtMinHCWObservation.Text = Convert.ToString(objSystemConfiguration.MinHCWObservation);

                    chkenableresulttimer.Checked = objSystemConfiguration.EnableResultTimer;
                    if (chkenableresulttimer.Checked)
                    {
                        txtresultimeduration.Enabled = true;
                    }
                    chkEnaglePPE.Checked = objSystemConfiguration.EnablePPE;
                    if (chkEnaglePPE.Checked)
                    {
                        chkEnaglePPE.Enabled = true;
                    }

                    chkEnaglePrecaution.Checked = objSystemConfiguration.EnablePrecautions;
                    if (chkEnaglePrecaution.Checked)
                    {
                        chkEnaglePrecaution.Enabled = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "ConfigurationEdit"))
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
                if (chkenableresulttimer.Checked)
                {
                    if (txtresultimeduration.Text.Trim().Length == 0)
                    {
                        string strDisAbleBackButton;
                        strDisAbleBackButton = "<SCRIPT language=javascript>\n";
                        strDisAbleBackButton += "alert('Enter Result time duration'); \n";
                        strDisAbleBackButton += "\n</SCRIPT>";
                        Page.RegisterStartupScript("clientScript", strDisAbleBackButton);
                        txtresultimeduration.Enabled = true;
                    }
                    else
                    {
                        SuccessMessage.Text = string.Empty;
                        ErrorMessage.Text = string.Empty;

                        if (!string.IsNullOrEmpty(lblsystemconfigid.Text))
                        {
                         
                            ClsSystem.UpdateSystemConfiguration(Convert.ToInt32(lblsystemconfigid.Text.Trim()),
                                Convert.ToInt32(txtauditduration.Text.Trim()), txtadditionaltime.Text.Trim(),
                                Convert.ToInt32(txtMinHCWObservation.Text.Trim()), Convert.ToInt32(txtobservationperhcw.Text.Trim()), Convert.ToInt32(txtMaxobservationperhcw.Text.Trim()),
                                chkenableresulttimer.Checked, txtresultimeduration.Text.Trim(), Convert.ToInt32(txtminobservation.Text.Trim()), chkEnaglePPE.Checked, chkEnaglePrecaution.Checked);
                         
                        }
                        else
                        {
                            error_alert.Visible = true;
                            ErrorMessage.Text = "The Configuration ID is not Selected for Update";
                        }
                    }
                }
                else
                {
                    SuccessMessage.Text = string.Empty;
                    ErrorMessage.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblsystemconfigid.Text))
                    {

                        ClsSystem.UpdateSystemConfiguration(Convert.ToInt32(lblsystemconfigid.Text.Trim()),
                            Convert.ToInt32(txtauditduration.Text.Trim()), txtadditionaltime.Text.Trim(),
                            Convert.ToInt32(txtMinHCWObservation.Text.Trim()), Convert.ToInt32(txtobservationperhcw.Text.Trim()), Convert.ToInt32(txtMaxobservationperhcw.Text.Trim()),
                            chkenableresulttimer.Checked, txtresultimeduration.Text.Trim(), Convert.ToInt32(txtminobservation.Text.Trim()), chkEnaglePPE.Checked, chkEnaglePrecaution.Checked);

                    }
                    else
                    {
                        error_alert.Visible = true;
                        ErrorMessage.Text = "The Configuration ID is not Selected for Update";
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
                string program = "Editing SystemConfiguration";
                string Severity = "Medium";
                string Message = ex.Message;
                string User_id = BUSessionUtility.BUSessionContainer.USER_ID;
                ClsSecurityManage.CreateErrorLog(Type, system, program, Severity, Message, User_id);
                error_alert.Visible = true;
                ErrorMessage.Text = Message;
                throw ex;
            }
        }
    }
}