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
    public partial class UpdateProgramType : System.Web.UI.Page
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

                    if (Session["ProgramTypeData"] != null)
                    {
                        ClsCommon.setFocusCurrentTabControl(Master, "liTableMaintainance");

                        ProgramType objProgramType = (ProgramType)Session["ProgramTypeData"];
                        lblProgramTypeCode.Text = objProgramType.ProgramTypeCode;
                        txtProgramTypeDescription.Text = objProgramType.Description;
                        chkIsActive.Checked = Convert.ToBoolean(objProgramType.IsActive);
                        txtDescriptionShort.Text = objProgramType.DescriptionShort;
                    }
                    else
                    {
                        Response.Redirect("~/SystemPages/ListOfProgramType.aspx");
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
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "ProgramTypeEdit"))
                    Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString()
                        , true);
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
                SuccessMessage.Text = string.Empty;
                ErrorMessage.Text = string.Empty;

                if (!string.IsNullOrEmpty(lblProgramTypeCode.Text) && !string.IsNullOrEmpty(txtProgramTypeDescription.Text))
                {
                    ClsSystem.UpdateProgramType(lblProgramTypeCode.Text, txtProgramTypeDescription.Text.Trim().Replace("'", "''"), chkIsActive.Checked.ToString(), txtDescriptionShort.Text.Trim().Replace("'", "''"));
                    //ClsCommon.setRecordUpdateStatusInDb("ProgramType", "ProgramTypCode", "Edited", lblProgramTypeCode.Text);
                    lblProgramTypeCode.Text = string.Empty;
                    txtProgramTypeDescription.Text = string.Empty;
                    success_alert.Visible = true;
                    SuccessMessage.Text = "Program Type Updated Successfully";
                    Session["ProgramTypeData"] = null;
                    Response.Redirect("~/SystemPages/ListOfProgramType.aspx");

                }
                else
                {
                    error_alert.Visible = true;
                    ErrorMessage.Text = "The Program  Type Code is not Selected for Update";
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
                string program = "Editing Program Type";
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
            Session["ProgramTypeData"] = null;
            Response.Redirect("~/SystemPages/ListOfProgramType.aspx");
        }
    }
}