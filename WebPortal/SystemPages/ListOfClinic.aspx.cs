using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BIZ;
using Portal.Model;
using Portal.Utility;

namespace WebPortal.SystemPages
{
    public partial class ListOfFacilityM : System.Web.UI.Page
    {
        public static bool IsDeleteAllowed;
        public static bool IsEditAllowed;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG == "1")
            {
                Response.Redirect("~/Account/ChangePassword.aspx");
            }

            error_alert.Visible = false;
            CheckPermission();
            if (!IsPostBack)
            {
                ClsCommon.setFocusCurrentTabControl(Master, "liTableMaintainance");
                LoadGrid();
            }
        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                List<ModulePermission> lstModulePermission = ClsSystem.GetPermission(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "Facility");
                if (lstModulePermission != null && lstModulePermission.Count > 0)
                {
                    var Facility = lstModulePermission.Where(x => x.ModuleKey == "Facility").FirstOrDefault();
                    if (!((Facility != null) ? Facility.IsActive : false))
                        Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
                    else
                    {
                        var FacilityAdd = lstModulePermission.Where(x => x.ModuleKey == "FacilityAdd").FirstOrDefault();
                        btnAddNew.Visible = (FacilityAdd != null) ? FacilityAdd.IsActive : false;

                        var FacilityEdit = lstModulePermission.Where(x => x.ModuleKey == "FacilityEdit").FirstOrDefault();
                        hdnIsEditAllowed.Value = (FacilityEdit != null) ? Convert.ToString(FacilityEdit.IsActive) : "false";

                        var FacilityDelete = lstModulePermission.Where(x => x.ModuleKey == "FacilityDelete").FirstOrDefault();
                        hdnIsDeleteAllowed.Value = (FacilityDelete != null) ? Convert.ToString(FacilityDelete.IsActive) : "false";
                    }
                }
                IsDeleteAllowed = Convert.ToBoolean(hdnIsDeleteAllowed.Value);
                IsEditAllowed = Convert.ToBoolean(hdnIsEditAllowed.Value);
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

