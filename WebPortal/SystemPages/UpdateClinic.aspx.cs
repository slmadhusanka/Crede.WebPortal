using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
    public partial class UpdateFacilityM : System.Web.UI.Page
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
                    if (Session["FacilityData"] != null)
                    {
                        ClsCommon.setFocusCurrentTabControl(Master, "liTableMaintainance");
                        BindCombo();

                        DimFacility objDimFacility = (DimFacility)Session["FacilityData"];
                        lblFacilityCode.Text = Convert.ToString(objDimFacility.FacilityCode);
                        txtFacilityDetails.Text = objDimFacility.Description;
                        ddlFacilityType.Text = Convert.ToString(objDimFacility.FacilityTypeCode);
                        ddlRegion.Text = Convert.ToString(objDimFacility.RegionCode);
                        txtDescriptionLong.Text = (objDimFacility.DescriptionLong != "&nbsp;") ? objDimFacility.DescriptionLong : null;
                        txtDescriptionShort.Text = (objDimFacility.DescriptionShort != "&nbsp;") ? objDimFacility.DescriptionShort : null;
                        chkIsActive.Checked = Convert.ToBoolean(objDimFacility.IsActive);
                    }
                    else
                    {
                        Response.Redirect("~/SystemPages/ListOfClinic.aspx");
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
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "FacilityEdit"))
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["FacilityData"] = null;
            Response.Redirect("~/SystemPages/ListOfClinic.aspx");
        }

        protected void UpdateUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    SuccessMessage.Text = string.Empty;
                    ErrorMessage.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblFacilityCode.Text) && !string.IsNullOrEmpty(ddlFacilityType.Text))
                    {
                        ClsSystem.UpdateDimFacility(lblFacilityCode.Text, txtFacilityDetails.Text.Trim(), ddlFacilityType.Text, ddlRegion.Text, txtDescriptionLong.Text.Trim(), txtDescriptionShort.Text.Trim(), chkIsActive.Checked.ToString());
                        //ClsCommon.setRecordUpdateStatusInDb("DimFacility", "FacilityCode", "Edited", lblFacilityCode.Text);
                        success_alert.Visible = true;
                        SuccessMessage.Text = "Clinic updated successfully";
                        Session["FacilityData"] = null;
                        Response.Redirect("~/SystemPages/ListOfClinic.aspx");
                    }
                    else
                    {
                        error_alert.Visible = true;
                        ErrorMessage.Text = "The Clinic code is not selected for update";
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
                string program = "Editing DimFaciltiy";
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