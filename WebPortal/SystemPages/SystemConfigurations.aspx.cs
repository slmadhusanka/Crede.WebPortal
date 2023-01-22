using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Utility;
using Portal.Model;
using Portal.BIZ;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using Newtonsoft.Json;

namespace WebPortal.SystemPages
{
    public partial class SystemConfigurations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG == "1")
            {
                Response.Redirect("~/Account/ChangePassword.aspx");
            }
            //success_alert.Visible = false;
            //error_alert.Visible = false;

            try
            {
                if (!IsPostBack)
                {
                    CheckPermission();
                    ClsCommon.setFocusCurrentTabControl(Master, "liMobileDeviceMantenance");
                    //SetData();
                    BindControlls();
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

        protected void BindControlls()
        {
            //Bind user roles dropdown
            DataTable table = ClsSystem.GetAllUserRolesAsDataSet();
            table.AsEnumerable().Where(r => r.Field<bool>("IsActive") == false).ToList().ForEach(row => row.Delete());
            table.DefaultView.Sort = "Description ASC";
            table = table.DefaultView.ToTable();
            table.AcceptChanges();

            ddl_roles.DataSource = table;
            ddl_roles.DataTextField = "Description";
            ddl_roles.DataValueField = "RoleCode";
            ddl_roles.DataBind();
        }
        //private void SetData()
        //{
        //    try
        //    {
        //        SystemConfiguration objSystemConfiguration = ClsSystem.GetSystemConfiguration();
        //        if (objSystemConfiguration != null)
        //        {
        //            lblsystemconfigid.Text = Convert.ToString(objSystemConfiguration.ConfigurationID);
        //            txtauditduration.Text = Convert.ToString(objSystemConfiguration.AuditDuration);
        //            txtadditionaltime.Text = objSystemConfiguration.AdditionalTime;
        //            txtminobservation.Text = Convert.ToString(objSystemConfiguration.MinObservation);
        //            txtobservationperhcw.Text = Convert.ToString(objSystemConfiguration.MinObservationPerHCW);
        //            txtMaxobservationperhcw.Text = Convert.ToString(objSystemConfiguration.MaxObservationPerHCW);
        //            txtresultimeduration.Text = objSystemConfiguration.ResultTimerDuration;
        //            txtMinHCWObservation.Text = Convert.ToString(objSystemConfiguration.MinHCWObservation);

        //            chkenableresulttimer.Checked = objSystemConfiguration.EnableResultTimer;
        //            if (chkenableresulttimer.Checked)
        //            {
        //                txtresultimeduration.Enabled = true;
        //            }
        //            chkEnaglePPE.Checked = objSystemConfiguration.EnablePPE;
        //            if (chkEnaglePPE.Checked)
        //            {
        //                chkEnaglePPE.Enabled = true;
        //            }

        //            chkEnaglePrecaution.Checked = objSystemConfiguration.EnablePrecautions;
        //            if (chkEnaglePrecaution.Checked)
        //            {
        //                chkEnaglePrecaution.Enabled = true;
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
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

        //protected void UpdateUserButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (chkenableresulttimer.Checked)
        //        {
        //            if (txtresultimeduration.Text.Trim().Length == 0)
        //            {
        //                string strDisAbleBackButton;
        //                strDisAbleBackButton = "<SCRIPT language=javascript>\n";
        //                strDisAbleBackButton += "alert('Enter Result time duration'); \n";
        //                strDisAbleBackButton += "\n</SCRIPT>";
        //                Page.RegisterStartupScript("clientScript", strDisAbleBackButton);
        //                txtresultimeduration.Enabled = true;
        //            }
        //            else
        //            {
        //                SuccessMessage.Text = string.Empty;
        //                ErrorMessage.Text = string.Empty;

        //                if (!string.IsNullOrEmpty(lblsystemconfigid.Text))
        //                {
        //                    /*int AuditLimit = 0;
        //                    if (chkReviewLimit.Checked) 
        //                    {
        //                        AuditLimit = Convert.ToInt32(txtReviewLimit.Text.Trim());
        //                    }*/
        //                    ClsSystem.UpdateSystemConfiguration(Convert.ToInt32(lblsystemconfigid.Text.Trim()),
        //                        Convert.ToInt32(txtauditduration.Text.Trim()), txtadditionaltime.Text.Trim(),
        //                        Convert.ToInt32(txtMinHCWObservation.Text.Trim()), Convert.ToInt32(txtobservationperhcw.Text.Trim()), Convert.ToInt32(txtMaxobservationperhcw.Text.Trim()),
        //                        chkenableresulttimer.Checked, txtresultimeduration.Text.Trim(), Convert.ToInt32(txtminobservation.Text.Trim()), chkEnaglePPE.Checked, chkEnaglePrecaution.Checked);
        //                    /*ClsSystem.UpdateSystemConfiguration(Convert.ToInt32(lblsystemconfigid.Text.Trim()),
        //                        Convert.ToInt32(txtauditduration.Text.Trim()), txtadditionaltime.Text.Trim(),
        //                        Convert.ToInt32(txtMinHCWObservation.Text.Trim()), Convert.ToInt32(txtobservationperhcw.Text.Trim()), Convert.ToInt32(txtMaxobservationperhcw.Text.Trim()),
        //                        chkenableresulttimer.Checked, txtresultimeduration.Text.Trim(), Convert.ToInt32(txtminobservation.Text.Trim()), AuditLimit, chkReviewLimit.Checked);*/

        //                    //ClsCommon.setRecordUpdateStatusInDb("SystemConfiguration", "SystemConfigurationID", "Edited", lblsystemconfigid.Text);
        //                    //SuccessMessage.Text = "DimFacility Updated Successfully";
        //                    Session["SystemConfigurationData"] = null;

        //                }
        //                else
        //                {
        //                    error_alert.Visible = true;
        //                    ErrorMessage.Text = "The Configuration ID is not Selected for Update";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            SuccessMessage.Text = string.Empty;
        //            ErrorMessage.Text = string.Empty;

        //            if (!string.IsNullOrEmpty(lblsystemconfigid.Text))
        //            {

        //                ClsSystem.UpdateSystemConfiguration(Convert.ToInt32(lblsystemconfigid.Text.Trim()),
        //                    Convert.ToInt32(txtauditduration.Text.Trim()), txtadditionaltime.Text.Trim(),
        //                    Convert.ToInt32(txtMinHCWObservation.Text.Trim()), Convert.ToInt32(txtobservationperhcw.Text.Trim()), Convert.ToInt32(txtMaxobservationperhcw.Text.Trim()),
        //                    chkenableresulttimer.Checked, txtresultimeduration.Text.Trim(), Convert.ToInt32(txtminobservation.Text.Trim()), chkEnaglePPE.Checked, chkEnaglePrecaution.Checked);


        //                Session["SystemConfigurationData"] = null;

        //            }
        //            else
        //            {
        //                error_alert.Visible = true;
        //                ErrorMessage.Text = "The Configuration ID is not Selected for Update";
        //            }
        //        }

        //    }
        //    catch (System.Threading.ThreadAbortException)
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        string Type = "Error";
        //        string system = string.Empty;
        //        string program = "Editing SystemConfiguration";
        //        string Severity = "Medium";
        //        string Message = ex.Message;
        //        string User_id = BUSessionUtility.BUSessionContainer.USER_ID;
        //        //ClsSecurityManage.CreateErrorLog(Type, system, program, Severity, Message, User_id);
        //        error_alert.Visible = true;
        //        ErrorMessage.Text = Message;
        //        throw ex;
        //    }
        //}

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static string[] GetData()
        {
            string[] result = new string[3];

            DataSet ds = new DataSet();

            try
            {

                ds = ClsSystem.GetSystemSetting();
                List<int> user_roles = new List<int>();
                DataTable tbl_mfa_activated = ClsSystem.GetCurrentActiveMFA();
                if (tbl_mfa_activated != null && tbl_mfa_activated.Rows.Count > 0)
                {
                    user_roles = tbl_mfa_activated.AsEnumerable().Select(x => Convert.ToInt32(x[0])).ToList();
                }
                result[0] = JsonConvert.SerializeObject(ds.Tables[0], Formatting.None);
                //result[1] = JsonConvert.SerializeObject(ds.Tables[1], Formatting.None);
                result[2] = JsonConvert.SerializeObject(user_roles, Formatting.None);

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;


        }
        [WebMethod]
        public static bool SaveData(object[] settings)
        {
            bool success = false;

            try
            {
                DataTable tbl_settings = ConvertObjectArrayToDataTable(settings);
                tbl_settings.Columns.Add("UserID", typeof(Int32), BUSessionUtility.BUSessionContainer.USER_ID);
                success = ClsSystem.SaveSystemSetting(tbl_settings);
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return success;
        }

        public static DataTable ConvertObjectArrayToDataTable(object[] objArray)
        {
            DataTable table = new DataTable();
            try
            {
                string jsonString = JsonConvert.SerializeObject(objArray, Formatting.None);
                table = JsonConvert.DeserializeObject<DataTable>(jsonString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return table;
        }
       
        [WebMethod]
        public static bool UpdateMFA(string[] user_roles)
        {
            bool result = false;
            try
            {
                //we have to decide what is the mfa is enabled and not enabled user roles
                DataTable tbl_roles = ClsSystem.GetAllUserRolesAsDataSet();
                tbl_roles.AsEnumerable().Where(r => r.Field<bool>("IsActive") == false).ToList().ForEach(row => row.Delete());
                tbl_roles = tbl_roles.DefaultView.ToTable();
                tbl_roles.AcceptChanges();

                List<int> role_codes = new List<int>();
                if (tbl_roles != null && tbl_roles.Rows.Count > 0)
                {
                    role_codes = tbl_roles.AsEnumerable().Select(x => Convert.ToInt32(x[0])).ToList();
                }

                List<int> updated_roles = new List<int>();
                if (user_roles.Any())
                {
                    updated_roles = user_roles.Select(Int32.Parse).ToList();
                }

                DataTable tbl_mfa = update_mfa_table(role_codes, updated_roles);
                result = ClsSystem.UpdateMFA(tbl_mfa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static DataTable update_mfa_table(List<int> role_list, List<int> mfa_list)
        {
            DataTable tbl_mfa = new DataTable();
            try
            {
                tbl_mfa.Columns.Add("FK_RoleCode", typeof(int));
                tbl_mfa.Columns.Add("MFA_Enabled", typeof(bool));
                foreach (int RoleCode in role_list)
                {
                    DataRow dr_mfa = tbl_mfa.NewRow();
                    dr_mfa["FK_RoleCode"] = RoleCode;
                    dr_mfa["MFA_Enabled"] = mfa_list.Contains(RoleCode);
                    tbl_mfa.Rows.Add(dr_mfa);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tbl_mfa;
        }
    }
}