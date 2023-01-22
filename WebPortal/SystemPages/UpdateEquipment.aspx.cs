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
    public partial class UpdateUnitM : System.Web.UI.Page
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
                    if (Session["UnitData"] != null)
                    {
                        ClsCommon.setFocusCurrentTabControl(Master, "liTableMaintainance");
                        BindCombo();

                        DimUnit objDimUnit = (DimUnit)Session["UnitData"];
                        lblUnitCode.Text = Convert.ToString(objDimUnit.UnitCode);
                        txtOrderID.Text = Convert.ToString(objDimUnit.OrderID);
                        //ddlProgramme.Text = Convert.ToString(objDimUnit.ProgramCode);
                        ddlFacility.Text = Convert.ToString(objDimUnit.FacilityCode);
                        ddlUnitType1.SelectedValue = Convert.ToString(objDimUnit.UnitType1Code);
                        //ddlUnitType2.SelectedValue = Convert.ToString(objDimUnit.UnitType2Code);
                        txtUnitName.Text = objDimUnit.Description;
                        txtunitdescriptionlong.Text = (objDimUnit.DescriptionLong != "&nbsp;") ? objDimUnit.DescriptionLong : null;
                        txtunitdescriptionshort.Text = (objDimUnit.DescriptionShort != "&nbsp;") ? objDimUnit.DescriptionShort : null;
                        chkIsActive.Checked = Convert.ToBoolean(objDimUnit.IsActive);
                        txtbeds.Text = Convert.ToString(objDimUnit.Beds);
                    }
                    else
                    {
                        Response.Redirect("~/SystemPages/ListOfEquipment.aspx");
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
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "UnitEdit"))
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

                    //if (!string.IsNullOrEmpty(lblUnitCode.Text) && !string.IsNullOrEmpty(ddlFacility.Text) && !string.IsNullOrEmpty(ddlProgramme.Text) && !string.IsNullOrEmpty(txtUnitName.Text))
                    if (!string.IsNullOrEmpty(lblUnitCode.Text) && !string.IsNullOrEmpty(ddlFacility.Text) && !string.IsNullOrEmpty(txtUnitName.Text))
                    {
                        //ClsSystem.UpdateDimUnit(lblUnitCode.Text.Trim(), txtOrderID.Text.Trim(),
                        //    ddlFacility.SelectedItem.Value, ddlProgramme.SelectedItem.Value, ddlUnitType1.SelectedItem.Value,
                        //    ddlUnitType2.SelectedItem.Value, txtUnitName.Text.Trim(), txtunitdescriptionlong.Text.Trim(), ddlManagers.SelectedItem.Value);

                        ClsSystem.UpdateDimUnit(lblUnitCode.Text.Trim(), txtOrderID.Text.Trim(),
                            ddlFacility.SelectedItem.Value, txtUnitName.Text.Trim(), txtunitdescriptionshort.Text.Trim(), txtunitdescriptionlong.Text.Trim(), ddlUnitType1.SelectedItem.Value, 
                            chkIsActive.Checked.ToString());

                        //ClsCommon.setRecordUpdateStatusInDb("DimUnit", "UnitCode", "Edited", lblUnitCode.Text);
                        success_alert.Visible = true;
                        SuccessMessage.Text = "Equipment Updated Successfully";
                        Session["UnitData"] = null;
                        Response.Redirect("~/SystemPages/ListOfEquipment.aspx");

                    }
                    else
                    {
                        error_alert.Visible = true;
                        ErrorMessage.Text = "The Equipment Code is not Selected for Update!!";
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
                string program = "Editing DimUnit";
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
            Session["UnitData"] = null;
            Response.Redirect("~/SystemPages/ListOfEquipment.aspx");
        }

        private void BindCombo()
        {
            try
            {
                DataSet ds;
                //ds = ClsSystem.GetAllProgram();
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    ddlProgramme.DataSource = ds.Tables[0];
                //    ddlProgramme.DataTextField = "Description";
                //    ddlProgramme.DataValueField = "ProgramCode";
                //    ddlProgramme.DataBind();
                //    ddlProgramme.Items.Insert(0, new ListItem("---Select---", ""));
                //}
                //else
                //{
                //    ddlProgramme.Items.Clear();
                //    ddlProgramme.Items.Insert(0, new ListItem("---Select---", ""));
                //}

                ds = ClsSystem.GetAllDimFacility();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlFacility.DataSource = ds.Tables[0];
                    ddlFacility.DataTextField = "Description";
                    ddlFacility.DataValueField = "FacilityCode";
                    ddlFacility.DataBind();
                    ddlFacility.Items.Insert(0, new ListItem("---Select---", ""));
                }
                else
                {
                    ddlFacility.Items.Clear();
                    ddlFacility.Items.Insert(0, new ListItem("---Select---", ""));
                }

                ds = ClsSystem.GetAllUnitType1();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlUnitType1.DataSource = ds.Tables[0];
                    ddlUnitType1.DataTextField = "Description";
                    ddlUnitType1.DataValueField = "UnitType1Code";
                    ddlUnitType1.DataBind();
                    ddlUnitType1.Items.Insert(0, new ListItem("---Select---", ""));
                }
                else
                {
                    ddlUnitType1.Items.Clear();
                    ddlUnitType1.Items.Insert(0, new ListItem("---Select---", ""));
                }

                //ds = ClsSystem.GetAllUnitType2();
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    ddlUnitType2.DataSource = ds.Tables[0];
                //    ddlUnitType2.DataTextField = "Description";
                //    ddlUnitType2.DataValueField = "UnitType2Code";
                //    ddlUnitType2.DataBind();
                //    ddlUnitType2.Items.Insert(0, new ListItem("---Select---", ""));
                //}
                //else
                //{
                //    ddlUnitType2.Items.Clear();
                //    ddlUnitType2.Items.Insert(0, new ListItem("---Select---", ""));
                //}
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //ClsCommon.BindComboWithSQL(ddlFacilityType, @"SELECT  FacilityTypeCode , Description  FROM  FacilityType ORDER BY Description ", true, string.Empty, "Description", "FacilityTypeCode");
        }
    }
}