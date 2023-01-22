using Portal.DAL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Diagnostics;
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
    public sealed class PortalMembershipProvider : MembershipProvider
    {
        //
        // Global connection string, generated password length, generic exception message, event log info.
        //

        private int newPasswordLength = 8;
        private string eventSource = "OdbcMembershipProvider";
        private string eventLog = "Application";
        private string exceptionMessage = "An exception occurred. Please check the Event Log.";
        private string connectionString;
        public static string pfailureType;
        public static string vCustomErrorMsg;

        //
        // Used when determining encryption key values.
        //

        private MachineKeySection machineKey;

        //
        // If false, exceptions are thrown to the caller. If true,
        // exceptions are written to the event log.
        //

        private bool pWriteExceptionsToEventLog;

        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }

        #region System.Web.Security.MembershipProvider properties.
        //
        // System.Web.Security.MembershipProvider properties.
        //


        private string pApplicationName;
        private bool pEnablePasswordReset;
        private bool pEnablePasswordRetrieval;
        private bool pRequiresQuestionAndAnswer;
        private bool pRequiresUniqueEmail;
        private int pMaxInvalidPasswordAttempts;
        private int pPasswordAttemptWindow;
        private MembershipPasswordFormat pPasswordFormat;

        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }

        public override bool EnablePasswordReset
        {
            get { return pEnablePasswordReset; }
        }


        public override bool EnablePasswordRetrieval
        {
            get { return pEnablePasswordRetrieval; }
        }


        public override bool RequiresQuestionAndAnswer
        {
            get { return pRequiresQuestionAndAnswer; }
        }


        public override bool RequiresUniqueEmail
        {
            get { return pRequiresUniqueEmail; }
        }


        public override int MaxInvalidPasswordAttempts
        {
            get { return pMaxInvalidPasswordAttempts; }
        }


        public override int PasswordAttemptWindow
        {
            get { return pPasswordAttemptWindow; }
        }


        public override MembershipPasswordFormat PasswordFormat
        {
            get { return pPasswordFormat; }
        }

        private int pMinRequiredNonAlphanumericCharacters;

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return pMinRequiredNonAlphanumericCharacters; }
        }

        private int pMinRequiredPasswordLength;

        public override int MinRequiredPasswordLength
        {
            get { return pMinRequiredPasswordLength; }
        }

        private string pPasswordStrengthRegularExpression;

        public override string PasswordStrengthRegularExpression
        {
            get { return pPasswordStrengthRegularExpression; }
        }

        #endregion

        //
        // System.Configuration.Provider.ProviderBase.Initialize Method
        //

        public override void Initialize(string name, NameValueCollection config)
        {
            //
            // Initialize values from web.config.
            //

            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "OdbcMembershipProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Sample ODBC Membership provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);

            pApplicationName = GetConfigValue(config["applicationName"],
                                            System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            pMaxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            pPasswordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            pMinRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
            pMinRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            pPasswordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
            pEnablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            pEnablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            pRequiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            pRequiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));
            pWriteExceptionsToEventLog = Convert.ToBoolean(GetConfigValue(config["writeExceptionsToEventLog"], "true"));

            string temp_format = config["passwordFormat"];
            if (temp_format == null)
            {
                temp_format = "Hashed";
            }

            switch (temp_format)
            {
                case "Hashed":
                    pPasswordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    pPasswordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    pPasswordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException("Password format not supported.");
            }

            //
            // Initialize OdbcConnection.
            // 

            //connectionString = CDBConnection.GetCDBConnection("application");

            //// Get encryption and decryption key information from the configuration.
            //Configuration cfg =
            //  WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            //machineKey = (MachineKeySection)cfg.GetSection("system.web/machineKey");

            connectionString = CDBConnection.GetCDBConnection("application");


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
            machineKey.DecryptionKey = xmlDoc.GetElementsByTagName("machineKey")[0].Attributes["decryptionKey"].Value;
            machineKey.Validation = MachineKeyValidation.SHA1;

            if (machineKey.ValidationKey.Contains("AutoGenerate"))
                if (PasswordFormat != MembershipPasswordFormat.Clear)
                    throw new ProviderException("Hashed or Encrypted passwords " +
                                                "are not supported with auto-generated keys.");
        }


        //
        // A helper function to retrieve config values from the configuration file.
        //

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }



        #region Membership Method is Customized and overloaded
        public override bool DeleteUser(string UserID, bool deleteAllRelatedData)
        {

            int rowsAffected = 0;

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            string SQL =
                "DELETE FROM Users " +
                    " WHERE UserID ='" + UserID + "'";
            try
            {

                rowsAffected = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL);

                if (rowsAffected > 0)
                {
                    objDbCommand.Transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                string Type = "Error";
                string system = string.Empty;
                string program = "Deleting User";
                string Severity = "Medium";
                string Message = ex.Message;
                //string User_id = BUSessionUtility.BUSessionContainer.USER_ID;
                ClsSecurityManage.CreateErrorLog(Type, system, program, Severity, Message, "");
                objDbCommand.Transaction.Rollback();
                throw ex;
                /*if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ex, "DeleteUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw ex;
                }*/
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
        public override bool ValidateUser(string UserName, string password)
        {

            bool isValid = false;
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_VALIDATE_USER";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("username", UserName, false));
            DbDataReader reader = null;

            string pwd = "";
            try
            {
                using (reader = objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (!string.IsNullOrEmpty(reader["UserID"].ToString()))
                        {
                            if (!string.IsNullOrEmpty(reader["IsLockedOut"].ToString()))
                            {
                                if (Convert.ToBoolean(reader["IsLockedOut"].ToString()) == false)
                                {
                                    pwd = reader["Password"].ToString();
                                    if (!string.IsNullOrEmpty(pwd))
                                    {

                                        if (CheckPassword(password, pwd))
                                        {
                                            isValid = true;
                                        }
                                        else
                                        {
                                            vCustomErrorMsg = "Invalid Password.";
                                            isValid = false;
                                            pfailureType = "password";
                                        }

                                    }
                                }
                                else
                                {
                                    vCustomErrorMsg = "Your account has been locked. Please contact your system administrator or coordinator to unlock it.";
                                    isValid = false;
                                    pfailureType = "lockedout";
                                }

                            }

                        }
                    }
                    else
                    {

                        vCustomErrorMsg = "Invalid Username.";
                        pfailureType = "UserID_error";
                        isValid = false;
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isValid;
        }
        /// <summary>
        /// UpdateFailureCount:A helper method that performs the checks and updates associated with password failure tracking. 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="failureType"></param>
        /// <param name="objCDataAccess"></param>
        /// <param name="objDbCommand"></param>
        private void UpdateFailureCount(string UserID, string failureType)
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            string SQL = @"SELECT isnull(FailedPasswordAttemptCount,0), " +
                                              "  isnull(FailedPasswordAttemptWindowStart,getdate()) " +
                                              "  FROM Users " +
                                              "  WHERE UserID = '" + UserID + "'";
            DbDataReader reader = null;
            DateTime windowStart = new DateTime();
            int failureCount = 0;
            try
            {
                reader = objCDataAccess.ExecuteReader(objDbCommand, SQL);
                if (reader.HasRows)
                {
                    reader.Read();

                    if (failureType == "password")
                    {
                        failureCount = reader.GetInt32(0);
                        windowStart = reader.GetDateTime(1);
                    }

                    /* if (failureType == "passwordAnswer")
                     {
                         failureCount = reader.GetInt32(2);
                         windowStart = reader.GetDateTime(3);
                     }*/
                }

                reader.Close();

                DateTime windowEnd = windowStart.AddMinutes(PasswordAttemptWindow);

                if (failureCount == 0 || DateTime.Now > windowEnd)
                {
                    vCustomErrorMsg = "Invalid Password!!";
                    // First password failure or outside of PasswordAttemptWindow. 
                    // Start a new password failure count from 1 and a new window starting now.

                    if (failureType == "password")
                        SQL = "UPDATE Users " +
                                          "  SET FailedPasswordAttemptCount = " + failureCount +
                                          "     , FailedPasswordAttemptWindowStart = getdate() " +
                                          "  WHERE UserID = '" + UserID + "'";

                    /*if (failureType == "passwordAnswer")
                        SQL = "UPDATE Users " +
                                          "  SET FailedPasswordAnswerAttemptCount = ?, " +
                                          "      FailedPasswordAnswerAttemptWindowStart = ? " +
                                          "  WHERE UserID = ? AND ApplicationName = ?";

                    */

                    if (objCDataAccess.ExecuteNonQuery(objDbCommand, SQL) < 0)
                        throw new ProviderException("Unable to update failure count and window start.");
                }
                else
                {
                    if (failureCount++ >= MaxInvalidPasswordAttempts)
                    {
                        // Password attempts have exceeded the failure threshold. Lock out
                        // the user.
                        //In this case the Temporay Password will be sent to the User's email address
                        vCustomErrorMsg = "A temporary password will be sent to your e-mail address on record. You must change your password at first login";
                    }
                    else
                    {
                        // Password attempts have not exceeded the failure threshold. Update
                        // the failure counts. Leave the window the same.

                        if (failureType == "password")
                            SQL = "UPDATE Users " +
                                              "  SET FailedPasswordAttemptCount = " + failureCount +
                                              "     , FailedPasswordAttemptWindowStart = getdate() " +
                                              "  WHERE UserID = '" + UserID + "'";

                        /*  if (failureType == "passwordAnswer")
                              SQL = "UPDATE Users " +
                                                "  SET FailedPasswordAnswerAttemptCount = ?, " +
                                                "      FailedPasswordAnswerAttemptWindowStart = ? " +
                                                "  WHERE UserID = ? AND ApplicationName = ?";*/



                        if (objCDataAccess.ExecuteNonQuery(objDbCommand, SQL) < 0)
                            throw new ProviderException("Unable to update failure count and window start.");
                    }
                }
                objDbCommand.Transaction.Commit();
            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ex, "UpdateFailureCount");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                objDbCommand.Connection.Close();
            }
        }
        private bool CheckPassword(string password, string dbpassword)
        {
            string pass1 = password;
            string pass2 = dbpassword;
            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Encrypted:
                    pass2 = UnEncodePassword(dbpassword);
                    break;
                case MembershipPasswordFormat.Hashed:
                    pass1 = EncodePassword(password);

                    break;
                default:
                    break;
            }

            if (pass1 == pass2)
            {

                return true;
            }

            return false;
        }
        /// <summary>
        ///EncodePassword: Encrypts, Hashes, or leaves the password clear based on the PasswordFormat.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string EncodePassword(string password)
        {
            string encodedPassword = password;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    encodedPassword =
                      Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    HMACSHA1 hash = new HMACSHA1();
                    hash.Key = HexToByte(machineKey.ValidationKey);
                    encodedPassword =
                      Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
                    break;
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return encodedPassword;


        }
        /// <summary>
        /// UnEncodePassword:Decrypts or leaves the password clear based on the PasswordFormat.
        /// </summary>
        /// <param name="encodedPassword"></param>
        /// <returns></returns>
        private string UnEncodePassword(string encodedPassword)
        {
            string password = encodedPassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    password =
                      Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    throw new ProviderException("Cannot unencode a hashed password.");
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return password;
        }
        /// <summary>
        ///  HexToByte : Converts a hexadecimal string to a byte array. Used to convert encryption key values from the configuration.
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        #endregion

        #region Membership Method which are not implemented yet
        /// <summary>
        /// Membership Method to change the User Password
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>


        //
        // MembershipProvider.ChangePassword
        //

        public override bool ChangePassword(string username, string oldPwd, string newPwd)
        {
            if (!ValidateUser(username, oldPwd))
                return false;


            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(username, newPwd, true);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Change password canceled due to new password validation failure.");


            OdbcConnection conn = new OdbcConnection(connectionString);
            OdbcCommand cmd = new OdbcCommand("UPDATE Users " +
                    " SET Password = ?, LastPasswordChangedDate = ? " +
                    " WHERE Username = ? AND ApplicationName = ?", conn);

            cmd.Parameters.Add("@Password", OdbcType.VarChar, 255).Value = EncodePassword(newPwd);
            cmd.Parameters.Add("@LastPasswordChangedDate", OdbcType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Username", OdbcType.VarChar, 255).Value = username;
            cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = pApplicationName;


            int rowsAffected = 0;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (OdbcException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ChangePassword");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }

        //
        // MembershipProvider.ChangePasswordQuestionAndAnswer
        //

        public override bool ChangePasswordQuestionAndAnswer(string UserID,
                      string password,
                      string newPwdQuestion,
                      string newPwdAnswer)
        {
            if (!ValidateUser(UserID, password))
                return false;

            OdbcConnection conn = new OdbcConnection(connectionString);
            OdbcCommand cmd = new OdbcCommand("UPDATE Users " +
                    " SET PasswordQuestion = ?, PasswordAnswer = ?" +
                    " WHERE UserID = ? AND ApplicationName = ?", conn);

            cmd.Parameters.Add("@Question", OdbcType.VarChar, 255).Value = newPwdQuestion;
            cmd.Parameters.Add("@Answer", OdbcType.VarChar, 255).Value = EncodePassword(newPwdAnswer);
            cmd.Parameters.Add("@UserID", OdbcType.VarChar, 255).Value = UserID;
            cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = pApplicationName;


            int rowsAffected = 0;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (OdbcException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ChangePasswordQuestionAndAnswer");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }



        //
        // MembershipProvider.CreateUser
        //
        /// <summary>
        /// MemberShip Provider Creates User by this method
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="passwordQuestion"></param>
        /// <param name="passwordAnswer"></param>
        /// <param name="isApproved"></param>
        /// <param name="providerUserKey"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public override MembershipUser CreateUser(string UserID,
                 string password,
                 string email,
                 string passwordQuestion,
                 string passwordAnswer,
                 bool isApproved,
                 object providerUserKey,
                 out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(UserID, password, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }



            if (RequiresUniqueEmail && GetUserNameByEmail(email) != "")
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MembershipUser u = GetUser(UserID, false);

            if (u == null)
            {
                DateTime createDate = DateTime.Now;

                if (providerUserKey == null)
                {
                    providerUserKey = Guid.NewGuid();
                }
                else
                {
                    if (!(providerUserKey is Guid))
                    {
                        status = MembershipCreateStatus.InvalidProviderUserKey;
                        return null;
                    }
                }

                OdbcConnection conn = new OdbcConnection(connectionString);
                OdbcCommand cmd = new OdbcCommand("INSERT INTO Users " +
                      " (PKID, UserID, Password, Email, PasswordQuestion, " +
                      " PasswordAnswer, IsApproved," +
                      " Comment, CreationDate, LastPasswordChangedDate, LastActivityDate," +
                      " ApplicationName, IsLockedOut, LastLockedOutDate," +
                      " FailedPasswordAttemptCount, FailedPasswordAttemptWindowStart, " +
                      " FailedPasswordAnswerAttemptCount, FailedPasswordAnswerAttemptWindowStart)" +
                      " Values(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", conn);

                cmd.Parameters.Add("@PKID", OdbcType.UniqueIdentifier).Value = providerUserKey;
                cmd.Parameters.Add("@UserID", OdbcType.VarChar, 255).Value = UserID;
                cmd.Parameters.Add("@Password", OdbcType.VarChar, 255).Value = EncodePassword(password);
                cmd.Parameters.Add("@Email", OdbcType.VarChar, 128).Value = email;
                cmd.Parameters.Add("@PasswordQuestion", OdbcType.VarChar, 255).Value = passwordQuestion;
                cmd.Parameters.Add("@PasswordAnswer", OdbcType.VarChar, 255).Value = EncodePassword(passwordAnswer);
                cmd.Parameters.Add("@IsApproved", OdbcType.Bit).Value = isApproved;
                cmd.Parameters.Add("@Comment", OdbcType.VarChar, 255).Value = "";
                cmd.Parameters.Add("@CreationDate", OdbcType.DateTime).Value = createDate;
                cmd.Parameters.Add("@LastPasswordChangedDate", OdbcType.DateTime).Value = createDate;
                cmd.Parameters.Add("@LastActivityDate", OdbcType.DateTime).Value = createDate;
                cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = pApplicationName;
                cmd.Parameters.Add("@IsLockedOut", OdbcType.Bit).Value = false;
                cmd.Parameters.Add("@LastLockedOutDate", OdbcType.DateTime).Value = createDate;
                cmd.Parameters.Add("@FailedPasswordAttemptCount", OdbcType.Int).Value = 0;
                cmd.Parameters.Add("@FailedPasswordAttemptWindowStart", OdbcType.DateTime).Value = createDate;
                cmd.Parameters.Add("@FailedPasswordAnswerAttemptCount", OdbcType.Int).Value = 0;
                cmd.Parameters.Add("@FailedPasswordAnswerAttemptWindowStart", OdbcType.DateTime).Value = createDate;

                try
                {
                    conn.Open();

                    int recAdded = cmd.ExecuteNonQuery();

                    if (recAdded > 0)
                    {
                        status = MembershipCreateStatus.Success;
                    }
                    else
                    {
                        status = MembershipCreateStatus.UserRejected;
                    }
                }
                catch (OdbcException e)
                {
                    if (WriteExceptionsToEventLog)
                    {
                        WriteToEventLog(e, "CreateUser");
                    }

                    status = MembershipCreateStatus.ProviderError;
                }
                finally
                {
                    conn.Close();
                }


                return GetUser(UserID, false);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }


            return null;
        }

        //
        // MembershipProvider.GetAllUsers
        //

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            OdbcConnection conn = new OdbcConnection(connectionString);
            OdbcCommand cmd = new OdbcCommand("SELECT Count(*) FROM Users " +
                                              "WHERE ApplicationName = ?", conn);
            cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = ApplicationName;

            MembershipUserCollection users = new MembershipUserCollection();

            OdbcDataReader reader = null;
            totalRecords = 0;

            try
            {
                conn.Open();
                totalRecords = (int)cmd.ExecuteScalar();

                if (totalRecords <= 0) { return users; }

                cmd.CommandText = "SELECT PKID, UserID, Email, PasswordQuestion," +
                         " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," +
                         " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate " +
                         " FROM Users " +
                         " WHERE ApplicationName = ? " +
                         " ORDER BY UserID Asc";

                reader = cmd.ExecuteReader();

                int counter = 0;
                int startIndex = pageSize * pageIndex;
                int endIndex = startIndex + pageSize - 1;

                while (reader.Read())
                {
                    if (counter >= startIndex)
                    {
                        MembershipUser u = GetUserFromReader(reader);
                        users.Add(u);
                    }

                    if (counter >= endIndex) { cmd.Cancel(); }

                    counter++;
                }
            }
            catch (OdbcException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetAllUsers ");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            return users;
        }


        //
        // MembershipProvider.GetNumberOfUsersOnline
        //

        public override int GetNumberOfUsersOnline()
        {

            TimeSpan onlineSpan = new TimeSpan(0, System.Web.Security.Membership.UserIsOnlineTimeWindow, 0);
            DateTime compareTime = DateTime.Now.Subtract(onlineSpan);

            OdbcConnection conn = new OdbcConnection(connectionString);
            OdbcCommand cmd = new OdbcCommand("SELECT Count(*) FROM Users " +
                    " WHERE LastActivityDate > ? AND ApplicationName = ?", conn);

            cmd.Parameters.Add("@CompareDate", OdbcType.DateTime).Value = compareTime;
            cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = pApplicationName;

            int numOnline = 0;

            try
            {
                conn.Open();

                numOnline = (int)cmd.ExecuteScalar();
            }
            catch (OdbcException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetNumberOfUsersOnline");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }

            return numOnline;
        }



        //
        // MembershipProvider.GetPassword
        //

        public override string GetPassword(string UserID, string answer)
        {
            if (!EnablePasswordRetrieval)
            {
                throw new ProviderException("Password Retrieval Not Enabled.");
            }

            if (PasswordFormat == MembershipPasswordFormat.Hashed)
            {
                throw new ProviderException("Cannot retrieve Hashed passwords.");
            }

            OdbcConnection conn = new OdbcConnection(connectionString);
            OdbcCommand cmd = new OdbcCommand("SELECT Password, PasswordAnswer, IsLockedOut FROM Users " +
                  " WHERE UserID = ? AND ApplicationName = ?", conn);

            cmd.Parameters.Add("@UserID", OdbcType.VarChar, 255).Value = UserID;
            cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = pApplicationName;

            string password = "";
            string passwordAnswer = "";
            OdbcDataReader reader = null;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.HasRows)
                {
                    reader.Read();

                    if (reader.GetBoolean(2))
                        throw new MembershipPasswordException("The supplied user is locked out.");

                    password = reader.GetString(0);
                    passwordAnswer = reader.GetString(1);
                }
                else
                {
                    throw new MembershipPasswordException("The supplied user name is not found.");
                }
            }
            catch (OdbcException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetPassword");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }


            if (RequiresQuestionAndAnswer && !CheckPassword(answer, passwordAnswer))
            {
                //UpdateFailureCount(UserID, "passwordAnswer"); Temporarily Comment Out

                throw new MembershipPasswordException("Incorrect password answer.");
            }


            if (PasswordFormat == MembershipPasswordFormat.Encrypted)
            {
                password = UnEncodePassword(password);
            }

            return password;
        }



        //
        // MembershipProvider.GetUser(string, bool)
        //

        public override MembershipUser GetUser(string UserID, bool userIsOnline)
        {
            /* OdbcConnection conn = new OdbcConnection(connectionString);
             OdbcCommand cmd = new OdbcCommand("SELECT PKID, UserID, Email, PasswordQuestion," +
                  " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," +
                  " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate" +
                  " FROM Users WHERE UserID = ? AND ApplicationName = ?", conn);

             cmd.Parameters.Add("@UserID", OdbcType.VarChar, 255).Value = UserID;
             cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = pApplicationName;*/

            MembershipUser u = null;
            OdbcDataReader reader = null;
            /*
                        try
                        {
                            conn.Open();

                            reader = cmd.ExecuteReader();

                            if (reader.HasRows)
                            {
                                reader.Read();
                                u = GetUserFromReader(reader);

                                if (userIsOnline)
                                {
                                    OdbcCommand updateCmd = new OdbcCommand("UPDATE Users " +
                                              "SET LastActivityDate = ? " +
                                              "WHERE UserID = ? AND Applicationname = ?", conn);

                                    updateCmd.Parameters.Add("@LastActivityDate", OdbcType.DateTime).Value = DateTime.Now;
                                    updateCmd.Parameters.Add("@UserID", OdbcType.VarChar, 255).Value = UserID;
                                    updateCmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = pApplicationName;

                                    updateCmd.ExecuteNonQuery();
                                }
                            }

                        }
                        catch (OdbcException e)
                        {
                            if (WriteExceptionsToEventLog)
                            {
                                WriteToEventLog(e, "GetUser(String, Boolean)");

                                throw new ProviderException(exceptionMessage);
                            }
                            else
                            {
                                throw e;
                            }
                        }
                        finally
                        {
                            if (reader != null) { reader.Close(); }

                            conn.Close();
                        }
                        */
            return u;
        }



        //
        // MembershipProvider.GetUser(object, bool)
        //

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            OdbcConnection conn = new OdbcConnection(connectionString);
            OdbcCommand cmd = new OdbcCommand("SELECT PKID, UserID, Email, PasswordQuestion," +
                  " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," +
                  " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate" +
                  " FROM Users WHERE PKID = ?", conn);

            cmd.Parameters.Add("@PKID", OdbcType.UniqueIdentifier).Value = providerUserKey;

            MembershipUser u = null;
            OdbcDataReader reader = null;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    u = GetUserFromReader(reader);

                    if (userIsOnline)
                    {
                        OdbcCommand updateCmd = new OdbcCommand("UPDATE Users " +
                                  "SET LastActivityDate = ? " +
                                  "WHERE PKID = ?", conn);

                        updateCmd.Parameters.Add("@LastActivityDate", OdbcType.DateTime).Value = DateTime.Now;
                        updateCmd.Parameters.Add("@PKID", OdbcType.UniqueIdentifier).Value = providerUserKey;

                        updateCmd.ExecuteNonQuery();
                    }
                }

            }
            catch (OdbcException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetUser(Object, Boolean)");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }

                conn.Close();
            }

            return u;
        }


        //
        // GetUserFromReader
        //    A helper function that takes the current row from the OdbcDataReader
        // and hydrates a MembershiUser from the values. Called by the 
        // MembershipUser.GetUser implementation.
        //

        private MembershipUser GetUserFromReader(OdbcDataReader reader)
        {
            object providerUserKey = reader.GetValue(0);
            string UserID = reader.GetString(1);
            string email = reader.GetString(2);

            string passwordQuestion = "";
            if (reader.GetValue(3) != DBNull.Value)
                passwordQuestion = reader.GetString(3);

            string comment = "";
            if (reader.GetValue(4) != DBNull.Value)
                comment = reader.GetString(4);

            bool isApproved = reader.GetBoolean(5);
            bool isLockedOut = reader.GetBoolean(6);
            DateTime creationDate = reader.GetDateTime(7);

            DateTime lastLoginDate = new DateTime();
            if (reader.GetValue(8) != DBNull.Value)
                lastLoginDate = reader.GetDateTime(8);

            DateTime lastActivityDate = reader.GetDateTime(9);
            DateTime lastPasswordChangedDate = reader.GetDateTime(10);

            DateTime lastLockedOutDate = new DateTime();
            if (reader.GetValue(11) != DBNull.Value)
                lastLockedOutDate = reader.GetDateTime(11);

            MembershipUser u = new MembershipUser(this.Name,
                                                  UserID,
                                                  providerUserKey,
                                                  email,
                                                  passwordQuestion,
                                                  comment,
                                                  isApproved,
                                                  isLockedOut,
                                                  creationDate,
                                                  lastLoginDate,
                                                  lastActivityDate,
                                                  lastPasswordChangedDate,
                                                  lastLockedOutDate);

            return u;
        }


        //
        // MembershipProvider.UnlockUser
        //

        public override bool UnlockUser(string UserID)
        {
            OdbcConnection conn = new OdbcConnection(connectionString);
            OdbcCommand cmd = new OdbcCommand("UPDATE Users " +
                                              " SET IsLockedOut = False, LastLockedOutDate = ? " +
                                              " WHERE UserID = ? AND ApplicationName = ?", conn);

            cmd.Parameters.Add("@LastLockedOutDate", OdbcType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@UserID", OdbcType.VarChar, 255).Value = UserID;
            cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = pApplicationName;

            int rowsAffected = 0;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (OdbcException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "UnlockUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }

            if (rowsAffected > 0)
                return true;

            return false;
        }


        //
        // MembershipProvider.GetUserNameByEmail
        //

        public override string GetUserNameByEmail(string email)
        {
            OdbcConnection conn = new OdbcConnection(connectionString);
            OdbcCommand cmd = new OdbcCommand("SELECT UserID" +
                  " FROM Users WHERE Email = ? AND ApplicationName = ?", conn);

            cmd.Parameters.Add("@Email", OdbcType.VarChar, 128).Value = email;
            cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = pApplicationName;

            string UserID = "";

            try
            {
                conn.Open();

                UserID = (string)cmd.ExecuteScalar();
            }
            catch (OdbcException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetUserIDByEmail");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }

            if (UserID == null)
                UserID = "";

            return UserID;
        }




        //
        // MembershipProvider.ResetPassword
        //

        public override string ResetPassword(string UserID, string answer)
        {
            if (!EnablePasswordReset)
            {
                throw new NotSupportedException("Password reset is not enabled.");
            }

            if (answer == null && RequiresQuestionAndAnswer)
            {
                //UpdateFailureCount(UserID, "passwordAnswer");

                throw new ProviderException("Password answer required for password reset.");
            }

            string newPassword =
              System.Web.Security.Membership.GeneratePassword(newPasswordLength, MinRequiredNonAlphanumericCharacters);


            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(UserID, newPassword, true);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Reset password canceled due to password validation failure.");


            OdbcConnection conn = new OdbcConnection(connectionString);
            OdbcCommand cmd = new OdbcCommand("SELECT PasswordAnswer, IsLockedOut FROM Users " +
                  " WHERE UserID = ? AND ApplicationName = ?", conn);

            cmd.Parameters.Add("@UserID", OdbcType.VarChar, 255).Value = UserID;
            cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = pApplicationName;

            int rowsAffected = 0;
            string passwordAnswer = "";
            OdbcDataReader reader = null;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.HasRows)
                {
                    reader.Read();

                    if (reader.GetBoolean(1))
                        throw new MembershipPasswordException("The supplied user is locked out.");

                    passwordAnswer = reader.GetString(0);
                }
                else
                {
                    throw new MembershipPasswordException("The supplied user name is not found.");
                }

                if (RequiresQuestionAndAnswer && !CheckPassword(answer, passwordAnswer))
                {
                    // UpdateFailureCount(UserID, "passwordAnswer");

                    throw new MembershipPasswordException("Incorrect password answer.");
                }

                OdbcCommand updateCmd = new OdbcCommand("UPDATE Users " +
                    " SET Password = ?, LastPasswordChangedDate = ?" +
                    " WHERE UserID = ? AND ApplicationName = ? AND IsLockedOut = False", conn);

                updateCmd.Parameters.Add("@Password", OdbcType.VarChar, 255).Value = EncodePassword(newPassword);
                updateCmd.Parameters.Add("@LastPasswordChangedDate", OdbcType.DateTime).Value = DateTime.Now;
                updateCmd.Parameters.Add("@UserID", OdbcType.VarChar, 255).Value = UserID;
                updateCmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = pApplicationName;

                rowsAffected = updateCmd.ExecuteNonQuery();
            }
            catch (OdbcException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ResetPassword");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            if (rowsAffected > 0)
            {
                return newPassword;
            }
            else
            {
                throw new MembershipPasswordException("User not found, or user is locked out. Password not Reset.");
            }
        }


        //
        // MembershipProvider.UpdateUser
        //

        public override void UpdateUser(MembershipUser user)
        {
            OdbcConnection conn = new OdbcConnection(connectionString);
            OdbcCommand cmd = new OdbcCommand("UPDATE Users " +
                    " SET Email = ?, Comment = ?," +
                    " IsApproved = ?" +
                    " WHERE UserID = ? AND ApplicationName = ?", conn);

            cmd.Parameters.Add("@Email", OdbcType.VarChar, 128).Value = user.Email;
            cmd.Parameters.Add("@Comment", OdbcType.VarChar, 255).Value = user.Comment;
            cmd.Parameters.Add("@IsApproved", OdbcType.Bit).Value = user.IsApproved;
            cmd.Parameters.Add("@UserID", OdbcType.VarChar, 255).Value = user.UserName;
            cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = pApplicationName;


            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (OdbcException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "UpdateUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }
        }




        //
        // MembershipProvider.FindUsersByName
        //

        public override MembershipUserCollection FindUsersByName(string UserIDToMatch, int pageIndex, int pageSize, out int totalRecords)
        {

            OdbcConnection conn = new OdbcConnection(connectionString);
            OdbcCommand cmd = new OdbcCommand("SELECT Count(*) FROM Users " +
                      "WHERE UserID LIKE ? AND ApplicationName = ?", conn);
            cmd.Parameters.Add("@UserIDSearch", OdbcType.VarChar, 255).Value = UserIDToMatch;
            cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = pApplicationName;

            MembershipUserCollection users = new MembershipUserCollection();

            OdbcDataReader reader = null;

            try
            {
                conn.Open();
                totalRecords = (int)cmd.ExecuteScalar();

                if (totalRecords <= 0) { return users; }

                cmd.CommandText = "SELECT PKID, UserID, Email, PasswordQuestion," +
                  " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," +
                  " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate " +
                  " FROM Users " +
                  " WHERE UserID LIKE ? AND ApplicationName = ? " +
                  " ORDER BY UserID Asc";

                reader = cmd.ExecuteReader();

                int counter = 0;
                int startIndex = pageSize * pageIndex;
                int endIndex = startIndex + pageSize - 1;

                while (reader.Read())
                {
                    if (counter >= startIndex)
                    {
                        MembershipUser u = GetUserFromReader(reader);
                        users.Add(u);
                    }

                    if (counter >= endIndex) { cmd.Cancel(); }

                    counter++;
                }
            }
            catch (OdbcException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "FindUsersByName");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }

                conn.Close();
            }

            return users;
        }

        //
        // MembershipProvider.FindUsersByEmail
        //

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            OdbcConnection conn = new OdbcConnection(connectionString);
            OdbcCommand cmd = new OdbcCommand("SELECT Count(*) FROM Users " +
                                              "WHERE Email LIKE ? AND ApplicationName = ?", conn);
            cmd.Parameters.Add("@EmailSearch", OdbcType.VarChar, 255).Value = emailToMatch;
            cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = ApplicationName;

            MembershipUserCollection users = new MembershipUserCollection();

            OdbcDataReader reader = null;
            totalRecords = 0;

            try
            {
                conn.Open();
                totalRecords = (int)cmd.ExecuteScalar();

                if (totalRecords <= 0) { return users; }

                cmd.CommandText = "SELECT PKID, UserID, Email, PasswordQuestion," +
                         " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," +
                         " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate " +
                         " FROM Users " +
                         " WHERE Email LIKE ? AND ApplicationName = ? " +
                         " ORDER BY UserID Asc";

                reader = cmd.ExecuteReader();

                int counter = 0;
                int startIndex = pageSize * pageIndex;
                int endIndex = startIndex + pageSize - 1;

                while (reader.Read())
                {
                    if (counter >= startIndex)
                    {
                        MembershipUser u = GetUserFromReader(reader);
                        users.Add(u);
                    }

                    if (counter >= endIndex) { cmd.Cancel(); }

                    counter++;
                }
            }
            catch (OdbcException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "FindUsersByEmail");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }

                conn.Close();
            }

            return users;
        }

        #endregion

        //
        // WriteToEventLog
        //   A helper function that writes exception detail to the event log. Exceptions
        // are written to the event log as a security measure to avoid private database
        // details from being returned to the browser. If a method does not return a status
        // or boolean indicating the action succeeded or failed, a generic exception is also 
        // thrown by the caller.
        //

        private void WriteToEventLog(Exception e, string action)
        {
            EventLog log = new EventLog();
            log.Source = eventSource;
            log.Log = eventLog;

            string message = "An exception occurred communicating with the data source.\n\n";
            message += "Action: " + action + "\n\n";
            message += "Exception: " + e.ToString();

            log.WriteEntry(message);
        }
    }
}
