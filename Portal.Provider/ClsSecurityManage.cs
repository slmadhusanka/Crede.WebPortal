using Portal.DAL;
using Portal.Provider.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Security;
using System.Xml;

namespace Portal.Provider
{
    public class ClsSecurityManage
    {
        public void CreateCustomUser(USERS Obj, out MembershipCreateStatus status)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            string SQL = "USP_ADD_USER";
            List<DSSQLParam> objList = new List<DSSQLParam>();

            int recAdded = 0;
            try
            {
                objList.Add(new DSSQLParam("FirstName", Obj.FirstName, false));
                objList.Add(new DSSQLParam("LastName", Obj.LastName, false));
                objList.Add(new DSSQLParam("UserName", Obj.UserName, false));
                objList.Add(new DSSQLParam("Email", Obj.Email, false));

                objList.Add(new DSSQLParam("Occupation", Obj.Occupation, false));

                objList.Add(new DSSQLParam("Role_Code", Obj.RoleCode, false));

                if (!string.IsNullOrEmpty(Obj.FacilityCode))
                {
                    objList.Add(new DSSQLParam("Facility_Code", Obj.FacilityCode, false));
                }
                if (!string.IsNullOrEmpty(Obj.UnitCode))
                {
                    objList.Add(new DSSQLParam("UnitCode", Obj.UnitCode, false));
                }
                if (!string.IsNullOrEmpty(Obj.RegionCode))
                {
                    objList.Add(new DSSQLParam("Region_Code", Obj.RegionCode, false));
                }
                objList.Add(new DSSQLParam("Password", EncodePassword(Obj.Password), false));
                objList.Add(new DSSQLParam("IsLockedOut", Obj.IsLockedOut, false));
                objList.Add(new DSSQLParam("IsAuditor", Obj.IsAuditor, false));
                objList.Add(new DSSQLParam("DisplayName", Obj.DisplayName, false));
                objList.Add(new DSSQLParam("IsActive", Obj.IsActive, false));
                objList.Add(new DSSQLParam("Phone", Obj.Phone, false));
                objList.Add(new DSSQLParam("Lab", Obj.Lab, false));

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    status = MembershipCreateStatus.Success;
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    status = MembershipCreateStatus.UserRejected;
                }

            }
            catch (Exception ex)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(ex, "CreateUser");
                //}

