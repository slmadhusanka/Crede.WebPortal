using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Portal.Model;

namespace WebPortal.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        private const string V = "Uploading Failed.The response is not a valid JSON response.";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                var response = new WebApiResponse();
                response.Success = false;
                response.ErrorMessage = V;
                response.ErrorTrace = actionContext.ModelState;

                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest, response);
            }
        }
    }
}