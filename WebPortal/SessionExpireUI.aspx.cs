using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BIZ;

namespace WebPortal
{
    public partial class SessionExpireUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!IsPostBack)
            {
                string usernm = string.Empty;
                ClsSecurity.Logout();
                Session.Abandon();
                FormsAuthentication.SignOut();

            }
        }

        protected void LnkLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}