using Portal.BIZ;
using Portal.Model;
using Portal.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using Newtonsoft.Json;

namespace WebPortal
{
    public partial class hld_solution_testing_log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG == "1")
            {
                Response.Redirect("~/Account/ChangePassword.aspx");
            }

            CheckPermission();
        }


        [WebMethod]
        public static bool SaveData(string Date, string Time, int LotNumber, string Temp, bool IsDaily, bool IsBeforeChanging, string DateChange, string NextDateChange,string BottleNumber)
        {
            bool result = false;
            try
            {
                result = ClsSystem.CreateHldSolutionTestingLog(Date, Time, LotNumber, Temp, IsDaily, IsBeforeChanging, DateChange, NextDateChange, BottleNumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        [WebMethod]
        public static bool EditData(string Date, string Time, int LotNumber, string Temp, bool IsDaily, bool IsBeforeChanging, string DateChange, string NextDateChange, int EditId,string BottleNumber)
        {
            bool result = false;
            try
            {
                result = ClsSystem.EditHldSolutionTestingLog(Date, Time, LotNumber, Temp, IsDaily, IsBeforeChanging, DateChange, NextDateChange, EditId,BottleNumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        [WebMethod(EnableSession = true)]
        public static bool DeleteData(int Id)
        {
            bool result = false;
            try
            {
                result = ClsSystem.DeleteHldSolutionTestingLog(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static string LoadTable()
        {
            string result = string.Empty;
            DataTable table = null;
            try
            {
                table = ClsSystem.GETALL_SolutionTestingLog();
                result = JsonConvert.SerializeObject(table, Formatting.Indented, new JsonSerializerSettings { DateFormatString = "MMM d, yyyy" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }



        #region Permission Check (Author: Grishma) 
        private void CheckPermission()
        {
            try
            {
                List<ModulePermission> lstModulePermission = ClsSystem.GetPermission(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "SolutionTestingLog");
                if (lstModulePermission != null && lstModulePermission.Count > 0)
                {
                    var SolutionTestingLog = lstModulePermission.Where(x => x.ModuleKey == "SolutionTestingLog").FirstOrDefault();
                    if (!((SolutionTestingLog != null) ? SolutionTestingLog.IsActive : false))
                        Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
                    else
                    {
                        var SolutionTestingAdd = lstModulePermission.Where(x => x.ModuleKey == "SolutionTestingLogAdd").FirstOrDefault();
                        btnAddNew.Visible = (SolutionTestingAdd != null) ? SolutionTestingAdd.IsActive : false;

                        var SolutionTestingEdit = lstModulePermission.Where(x => x.ModuleKey == "SolutionTestingLogEdit").FirstOrDefault();
                        hdnIsEditAllowed.Value = (SolutionTestingEdit != null) ? Convert.ToString(SolutionTestingEdit.IsActive) : "false";

                        var SolutionTestingDelete = lstModulePermission.Where(x => x.ModuleKey == "SolutionTestingLogDelete").FirstOrDefault();
                        hdnIsDeleteAllowed.Value = (SolutionTestingDelete != null) ? Convert.ToString(SolutionTestingDelete.IsActive) : "false";
                    }
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

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetSolutionTestingLogDetails(int Id)
        {
            DataTable dt = new DataTable();
            string result = string.Empty;
            try
            {


                dt = ClsSystem.GetSolutionTestingLogDetails(Id);
                result = JsonConvert.SerializeObject(dt, Formatting.None);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }




        #endregion
    }
}