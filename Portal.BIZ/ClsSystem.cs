using System;
using System.Collections.Generic;
using System.Linq;
using Portal.DAL;
using Portal.Model;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using Portal.Utility;
using Portal.BIZ.HelperModel;
using Newtonsoft.Json;
using System.Web;
using Portal.Provider.Model;
using Portal.Model.deserialize;
using Action = Portal.Model.deserialize.Action;
using System.Web.Script.Serialization;

namespace Portal.BIZ
{
    public class ClsSystem
    {
        
        #region Method Add by Ujjaval For DimFacility Table Operation [ fetch/add/edit/delete ]

        public static bool CreateDimFacility(string facilitydesc, string facilitytype, string RegionCode,
            string DescriptionLong, string DescriptionShort, string IsActive)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_DIMFACILITY";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Description", facilitydesc, false));
            objList.Add(new DSSQLParam("FacilityTypeCode", facilitytype, false));
            objList.Add(new DSSQLParam("RegionCode", RegionCode, false));
            objList.Add(new DSSQLParam("DescriptionLong", DescriptionLong, false));
            objList.Add(new DSSQLParam("DescriptionShort", DescriptionShort, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var inseredID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_DIMFACILITY",
                        ModuleID = Convert.ToInt32(inseredID),
                        TableName = LogTable.DimFacility.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static bool UpdateDimFacility(string facilityid, string facilitydesc, string facilitytypecode,
            string RegionCode, string DescriptionLong, string DescriptionShort, string IsActive)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_UPDATE_DIMFACILITY";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("FacilityCode", facilityid, false));
            objList.Add(new DSSQLParam("Description", facilitydesc, false));
            objList.Add(new DSSQLParam("FacilityTypeCode", facilitytypecode, false));
            objList.Add(new DSSQLParam("RegionCode", RegionCode, false));
            objList.Add(new DSSQLParam("DescriptionLong", DescriptionLong, false));
            objList.Add(new DSSQLParam("DescriptionShort", DescriptionShort, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_UPDATE_DIMFACILITY",
                        ModuleID = Convert.ToInt32(facilityid),
                        TableName = LogTable.DimFacility.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static List<DimFacility> GetAllDimFacilityList()
        {
            DimFacility OBJ_DimFacility = new DimFacility();
            List<DimFacility> LIST_DimFacility = new List<DimFacility>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GETALL_DIMFACILITY";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            using (DbDataReader dr =
                objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
            {
                while (dr.Read())
                {
                    OBJ_DimFacility = new DimFacility();
                    OBJ_DimFacility.FacilityCode = Convert.ToInt32(dr["FacilityCode"].ToString());
                    OBJ_DimFacility.Description = dr["DimFacilityDesc"].ToString();
                    OBJ_DimFacility.FacilityTypeCode = Convert.ToInt32(dr["FacilityTypeCode"].ToString());
                    OBJ_DimFacility.FacilityTypeDesc = dr["FacilityTypeDesc"].ToString();
                    OBJ_DimFacility.LastChangedDate = dr["LastChangedDate"].ToString();
                    OBJ_DimFacility.RegionCode = Convert.ToInt32(dr["RegionCode"].ToString());
                    OBJ_DimFacility.RegionCodeDesc = dr["RegionCodeDesc"].ToString();
                    OBJ_DimFacility.DescriptionLong = dr["DescriptionLong"].ToString();
                    OBJ_DimFacility.DescriptionShort = dr["DescriptionShort"].ToString();
                    if (dr["IsActive"].ToString() == "True")
                        OBJ_DimFacility.IsActive = "Yes";
                    else
                        OBJ_DimFacility.IsActive = "No";
                    //OBJ_DimFacility.IsActive = dr["IsActive"].ToString();
                    LIST_DimFacility.Add(OBJ_DimFacility);
                }

                dr.Close();
            }

            objCDataAccess.Dispose(objDbCommand);
            return LIST_DimFacility;
        }

        public static bool DeleteDimFacility(string FacilityCode)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_DELETE_DIMFACILITY";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("FacilityCode", FacilityCode, false));
            int recAdded = 0;
            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "USP_DELETE_DIMFACILITY",
                        ModuleID = Convert.ToInt32(FacilityCode),
                        TableName = LogTable.DimFacility.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static bool CheckFacilityInUnit(string FacilityCode)
        {
            bool exsit = false;
            DataSet dsTempOBSData = new DataSet();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = @" SELECT * FROM DimUnit WHERE FacilityCode=" + FacilityCode;
            dsTempOBSData = objCDataAccess.ExecuteDataSet(objDbCommand, SQL);
            objCDataAccess.Dispose(objDbCommand);

            if (dsTempOBSData.Tables[0].Rows.Count > 0)
                exsit = true;
            else
                exsit = false;

            return exsit;
        }

        public static DataSet GetAllFacilityType()
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GETALL_FACILITY_TYPE";
            List<DSSQLParam> objList = new List<DSSQLParam>();
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

        #endregion

        #region Method Add by Ujjaval For SuggestedPriorityType Table Operation [ fetch/add/edit/delete ]

        public static List<SuggestedPriority> GetAllSuggestedPriorityTypeList()
        {
            SuggestedPriority OBJ_Dim_SuggestedPriority = new SuggestedPriority();
            List<SuggestedPriority> LIST_Dim_SuggestedPriority = new List<SuggestedPriority>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GETALL_SuggestedPriorityType";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            using (DbDataReader dr =
                objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
            {
                while (dr.Read())
                {
                    OBJ_Dim_SuggestedPriority = new SuggestedPriority();
                    OBJ_Dim_SuggestedPriority.ID = Convert.ToString(dr["ID"].ToString());
                    OBJ_Dim_SuggestedPriority.Description = dr["Description"].ToString();
                    if (dr["IsActive"].ToString() == "True")
                        OBJ_Dim_SuggestedPriority.IsActive = "Yes";
                    else
                        OBJ_Dim_SuggestedPriority.IsActive = "No";
                    LIST_Dim_SuggestedPriority.Add(OBJ_Dim_SuggestedPriority);
                }

                dr.Close();
            }

            objCDataAccess.Dispose(objDbCommand);
            return LIST_Dim_SuggestedPriority;
        }

        public static bool DeleteSuggestedPriorityType(string ID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_Delete_SuggestedPriorityType";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ID", ID, false));
            int recAdded = 0;
            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "USP_Delete_SuggestedPriorityType",
                        ModuleID = Convert.ToInt32(ID),
                        TableName = LogTable.SuggestedPriorityType.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }


        public static bool CreateSuggestedPriorityType(string description, string IsActive)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_SuggestedPriorityType";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Description", description, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var inseretedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_SuggestedPriorityType",
                        ModuleID = Convert.ToInt32(inseretedID),
                        TableName = LogTable.SuggestedPriorityType.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static bool UpdateSuggestedPriorityType(string ID, string Description, string IsActive
            )
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_Update_SuggestedPriorityType";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Description", Description, false));
            objList.Add(new DSSQLParam("ID", ID, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
           

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_Update_SuggestedPriorityType",
                        ModuleID = Convert.ToInt32(ID),
                        TableName = LogTable.SuggestedPriorityType.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }
        #endregion

        #region Method Add by Ujjaval For PossibleConsequence Table Operation [ fetch/add/edit/delete ]

        public static List<PossibleConsequence> GetAllPossibleConsequence()
        {
            PossibleConsequence OBJ_Dim_SuggestedPriority = new PossibleConsequence();
            List<PossibleConsequence> LIST_Dim_SuggestedPriority = new List<PossibleConsequence>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GETALL_PossibleConsequence";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            using (DbDataReader dr =
                objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
            {
                while (dr.Read())
                {
                    OBJ_Dim_SuggestedPriority = new PossibleConsequence();
                    OBJ_Dim_SuggestedPriority.ID = Convert.ToString(dr["ID"].ToString());
                    OBJ_Dim_SuggestedPriority.Description = dr["Description"].ToString();
                    if (dr["IsActive"].ToString() == "True")
                        OBJ_Dim_SuggestedPriority.IsActive = "Yes";
                    else
                        OBJ_Dim_SuggestedPriority.IsActive = "No";
                    LIST_Dim_SuggestedPriority.Add(OBJ_Dim_SuggestedPriority);
                }

                dr.Close();
            }

            objCDataAccess.Dispose(objDbCommand);
            return LIST_Dim_SuggestedPriority;
        }

        public static bool DeleteSPossibleConsequence(string ID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_DELETE_PossibleConsequence";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ID", ID, false));
            int recAdded = 0;
            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "USP_DELETE_PossibleConsequence",
                        ModuleID = Convert.ToInt32(ID),
                        TableName = LogTable.SuggestedPriorityType.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }


        public static bool CreatePossibleConsequence(string description, string IsActive)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_PossibleConsequence";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Description", description, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var inseretedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_PossibleConsequence",
                        ModuleID = Convert.ToInt32(inseretedID),
                        TableName = LogTable.SuggestedPriorityType.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static bool UpdatePossibleConsequence(string ID, string Description, string IsActive
            )
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_UPDATE_PossibleConsequence";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Description", Description, false));
            objList.Add(new DSSQLParam("ID", ID, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));


            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_UPDATE_PossibleConsequence",
                        ModuleID = Convert.ToInt32(ID),
                        TableName = LogTable.PossibleConsequence.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }
        #endregion


        #region Method Add by Ujjaval For FacilityType Table Operation [ fetch/add/edit/delete ]

        public static bool CreateFacilityType(string description, string IsActive, string DescriptionShort,
            string DescriptionLong)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_FACILITYType";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Description", description, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("DescriptionShort", DescriptionShort, false));
            objList.Add(new DSSQLParam("DescriptionLong", DescriptionLong, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var insertedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_FACILITYType",
                        ModuleID = Convert.ToInt32(insertedID),
                        TableName = LogTable.FacilityType.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static bool UpdateFacilityType(string facilityTypeCode, string description, string IsActive,
            string DescriptionShort, string DescriptionLong)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_UPDATE_FACILITYType";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Description", description, false));
            objList.Add(new DSSQLParam("FacilityTypeCode", facilityTypeCode, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("DescriptionShort", DescriptionShort, false));
            objList.Add(new DSSQLParam("DescriptionLong", DescriptionLong, false));


            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_UPDATE_FACILITYType",
                        ModuleID = Convert.ToInt32(facilityTypeCode),
                        TableName = LogTable.FacilityType.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static List<FacilityType> GetAllFacilityTypeList()
        {
            FacilityType OBJ_DimFacility = new FacilityType();
            List<FacilityType> LIST_DimFacility = new List<FacilityType>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GETALL_FacilityType";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            using (DbDataReader dr =
                objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
            {
                while (dr.Read())
                {
                    OBJ_DimFacility = new FacilityType();
                    OBJ_DimFacility.Description = dr["Description"].ToString();
                    OBJ_DimFacility.FacilityTypeCode = dr["FacilityTypeCode"].ToString();
                    OBJ_DimFacility.DescriptionShort = dr["DescriptionShort"].ToString();
                    OBJ_DimFacility.DescriptionLong = dr["DescriptionLong"].ToString();
                    if (dr["IsActive"].ToString() == "True")
                        OBJ_DimFacility.IsActive = "Yes";
                    else
                        OBJ_DimFacility.IsActive = "No";
                    LIST_DimFacility.Add(OBJ_DimFacility);
                }

                dr.Close();
            }

            objCDataAccess.Dispose(objDbCommand);
            return LIST_DimFacility;
        }

        public static bool DeleteFacilityType(string FacilityCode)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_DELETE_FACILITYType";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("FacilityTypeCode", FacilityCode, false));
            int recAdded = 0;
            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "USP_DELETE_FACILITYType",
                        ModuleID = Convert.ToInt32(FacilityCode),
                        TableName = LogTable.FacilityType.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        #endregion

        #region Method Add by Ujjaval For ProgramType Table Operation [ fetch/add/edit/delete ]

        public static bool CreateProgramType(string description, string IsActive, string DescriptionShort)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_ProgramType";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Description", description, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("DescriptionShort", DescriptionShort, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));


            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var inseretedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();


                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_ProgramType",
                        ModuleID = Convert.ToInt32(inseretedID),
                        TableName = LogTable.ProgramType.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static bool UpdateProgramType(string ProgramTypeCode, string description, string IsActive,
            string DescriptionShort)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_UPDATE_ProgramType";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Description", description, false));
            objList.Add(new DSSQLParam("ProgramTypeCode", ProgramTypeCode, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("DescriptionShort", DescriptionShort, false));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_UPDATE_ProgramType",
                        ModuleID = Convert.ToInt32(ProgramTypeCode),
                        TableName = LogTable.ProgramType.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static List<ProgramType> GetAllProgramTypeList()
        {
            ProgramType OBJ_DimFacility = new ProgramType();
            List<ProgramType> LIST_DimFacility = new List<ProgramType>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GETALL_ProgramType";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            using (DbDataReader dr =
                objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
            {
                while (dr.Read())
                {
                    OBJ_DimFacility = new ProgramType();
                    OBJ_DimFacility.Description = dr["Description"].ToString();
                    OBJ_DimFacility.ProgramTypeCode = dr["ProgramTypeCode"].ToString();
                    //OBJ_DimFacility.IsActive = dr["IsActive"].ToString();
                    if (dr["IsActive"].ToString() == "True")
                        OBJ_DimFacility.IsActive = "Yes";
                    else
                        OBJ_DimFacility.IsActive = "No";
                    OBJ_DimFacility.DescriptionShort = dr["DescriptionShort"].ToString();
                    LIST_DimFacility.Add(OBJ_DimFacility);
                }

                dr.Close();
            }

            objCDataAccess.Dispose(objDbCommand);
            return LIST_DimFacility;
        }

        public static bool DeleteProgramType(string ProgramTypeCode)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_DELETE_ProgramType";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ProgramTypeCode", ProgramTypeCode, false));

            int recAdded = 0;
            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "USP_DELETE_ProgramType",
                        ModuleID = Convert.ToInt32(ProgramTypeCode),
                        TableName = LogTable.ProgramType.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        #endregion

        #region Method Add by Ujjaval For UnitType1 Table Operation [ fetch/add/edit/delete ]

        public static bool CreateUnitType1(string description, string IsActive, string DescriptionShort)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_UnitType1";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Description", description, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("DescriptionShort", DescriptionShort, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var inseretedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_UnitType1",
                        ModuleID = Convert.ToInt32(inseretedID),
                        TableName = LogTable.UnitType1.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static bool UpdateUnitType1(string UnitType1Code, string description, string IsActive,
            string DescriptionShort)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_UPDATE_UnitType1";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Description", description, false));
            objList.Add(new DSSQLParam("UnitType1Code", UnitType1Code, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("DescriptionShort", DescriptionShort, false));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_UPDATE_UnitType1",
                        ModuleID = Convert.ToInt32(UnitType1Code),
                        TableName = LogTable.UnitType1.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static List<UnitType1> GetAllUnitType1List()
        {
            UnitType1 OBJ_DimFacility = new UnitType1();
            List<UnitType1> LIST_DimFacility = new List<UnitType1>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GETALL_UnitType1";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            using (DbDataReader dr =
                objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
            {
                while (dr.Read())
                {
                    OBJ_DimFacility = new UnitType1();
                    OBJ_DimFacility.Description = dr["Description"].ToString();
                    OBJ_DimFacility.UnitType1Code = dr["UnitType1Code"].ToString();
                    if (dr["IsActive"].ToString() == "True")
                        OBJ_DimFacility.IsActive = "Yes";
                    else
                        OBJ_DimFacility.IsActive = "No";
                    OBJ_DimFacility.DescriptionShort = dr["DescriptionShort"].ToString();
                    LIST_DimFacility.Add(OBJ_DimFacility);
                }

                dr.Close();
            }

            objCDataAccess.Dispose(objDbCommand);
            return LIST_DimFacility;
        }

        public static bool DeleteUnitType1(string Unit1TypeCode)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_DELETE_UnitType1";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("UnitType1Code", Unit1TypeCode, false));
            int recAdded = 0;
            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "USP_DELETE_UnitType1",
                        ModuleID = Convert.ToInt32(Unit1TypeCode),
                        TableName = LogTable.UnitType1.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        #endregion

        #region Method Add by Ujjaval For ErrorCategory Table Operation [ fetch/add/edit/delete ]

        public static bool CreateErrorCategory(string Description, string IsActive)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_Error_Category";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Description", Description, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));
            //@IsActive

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {

                    var insertedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_Error_Category",
                        ModuleID = Convert.ToInt32(insertedID),
                        TableName = LogTable.DimRegion.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static bool UpdateErrorCategory(string ID, string Description, string IsActive)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_UPDATE_ErrorCategory";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Description", Description, false));
            objList.Add(new DSSQLParam("ID", ID, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_UPDATE_ErrorCategory",
                        ModuleID = Convert.ToInt32(ID),
                        TableName = LogTable.dimErrorCat.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static List<ErrorCategory> GetAll_Error_Category()
        {
            ErrorCategory OBJ_DimRegion = new ErrorCategory();
            List<ErrorCategory> LIST_DimRegion = new List<ErrorCategory>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GetAll_Error_Category";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            using (DbDataReader dr =
                objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
            {
                while (dr.Read())
                {
                    OBJ_DimRegion = new ErrorCategory();
                    OBJ_DimRegion.ID = Convert.ToString(dr["ID"].ToString());
                    OBJ_DimRegion.Description = dr["Description"].ToString();
                  
                    if (dr["IsActive"].ToString() == "True")
                        OBJ_DimRegion.IsActive = "Yes";
                    else
                        OBJ_DimRegion.IsActive = "No";
                  
                    LIST_DimRegion.Add(OBJ_DimRegion);
                }

                dr.Close();
            }

            objCDataAccess.Dispose(objDbCommand);
            return LIST_DimRegion;
        }

        public static bool DeleteErrorCategory(string ID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_DELETE_ErrorCategory";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ID", ID, false));
            int recAdded = 0;
            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "USP_DELETE_ErrorCategory",
                        ModuleID = Convert.ToInt32(ID),
                        TableName = LogTable.dimErrorCat.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }


        #endregion

        #region Method Add by Ujjaval For DimRegion Table Operation [ fetch/add/edit/delete ]

        public static bool CreateDimRegion(string Description, string IsActive, string DescriptionShort)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_DIMREGION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Description", Description, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("DescriptionShort", DescriptionShort, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));
            //@IsActive

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {

                    var insertedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_DIMREGION",
                        ModuleID = Convert.ToInt32(insertedID),
                        TableName = LogTable.DimRegion.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static bool UpdateDimRegion(string RegionCode, string Description, string IsActive,
            string DescriptionShort)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_UPDATE_DIMREGION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Description", Description, false));
            objList.Add(new DSSQLParam("RegionCode", RegionCode, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("DescriptionShort", DescriptionShort, false));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_UPDATE_DIMREGION",
                        ModuleID = Convert.ToInt32(RegionCode),
                        TableName = LogTable.DimRegion.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static List<Region> GetAllDimRegionList()
        {
            Region OBJ_DimRegion = new Region();
            List<Region> LIST_DimRegion = new List<Region>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GETALL_DIMREGION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            using (DbDataReader dr =
                objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
            {
                while (dr.Read())
                {
                    OBJ_DimRegion = new Region();
                    OBJ_DimRegion.RegionCode = Convert.ToString(dr["RegionCode"].ToString());
                    OBJ_DimRegion.Description = dr["Description"].ToString();
                    OBJ_DimRegion.LastChangedDate = dr["LastChangedDate"].ToString();
                    if (dr["IsActive"].ToString() == "True")
                        OBJ_DimRegion.IsActive = "Yes";
                    else
                        OBJ_DimRegion.IsActive = "No";
                    OBJ_DimRegion.DescriptionShort = dr["DescriptionShort"].ToString();
                    LIST_DimRegion.Add(OBJ_DimRegion);
                }

                dr.Close();
            }

            objCDataAccess.Dispose(objDbCommand);
            return LIST_DimRegion;
        }

        public static bool DeleteRegion(string RegionCode)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_DELETE_DIMREGION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ID", RegionCode, false));
            int recAdded = 0;
            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "USP_DELETE_DIMREGION",
                        ModuleID = Convert.ToInt32(RegionCode),
                        TableName = LogTable.DimRegion.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }


        #endregion

        #region Method Add by Ujjaval For DimUnit Table Operation [ fetch/add/edit/delete ]

        public static bool CreateDimUnit(string OrderID, string FacilityCode, string Description, 
            string DescriptionShort, string DescriptionLong, string UnitType1Code, 
            string IsActive)
        {


            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_DIMUNIT";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("OrderID", OrderID, false));
            objList.Add(new DSSQLParam("FacilityCode", FacilityCode, false));
            objList.Add(new DSSQLParam("Description", Description, false));
            objList.Add(new DSSQLParam("DescriptionShort", DescriptionShort, false));
            objList.Add(new DSSQLParam("DescriptionLong", DescriptionLong, false));
            objList.Add(new DSSQLParam("UnitType1Code", UnitType1Code, false));
           // objList.Add(new DSSQLParam("UnitType2Code", UnitType2Code, false));
           // objList.Add(new DSSQLParam("ProgramCode", ProgramCode, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));
           // objList.Add(new DSSQLParam("Beds", Beds, false));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {

                    var inseredID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_DIMUNIT",
                        ModuleID = Convert.ToInt32(inseredID),
                        TableName = LogTable.DimUnit.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return success;
        }

        public static bool UpdateDimUnit(string UnitCode, string OrderID, string FacilityCode,
            string Description, string DescriptionShort, string DescriptionLong, string UnitType1Code,string IsActive)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_UPDATE_DIMUNIT";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("UnitCode", UnitCode, false));
            objList.Add(new DSSQLParam("OrderID", OrderID, false));
            //objList.Add(new DSSQLParam("ProgramCode", ProgramCode, false));
            objList.Add(new DSSQLParam("FacilityCode", FacilityCode, false));
            objList.Add(new DSSQLParam("Description", Description, false));
            objList.Add(new DSSQLParam("DescriptionLong", DescriptionLong, false));
            objList.Add(new DSSQLParam("DescriptionShort", DescriptionShort, false));
            objList.Add(new DSSQLParam("UnitType1Code", UnitType1Code, false));
            //objList.Add(new DSSQLParam("UnitType2Code", UnitType2Code, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
           //objList.Add(new DSSQLParam("Beds", Beds, false));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_UPDATE_DIMUNIT",
                        ModuleID = Convert.ToInt32(UnitCode),
                        TableName = LogTable.DimUnit.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return success;
        }

        public static List<DimUnit> GetAllDimUnitList()
        {
            DimUnit OBJ_DimUnit = new DimUnit();
            List<DimUnit> LIST_DimUnit = new List<DimUnit>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GETALL_DIMUNIT";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            using (DbDataReader dr =
                objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
            {
                while (dr.Read())
                {
                    OBJ_DimUnit = new DimUnit();
                    OBJ_DimUnit.UnitCode = Convert.ToInt32(dr["UnitCode"].ToString());
                    OBJ_DimUnit.OrderID = Convert.ToInt32(dr["OrderID"].ToString());
                    OBJ_DimUnit.FacilityCode = Convert.ToInt32(dr["FacilityCode"].ToString());
                    OBJ_DimUnit.FacilityCodeDesc = dr["FacilityDesc"].ToString();
                    OBJ_DimUnit.UnitType1Code = Convert.ToInt32(dr["UnitType1Code"].ToString());
                    OBJ_DimUnit.UnitType1CodeDesc = dr["UnitType1Desc"].ToString();

                    OBJ_DimUnit.ProgramCode = Convert.ToInt32(dr["ProgramCode"].ToString());
                    OBJ_DimUnit.ProgramCodeDesc = dr["ProgramDesc"].ToString();
                    OBJ_DimUnit.UnitType2Code = Convert.ToInt32(dr["UnitType2Code"].ToString());
                    OBJ_DimUnit.UnitType2CodeDesc = dr["UnitType2Desc"].ToString();

                    OBJ_DimUnit.Description = dr["DimUnitDesc"].ToString();
                    OBJ_DimUnit.DescriptionLong = dr["DimUnitDescLong"].ToString();
                    OBJ_DimUnit.DescriptionShort = dr["DimUnitDescShort"].ToString();
                    OBJ_DimUnit.LastChangedDate = dr["LastChangedDate"].ToString();
                    OBJ_DimUnit.Dim_Unit_Desc_long = dr["Dim_Unit_Desc_long"].ToString();
                    OBJ_DimUnit.Beds = dr["NoOfBeds"].ToString();

                    if (dr["IsActive"].ToString() == "True")
                        OBJ_DimUnit.IsActive = "Yes";
                    else
                        OBJ_DimUnit.IsActive = "No";


                    LIST_DimUnit.Add(OBJ_DimUnit);
                }

                dr.Close();
            }

            objCDataAccess.Dispose(objDbCommand);
            return LIST_DimUnit;
        }

        public static bool DeleteDimUnit(string UnitCode)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_DELETE_DIMUNIT";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("UnitCode", UnitCode, false));
            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "USP_DELETE_DIMUNIT",
                        ModuleID = Convert.ToInt32(UnitCode),
                        TableName = LogTable.DimUnit.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return success;
        }



        #endregion

        #region Method Add by Ujjaval For DimHCW Table Operation [ fetch/add/edit/delete ]

        public static bool CreateDimHCW(string OrderID, string HCWCategoryCode, string Description, string IsActive)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_DIMHCW";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("OrderID", OrderID, false));
            objList.Add(new DSSQLParam("HCWCategoryCode", HCWCategoryCode, false));
            objList.Add(new DSSQLParam("Description", Description, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));


            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var inseredID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_DIMHCW",
                        ModuleID = Convert.ToInt32(inseredID),
                        TableName = LogTable.DimHCW.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static bool UpdateDimHCW(string HCWCode, string OrderID, string HCWCategoryCode, string Description,
            string IsActive)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_UPDATE_DIMHCW";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("HCWCode", HCWCode, false));
            objList.Add(new DSSQLParam("OrderID", OrderID, false));
            objList.Add(new DSSQLParam("HCWCategoryCode", HCWCategoryCode, false));
            objList.Add(new DSSQLParam("Description", Description, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_UPDATE_DIMHCW",
                        ModuleID = Convert.ToInt32(HCWCode),
                        TableName = LogTable.DimHCW.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        #endregion

        #region Method Add by Ujjaval For DimHCWCategory Table Operation [ fetch/add/edit/delete ]

        public static bool CreateDimHCWGroup(string Description, string IsActive)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_DIMHCWCategory";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Description", Description, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));


            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var inseredID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_DIMHCWCategory",
                        ModuleID = Convert.ToInt32(inseredID),
                        TableName = LogTable.HCWCategory.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static bool UpdateDimHCWGroup(string HCWCategoryCode, string Description, string IsActive)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_UPDATE_DIMHCWCategory";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("HCWCategoryCode", HCWCategoryCode, false));
            objList.Add(new DSSQLParam("Description", Description, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_UPDATE_DIMHCWCategory",
                        ModuleID = Convert.ToInt32(HCWCategoryCode),
                        TableName = LogTable.HCWCategory.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }
           
        #endregion

        #region Method Add By Ujjaval for Fill Dropdown

        public static DataSet GetAllHCWCategory()
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_ALL_HCWCATEGORY_FOR_COMBO";
            List<DSSQLParam> objList = new List<DSSQLParam>();
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

        public static DataSet GetAllDimFacility()
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_ALL_DIMFACILITY_FOR_COMBO";
            List<DSSQLParam> objList = new List<DSSQLParam>();
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

        public static DataSet GetAllProgram()
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_ALL_PROGRAM_FOR_COMBO";
            List<DSSQLParam> objList = new List<DSSQLParam>();
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

        public static DataSet GetAllUnitType1()
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_ALL_UNIT_TYPE1_FOR_COMBO";
            List<DSSQLParam> objList = new List<DSSQLParam>();
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

        public static DataSet GetAllUnitType2()
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_ALL_UNIT_TYPE2_FOR_COMBO";
            List<DSSQLParam> objList = new List<DSSQLParam>();
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

        public static string GetAuditTypeByAuditID(string AuditID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "sp_GetAuditTypeByAuditID";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("AuditID", AuditID, false));
            try
            {
                ds = objCDataAccess.ExecuteDataSet(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                objDbCommand.Transaction.Commit();


                string SelectedID = string.Empty;

                if (ds.Tables[0].Rows.Count >= 1)
                {
                    if (ds.Tables[0].Rows[0]["AuditType"] != DBNull.Value)
                    {
                        SelectedID = ds.Tables[0].Rows[0]["AuditType"].ToString();
                    }
                }

                return SelectedID;


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

        public static DataSet GetAllActiveAuditTypes()
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "sp_GetAuditTypes";
            List<DSSQLParam> objList = new List<DSSQLParam>();
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

        public static DataSet GetAllDimRegion()
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_ALL_DIMREGION_FOR_COMBO";
            List<DSSQLParam> objList = new List<DSSQLParam>();
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

        public static DataSet GetAllFacilityTypeCombo()
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_ALL_DIMFACILITYT_TYPE_FOR_COMBO";
            List<DSSQLParam> objList = new List<DSSQLParam>();
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

        public static DataSet GetAllUsers()
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_ALL_USERS";
            List<DSSQLParam> objList = new List<DSSQLParam>();
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


        #endregion

        #region Method Add By Ujjaval For SystemConfiguration [ fetch / add / edit / delete ]

        public static bool UpdateSystemConfiguration(Int32 SystemConfigurationID, int AuditDuration,
            string AdditionalTime,
            Int32 MinHCWObservation, Int32 ObservationPerHCW, Int32 MaxObservationPerHCW, bool EnableResultTimer,
            string ResultTimerDuration,
            Int32 MinObservation, bool EnablePPE, bool EnablePrecautions)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_UPDATE_SYSTEMCONFIGURATION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("SystemConfigurationID", SystemConfigurationID, false));
            objList.Add(new DSSQLParam("AuditDuration", AuditDuration, false));
            if (AdditionalTime.Trim().Length > 0)
            {
                objList.Add(new DSSQLParam("AdditionalTime", AdditionalTime, false));
            }

            if (ResultTimerDuration.Trim().Length > 0)
            {
                objList.Add(new DSSQLParam("ResultTimerDuration", ResultTimerDuration, false));
            }

            objList.Add(new DSSQLParam("MinHCWObservation", MinHCWObservation, false));
            objList.Add(new DSSQLParam("MinObservationPerHCW", ObservationPerHCW, false));
            objList.Add(new DSSQLParam("MaxObservationPerHCW", MaxObservationPerHCW, false));
            objList.Add(new DSSQLParam("EnableResultTimer", EnableResultTimer, false));
            objList.Add(new DSSQLParam("MinObservation", MinObservation, false));
            objList.Add(new DSSQLParam("EnablePPE", EnablePPE, false));
            objList.Add(new DSSQLParam("EnablePrecautions", EnablePrecautions, false));
            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_UPDATE_SYSTEMCONFIGURATION",
                        ModuleID = Convert.ToInt32(SystemConfigurationID),
                        TableName = LogTable.SystemConfiguration.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        //Author: Grishma
        public static SystemConfiguration GetSystemConfiguration()
        {
            SystemConfiguration OBJ_SystemConfiguration = new SystemConfiguration();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GET_SystemConfiguration";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            using (DbDataReader dr =
                objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
            {
                while (dr.Read())
                {
                    OBJ_SystemConfiguration = new SystemConfiguration();
                    OBJ_SystemConfiguration.ConfigurationID = Convert.ToInt32(dr["SystemConfigurationID"].ToString());
                    OBJ_SystemConfiguration.AuditDuration = Convert.ToInt32(dr["AuditDuration"].ToString());
                    OBJ_SystemConfiguration.AdditionalTime = dr["AdditionalTime"].ToString();
                    OBJ_SystemConfiguration.MinHCWObservation = Convert.ToInt32(dr["MinHCWObservation"].ToString());
                    OBJ_SystemConfiguration.MinObservationPerHCW =
                        Convert.ToInt32(dr["MinObservationPerHCW"].ToString());
                    if (dr["MaxObservationPerHCW"] != DBNull.Value)
                    {
                        OBJ_SystemConfiguration.MaxObservationPerHCW =
                            Convert.ToInt32(dr["MaxObservationPerHCW"].ToString());
                    }
                    else
                    {
                        OBJ_SystemConfiguration.MaxObservationPerHCW = 0;
                    }

                    OBJ_SystemConfiguration.EnableResultTimer = Convert.ToBoolean(dr["EnableResultTimer"].ToString());
                    OBJ_SystemConfiguration.LastChangedDate = dr["LastChangedDate"].ToString();
                    OBJ_SystemConfiguration.ResultTimerDuration = dr["ResultTimerDuration"].ToString();
                    OBJ_SystemConfiguration.MinObservation = Convert.ToInt32(dr["MinObservation"].ToString());

                    if (dr["IsPPE"] != DBNull.Value)
                    {
                        OBJ_SystemConfiguration.EnablePPE = Convert.ToBoolean(dr["IsPPE"].ToString());
                    }
                    else
                    {
                        OBJ_SystemConfiguration.EnablePPE = false;
                    }

                    if (dr["IsPrecautions"] != DBNull.Value)
                    {
                        OBJ_SystemConfiguration.EnablePrecautions = Convert.ToBoolean(dr["IsPrecautions"].ToString());
                    }
                    else
                    {
                        OBJ_SystemConfiguration.EnablePrecautions = false;
                    }


                }

                dr.Close();
            }

            objCDataAccess.Dispose(objDbCommand);
            return OBJ_SystemConfiguration;
        }

        #endregion

        #region Method Add By Ujjaval For DimResult [ fetch / add / edit / delete ]

        public static bool UpdateDimResult(Int32 ResultCode, string Result1, string Result2, string Result3,
            string Result4)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_UPDATE_DIMRESULT";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ResultCode", ResultCode, false));
            objList.Add(new DSSQLParam("Result1", Result1, false));
            objList.Add(new DSSQLParam("Result2", Result2, false));
            objList.Add(new DSSQLParam("Result3", Result3, false));
            objList.Add(new DSSQLParam("Result4", Result4, false));



            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_UPDATE_DIMRESULT",
                        ModuleID = Convert.ToInt32(ResultCode),
                        TableName = LogTable.DimResult.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        


        #endregion

       
        #region Method Add by Ujjaval For KeyDates Table Operation [ fetch/add/edit/delete ]

        public static bool CreateKeyDates(string KeyDateStart, string KeyDateEnd, string DescriptionShort,
            string Description, string Reference, string IsActive)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_KeyDates";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("KeyDateStart", KeyDateStart, false));
            objList.Add(new DSSQLParam("KeyDateEnd", KeyDateEnd, false));
            objList.Add(new DSSQLParam("DescriptionShort", DescriptionShort, false));
            objList.Add(new DSSQLParam("Description", Description, false));
            objList.Add(new DSSQLParam("Reference", Reference, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var inseredID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_KeyDates",
                        ModuleID = Convert.ToInt32(inseredID),
                        TableName = LogTable.KeyDates.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static bool UpdateKeyDates(string KeyDateID, string KeyDateStart, string KeyDateEnd,
            string DescriptionShort, string Description, string Reference, string IsActive)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_UPDATE_KeyDates";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("KeyDateID", KeyDateID, false));
            objList.Add(new DSSQLParam("KeyDateStart", KeyDateStart, false));
            objList.Add(new DSSQLParam("KeyDateEnd", KeyDateEnd, false));
            objList.Add(new DSSQLParam("DescriptionShort", DescriptionShort, false));
            objList.Add(new DSSQLParam("Description", Description, false));
            objList.Add(new DSSQLParam("Reference", Reference, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_UPDATE_KeyDates",
                        ModuleID = Convert.ToInt32(KeyDateID),
                        TableName = LogTable.KeyDates.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static List<KeyDates> GetAllKeyDatesList()
        {
            KeyDates OBJ_KeyDate = new KeyDates();
            List<KeyDates> LIST_KeyDate = new List<KeyDates>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GETALL_KeyDates";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            using (DbDataReader dr =
                objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
            {
                while (dr.Read())
                {
                    OBJ_KeyDate = new KeyDates();
                    OBJ_KeyDate.KeyDateID = Convert.ToString(dr["KeyDateID"]);
                    if (dr["KeyDateStart"] != DBNull.Value)
                    {
                        OBJ_KeyDate.KeyDateStart = Convert.ToDateTime(dr["KeyDateStart"]).ToString("MM/dd/yyyy");
                    }

                    if (dr["KeyDateEnd"] != DBNull.Value)
                    {
                        OBJ_KeyDate.KeyDateEnd = Convert.ToDateTime(dr["KeyDateEnd"]).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        OBJ_KeyDate.KeyDateEnd = string.Empty;
                    }

                    OBJ_KeyDate.Description = dr["Description"].ToString();
                    OBJ_KeyDate.DescriptionShort = dr["DescriptionShort"].ToString();
                    OBJ_KeyDate.Reference = dr["Reference"].ToString();
                    //OBJ_KeyDate.IsActive = dr["IsActive"].ToString();

                    if (dr["IsActive"].ToString() == "True")
                        OBJ_KeyDate.IsActive = "Yes";
                    else
                        OBJ_KeyDate.IsActive = "No";

                    LIST_KeyDate.Add(OBJ_KeyDate);
                }

                dr.Close();
            }

            objCDataAccess.Dispose(objDbCommand);
            return LIST_KeyDate;
        }

        public static bool DeleteKeyDates(string KeyDateID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_DELETE_KeyDates";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("KeyDateID", @KeyDateID, false));
            int recAdded = 0;
            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "USP_DELETE_KeyDates",
                        ModuleID = Convert.ToInt32(KeyDateID),
                        TableName = LogTable.KeyDates.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }


        #endregion

        #region Method Change By Ujjaval For Program Table

        public static bool CreateProgram(string Program_desc, string ProgramTypeCode, string IsActive,
            string DescriptionShort)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_PROGRAM";
            List<DSSQLParam> objList = new List<DSSQLParam>();


            int recAdded = 0;
            objList.Add(new DSSQLParam("Description", Program_desc, false));
            objList.Add(new DSSQLParam("ProgramTypeCode", ProgramTypeCode, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("DescriptionShort", DescriptionShort, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));


            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var inseredID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_PROGRAM",
                        ModuleID = Convert.ToInt32(inseredID),
                        TableName = LogTable.Program.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static bool UpdateProgram(string Program_code, string description, string ProgramTypeCode,
            string IsActive, string DescriptionShort)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;

            string SQL = "USP_UPDATE_PROGRAM";
            List<DSSQLParam> objList = new List<DSSQLParam>();

            objList.Add(new DSSQLParam("Description", description, false));
            objList.Add(new DSSQLParam("ProgramTypeCode", ProgramTypeCode, false));
            objList.Add(new DSSQLParam("ProgramCode", Program_code, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("DescriptionShort", DescriptionShort, false));

            int recAdded = 0;
            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_UPDATE_PROGRAM",
                        ModuleID = Convert.ToInt32(Program_code),
                        TableName = LogTable.Program.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static bool DeleteProgram(string Program_code)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_DELETE_PROGRAM";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ProgramCode", Program_code, false));
            int recAdded = 0;
            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "USP_DELETE_PROGRAM",
                        ModuleID = Convert.ToInt32(Program_code),
                        TableName = LogTable.Program.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static List<Program> GetAllProgramList()
        {
            Program OBJ_ProgramCode = new Program();
            List<Program> LIST_Program = new List<Program>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GETALL_PROGRAM";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            using (DbDataReader dr =
                objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
            {
                while (dr.Read())
                {
                    OBJ_ProgramCode = new Program();

                    OBJ_ProgramCode.Program_Code = dr["ProgramCode"].ToString();
                    OBJ_ProgramCode.Program_Desc = dr["Description"].ToString();
                    OBJ_ProgramCode.Program_Type_Code = dr["ProgramTypeCode"].ToString();
                    OBJ_ProgramCode.Program_Type_Desc = dr["ProgramTypeDescription"].ToString();
                    OBJ_ProgramCode.DescriptionShort = dr["DescriptionShort"].ToString();
                    // OBJ_ProgramCode.IsActive = dr["IsActive"].ToString();

                    if (dr["IsActive"].ToString() == "True")
                        OBJ_ProgramCode.IsActive = "Yes";
                    else
                        OBJ_ProgramCode.IsActive = "No";

                    LIST_Program.Add(OBJ_ProgramCode);
                }

                dr.Close();
            }

            objCDataAccess.Dispose(objDbCommand);
            return LIST_Program;
        }

        #endregion

    
        #region Method Add By Ujjaval for Fill Dropdown

        public static DataSet GetAllAuditors()
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_All_Auditors";
            List<DSSQLParam> objList = new List<DSSQLParam>();
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

        public static DataSet GetAllDropDown(string Param1, string Param2)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_All_DropDown_Data";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Param1", Param1, false));

            if (!string.IsNullOrEmpty(Param2))
                objList.Add(new DSSQLParam("Param2", Param2, false));

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

        public static DataSet GetAllUnitDropDown(string Param1, string FacilityCode, string ProgramCode,
            string UnitType1Code, string UnitType2Code)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_All_DropDown_Data";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Param1", Param1, false));

            if (!string.IsNullOrEmpty(FacilityCode))
                objList.Add(new DSSQLParam("Param2", FacilityCode.Remove(FacilityCode.LastIndexOf(",")), false));

            if (!string.IsNullOrEmpty(ProgramCode))
                objList.Add(new DSSQLParam("Param3", ProgramCode.Remove(ProgramCode.LastIndexOf(",")), false));

            if (!string.IsNullOrEmpty(UnitType1Code))
                objList.Add(new DSSQLParam("Param4", UnitType1Code.Remove(UnitType1Code.LastIndexOf(",")), false));

            if (!string.IsNullOrEmpty(UnitType2Code))
                objList.Add(new DSSQLParam("Param5", UnitType2Code.Remove(UnitType2Code.LastIndexOf(",")), false));

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

        public static DataSet GetAllFacilityDropDown(string Param1, string FacilityTypeCode, string ZoneCode)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_All_DropDown_Data";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Param1", Param1, false));

            if (!string.IsNullOrEmpty(FacilityTypeCode))
                objList.Add(new DSSQLParam("Param2", FacilityTypeCode.Remove(FacilityTypeCode.LastIndexOf(",")),
                    false));

            if (!string.IsNullOrEmpty(ZoneCode))
                objList.Add(new DSSQLParam("Param3", ZoneCode.Remove(ZoneCode.LastIndexOf(",")), false));

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

        #endregion

      
        //charuka


        public static bool CreateTempHCP(string Auditid, string HCP, int UserId, string Hcpcode)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_Temphcp";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Auditid", Auditid, false));
            objList.Add(new DSSQLParam("HCP", HCP, false));
            objList.Add(new DSSQLParam("Userid", UserId, false));
            objList.Add(new DSSQLParam("Hcpcode", Hcpcode, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));


            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var inseredID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_Temphcp",
                        ModuleID = Convert.ToInt32(inseredID),
                        TableName = LogTable.tmp_hcp.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static DataSet GetAllMoment()
        {
            DataSet dsMomentData = new DataSet();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = @"SELECT [Moment1],[Moment2],[Moment3],[Moment4],[Moment5] FROM DimMoment";
            try
            {
                dsMomentData = objCDataAccess.ExecuteDataSet(objDbCommand, SQL);
            }
            catch (Exception ex)
            {
                dsMomentData = null;
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return dsMomentData;
        }

        public static DataSet GetAllGuideline()
        {
            DataSet dsGuidelineData = new DataSet();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = @"SELECT [Guideline1],[Guideline2],[Guideline3],[Guideline4],[Guideline5] FROM DimGuideline";
            try
            {
                dsGuidelineData = objCDataAccess.ExecuteDataSet(objDbCommand, SQL);
            }
            catch (Exception ex)
            {
                dsGuidelineData = null;
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return dsGuidelineData;
        }

        public static DataSet GetAllPPE()
        {
            DataSet dsGuidelineData = new DataSet();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            // string SQL = @"SELECT [PPE1],[PPE2],[PPE3],[PPE4],[PPE5],[Precautions1] ,[Precautions2] ,[Precautions3] ,[Precautions4] ,[EQP1],[EQP2],[EQP3],[EQP4],[EQP5] FROM DimPPE";
            string SQL =
                @"SELECT [PPE1],[PPE2],[PPE3],[PPE4],[PPE5],[Precautions1] ,[Precautions2] ,[Precautions3] ,[Precautions4] FROM DimPPE";
            dsGuidelineData = objCDataAccess.ExecuteDataSet(objDbCommand, SQL);
            objCDataAccess.Dispose(objDbCommand);
            return dsGuidelineData;
        }

        public static DataSet GetAllHcpwithobs(string UserID)
        {
            DataSet dsAuidtData = new DataSet();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL =
                @"select * from tmp_hcp INNER JOIN tmp_obs ON tmp_hcp.id = tmp_obs.hcpid WHERE tmp_hcp.userid =" +
                UserID;
            try
            {
                dsAuidtData = objCDataAccess.ExecuteDataSet(objDbCommand, SQL);
            }
            catch (Exception ex)
            {
                dsAuidtData = null;
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return dsAuidtData;
        }

        public static bool CreateTempOBS(int Obsid, string Moment1, string Moment2, string Moment3, string Moment4,
            string Moment5, string Result1, string Result2, string Result3, string Result4, string Guideline1,
            string Guideline2, string Guideline3, string Guideline4, string Guideline5, string Notecode,
            string Notefreetext, string userid)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_Tempobs";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("hcpid", Obsid, false));
            objList.Add(new DSSQLParam("Moment1", Moment1, false));
            objList.Add(new DSSQLParam("Moment2", Moment2, false));
            objList.Add(new DSSQLParam("Moment3", Moment3, false));
            objList.Add(new DSSQLParam("Moment4", Moment4, false));
            objList.Add(new DSSQLParam("Moment5", Moment5, false));

            objList.Add(new DSSQLParam("Result1", Result1, false));
            objList.Add(new DSSQLParam("Result2", Result2, false));
            objList.Add(new DSSQLParam("Result3", Result3, false));
            objList.Add(new DSSQLParam("Result4", Result4, false));


            objList.Add(new DSSQLParam("Guideline1", Guideline1, false));
            objList.Add(new DSSQLParam("Guideline2", Guideline2, false));
            objList.Add(new DSSQLParam("Guideline3", Guideline3, false));
            objList.Add(new DSSQLParam("Guideline4", Guideline4, false));
            objList.Add(new DSSQLParam("Guideline5", Guideline5, false));

            objList.Add(new DSSQLParam("Notecode", Notecode, false));
            objList.Add(new DSSQLParam("Notefreetext", Notefreetext, false));

            objList.Add(new DSSQLParam("Userid", userid, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var inseredID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_Tempobs",
                        ModuleID = Convert.ToInt32(inseredID),
                        TableName = LogTable.tmp_obs.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;
                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return success;
        }

        public static string GetHCPCount(string Userid)
        {
            string Total = "";
            DataSet dsTempOBSData = new DataSet();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = @"select COUNT(id)as Total from tmp_hcp WHERE Userid=" + Userid;
            try
            {
                dsTempOBSData = objCDataAccess.ExecuteDataSet(objDbCommand, SQL);
                DataSet OBJ_GUIDE = new DataSet();
                OBJ_GUIDE = dsTempOBSData;
                DataTable dt_GUIDE = new DataTable();

                foreach (DataRow dr in OBJ_GUIDE.Tables[0].Rows)
                {
                    if (dr["Total"].ToString() != "" || dr["Total"].ToString() != null)
                    {
                        Total = dr["Total"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Total = null;
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }




            return Total;
        }

        public static string GetOBSCount(string Userid)
        {
            string Total = "";
            DataSet dsTempOBSData = new DataSet();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = @"select COUNT(id)as Total from tmp_obs WHERE Userid=" + Userid;
            try
            {
                dsTempOBSData = objCDataAccess.ExecuteDataSet(objDbCommand, SQL);
                DataSet OBJ_GUIDE = new DataSet();
                OBJ_GUIDE = dsTempOBSData;
                DataTable dt_GUIDE = new DataTable();

                foreach (DataRow dr in OBJ_GUIDE.Tables[0].Rows)
                {
                    if (dr["Total"].ToString() != "" || dr["Total"].ToString() != null)
                    {
                        Total = dr["Total"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Total = null;
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return Total;
        }

        public static DataSet GetAllTempHcp(string Userid)
        {
            DataSet dsTempOBSData = new DataSet();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL =
                @"SELECT id,auditid,userid, (hcp + ' ' + Convert(varchar,hcpno)) as HCP FROM tmp_hcp WHERE Userid=" +
                Userid;
            try
            {
                dsTempOBSData = objCDataAccess.ExecuteDataSet(objDbCommand, SQL);
            }
            catch (Exception ex)
            {
                dsTempOBSData = null;
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return dsTempOBSData;
        }

        public static DataSet GetAllTempOBS(string HCPID)
        {
            DataSet dsTempOBSData = new DataSet();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            //string SQL = @"SELECT * FROM tmp_obs WHERE hcpid=" + HCPID;
            //string SQL = @"select (Moment1 + moment2 + moment3+moment4+moment5) as Moment, (result1 + result2+result3 +result4)as Result,id,hcpid,gloves,guideline1,guideline2,guideline3,guideline4,guideline5,NoteCode from tmp_obs WHERE hcpid=" + HCPID;
            string SQL =
                @"select (Moment1 + moment2 + moment3+moment4+moment5) as Moment, (result1 + (CASE WHEN ISNULL(result1,'') = '' THEN '' ELSE ', 'END) + result2 + (CASE WHEN ISNULL(result2,'') = '' THEN '' ELSE ', 'END) + result3 +  (CASE WHEN ISNULL(result3,'') = '' THEN '' ELSE ', 'END) + result4) as Result,id,hcpid,guideline1,guideline2,guideline3,guideline4,guideline5,NoteCode from tmp_obs WHERE hcpid=" +
                HCPID;
            try
            {
                dsTempOBSData = objCDataAccess.ExecuteDataSet(objDbCommand, SQL);
            }
            catch (Exception ex)
            {
                dsTempOBSData = null;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return dsTempOBSData;
        }



        public static bool CreateAudit(string AuditID, string AuditDate, string AuditTimeStart, string AuditTimeEnd,
            string AuditDuration,
            string UserID, string FacilityCode, string UnitCode, string ObservationID, string HCWCode,
            string Guideline1, string Guideline2, string Guideline3, string Guideline4, string Guideline5,
            string Moment1, string Moment2, string Moment3, string Moment4, string Moment5,
            string ObservationResultNumber,
            string Result1, string ResultTimeStart1, string ResultTimeEnd1, string Result2, string ResultTimeStart2,
            string ResultTimeEnd2,
            string Result3, string Result4, string NoteCode, string NoteFreeText, string HCWInstance, string AuditName,
            string Status, string RegionCode)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_AUDIT_BYFORM";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("AuditID", AuditID, false));
            objList.Add(new DSSQLParam("AuditDate", AuditDate, false));
            objList.Add(new DSSQLParam("AuditTimeStart", AuditTimeStart, false));
            objList.Add(new DSSQLParam("AuditTimeEnd", AuditTimeEnd, false));
            objList.Add(new DSSQLParam("AuditDuration", AuditDuration, false));
            objList.Add(new DSSQLParam("UserID", UserID, false));
            objList.Add(new DSSQLParam("FacilityCode", FacilityCode, false));
            objList.Add(new DSSQLParam("UnitCode", UnitCode, false));
            objList.Add(new DSSQLParam("ObservationID", ObservationID, false));
            objList.Add(new DSSQLParam("HCWCode", HCWCode, false));

            objList.Add(new DSSQLParam("Guideline1", Guideline1, false));
            objList.Add(new DSSQLParam("Guideline2", Guideline2, false));
            objList.Add(new DSSQLParam("Guideline3", Guideline3, false));
            objList.Add(new DSSQLParam("Guideline4", Guideline4, false));
            objList.Add(new DSSQLParam("Guideline5", Guideline5, false));

            objList.Add(new DSSQLParam("Moment1", Moment1, false));
            objList.Add(new DSSQLParam("Moment2", Moment2, false));
            objList.Add(new DSSQLParam("Moment3", Moment3, false));
            objList.Add(new DSSQLParam("Moment4", Moment4, false));
            objList.Add(new DSSQLParam("Moment5", Moment5, false));
            objList.Add(new DSSQLParam("ObservationResultNumber", ObservationResultNumber, false));

            //objList.Add(new DSSQLParam("Gloves", Gloves, false));

            objList.Add(new DSSQLParam("Result1", Result1, false));
            objList.Add(new DSSQLParam("ResultTimeStart1", ResultTimeStart1, false));
            objList.Add(new DSSQLParam("ResultTimeEnd1", ResultTimeEnd1, false));
            objList.Add(new DSSQLParam("Result2", Result2, false));
            objList.Add(new DSSQLParam("ResultTimeStart2", ResultTimeStart2, false));
            objList.Add(new DSSQLParam("ResultTimeEnd2", ResultTimeEnd2, false));
            objList.Add(new DSSQLParam("Result3", Result3, false));
            objList.Add(new DSSQLParam("Result4", Result4, false));

            objList.Add(new DSSQLParam("NoteCode", NoteCode, false));
            objList.Add(new DSSQLParam("NoteFreeText", NoteFreeText, false));
            objList.Add(new DSSQLParam("HCWInstance", HCWInstance, false));
            objList.Add(new DSSQLParam("AuditName", AuditName, false));
            objList.Add(new DSSQLParam("Status", Status, false));
            objList.Add(new DSSQLParam("RegionCode", RegionCode, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));
            // @AuditID varchar (50),


            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var insertedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),
                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_AUDIT_BYFORM",
                        ModuleID = Convert.ToInt32(insertedID),
                        TableName = LogTable.Audit.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }


        #region Methods added by Grishma

        public static DataSet GetAllRoles()
        {
            try
            {
                //Returns only active Roles
                CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
                DbCommand objDbCommand =
                    objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
                DataSet ds;
                string SQL = "USP_GET_ALL_Roles";
                List<DSSQLParam> objList = new List<DSSQLParam>();
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
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<ModuleDetails> GetAllModules()
        {
            try
            {
                ModuleDetails objModuleDetails = new ModuleDetails();
                List<ModuleDetails> lstModuleDetails = new List<ModuleDetails>();
                CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
                List<DSSQLParam> objParams = new List<DSSQLParam>();
                DbCommand objDbCommand =
                    objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
                string SQL = "USP_GET_ALL_Modules";

                using (DbDataReader dr =
                    objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objParams))
                {
                    while (dr.Read())
                    {
                        objModuleDetails = new ModuleDetails();
                        objModuleDetails.ModuleId = Convert.ToInt32(dr["ModuleId"]);
                        objModuleDetails.Name = Convert.ToString(dr["Name"]);
                        objModuleDetails.AccessDescription = Convert.ToString(dr["AccessDescription"]);
                        objModuleDetails.ModuleKey = Convert.ToString(dr["ModuleKey"]);
                        objModuleDetails.ParentModuleId = Convert.ToInt32(dr["ParentModuleId"]);
                        lstModuleDetails.Add(objModuleDetails);
                    }

                    dr.Close();
                }

                objCDataAccess.Dispose(objDbCommand);
                return lstModuleDetails;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }

        }

        public static List<Permission> GetAllPermissionsRoleWise(int roleCode)
        {
            try
            {
                Permission objPermission = new Permission();
                List<Permission> lstPermission = new List<Permission>();
                CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
                List<DSSQLParam> objParams = new List<DSSQLParam>();
                objParams.Add(new DSSQLParam("RoleCode", roleCode, false));
                DbCommand objDbCommand =
                    objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
                string SQL = "USP_GET_ALL_Permissions_RoleWise";

                using (DbDataReader dr =
                    objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objParams))
                {
                    while (dr.Read())
                    {
                        objPermission = new Permission();
                        objPermission.PermissionId = Convert.ToInt32(dr["PermissionId"]);
                        objPermission.FKModuleId = Convert.ToInt32(dr["FKModuleId"]);
                        objPermission.ModuleKey = Convert.ToString(dr["ModuleKey"]);
                        objPermission.FKRoleCode = Convert.ToInt32(dr["FKRoleCode"]);
                        lstPermission.Add(objPermission);
                    }

                    dr.Close();
                }

                objCDataAccess.Dispose(objDbCommand);
                return lstPermission;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }

        }

        public static bool AssignPermission(int FKRoleCode, string PermissionXML)
        {
            DataSet dsCheckListGroup = new DataSet();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            List<DSSQLParam> objParams = new List<DSSQLParam>();
            objParams.Add(new DSSQLParam("FKRoleCode", FKRoleCode, false));
            objParams.Add(new DSSQLParam("PermissionXML", PermissionXML, false));
            objParams.Add(new DSSQLParam("result", 0, ParameterDirection.Output));
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_ASSIGN_Permission";
            dsCheckListGroup = objCDataAccess.ExecuteDataSet(objDbCommand, SQL, CommandType.StoredProcedure, objParams);
            bool result = Convert.ToBoolean(objDbCommand.Parameters["@result"].Value);
            objCDataAccess.Dispose(objDbCommand);

            // Audit Trail
            SerilogAuditTrail.LogInfo(new AuditTrailDataModel
            {
                Description = JsonConvert.SerializeObject(objParams.Select(s => new LogType
                {
                    Key = s.sParamName,
                    Value = s.objParamValue.ToString()
                })),

                Action = LogAction.Assign.Value,
                Module = "USP_ASSIGN_Permission",
                ModuleID = Convert.ToInt32(FKRoleCode),
                TableName = LogTable.PermissionDetails.Value,

                UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                UserName =
                    $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                Email = BUSessionUtility.BUSessionContainer.Email,
                UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
            });

            return result;
        }

        public static bool UnassignPermission(int FKRoleCode, string PermissionXML)
        {
            DataSet dsCheckListGroup = new DataSet();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            List<DSSQLParam> objParams = new List<DSSQLParam>();
            objParams.Add(new DSSQLParam("FKRoleCode", FKRoleCode, false));
            objParams.Add(new DSSQLParam("PermissionXML", PermissionXML, false));
            objParams.Add(new DSSQLParam("result", 0, ParameterDirection.Output));
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_UNASSIGN_Permission";
            dsCheckListGroup = objCDataAccess.ExecuteDataSet(objDbCommand, SQL, CommandType.StoredProcedure, objParams);
            bool result = Convert.ToBoolean(objDbCommand.Parameters["@result"].Value);
            objCDataAccess.Dispose(objDbCommand);

            // Audit Trail
            SerilogAuditTrail.LogInfo(new AuditTrailDataModel
            {
                Description = JsonConvert.SerializeObject(objParams.Select(s => new LogType
                {
                    Key = s.sParamName,
                    Value = s.objParamValue.ToString()
                })),

                Action = LogAction.UnAssign.Value,
                Module = "USP_UNASSIGN_Permission",
                ModuleID = Convert.ToInt32(FKRoleCode),
                TableName = LogTable.PermissionDetails.Value,

                UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                UserName =
                    $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                Email = BUSessionUtility.BUSessionContainer.Email,
                UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
            });

            return result;
        }

        public static List<ModulePermission> GetMenuPermission(int roleCode)
        {
            try
            {
                ModulePermission objModulePermission = new ModulePermission();
                List<ModulePermission> lstModulePermission = new List<ModulePermission>();
                CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
                List<DSSQLParam> objParams = new List<DSSQLParam>();
                objParams.Add(new DSSQLParam("RoleCode", roleCode, false));
                DbCommand objDbCommand =
                    objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
                string SQL = "USP_GET_MenuPermission";

                using (DbDataReader dr =
                    objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objParams))
                {
                    while (dr.Read())
                    {
                        objModulePermission = new ModulePermission();
                        objModulePermission.ModuleId = Convert.ToInt32(dr["ModuleId"]);
                        objModulePermission.Name = Convert.ToString(dr["Name"]);
                        objModulePermission.ModuleKey = Convert.ToString(dr["ModuleKey"]);
                        objModulePermission.PermissionId = Convert.ToInt32(dr["PermissionId"]);
                        objModulePermission.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        lstModulePermission.Add(objModulePermission);
                    }

                    dr.Close();
                }

                objCDataAccess.Dispose(objDbCommand);
                return lstModulePermission;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }

        }

        public static List<ModulePermission> GetPermission(int roleCode, string moduleKey)
        {
            List<ModulePermission> lstModulePermission = new List<ModulePermission>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            try
            {
                if (roleCode == 0)
                {
                    HttpContext.Current.Response.Redirect("~/Login.aspx");
                }

                ModulePermission objModulePermission = new ModulePermission();
                List<DSSQLParam> objParams = new List<DSSQLParam>();
                objParams.Add(new DSSQLParam("RoleCode", roleCode, false));
                objParams.Add(new DSSQLParam("ModuleKey", moduleKey, false));

                string SQL = "USP_GET_Permission";

                using (DbDataReader dr =
                    objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objParams))
                {
                    while (dr.Read())
                    {
                        objModulePermission = new ModulePermission();
                        objModulePermission.ModuleId = Convert.ToInt32(dr["ModuleId"]);
                        objModulePermission.Name = Convert.ToString(dr["Name"]);
                        objModulePermission.ModuleKey = Convert.ToString(dr["ModuleKey"]);
                        objModulePermission.PermissionId = Convert.ToInt32(dr["PermissionId"]);
                        objModulePermission.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        lstModulePermission.Add(objModulePermission);
                    }

                    dr.Close();
                }
            }
            catch (System.Threading.ThreadAbortException ex)
            {

            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return lstModulePermission;
        }

        public static bool GetPermissionForPage(int roleCode, string moduleKey)
        {
            bool IsActive = false;
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            try
            {
                if (roleCode == 0)
                {
                    HttpContext.Current.Response.Redirect("~/Login.aspx");
                }

                ModulePermission objModulePermission = new ModulePermission();
                List<ModulePermission> lstModulePermission = new List<ModulePermission>();

                List<DSSQLParam> objParams = new List<DSSQLParam>();
                objParams.Add(new DSSQLParam("RoleCode", roleCode, false));
                objParams.Add(new DSSQLParam("ModuleKey", moduleKey, false));

                string SQL = "USP_GET_Permission_For_Page";
                IsActive = Convert.ToBoolean(objCDataAccess.ExecuteScalar(objDbCommand, SQL,
                    CommandType.StoredProcedure, objParams));
                objCDataAccess.Dispose(objDbCommand);

            }
            catch (System.Threading.ThreadAbortException ex)
            {

            }
            catch (Exception ex)
            {
                IsActive = false;
                throw ex;
            }

            return IsActive;
        }


        #region Method Added By Grishma For Role [ fetch / add / edit / delete ]

        public static bool AddUpdateRole(Role objRole, string operation, int templateRoleId)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_UPDATE_Role";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Operation", operation, false));
            objList.Add(new DSSQLParam("RoleCode", objRole.RoleCode, false));
            objList.Add(new DSSQLParam("Description", objRole.Description, false));
            objList.Add(new DSSQLParam("IsActive", objRole.IsActive, false));
            objList.Add(new DSSQLParam("TemplateRole", templateRoleId, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {

                    success = true;
                    objDbCommand.Transaction.Commit();

                    if (operation == "Add")
                    {

                        // Audit Trail
                        var inseredID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);
                        SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                        {
                            Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                            {
                                Key = s.sParamName,
                                Value = s.objParamValue.ToString()
                            })),

                            Action = LogAction.Add.Value,
                            Module = "USP_ADD_UPDATE_Role",
                            ModuleID = Convert.ToInt32(inseredID),
                            TableName = LogTable.PermissionDetails.Value,

                            UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                            UserName =
                                $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                            Email = BUSessionUtility.BUSessionContainer.Email,
                            UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                            UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                        });
                    }
                    else
                    {
                        // Audit Trail
                        SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                        {
                            Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                            {
                                Key = s.sParamName,
                                Value = s.objParamValue.ToString()
                            })),

                            Action = LogAction.Edit.Value,
                            Module = "USP_ADD_UPDATE_Role",
                            ModuleID = Convert.ToInt32(objRole.RoleCode),
                            TableName = LogTable.PermissionDetails.Value,

                            UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                            UserName =
                                $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                            Email = BUSessionUtility.BUSessionContainer.Email,
                            UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                            UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                        });
                    }

                }
                else
                {
                    success = false;
                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return success;
        }

        public static bool DeleteRole(Int32 RoleCode)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_DELETE_Role";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("RoleCode", RoleCode, false));
            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "USP_DELETE_Role",
                        ModuleID = Convert.ToInt32(RoleCode),
                        TableName = LogTable.Role.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static List<Role> GetAllRoleList()
        {
            //Returns all Roles
            Role OBJ_Role = new Role();
            List<Role> LIST_Role = new List<Role>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GETALL_Roles";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            using (DbDataReader dr =
                objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
            {
                while (dr.Read())
                {
                    OBJ_Role = new Role();
                    OBJ_Role.RoleCode = Convert.ToInt32(dr["RoleCode"]);
                    OBJ_Role.Description = Convert.ToString(dr["Description"]);
                    OBJ_Role.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (dr["IsActive"].ToString() == "True")
                        OBJ_Role.Active = "Yes";
                    else
                        OBJ_Role.Active = "No";

                    LIST_Role.Add(OBJ_Role);
                }

                dr.Close();
            }

            objCDataAccess.Dispose(objDbCommand);
            return LIST_Role;
        }

        public static DataSet GetRoleWiseUserList(int RoleCode)
        {
            try
            {

                CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
                DbCommand objDbCommand =
                    objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
                DataSet ds;
                string SQL = "USP_GET_RoleWise_UserList";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("RoleCode", RoleCode, false));
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
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool ReassignUserRole(string userXML, string userID)
        {
            DataSet dsCheckListGroup = new DataSet();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            List<DSSQLParam> objParams = new List<DSSQLParam>();
            objParams.Add(new DSSQLParam("UserXML", userXML, false));
            objParams.Add(new DSSQLParam("result", 0, ParameterDirection.Output));
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_REASSIGN_UserRole";
            dsCheckListGroup = objCDataAccess.ExecuteDataSet(objDbCommand, SQL, CommandType.StoredProcedure, objParams);
            bool result = Convert.ToBoolean(objDbCommand.Parameters["@result"].Value);
            objCDataAccess.Dispose(objDbCommand);

            // Audit Trail
            SerilogAuditTrail.LogInfo(new AuditTrailDataModel
            {
                Description = JsonConvert.SerializeObject(objParams.Select(s => new LogType
                {
                    Key = s.sParamName,
                    Value = s.objParamValue.ToString()
                })),

                Action = LogAction.Edit.Value,
                Module = "USP_REASSIGN_UserRole",
                ModuleID = Convert.ToInt32(userID),
                TableName = LogTable.Users.Value,

                UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                UserName =
                    $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                Email = BUSessionUtility.BUSessionContainer.Email,
                UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
            });

            return result;
        }

        #endregion

        #region Method Added By Grishma For Report [ fetch / add / edit / delete ]

        public static bool AddUpdateReport(Report objReport, string operation)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_UPDATE_Report";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Operation", operation, false));
            objList.Add(new DSSQLParam("ReportID", objReport.ReportID, false));
            objList.Add(new DSSQLParam("ReportCode", objReport.ReportCode, false));
            objList.Add(new DSSQLParam("ReportName", objReport.ReportName, false));
            objList.Add(new DSSQLParam("ReportDescription", objReport.ReportDescription, false));
            objList.Add(new DSSQLParam("ReportServerLocation", objReport.ReportServerLocation, false));
            objList.Add(new DSSQLParam("ReportCategory", objReport.ReportCategory, false));
            objList.Add(new DSSQLParam("ReportSubCategory", objReport.ReportSubCategory, false));
            objList.Add(new DSSQLParam("IsActive", objReport.IsActive, false));
            objList.Add(new DSSQLParam("SortOrder", objReport.SortOrder, false));
            objList.Add(new DSSQLParam("IsSendEnable", objReport.IsSendEnable, false));
            objList.Add(new DSSQLParam("ReportKey", objReport.ReportKey, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));


            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var inseredID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();

                    if (operation == "Add")
                    {
                        // Audit Trail
                        SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                        {
                            Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                            {
                                Key = s.sParamName,
                                Value = s.objParamValue.ToString()
                            })),

                            Action = LogAction.Add.Value,
                            Module = "USP_ADD_UPDATE_Report",
                            ModuleID = Convert.ToInt32(inseredID),
                            TableName = LogTable.ReportsAndRoles.Value,

                            UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                            UserName =
                                $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                            Email = BUSessionUtility.BUSessionContainer.Email,
                            UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                            UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                        });
                    }
                    else
                    {
                        // Audit Trail
                        SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                        {
                            Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                            {
                                Key = s.sParamName,
                                Value = s.objParamValue.ToString()
                            })),

                            Action = LogAction.Edit.Value,
                            Module = "USP_ADD_UPDATE_Report",
                            ModuleID = Convert.ToInt32(objReport.ReportID),
                            TableName = LogTable.ReportsAndRoles.Value,

                            UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                            UserName =
                                $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                            Email = BUSessionUtility.BUSessionContainer.Email,
                            UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                            UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                        });
                    }

                }
                else
                {
                    success = false;
                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return success;
        }

        public static bool DeleteReport(Int32 ReportID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_DELETE_Report";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ReportID", ReportID, false));
            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "USP_DELETE_Report",
                        ModuleID = Convert.ToInt32(ReportID),
                        TableName = LogTable.ReportsAndRoles.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static List<Report> GetAllReportList()
        {
            Report OBJ_Report = new Report();
            List<Report> LIST_Report = new List<Report>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GETALL_Reports";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            using (DbDataReader dr =
                objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
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
                    OBJ_Report.SortOrder = Convert.ToInt32(dr["SortOrder"]);
                    OBJ_Report.IsSendEnable = Convert.ToBoolean(dr["IsSendEnable"]);
                    OBJ_Report.ReportKey = Convert.ToString(dr["ReportKey"]);
                    if (dr["IsActive"].ToString() == "True")
                        OBJ_Report.Active = "Yes";
                    else
                        OBJ_Report.Active = "No";

                    LIST_Report.Add(OBJ_Report);
                }

                dr.Close();
            }

            objCDataAccess.Dispose(objDbCommand);
            return LIST_Report;
        }

        #endregion

          
        #endregion

        #region "NewSystemSetting"

        public static bool SaveSystemSetting(DataTable tbl_settings)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool result = false;
            try
            {
                DSSQLParam param = new DSSQLParam();
                param.sParamName = "tbl_settings";
                param.dbParamSqlDBType = SqlDbType.Structured;
                param.objParamValue = tbl_settings;
                param.paramDirection = ParameterDirection.Input;


                string SQL = "USP_SAVE_SYS_SETTINGS";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(param);

                int cnt = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);

                if (cnt > 0)
                {
                    objDbCommand.Transaction.Commit();
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

        public static DataSet GetEmails()
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds = new DataSet();
            string SQL = "USP_GetSystemSettingEmails";
            List<DSSQLParam> objList = new List<DSSQLParam>();
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

        public static int getPasswordExpiredDate(string UserId)
        {
            int date_to_expire = 0;
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            try
            {

                string SQL = "SP_GetPasswordExpiration";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("UserId ", UserId, false));
                DataTable tbl =
                    objCDataAccess.ExecuteDataTable(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(tbl.Rows[0]["PasswordExpireOn"].ToString()))
                    {
                        date_to_expire = Convert.ToInt32(tbl.Rows[0]["PasswordExpireOn"].ToString());
                    }
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

            return date_to_expire;
        }

        public static DataSet GetSystemSetting()
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds = new DataSet();
            string SQL = "USP_GETSystemSetting";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            // objList.Add(new DSSQLParam("RoleCode", Rolecode, false));
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

        public static DataTable GetCurrentActiveMFA()
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataTable dt = new DataTable();
            try
            {
                string SQL = "USP_GET_CURRENT_ACTIVE_MFA_ROLE_CODES";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                dt = objCDataAccess.ExecuteDataTable(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                objDbCommand.Transaction.Commit();
            }
            catch (Exception ex)
            {
                dt = null;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }

        public static DataTable GetAllUserRolesAsDataSet()
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataTable table;
            try
            {
                string SQL = "USP_GETALL_Roles";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                table = objCDataAccess.ExecuteDataTable(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                objDbCommand.Transaction.Commit();

            }
            catch (Exception ex)
            {
                table = null;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return table;
        }

        public static DataTable GetFacilityByRegion(int Region)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataTable table;
            try
            {
                string SQL = "USP_GetFacility_By_Region";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("RegionCode", Region, false));
                table = objCDataAccess.ExecuteDataTable(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                objDbCommand.Transaction.Commit();

            }
            catch (Exception ex)
            {
                table = null;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return table;
        }





        public static bool UpdateMFA(DataTable tbl_mfa)
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool result = false;
            try
            {
                DSSQLParam param = new DSSQLParam();
                param.sParamName = "tbl_mfa";
                param.dbParamSqlDBType = SqlDbType.Structured;
                param.objParamValue = tbl_mfa;
                param.paramDirection = ParameterDirection.Input;

                string SQL = "USP_UPDATE_MFA";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(param);
                int recAdded = 0;
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    result = true;
                    objDbCommand.Transaction.Commit();
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

        #endregion

        public static bool UpdateTempObsPPE(Int32 Id, string guideline, string guidelineText)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_UPDATE_TempObsPPE";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Id", Id, false));
            objList.Add(new DSSQLParam("PPE", guideline, false));
            objList.Add(new DSSQLParam("PPEText", guidelineText, false));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_UPDATE_TempObsPPE",
                        ModuleID = Id,
                        TableName = LogTable.TempObs.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);
            }

            return success;
        }

   
       
        public static USERS GetUserByEmail(string email)
        {
            var OBJ_USER_INFO = new USERS();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "SP_GET_USER_By_EMAIL";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Email", email, false));

            using (DbDataReader dr =
                objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
            {
                while (dr.Read())
                {
                    OBJ_USER_INFO = new USERS();
                    OBJ_USER_INFO.User_ID = dr["UserID"].ToString();
                    OBJ_USER_INFO.FirstName = dr["FirstName"].ToString();
                    OBJ_USER_INFO.LastName = dr["LastName"].ToString();
                    OBJ_USER_INFO.RoleCode = dr["Role_Code"].ToString();
                    OBJ_USER_INFO.FacilityCode = dr["Facility_Code"].ToString();
                    //OBJ_USER_INFO.RoleDescription = dr["role_desc"].ToString();
                    //OBJ_USER_INFO.FacilityDescription = dr["facility_desc"].ToString();
                    //OBJ_USER_INFO.LastLoginDate = dr["LastLoginDate"].ToString();
                    OBJ_USER_INFO.LastPasswordChangedDate = dr["LastPasswordChangedDate"].ToString();
                    OBJ_USER_INFO.CreationDate = dr["CreationDate"].ToString();
                    OBJ_USER_INFO.UnitCode = dr["UnitCode"].ToString();
                    // OBJ_USER_INFO.Unit = dr["Unit_Name_Description_Long"].ToString();
                    OBJ_USER_INFO.Email = dr["Email"].ToString();
                    OBJ_USER_INFO.Occupation = dr["Occupation"].ToString();

                    if (dr["IsLockedOut"].ToString() == "True")
                        OBJ_USER_INFO.IsLockedOut = "Yes";
                    else
                        OBJ_USER_INFO.IsLockedOut = "No";
                    if (dr["IsAuditor"].ToString() == "True")
                        OBJ_USER_INFO.IsAuditor = "Yes";
                    else
                        OBJ_USER_INFO.IsAuditor = "No";
                    OBJ_USER_INFO.LastChangedDate = dr["LastChangedDate"].ToString();
                    OBJ_USER_INFO.DisplayName = dr["DisplayName"].ToString();
                    if (dr["IsActive"].ToString() == "True")
                        OBJ_USER_INFO.IsActive = "Yes";
                    else
                        OBJ_USER_INFO.IsActive = "No";
                    OBJ_USER_INFO.RegionCode = dr["Region_Code"].ToString();
                    //OBJ_USER_INFO.Region = dr["Region"].ToString();
                }

                dr.Close();
            }

            objCDataAccess.Dispose(objDbCommand);
            return OBJ_USER_INFO;
        }

        #region Activity Follow Temp

        /// <summary>
        /// Delete all the old data of activity follow temp data with sub tables
        /// </summary>
        /// <param name="fk_TempAuditHeader"></param>
        /// <returns></returns>
        public static bool RemoveOldTempActivityFollowData(int fk_TempAuditHeader)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "SP_REMOVE_TEMP_ACTIVITY_FOLLOW";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("FK_TempAuditHeader", fk_TempAuditHeader, false));


            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),
                        Action = LogAction.Delete.Value,
                        Module = "SP_REMOVE_TEMP_ACTIVITY_FOLLOW",
                        ModuleID = Convert.ToInt32(fk_TempAuditHeader),
                        TableName = LogTable.TempActivityFollowContent.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        private static int CreateTempActivityFollowContent(Hcp dataHcp, int fk_TempAuditHeader)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            var tempActivityFollowID = 0;
            string SQL = "SP_ADD_TEMP_ACTIVITY_FOLLOW_CONTENT";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("FK_TempAuditHeader", fk_TempAuditHeader, false));
            objList.Add(new DSSQLParam("FK_HCPID", dataHcp.hcp_id, false));

            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var insertedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);
                    tempActivityFollowID = insertedID;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),
                        Action = LogAction.Add.Value,
                        Module = "SP_ADD_TEMP_ACTIVITY_FOLLOW_CONTENT",
                        ModuleID = Convert.ToInt32(insertedID),
                        TableName = LogTable.TempActivityFollowContent.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
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

            return tempActivityFollowID;
        }

        private static int CreateTempActivityFollowAction(Action data, int fk_TempAuditHeader,
            int fkActifivtyFollowContentID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            var tempActivityFollowID = 0;
            string SQL = "SP_ADD_TEMP_ACTIVITY_FOLLOW_ACTION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("FK_TempAuditHeader", fk_TempAuditHeader, false));
            objList.Add(new DSSQLParam("FK_TempActivityFollowContentID", fkActifivtyFollowContentID, false));
            objList.Add(new DSSQLParam("FK_ActivityActionID", data.id, false));
            objList.Add(new DSSQLParam("Time", data.time, false));

            //unix timestmp to datetime (Utc)
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(data.timestamp * TimeSpan.TicksPerMillisecond);
            var unixDateTime = new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);

            objList.Add(new DSSQLParam("Timestamp", unixDateTime, false));

            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var insertedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);
                    tempActivityFollowID = insertedID;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),
                        Action = LogAction.Add.Value,
                        Module = "SP_ADD_TEMP_ACTIVITY_FOLLOW_ACTION",
                        ModuleID = Convert.ToInt32(insertedID),
                        TableName = LogTable.TempActivityFollowAction.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
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

            return tempActivityFollowID;
        }

        private static int CreateTempActivityFollowSubAction(SubAction data, int fk_TempAuditHeader,
            int fkActifivtyFollowActionID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            var tempActivityFollowID = 0;
            string SQL = "SP_ADD_TEMP_ACTIVITY_FOLLOW_SUB_ACTION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("FK_TempAuditHeader", fk_TempAuditHeader, false));
            objList.Add(new DSSQLParam("FK_TempActivityFollowActionID", fkActifivtyFollowActionID, false));
            objList.Add(new DSSQLParam("FK_ActivitySubActionID", data.id, false));

            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var insertedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);
                    tempActivityFollowID = insertedID;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),
                        Action = LogAction.Add.Value,
                        Module = "SP_ADD_TEMP_ACTIVITY_FOLLOW_SUB_ACTION",
                        ModuleID = Convert.ToInt32(insertedID),
                        TableName = LogTable.TempActivityFollowSubAction.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
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

            return tempActivityFollowID;
        }

        private static int CreateTempActivityFollowFactor(Factor data, int fk_TempAuditHeader,
            int fkActifivtyFollowContentID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            var tempActivityFollowID = 0;
            string SQL = "SP_ADD_TEMP_ACTIVITY_FOLLOW_FACTOR";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("FK_TempActivityFollowContentID", fkActifivtyFollowContentID, false));
            objList.Add(new DSSQLParam("FK_TempAuditHeader", fk_TempAuditHeader, false));
            objList.Add(new DSSQLParam("FK_ActivityFactorID", data.id, false));
            objList.Add(new DSSQLParam("Time", data.time, false));

            //unix timestmp to datetime (Utc)
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(data.timestamp * TimeSpan.TicksPerMillisecond);
            var unixDateTime = new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);

            objList.Add(new DSSQLParam("Timestamp", unixDateTime, false));

            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var insertedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);
                    tempActivityFollowID = insertedID;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),
                        Action = LogAction.Add.Value,
                        Module = "SP_ADD_TEMP_ACTIVITY_FOLLOW_FACTOR",
                        ModuleID = Convert.ToInt32(insertedID),
                        TableName = LogTable.TempActivityFollowFactor.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
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

            return tempActivityFollowID;
        }

        private static int CreateTempActivityFollowSubFactor(SubFactor data, int fk_TempAuditHeader,
            int fkActifivtyFollowFactorID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            var tempActivityFollowID = 0;
            string SQL = "SP_ADD_TEMP_ACTIVITY_FOLLOW_SUB_FACTOR";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("FK_TempAuditHeader", fk_TempAuditHeader, false));
            objList.Add(new DSSQLParam("FK_TempActivityFollowFactorID", fkActifivtyFollowFactorID, false));
            objList.Add(new DSSQLParam("FK_ActivitySubFactorID", data.id, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var insertedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);
                    tempActivityFollowID = insertedID;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),
                        Action = LogAction.Add.Value,
                        Module = "SP_ADD_TEMP_ACTIVITY_FOLLOW_SUB_FACTOR",
                        ModuleID = Convert.ToInt32(insertedID),
                        TableName = LogTable.TempActivityFollowSubFactor.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
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

            return tempActivityFollowID;
        }

        public static bool UpdateTempActivityFollow(List<Hcp> dataHcp, int fk_TempAuditHeader)
        {

            var success = true;

            try
            {
                RemoveOldTempActivityFollowData(fk_TempAuditHeader);

                if (dataHcp == null)
                    return success;

                foreach (var hcp in dataHcp)
                {
                    var FollowContentID = CreateTempActivityFollowContent(hcp, fk_TempAuditHeader);

                    if (hcp.hcp_actions != null)
                    {
                        foreach (var actions in hcp.hcp_actions)
                        {
                            var activityFollowActionID =
                                CreateTempActivityFollowAction(actions, fk_TempAuditHeader, FollowContentID);

                            if (actions.sub_action != null)
                            {
                                CreateTempActivityFollowSubAction(actions.sub_action, fk_TempAuditHeader,
                                    activityFollowActionID);
                            }
                        }
                    }

                    if (hcp.hcp_factors != null)
                    {
                        foreach (var factors in hcp.hcp_factors)
                        {
                            var activityFollowFactorID =
                                CreateTempActivityFollowFactor(factors, fk_TempAuditHeader, FollowContentID);

                            if (factors.sub_factor != null)
                            {
                                CreateTempActivityFollowSubFactor(factors.sub_factor, fk_TempAuditHeader,
                                    activityFollowFactorID);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return success;
        }

        public static List<Hcp> GetTempAuditByID(int auditTempHeaderID)
        {
            var tempActivityFollowDataSet = new List<Hcp>();

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds = new DataSet();
            string SQL = "SP_GET_TEMP_ACTIVITY_FOLLOW_DATA";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("FK_TempAuditHeader", auditTempHeaderID, false));
            try
            {
                ds = objCDataAccess.ExecuteDataSet(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                objDbCommand.Transaction.Commit();

                var tempActivityFollowContents = Helper.DataTableToList<Hcp>(ds.Tables[0]);
                var tempActivityFollowAction = Helper.DataTableToList<Action>(ds.Tables[1]);
                var tempActivityFollowSubAction = Helper.DataTableToList<SubAction>(ds.Tables[2]);
                var tempActivityFollowFactor = Helper.DataTableToList<Factor>(ds.Tables[3]);
                var tempActivityFollowSubFactor = Helper.DataTableToList<SubFactor>(ds.Tables[4]);

                var actionList = new List<Action>();

                foreach (var tempActivityFollowData in tempActivityFollowContents)
                {

                    var actions = tempActivityFollowAction.Where(o =>
                        o.FK_TempActivityFollowContentID == tempActivityFollowData.AuditTableHcpID).ToList();

                    actions = actions.Select(c =>
                    {
                        c.timestamp = Helper.ToUnixTimestamp(c.TimestampDateTimeUTC);
                        return c;
                    }).ToList();

                    var completeActions = new List<Action>();
                    foreach (var action in actions)
                    {
                        var subAction = tempActivityFollowSubAction
                            .FirstOrDefault(o => o.FK_TempActivityFollowActionID == action.AuditActionTableID);

                        action.sub_action = subAction;
                        completeActions.Add(action);
                    }

                    tempActivityFollowData.hcp_actions = completeActions;

                    var factors = tempActivityFollowFactor.Where(o =>
                        o.FK_TempActivityFollowContentID == tempActivityFollowData.AuditTableHcpID).ToList();

                    factors = factors.Select(c =>
                    {
                        c.timestamp = Helper.ToUnixTimestamp(c.TimestampDateTimeUTC);
                        return c;
                    }).ToList();

                    var completeFactors = new List<Factor>();
                    foreach (var factor in factors)
                    {
                        var subFactors = tempActivityFollowSubFactor
                            .FirstOrDefault(o => o.FK_TempActivityFollowFactorID == factor.FactorAuditTableID);

                        factor.sub_factor = subFactors;
                        completeFactors.Add(factor);
                    }

                    tempActivityFollowData.hcp_factors = completeFactors;

                    tempActivityFollowDataSet.Add(tempActivityFollowData);
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

            return tempActivityFollowDataSet;
        }

        #endregion


        public static DataSet GetActivityActionsAsDataset()
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "GetActivityActions";
            List<DSSQLParam> objList = new List<DSSQLParam>();


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

        public static DataSet GetActivitySubActions(int ActivityActionID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "GetActivitySubActionsByActivityAct";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ActivityActionID", ActivityActionID, false));

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


        public static bool CreateActivityAction(string ActivityAction, int SortOrder, bool IsPPE, bool IsResultActive,
            bool IsOppertunity, bool IsActive, string userId, int? moment, int? ComplianceConditionId)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "Add_ActivityActions";
            List<DSSQLParam> objList = new List<DSSQLParam>();


            int recAdded = 0;
            objList.Add(new DSSQLParam("ActivityAction", ActivityAction, false));
            objList.Add(new DSSQLParam("SortOrder", SortOrder, false));
            objList.Add(new DSSQLParam("IsPPE", IsPPE, false));
            objList.Add(new DSSQLParam("IsResultActive", IsResultActive, false));
            objList.Add(new DSSQLParam("IsOppertunity", IsOppertunity, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("userId", userId, false));
            objList.Add(new DSSQLParam("MomentId", moment, false));
            objList.Add(new DSSQLParam("ComplianceConditionId", ComplianceConditionId, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));

            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var inseredID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {

                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue != null ? s.objParamValue.ToString() : null
                        })),

                        Action = LogAction.Add.Value,
                        Module = "Add_ActivityActions",
                        ModuleID = Convert.ToInt32(inseredID),
                        TableName = LogTable.Program.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }

        public static bool UpdateActivityAction(int ActivityActionItemId, string ActivityAction, int SortOrder,
            bool IsPPE,
            bool IsResultActive, bool IsOppertunity, bool IsActive, int? momentId, string userId, int? ComplianceCondition)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "Update_ActivityActions";
            List<DSSQLParam> objList = new List<DSSQLParam>();


            int recAdded = 0;
            objList.Add(new DSSQLParam("ActivityActionItemId", ActivityActionItemId, false));
            objList.Add(new DSSQLParam("ActivityAction", ActivityAction, false));
            objList.Add(new DSSQLParam("SortOrder", SortOrder, false));
            objList.Add(new DSSQLParam("IsPPE", IsPPE, false));
            objList.Add(new DSSQLParam("IsResultActive", IsResultActive, false));
            objList.Add(new DSSQLParam("IsOppertunity", IsOppertunity, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("MomentId", momentId, false));
            objList.Add(new DSSQLParam("userId", userId, false));
            objList.Add(new DSSQLParam("ComplianceCondition", ComplianceCondition, false));

            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {

                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue != null ? s.objParamValue.ToString() : null
                        })),

                        Action = LogAction.Add.Value,
                        Module = "Update_ActivityActions",
                        ModuleID = Convert.ToInt32(ActivityActionItemId),
                        TableName = LogTable.Program.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }




        public static bool UpdateActivitySubAction(int ActivitySubActionItemId, string ActivitySubAction, int SortOrder,
            bool IsOppertunity, bool IsActive, string userId)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "Update_ActivitySubActions";
            List<DSSQLParam> objList = new List<DSSQLParam>();


            int recAdded = 0;
            objList.Add(new DSSQLParam("ActivitySubActionItemId", ActivitySubActionItemId, false));
            objList.Add(new DSSQLParam("ActivitySubAction", ActivitySubAction, false));
            objList.Add(new DSSQLParam("SortOrder", SortOrder, false));
            objList.Add(new DSSQLParam("IsOppertunity", IsOppertunity, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("userId", userId, false));
            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {

                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "Update_ActivitySubActions",
                        ModuleID = Convert.ToInt32(ActivitySubActionItemId),
                        TableName = LogTable.Program.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }


        public static bool DeleteActivityAction(int ActivityActionItemId)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "Delete_ActivityActions";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ActivityActionItemId", ActivityActionItemId, false));
            int recAdded = 0;
            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "Delete_ActivityActions",
                        ModuleID = Convert.ToInt32(ActivityActionItemId),
                        TableName = LogTable.Program.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }


        public static DataSet GetNextActivityActionSort()
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "GetNextActivityActionSort";
            List<DSSQLParam> objList = new List<DSSQLParam>();


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

        public static DataSet GetComplianceConditionSeperately()
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "Get_ComplianceCondition_Seperate";
            List<DSSQLParam> objList = new List<DSSQLParam>();


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

        public static DataSet GetMomentSeperately()
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "Get_moment_seperate";
            List<DSSQLParam> objList = new List<DSSQLParam>();


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

        public static DataSet GetNextActivitySubActionSort(int ActivityActionItemId)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "GetNextActivitySubSort";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ActivityActionItemId", ActivityActionItemId, false));

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


        public static bool CreateActivitySubAction(string ActivitySubAction, int ActivitySubActionID, int SortOrder,
            bool IsOppertunity, bool IsActive, string userId)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "Add_ActivitySubActions";
            List<DSSQLParam> objList = new List<DSSQLParam>();


            int recAdded = 0;
            objList.Add(new DSSQLParam("ActivitySubAction", ActivitySubAction, false));
            objList.Add(new DSSQLParam("ActivitySubActionID", ActivitySubActionID, false));
            objList.Add(new DSSQLParam("SortOrder", SortOrder, false));
            objList.Add(new DSSQLParam("IsOppertunity", IsOppertunity, false));
            objList.Add(new DSSQLParam("IsActive", IsActive, false));
            objList.Add(new DSSQLParam("UserId", userId, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));
            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var inseredID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "Add_ActivitySubActions",
                        ModuleID = Convert.ToInt32(inseredID),
                        TableName = LogTable.Program.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }



        public static bool DeleteSubActivityAction(int ActivitySubActionItemId)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "Delete_ActivitySubActions";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ActivitySubActionItemId", ActivitySubActionItemId, false));
            int recAdded = 0;
            try
            {

                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "Delete_ActivitySubActions",
                        ModuleID = Convert.ToInt32(ActivitySubActionItemId),
                        TableName = LogTable.Program.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }

            return success;
        }


        public static bool UpdateActionItemOrder(DataTable dtActionItemOrders)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            string SQL = "USP_UPDATE_ItemAction_ORDER";

            DSSQLParam param = new DSSQLParam();
            param.sParamName = "tbl_ActivityActionSortOrder";
            param.dbParamSqlDBType = SqlDbType.Structured;
            param.objParamValue = dtActionItemOrders;
            param.paramDirection = ParameterDirection.Input;

            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(param);
            bool result = false;
            try
            {
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    result = true;
                    objDbCommand.Transaction.Commit();
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

        public static bool getCountFromReporcesingLogExistingLabDetails(string FacilityCode)
        {

            bool exsit = false;

            DataSet dsTempOBSData = new DataSet();

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();

            DbCommand objDbCommand =

                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);

            string SQL = @" Select *  FROM [ReprocessingLog] where Lab =" + FacilityCode;

            dsTempOBSData = objCDataAccess.ExecuteDataSet(objDbCommand, SQL);

            objCDataAccess.Dispose(objDbCommand);

            if (dsTempOBSData.Tables[0].Rows.Count > 0)

                exsit = true;

            else

                exsit = false;

            return exsit;

        }
        public static bool getCountFromEquipmentLogExistingLabDetails(string FacilityCode)

        {

            bool exsit = false;

            DataSet dsTempOBSData = new DataSet();

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();

            DbCommand objDbCommand =

                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);

            string SQL = @"  Select *  FROM [DimUnit] where [FacilityCode] =" + FacilityCode;

            dsTempOBSData = objCDataAccess.ExecuteDataSet(objDbCommand, SQL);

            objCDataAccess.Dispose(objDbCommand);

            if (dsTempOBSData.Tables[0].Rows.Count > 0)

                exsit = true;

            else

                exsit = false;

            return exsit;

        }
        public static bool getCountFromUserExistingLabDetails(string FacilityCode)

        {

            bool exsit = false;

            DataSet dsTempOBSData = new DataSet();

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();

            DbCommand objDbCommand =

                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);

            string SQL = @" Select *  FROM [Users] where Users.Facility_Code =" + FacilityCode;

            dsTempOBSData = objCDataAccess.ExecuteDataSet(objDbCommand, SQL);

            objCDataAccess.Dispose(objDbCommand);

            if (dsTempOBSData.Tables[0].Rows.Count > 0)

                exsit = true;

            else

                exsit = false;

            return exsit;

        }
        public static bool getCountFromReporcesingLogExistingTransducerDetails(int id)
        {

            bool exsit = false;

            DataSet dsTempOBSData = new DataSet();

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();

            DbCommand objDbCommand =

                objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);

            string SQL = @" Select *  FROM [ReprocessingLog] where Transducer =" + id;

            dsTempOBSData = objCDataAccess.ExecuteDataSet(objDbCommand, SQL);

            objCDataAccess.Dispose(objDbCommand);

            if (dsTempOBSData.Tables[0].Rows.Count > 0)

                exsit = true;

            else

                exsit = false;

            return exsit;

        }
        public static bool UpdateActivityItemSubActionOrder(DataTable dt)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            string SQL = "USP_UPDATE_ActivitySubAction_ORDER";

            DSSQLParam param = new DSSQLParam();
            param.sParamName = "tbl_ActivitySubActionSortOrder";
            param.dbParamSqlDBType = SqlDbType.Structured;
            param.objParamValue = dt;
            param.paramDirection = ParameterDirection.Input;

            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(param);
            bool result = false;
            try
            {
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    result = true;
                    objDbCommand.Transaction.Commit();
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


        public static bool IsSessionValied()
        {
            bool valied = true;

            if (string.IsNullOrEmpty(BUSessionUtility.BUSessionContainer.USER_ID))
            {
                valied = false;
            }

            //valied = false; //for testing perpose
            return valied;
        }

        public static int AddTempHcp(int temp_audit_id, int hcp_id, int index)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            int temp_hcp_id = 0;

            string SQL = "USP_ADD_TEMP_HCP";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("temp_audit_id", temp_audit_id, false));
            objList.Add(new DSSQLParam("hcp_id", hcp_id, false));
            objList.Add(new DSSQLParam("index", index, false));
            objList.Add(new DSSQLParam("temp_hcp_id", temp_hcp_id, true));
            try
            {
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    temp_hcp_id = Convert.ToInt32(objDbCommand.Parameters["@temp_hcp_id"].Value);
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "Delete_ActivitySubActions",
                        ModuleID = Convert.ToInt32(temp_hcp_id),
                        TableName = LogTable.TempActivityFollowHCP.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
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

            return temp_hcp_id;
        }

        public static int AddTempAction(int temp_audit_id, int temp_hcp_id, int action_id, int index, int moment, int compliance_condition)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            int temp_action_id = 0;

            string SQL = "USP_ADD_TEMP_ACTION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("temp_audit_id", temp_audit_id, false));
            objList.Add(new DSSQLParam("temp_hcp_id", temp_hcp_id, false));
            objList.Add(new DSSQLParam("action_id", action_id, false));
            objList.Add(new DSSQLParam("index", index, false));
            objList.Add(new DSSQLParam("moment", moment, false));
            objList.Add(new DSSQLParam("compliance_condition", compliance_condition, false));
            objList.Add(new DSSQLParam("temp_action_id", temp_action_id, true));
            try
            {
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    temp_action_id = Convert.ToInt32(objDbCommand.Parameters["@temp_action_id"].Value);
                    objDbCommand.Transaction.Commit();
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

            return temp_action_id;
        }

        public static int AddTempFactor(int temp_audit_id, int temp_hcp_id, int factor_id, int index)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            int temp_factor_id = 0;

            string SQL = "USP_ADD_TEMP_FACTOR";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("temp_audit_id", temp_audit_id, false));
            objList.Add(new DSSQLParam("temp_hcp_id", temp_hcp_id, false));
            objList.Add(new DSSQLParam("factor_id", factor_id, false));
            objList.Add(new DSSQLParam("index", index, false));
            objList.Add(new DSSQLParam("temp_factor_id", temp_factor_id, true));
            try
            {
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    temp_factor_id = Convert.ToInt32(objDbCommand.Parameters["@temp_factor_id"].Value);
                    objDbCommand.Transaction.Commit();
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

            return temp_factor_id;
        }

        public static int AddTempSubAction(int temp_audit_id, int temp_hcp_id, int action_id, int sub_action_id,
            int index, int moment, int compliance_condition)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            int temp_sub_action_id = 0;

            string SQL = "USP_ADD_TEMP_SUB_ACTION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("temp_audit_id", temp_audit_id, false));
            objList.Add(new DSSQLParam("temp_hcp_id", temp_hcp_id, false));
            objList.Add(new DSSQLParam("action_id", action_id, false));
            objList.Add(new DSSQLParam("sub_action_id", sub_action_id, false));
            objList.Add(new DSSQLParam("index", index, false));
            objList.Add(new DSSQLParam("moment", moment, false));
            objList.Add(new DSSQLParam("compliance_condition", compliance_condition, false));
            objList.Add(new DSSQLParam("temp_sub_action_id", temp_sub_action_id, true));
            try
            {
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    temp_sub_action_id = Convert.ToInt32(objDbCommand.Parameters["@temp_sub_action_id"].Value);
                    objDbCommand.Transaction.Commit();
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

            return temp_sub_action_id;
        }

        public static int GetTempActionIdFromSubAction(int temp_sub_action_id)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            int temp_action_id = 0;

            string SQL = "USP_GET_TEMP_ACTION_ID";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("temp_sub_action_id", temp_sub_action_id, false));
            try
            {
                using (DbDataReader dr =
                    objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
                {
                    while (dr.Read())
                    {
                        temp_action_id = Convert.ToInt32(dr["FK_TempActionId"]);
                    }

                    dr.Close();
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

            return temp_action_id;
        }

        public static int AddTempSubFactor(int temp_audit_id, int temp_hcp_id, int factor_id, int sub_factor_id,
            int index)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            int temp_sub_factor_id = 0;

            string SQL = "USP_ADD_TEMP_SUB_FACTOR";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("temp_audit_id", temp_audit_id, false));
            objList.Add(new DSSQLParam("temp_hcp_id", temp_hcp_id, false));
            objList.Add(new DSSQLParam("factor_id", factor_id, false));
            objList.Add(new DSSQLParam("sub_factor_id", sub_factor_id, false));
            objList.Add(new DSSQLParam("index", index, false));
            objList.Add(new DSSQLParam("temp_sub_factor_id", temp_sub_factor_id, true));
            try
            {
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    temp_sub_factor_id = Convert.ToInt32(objDbCommand.Parameters["@temp_sub_factor_id"].Value);
                    objDbCommand.Transaction.Commit();
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

            return temp_sub_factor_id;
        }

        public static int GetTempFactorIdFromSubFactor(int temp_sub_factor_id)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            int temp_factor_id = 0;

            string SQL = "USP_GET_TEMP_FACTOR_ID";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("temp_sub_factor_id", temp_sub_factor_id, false));
            try
            {
                using (DbDataReader dr =
                    objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
                {
                    while (dr.Read())
                    {
                        temp_factor_id = Convert.ToInt32(dr["FK_TempFactorId"]);
                    }

                    dr.Close();
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

            return temp_factor_id;
        }

        public static bool RemoveTempAction(int temp_audit_id, int temp_hcp_id, int temp_action_id)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool result = false;
            string SQL = "USP_REMOVE_TEMP_ACTION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("temp_audit_id", temp_audit_id, false));
            objList.Add(new DSSQLParam("temp_hcp_id", temp_hcp_id, false));
            objList.Add(new DSSQLParam("temp_action_id", temp_action_id, false));
            try
            {
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    result = true;
                    objDbCommand.Transaction.Commit();
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

        public static bool RemoveTempFactor(int temp_audit_id, int temp_hcp_id, int temp_factor_id)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool result = false;
            string SQL = "USP_REMOVE_TEMP_FACTOR";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("temp_audit_id", temp_audit_id, false));
            objList.Add(new DSSQLParam("temp_hcp_id", temp_hcp_id, false));
            objList.Add(new DSSQLParam("temp_factor_id", temp_factor_id, false));
            try
            {
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    result = true;
                    objDbCommand.Transaction.Commit();
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

        public static bool RemoveTempSubFactor(int temp_audit_id, int temp_hcp_id, int temp_factor_id, int temp_sub_factor_id)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool result = false;
            string SQL = "USP_REMOVE_TEMP_SUB_FACTOR";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("temp_audit_id", temp_audit_id, false));
            objList.Add(new DSSQLParam("temp_hcp_id", temp_hcp_id, false));
            objList.Add(new DSSQLParam("temp_factor_id", temp_factor_id, false));
            objList.Add(new DSSQLParam("temp_sub_factor_id", temp_sub_factor_id, false));
            try
            {
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    result = true;
                    objDbCommand.Transaction.Commit();
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

        public static bool RemoveTempSubAction(int temp_audit_id, int temp_hcp_id, int temp_action_id, int temp_sub_action_id)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool result = false;
            string SQL = "USP_REMOVE_TEMP_SUB_ACTION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("temp_audit_id", temp_audit_id, false));
            objList.Add(new DSSQLParam("temp_hcp_id", temp_hcp_id, false));
            objList.Add(new DSSQLParam("temp_action_id", temp_action_id, false));
            objList.Add(new DSSQLParam("temp_sub_action_id", temp_sub_action_id, false));
            try
            {
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    result = true;
                    objDbCommand.Transaction.Commit();
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

        public static bool RemoveTempHcp(int temp_audit_id, int temp_hcp_id)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool result = false;
            string SQL = "USP_REMOVE_TEMP_HCP";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("temp_audit_id", temp_audit_id, false));
            objList.Add(new DSSQLParam("temp_hcp_id", temp_hcp_id, false));
            try
            {
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    result = true;
                    objDbCommand.Transaction.Commit();
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

        public static DataSet GetTempActivityFollowData(int temp_audit_id)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_TEMP_ACTIVITY_FOLLOW_DATA";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("temp_audit_id", temp_audit_id, false));

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

        public static string GetTempActivityFollowDataAsJsonString(int temp_audit_id)
        {
            DataSet dataSet = GetTempActivityFollowData(temp_audit_id);
            List<object> hcp_objects = new List<object>();

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                DataTable tblHcp = dataSet.Tables[0];
                DataTable tblActions = dataSet.Tables[1];
                DataTable tblFactors = dataSet.Tables[2];
                DataTable tblSubActions = dataSet.Tables[3];
                DataTable tblSubFactors = dataSet.Tables[4];

                if (tblHcp.Rows.Count > 0)
                {
                    foreach (DataRow dr_hcp in tblHcp.Rows)
                    {
                        List<object> actions = new List<object>();
                        List<object> factors = new List<object>();
                        int fk_temp_hcp_id = Convert.ToInt32(dr_hcp["temp_hcp_id"]);
                        //filter Actions
                        DataRow[] darrActions = tblActions.Select("fk_temp_hcp_id=" + fk_temp_hcp_id);
                        if (darrActions.Any())
                        {
                            DataTable tblActionFiltered = darrActions.CopyToDataTable();
                            int odd_counter = 0;
                            long start_time = 0, end_time = 0;
                            foreach (DataRow dr_actions in tblActionFiltered.Rows)
                            {
                                int fk_temp_action_id = Convert.ToInt32(dr_actions["temp_id"]);
                                List<object> sub_actions = new List<object>();
                                string description = string.Empty;
                                bool has_sub = false;
                                //filter Sub Actions
                                if (tblSubActions.Rows.Count > 0)
                                {
                                    string search_expression = "fk_temp_hcp_id=" + fk_temp_hcp_id +
                                                               " AND fk_temp_action_id = " + fk_temp_action_id;
                                    DataRow[] darrSubActions = tblSubActions.Select(search_expression);
                                    if (darrSubActions.Any())
                                    {
                                        foreach (DataRow dr_sa in darrSubActions)
                                        {
                                            var sub_action = new
                                            {
                                                id = Convert.ToInt32(dr_sa["id"]),
                                                temp_id = Convert.ToInt32(dr_sa["temp_id"]),
                                                description = Convert.ToString(dr_sa["description"]),
                                                IsOpportunity = Convert.ToBoolean(dr_sa["IsOpportunity"]),
                                                index = Convert.ToInt32(dr_sa["index"]),
                                                addedDateTime = Convert.ToDateTime(dr_sa["addedDateTime"])
                                            };

                                            sub_actions.Add(sub_action);
                                        }
                                    }
                                }

                                string desc = string.Empty;
                                if (Convert.ToBoolean(dr_actions["IsResult"]) && Convert.ToInt32(dr_actions["id"]) != 15)
                                {
                                    if ((odd_counter % 2) == 0)
                                    {
                                        start_time = Convert.ToInt64(dr_actions["timestamp"]);
                                        desc = Convert.ToString(dr_actions["description"]) + " - Started";
                                    }
                                    else
                                    {
                                        end_time = Convert.ToInt64(dr_actions["timestamp"]);
                                        long difference = (end_time - start_time) / 1000;
                                        desc = Convert.ToString(dr_actions["description"]) + " - Stopped (" + difference + " Seconds)";
                                    }
                                    odd_counter++;
                                }
                                else
                                {
                                    desc = Convert.ToString(dr_actions["description"]);
                                }
                                var action = new
                                {
                                    id = Convert.ToInt32(dr_actions["id"]),
                                    temp_id = fk_temp_action_id,
                                    fk_temp_hcp_id = fk_temp_hcp_id,
                                    index = Convert.ToInt32(dr_actions["index"]),
                                    description = desc,
                                    IsOpportunity = Convert.ToBoolean(dr_actions["IsOpportunity"]),
                                    IsPPE = Convert.ToBoolean(dr_actions["IsPPE"]),
                                    IsResult = Convert.ToBoolean(dr_actions["IsResult"]),
                                    time = Convert.ToString(dr_actions["time"]),
                                    timestamp = Convert.ToString(dr_actions["timestamp"]),
                                    moment = Convert.ToInt32(dr_actions["Moment"]),
                                    compliance_condition = Convert.ToInt32(dr_actions["CompilanceCondition"]),
                                    type = "action",
                                    sub_actions = sub_actions
                                };

                                actions.Add(action);
                            }
                        }

                        //Filtered Factors
                        if (tblFactors.Rows.Count > 0)
                        {
                            DataRow[] darrFactros = tblFactors.Select("fk_temp_hcp_id=" + fk_temp_hcp_id);
                            if (darrFactros.Any())
                            {
                                foreach (DataRow dr_factor in darrFactros)
                                {
                                    int fk_temp_factor_id = Convert.ToInt32(dr_factor["temp_id"]);
                                    List<object> sub_factors = new List<object>();
                                    string description = string.Empty;
                                    bool has_sub = false;
                                    //Filter Sub Factors
                                    if (tblSubFactors.Rows.Count > 0)
                                    {
                                        string search_expression = "fk_temp_hcp_id=" + fk_temp_hcp_id + " AND fk_temp_factor_id=" + fk_temp_factor_id;
                                        DataRow[] drrSubFactors =
                                            tblSubFactors.Select(search_expression);
                                        if (drrSubFactors.Any())
                                        {
                                            foreach (DataRow dr_sf in drrSubFactors)
                                            {
                                                var sub_factor = new
                                                {
                                                    id = Convert.ToInt32(dr_sf["id"]),
                                                    temp_id = Convert.ToInt32(dr_sf["temp_id"]),
                                                    description = Convert.ToString(dr_sf["description"]),
                                                    IsOpportunity = Convert.ToBoolean(dr_sf["IsOpportunity"]),
                                                    index = Convert.ToInt32(dr_sf["index"]),
                                                    addedDateTime = Convert.ToDateTime(dr_sf["addedDateTime"])
                                                };
                                                sub_factors.Add(sub_factor);
                                                has_sub = true;
                                            }
                                        }
                                    }

                                    var factor = new
                                    {
                                        id = Convert.ToInt32(dr_factor["id"]),
                                        temp_id = fk_temp_factor_id,
                                        fk_temp_hcp_id = fk_temp_hcp_id,
                                        index = Convert.ToInt32(dr_factor["index"]),
                                        description = Convert.ToString(dr_factor["description"]),
                                        IsOpportunity = Convert.ToBoolean(dr_factor["IsOpportunity"]),
                                        time = Convert.ToString(dr_factor["time"]),
                                        timestamp = Convert.ToString(dr_factor["timestamp"]),
                                        type = "factor",
                                        sub_factors = sub_factors
                                    };

                                    factors.Add(factor);
                                }
                            }
                        }
                        //ahh hcp attributes
                        var hcp = new
                        {
                            hcp_id = Convert.ToInt32(dr_hcp["hcp_id"]),
                            temp_hcp_id = Convert.ToInt32(dr_hcp["temp_hcp_id"]),
                            index = Convert.ToInt32(dr_hcp["index"]),
                            description = Convert.ToString(dr_hcp["description"]) + " " + Convert.ToInt32(dr_hcp["index"]),
                            IsLocked = Convert.ToBoolean(dr_hcp["IsLocked"]),
                            AddedDateTime = Convert.ToDateTime(dr_hcp["AddedDateTime"]),
                            hcp_actions = actions,
                            hcp_factors = factors
                        };
                        hcp_objects.Add(hcp);
                    }
                }
            }

            return JsonConvert.SerializeObject(hcp_objects, Formatting.None);
        }

        public static List<ActivityFollowHCP> GetTempActivityFollowDataAsHCPObject(int temp_audit_id)
        {
            List<ActivityFollowHCP> hcpList = new List<ActivityFollowHCP>();
            try
            {
                var json = GetTempActivityFollowDataAsJsonString(temp_audit_id);
                hcpList = new JavaScriptSerializer().Deserialize<List<ActivityFollowHCP>>(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return hcpList;
        }

        public static int AddActivityFollowAuditFromWebAPI(Model.deserialize.AuditHeader headerData)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            int hcp_id = 0;

            string SQL = "SP_SAVE_AUDIT_HEADER_FROM_WEBAPI";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("AuditId", headerData.tempAuditID, false));
            objList.Add(new DSSQLParam("DeviceID", headerData.deviceID, false));
            objList.Add(new DSSQLParam("AuditDateTime", headerData.auditDateTime, false));
            objList.Add(new DSSQLParam("UserId", headerData.userID, false));
            objList.Add(new DSSQLParam("FacilityId", headerData.facilityCode, false));
            objList.Add(new DSSQLParam("UnitId", headerData.unitCode, false));
            objList.Add(new DSSQLParam("RegionId", headerData.regionCode, false));
            objList.Add(new DSSQLParam("AuditType", headerData.auditTypeCode, false));
            objList.Add(new DSSQLParam("AuditComment", headerData.auditComment, false));
            objList.Add(new DSSQLParam("IsFeedbackProvided", headerData.feedbackProvided, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));
            try
            {
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    hcp_id = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "SP_SAVE_AUDIT_HEADER",
                        ModuleID = Convert.ToInt32(hcp_id),
                        TableName = LogTable.Audit.Value,

                        UserID = 0,
                        UserName =
                            $"WebService",
                        Email = "",
                        UserRole = "",
                        UserRoleID = 0,
                    });
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

            return hcp_id;
        }

        public static int AddActivityFollowAudit(int fk_audit_id, string deviceID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            int hcp_id = 0;

            string SQL = "SP_SAVE_AUDIT_HEADER";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("FKHeaderID", fk_audit_id, false));
            objList.Add(new DSSQLParam("DeviceID", deviceID, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));
            try
            {
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    hcp_id = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "SP_SAVE_AUDIT_HEADER",
                        ModuleID = Convert.ToInt32(hcp_id),
                        TableName = LogTable.Audit.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
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

            return hcp_id;
        }

        public static int AddActivityFollowHcp(int audit_id, ActivityFollowHCP hcpData)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            int hcp_id = 0;

            string SQL = "SP_ADD_ActivityFollowHCP";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("audit_id", audit_id, false));
            objList.Add(new DSSQLParam("hcp_id", hcpData.hcp_id, false));
            objList.Add(new DSSQLParam("index", hcpData.index, false));
            objList.Add(new DSSQLParam("isLocked", hcpData.IsLocked, false));
            objList.Add(new DSSQLParam("addedDateTime", hcpData.AddedDateTime, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));
            try
            {
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    hcp_id = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "SP_ADD_ActivityFollowHCP",
                        ModuleID = Convert.ToInt32(hcp_id),
                        TableName = LogTable.ActivityFollowHCP.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
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

            return hcp_id;
        }

        private static int CreateAuditAction(AuditHcpActions data, int fk_AuditHeader,
            int fkActifivtyFollowHCPID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            var tempActivityFollowID = 0;
            string SQL = "SP_ADD_AUDIT_HCP_ACTION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("FK_AuditId", fk_AuditHeader, false));
            objList.Add(new DSSQLParam("FK_ActionId", data.id, false));
            objList.Add(new DSSQLParam("index", data.index, false));

            objList.Add(new DSSQLParam("FK_HcpId", fkActifivtyFollowHCPID, false));

            //unix timestmp to datetime (Utc)
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(data.timestamp * TimeSpan.TicksPerMillisecond);
            var unixDateTime = new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);

            objList.Add(new DSSQLParam("AddedDateTime", unixDateTime, false));

            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var insertedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);
                    tempActivityFollowID = insertedID;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),
                        Action = LogAction.Add.Value,
                        Module = "SP_ADD_AUDIT_HCP_ACTION",
                        ModuleID = Convert.ToInt32(insertedID),
                        TableName = LogTable.AuditHcpActions.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
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

            return tempActivityFollowID;
        }

        private static int CreateAuditHcpSubAcions(HcpSubActions data, int fk_AuditHeader,
            int fkActifivtyFollowActionID, int activityFollowHCPID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            var tempActivityFollowID = 0;
            string SQL = "SP_ADD_ACTIVITY_FOLLOW_SUB_ACTION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("AuditHeaderId", fk_AuditHeader, false));
            objList.Add(new DSSQLParam("FK_HcpId", activityFollowHCPID, false));
            objList.Add(new DSSQLParam("FK_ActionId", fkActifivtyFollowActionID, false));
            objList.Add(new DSSQLParam("FK_SubActionId", data.id, false));
            objList.Add(new DSSQLParam("Index", data.index, false));
            objList.Add(new DSSQLParam("AddedDateTime", data.addedDateTime, false));

            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var insertedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);
                    tempActivityFollowID = insertedID;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),
                        Action = LogAction.Add.Value,
                        Module = "SP_ADD_TEMP_ACTIVITY_FOLLOW_SUB_ACTION",
                        ModuleID = Convert.ToInt32(insertedID),
                        TableName = LogTable.AuditHcpSubAcions.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
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

            return tempActivityFollowID;
        }

        private static int CreateActivityFollowFactor(HcpFactors data, int fkAuditHeader,
            int fkActifivtyFollowContentID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            var tempActivityFollowID = 0;
            string SQL = "SP_ADD_ACTIVITY_FOLLOW_FACTOR";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("FK_FactorId", data.id, false));
            objList.Add(new DSSQLParam("FK_AuditId", fkAuditHeader, false));
            objList.Add(new DSSQLParam("FK_HcpId", fkActifivtyFollowContentID, false));
            objList.Add(new DSSQLParam("Index", data.index, false));

            //unix timestmp to datetime (Utc)
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(data.timestamp * TimeSpan.TicksPerMillisecond);
            var unixDateTime = new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);

            objList.Add(new DSSQLParam("AddedDateTime", unixDateTime, false));

            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var insertedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);
                    tempActivityFollowID = insertedID;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),
                        Action = LogAction.Add.Value,
                        Module = "SP_ADD_ACTIVITY_FOLLOW_FACTOR",
                        ModuleID = Convert.ToInt32(insertedID),
                        TableName = LogTable.AuditHcpFactors.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
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

            return tempActivityFollowID;
        }

        private static int CreateActivityFollowSubFactor(HcpSubFactors data, int fk_AuditHeader,
            int fkActifivtyFollowHCPID, int fkFactorID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            var tempActivityFollowID = 0;
            string SQL = "SP_ADD_ACTIVITY_FOLLOW_SUB_FACTOR";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("FK_AuditId", fk_AuditHeader, false));
            objList.Add(new DSSQLParam("FK_HcpId", fkActifivtyFollowHCPID, false));
            objList.Add(new DSSQLParam("FK_FactorId", fkFactorID, false));
            objList.Add(new DSSQLParam("FK_SubFactorId", data.id, false));
            objList.Add(new DSSQLParam("Index", data.index, false));
            objList.Add(new DSSQLParam("AddedDateTime", data.addedDateTime, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var insertedID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);
                    tempActivityFollowID = insertedID;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),
                        Action = LogAction.Add.Value,
                        Module = "SP_ADD_ACTIVITY_FOLLOW_SUB_FACTOR",
                        ModuleID = Convert.ToInt32(insertedID),
                        TableName = LogTable.AuditHcpSubFactors.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
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

            return tempActivityFollowID;
        }

        private static int RemoveTempHeaderData(int fk_AuditHeader)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            var tempActivityFollowID = 0;
            string SQL = "SP_REMOVE_OLD_TEMP_AUDIT_DATA";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("TempHeaderID", fk_AuditHeader, false));

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {

                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),
                        Action = LogAction.Delete.Value,
                        Module = "SP_REMOVE_OLD_TEMP_AUDIT_DATA",
                        ModuleID = Convert.ToInt32(fk_AuditHeader),
                        TableName = LogTable.TempAuditHeader.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName =
                            $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
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

            return tempActivityFollowID;
        }

        public static bool SaveActivityFollow(int fk_TempAuditHeader, int auditID)
        {

            var success = true;

            var dataHcp = GetTempActivityFollowDataAsHCPObject(fk_TempAuditHeader);

            try
            {
                foreach (var hcp in dataHcp)
                {
                    var FollowContentID = AddActivityFollowHcp(auditID, hcp);

                    if (hcp.hcp_actions != null)
                    {
                        foreach (var actions in hcp.hcp_actions)
                        {
                            var activityFollowActionID =
                                CreateAuditAction(actions, auditID, FollowContentID);

                            if (actions.sub_action != null)
                            {
                                CreateAuditHcpSubAcions(actions.sub_action, auditID,
                                    activityFollowActionID, FollowContentID);
                            }
                        }
                    }

                    if (hcp.hcp_factors != null)
                    {
                        foreach (var factors in hcp.hcp_factors)
                        {
                            var activityFollowFactorID =
                                CreateActivityFollowFactor(factors, auditID, FollowContentID);

                            if (factors.hcp_factors != null)
                            {
                                CreateActivityFollowSubFactor(factors.hcp_factors, auditID,
                                    FollowContentID, activityFollowFactorID);
                            }

                        }
                    }
                }

                RemoveTempHeaderData(fk_TempAuditHeader);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return success;
        }

        public static int SaveActivityFollowAuditHeader(List<CalculatedAuditHeaderDetails> dataSet)
        {
            DataTable dtActivityHeader = new DataTable();
            dtActivityHeader.Columns.Add(new DataColumn("DeviceID"));
            dtActivityHeader.Columns.Add(new DataColumn("AuditID"));
            dtActivityHeader.Columns.Add(new DataColumn("AuditDate"));
            dtActivityHeader.Columns.Add(new DataColumn("AuditTimeStart"));
            dtActivityHeader.Columns.Add(new DataColumn("AuditTimeEnd"));
            dtActivityHeader.Columns.Add(new DataColumn("AuditDuration"));
            dtActivityHeader.Columns.Add(new DataColumn("UserID"));
            dtActivityHeader.Columns.Add(new DataColumn("FacilityCode"));
            dtActivityHeader.Columns.Add(new DataColumn("UnitCode"));
            dtActivityHeader.Columns.Add(new DataColumn("ObservationID"));
            dtActivityHeader.Columns.Add(new DataColumn("HCWCode"));
            dtActivityHeader.Columns.Add(new DataColumn("Guideline1"));
            dtActivityHeader.Columns.Add(new DataColumn("Guideline2"));
            dtActivityHeader.Columns.Add(new DataColumn("Guideline3"));
            dtActivityHeader.Columns.Add(new DataColumn("Guideline4"));
            dtActivityHeader.Columns.Add(new DataColumn("Guideline5"));
            dtActivityHeader.Columns.Add(new DataColumn("Moment1"));
            dtActivityHeader.Columns.Add(new DataColumn("Moment2"));
            dtActivityHeader.Columns.Add(new DataColumn("Moment3"));
            dtActivityHeader.Columns.Add(new DataColumn("Moment4"));
            dtActivityHeader.Columns.Add(new DataColumn("Moment5"));
            dtActivityHeader.Columns.Add(new DataColumn("ObservationResultNumber"));
            dtActivityHeader.Columns.Add(new DataColumn("Result1"));
            dtActivityHeader.Columns.Add(new DataColumn("ResultTimeStart1"));
            dtActivityHeader.Columns.Add(new DataColumn("ResultTimeEnd1"));
            dtActivityHeader.Columns.Add(new DataColumn("Result2"));
            dtActivityHeader.Columns.Add(new DataColumn("ResultTimeStart2"));
            dtActivityHeader.Columns.Add(new DataColumn("ResultTimeEnd2"));
            dtActivityHeader.Columns.Add(new DataColumn("Result3"));
            dtActivityHeader.Columns.Add(new DataColumn("Result4"));
            dtActivityHeader.Columns.Add(new DataColumn("NoteCode"));
            dtActivityHeader.Columns.Add(new DataColumn("NoteFreeText"));
            dtActivityHeader.Columns.Add(new DataColumn("HCWInstance"));
            dtActivityHeader.Columns.Add(new DataColumn("AuditName"));
            dtActivityHeader.Columns.Add(new DataColumn("Status"));
            dtActivityHeader.Columns.Add(new DataColumn("RegionCode"));
            dtActivityHeader.Columns.Add(new DataColumn("System"));
            dtActivityHeader.Columns.Add(new DataColumn("AuditType"));
            dtActivityHeader.Columns.Add(new DataColumn("PPE1"));
            dtActivityHeader.Columns.Add(new DataColumn("PPE2"));
            dtActivityHeader.Columns.Add(new DataColumn("PPE3"));
            dtActivityHeader.Columns.Add(new DataColumn("PPE4"));
            dtActivityHeader.Columns.Add(new DataColumn("PPE5"));
            dtActivityHeader.Columns.Add(new DataColumn("Precaution1"));
            dtActivityHeader.Columns.Add(new DataColumn("Precaution2"));
            dtActivityHeader.Columns.Add(new DataColumn("Precaution3"));
            dtActivityHeader.Columns.Add(new DataColumn("Precaution4"));
            dtActivityHeader.Columns.Add(new DataColumn("Result1Time"));
            dtActivityHeader.Columns.Add(new DataColumn("Result2Time"));
            dtActivityHeader.Columns.Add(new DataColumn("IsHH"));
            dtActivityHeader.Columns.Add(new DataColumn("IsPPE"));
            dtActivityHeader.Columns.Add(new DataColumn("isDirectReview"));
            dtActivityHeader.Columns.Add(new DataColumn("AuditComment"));
            dtActivityHeader.Columns.Add(new DataColumn("IsFeedbackProvided"));

            if (dataSet != null && dataSet.Any())
            {
                foreach (var header in dataSet)
                {
                    var newHeaderRow = dtActivityHeader.NewRow();
                    newHeaderRow["DeviceID"] = header.DeviceID;
                    newHeaderRow["AuditID"] = header.AuditID;
                    newHeaderRow["AuditDate"] = header.AuditDate;
                    newHeaderRow["AuditTimeStart"] = header.AuditTimeStart;
                    newHeaderRow["AuditTimeEnd"] = header.AuditTimeEnd;
                    newHeaderRow["AuditDuration"] = header.AuditDuration;
                    newHeaderRow["UserID"] = header.UserID;
                    newHeaderRow["FacilityCode"] = header.FacilityCode;
                    newHeaderRow["UnitCode"] = header.UnitCode;
                    newHeaderRow["ObservationID"] = header.ObservationID;
                    newHeaderRow["HCWCode"] = header.HCWCode;
                    newHeaderRow["Guideline1"] = header.Guideline1;
                    newHeaderRow["Guideline2"] = header.Guideline2;
                    newHeaderRow["Guideline3"] = header.Guideline3;
                    newHeaderRow["Guideline4"] = header.Guideline4;
                    newHeaderRow["Guideline5"] = header.Guideline5;
                    newHeaderRow["Moment1"] = header.Moment1;
                    newHeaderRow["Moment2"] = header.Moment2;
                    newHeaderRow["Moment3"] = header.Moment3;
                    newHeaderRow["Moment4"] = header.Moment4;
                    newHeaderRow["Moment5"] = header.Moment5;
                    newHeaderRow["ObservationResultNumber"] = header.ObservationResultNumber;
                    newHeaderRow["Result1"] = header.Result1;
                    newHeaderRow["ResultTimeStart1"] = header.ResultTimeStart1;
                    newHeaderRow["ResultTimeEnd1"] = header.ResultTimeEnd1;
                    newHeaderRow["Result2"] = header.Result2;
                    newHeaderRow["ResultTimeStart2"] = header.ResultTimeStart2;
                    newHeaderRow["ResultTimeEnd2"] = header.ResultTimeEnd2;
                    newHeaderRow["Result3"] = header.Result3;
                    newHeaderRow["Result4"] = header.Result4;
                    newHeaderRow["NoteCode"] = header.NoteCode;
                    newHeaderRow["NoteFreeText"] = header.NoteFreeText;
                    newHeaderRow["HCWInstance"] = header.HCWInstance;
                    newHeaderRow["AuditName"] = header.AuditName;
                    newHeaderRow["Status"] = header.Status;
                    newHeaderRow["RegionCode"] = header.RegionCode;
                    newHeaderRow["System"] = header.System;
                    newHeaderRow["AuditType"] = header.AuditType;
                    newHeaderRow["PPE1"] = header.PPE1;
                    newHeaderRow["PPE2"] = header.PPE2;
                    newHeaderRow["PPE3"] = header.PPE3;
                    newHeaderRow["PPE4"] = header.PPE4;
                    newHeaderRow["PPE5"] = header.PPE5;
                    newHeaderRow["Precaution1"] = header.Precaution1;
                    newHeaderRow["Precaution2"] = header.Precaution2;
                    newHeaderRow["Precaution3"] = header.Precaution3;
                    newHeaderRow["Precaution4"] = header.Precaution4;
                    newHeaderRow["Result1Time"] = header.Result1Time;
                    newHeaderRow["Result2Time"] = header.Result2Time;
                    newHeaderRow["IsHH"] = header.IsHH;
                    newHeaderRow["IsPPE"] = header.IsPPE;
                    newHeaderRow["isDirectReview"] = header.isDirectReview;
                    newHeaderRow["AuditComment"] = header.AuditComment;
                    newHeaderRow["IsFeedbackProvided"] = header.IsFeedbackProvided;


                    dtActivityHeader.Rows.Add(newHeaderRow);
                }
            }

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);

            string SQL = "USP_ADD_HEADER_AUDIT_FROM_WEBSERVICES";

            DSSQLParam param = new DSSQLParam();
            param.sParamName = "tbl_AuditHeader";
            param.dbParamSqlDBType = SqlDbType.Structured;
            param.objParamValue = dtActivityHeader;
            param.paramDirection = ParameterDirection.Input;

            DSSQLParam paramReturn = new DSSQLParam();
            paramReturn.sParamName = "ResultID";
            paramReturn.dbParamSqlDBType = SqlDbType.Int;
            paramReturn.objParamValue = 0;
            paramReturn.paramDirection = ParameterDirection.Output;

            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(param);
            objList.Add(paramReturn);

            // objList.Add(new DSSQLParam("ResultID", 0, true));

            bool result = false;

            var responseID = 0;
            try
            {
                int recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    result = true;
                    objDbCommand.Transaction.Commit();

                    var inseredID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);
                    responseID = inseredID;

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {

                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue != null ? s.objParamValue.ToString() : null
                        })),

                        Action = LogAction.Add.Value,
                        Module = "Add_ActivityActions",
                        ModuleID = Convert.ToInt32(inseredID),
                        TableName = LogTable.Audit.Value,

                        UserID = 0,
                        UserName =
                            $"Web API",
                        Email = "",
                        UserRole = "",
                        UserRoleID = 0,
                    });
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
            return responseID;

        }

        public static int SaveActivityFollowFromWebAPI(Model.deserialize.AuditHeader dataSet, int auditHeaderID)
        {
            var auditID = 0;

            try
            {
                dataSet.auditDateTime = Helper.TimestampToDateTime(dataSet.timestamp);

                // auditID = AddActivityFollowAuditFromWebAPI(dataSet);
                auditID = auditHeaderID;

                foreach (var hcp in dataSet.hcpData)
                {

                    var unixDateTime = Helper.TimestampToDateTime(hcp.timestamp);

                    hcp.AddedDateTime = unixDateTime;
                    hcp.IsLocked = false;

                    var FollowContentID = AddActivityFollowHcp(auditID, hcp);

                    if (hcp.hcp_actions != null)
                    {
                        foreach (var actions in hcp.hcp_actions)
                        {
                            var activityFollowActionID =
                                CreateAuditAction(actions, auditID, FollowContentID);

                            if (actions.isSelectedFromChild)
                            {
                                var subAction = new HcpSubActions
                                {
                                    id = Convert.ToInt32(actions.activityChildCode),
                                    index = 0,
                                    addedDateTime = Helper.TimestampToDateTime(actions.timestamp)
                                };

                                CreateAuditHcpSubAcions(subAction, auditID,
                                    activityFollowActionID, FollowContentID);
                            }
                        }
                    }

                    if (hcp.hcp_factors != null)
                    {
                        foreach (var factors in hcp.hcp_factors)
                        {
                            var activityFollowFactorID =
                                CreateActivityFollowFactor(factors, auditID, FollowContentID);

                            if (factors.isSelectedFromChild)
                            {
                                var subFactor = new HcpSubFactors
                                {
                                    id = Convert.ToInt32(factors.activityChildCode),
                                    index = 0,
                                    addedDateTime = Helper.TimestampToDateTime(factors.timestamp)
                                };

                                CreateActivityFollowSubFactor(subFactor, auditID,
                                    FollowContentID, activityFollowFactorID);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return auditID;
        }

        #region Method Add By Ujjaval For ActivityFollowConfiguration [ fetch / add / edit / delete ]

        public static bool CreateActivityFollowConfiguration(Int32 AuditDuration, string AdditionalTime,
            Int32 MinHCWObservation, bool EnableResultTimer, string ResultTimerDuration)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_ActivityFollowCONFIGURATION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("AuditDuration", AuditDuration, false));
            if (AdditionalTime.Trim().Length > 0)
            {
                objList.Add(new DSSQLParam("AdditionalTime", AdditionalTime, false));
            }
            if (ResultTimerDuration.Trim().Length > 0)
            {
                objList.Add(new DSSQLParam("ResultTimerDuration", ResultTimerDuration, false));
            }
            objList.Add(new DSSQLParam("MinHCWObservation", MinHCWObservation, false));
            objList.Add(new DSSQLParam("EnableResultTimer", EnableResultTimer, false));
            objList.Add(new DSSQLParam("ResultID", 0, true));

            //objList.Add(new DSSQLParam("MinObservationPerHCW", ObservationPerHCW, false));
            //objList.Add(new DSSQLParam("MaxObservationPerHCW", MaxObservationPerHCW, false));
            //objList.Add(new DSSQLParam("MinObservation", MinObservation, false));



            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    var inseredID = Convert.ToInt32(objDbCommand.Parameters["@ResultID"].Value);

                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Add.Value,
                        Module = "USP_ADD_SYSTEMCONFIGURATION",
                        ModuleID = Convert.ToInt32(inseredID),
                        TableName = LogTable.SystemConfiguration.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName = $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }

        public static bool UpdateActivityFollowConfiguration(Int32 ActivityFollowConfigurationID, int AuditDuration, string AdditionalTime,
           Int32 MinHCWObservation, bool EnableResultTimer, string ResultTimerDuration)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_UPDATE_ActivityFollowCONFIGURATION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ActivityFollowConfigurationID", ActivityFollowConfigurationID, false));
            objList.Add(new DSSQLParam("AuditDuration", AuditDuration, false));
            if (AdditionalTime.Trim().Length > 0)
            {
                objList.Add(new DSSQLParam("AdditionalTime", AdditionalTime, false));
            }
            if (ResultTimerDuration.Trim().Length > 0)
            {
                objList.Add(new DSSQLParam("ResultTimerDuration", ResultTimerDuration, false));
            }
            objList.Add(new DSSQLParam("MinHCWObservation", MinHCWObservation, false));
            objList.Add(new DSSQLParam("EnableResultTimer", EnableResultTimer, false));
            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogInfo(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Edit.Value,
                        Module = "USP_UPDATE_SYSTEMCONFIGURATION",
                        ModuleID = Convert.ToInt32(ActivityFollowConfigurationID),
                        TableName = LogTable.SystemConfiguration.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName = $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }

        public static bool DeleteActivityFollowConfiguration(Int32 SystemConfigurationID)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_DELETE_ActivityFollowCONFIGURATION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("SystemConfigurationID", SystemConfigurationID, false));
            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

                    // Audit Trail
                    SerilogAuditTrail.LogWarning(new AuditTrailDataModel
                    {
                        Description = JsonConvert.SerializeObject(objList.Select(s => new LogType
                        {
                            Key = s.sParamName,
                            Value = s.objParamValue.ToString()
                        })),

                        Action = LogAction.Delete.Value,
                        Module = "USP_DELETE_SYSTEMCONFIGURATION",
                        ModuleID = Convert.ToInt32(SystemConfigurationID),
                        TableName = LogTable.SystemConfiguration.Value,

                        UserID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID),
                        UserName = $"{BUSessionUtility.BUSessionContainer.FirstName} {BUSessionUtility.BUSessionContainer.LastName}",
                        Email = BUSessionUtility.BUSessionContainer.Email,
                        UserRole = BUSessionUtility.BUSessionContainer.ROLE,
                        UserRoleID = Convert.ToInt32(BUSessionUtility.BUSessionContainer.ROLES_FOR_USER),
                    });
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }


        public static List<SystemConfiguration> GetAllActivityFollowConfigurationList()
        {
            SystemConfiguration OBJ_SystemConfiguration = new SystemConfiguration();
            List<SystemConfiguration> LIST_SystemConfiguration = new List<SystemConfiguration>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GET_ALLActivityFollowCONFIGURATION";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
            {
                while (dr.Read())
                {
                    OBJ_SystemConfiguration = new SystemConfiguration();
                    OBJ_SystemConfiguration.ConfigurationID = Convert.ToInt32(dr["SystemConfigurationID"].ToString());
                    OBJ_SystemConfiguration.AuditDuration = Convert.ToInt32(dr["AuditDuration"].ToString());
                    OBJ_SystemConfiguration.AdditionalTime = dr["AdditionalTime"].ToString();
                    OBJ_SystemConfiguration.MinHCWObservation = Convert.ToInt32(dr["MinHCWObservation"].ToString());
                    OBJ_SystemConfiguration.MinObservationPerHCW = Convert.ToInt32(dr["MinObservationPerHCW"].ToString());
                    if (dr["MaxObservationPerHCW"] != DBNull.Value)
                    {
                        OBJ_SystemConfiguration.MaxObservationPerHCW = Convert.ToInt32(dr["MaxObservationPerHCW"].ToString());
                    }
                    else
                    {
                        OBJ_SystemConfiguration.MaxObservationPerHCW = 0;
                    }
                    OBJ_SystemConfiguration.EnableResultTimer = Convert.ToBoolean(dr["EnableResultTimer"].ToString());
                    OBJ_SystemConfiguration.LastChangedDate = dr["LastChangedDate"].ToString();
                    OBJ_SystemConfiguration.ResultTimerDuration = dr["ResultTimerDuration"].ToString();
                    OBJ_SystemConfiguration.MinObservation = Convert.ToInt32(dr["MinObservation"].ToString());

                    LIST_SystemConfiguration.Add(OBJ_SystemConfiguration);
                }

                dr.Close();
            }
            objCDataAccess.Dispose(objDbCommand);
            return LIST_SystemConfiguration;
        }

        //Author: Grishma
          #endregion

        public static bool RemoveTempHcpActivities(int temp_audit_id, int temp_hcp_id)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_REMOVE_TEMP_HCP_ACTIVITIES";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("temp_audit_id", temp_audit_id, false));
            objList.Add(new DSSQLParam("temp_hcp_id", temp_hcp_id, false));
            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }

        public static bool LockUnlockHcp(int temp_audit_id, int temp_hcp_id, bool is_locked)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_LOCK_UNLOCK_TEMP_HCP";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("temp_audit_id", temp_audit_id, false));
            objList.Add(new DSSQLParam("temp_hcp_id", temp_hcp_id, false));
            objList.Add(new DSSQLParam("is_locked", is_locked, false));
            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }

       

       public static bool CheckUsernameIsTaken(string username)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            bool isAvailable = false;
            try
            {
                string SQL = "USP_CHECK_USERNAME_AVAILABILITY";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("username", username, false));
                using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
                {
                    isAvailable = !dr.HasRows;
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
            return isAvailable;
        }

        public static bool CheckUsernameIsTakenOnEdit(int user_id, string username)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            bool isAvailable = false;
            try
            {
                string SQL = "USP_CHECK_USERNAME_AVAILABILITY_ON_EDIT";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("user_id", user_id, false));
                objList.Add(new DSSQLParam("username", username, false));
                using (DbDataReader dr = objCDataAccess.ExecuteReader(objDbCommand, SQL, CommandType.StoredProcedure, objList))
                {
                    isAvailable = !dr.HasRows;
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
            return isAvailable;
        }

        public static DataTable GetAllReprocessingLogs()
        {
            DataTable table;
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            try
            {

                string SQL = "USP_GETALL_ReprocessingLogs";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                table = objCDataAccess.ExecuteDataTable(objDbCommand, SQL, CommandType.StoredProcedure, objList);

            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                table = null;
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return table;

        }

        public static DataTable GETALL_SolutionTestingLog()
        {
            DataTable table;
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            try
            {

                string SQL = "USP_GETALL_SolutionTestingLog";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                table = objCDataAccess.ExecuteDataTable(objDbCommand, SQL, CommandType.StoredProcedure, objList);

            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                table = null;
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return table;

        }
        public static bool UpdateReprocessingLog(string Date, string Transducer,string Lab, string Tec, string VisitNumber, string TimeHLDInitiated,string TimeHLDCompleted,int EditId)
        {
            string User_ID = BUSessionUtility.BUSessionContainer.USER_ID;
            
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            try
            {
                string SQL = "USP_UPDATE_ReprocessingLog";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("EditId", EditId, false));
                objList.Add(new DSSQLParam("Date", Date, false));
                objList.Add(new DSSQLParam("Transducer", Transducer, false));
                objList.Add(new DSSQLParam("Lab", Lab, false));
                objList.Add(new DSSQLParam("Tec", Tec, false));
                objList.Add(new DSSQLParam("VisitNumber", VisitNumber, false));
                objList.Add(new DSSQLParam("TimeHLDInitiated", TimeHLDInitiated, false));
                objList.Add(new DSSQLParam("TimeHLDCompleted", TimeHLDCompleted, false));
                objList.Add(new DSSQLParam("LastModifiedBy", User_ID, false));
                objList.Add(new DSSQLParam("LastModifiedOn", DateTime.Now, false));

                int recAdded = 0;
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }


        public static bool CreateHdlReprocessingLog( DateTime Date, string Transducer,string Lab,string Tec, string VisitNumber, string TimeHLDInitiated,string TimeHLDCompleted)
        {
            string User_ID = BUSessionUtility.BUSessionContainer.USER_ID;
            
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_Reprocessing_Log";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Date", Date, false));
            objList.Add(new DSSQLParam("Transducer", Transducer, false));
            objList.Add(new DSSQLParam("Lab", Lab, false));
            objList.Add(new DSSQLParam("Tec", Tec, false));
            objList.Add(new DSSQLParam("VisitNumber", VisitNumber, false));
            objList.Add(new DSSQLParam("TimeHLDInitiated", TimeHLDInitiated, false));
            objList.Add(new DSSQLParam("TimeHLDCompleted", TimeHLDCompleted, false));
            objList.Add(new DSSQLParam("CreatedBy", User_ID, false));
            objList.Add(new DSSQLParam("CreatedOn", DateTime.Now, false)); 
            objList.Add(new DSSQLParam("LastModifiedBy", User_ID, false));
            objList.Add(new DSSQLParam("LastModifiedOn", DateTime.Now, false)); 

            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }
        public static bool CreateTransduser(string Transducer,string Unit, string Discreption,string DeviceNo)
        {
            string User_ID = BUSessionUtility.BUSessionContainer.USER_ID;
                
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_ADD_New_Transducer";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Transducer_serial_no", Transducer, false));
            objList.Add(new DSSQLParam("UnitCode", Unit, false));
            objList.Add(new DSSQLParam("Description", Discreption, false));
            objList.Add(new DSSQLParam("DeviceNo", DeviceNo, false));
 
            objList.Add(new DSSQLParam("CreatedBy", User_ID, false));
            objList.Add(new DSSQLParam("CreatedOn", DateTime.Now, false));            
            
            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }
        public static DataSet GetAllSerialNumberCombo(){

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_ALL_SERIAL_NUMBER_FOR_COMBO";
            List<DSSQLParam> objList = new List<DSSQLParam>();
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

        public static DataSet GetDeviceIdByUser()
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_DeviceIdByUser";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("userId", BUSessionUtility.BUSessionContainer.USER_ID, false));
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
        public static DataSet GetUserFacilityCombo()
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_USER_FACILITY_FOR_COMBO";
            List<DSSQLParam> objList = new List<DSSQLParam>();
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
        public static DataSet GetFacilityIdByUser()
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_FacilityIdByUser";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("userId", BUSessionUtility.BUSessionContainer.USER_ID, false));
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
        public static DataSet GetUsersCombo()
        {

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_USER_FOR_COMBO";
            List<DSSQLParam> objList = new List<DSSQLParam>();
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

        public static DataSet GetAllunitForCombo()
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            DataSet ds;
            string SQL = "USP_GET_ALL_Unit_FOR_COMBO";
            List<DSSQLParam> objList = new List<DSSQLParam>();
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

        public static bool CreateHldSolutionTestingLog(string Date, string Time, int LotNumber, string Temp, bool IsDaily, bool IsBeforeChanging, string DateChange, string NextDateChange,string BottleNumber)
        {

            
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_HldSolutionTestingLog";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Date", Date, false));
            objList.Add(new DSSQLParam("Time", Time, false));
            objList.Add(new DSSQLParam("LotNumber", LotNumber, false));
            objList.Add(new DSSQLParam("Temp", Temp, false));
            objList.Add(new DSSQLParam("DailyPassFail", IsDaily, false));
            objList.Add(new DSSQLParam("BeforeChangingPassFail", IsBeforeChanging, false));
            objList.Add(new DSSQLParam("DateChanged", DateChange, false));
            objList.Add(new DSSQLParam("NextChangeDate", NextDateChange, false));
            objList.Add(new DSSQLParam("LastModified", BUSessionUtility.BUSessionContainer.USER_ID, false));
            objList.Add(new DSSQLParam("BottleNumber", BottleNumber, false));
            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }

        public static bool EditHldSolutionTestingLog(string Date, string Time, int LotNumber, string Temp, bool IsDaily, bool IsBeforeChanging, string DateChange, string NextDateChange, int EditId,string BottleNumber)
        {

            string User_ID = BUSessionUtility.BUSessionContainer.USER_ID;

            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_Update_HldSolutionTestingLog";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Date", Date, false));
            objList.Add(new DSSQLParam("Time", Time, false));
            objList.Add(new DSSQLParam("LotNumber", LotNumber, false));
            objList.Add(new DSSQLParam("Temp", Temp, false));
            objList.Add(new DSSQLParam("DailyPassFail", IsDaily, false));
            objList.Add(new DSSQLParam("BeforeChangingPassFail", IsBeforeChanging, false));
            objList.Add(new DSSQLParam("DateChanged", DateChange, false));
            objList.Add(new DSSQLParam("NextChangeDate", NextDateChange, false));
            objList.Add(new DSSQLParam("LastModified", BUSessionUtility.BUSessionContainer.USER_ID, false));
            objList.Add(new DSSQLParam("EditId", EditId, false));
            objList.Add(new DSSQLParam("BottleNumber", BottleNumber, false));
            objList.Add(new DSSQLParam("LastModifiedBy", User_ID, false));
            objList.Add(new DSSQLParam("LastModifiedOn", DateTime.Now, false));


            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }
        public static DataTable GetSolutionTestingLogDetails(int Id)
        {
            DataTable dataTable = new DataTable();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GET_For_Edit_SolutionTestingLog";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Id", Id, false));
            try
            {
                dataTable = objCDataAccess.ExecuteDataTable(objDbCommand, SQL, CommandType.StoredProcedure, objList);
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
            return dataTable;
        }

        public static bool DeleteHldSolutionTestingLog(int Id)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;

            try
            {
                string SQL = "USP_DeleteHldSolutionTestingLog";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("Id", Id, false));
                int recAdded = 0;
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }
       
        public static bool EditTransduser(string Transducer, string Unit, string Discreption, string DeviceNo, int EditId)
        {
            string User_ID = BUSessionUtility.BUSessionContainer.USER_ID;
            
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_Update_Transducer";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Transducer_serial_no", Transducer, false));
            objList.Add(new DSSQLParam("UnitCode", Unit, false));
            objList.Add(new DSSQLParam("Description", Discreption, false));
            objList.Add(new DSSQLParam("DeviceNo", DeviceNo, false));
            objList.Add(new DSSQLParam("EditId", EditId, false));
            objList.Add(new DSSQLParam("LastModifiedBy", User_ID, false));
            objList.Add(new DSSQLParam("LastModifiedOn", DateTime.Now, false));  



            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }
        public static DataTable GETALL_ListOfTransduser()
        {
            DataTable table;
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            try
            {

                string SQL = "USP_GETALL_Transducer";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                table = objCDataAccess.ExecuteDataTable(objDbCommand, SQL, CommandType.StoredProcedure, objList);

            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                table = null;
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return table;

        }
        public static bool DeleteHldReprocessingLog(int Id)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;

            try
            {
                string SQL = "USP_DeleteHldReprocessingLog";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("ReprocessingLogID", Id, false));
                int recAdded = 0;
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }
        public static DataTable GetreprocessingLogDetails(int Id)
        {
            DataTable dataTable = new DataTable();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand =
                objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GET_For_Edit_ReprocessingLog";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Id", Id, false));
            try
            {
                dataTable = objCDataAccess.ExecuteDataTable(objDbCommand, SQL, CommandType.StoredProcedure, objList);
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
            return dataTable;
        }

        public static bool DeleteTransduser(int Id)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;

            try
            {
                string SQL = "USP_DeleteTransduser";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("Id", Id, false));
                int recAdded = 0;
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }
        public static DataTable GetTransduserDetails(int Id)
        {
            DataTable dataTable = new DataTable();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            string SQL = "USP_GET_For_Transducer";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("Id", Id, false));
            try
            {
                dataTable = objCDataAccess.ExecuteDataTable(objDbCommand, SQL, CommandType.StoredProcedure, objList);
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
            return dataTable;
        }

        public static LogRowData GetAllLogRowData()
        {
            var response = new LogRowData();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            try
            {

                string SQL = "SP_GET_ROW_DATA";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                var dataSet = objCDataAccess.ExecuteDataSet(objDbCommand, SQL, CommandType.StoredProcedure, objList);

                response.SuggestedPriorities = Helper.DataTableToList<SuggestedPriority>(dataSet.Tables[0]);
                response.Locations = Helper.DataTableToList<Location>(dataSet.Tables[1]);
                response.Modalities = Helper.DataTableToList<Modality>(dataSet.Tables[2]);
                response.ErrorCategories = Helper.DataTableToList<ErrorCategory>(dataSet.Tables[3]);
                response.PossibleConsequences = Helper.DataTableToList<PossibleConsequence>(dataSet.Tables[4]);
                response.RemedialActionTakenMRPs = Helper.DataTableToList<RemedialActionTakenMRP>(dataSet.Tables[5]);
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
            return response;

        }

        /// <summary>
        /// Add new error log data
        /// </summary>
        /// <param name="errorLogXray"></param>
        /// <returns></returns>
        public static bool AddNewErrorLog(ErrorLogXray errorLogXray)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "SP_SAVEDATA_ERROR_LOG_XRAY";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("DateOfError", errorLogXray.DateOfError, false));
            objList.Add(new DSSQLParam("Operator", errorLogXray.Operator, false));
            objList.Add(new DSSQLParam("SuggestedPriorityID", errorLogXray.SuggestedPriorityID, false));
            objList.Add(new DSSQLParam("LocationID", errorLogXray.LocationID, false));
            objList.Add(new DSSQLParam("ModalityID", errorLogXray.ModalityID, false));
            objList.Add(new DSSQLParam("ErrorCategoryID", errorLogXray.ErrorCategoryID, false));
            objList.Add(new DSSQLParam("DescriptionOfError", errorLogXray.DescriptionOfError, false));
            objList.Add(new DSSQLParam("PossibleConsequenceID", errorLogXray.PossibleConsequenceID, false));
            objList.Add(new DSSQLParam("RemedialActionTakenMRPID", errorLogXray.RemedialActionTakenMRPID, false));
            objList.Add(new DSSQLParam("RemedialAction", errorLogXray.RemedialAction, false));
            objList.Add(new DSSQLParam("Notes", errorLogXray.Notes, false));
            objList.Add(new DSSQLParam("UserId", Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID), false));
            objList.Add(new DSSQLParam("PersonEnteringItem", errorLogXray.PersonEnteringItem, false));

            try
            {
                var recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }
            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }

        /// <summary>
        /// Get all the error log data
        /// </summary>
        /// <returns></returns>
        public static List<ErrorLogXray> GetAllLogData()
        {
            var response = new List<ErrorLogXray>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            try
            {

                string SQL = "SP_GET_ERROR_LOG_DATA";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                var dataTable = objCDataAccess.ExecuteDataTable(objDbCommand, SQL, CommandType.StoredProcedure, objList);

                response = Helper.DataTableToList<ErrorLogXray>(dataTable);
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
            return response;

        }

        /// <summary>
        /// Get error log data by row ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static ErrorLogXray GetLogDataById(int Id)
        {
            var response = new ErrorLogXray();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            try
            {

                string SQL = "SP_GET_ERROR_LOG_DATA_BYID";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("ID", Id, false));
                var dataTable = objCDataAccess.ExecuteDataTable(objDbCommand, SQL, CommandType.StoredProcedure, objList);

                if (dataTable.Rows.Count > 0)
                {
                    response = Helper.DataTableToList<ErrorLogXray>(dataTable).First();
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
            return response;

        }

        /// <summary>
        /// Update the error log
        /// </summary>
        /// <param name="errorLogXray"></param>
        /// <returns></returns>
        public static bool UpdateErrorLog(ErrorLogXray errorLogXray)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "SP_UPDATEDATA_ERROR_LOG_XRAY";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ID", errorLogXray.Id, false));
            objList.Add(new DSSQLParam("DateOfError", errorLogXray.DateOfError, false));
            objList.Add(new DSSQLParam("Operator", errorLogXray.Operator, false));
            objList.Add(new DSSQLParam("SuggestedPriorityID", errorLogXray.SuggestedPriorityID, false));
            objList.Add(new DSSQLParam("LocationID", errorLogXray.LocationID, false));
            objList.Add(new DSSQLParam("ModalityID", errorLogXray.ModalityID, false));
            objList.Add(new DSSQLParam("ErrorCategoryID", errorLogXray.ErrorCategoryID, false));
            objList.Add(new DSSQLParam("DescriptionOfError", errorLogXray.DescriptionOfError, false));
            objList.Add(new DSSQLParam("PossibleConsequenceID", errorLogXray.PossibleConsequenceID, false));
            objList.Add(new DSSQLParam("RemedialActionTakenMRPID", errorLogXray.RemedialActionTakenMRPID, false));
            objList.Add(new DSSQLParam("RemedialAction", errorLogXray.RemedialAction, false));
            objList.Add(new DSSQLParam("Notes", errorLogXray.Notes, false));
            objList.Add(new DSSQLParam("UserId", Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID), false));
            objList.Add(new DSSQLParam("PersonEnteringItem", errorLogXray.PersonEnteringItem, false));

            try
            {
                var recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }
            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }

        /// <summary>
        /// Delete error log
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool DeleteErrorLog(int Id)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;

            try
            {
                string SQL = "SP_DELETE_ERROR_LOG";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("Id", Id, false));
                int recAdded = 0;
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }

        /// <summary>
        /// Get All row data for Assistance Log
        /// </summary>
        /// <returns></returns>
        public static AssistanceLogRowData GetAllLogRowDataForAssistanceLog()
        {
            var response = new AssistanceLogRowData();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            try
            {

                string SQL = "SP_GET_ROW_DATA_FOR_ASSISTANCE_LOG";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                var dataSet = objCDataAccess.ExecuteDataSet(objDbCommand, SQL, CommandType.StoredProcedure, objList);

                response.Operators = Helper.DataTableToList<Operators>(dataSet.Tables[0]);
                response.SuggestedPriorities = Helper.DataTableToList<SuggestedPriority>(dataSet.Tables[1]);
                response.Locations = Helper.DataTableToList<Location>(dataSet.Tables[2]);
                response.Areas = Helper.DataTableToList<Area>(dataSet.Tables[3]);
                response.Modalities = Helper.DataTableToList<Modality>(dataSet.Tables[4]);
                response.ReferredTo = Helper.DataTableToList<ReferredTo>(dataSet.Tables[5]);
                response.Responses = Helper.DataTableToList<Responses>(dataSet.Tables[6]);
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
            return response;
        }

        /// <summary>
        ///  Need Assistance Log
        /// </summary>
        /// <param name="errorLogXray"></param>
        /// <returns></returns>
        public static bool AddNewNeedAssistanceLog(NeedAssistanceLog_Xray log)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "SP_SAVEDATA_NEED_ASSISTANCE_LOG";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("DateTimeLog", log.DateTimeLog, false));
            objList.Add(new DSSQLParam("OperatorId", log.OperatorId, false));
            objList.Add(new DSSQLParam("OperatorName", log.OperatorName, false));
            objList.Add(new DSSQLParam("EnteringPersonName", log.PersonName, false));
            objList.Add(new DSSQLParam("SuggestedPriorityId", log.SuggestedPriorityId, false));
            objList.Add(new DSSQLParam("LocationId", log.LocationId, false));
            objList.Add(new DSSQLParam("AreaId", log.AreaId, false));
            objList.Add(new DSSQLParam("ModalityId", log.ModalityId, false));
            objList.Add(new DSSQLParam("DescriptionOfIssue", log.DescriptionOfIssue, false));
            if(log.ReferredTo_ResponderID != 0)
                objList.Add(new DSSQLParam("ReferredTo_ResponderID", log.ReferredTo_ResponderID, false));
            if(log.ResponseId != 0)
                objList.Add(new DSSQLParam("ResponseId", log.ResponseId , false));
            objList.Add(new DSSQLParam("Notes", log.Notes, false));
            objList.Add(new DSSQLParam("UserId", Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID), false));


            try
            {
                var recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }
            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }

        /// <summary>
        /// Get all Need assistance log
        /// </summary>
        /// <returns></returns>
        public static List<NeedAssistanceLog_Xray> GetAllNeedAssistanceLogData()
        {
            var response = new List<NeedAssistanceLog_Xray>();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            try
            {

                string SQL = "SP_GET_ALL_ASSISTANCE_LOG";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                var dataTable = objCDataAccess.ExecuteDataTable(objDbCommand, SQL, CommandType.StoredProcedure, objList);

                response = Helper.DataTableToList<NeedAssistanceLog_Xray>(dataTable);
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
            return response;

        }

        /// <summary>
        /// Get need assistanbce3 log by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static NeedAssistanceLog_Xray GetNeedAssiatceLogDataById(int Id)
        {
            var response = new NeedAssistanceLog_Xray();
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(false, IsolationLevel.ReadCommitted, "application", false);
            try
            {

                string SQL = "SP_GET_ASSISTANCE_LOG_DATA_BYID";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("ID", Id, false));
                var dataTable = objCDataAccess.ExecuteDataTable(objDbCommand, SQL, CommandType.StoredProcedure, objList);

                if (dataTable.Rows.Count > 0)
                {
                    response = Helper.DataTableToList<NeedAssistanceLog_Xray>(dataTable).First();
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
            return response;

        }

        /// <summary>
        /// Update assistance log
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static bool UpdateNeedAssistanceLog(NeedAssistanceLog_Xray log)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "SP_UPDATE_NEED_ASSISTANCE_LOG";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("ID", log.Id, false));
            objList.Add(new DSSQLParam("DateTimeLog", log.DateTimeLog, false));
            objList.Add(new DSSQLParam("OperatorId", log.OperatorId, false));
            objList.Add(new DSSQLParam("OperatorName", log.OperatorName, false));
            objList.Add(new DSSQLParam("EnteringPersonName", log.PersonName, false));
            objList.Add(new DSSQLParam("SuggestedPriorityId", log.SuggestedPriorityId, false));
            objList.Add(new DSSQLParam("LocationId", log.LocationId, false));
            objList.Add(new DSSQLParam("AreaId", log.AreaId, false));
            objList.Add(new DSSQLParam("ModalityId", log.ModalityId, false));
            objList.Add(new DSSQLParam("DescriptionOfIssue", log.DescriptionOfIssue, false));
            if (log.ReferredTo_ResponderID != 0)
                objList.Add(new DSSQLParam("ReferredTo_ResponderID", log.ReferredTo_ResponderID, false));
            if (log.ResponseId != 0)
                objList.Add(new DSSQLParam("ResponseId", log.ResponseId, false));
            objList.Add(new DSSQLParam("Notes", log.Notes, false));
            objList.Add(new DSSQLParam("UserId", Convert.ToInt32(BUSessionUtility.BUSessionContainer.USER_ID), false));


            try
            {
                var recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }
            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }

        public static bool DeleteAssistanceLog(int Id)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;

            try
            {
                string SQL = "SP_DELETE_ASSISTANCE_LOG";
                List<DSSQLParam> objList = new List<DSSQLParam>();
                objList.Add(new DSSQLParam("Id", Id, false));
                int recAdded = 0;
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded > 0)
                {
                    success = true;

                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }


        public static bool DeleteUserLab(int UserId)
        {
            CDataAccess objCDataAccess = CDataAccess.NewCDataAccess();
            DbCommand objDbCommand = objCDataAccess.GetMyCommand(true, IsolationLevel.ReadCommitted, "application", false);
            bool success = false;
            string SQL = "USP_Delete_UserLab";
            List<DSSQLParam> objList = new List<DSSQLParam>();
            objList.Add(new DSSQLParam("UserId", UserId, false));
            int recAdded = 0;
            try
            {
                recAdded = objCDataAccess.ExecuteNonQuery(objDbCommand, SQL, CommandType.StoredProcedure, objList);
                if (recAdded >= 0)
                {
                    success = true;
                    objDbCommand.Transaction.Commit();

               
                }
                else
                {
                    success = false;

                }

            }
            catch (Exception ex)
            {
                success = false;
                objDbCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                objCDataAccess.Dispose(objDbCommand);

            }
            return success;
        }



    }
}
