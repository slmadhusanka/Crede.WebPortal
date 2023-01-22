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
    public partial class UpdateProgram : System.Web.UI.Page
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
                    if (Session["ProgramData"] != null)
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

                        Program objProgram = (Program)Session["ProgramData"];
                        lblProgramCode.Text = Convert.ToString(objProgram.Program_Code);
                        txtProgramDetails.Text = objProgram.Program_Desc;
                        ddlProgrammeType.Text = objProgram.Program_Type_Code;
                        chkIsActive.Checked = Convert.ToBoolean(objProgram.IsActive);
                        txtDescriptionShort.Text = objProgram.DescriptionShort;
                    }
                    else
                    {
                        Response.Redirect("~/SystemPages/ListOfProgram.aspx");
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
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "ProgramEdit"))
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

        private void BindCombo()
        {
            ClsCommon.BindComboWithSQL(ddlProgrammeType, @"SELECT ProgramTypeCode,Description FROM  ProgramType ORDER BY Description ", true, string.Empty, "Description", "ProgramTypeCode");
        }

        protected void UpdateUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    SuccessMessage.Text = string.Empty;
                    ErrorMessage.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblProgramCode.Text) && !string.IsNullOrEmpty(txtProgramDetails.Text) && !string.IsNullOrEmpty(ddlProgrammeType.Text))
                    {
                        ClsSystem.UpdateProgram(lblProgramCode.Text, txtProgramDetails.Text.Trim().Replace("'", "''"), ddlProgrammeType.Text, chkIsActive.Checked.ToString(), txtDescriptionShort.Text.Trim().Replace("'", "''"));
                        //ClsCommon.setRecordUpdateStatusInDb("Program", "ProgramCode", "Edited", lblProgramCode.Text);
                        success_alert.Visible = true;
                        SuccessMessage.Text = "Program Updated Successfully";
                        Session["ProgramData"] = null;
                        txtProgramDetails.Text = string.Empty;
                        Response.Redirect("~/SystemPages/ListOfProgram.aspx");
                    }
                    else
                    {
                        error_alert.Visible = true;
                        ErrorMessage.Text = "The Program Code is not Selected for Update!!";
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
                string program = "Editing Program";
                string Severity = "Medium";
                string Message = ex.Message;
                string User_id = BUSessionUtility.BUSessionContainer.USER_ID;
                ClsSecurityManage.CreateErrorLog(Type, system, program, Severity, Message, User_id);
                error_alert.Visible = true;
                ErrorMessage.Text = Message;
                throw ex;
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            //txtProgramDetails.Text = string.Empty;
            Session["ProgramData"] = null;
            Response.Redirect("~/SystemPages/ListOfProgram.aspx");
        }
    }
}