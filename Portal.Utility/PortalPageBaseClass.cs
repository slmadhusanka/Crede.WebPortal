using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace Portal.Utility
{
    public class PortalPageBaseClass : Page
    {
        private string appPath = ConfigurationManager.AppSettings["appPath"].ToString();
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);


            //It appears from testing that the Request and Response both share the 
            // same cookie collection.  If I set a cookie myself in the Reponse, it is 
            // also immediately visible to the Request collection.  This just means that 
            // since the ASP.Net_SessionID is set in the Session HTTPModule (which 
            // has already run), thatwe can't use our own code to see if the cookie was 
            // actually sent by the agent with the request using the collection. Check if 
            // the given page supports session or not (this tested as reliable indicator 
            // if EnableSessionState is true), should not care about a page that does 
            // not need session
            if (Context.Session != null)
            {
                //Tested and the IsNewSession is more advanced then simply checking if 
                // a cookie is present, it does take into account a session timeout, because 
                // I tested a timeout and it did show as a new session
                if (Session.IsNewSession)
                {
                    // If it says it is a new session, but an existing cookie exists, then it must 
                    // have timed out (can't use the cookie collection because even on first 
                    // request it already contains the cookie (request and response
                    // seem to share the collection)
                    string szCookieHeader = Request.Headers["Cookie"];
                    if ((null != szCookieHeader) && (szCookieHeader.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        Response.Redirect(appPath + "SessionExpireUI.aspx?timeout=yes&success=no");
                    }
                }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
                return;
            }
            if (string.IsNullOrEmpty(BUSessionUtility.BUSessionContainer.USER_ID))
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
                return;
            }

            base.OnLoad(e);
        }

        protected override void OnError(EventArgs e)
        {
            /*Exception objErr = Server.GetLastError().GetBaseException();
            StringBuilder sb = new StringBuilder("Error Caught in Page_Error event");
            sb.Append("\n Error in: ");
            sb.Append(Request.Url.ToString());
            sb.Append("\n Error Message: ");
            sb.Append(objErr.Message.ToString());
            sb.Append("\n Stack Trace: ");
            sb.Append(objErr.StackTrace.ToString());
            Session["Msg"] = sb.ToString();
            Server.ClearError();*/

            base.OnError(e);
            //Server.Transfer(ConfigurationManager.AppSettings["appPath"].ToString() + "CustomErrorPageUI.aspx");
            //Response.Redirect(ConfigurationManager.AppSettings["appPath"].ToString() + "CustomErrorPageUI.aspx");
        }
        protected override void InitializeCulture()
        {
            string Culture_Name = "en-us";
            Culture_Name = ConfigurationManager.AppSettings["Culture"];
            //string Culture_Name = "ru-RU";
            //if (SessionUtility.SessionContainer.User != null)
            //Culture_Name = SessionUtility.SessionContainer.User.UserCultureCode;
            if (string.IsNullOrEmpty(Culture_Name))
                Culture_Name = "Auto";
            //Use this
            UICulture = Culture_Name;
            Culture = Culture_Name;
            //OR This
            if (Culture_Name != "Auto")
            {
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(Culture_Name);
                ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                System.Threading.Thread.CurrentThread.CurrentCulture = ci;
                System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            }

            base.InitializeCulture();
        }
    }
}
