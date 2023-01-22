using System;
using Portal.Utility;

namespace WebPortal
{
    public partial class _404 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClsCommon.SetPageTitle(Page, "Page Not Found");
        }
    }
}