using System;
using System.Collections.Generic;
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

namespace WebPortal.SystemPages
{
    public partial class ListOfTransduser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static bool CreateTransduser(string Transducer, string DeviceNo, string Unit, string Discreption)
        {
            bool result = false;
            try
            {
                result = ClsSystem.CreateTransduser(Transducer, Unit, Discreption, DeviceNo);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static string GetFormData()
        {
            string results = string.Empty;
            try
            {
                results = JsonConvert.SerializeObject(ClsSystem.GetAllSerialNumberCombo().Tables[0], Formatting.None);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return results;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static string GetFormDataUnit()
        {
            string results = string.Empty;
            try
            {
                results = JsonConvert.SerializeObject(ClsSystem.GetAllunitForCombo().Tables[0], Formatting.None);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return results;
        }

        [WebMethod]
        public static bool EditData(string Transducer, string Unit, string Discreption,string DeviceNo, int EditId)
        {
            bool result = false;
            try
            {
                result = ClsSystem.EditTransduser(Transducer, Unit, Discreption, DeviceNo, EditId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        public static int DeleteData(int Id)
        {
            bool ReporcesingLogRefarance = ClsSystem.getCountFromReporcesingLogExistingTransducerDetails(Id);
            int result = 0;
            if (ReporcesingLogRefarance.Equals(true) )
            {
                //must return message to (front end )inform about refarance details
                result = 2;
            }
            else
            {
               
                try
                {
                    bool x = ClsSystem.DeleteTransduser(Id);
                    if (x.Equals(true))
                    {
                        result = 1;
                    }
                    else {
                        result = 0;
                    }
                    
                }
                catch (Exception ex)
                {
                    result = 0;
                    throw ex;
                }
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
                table = ClsSystem.GETALL_ListOfTransduser();
                result = JsonConvert.SerializeObject(table, Formatting.Indented, new JsonSerializerSettings { DateFormatString = "MMM d, yyyy" });
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetDetails(int Id)
        {
            DataTable dt = new DataTable();
            string result = string.Empty;
            try
            {


                dt = ClsSystem.GetTransduserDetails(Id);
                result = JsonConvert.SerializeObject(dt, Formatting.None);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}