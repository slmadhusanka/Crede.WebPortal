using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BIZ;
using Portal.Provider;
using Portal.Utility;

namespace WebPortal.SystemPages
{
    public partial class AddFacilityM : System.Web.UI.Page
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
                ClsCommon.setFocusCurrentTabControl(Master, "liTableMaintainance");
                BindCombo();
            }
        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "FacilityAdd"))
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
            try
            {
                DataSet ds;
                ds = ClsSystem.GetAllFacilityTypeCombo();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlFacilityType.DataSource = ds.Tables[0];
                    ddlFacilityType.DataTextField = "Description";
                    ddlFacilityType.DataValueField = "FacilityTypeCode";
                    ddlFacilityType.DataBind();
                    ddlFacilityType.Items.Insert(0, new ListItem("---Select---", ""));
                }
                else
                {
                    ddlFacilityType.Items.Clear();
                    ddlFacilityType.Items.Insert(0, new ListItem("---Select---", ""));
                }


                ds = ClsSystem.GetAllDimRegion();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlRegion.DataSource = ds.Tables[0];
                    ddlRegion.DataTextField = "Description";
                    ddlRegion.DataValueField = "RegionCode";
                    ddlRegion.DataBind();
                    ddlRegion.Items.Insert(0, new ListItem("---Select---", ""));
                }
                else
                {
                    ddlRegion.Items.Clear();
                    ddlRegion.Items.Insert(0, new ListItem("---Select---", ""));
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
            //ClsCommon.BindComboWithSQL(ddlFacilityType, @"SELECT  FacilityTypeCode , Description  FROM  FacilityType ORDER BY Description ", true, string.Empty, "Description", "FacilityTypeCode");
        }

        protected void UpdateUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    SuccessMessage.Text = string.Empty;
                    ErrorMessage.Text = string.Empty;
                    ClsSystem.CreateDimFacility(txtFacilityDetails.Text.Trim(), ddlFacilityType.Text, ddlRegion.Text, txtDescriptionLong.Text.Trim(), txtDescriptionShort.Text.Trim(), chkIsActive.Checked.ToString());
                    //ClsCommon.setRecordUpdateStatusInDb("DimFacility", "FacilityCode", "Added", "");
                    Response.Redirect("~/SystemPages/ListOfClinic.aspx");

                }
                
                

            }
            catch (Exception ex)
            {
                string Type = "Error";
                string system = string.Empty;
                string program = "Adding DimFacility";
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
            //txtFacilityDetails.Text = string.Empty;
            //ddlFacilityType.Text = string.Empty;
            Response.Redirect("~/SystemPages/ListOfClinic.aspx");
        }
    }
}