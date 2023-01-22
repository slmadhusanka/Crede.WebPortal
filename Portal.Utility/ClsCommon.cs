using Portal.DAL;
using Portal.Utility.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Portal.Utility
{
    public class ClsCommon
    {
        public static void setRecordUpdateStatusInDb(string Table_Name, string Table_Column_name, string Operation, string EditedRecord_id)
        {
            string ToDisplayName = string.Empty;
            string ToAdr = string.Empty;
            string FromDisplayName = string.Empty;
            string FromAdr = string.Empty;
            string CcDisplayName = string.Empty;
            string CcAdr = string.Empty;
            string BccAdr = string.Empty;
            string Subject = string.Empty;
            string BodyText = string.Empty;
            string AttachmentFileName = string.Empty;
            string updated_record_id = string.Empty;
            if (ConfigurationManager.AppSettings["isEmail"].ToString() == "0")
            {
                return;
            }
            try
            {
                if (Operation == "Deleted")
                    updated_record_id = (Convert.ToInt32(updated_record_id) - 1).ToString();
                else if (Operation == "Edited")
                    updated_record_id = EditedRecord_id;
                else if (Operation == "Added")
                    updated_record_id = ClsCommon.getRecordUpdated(Table_Name, Table_Column_name);
                ToDisplayName = string.Empty;
                ToAdr = ConfigurationManager.AppSettings["system_email_receiever"].ToString();
                FromDisplayName = string.Empty;
                FromAdr = ConfigurationManager.AppSettings["system_email_sender"].ToString();
                CcDisplayName = string.Empty;
                CcAdr = string.Empty;
                BccAdr = string.Empty;
                Subject = SetEmailMessageSubject(Table_Name, Operation.ToLower());
                BodyText = SetEmailMessageBody(Table_Name, updated_record_id, Operation.ToLower());
                AttachmentFileName = string.Empty;
                string spoofingEmailAdress = ConfigurationManager.AppSettings["NotificationEmailSpoofing"].ToString();
                EmailService.BasicSendMail(ToDisplayName, ToAdr, FromDisplayName, FromAdr, CcDisplayName, CcAdr, BccAdr, Subject, BodyText, AttachmentFileName, spoofingEmailAdress);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SendMailtoCustomerAfterUserCreation(string Password, string Email_address, string fpath, string UserName)
        {
            string ToDisplayName = string.Empty;
            string ToAdr = string.Empty;
            string FromDisplayName = string.Empty;
            string FromAdr = string.Empty;
            string CcDisplayName = string.Empty;
            string CcAdr = string.Empty;
            string BccAdr = string.Empty;
            string Subject = string.Empty;
            string BodyText = string.Empty;
            string AttachmentFileName = string.Empty;
            string Company_Name = string.Empty;
            string User_id = string.Empty;
            string website_url = string.Empty;

            if (ConfigurationManager.AppSettings["isEmail"].ToString() == "0")
            {
                return;
            }
            try
            {
                website_url = ConfigurationManager.AppSettings["WebSiteUrl"].ToString();
                User_id = ClsCommon.getRecordUpdated("Users", "UserID");
                Company_Name = BUSessionUtility.BUSessionContainer.CompanyName;
                ToDisplayName = string.Empty;
                ToAdr = Email_address;
                FromDisplayName = string.Empty;
                FromAdr = ConfigurationManager.AppSettings["system_email_sender"].ToString();
                CcDisplayName = string.Empty;
                CcAdr = string.Empty;
                BccAdr = string.Empty;
                Subject = SetEmailMessageSubjectForCustomerAfterUserCreation(Company_Name);
                BodyText = SetEmailMessageBodyForCustomerAfterUserCreation(Company_Name, User_id, Password, website_url, fpath, UserName);
                AttachmentFileName = string.Empty;
                string AuthenticatedSMTP = ConfigurationManager.AppSettings["AuthenticatedSMTP"].ToString();

                if (AuthenticatedSMTP == "No")
                {
                    MailMessage mMailMessage = new MailMessage();

                    string server = ConfigurationManager.AppSettings["MailServerIP"].ToString(); ;

                    mMailMessage.From = new MailAddress(FromAdr);
                    mMailMessage.IsBodyHtml = true;
                    mMailMessage.To.Add(new MailAddress(Email_address));
                    mMailMessage.Subject = Subject;
                    // Set the body of the mail message
                    mMailMessage.Body = BodyText;

                    SmtpClient mSmtpClient = new SmtpClient(server);
                    mSmtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                    mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mSmtpClient.Send(mMailMessage);

                    mSmtpClient.Send(mMailMessage);
                }
                else
                {
                    string spoofingEmailAdress = ConfigurationManager.AppSettings["NotificationEmailSpoofing"].ToString();
                    EmailService.BasicSendMail(ToDisplayName, ToAdr, FromDisplayName, FromAdr, CcDisplayName, CcAdr, BccAdr, Subject, BodyText, AttachmentFileName, spoofingEmailAdress);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void setFocusCurrentTabControl(MasterPage masterPage, string FindControlId)
        {

        }
        public static bool validate(string pUserId, string password)
        {

            bool IsValid = false;
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);

            string sql = @"select t.rowid from user_login t where t.user_id='" + pUserId + @"' and t.password='" + password + @"' ";
            string rowid = string.Empty;
            using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, sql))
            {
                while (dr.Read())
                {


                    rowid = dr["rowid"].ToString();
                    if (!string.IsNullOrEmpty(rowid))
                        IsValid = true;
                }
                dr.Close();
            }
            objCDataAccess.Dispose(objDbCommand);
            return IsValid;
        }
        public static string getRecordUpdated(string table_name, string column_name)
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);

            string sql = @"select max(" + column_name + ") as primary_key from " + table_name + ";";
            string primary_key = string.Empty;
            using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, sql))
            {
                if (dr.Read())
                {
                    primary_key = dr["primary_key"].ToString();
                }
                dr.Close();
            }
            objCDataAccess.Dispose(objDbCommand);
            return primary_key;
        }
        public static string SetEmailMessageBody(string Table_name, string Record_id, string Operation)
        {
            string SetEmailMessage = "The table " + Table_name + " has been updated. The record ID " + Record_id; // +" has been " + Operation;
            return SetEmailMessage;
        }
        public static string SetEmailMessageSubject(string Table_name, string Operation)
        {
            string SetEmailMessageSub = "Alberta health services update ( " + Table_name + " - " + Operation + ")";
            return SetEmailMessageSub;
        }
        public static string SetEmailMessageSubjectForCustomerAfterUserCreation(string Company_Name)
        {
            string SetEmailMessage = "Your " + Company_Name + " Clinic portal account has been created";
            return SetEmailMessage;
        }
        public static string SetEmailMessageBodyForCustomerAfterUserCreation(string CompanyName, string User_id, string Password, string website_url, string BodyTemp, string UserName)
        {

            string body1 = File.ReadAllText(BodyTemp);


            body1 = string.Format(body1, UserName, Password, website_url);

            //string SetEmailMessageSub = string.Empty;
            //SetEmailMessageSub = @"<html><body> <b>Welcome to the Hand Hygiene Audit System!</b><br/><br/><b>A new account for the Hand Hygiene Audit System at Crede has been created for you.</b> <br/> <br/> <b> You can login to your account using the credentials below at <a href=" + "\"" + website_url + "\"" + " >" + website_url + "</a> . You will be asked to reset your password when you login. </b>";
            //SetEmailMessageSub += " <br/>  <br/> <b> User ID: "+ User_id + "</b><br/> <b> Password: "+ Password +"</b>"
            //    + " </body></html>";
            //return SetEmailMessageSub;
            return body1;

        }
        public static void BindCombo(System.Web.UI.WebControls.DropDownList objddlControl, string sql)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);

            //string sql = @"select t.PRODUCT_NM,t.PRODUCT_SL from product_type t";
            string rowid = string.Empty;
            using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, sql))
            {
                objddlControl.DataSource = dr;
                objddlControl.DataTextField = "PRODUCT_NM";
                objddlControl.DataValueField = "PRODUCT_SL";
                objddlControl.DataBind();
                dr.Close();
            }
            objCDataAccess.Dispose(objDbCommand);

        }
        #region BindCombo
        public static void BindComboWithSQL(System.Web.UI.WebControls.DropDownList objddlControl, string SQL, bool isOption0, string selectStatement, string col_text, string col_val)
        {

            BINDCOMBO OBJ_BINDCOMBO = new BINDCOMBO();
            List<BINDCOMBO> listOfDDL = new List<BINDCOMBO>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            //string sql = @"select t.PRODUCT_NM,t.PRODUCT_SL from product_type t";
            //string sql = @"select * from product_type t";

            int i = 0;
            using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL))
            {
                if (isOption0)
                {
                    while (dr.Read())
                    {

                        if (i == 0)
                        {
                            if (!string.IsNullOrEmpty(selectStatement))
                            {
                                OBJ_BINDCOMBO.Data_text = selectStatement;
                            }
                            else
                            {
                                //OBJ_BINDCOMBO.Data_text = "---Select---";
                            }
                            OBJ_BINDCOMBO.Data_Value = "0";
                            OBJ_BINDCOMBO.Data_Value = string.Empty;
                            listOfDDL.Add(OBJ_BINDCOMBO);
                            OBJ_BINDCOMBO = new BINDCOMBO();
                            OBJ_BINDCOMBO.Data_text = dr[col_text].ToString();
                            OBJ_BINDCOMBO.Data_Value = dr[col_val].ToString();
                            listOfDDL.Add(OBJ_BINDCOMBO);
                            i++;
                        }
                        else
                        {
                            OBJ_BINDCOMBO = new BINDCOMBO();
                            OBJ_BINDCOMBO.Data_text = dr[col_text].ToString();
                            OBJ_BINDCOMBO.Data_Value = dr[col_val].ToString();
                            listOfDDL.Add(OBJ_BINDCOMBO);
                        }
                    }
                    if (!dr.HasRows)
                    {
                        objddlControl.Items.Clear();
                        objddlControl.Items.Add(new System.Web.UI.WebControls.ListItem("---No Data---", "0"));
                        objddlControl.DataBind();
                    }
                }

                else
                {
                    objddlControl.DataSource = dr;
                    objddlControl.DataTextField = col_text;
                    objddlControl.DataValueField = col_val;
                    objddlControl.DataBind();
                }

                dr.Close();
            }
            objCDataAccess.Dispose(objDbCommand);

            if (listOfDDL.Count > 1)
            {
                objddlControl.DataSource = listOfDDL;
                objddlControl.DataTextField = "Data_text";
                objddlControl.DataValueField = "Data_Value";
                objddlControl.DataBind();
            }


        }
       
        public static void BindComboWithSP(System.Web.UI.WebControls.DropDownList objddlControl, string SP_NM, bool isOption0, string selectStatement, string col_text, string col_val, List<DSSQLParam> objList)
        {

            BINDCOMBO OBJ_BINDCOMBO = new BINDCOMBO();
            List<BINDCOMBO> listOfDDL = new List<BINDCOMBO>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);

            int i = 0;
            using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SP_NM, CommandType.StoredProcedure, objList))
            {
                if (isOption0)
                {
                    while (dr.Read())
                    {

                        if (i == 0)
                        {
                            if (!string.IsNullOrEmpty(selectStatement))
                            {
                                OBJ_BINDCOMBO.Data_text = selectStatement;
                            }
                            else
                            {
                                OBJ_BINDCOMBO.Data_text = "---Select---";
                            }
                            OBJ_BINDCOMBO.Data_Value = "-1";
                            listOfDDL.Add(OBJ_BINDCOMBO);
                            OBJ_BINDCOMBO = new BINDCOMBO();
                            OBJ_BINDCOMBO.Data_text = dr[col_text].ToString();
                            OBJ_BINDCOMBO.Data_Value = dr[col_val].ToString();
                            listOfDDL.Add(OBJ_BINDCOMBO);
                            i++;
                        }
                        else
                        {
                            OBJ_BINDCOMBO = new BINDCOMBO();
                            OBJ_BINDCOMBO.Data_text = dr[col_text].ToString();
                            OBJ_BINDCOMBO.Data_Value = dr[col_val].ToString();
                            listOfDDL.Add(OBJ_BINDCOMBO);
                        }
                    }
                }

                else
                {
                    objddlControl.DataSource = dr;
                    objddlControl.DataTextField = col_text;
                    objddlControl.DataValueField = col_val;
                    objddlControl.DataBind();
                }
                dr.Close();
            }
            objCDataAccess.Dispose(objDbCommand);

            if (listOfDDL.Count > 1)
            {
                objddlControl.DataSource = listOfDDL;
                objddlControl.DataTextField = "Data_text";
                objddlControl.DataValueField = "Data_Value";
                objddlControl.DataBind();
            }
        }
        #endregion

        #region Insert into tables


        public static void Insert(string TABLE_NM, ArrayList list, CDataAccess objCDataAccess, DbCommand objDbCommand)
        {
            //CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            //DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.Serializable, "application", false);
            List<DSSQLParam> objList = new List<DSSQLParam>();

            string SQL = @"insert into " + TABLE_NM + " values (";

            foreach (string str in list)
            {
                SQL = SQL + str + ",";
            }
            SQL = SQL.Substring(0, SQL.LastIndexOf(","));
            SQL = SQL + ");";
            try
            {

                objCDataAccess.ExecuteNonQuery(objDbCommand, SQL);
                //objDbCommand.Transaction.Commit();
            }
            catch (Exception ex)
            {
                //objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                //objDbCommand.Connection.Close();
            }

        }
        public static void Insert(string TABLE_NM, ArrayList list)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.Serializable, "application", false);
            List<DSSQLParam> objList = new List<DSSQLParam>();

            string SQL = @"insert into " + TABLE_NM + " values (";

            foreach (string str in list)
            {
                SQL = SQL + str + ",";
            }
            SQL = SQL.Substring(0, SQL.LastIndexOf(","));
            SQL = SQL + ");";
            try
            {

                objCDataAccess.ExecuteNonQuery(objDbCommand, SQL);
                objDbCommand.Transaction.Commit();
            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objDbCommand.Connection.Close();
            }

        }
        public static void Update(string TABLE_NM, ArrayList list, ArrayList WhereConditions)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.Serializable, "application", false);
            List<DSSQLParam> objList = new List<DSSQLParam>();

            string SQL = @"Update " + TABLE_NM + " set ";
            string WherCondition = string.Empty;
            string[] arr;
            foreach (string str in list)
            {
                SQL = SQL + str.Split('-')[0] + "=" + str.Split('-')[1] + ", ";
            }
            SQL = SQL.Substring(0, SQL.LastIndexOf(",")) + " ";

            foreach (string str in WhereConditions)
            {
                arr = str.Split('-');
                if (arr.Length == 3)
                    WherCondition = WherCondition + str.Split('-')[0] + " " + str.Split('-')[1] + "=" + str.Split('-')[2] + " ";
                else if (arr.Length == 2)
                {
                    WherCondition = WherCondition + str.Split('-')[1] + "=" + str.Split('-')[2] + " ";
                }

            }
            SQL = SQL + WherCondition + ";";
            try
            {

                objCDataAccess.ExecuteNonQuery(objDbCommand, SQL);
                objDbCommand.Transaction.Commit();
            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objDbCommand.Connection.Close();
            }

        }
        public static void Delete(string TABLE_NM, ArrayList WhereConditions)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.Serializable, "application", false);
            List<DSSQLParam> objList = new List<DSSQLParam>();

            string SQL = @"Delete From " + TABLE_NM + " ";
            string WherCondition = string.Empty;
            string[] arr;

            foreach (string str in WhereConditions)
            {
                arr = str.Split('-');
                if (arr.Length == 3)
                    WherCondition = WherCondition + str.Split('-')[0] + " " + str.Split('-')[1] + "=" + str.Split('-')[2] + " ";
                else if (arr.Length == 2)
                {
                    WherCondition = WherCondition + str.Split('-')[1] + "=" + str.Split('-')[2] + " ";
                }

            }
            SQL = SQL + WherCondition + ";";
            try
            {

                objCDataAccess.ExecuteNonQuery(objDbCommand, SQL);
                objDbCommand.Transaction.Commit();
            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objDbCommand.Connection.Close();
            }

        }
        public static void ExecuteSQL(string SQL, CDataAccess objCDataAccess, DbCommand objDbCommand)
        {
            //CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            //DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.Serializable, "application", false);
            List<DSSQLParam> objList = new List<DSSQLParam>();
            try
            {

                objCDataAccess.ExecuteNonQuery(objDbCommand, SQL);
                //objDbCommand.Transaction.Commit();
            }
            catch (Exception ex)
            {
                //objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                //objDbCommand.Connection.Close();
            }

        }
        public static void ExecuteSQL(string SQL)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.Serializable, "application", false);
            List<DSSQLParam> objList = new List<DSSQLParam>();
            try
            {

                objCDataAccess.ExecuteNonQuery(objDbCommand, SQL);
                objDbCommand.Transaction.Commit();
            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objDbCommand.Connection.Close();
            }

        }
        //For Single RoW Fetch
        public static ArrayList Fetch(string TABLE_NM, ArrayList list, ArrayList WhereConditions)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.Serializable, "application", false);
            List<DSSQLParam> objList = new List<DSSQLParam>();
            string SQL = @"Select ";
            string WherCondition = string.Empty;
            string[] arr;
            ArrayList listFetch = new ArrayList();
            foreach (string str in list)
            {
                SQL = SQL + str + ", ";
            }
            SQL = SQL.Substring(0, SQL.LastIndexOf(",")) + " ";
            SQL = SQL + "From " + TABLE_NM + " ";
            if (WhereConditions.Count != 0)
                SQL = SQL + " " + "Where ";
            foreach (string str in WhereConditions)
            {
                arr = str.Split('-');
                if (arr.Length == 3)
                    WherCondition = WherCondition + str.Split('-')[0] + " " + str.Split('-')[1] + "=" + str.Split('-')[2] + " ";
                else if (arr.Length == 2)
                {
                    WherCondition = WherCondition + str.Split('-')[0] + "=" + str.Split('-')[1] + " ";
                }

            }
            SQL = SQL + WherCondition + ";";
            //listFetch.Add(SQL);
            using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL))
            {

                while (dr.Read())
                {
                    foreach (string str in list)
                    {
                        if (str.Contains("AS"))
                            listFetch.Add(dr[str.Substring(str.IndexOf("AS") + 2).Trim()].ToString());
                        else if (str.Contains("as"))
                            listFetch.Add(dr[str.Substring(str.IndexOf("as") + 2).Trim()].ToString());
                        else
                            listFetch.Add(dr[str].ToString());
                    }

                }

                dr.Close();
            }
            objCDataAccess.Dispose(objDbCommand);
            return listFetch;

        }
        #endregion

        public static void SetPageTitle(Page page, string PageName)
        {
            try
            {
                string title = string.Empty;
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Customer"]) && !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Product"]))
                {
                    title = ConfigurationManager.AppSettings["Customer"] + " " + ConfigurationManager.AppSettings["Product"] + " - " + PageName;
                }
                else if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Customer"]))
                {
                    title = ConfigurationManager.AppSettings["Customer"] + " - " + PageName;
                }
                else if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Product"]))
                {
                    title = ConfigurationManager.AppSettings["Product"] + " - " + PageName;
                }
                else
                {
                    title = PageName;
                }
                if (!string.IsNullOrEmpty(title) && !string.IsNullOrWhiteSpace(title))
                {
                    page.Title = title;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void BindComboWithList(System.Web.UI.WebControls.DropDownList objddlControl, ArrayList List, bool isOption0, string selectStatement)
        {

            BINDCOMBO OBJ_BINDCOMBO = new BINDCOMBO();
            List<BINDCOMBO> listOfDDL = new List<BINDCOMBO>();
            if (isOption0)
            {
                if (string.IsNullOrEmpty(selectStatement))
                {
                    OBJ_BINDCOMBO.Data_text = "---Select---";
                    OBJ_BINDCOMBO.Data_Value = "-1";
                }
                else
                {
                    OBJ_BINDCOMBO.Data_text = selectStatement;
                    OBJ_BINDCOMBO.Data_Value = "-1";
                }
                listOfDDL.Add(OBJ_BINDCOMBO);
            }
            foreach (string str in List)
            {
                OBJ_BINDCOMBO = new BINDCOMBO();
                OBJ_BINDCOMBO.Data_text = str.Split('#')[0];
                OBJ_BINDCOMBO.Data_Value = str.Split('#')[1];
                listOfDDL.Add(OBJ_BINDCOMBO);

            }

            if (listOfDDL.Count > 1)
            {
                objddlControl.DataSource = listOfDDL;
                objddlControl.DataTextField = "Data_text";
                objddlControl.DataValueField = "Data_Value";
                objddlControl.DataBind();
            }

        }
        public static void SendMailtoDistributionList(string Email_address, string Subject, string message, string Name)
        {
            string ToDisplayName = string.Empty;
            string ToAdr = string.Empty;
            string FromDisplayName = string.Empty;
            string FromAdr = string.Empty;
            string CcDisplayName = string.Empty;
            string CcAdr = string.Empty;
            string BccAdr = string.Empty;
            //string Subject = string.Empty;
            string BodyText = string.Empty;
            string AttachmentFileName = string.Empty;
            string Company_Name = string.Empty;
            string User_id = string.Empty;
            string website_url = string.Empty;
            if (ConfigurationManager.AppSettings["isEmail"].ToString() == "0")
            {
                return;
            }
            try
            {
                website_url = ConfigurationManager.AppSettings["WebSiteUrl"].ToString();
                //User_id = ClsCommon.getRecordUpdated("Users", "UserID");
                Company_Name = BUSessionUtility.BUSessionContainer.CompanyName;
                ToDisplayName = string.Empty;
                ToAdr = Email_address;
                FromDisplayName = string.Empty;
                FromAdr = ConfigurationManager.AppSettings["system_email_sender"].ToString();
                CcDisplayName = string.Empty;
                CcAdr = string.Empty;
                BccAdr = string.Empty;

                BodyText = "<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><title>Untitled Document</title><style type='text/css'>body,td,th {";
                BodyText += "font-family: 'Arial Black', Gadget, sans-serif;font-size: 12px;color: #333;font-weight: normal;text-align:justify;}";
                BodyText += "body {background-color: #FFF;font-weight: normal;}a {font-family: 'Arial Black', Gadget, sans-serif;font-size: 12px;color: #3076B6;}";
                BodyText += "a:link {text-decoration: none;}a:visited {text-decoration: none;color: #3076B6;}a:hover {text-decoration: underline;color: #3076B6;}";
                BodyText += "a:active {text-decoration: none;color: #3076B6;}</style></head><body>";
                BodyText += "<table width='800' border='0' align='center' cellpadding='0' cellspacing='0'><tr>";
                BodyText += "<td align='center' valign='middle' style='font-family:Arial, Helvetica, sans-serif; color: #61A874; font-size: 11px; text-align:center;'><br /></td></tr>";
                BodyText += "<tr><td style='border: 1px solid #2C6DA9;background:#fff;'><img style='margin-left:317px;margin-bottom:5px;' src='" + website_url + "/images/login-logo.jpg' height='99' align='middle' /></td></tr>";
                BodyText += "<tr><td style='padding: 30px;'><p><strong>" + Name + "</strong><br /><br /><br /></p>";
                BodyText += "<p>" + message + "</p>";
                BodyText += "</td></tr> <tr><td style='border: 1px solid #2C6DA9;background:#fff;'><img style='margin-left:360px;' src='" + website_url + "/images/Logo-Portal.png' height='36' /></td></tr></table></body></html>";
                AttachmentFileName = string.Empty;
                string AuthenticatedSMTP = ConfigurationManager.AppSettings["AuthenticatedSMTP"].ToString();

                if (AuthenticatedSMTP == "No")
                {

                    MailMessage mMailMessage = new MailMessage();

                    string server = ConfigurationManager.AppSettings["MailServerIP"].ToString();

                    mMailMessage.From = new MailAddress(FromAdr);
                    mMailMessage.IsBodyHtml = true;
                    mMailMessage.To.Add(new MailAddress(ToAdr));
                    mMailMessage.Subject = Subject;
                    mMailMessage.Body = BodyText;

                    SmtpClient mSmtpClient = new SmtpClient(server);
                    mSmtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                    mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mSmtpClient.Send(mMailMessage);
                }
                else
                {
                    string spoofingEmailAdress = ConfigurationManager.AppSettings["NotificationEmailSpoofing"].ToString();
                    EmailService.BasicSendMail(ToDisplayName, ToAdr, FromDisplayName, FromAdr, CcDisplayName, CcAdr, BccAdr, Subject, BodyText, AttachmentFileName, spoofingEmailAdress);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool SendSimpleMail(List<string> emails, string subject, string body_text, string display_name, MemoryStream attachment, string file_name)
        {
            bool result = false;
            try
            {
                string from_address = ConfigurationManager.AppSettings["system_email_sender"].ToString();
                string AuthenticatedSMTP = ConfigurationManager.AppSettings["AuthenticatedSMTP"].ToString();
                string mail_server_IP = ConfigurationManager.AppSettings["MailServerIP"].ToString();
                string system_email_sender_password = ConfigurationManager.AppSettings["system_email_sender_password"].ToString();

                MailMessage MailObj = new MailMessage();
                MailObj.IsBodyHtml = true;
                MailObj.Subject = subject;
                MailObj.From = new MailAddress(from_address, display_name);
                foreach (string email in emails)
                {
                    MailObj.To.Add(new MailAddress(email));
                }
                MailObj.Priority = MailPriority.Normal;
                if (attachment != null && !string.IsNullOrEmpty(file_name))
                {
                    MailObj.Attachments.Add(new Attachment(attachment, file_name));
                }

                MailObj.Body = body_text;
                if (AuthenticatedSMTP == "No")
                {
                    SmtpClient mSmtpClient = new SmtpClient(mail_server_IP);
                    mSmtpClient.UseDefaultCredentials = false;
                    mSmtpClient.Credentials = new NetworkCredential(from_address, system_email_sender_password);
                    mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mSmtpClient.Send(MailObj);
                    result = true;
                }
                else
                {
                    SmtpClient smtpcli = new SmtpClient(mail_server_IP, 587); //use this PORT! and "smtp.gmail.com"
                    smtpcli.EnableSsl = true;
                    smtpcli.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpcli.UseDefaultCredentials = false;
                    smtpcli.Credentials = new NetworkCredential(from_address, system_email_sender_password);
                    smtpcli.Send(MailObj);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #region Get Date Time
        public static string GetDateFormate(string pDateValue)
        {
            string[] strArDateValue = pDateValue.Split('/');
            int month_value = Convert.ToInt32(strArDateValue[1]);
            string month = string.Empty;
            switch (month_value)
            {
                case 1:
                    month = "Jan";
                    break;
                case 2:
                    month = "Feb";
                    break;
                case 3:
                    month = "Mar";
                    break;
                case 4:
                    month = "Apr";
                    break;
                case 5:
                    month = "May";
                    break;
                case 6:
                    month = "Jun";
                    break;
                case 7:
                    month = "Jul";
                    break;
                case 8:
                    month = "Aug";
                    break;
                case 9:
                    month = "Sep";
                    break;
                case 10:
                    month = "Oct";
                    break;
                case 11:
                    month = "Nov";
                    break;
                case 12:
                    month = "Dec";
                    break;
                default:
                    month = "Invalid";
                    break;


            }
            pDateValue = strArDateValue[0] + "-" + month + "-" + strArDateValue[2];

            string vDateFormate = string.Empty;
            if (!string.IsNullOrEmpty(pDateValue))
            {
                vDateFormate = pDateValue;
            }
            return vDateFormate;
        }

        public static string GetDBDateFormate(string pDateValue)
        {
            string[] strArDateValue = pDateValue.Split('/');
            //string strTemp = strArDateValue[0];
            //strArDateValue[0] = strArDateValue[1];
            //strArDateValue[1] = strTemp;
            pDateValue = strArDateValue[0] + "/" + strArDateValue[1] + "/" + strArDateValue[2];
            DateTime vValue = new DateTime();
            string vDateFormate = null;
            if (!string.IsNullOrEmpty(pDateValue))
            {
                try
                {
                    vValue = Convert.ToDateTime(pDateValue);
                    vDateFormate = vValue.ToString("dd-MMM-yyyy");
                }
                catch
                {
                    if (vDateFormate == null)
                    {
                        return string.Empty;
                    }
                }
            }
            return vDateFormate;
        }

        public static string GetDBDateFormateYYYYMMDD(string pDateValue)
        {
            string[] strArDateValue = pDateValue.Split('/');
            //string strTemp = strArDateValue[0];
            //strArDateValue[0] = strArDateValue[1];
            //strArDateValue[1] = strTemp;
            //pDateValue = strArDateValue[0] + "/" + strArDateValue[1] + "/" + strArDateValue[2];
            DateTime vValue = new DateTime();
            string vDateFormate = null;
            if (!string.IsNullOrEmpty(pDateValue))
            {
                try
                {
                    vValue = Convert.ToDateTime(pDateValue);
                    vDateFormate = vValue.ToString(vValue.Year + "-" + vValue.Month + "-" + vValue.Day);
                }
                catch
                {
                    if (vDateFormate == null)
                    {
                        return string.Empty;
                    }
                }
            }
            return vDateFormate;
        }
        public static string GetDBDateFormateYYYYMMDDWithSplitChar(string pDateValue, char Split)
        {
            string[] strArDateValue = pDateValue.Split(Split);
            //string strTemp = strArDateValue[0];
            //strArDateValue[0] = strArDateValue[1];
            //strArDateValue[1] = strTemp;
            //pDateValue = strArDateValue[0] + "/" + strArDateValue[1] + "/" + strArDateValue[2];
            DateTime vValue = new DateTime();
            string vDateFormate = null;
            if (!string.IsNullOrEmpty(pDateValue))
            {
                try
                {
                    vValue = Convert.ToDateTime(pDateValue);
                    vDateFormate = vValue.ToString(vValue.Year + "-" + vValue.Month + "-" + vValue.Day);
                }
                catch
                {
                    if (vDateFormate == null)
                    {
                        return string.Empty;
                    }
                }
            }
            return vDateFormate;
        }
        #endregion

        #region Convert Number to Word For Display the Balance Amount
        public static string word_table(double val)
        {
            string[] word_arr = { "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety", " ", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string word = string.Empty;
            int i = 0;
            i = Convert.ToInt32(val) - 1;

            if (word_arr.Length >= Convert.ToInt32(val) - 1)
            {
                word = word_arr[i];
            }
            return word;
        }
        public static string NumbToWord(double amount)
        {

            string retWord = string.Empty;
            double amt = amount;
            string g_cur_dec_name = "Paisa Only";
            //string g_cur_dec_name = string.Empty;
            //--
            if (amount >= 10000000)
            {

                return NumbToWord(Math.Floor(amount / 10000000)) + " Crore " + NumbToWord(amt %
                                                                             10000000);
            }
            else if (amount >= 100000)
            {
                return NumbToWord(Math.Floor(amount / 100000)) + " Lac " + NumbToWord(amt %
                                                                         100000);
            }
            else if (amount >= 1000)
            {
                return NumbToWord(Math.Floor(amount / 1000)) + " Thousand " + NumbToWord(amt %
                                                                            1000);
            }
            else if (amount >= 100)
            {
                return NumbToWord(Math.Floor(amount / 100)) + " Hundred " + NumbToWord(amt % 100);
            }
            else if (amount >= 20)
            {
                return word_table(Math.Floor(amount / 10)) + " " + NumbToWord(amt % 10);
            }
            else if (amount >= 1)
            {
                return word_table(Math.Floor(amount) + 10) + NumbToWord(amt - Math.Floor(amt));
                // --- fractions ------
            }
            else if (amount >= .20)
            {
                return " and " + word_table(Math.Floor((amount * 100) / 10)) + " " + NumbToWord((Math.Round(amt, 2) * 100) %
                                                                                           10) + " " + g_cur_dec_name;
            }
            else if (amount >= .01)
            {
                return " and " + word_table((Math.Round(amount, 2) * 100) + 10) + " " + g_cur_dec_name;
            }


            return word_table(Math.Floor(amount * 100) + 10);

        }
        #endregion

        #region Send Email for Allowed Device

        public static bool SendEmailForAllowedDevice(string Email_address, string verificationCode, string udid)
        {
            string ToDisplayName = string.Empty;
            string ToAdr = string.Empty;
            string FromDisplayName = string.Empty;
            string FromAdr = string.Empty;
            string CcDisplayName = string.Empty;
            string CcAdr = string.Empty;
            string BccAdr = string.Empty;
            string Subject = string.Empty;
            string BodyText = string.Empty;
            string AttachmentFileName = string.Empty;

            if (ConfigurationManager.AppSettings["isEmail"].ToString() == "0")
            {
                return false;
            }
            try
            {
                ToDisplayName = string.Empty;
                ToAdr = Email_address;
                FromDisplayName = string.Empty;
                FromAdr = ConfigurationManager.AppSettings["system_email_sender"].ToString();
                CcDisplayName = string.Empty;
                CcAdr = string.Empty;
                BccAdr = string.Empty;

                Subject = ConfigurationManager.AppSettings["Email_Subject_AllowedDevice"].ToString();
                string emailFormatTextFile = ConfigurationManager.AppSettings["Email_Format_AllowedDevice"].ToString();

                if (!string.IsNullOrEmpty(Subject) && !string.IsNullOrEmpty(emailFormatTextFile))
                {
                    string emailFormat = File.ReadAllText(emailFormatTextFile);
                    BodyText = string.Format(emailFormat, verificationCode, udid);
                    AttachmentFileName = string.Empty;
                    string AuthenticatedSMTP = ConfigurationManager.AppSettings["AuthenticatedSMTP"].ToString();

                    if (AuthenticatedSMTP == "No")
                    {
                        MailMessage mMailMessage = new MailMessage();

                        string server = ConfigurationManager.AppSettings["MailServerIP"].ToString();

                        mMailMessage.From = new MailAddress(FromAdr);
                        mMailMessage.IsBodyHtml = true;
                        mMailMessage.To.Add(new MailAddress(ToAdr));
                        mMailMessage.Subject = Subject;
                        mMailMessage.Body = BodyText;

                        SmtpClient mSmtpClient = new SmtpClient(server);
                        mSmtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                        mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        mSmtpClient.Send(mMailMessage);
                    }
                    else
                    {
                        string spoofingEmailAdress = ConfigurationManager.AppSettings["NotificationEmailSpoofing"].ToString();
                        EmailService.BasicSendMail(ToDisplayName, ToAdr, FromDisplayName, FromAdr, CcDisplayName, CcAdr, BccAdr, Subject, BodyText, AttachmentFileName, spoofingEmailAdress);
                    }

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        #endregion
    }
}
