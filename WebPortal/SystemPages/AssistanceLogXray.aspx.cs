using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace WebPortal.SystemPages
{
    public partial class AssistanceLogXray : System.Web.UI.Page
    {
        // public permission configuration values
        public static bool IsDeleteAllowed;
        public static bool IsEditAllowed;

        public static List<Portal.Model.ErrorLogXray> ErrorLogXrays = new List<Portal.Model.ErrorLogXray>();
        public static List<Portal.Model.NeedAssistanceLog_Xray> NeedNeedAssistanceLog = new List<Portal.Model.NeedAssistanceLog_Xray>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (BUSessionUtility.BUSessionContainer.FORCE_PASSWORD_CHANGED_FLAG == "1")
            {
                Response.Redirect("~/Account/ChangePassword.aspx");
            }

            // CheckPermission();
        }

        /// <summary>
        /// All the row data export
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static string LoadRowData()
        {
            var result = string.Empty;
            try
            {
                var data = ClsSystem.GetAllLogRowDataForAssistanceLog();
                result = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { DateFormatString = "MMM d, yyyy" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        [WebMethod]
        public static string SaveData(NeedAssistanceLog_Xray logData)
        {
            string result;
            try
            {
                var dat2 = (DateTime.ParseExact(logData.Time, "HH:mm", CultureInfo.InvariantCulture));

                logData.DateTimeLog = logData.Date.Add(dat2.TimeOfDay);
                var isAdded = ClsSystem.AddNewNeedAssistanceLog(logData);
                result = isAdded ? "1" : "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Data loading
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static string LoadTable()
        {
            var result = string.Empty;
            try
            {
                var data = ClsSystem.GetAllNeedAssistanceLogData();
                NeedNeedAssistanceLog = data;
                result = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { DateFormatString = "MMM d, yyyy HH:mm" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        [WebMethod]
        public static string GetDataById(int Id)
        {
            string result;
            try
            {
                var data = ClsSystem.GetNeedAssiatceLogDataById(Id);
                result = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { DateFormatString = "MMM d, yyyy HH:mm" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        [WebMethod]
        public static string SaveEditData(Portal.Model.NeedAssistanceLog_Xray logData)
        {
            string result;
            try
            {
                var dat2 = (DateTime.ParseExact(logData.Time, "HH:mm", CultureInfo.InvariantCulture));

                logData.DateTimeLog = logData.Date.Add(dat2.TimeOfDay);

                var isAdded = ClsSystem.UpdateNeedAssistanceLog(logData);
                result = isAdded ? "1" : "0";

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
            var result = false;
            try
            {
                result = ClsSystem.DeleteAssistanceLog(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}