        private void LoadGrid()
        {
            try
            {
                List<DimFacility> LIST_OBJ_DimFacility = new List<DimFacility>();
                LIST_OBJ_DimFacility = ClsSystem.GetAllDimFacilityList();
                //DataView dv = new DataView();
                if (LIST_OBJ_DimFacility.Count != 0)
                {
                    BUSessionUtility.BUSessionContainer.OBJ_CLASS1 = LIST_OBJ_DimFacility;
                    gvDimFacility.DataSource = LIST_OBJ_DimFacility;
                    gvDimFacility.DataBind();
                }
                else
                {
                    gvDimFacility.DataSource = string.Empty.ToList();
                    gvDimFacility.DataBind();
                }

                MakeAccessible(gvDimFacility);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MakeAccessible(GridView grid)
        {
            //This replaces <td> with <th> and adds the scope attribute 
            grid.UseAccessibleHeader = true;

            //This will add the <thead> and <tbody> elements
            grid.HeaderRow.TableSection = TableRowSection.TableHeader;

            //This adds the <tfoot> element. Remove if you don't have a footer row 
            //grid.HeaderRow.TableSection = TableRowSection.TableFooter;
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SystemPages/AddClinic.aspx");
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void gvDimFacility_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.Header)
            {
                foreach (System.Web.UI.WebControls.DataControlFieldCell cell in e.Row.Cells)
                {
                    if (cell.ContainingField.AccessibleHeaderText == "Edit")
                        cell.Visible = IsEditAllowed;

                    if (cell.ContainingField.AccessibleHeaderText == "Select")
                        cell.Visible = IsDeleteAllowed;
                }
            }

            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {

                foreach (System.Web.UI.WebControls.DataControlFieldCell cell in e.Row.Cells)
                {
                    if (cell.ContainingField.AccessibleHeaderText == "Select")
                    {
                        cell.ContainingField.Visible = IsDeleteAllowed;
                    }
                    if (cell.ContainingField.AccessibleHeaderText == "Edit")
                    {
                        cell.ContainingField.Visible = IsEditAllowed;
                    }
                }
            }
        }

        //Detele Operation
        [WebMethod]
        public static int DeleteFacility(string id)
        {
            int returnVal = 0;
            bool ReporcesingLogRefarance = ClsSystem.getCountFromReporcesingLogExistingLabDetails(id);
            bool UserRefarance = ClsSystem.getCountFromUserExistingLabDetails(id);
            bool EquipmentRefarance = ClsSystem.getCountFromEquipmentLogExistingLabDetails(id);

            if (ReporcesingLogRefarance.Equals(true) & UserRefarance.Equals(true)& EquipmentRefarance.Equals(true))
            {
                //must return message to (front end )inform about refarance details
                returnVal = 2;
            }
            else if (ReporcesingLogRefarance.Equals(false) & UserRefarance.Equals(true) & EquipmentRefarance.Equals(true))
            {
                //must return message to (front end )inform about refarance details
                returnVal = 3;
            }
            else if (ReporcesingLogRefarance.Equals(true) & UserRefarance.Equals(false) & EquipmentRefarance.Equals(true))
            {
                //must return message to (front end )inform about refarance details
                returnVal = 4;
            }
            else if (ReporcesingLogRefarance.Equals(true) & UserRefarance.Equals(true) & EquipmentRefarance.Equals(false))
            {
                //must return message to (front end )inform about refarance details
                returnVal = 5;
            }
            else if (ReporcesingLogRefarance.Equals(false) & UserRefarance.Equals(false) & EquipmentRefarance.Equals(true))
            {
                //must return message to (front end )inform about refarance details
                returnVal = 6;
            }
            else if (ReporcesingLogRefarance.Equals(true) & UserRefarance.Equals(false) & EquipmentRefarance.Equals(false))
            {
                //must return message to (front end )inform about refarance details
                returnVal = 8;
            }
            else if (ReporcesingLogRefarance.Equals(false) & UserRefarance.Equals(true) & EquipmentRefarance.Equals(false))
            {
                //must return message to (front end )inform about refarance details
                returnVal = 7;
            }
            else {

                try
                {
                    bool x = ClsSystem.DeleteDimFacility(id);
                    if (x.Equals(true))
                    {
                        returnVal = 1;
                    }
                    else {
                        returnVal = 0;
                    }
                }
                catch (Exception ex)
                {
                    returnVal = 0;
                    throw ex;
                }

            }

            return returnVal;
        }

        protected void gvDimFacility_RowEditing(object sender, GridViewEditEventArgs e)
        {
            #region Set data in Session for editing (Author: Grishma)
            try
            {
                Session["FacilityData"] = null;

                if (!string.IsNullOrEmpty(gvDimFacility.Rows[e.NewEditIndex].Cells[0].Text))
                {
                    DimFacility objDimFacility = new DimFacility();
                    objDimFacility.FacilityCode = Convert.ToInt32(gvDimFacility.Rows[e.NewEditIndex].Cells[0].Text);
                    objDimFacility.Description = gvDimFacility.Rows[e.NewEditIndex].Cells[1].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
                    objDimFacility.FacilityTypeCode = Convert.ToInt32(gvDimFacility.Rows[e.NewEditIndex].Cells[2].Text);
                    objDimFacility.RegionCode = Convert.ToInt32(gvDimFacility.Rows[e.NewEditIndex].Cells[4].Text);
                    objDimFacility.DescriptionLong = gvDimFacility.Rows[e.NewEditIndex].Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
                    objDimFacility.DescriptionShort = gvDimFacility.Rows[e.NewEditIndex].Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
                    if (gvDimFacility.Rows[e.NewEditIndex].Cells[9].Text == "Yes")
                        objDimFacility.IsActive = "True";
                    else
                        objDimFacility.IsActive = "False";
                    //objDimFacility.IsActive = gvDimFacility.Rows[e.NewEditIndex].Cells[9].Text;

                    Session["FacilityData"] = objDimFacility;
                    Response.Redirect("~/SystemPages/UpdateClinic.aspx");
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
        }
    }
}