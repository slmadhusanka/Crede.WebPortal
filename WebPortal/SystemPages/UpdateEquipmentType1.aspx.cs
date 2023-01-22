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
    public partial class UpdateUnitType1 : System.Web.UI.Page
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
                    if (Session["UnitType1Data"] != null)
                    {
                        ClsCommon.setFocusCurrentTabControl(Master, "liTableMaintainance");

                        UnitType1 objUnitType1 = (UnitType1)Session["UnitType1Data"];
                        lblUnitType1Code.Text = objUnitType1.UnitType1Code;
                        txtUnitType1Desc.Text = objUnitType1.Description;
                        chkIsActive.Checked = Convert.ToBoolean(objUnitType1.IsActive);
                        txtDescriptionShort.Text = objUnitType1.DescriptionShort;
                    }
                    else
                    {
                        Response.Redirect("~/SystemPages/ListOfEquipmentType1.aspx");
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
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "UnitType1Edit"))
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

                    if (!string.IsNullOrEmpty(lblUnitType1Code.Text) && !string.IsNullOrEmpty(txtUnitType1Desc.Text))
                    {
                        ClsSystem.UpdateUnitType1(lblUnitType1Code.Text, txtUnitType1Desc.Text.Trim().Replace("'", "''"), chkIsActive.Checked.ToString(), txtDescriptionShort.Text.Trim().Replace("'", "''"));
                        //ClsCommon.setRecordUpdateStatusInDb("UnitType1", "UnitType1Code", "Edited", lblUnitType1Code.Text);
                        success_alert.Visible = true;
                        SuccessMessage.Text = "Equipment Type Updated Successfully";
                        Session["UnitType1Data"] = null;
                        Response.Redirect("~/SystemPages/ListOfEquipmentType1.aspx");
                        lblUnitType1Code.Text = string.Empty;
                        txtUnitType1Desc.Text = string.Empty;
                    }
                    else
                    {
                        error_alert.Visible = true;
                        ErrorMessage.Text = "The Unit Type1 Code is not Selected for Update";
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
                string program = "Editing Unit Type 1";
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
            Session["UnitType1Data"] = null;
            Response.Redirect("~/SystemPages/ListOfEquipmentType1.aspx");
        }
    }
}