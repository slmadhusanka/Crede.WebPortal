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
    public partial class ListOfFacilityType : System.Web.UI.Page
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
                List<ModulePermission> lstModulePermission = ClsSystem.GetPermission(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "FacilityType");
                if (lstModulePermission != null && lstModulePermission.Count > 0)
                {
                    var FacilityType = lstModulePermission.Where(x => x.ModuleKey == "FacilityType").FirstOrDefault();
                    if (!((FacilityType != null) ? FacilityType.IsActive : false))
                        Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
                    else
                    {
                       // var FacilityTypeAdd = lstModulePermission.Where(x => x.ModuleKey == "FacilityTypeAdd").FirstOrDefault();
                       // btnAddNew.Visible = (FacilityTypeAdd != null) ? FacilityTypeAdd.IsActive : false;

                        //var FacilityTypeEdit = lstModulePermission.Where(x => x.ModuleKey == "FacilityTypeEdit").FirstOrDefault();
                       // hdnIsEditAllowed.Value = (FacilityTypeEdit != null) ? Convert.ToString(FacilityTypeEdit.IsActive) : "false";

                       // var FacilityTypeDelete = lstModulePermission.Where(x => x.ModuleKey == "FacilityTypeDelete").FirstOrDefault();
                       // hdnIsDeleteAllowed.Value = (FacilityTypeDelete != null) ? Convert.ToString(FacilityTypeDelete.IsActive) : "false";
                    }
                }

                hdnIsDeleteAllowed.Value = "true";
                hdnIsEditAllowed.Value = "true";
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
                List<FacilityType> LIST_OBJ_Facility = new List<FacilityType>();
                LIST_OBJ_Facility = ClsSystem.GetAllFacilityTypeList();
                //DataView dv = new DataView();

                if (LIST_OBJ_Facility.Count != 0)
                {
                    BUSessionUtility.BUSessionContainer.OBJ_CLASS1 = LIST_OBJ_Facility;
                    gvFacility.DataSource = LIST_OBJ_Facility;
                    gvFacility.DataBind();
                }
                else
                {
                    gvFacility.DataSource = string.Empty.ToList();
                    gvFacility.DataBind();
                }
                MakeAccessible(gvFacility);
            }
            catch (Exception ex)
            {
                //Response.Write("this" + ex.Message);
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

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void gvFacility_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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

        //Delete Operation
        [WebMethod]
        public static bool DeleteFacilityType(string id)
        {
            bool returnVal = false;
            try
            {
                returnVal = ClsSystem.DeleteFacilityType(id);
            }
            catch (Exception ex)
            {
                returnVal = false;
                throw ex;
            }
            return returnVal;
        }

        protected void gvFacility_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            #region Set data in Session for editing (Author: Grishma)
            try
            {
                Session["FacilityTypeData"] = null;

                if (!string.IsNullOrEmpty(gvFacility.Rows[e.NewEditIndex].Cells[0].Text))
                {
                    FacilityType objFacilityType = new FacilityType();
                    objFacilityType.FacilityTypeCode = gvFacility.Rows[e.NewEditIndex].Cells[0].Text;
                    objFacilityType.Description = gvFacility.Rows[e.NewEditIndex].Cells[1].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
                    if (gvFacility.Rows[e.NewEditIndex].Cells[2].Text == "Yes")
                        objFacilityType.IsActive = "True";
                    else
                        objFacilityType.IsActive = "False";
                    //objFacilityType.IsActive = gvFacility.Rows[e.NewEditIndex].Cells[2].Text;
                    objFacilityType.DescriptionShort =
                        Utils.Utils.RemoveNbsp(gvFacility.Rows[e.NewEditIndex].Cells[3].Text);
                    objFacilityType.DescriptionLong =
                        Utils.Utils.RemoveNbsp(gvFacility.Rows[e.NewEditIndex].Cells[4].Text);

                    Session["FacilityTypeData"] = objFacilityType;
                    Response.Redirect("~/SystemPages/UpdateClinicType.aspx");
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