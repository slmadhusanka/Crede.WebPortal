using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class SessionSecurity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static void UpdateSession()
        {
            if (!string.IsNullOrEmpty(BUSessionUtility.BUSessionContainer.SessionTimeOut))
            {
                //Setting the Session.Timeout by reading from web.config file. The value in the web.config file for this key must be integer.
                HttpContext.Current.Session.Timeout = Convert.ToInt32(BUSessionUtility.BUSessionContainer.SessionTimeOut);
            }
            else
            {
                HttpContext.Current.Session.Timeout = 30;//Default TimeOut 20 minuite.
            }

            string msgSession = @" Your Session will expire in three minutes. Click on \'Extend Session\' to stay logged in.";
            //time to remind, 3 minutes before session ends0
            int int_MilliSecondsTimeReminder = (HttpContext.Current.Session.Timeout * 60000) - 3 * 60000;
            //time to redirect, 5 milliseconds before session ends
            int int_MilliSecondsTimeOut = (HttpContext.Current.Session.Timeout * 60000) - 5;
            string default_page = HttpContext.Current.Request.Path;
            string strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            string WebSiteUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
            string vPATH = WebSiteUrl + "SessionExpireUI.aspx";

            string urlForAlert = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + Convert.ToString(ConfigurationManager.AppSettings["PageForSessionAlert"]) + msgSession;

            string str_Script = @"
            var myTimeReminder, myTimeOut; 
            clearTimeout(myTimeReminder); 
            clearTimeout(myTimeOut); " +
              "var sessionTimeReminder = " +
          int_MilliSecondsTimeReminder.ToString() + "; " +
              "var sessionTimeout = " + int_MilliSecondsTimeOut.ToString() + ";" +
              " function doRedirect(){ var ctrlHCPCount = document.querySelector('input[id*=\"hdnHCPCount\"]');if (ctrlHCPCount != null && ctrlHCPCount != \"\" && ctrlHCPCount != \"undefined\")ctrlHCPCount.value = \"0\"; window.location.href='" + vPATH + "'; }" +
              " function doReminder(){ var ctrlHCPCount = document.querySelector('input[id*=\"hdnHCPCount\"]');if (ctrlHCPCount != null && ctrlHCPCount != \"\" && ctrlHCPCount != \"undefined\")ctrlHCPCount.value = \"0\"; ShowConfirmationMessage();"
              + @" myTimeReminder=setTimeout('doReminder()', sessionTimeReminder);"
           + @" myTimeOut=setTimeout('doRedirect()', sessionTimeout);";

            ScriptManager.RegisterClientScriptBlock((Page)(HttpContext.Current.Handler), typeof(Page), "CheckSessionOut", str_Script, true);
        }
    }
}