                status = MembershipCreateStatus.ProviderError;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

        }

        public static bool AddPasswrodResetRequest(int UserId, string ResetData, DateTime RequestedTime, DateTime ExpiredTime, bool IsExpired)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            bool result = false;
            try
            {
                string SQL = "USP_ADD_USER_PASSWORD_RESET_REQUEST";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("UserId", UserId, false));
                objList.Add(new DSSQLParam("ResetData", ResetData, false));
                objList.Add(new DSSQLParam("RequestedTime", RequestedTime, false));
                objList.Add(new DSSQLParam("ExpiredTime", ExpiredTime, false));
                objList.Add(new DSSQLParam("IsExpired", IsExpired, false));
                int recAdded = 0;
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }
            return result;
        }

        public static string GetUserIdByUserName(string UserName)
        {
            try
            {

                string User_ID = string.Empty;
                CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
                DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
                string SQL = "USP_Get_UserId_by_UserName";

                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("Username", UserName, false));

                using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
                {
                    if (dr.Read())
                    {

                        User_ID = dr["UserID"].ToString();
                    }

                    dr.Close();
                }
                objCDataAccess.Dispose(objDbCommand);
                return User_ID;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<USERS> GetAllUserList()
        {

            USERS OBJ_USER_INFO = new USERS();
            List<USERS> LIST_USER = new List<USERS>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            List<DSSQLParam> objList = new List<DSSQLParam>();
            string SQL = "USP_GET_ALL_USERS_LIST";

            try
            {
                using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
                {
                    while (dr.Read())
                    {
                        OBJ_USER_INFO = new USERS();
                        OBJ_USER_INFO.User_ID = dr["UserID"].ToString();
                        OBJ_USER_INFO.FirstName = dr["FirstName"].ToString();
                        OBJ_USER_INFO.LastName = dr["LastName"].ToString();
                        OBJ_USER_INFO.UserName = dr["UserName"].ToString();
                        OBJ_USER_INFO.RoleCode = dr["Role_Code"].ToString();
                        OBJ_USER_INFO.FacilityCode = dr["Facility_Code"].ToString();
                        OBJ_USER_INFO.RoleDescription = dr["role_desc"].ToString();
                        OBJ_USER_INFO.FacilityDescription = dr["facility_desc"].ToString();
                        OBJ_USER_INFO.LastLoginDate = dr["LastLoginDate"].ToString();
                        OBJ_USER_INFO.LastPasswordChangedDate = dr["LastPasswordChangedDate"].ToString();
                        OBJ_USER_INFO.CreationDate = dr["CreationDate"].ToString();
                        OBJ_USER_INFO.UnitCode = dr["UnitCode"].ToString();
                        OBJ_USER_INFO.Unit = dr["Unit_Name_Description_Long"].ToString();
                        OBJ_USER_INFO.Email = dr["Email"].ToString();
                        OBJ_USER_INFO.Occupation = dr["Occupation"].ToString();
                        OBJ_USER_INFO.PhoneNumber = dr["PhoneNumber"].ToString();

                        if (dr["IsLockedOut"].ToString() == "True")
                            OBJ_USER_INFO.IsLockedOut = "Yes";
                        else
                            OBJ_USER_INFO.IsLockedOut = "No";
                        if (dr["IsAuditor"].ToString() == "True")
                            OBJ_USER_INFO.IsAuditor = "Yes";
                        else
                            OBJ_USER_INFO.IsAuditor = "No";
                        //OBJ_USER_INFO.IsLockedOut = string.IsNullOrEmpty(dr["IsLockedOut"].ToString()) ? false : Convert.ToBoolean(dr["IsLockedOut"].ToString());
                        //OBJ_USER_INFO.IsAuditor = string.IsNullOrEmpty(dr["IsAuditor"].ToString()) ? false : Convert.ToBoolean(dr["IsAuditor"].ToString());
                        OBJ_USER_INFO.LastChangedDate = dr["LastChangedDate"].ToString();
                        OBJ_USER_INFO.DisplayName = dr["DisplayName"].ToString();
                        if (dr["IsActive"].ToString() == "True")
                            OBJ_USER_INFO.IsActive = "Yes";
                        else
                            OBJ_USER_INFO.IsActive = "No";
                        OBJ_USER_INFO.RegionCode = dr["Region_Code"].ToString();
                        OBJ_USER_INFO.Region = dr["Region"].ToString();
                        LIST_USER.Add(OBJ_USER_INFO);
                    }

                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }
            return LIST_USER;
        }

        public static bool isDuplicateEmail(string pEmail_ID)
        {
            USERS OBJ_USER_INFO = new USERS();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);

            //string SQL = @"SELECT  count(*)  FROM Users where Email= '" + pEmail_ID + "'";
            string SQL = "USP_CHECK_DUPLICATE_EMAIL";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Email", pEmail_ID, false));
            bool isDuplicate = false;
            try
            {
                using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
                {
                    if (dr.Read())
                    {
                        if (dr[0].ToString() == "0")
                        {
                            isDuplicate = true;
                        }
                        else
                        {
                            isDuplicate = false;
                        }

                    }

                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }
            return isDuplicate;
        }

        public static USERS GetSpecificUser(string UserName)
        {

            USERS OBJ_USER_INFO = new USERS();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GET_SPECIFIC_USER";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("UserName", UserName, false));
            try
            {
                using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
                {
                    if (dr.Read())
                    {
                        OBJ_USER_INFO = new USERS();
                        OBJ_USER_INFO.User_ID = dr["UserID"].ToString();
                        OBJ_USER_INFO.FirstName = dr["FirstName"].ToString();
                        OBJ_USER_INFO.LastName = dr["LastName"].ToString();
                        OBJ_USER_INFO.UserName = dr["UserName"].ToString();
                        OBJ_USER_INFO.Email = dr["Email"].ToString();
                        OBJ_USER_INFO.RoleCode = dr["Role_Code"].ToString();
                        OBJ_USER_INFO.FacilityCode = dr["Facility_Code"].ToString();
                        OBJ_USER_INFO.LastLoginDate = dr["LastLoginDate"].ToString();
                        OBJ_USER_INFO.LastPasswordChangedDate = dr["LastPasswordChangedDate"].ToString();
                        OBJ_USER_INFO.CreationDate = dr["CreationDate"].ToString();
                        OBJ_USER_INFO.Force_Password_Changed_Flag = dr["Force_Password_Changed_Flag"].ToString();
                        OBJ_USER_INFO.IsLockedOut = dr["IsLockedOut"].ToString();
                        OBJ_USER_INFO.UnitCode = dr["UnitCode"].ToString();
                        OBJ_USER_INFO.IsAuditor = string.IsNullOrEmpty(dr["IsAuditor"].ToString()) ? "false" : dr["IsAuditor"].ToString();
                        OBJ_USER_INFO.LastChangedDate = dr["LastChangedDate"].ToString();
                        OBJ_USER_INFO.DisplayName = dr["DisplayName"].ToString();
                        OBJ_USER_INFO.TwoWayAuthActivied = Convert.ToBoolean(dr["TwoWayAuthActivied"].ToString());
                        OBJ_USER_INFO.PhoneNumber = dr["PhoneNumber"].ToString();

                    }

                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }
            return OBJ_USER_INFO;
        }

        public void UpdateUserAdmin(USERS Obj, out MembershipCreateStatus status, bool isAdmin)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            //string SQL = string.Empty;
            string SQL = "USP_UPDATE_USER";
            List<DSSQLParam> objList = new List<DSSQLParam>();


            int recAdded = 0;
            try
            {
                objList.Add(new DSSQLParam("UserId", Obj.User_ID, false));
                objList.Add(new DSSQLParam("FirstName", Obj.FirstName, false));
                objList.Add(new DSSQLParam("LastName", Obj.LastName, false));
                objList.Add(new DSSQLParam("UserName", Obj.UserName, false));
                objList.Add(new DSSQLParam("Email", Obj.Email, false));

                objList.Add(new DSSQLParam("PhoneNumber", Obj.PhoneNumber, false));

                objList.Add(new DSSQLParam("Occupation", Obj.Occupation, false));

                objList.Add(new DSSQLParam("Role_Code", Obj.RoleCode, false));

                if (!string.IsNullOrEmpty(Obj.FacilityCode))
                {
                    objList.Add(new DSSQLParam("Facility_Code", Obj.FacilityCode, false));
                }
                if (!string.IsNullOrEmpty(Obj.UnitCode))
                {
                    objList.Add(new DSSQLParam("UnitCode", Obj.UnitCode, false));
                }
                if (!string.IsNullOrEmpty(Obj.RegionCode))
                {
                    objList.Add(new DSSQLParam("Region_Code", Obj.RegionCode, false));
                }
                //if (!string.IsNullOrEmpty(Obj.Password))
                //{
                objList.Add(new DSSQLParam("Password", EncodePassword(Obj.Password), false));
                //}
                objList.Add(new DSSQLParam("IsLockedOut", Obj.IsLockedOut, false));
                objList.Add(new DSSQLParam("IsAuditor", Obj.IsAuditor, false));
                //if (!string.IsNullOrEmpty(Obj.DisplayName))
                //{
                objList.Add(new DSSQLParam("DisplayName", Obj.DisplayName, false));
                //}
                objList.Add(new DSSQLParam("IsAdmin", isAdmin, false));
                objList.Add(new DSSQLParam("IsActive", Obj.IsActive, false));

                if (!string.IsNullOrEmpty(Obj.Lab))
                {
                    objList.Add(new DSSQLParam("Lab", Obj.Lab, false));
                }


                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    status = MembershipCreateStatus.Success;
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    status = MembershipCreateStatus.UserRejected;
                }

            }
            catch (Exception ex)
            {
                status = MembershipCreateStatus.ProviderError;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

        }

        public bool ValidateUserOldPassword(string UserID, string password)
        {

            bool isValid = false;
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL_UserWithPassWord = @"select t.UserID,t.Password from Users t where t.UserID='" + UserID + "'";// and t.Password='" + password + @"' ";

            DbDataReader reader = null;

            string pwd = "";

            try
            {
                //---First Checking the User id is valid or Not
                reader = objCDataAccess.ExecuteReader(objDbCommand, SQL_UserWithPassWord);

                if (reader.Read())
                {

                    if (!string.IsNullOrEmpty(reader["UserID"].ToString()))
                    {
                        pwd = reader["Password"].ToString();
                        if (!string.IsNullOrEmpty(pwd))
                        {
                            if (string.Compare(EncodePassword(password), pwd) == 0)
                            {
                                isValid = true;
                            }
                            else
                            {
                                isValid = false;
                            }

                        }
                    }
                    reader.Close();
                }
                else
                {
                    isValid = false;
                    reader.Close();

                }

            }

            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                objCDataAccess.Dispose(objDbCommand);

            }

            return isValid;
        }

        public bool CustomChangePassword(string UserID, string oldPwd, string newPwd)
        {
            if (!ValidateUserOldPassword(UserID, oldPwd))
                return false;
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            string SQL =
                "UPDATE Users " +
                    " SET Password ='" + EncodePassword(newPwd) + "', LastPasswordChangedDate = getdate(), " + "Force_Password_Changed_Flag=0" +
                    " WHERE UserID ='" + UserID + "'";

            int rowsAffected = 0;

            try
            {

                rowsAffected = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL);
                objDbCommand.Transaction.Commit();
            }
            catch (Exception e)
            {
                objDbCommand.Transaction.Rollback();

                throw e;

            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }

        private string EncodePassword(string password)
        {
            string encodedPassword = password;
            try
            {
                MachineKeySection machineKey;
                ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                //map.ExeConfigFilename = Path.Combine(MapPath("~"), "web.config"); // the root web.config  
                map.ExeConfigFilename = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "web.config";
                string webConfigContents = File.ReadAllText(map.ExeConfigFilename);


                XmlTextReader reader = new XmlTextReader(map.ExeConfigFilename);
                reader.WhitespaceHandling = WhitespaceHandling.None;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(reader);
                reader.Close();


                // Get encryption and decryption key information from the configuration.
                //Configuration cfg =
                //  WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);



                //            xmlDoc.GetElementsByTagName("machineKey")[0].Value;
                machineKey = new MachineKeySection();
                machineKey.ValidationKey = xmlDoc.GetElementsByTagName("machineKey")[0].Attributes["validationKey"].Value;
                //machineKey.DecryptionKey = xmlDoc.GetElementsByTagName("machineKey")[0].Attributes["decryptionKey"].Value;
                //machineKey.Validation = MachineKeyValidation.SHA1;


                HMACSHA1 hash = new HMACSHA1();
                hash.Key = HexToByte(machineKey.ValidationKey);

                encodedPassword =
                  Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
            }
            catch (Exception ex)
            {

            }
            return encodedPassword;
        }

        private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static void CreateErrorLog(string Type, string system, string program, string Severity, string Message, string User_id)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            string SQL = @" INSERT INTO [dbo].[Error_Log]
           ([Error_dt]
           ,[Type]
           ,[System]
           ,[Program]
           ,[Severity]
           ,[Message]
,[User_id])
     VALUES
           (GETDATE()
           ,'" + Type + "','"
               + system + "','"
                + program + "','"
                 + Severity + "','"
                 + Message + "','"
            + User_id + "');";



            int recAdded = 0;
            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL);
                if (recAdded > 0)
                {

                    objDbCommand.Transaction.Commit();
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

        }

        public bool AssignNewPassword(string UserName, string newPwd)
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            string SQL =
                "UPDATE Users " +
                    " SET Password ='" + EncodePassword(newPwd) + "', LastPasswordChangedDate = getdate(), " + "Force_Password_Changed_Flag=0" +
                    " WHERE UserId ='" + UserName + "'";

            int rowsAffected = 0;

            try
            {

                rowsAffected = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL);
                objDbCommand.Transaction.Commit();
            }
            catch (Exception e)
            {
                objDbCommand.Transaction.Rollback();

                throw e;

            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }

        public void UpdateUserAccount(USERS Obj, out MembershipCreateStatus status)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            string SQL = "USP_UPDATE_USER_ACCOUNT";
            List<DSSQLParam> objList = new List<DSSQLParam>();

            int recAdded = 0;
            try
            {
                objList.Add(new DSSQLParam("UserId", Obj.User_ID, false));
                objList.Add(new DSSQLParam("FirstName", Obj.FirstName, false));
                objList.Add(new DSSQLParam("LastName", Obj.LastName, false));
                objList.Add(new DSSQLParam("Email", Obj.Email, false));

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    status = MembershipCreateStatus.Success;
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    status = MembershipCreateStatus.UserRejected;
                }

            }
            catch (Exception ex)
            {
                status = MembershipCreateStatus.ProviderError;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

        }

        /* Peter: Added the below methods */

        #region Old Report Method

        //        public static DataTable GetAuthenticatedUserReports(string ROLES_FOR_USER)
        //        {
        //            DataTable dtUserReports = null;
        //            try
        //            {
        //                CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
        //                DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);

        //                /*
        //                    declare @RoleCode int
        //                    SET @RoleCode =  311

        //                    --RoleCode	Description
        //                    --1		Administrator
        //                    --2		Authenticated User
        //                    --3		Manager

        //                    select * from dbo.ReportsAndRoles
        //                    where IsActive = 1 and 
        //                    case 
        //                        when @RoleCode = 1 then IsAdminRoleVisible
        //                        when @RoleCode = 2 then IsAuthenticatedRoleVisible
        //                        when @RoleCode = 3 then IsManagerRoleVisible
        //                        else IsPublicRoleVisible
        //                    end = 1
        //                 * 
        //                 * -- IsPublicRoleVisible -- SET to 1000
        //                  */
        //                string SQL = string.Format(@"select * from dbo.ReportsAndRoles
        //                where IsActive = 1 and 
        //                case 
        //	                when {0} = 1 then IsAdminRoleVisible
        //	                when {0} = 2 then IsAuthenticatedRoleVisible
        //	                when {0} = 3 then IsManagerRoleVisible
        //	                else IsPublicRoleVisible
        //                end = 1 ORDER BY  SortOrder ASC ;", (string.IsNullOrEmpty(ROLES_FOR_USER) ? "1000" : ROLES_FOR_USER));

        //                DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL);
        //                dtUserReports = new DataTable();
        //                dtUserReports.Load(dr);
        //            }
        //            catch (Exception ex)
        //            {
        //                // handle error
        //            }
        //            return dtUserReports;
        //        }

        #endregion

        //Author : Grishma

        public static List<Report> GetAuthenticatedUserReports(int RoleCode)
        {
            try
            {
                Report OBJ_Report = new Report();
                List<Report> LIST_Report = new List<Report>();
                CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
                DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
                string SQL = "USP_GET_ReportList_By_RolePermission";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("RoleCode", RoleCode, false));
                using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
                {
                    while (dr.Read())
                    {
                        OBJ_Report = new Report();
                        OBJ_Report.ReportID = Convert.ToInt32(dr["ReportID"]);
                        OBJ_Report.ReportCode = Convert.ToString(dr["ReportCode"]);
                        OBJ_Report.ReportName = Convert.ToString(dr["ReportName"]);
                        OBJ_Report.ReportDescription = Convert.ToString(dr["ReportDescription"]);
                        OBJ_Report.ReportServerLocation = Convert.ToString(dr["ReportServerLocation"]);
                        OBJ_Report.ReportCategory = Convert.ToString(dr["ReportCategory"]);
                        OBJ_Report.ReportSubCategory = Convert.ToString(dr["ReportSubCategory"]);
                        OBJ_Report.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        OBJ_Report.SortOrder = (dr["SortOrder"] != null && dr["SortOrder"] != DBNull.Value) ? Convert.ToInt32(dr["SortOrder"]) : 0;
                        OBJ_Report.IsSendEnable = Convert.ToBoolean(dr["IsSendEnable"]);
                        OBJ_Report.ReportKey = Convert.ToString(dr["ReportKey"]);

                        LIST_Report.Add(OBJ_Report);
                    }

                    dr.Close();
                }
                objCDataAccess.Dispose(objDbCommand);
                return LIST_Report;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public static List<Report> GetPublicReports()
        {
            try
            {
                Report OBJ_Report = new Report();
                List<Report> LIST_Report = new List<Report>();
                CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
                DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
                string SQL = "USP_GET_PublicReportList";

                using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, null))
                {
                    while (dr.Read())
                    {
                        OBJ_Report = new Report();
                        OBJ_Report.ReportID = Convert.ToInt32(dr["ReportID"]);
                        OBJ_Report.ReportCode = Convert.ToString(dr["ReportCode"]);
                        OBJ_Report.ReportName = Convert.ToString(dr["ReportName"]);
                        OBJ_Report.ReportDescription = Convert.ToString(dr["ReportDescription"]);
                        OBJ_Report.ReportServerLocation = Convert.ToString(dr["ReportServerLocation"]);
                        OBJ_Report.ReportCategory = Convert.ToString(dr["ReportCategory"]);
                        OBJ_Report.ReportSubCategory = Convert.ToString(dr["ReportSubCategory"]);
                        OBJ_Report.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        OBJ_Report.SortOrder = (dr["SortOrder"] != null && dr["SortOrder"] != DBNull.Value) ? Convert.ToInt32(dr["SortOrder"]) : 0;
                        OBJ_Report.IsSendEnable = Convert.ToBoolean(dr["IsSendEnable"]);
                        OBJ_Report.ReportKey = Convert.ToString(dr["ReportKey"]);

                        LIST_Report.Add(OBJ_Report);
                    }

                    dr.Close();
                }
                objCDataAccess.Dispose(objDbCommand);
                return LIST_Report;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public static DataTable GetAuthenticatedUserReports(string ReportCode, string ReportCategory)
        {
            DataTable dtUserReports = null;

            if (string.IsNullOrEmpty(ReportCode))
                return dtUserReports; // If REPORT_CODE is null or empty string, then this method should not be invoked.

            try
            {
                CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
                DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);

                /*
  
                    --RoleCode	Description
                    --1		Administrator
                    --2		Authenticated User
                    --3		Manager
  
                 * -- IsPublicRoleVisible -- SET to 1000
                  */

                //                string SQL = string.Format(@"select * from dbo.ReportsAndRoles
                //                where IsActive = 1 and ReportCode = '{1}' and
                //                case 
                //	                when {0} = 1 then IsAdminRoleVisible
                //	                when {0} = 2 then IsAuthenticatedRoleVisible
                //	                when {0} = 3 then IsManagerRoleVisible
                //	                else IsPublicRoleVisible
                //                end = 1;", (string.IsNullOrEmpty(ROLES_FOR_USER) ? "1000" : ROLES_FOR_USER), REPORT_CODE);

                string SQL = "USP_GetAuthenticatedUserReportDetails";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                //objList.Add(new DSSQLParam("ReportId", ReportId, false));
                objList.Add(new DSSQLParam("ReportCode", ReportCode, false));
                objList.Add(new DSSQLParam("ReportCategory", ReportCategory, false));
                DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                dtUserReports = new DataTable();
                dtUserReports.Load(dr);
            }
            catch (Exception ex)
            {
                // handle error
                throw ex;
            }
            return dtUserReports;
        }

        public static DataSet GetUserDataForReportFilter(string userId)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_USER_DATA_FOR_REPORT_FILTER";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("UserID", userId, false));
            try
            {
                ds = objCDataAccess.ExecuteDataSet(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                objDbCommand.Transaction.Commit();
            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }
            return ds;
        }

        public static DataSet GetSendButtonStatus(string reportId)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GetSendButtonStatus";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ReportId", reportId, false));
            try
            {
                ds = objCDataAccess.ExecuteDataSet(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                objDbCommand.Transaction.Commit();
            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }
            return ds;
        }

        public static DataSet GetSendButtonStatusForPublicReport(string ReportCode)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GetSendButtonStatus_PublicReport";

            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ReportCode", ReportCode, false));
            try
            {
                ds = objCDataAccess.ExecuteDataSet(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                objDbCommand.Transaction.Commit();
            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }
            return ds;
        }

        public static USERS GetUserId(string EmailId)
        {
            try
            {

                USERS OBJ_USER_INFO = new USERS();
                CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
                DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
                string SQL = "USP_Get_UserId";

                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("EmailId", EmailId, false));

                using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
                {
                    if (dr.Read())
                    {
                        OBJ_USER_INFO = new USERS();
                        OBJ_USER_INFO.User_ID = dr["UserID"].ToString();
                        OBJ_USER_INFO.UserName = dr["UserName"].ToString();
                    }

                    dr.Close();
                }
                objCDataAccess.Dispose(objDbCommand);
                return OBJ_USER_INFO;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public static bool isDuplicateEmailOnEdit(int user_id, string pEmail_ID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);

            //string SQL = @"SELECT  count(*)  FROM Users where Email= '" + pEmail_ID + "'";
            string SQL = "USP_CHECK_DUPLICATE_EMAIL_FOR_UpdateUser";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Email", pEmail_ID, false));
            objList.Add(new DSSQLParam("user_id", user_id, false));
            bool isDuplicate = false;
            try
            {
                using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
                {
                    isDuplicate = !dr.HasRows;
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }
            return isDuplicate;
        }

        public static List<USERS> GetSpecificUserLockedState(string Username)
        {
            USERS OBJ_USER_INFO = new USERS();
            List<USERS> LIST_USER = new List<USERS>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = @"SELECT UserName , IsLockedOut FROM  Users WHERE [UserName]= '" + Username + "'";

            using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL))
            {
                while (dr.Read())
                {
                    OBJ_USER_INFO = new USERS();
                    OBJ_USER_INFO.UserName = dr["UserName"].ToString();
                    if (dr["IsLockedOut"].ToString() == "True")
                        OBJ_USER_INFO.IsLockedOut = "Yes";
                    else
                        OBJ_USER_INFO.IsLockedOut = "No";
                    LIST_USER.Add(OBJ_USER_INFO);
                }

                dr.Close();
            }
            objCDataAccess.Dispose(objDbCommand);
            return LIST_USER;

        }

        public static List<USERS> GetSpecificUserEmail(string email)
        {
            USERS OBJ_USER_INFO = new USERS();
            List<USERS> LIST_USER = new List<USERS>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = @"SELECT Email , IsLockedOut FROM  Users WHERE [Email]= '" + email + "'";

            using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL))
            {
                while (dr.Read())
                {
                    OBJ_USER_INFO = new USERS();
                    OBJ_USER_INFO.Email = dr["Email"].ToString();
                    if (dr["IsLockedOut"].ToString() == "True")
                        OBJ_USER_INFO.IsLockedOut = "Yes";
                    else
                        OBJ_USER_INFO.IsLockedOut = "No";
                    LIST_USER.Add(OBJ_USER_INFO);
                }

                dr.Close();
            }
            objCDataAccess.Dispose(objDbCommand);
            return LIST_USER;

        }
    }
}
