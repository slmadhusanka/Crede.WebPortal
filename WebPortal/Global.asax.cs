using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Newtonsoft.Json;
using Portal.BIZ;
using Portal.BIZ.HelperModel;
using Portal.Utility;

namespace WebPortal
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = false;
        }

        protected void Application_EndRequest()
        {   //here breakpoint
            // under debug mode you can find the exceptions at code: this.Context.AllErrors
            var errors = this.Context.AllErrors;

            var userID = 0;
            var UserName = "";
            var Email = "";
            if (!string.IsNullOrEmpty(BUSessionUtility.BUSessionContainer.USER_ID) &&
                !string.IsNullOrWhiteSpace(BUSessionUtility.BUSessionContainer.USER_ID))
            {
                userID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID);
            }

            if (!string.IsNullOrEmpty(BUSessionUtility.BUSessionContainer.FirstName) &&
                !string.IsNullOrWhiteSpace(BUSessionUtility.BUSessionContainer.FirstName))
            {
                UserName =
                    $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}";
            }

            if (!string.IsNullOrEmpty(BUSessionUtility.BUSessionContainer.FirstName) &&
                !string.IsNullOrWhiteSpace(BUSessionUtility.BUSessionContainer.FirstName))
            {
                Email =
                    BUSessionUtility.BUSessionContainer.Email;
            }

            if (errors != null)
            {
                var errorList = errors;

                HttpResponse response = this.Context.Response;
                int httpCode = response.StatusCode;

                SerilogAuditTrail.LogError(new AuditTrailDataModel
                {
                    Description = JsonConvert.SerializeObject(errors),

                    Action = LogAction.Error.Value,
                    Module = httpCode.ToString(),
                    ModuleID = 0,
                    TableName = "Error",

                    UserID = userID,
                    UserName = UserName,
                    Email = Email,
                    UserRole = "",
                    UserRoleID = 0,
                    Exception = this.Context.Error
                });

                if (httpCode == 404)
                {
                    Response.Redirect("~/404.aspx");
                }
                else if (httpCode == 500)
                {
                    Response.Redirect("~/ErrorPage.aspx");
                }
            }
        }
        
        void Application_Error(object sender, EventArgs e)
        {
            var serverError = Server.GetLastError() as HttpException;
            string url = Request.RawUrl;
            if (serverError != null)
            {
                if (serverError.GetHttpCode() == 404)
                {
                    Server.ClearError();
                    Server.Transfer("~/404.aspx");
                }
                else if (serverError.GetHttpCode() == 500)
                {
                    Portal.BIZ.Logger.log(serverError, "");
                    Server.ClearError();
                    Server.Transfer("~/ErrorPage.aspx");
                }
            }
        }
        
        void Session_Start(object sender, EventArgs e) 
        {
            
        }
        
        void Session_End(object sender, EventArgs e) 
        {
            
        }
    }
}