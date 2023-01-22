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
    public partial class UpdateUnitType2 : System.Web.UI.Page
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

                    #region Set data for editing (Author: Grishma)
                    if (Session["UnitType2Data"] != null)
                    {
                        ClsCommon.setFocusCurrentTabControl(Master, "liTableMaintainance");

                        UnitType2 objUnitType2 = (UnitType2)Session["UnitType2Data"];
                        lblUnitType2Code.Text = objUnitType2.UnitType2Code;
                        txtUnitType2Desc.Text = objUnitType2.Description;
                        chkIsActive.Checked = Convert.ToBoolean(objUnitType2.IsActive);
                        txtDescriptionShort.Text = objUnitType2.DescriptionShort;
                    }
                    else
                    {
                        Response.Redirect("~/SystemPages/ListOfUnitType2.aspx");
                    }
                    #endregion
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
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "UnitType2Edit"))
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

                    if (!string.IsNullOrEmpty(lblUnitType2Code.Text) && !string.IsNullOrEmpty(txtUnitType2Desc.Text))
                    {
                        ClsSystem.UpdateUnitType2(lblUnitType2Code.Text, txtUnitType2Desc.Text.Trim().Replace("'", "''"), chkIsActive.Checked.ToString(), txtDescriptionShort.Text.Trim().Replace("'", "''"));
                        //ClsCommon.setRecordUpdateStatusInDb("UnitType2", "UnitType2Code", "Edited", lblUnitType2Code.Text);
                        success_alert.Visible = true;
                        SuccessMessage.Text = "Facility Type Updated Successfully";
                        Session["UnitType2Data"] = null;

                        lblUnitType2Code.Text = string.Empty;
                        txtUnitType2Desc.Text = string.Empty;
                        Response.Redirect("~/SystemPages/ListOfUnitType2.aspx");
                    }
                    else
                    {
                        error_alert.Visible = true;
                        ErrorMessage.Text = "The Facility Type Code is not Selected for Update";
                    }

                }


          
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                //ErrorMessage.Text = ex.Message;
                string Type = "Error";
                string system = string.Empty;
                string program = "Unit Type 2 Updated";
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
            Session["UnitType2Data"] = null;
            Response.Redirect("~/SystemPages/ListOfUnitType2.aspx");
        }
    }
}