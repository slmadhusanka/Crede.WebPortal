using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Portal.BIZ;
using Portal.Model;
using Portal.Model.deserialize;
using AuditHeader = Portal.Model.deserialize.AuditHeader;

namespace WebPortal.Controllers
{
    public class ActivityFollowController : ApiController
    {
        
        // POST: api/ActivityFollow
        public IHttpActionResult Post([FromBody] WebApiRequest data)
        {
            var response = new WebApiResponse();

            //if (!ModelState.IsValid)
            //{
            //    return Content(HttpStatusCode.BadRequest, ModelState);
            //}

            try
            {
                var auditHeaderID = ClsSystem.SaveActivityFollowAuditHeader(data.calculatedObservation);

                if (auditHeaderID == 0)
                {
                    response.Success = false;
                    response.ErrorMessage = "Error in Insert Audit Header";
                    response.ErrorTrace = "";

                    return Ok(response);
                }

                var auditID = ClsSystem.SaveActivityFollowFromWebAPI(data.activityFollow, auditHeaderID);
                response.AuditID = auditID;
                response.Success = true;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                response.ErrorTrace = ex.StackTrace;

                return Content(HttpStatusCode.InternalServerError, response);
            }


        }
        
    }
}
