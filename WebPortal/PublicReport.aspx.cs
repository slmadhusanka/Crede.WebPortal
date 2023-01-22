using System;
using System.Collections.Generic;
using System.Linq;
using Portal.Provider;
using Portal.Provider.Model;

namespace WebPortal
{
    public partial class PublicReport : System.Web.UI.Page
    {
        private int RowNumber = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<Report> lstReport = ClsSecurityManage.GetPublicReports();
                if (lstReport != null && lstReport.Count > 0)
                {
                    lvReportCategory.DataSource = lstReport;
                    lvReportCategory.DataBind();
                }
                else
                {
                    lvReportCategory.DataSource = string.Empty.ToList();
                    lvReportCategory.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region GetRowNumber
        protected int GetRowNumber()
        {
            RowNumber = RowNumber + 1;
            return RowNumber;
        }
        #endregion
    }
}