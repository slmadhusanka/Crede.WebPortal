using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using Portal.BIZ;
using Portal.Provider;
using Portal.Utility;

namespace WebPortal.ReportViewer
{
    public partial class ReportContainer : System.Web.UI.Page
    {
        private string _ReportServerUsername = ConfigurationManager.AppSettings["ReportServerUsername"].ToString();
        private string _ReportServerPassword = ConfigurationManager.AppSettings["ReportServerPassword"].ToString();
        private string _ReportServerURL = ConfigurationManager.AppSettings["ReportServerURL"].ToString();
        private string _facility;
        private string _unit;

        protected void Page_Load(object sender, EventArgs e)
        {
    
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 1200;

            if (!IsPostBack)
            {
                string report_name = "";
                if (Request.QueryString.Count == 3 && (!string.IsNullOrEmpty(Request.QueryString["ReportCategory"])) && (!string.IsNullOrEmpty(Request.QueryString["ReportCode"])) && Session["ShowReport"] == null)
                {

                    hdnReportCode.Value = Request.QueryString["ReportCode"];
                    hdnReportCategory.Value = Request.QueryString["ReportCategory"];
                    report_name = Request.QueryString["ReportName"];
                    LoadReport();
                    ReportViewer1.SubmittingParameterValues += new Microsoft.Reporting.WebForms.ReportParametersEventHandler(ReportViewer1_SubmittingParameterValues);

                    if (Request.QueryString["ReportCategory"] == "Public")
                    {
                        DataSet ds = ClsSecurityManage.GetSendButtonStatusForPublicReport(hdnReportCode.Value);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["IsSendEnable"].ToString().Length > 0)
                            {
                                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IsSendEnable"].ToString()) == true)
                                    btn.Visible = true;
                                else
                                    btn.Visible = false;
                            }
                            else
                                btn.Visible = false;
                        }
                        else
                            Response.Redirect("~//ReportViewer/AuthenticatedReport.aspx");
                    }
                    else
                        Response.Redirect("~//ReportViewer/AuthenticatedReport.aspx");


                }
                else if (Session["ShowReport"] != null)
                {

                    string[] data = Convert.ToString(Session["ShowReport"]).Split(';');
                    hdnReportId.Value = data[0];
                    hdnReportCode.Value = data[1];
                    hdnReportKey.Value = data[2];
                    report_name = data[3];

                    CheckPermission();

                    LoadReport();
                    ReportViewer1.SubmittingParameterValues += new Microsoft.Reporting.WebForms.ReportParametersEventHandler(ReportViewer1_SubmittingParameterValues);

                    if (!string.IsNullOrEmpty(hdnReportCode.Value))
                    {
                        if (!string.IsNullOrEmpty(hdnReportId.Value))
                        {
                            DataSet ds = ClsSecurityManage.GetSendButtonStatus(hdnReportId.Value);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["IsSendEnable"].ToString().Length > 0)
                                {
                                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IsSendEnable"].ToString()) == true)
                                        btn.Visible = true;
                                    else
                                        btn.Visible = false;
                                }
                                else
                                    btn.Visible = false;
                            }
                            else
                                Response.Redirect("~/ReportViewer/AuthenticatedReport.aspx");
                        }
                        else
                            Response.Redirect("~/ReportViewer/AuthenticatedReport.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/ReportViewer/AuthenticatedReport.aspx");
                    }

                }
                else
                {
                    Response.Redirect("~/ReportViewer/AuthenticatedReport.aspx");
                }
                lblrptName.InnerText = report_name + " Report";
            }
        }

        #region Permission Check (Author: Grishma)
        private void CheckPermission()
        {
            try
            {
                if (!ClsSystem.GetPermissionForPage(Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER), hdnReportKey.Value))
                    Response.Redirect(ConfigurationManager.AppSettings["SecurityRedirectPath"].ToString(), true);
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public void ReportViewer1_SubmittingParameterValues(object sender, Microsoft.Reporting.WebForms.ReportParametersEventArgs e)
        {
            Response.Redirect("~/ReportViewer/AuthenticatedReport.aspx");
        }

        private void LoadReport()
        {
            // Uncomment the below line for production once the report is showing properly:: ReportViewer1.Visible = false;
            ReportViewer1.Visible = false;
            DataTable dtUserReports = null;
            string ReportCode = null;

            try
            {
                if (!string.IsNullOrEmpty(hdnReportCode.Value))
                {
                    ReportCode = hdnReportCode.Value;
                }
                else
                {
                    Response.Redirect("~/ReportViewer/AuthenticatedReport.aspx");
                }

                //if (ReportCode.Equals("Unit1"))
                //    btn.Visible = true;
                //else
                //    btn.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {

                string curUserRoleCode = BUSessionUtility.BUSessionContainer.ROLES_FOR_USER;
                //Response.Write("Role: " + curUserRoleCode);
                //dtUserReports = ClsSecurityManage.GetAuthenticatedUserReports(curUserRoleCode, ReportCode);

                //dtUserReports = ClsSecurityManage.GetAuthenticatedUserReports(hdnReportId.Value, ReportCode);
                dtUserReports = ClsSecurityManage.GetAuthenticatedUserReports(ReportCode, hdnReportCategory.Value);
                if (dtUserReports == null) return;

                if (dtUserReports.Rows == null)
                    return;

                if (dtUserReports.Rows.Count < 1)
                    return;

                hdnReportName.Value = dtUserReports.Rows[0]["ReportName"].ToString();

                ReportViewer1.Visible = true;
                ReportViewer1.ServerReport.Timeout = 1800000;
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials(_ReportServerUsername, _ReportServerPassword, "");
                ReportViewer1.ServerReport.ReportServerUrl = new Uri(_ReportServerURL);
                ReportViewer1.ServerReport.ReportPath = "/" + dtUserReports.Rows[0]["ReportServerLocation"].ToString();

                //Author: Grishma
                #region Filter Parameters
                List<ReportParameter> lstReportParameters = new List<ReportParameter>();
                ReportParameterInfoCollection paramCollection = ReportViewer1.ServerReport.GetParameters();
                if (hdnReportCategory.Value != "Public" && BUSessionUtility.BUSessionContainer.USER_ID != null)
                {
                    //First get Zone,Facility and Unit assigned to user
                    DataSet ds = ClsSecurityManage.GetUserDataForReportFilter(BUSessionUtility.BUSessionContainer.USER_ID);

                    bool IsParamAvailable = false;
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        IsParamAvailable = true;

                    string nullValue = null;

                    //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count>0)
                    //{
                    //if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ZoneDesc"])))
                    //{

                    //Pass parameters for filtering Zone, Facility and Unit                        
                    //ReportParameterInfoCollection paramCollection = ReportViewer1.ServerReport.GetParameters();
                    foreach (ReportParameterInfo rp in paramCollection)
                    {
                        switch (rp.Name)
                        {
                            case "ParamZone":
                                ReportParameter paramZone = null;
                                if (IsParamAvailable && ds.Tables[0].Rows.Count > 0)
                                {
                                    //ReportParameter paramZone = new ReportParameter(rp.Name, Convert.ToString(ds.Tables[0].Rows[0]["ZoneDesc"]));
                                    paramZone = new ReportParameter(rp.Name);
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        paramZone.Values.Add(Convert.ToString(ds.Tables[0].Rows[i]["ZoneDesc"]));
                                    }
                                }
                                else
                                {
                                    paramZone = new ReportParameter(rp.Name, string.Empty);
                                }
                                lstReportParameters.Add(paramZone);
                                break;

                            case "ParamFacility":
                                ReportParameter paramFacility = null;
                                if (IsParamAvailable && ds.Tables[1].Rows.Count > 0)
                                {
                                    //ReportParameter paramFacility = new ReportParameter(rp.Name, Convert.ToString(ds.Tables[1].Rows[0]["FacilityDesc"]));

                                    paramFacility = new ReportParameter(rp.Name);
                                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                    {
                                        paramFacility.Values.Add(Convert.ToString(ds.Tables[1].Rows[i]["FacilityDesc"]));
                                    }

                                }
                                else
                                {
                                    paramFacility = new ReportParameter(rp.Name, string.Empty);
                                }
                                lstReportParameters.Add(paramFacility);
                                break;

                            case "ParamUnit":
                                ReportParameter paramUnit = null;
                                if (IsParamAvailable && ds.Tables[2].Rows.Count > 0)
                                {
                                    //ReportParameter paramUnit = new ReportParameter(rp.Name, Convert.ToString(ds.Tables[2].Rows[0]["UnitDesc"]));

                                    paramUnit = new ReportParameter(rp.Name);
                                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                                    {
                                        paramUnit.Values.Add(Convert.ToString(ds.Tables[2].Rows[i]["UnitDesc"]));
                                    }
                                }
                                else
                                {
                                    paramUnit = new ReportParameter(rp.Name, string.Empty);
                                }
                                lstReportParameters.Add(paramUnit);
                                break;

                            case "ParamZoneCode":
                                ReportParameter paramZoneCode = null;
                                if (IsParamAvailable && ds.Tables[0].Rows.Count > 0)
                                {
                                    paramZoneCode = new ReportParameter(rp.Name);
                                    //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    //{
                                    //    paramZoneCode.Values.Add(Convert.ToString(ds.Tables[0].Rows[i]["ZoneCode"]));
                                    //}
                                    string paramZoneCodes = string.Join("^", ds.Tables[0].Rows.OfType<DataRow>().Select(r => r["ZoneCode"]));
                                    paramZoneCode.Values.Add(paramZoneCodes);
                                }
                                else
                                {
                                    paramZoneCode = new ReportParameter(rp.Name, string.Empty);
                                }
                                lstReportParameters.Add(paramZoneCode);
                                break;

                            case "ParamFacilityCode":
                                ReportParameter paramFacilityCode = null;
                                if (IsParamAvailable && ds.Tables[1].Rows.Count > 0)
                                {
                                    paramFacilityCode = new ReportParameter(rp.Name);
                                    //for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                    //{
                                    //    paramFacilityCode.Values.Add(Convert.ToString(ds.Tables[1].Rows[i]["FacilityCode"]));
                                    //}
                                    string paramFacilityCodes = string.Join("^", ds.Tables[1].Rows.OfType<DataRow>().Select(r => r["FacilityCode"]));
                                    paramFacilityCode.Values.Add(paramFacilityCodes);
                                }
                                else
                                {
                                    paramFacilityCode = new ReportParameter(rp.Name, string.Empty);
                                }
                                lstReportParameters.Add(paramFacilityCode);
                                break;

                            case "ParamUnitCode":
                                ReportParameter paramUnitCode = null;
                                if (IsParamAvailable && ds.Tables[2].Rows.Count > 0)
                                {
                                    paramUnitCode = new ReportParameter(rp.Name);
                                    //for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                                    //{
                                    //    paramUnitCode.Values.Add(Convert.ToString(ds.Tables[2].Rows[i]["UnitDesc"]));
                                    //}
                                    string paramUnitCodes = string.Join("^", ds.Tables[2].Rows.OfType<DataRow>().Select(r => r["UnitCode"]));
                                    paramUnitCode.Values.Add(paramUnitCodes);
                                }
                                else
                                {
                                    paramUnitCode = new ReportParameter(rp.Name, string.Empty);
                                }
                                lstReportParameters.Add(paramUnitCode);
                                break;
                        }

                    }
                    //}
                    //}
                }

                else
                {
                    //Pass parameters for filtering Zone, Facility and Unit                         
                    //ReportParameterInfoCollection paramCollection = ReportViewer1.ServerReport.GetParameters();
                    foreach (ReportParameterInfo rp in paramCollection)
                    {
                        switch (rp.Name)
                        {
                            case "ParamZone":
                                ReportParameter paramZone = null;
                                paramZone = new ReportParameter(rp.Name, string.Empty);
                                lstReportParameters.Add(paramZone);
                                break;

                            case "ParamFacility":
                                ReportParameter paramFacility = null;
                                paramFacility = new ReportParameter(rp.Name, string.Empty);
                                lstReportParameters.Add(paramFacility);
                                break;

                            case "ParamUnit":
                                ReportParameter paramUnit = null;
                                paramUnit = new ReportParameter(rp.Name, string.Empty);
                                lstReportParameters.Add(paramUnit);
                                break;

                            case "ParamZoneCode":
                                ReportParameter paramZoneCode = null;
                                paramZoneCode = new ReportParameter(rp.Name, string.Empty);
                                lstReportParameters.Add(paramZoneCode);
                                break;

                            case "ParamFacilityCode":
                                ReportParameter paramFacilityCode = null;
                                paramFacilityCode = new ReportParameter(rp.Name, string.Empty);
                                lstReportParameters.Add(paramFacilityCode);
                                break;

                            case "ParamUnitCode":
                                ReportParameter paramUnitCode = null;
                                paramUnitCode = new ReportParameter(rp.Name, string.Empty);
                                lstReportParameters.Add(paramUnitCode);
                                break;
                        }

                    }


                }

                ReportViewer1.ServerReport.SetParameters(lstReportParameters);
                #endregion

                ReportViewer1.ShowParameterPrompts = true;
                ReportViewer1.ShowPrintButton = true;
                ReportViewer1.ShowBackButton = true;
                ReportViewer1.ShowExportControls = true;
                ReportViewer1.ShowCredentialPrompts = false;

                ReportViewer1.SizeToReportContent = true;
                ReportViewer1.ServerReport.Refresh();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
               
        private string[] ExportReport()
        {
            try
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;

                string[] filenameParams = new string[2];
                string randomFileName = Guid.NewGuid().ToString();
                string reportName = hdnReportName.Value;

                filenameParams[1] = reportName.Replace(' ', '_').Replace("&", "and").Replace("\\", "_").Replace("/", "_").Replace(":", "_").Replace("*", "_").Replace("?", "_").Replace("\"", "_").Replace("<", "_").Replace(">", "_").Replace("|", "_")
                                         + "_" +
                                         DateTime.Now.ToString("yyyy-MM-dd") + ".pdf";

                byte[] bytes;

                bytes = ReportViewer1.ServerReport.Render("PDF", null, out mimeType,
                 out encoding, out filenameExtension, out streamids, out warnings);

                string filename = Path.Combine(Path.GetTempPath(), randomFileName + ".pdf");

                using (FileStream fs = new FileStream(filename, FileMode.Create))
                { fs.Write(bytes, 0, bytes.Length); }

                filenameParams[0] = filename;
                return filenameParams;
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Login.aspx?id=" + "| " + ex.Message);
                return null;
                throw ex;
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {


            string[] reportFilename = ExportReport();
            if (reportFilename != null && !string.IsNullOrEmpty(reportFilename[0]) && !string.IsNullOrEmpty(reportFilename[1]))
            {
                bool result = false;
                try
                {
                    result = ClsSecurity.SendReport(reportFilename[0], reportFilename[1], txtTo.Value, "", "", txtSubject.Value, hdn_message.Value);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorLog", "ErrorLog('" + ExceptionWriter(ex) + "');", true);
                    throw ex;
                }
                if (result == false)
                {
                    string erroMessage = ConfigurationManager.AppSettings["ErrorMsg"].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", "alert('" + erroMessage + "');", true);
                }
                else
                {
                    //Response.Redirect(Request.RawUrl); 
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ClosePopup", "ClosePopup();", true);
                }
            }
            //ClsSecurity.SendReport("120", reportFilename);
        }

        public string ExceptionWriter(Exception ex)
        {
            StringBuilder sbExceptionMessage = new StringBuilder();
            sbExceptionMessage.Append("<p>DateTime : " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "</p>");
            sbExceptionMessage.Append("<p>Exception Type : " + ex.GetType().Name + "</p>");
            sbExceptionMessage.Append("</br></br>");
            sbExceptionMessage.Append("<p>Message : <b>" + ex.Message + "</b></p>");
            sbExceptionMessage.Append("<p>Stack trace : </p>");
            string stack_trace = Regex.Replace(ex.StackTrace, @"\r\n?|\n", "<br />");
            sbExceptionMessage.Append("<p>" + stack_trace + "</p>");

            return sbExceptionMessage.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string[] bindEmail(string reportName)
        {
            string emailFormat = File.ReadAllText(HostingEnvironment.MapPath("~/Email_Format_Report.txt") ?? string.Empty);
            string name = BUSessionUtility.BUSessionContainer.FirstName + " " + BUSessionUtility.BUSessionContainer.LastName;
            string email = BUSessionUtility.BUSessionContainer.Email;
            string todayDate = DateTime.Today.ToString("yyyy-MM-dd");
            string emailBodyText = string.Format(emailFormat, reportName, todayDate, name, email);
            string[] emailContents = new string[2];
            string[] ReturnVal = new string[2];

            emailContents[0] = todayDate;
            emailContents[1] = emailBodyText;
            return emailContents;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (BUSessionUtility.BUSessionContainer.USER_ID != null)
            {
                Response.Redirect("~/ReportViewer/AuthenticatedReport.aspx");
            }
            else
            {
                Response.Redirect("~/PublicReport.aspx");
            }
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            string randomFileName = Guid.NewGuid().ToString();

            byte[] bytes;
            bytes = ReportViewer1.ServerReport.Render("PDF", null, out mimeType,
               out encoding, out filenameExtension, out streamids, out warnings);

            string filename = Path.Combine(Path.GetTempPath(), randomFileName + ".pdf");

            using (FileStream fs = new FileStream(filename, FileMode.Create))
            { fs.Write(bytes, 0, bytes.Length); }
        }
    }
}