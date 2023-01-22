using Newtonsoft.Json;
using Portal.BIZ;
using Portal.Model;
using Portal.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Portal.BIZ;
using Portal.Model;
using Portal.Utility;

namespace WebPortal
{
    public partial class hld_reprocessing_log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG == "1")
            {
                Response.Redirect("~/Account/ChangePassword.aspx");
            }
           
            CheckPermission();
        }
        #region Permission Check (Author: Grishma) 
        private void CheckPermission()
        {
            try
            {
                List<ModulePermission> lstModulePermission = ClsSystem.GetPermission(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), "ReprocessingLog");
                if (lstModulePermission != null && lstModulePermission.Count > 0)
                {
                    var ReprocessingLog = lstModulePermission.Where(x => x.ModuleKey == "ReprocessingLog").FirstOrDefault();
                    if (!((ReprocessingLog != null) ? ReprocessingLog.IsActive : false))
                        Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
                    else
                    {
                        var ReprocessingLogAdd = lstModulePermission.Where(x => x.ModuleKey == "ReprocessingLogAdd").FirstOrDefault();
                        btnAddNew.Visible = (ReprocessingLogAdd != null) ? ReprocessingLogAdd.IsActive : false;

                        var ReprocessingLogEdit = lstModulePermission.Where(x => x.ModuleKey == "ReprocessingLogEdit").FirstOrDefault();
                        hdnIsEditAllowed.Value = (ReprocessingLogEdit != null) ? Convert.ToString(ReprocessingLogEdit.IsActive) : "false";

                        var ReprocessingLogDelete = lstModulePermission.Where(x => x.ModuleKey == "ReprocessingLogDelete").FirstOrDefault();
                        hdnIsDeleteAllowed.Value = (ReprocessingLogDelete != null) ? Convert.ToString(ReprocessingLogDelete.IsActive) : "false";
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
        #endregion
        [WebMethod]
        public static bool CreateReprocessingLog(DateTime Date, string Transducer,string Lab, string Tec, string VisitNumber, string TimeHLDInitiated, string TimeHLDCompleted)
        {
            //string ReprocessingLogID = "ASDF1234";
            bool result = false;
            try
            {
                result = ClsSystem.CreateHdlReprocessingLog(Date, Transducer,Lab,Tec, VisitNumber, TimeHLDInitiated, TimeHLDCompleted);

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
                table = ClsSystem.GetAllReprocessingLogs();
                result = JsonConvert.SerializeObject(table, Formatting.Indented, new JsonSerializerSettings { DateFormatString = "MMM d, yyyy" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        
        [WebMethod]
        public static bool EditReprocessingLog(string inputDate, string Transducer,string Lab, string Tec, string visitNumber, string TimeHLDInitiated, string TimeHLDCompleted, int EditId)
        {
            bool Result = false;
            try
            {
                Result = ClsSystem.UpdateReprocessingLog(inputDate, Transducer,Lab,Tec, visitNumber, TimeHLDInitiated, TimeHLDCompleted, EditId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        
        [WebMethod(EnableSession = true)]
        public static bool DeleteData(int Id)
        {
            bool result = false;
            try
            {
                result = ClsSystem.DeleteHldReprocessingLog(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static string[] GetFormData()
        {
            string[] results = new string[6];
            try
            {
                results[0] = JsonConvert.SerializeObject(ClsSystem.GetAllSerialNumberCombo().Tables[0], Formatting.None);
                results[1] = JsonConvert.SerializeObject(ClsSystem.GetDeviceIdByUser().Tables[0], Formatting.None);
                //Lab
                results[2] = JsonConvert.SerializeObject(ClsSystem.GetUserFacilityCombo().Tables[0], Formatting.None);
                results[3] = JsonConvert.SerializeObject(ClsSystem.GetFacilityIdByUser().Tables[0], Formatting.None);
                //Tec
                results[4] = JsonConvert.SerializeObject(ClsSystem.GetUsersCombo().Tables[0], Formatting.None);
                results[5] = JsonConvert.SerializeObject(BUSessionUtility.BUSessionContainer.USER_ID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return results;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string[] GetReprocessingLogDetails(int Id)
        {
            DataTable dt = new DataTable();
            string[] result = new string[7];
            try
            {


                result[0] = JsonConvert.SerializeObject(ClsSystem.GetAllSerialNumberCombo().Tables[0], Formatting.None);
                result[1] = JsonConvert.SerializeObject(ClsSystem.GetDeviceIdByUser().Tables[0], Formatting.None);
                //Lab
                result[2] = JsonConvert.SerializeObject(ClsSystem.GetUserFacilityCombo().Tables[0], Formatting.None);
                result[3] = JsonConvert.SerializeObject(ClsSystem.GetFacilityIdByUser().Tables[0], Formatting.None);
                //Tec
                result[4] = JsonConvert.SerializeObject(ClsSystem.GetUsersCombo().Tables[0], Formatting.None);
                result[5] = JsonConvert.SerializeObject(BUSessionUtility.BUSessionContainer.USER_ID);

                result[6] = JsonConvert.SerializeObject(ClsSystem.GetreprocessingLogDetails(Id));


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


    }
}