using Portal.BIZ;
using Portal.Model;
using Portal.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal.SystemPages
{
    public partial class Permission : System.Web.UI.Page
    {
        private List<Portal.Model.Permission> lstPermission = null;
        private List<ModuleDetails> lstModules = null;
        private string assignPermission = null;
        private string unassignPermission = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            success_alert.Visible = false;
            error_alert.Visible = false;
            if (BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG == "1")
            {
                string filename = HttpContext.Current.Request.Url.AbsolutePath;
                string[] tokens = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
                string req_url = tokens[tokens.Length - 2] + "/" + tokens[tokens.Length - 1];

                Response.Redirect("~/Account/ChangePassword.aspx?RedirectURL=" + req_url);
            }
          
            if (!IsPostBack)
            {
                CheckPermission();
                ClsCommon.setFocusCurrentTabControl(Master, "liMobileDeviceMantenance");
                FillRoleDropDown();
                SetData();
            }
        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "Permission"))
                    Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {

            }
        }
        #endregion


        private void SetData()
        {
            lstModules = new List<ModuleDetails>();
            lstModules = ClsSystem.GetAllModules();
            lstPermission = new List<Portal.Model.Permission>();
            int role_code = Convert.ToInt32(ddlRole.SelectedValue.ToString());
            if (role_code > 0)
            {
                lstPermission = ClsSystem.GetAllPermissionsRoleWise(role_code);
                GetParentModule();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Nothing to Save');", true);
            }
        }

        private void FillRoleDropDown()
        {
            List<Portal.Model.Role> RoleList = new List<Portal.Model.Role>();
            RoleList = ClsSystem.GetAllRoleList();
            if (RoleList.Count > 0)
            {
                ddlRole.DataSource = RoleList;
                ddlRole.DataTextField = "Description";
                ddlRole.DataValueField = "RoleCode";
                ddlRole.DataBind();
            }
            else
            {
                ddlRole.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        private void GetParentModule()
        {
            try
            {
                List<ModuleDetails> lstParentModule = lstModules.Where(x => x.ParentModuleId == 0).ToList();
                if (lstParentModule != null && lstParentModule.Count != 0)
                {
                    lvModules1.DataSource = lstParentModule;
                    lvModules1.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void lvModules1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    HiddenField hdnModule1 = (HiddenField)e.Item.FindControl("hdnModule1");
                    CheckBox chkModule1 = (CheckBox)e.Item.FindControl("chkModule1");
                    chkModule1.Checked = lstPermission.Any(x => x.FKModuleId == Convert.ToInt32(hdnModule1.Value));

                    ListView lvModules2 = (ListView)e.Item.FindControl("lvModules2");
                    List<ModuleDetails> lstModules2 = GetChildModule(Convert.ToInt32(hdnModule1.Value));

                    if (lstModules2 != null && lstModules2.Count != 0)
                    {
                        lvModules2.DataSource = lstModules2;
                        lvModules2.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void lvModules2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    HiddenField hdnModule2 = (HiddenField)e.Item.FindControl("hdnModule2");
                    CheckBox chkModule2 = (CheckBox)e.Item.FindControl("chkModule2");
                    chkModule2.Checked = lstPermission.Any(x => x.FKModuleId == Convert.ToInt32(hdnModule2.Value));

                    ListView lvModules3 = (ListView)e.Item.FindControl("lvModules3");
                    List<ModuleDetails> lstModules3 = GetChildModule(Convert.ToInt32(hdnModule2.Value));

                    if (lstModules3 != null && lstModules3.Count != 0)
                    {
                        lvModules3.DataSource = lstModules3;
                        lvModules3.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void lvModules3_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    HiddenField hdnModule3 = (HiddenField)e.Item.FindControl("hdnModule3");
                    CheckBox chkModule3 = (CheckBox)e.Item.FindControl("chkModule3");
                    chkModule3.Checked = lstPermission.Any(x => x.FKModuleId == Convert.ToInt32(hdnModule3.Value));

                    ListView lvModules4 = (ListView)e.Item.FindControl("lvModules4");
                    List<ModuleDetails> lstModules4 = GetChildModule(Convert.ToInt32(hdnModule3.Value));

                    if (lstModules4 != null && lstModules4.Count != 0)
                    {
                        lvModules4.DataSource = lstModules4;
                        lvModules4.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void lvModules4_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    HiddenField hdnModule4 = (HiddenField)e.Item.FindControl("hdnModule4");
                    CheckBox chkModule4 = (CheckBox)e.Item.FindControl("chkModule4");
                    chkModule4.Checked = lstPermission.Any(x => x.FKModuleId == Convert.ToInt32(hdnModule4.Value));
                }
            }
            catch (Exception ex)
            {

            }
        }

        private List<ModuleDetails> GetChildModule(int parentModuleId)
        {
            try
            {
                List<ModuleDetails> lstModuleDetails = lstModules.Where(x => x.ParentModuleId == parentModuleId).ToList();
                return lstModuleDetails;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SuccessMessage.Text = string.Empty;
                ErrorMessage.Text = string.Empty;

                bool result1 = true;
                bool result2 = true;
                assignPermission = string.Empty;
                unassignPermission = string.Empty;

                SaveData(1, lvModules1);
                if (!string.IsNullOrEmpty(assignPermission))
                {
                    assignPermission = "<root>" + assignPermission + "</root>";
                    result1 = ClsSystem.AssignPermission(Convert.ToInt32(ddlRole.Text), assignPermission);
                }

                if (!string.IsNullOrEmpty(unassignPermission))
                {
                    unassignPermission = "<root>" + unassignPermission + "</root>";
                    result2 = ClsSystem.UnassignPermission(Convert.ToInt32(ddlRole.Text), unassignPermission);
                }

                if (result1 && result2)
                {
                    success_alert.Visible = true;
                    SuccessMessage.Text = "permissions saved successfully.";
                    SetData();
                }
                else
                {
                    error_alert.Visible = true;
                    ErrorMessage.Text = "perissions not assigned.";
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {

            }

        }

        protected void chk_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;
                if (chk != null)
                {

                    int id = Convert.ToInt32(chk.ID.Substring(9)) + 1;
                    var lvParent = chk.Parent as ListViewDataItem;

                    ListView lv = (ListView)lvParent.FindControl("lvModules" + id.ToString());

                    //int parentId = Convert.ToInt32(chk.ID.Substring(9)) - 1;
                    //SetParentCheckbox(lvParent.Parent as ListView, chk, parentId,id);

                    SetCheckbox(lv, id, chk.Checked);
                }
            }

            catch (Exception ex)
            {

            }
        }

        private void SetParentCheckbox(ListView lv, CheckBox chk, int parentId, int id)
        {
            try
            {
                var chkParent = chk.Parent.Parent.Parent.FindControl("chkModule" + parentId.ToString()) as CheckBox;
                if (chkParent != null)
                {
                    if (chk.Checked == true)
                    {
                        int count = 0;
                        foreach (ListViewDataItem it in lv.Items)
                        {
                            var chkCurr = it.FindControl("chkModule" + (id - 1).ToString()) as CheckBox;
                            if (chkCurr != null && chkCurr.Checked == true)
                                count++;
                        }
                        if (count == lv.Items.Count)
                            chkParent.Checked = true;
                    }
                    else
                        chkParent.Checked = false;

                    var lvParent = chkParent.Parent as ListViewDataItem;
                    int parentId1 = Convert.ToInt32(chkParent.ID.Substring(9)) - 1;
                    int id1 = Convert.ToInt32(chkParent.ID.Substring(9)) + 1;
                    SetParentCheckbox(lvParent.Parent as ListView, chkParent, parentId1, id1);
                }
            }

            catch (Exception ex)
            {

            }
        }

        private void SetCheckbox(ListView lv, int id, bool isCheck)
        {
            try
            {
                foreach (ListViewDataItem item in lv.Items)
                {
                    var chk = item.FindControl("chkModule" + id.ToString()) as CheckBox;
                    if (chk != null)
                    {
                        chk.Checked = isCheck;
                    }

                    var lvChild = item.FindControl("lvModules" + (id + 1).ToString()) as ListView;
                    if (lvChild != null)
                        SetCheckbox(lvChild, (id + 1), isCheck);
                }
            }

            catch (Exception ex)
            {

            }
        }

        private void SaveData(int c, ListView lvModule)
        {
            try
            {
                ListView lv = lvModule;
                for (int i = 0; i < lv.Items.Count; i++)
                {
                    HiddenField hdnModule = (HiddenField)lv.Items[i].FindControl("hdnModule" + c.ToString());
                    HiddenField hdnModuleKey = (HiddenField)lv.Items[i].FindControl("hdnModuleKey" + c.ToString());
                    CheckBox chkModule = (CheckBox)lv.Items[i].FindControl("chkModule" + c.ToString());
                    string data = string.Empty;
                    if (chkModule.Checked)
                    {
                        assignPermission += @"<Permission><FKModuleId>" + hdnModule.Value + "</FKModuleId><ModuleKey>" + hdnModuleKey.Value + "</ModuleKey></Permission>";
                    }
                    else
                    {
                        unassignPermission += @"<FKModuleId>" + hdnModule.Value + "</FKModuleId>";
                    }
                    ListView lvChild = (ListView)lv.Items[i].FindControl("lvModules" + (c + 1).ToString());
                    if (lvChild != null)
                        SaveData(c + 1, lvChild);

                }

            }

            catch (Exception ex)
            {

            }
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetData();
        }
    }
}