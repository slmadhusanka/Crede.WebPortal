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
using Portal.Model.deserialize;
using Portal.Utility;

namespace WebPortal.SystemPages
{
    public partial class ErrorLogXray : System.Web.UI.Page
    {
        // public permission configuration values
        public static bool IsDeleteAllowed;
        public static bool IsEditAllowed;

        public static List<Portal.Model.ErrorLogXray> ErrorLogXrays = new List<Portal.Model.ErrorLogXray>();

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
                var data = ClsSystem.GetAllLogRowData();
                result = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { DateFormatString = "MMM d, yyyy" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// API Method for data pass for main table
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static string LoadTable()
        {
            var result = string.Empty;
            try
            {
                var data = ClsSystem.GetAllLogData();
                ErrorLogXrays = data;
                result = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { DateFormatString = "MMM d, yyyy" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        [WebMethod]
        public static string SaveData(Portal.Model.ErrorLogXray logData)
        {
            string result;
            try
            {
                var isAdded = ClsSystem.AddNewErrorLog(logData);
                result = isAdded ? "1" : "0";

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        [WebMethod]
        public static string SaveEditData(Portal.Model.ErrorLogXray logData)
        {
            string result;
            try
            {
                var isAdded = ClsSystem.UpdateErrorLog(logData);
                result = isAdded ? "1" : "0";

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
                var data = ClsSystem.GetLogDataById(Id);
                result = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { DateFormatString = "MMM d, yyyy" });

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
                result = ClsSystem.DeleteErrorLog(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}