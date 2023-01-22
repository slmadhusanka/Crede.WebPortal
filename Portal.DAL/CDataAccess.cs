using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DAL
{
    public class CDataAccess
    {
        #region Constructors
        /// <summary>
        /// Constructor is private, use static method to create a new instance
        /// </summary>
        private CDataAccess()
        {
            //  
        }
        #endregion
        #region Static NewCDataAccess
        /// <summary>
        /// Create a new instance of CDataAccess
        /// </summary>
        /// <returns>new instance of CDataAccess</returns>        
        public static CDataAccess NewCDataAccess()
        {
            return new CDataAccess();
        }
        #endregion
        #region GetMyCommand
        /// <summary>
        /// provides a provider specific command object
        /// </summary>
        /// <param name="bIsTransaction">whether the command object supports transaction</param>
        /// <param name="iso">if transacion is suported the Isolation level of transaction</param>
        /// <param name="sCode">the code for get the connection string</param>
        /// <param name="isSecurityDB">whether to connect security DB</param>
        /// <returns>Command object containg the connection and transaction</returns>
        public DbCommand GetMyCommand(bool bIsTransaction, IsolationLevel iso, string sCode, bool isSecurityDB)
        {
            DbProviderFactory objDbProviderFactory;
            // retrieve provider invariant name from web.config
            string sInvariant = ConfigurationManager.AppSettings["provider-type"];
            // create the specific invariant provider
            objDbProviderFactory = DbProviderFactories.GetFactory(sInvariant);
            DbConnection objDbConnection = objDbProviderFactory.CreateConnection();
            //if (isSecurityDB)
            //    objDbConnection.ConnectionString = CDBConnection.GetCDBConnectionSecurity();
            //else
            objDbConnection.ConnectionString = CDBConnection.GetCDBConnection(sCode);
            DbCommand objDbCommand = objDbProviderFactory.CreateCommand();
            objDbCommand.Connection = objDbConnection;
            objDbConnection.Open();
            if (bIsTransaction == true)
            {
                DbTransaction objDbTransaction = objDbConnection.BeginTransaction(iso);
                objDbCommand.Transaction = objDbTransaction;
                return objDbCommand;
            }
            return objDbCommand;
        }
        #endregion
        #region ExecuteNonQuery

        /// <summary>
        /// Execute a non query at a Database Insert, delete, update or alike.
        /// </summary>
        /// <param name="objDbCommand">Command object containg the connection and transaction</param>
        /// <param name="SqlText">The SQL string as plain Text</param>
        /// <returns>The number of rows effected or -1 or -2 for concurrent error</returns>
        public int ExecuteNonQuery(DbCommand objDbCommand, string SqlText)
        {
            return (ExecuteNonQuery(objDbCommand, SqlText, CommandType.Text, null));
        }
        /// <summary>
        /// Execute a non query at a Database Insert, delete, update or alike.
        /// </summary>
        /// <param name="objDbCommand">Command object containg the connection and transaction</param>
        /// <param name="TextOrSPName">The SQL string as plain Text but the parameter
        /// should be read from parameter list. (default)</param>		
        /// <param name="lstDSSQLParam">The list of parameter</param>
        /// <returns>The number of rows effected or -1 or -2 for concurrent error</returns>
        public int ExecuteNonQuery(DbCommand objDbCommand, string SqlText, List<DSSQLParam> lstDSSQLParam)
        {
            return (ExecuteNonQuery(objDbCommand, SqlText, CommandType.Text, lstDSSQLParam));
        }
        /// <summary>
        /// Execute a non query at a Database
        /// Insert, delete, update or alike. Return the number of rows affected or -1.
        /// </summary>
        /// <param name="objDbCommand">Command object containg the connection and transaction</param>
        /// <param name="TextOrSPName">Name of Stored procedure or Plain SQL to execute</param>
        /// <param name="ComType">Type of execute - Stored procedure or sql(text) Default: (text)</param>
        /// <param name="lstDSSQLParam">The list of parameter</param>
        /// <returns>The number of rows effected or -1 or -2 for concurrent error
        /// -3 for referential integrity error</returns>
        public int ExecuteNonQuery(DbCommand objDbCommand, string TextOrSPName, CommandType ComType, List<DSSQLParam> lstDSSQLParam)
        {
            //if (Leadsoft_DebugHelper.IsSqlLogOn == true)
            //    this.CDataAccessEventlogInfo("ExecuteNonQuery", TextOrSPName, lstDSSQLParam);	
            //// in this case no concurrency check
            objDbCommand.CommandType = ComType;
            objDbCommand.CommandText = TextOrSPName;
            objDbCommand.Parameters.Clear();
            string sInvariant = ConfigurationManager.AppSettings["provider-type"];
            if (lstDSSQLParam != null)
            {
                for (int iIndex = 0; iIndex < lstDSSQLParam.Count; iIndex++)
                {

                    if (sInvariant == "System.Data.SqlClient")
                    {
                        DbParameter objDbParameter = new SqlParameter();
                        objDbParameter.ParameterName = CParameter.GetParameterName(lstDSSQLParam, iIndex);
                        objDbParameter.Value = CParameter.GetParameterValue(lstDSSQLParam, iIndex);
                        objDbParameter.Direction = CParameter.GetParameterDirection(lstDSSQLParam, iIndex);
                        objDbCommand.Parameters.Add(objDbParameter);
                    }
                    // Here section have to add for Oracle database


                }
            }
            try
            {
                int x = objDbCommand.ExecuteNonQuery();
                return x;
            }
            catch (SqlException ex)
            {
                return 0;
                ///Original exception is replaced here. So do necessary code here
                ///for loging the original exceltion. But now only replaced took
                ///place here with a formatted message.
                throw new SystemException(ex.Message);
            }

        }
        #endregion

        #region Helper Functions
        /// <summary>

        /// Purpose : This function will Dispose all the resourses that is allocated. This function should be
        ///           called after every calling of ExecuteNonquery(), Execute......
        /// </summary>
        /// <param name="objDbCommand"></param>
        /// <param name="objDbConnection"></param>
        /// <param name="objDbTransaction"></param>
        public void Dispose(DbCommand objDbCommand)
        {
            if (objDbCommand.Connection != null)
            {
                ///It will Close the connection autometically and perform additional releasing tasks.
                objDbCommand.Connection.Dispose();
                objDbCommand.Connection = null;
            }
            if (objDbCommand.Transaction != null)
            {
                objDbCommand.Transaction.Dispose();
                objDbCommand.Transaction = null;
            }
            if (objDbCommand != null)
            {
                objDbCommand.Dispose();
                objDbCommand = null;
            }
        }

        #endregion

        #region ExecuteReader
        public DbDataReader ExecuteReader(DbCommand objDbCommand, string SqlText)
        {
            return (ExecuteReader(objDbCommand, SqlText, CommandType.Text, null, CommandBehavior.CloseConnection));
        }

        public DbDataReader ExecuteReader(DbCommand objDbCommand, string TextOrSPName, CommandType ComType, List<DSSQLParam> lstDSSQLParam)
        {
            return (ExecuteReader(objDbCommand, TextOrSPName, ComType, lstDSSQLParam, CommandBehavior.CloseConnection));
        }

        /// <summary>
        /// Execute a reader at a Database using either sql or stored procedure
        /// </summary>
        /// <param name="objDbCommand">Command object containg the connection and transaction</param>
        /// <param name="TextOrSPName">Text (sql) or Name of Stored procedure to execute</param>
        /// <param name="ComType">Type of execute - Stored procedure or sql(text) Default: (text)</param>
        /// <param name="lstDSSQLParam">The list of parameter</param>
        /// <param name="combeh">The commandbehaviour for the reader</param>
        /// <returns>DataReader containing the result set</returns> 
        public DbDataReader ExecuteReader(DbCommand objDbCommand, string TextOrSPName, CommandType ComType, List<DSSQLParam> lstDSSQLParam, CommandBehavior combeh)
        {

            objDbCommand.CommandType = ComType;
            objDbCommand.CommandText = TextOrSPName;
            objDbCommand.Parameters.Clear();

            string sInvariant = ConfigurationManager.AppSettings["provider-type"];
            if (lstDSSQLParam != null)
            {
                for (int iIndex = 0; iIndex < lstDSSQLParam.Count; iIndex++)
                {

                    if (sInvariant == "System.Data.SqlClient")
                    {
                        DbParameter objDbParameter = new SqlParameter();
                        objDbParameter.ParameterName = CParameter.GetParameterName(lstDSSQLParam, iIndex);
                        objDbParameter.Value = CParameter.GetParameterValue(lstDSSQLParam, iIndex);
                        objDbParameter.Direction = CParameter.GetParameterDirection(lstDSSQLParam, iIndex);
                        objDbCommand.Parameters.Add(objDbParameter);
                    }
                    // this section will have to added for Oracle database

                }
            }
            DbDataReader dr;
            try
            {


                if (sInvariant == "System.Data.SqlClient")
                {


                    dr = objDbCommand.ExecuteReader(combeh);

                }
                else
                {
                    // this section will have to implented for Oracle or other database
                    dr = objDbCommand.ExecuteReader(combeh);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// Gets a scalar value
        /// </summary>
        /// <param name="objDbCommand">Command object containg the connection and transaction</param>
        /// <param name="SqlText">Plain SQL to execute (default)</param>
        /// <returns>an object containg the result</returns>
        public Object ExecuteScalar(DbCommand objDbCommand, string SqlText)
        {
            return (ExecuteScalar(objDbCommand, SqlText, CommandType.Text, null));
        }
        /// <summary>
        /// Gets a scalar value
        /// </summary>
        /// <param name="objDbCommand">Command object containg the connection and transaction</param>
        /// <param name="SqlText">Plain SQL to execute (default) but parameter should be from 
        /// parameter List</param>
        /// <param name="lstDSSQLParam">The List of Parameter</param>
        /// <returns>an object containg the result</returns>
        public Object ExecuteScalar(DbCommand objDbCommand, string SqlText, List<DSSQLParam> lstDSSQLParam)
        {
            return (ExecuteScalar(objDbCommand, SqlText, CommandType.Text, lstDSSQLParam));
        }
        /// <summary>
        /// Gets a scalar value
        /// </summary>
        /// <param name="objDbCommand">Command object containg the connection and transaction</param>
        /// <param name="TextOrSPName">Text (sql) or Name of Function to execute</param>
        /// <param name="ComType">Type of execute - Stored procedure or sql(text) Default: (text)</param>
        /// <param name="lstDSSQLParam">The list of parameter</param>
        /// <returns>an object containg the result</returns>
        public Object ExecuteScalar(DbCommand objDbCommand, string TextOrSPName, CommandType ComType, List<DSSQLParam> lstDSSQLParam)
        {

            objDbCommand.CommandType = ComType;
            objDbCommand.CommandText = TextOrSPName;
            objDbCommand.Parameters.Clear();
            if (lstDSSQLParam != null)
            {
                for (int iIndex = 0; iIndex < lstDSSQLParam.Count; iIndex++)
                {
                    string sInvariant = ConfigurationManager.AppSettings["provider-type"];
                    if (sInvariant == "System.Data.SqlClient")
                    {
                        DbParameter objDbParameter = new SqlParameter();
                        objDbParameter.ParameterName = CParameter.GetParameterName(lstDSSQLParam, iIndex);
                        objDbParameter.Value = CParameter.GetParameterValue(lstDSSQLParam, iIndex);
                        objDbParameter.Direction = CParameter.GetParameterDirection(lstDSSQLParam, iIndex);
                        objDbCommand.Parameters.Add(objDbParameter);
                    }
                }
            }
            return objDbCommand.ExecuteScalar();
        }
        #endregion

        public DataSet ExecuteDataSet(DbCommand objDbCommand, string SqlText)
        {
            return (ExecuteDataSet(objDbCommand, SqlText, CommandType.Text, null));
        }

        public DataSet ExecuteDataSet(DbCommand objDbCommand, string TextOrSPName, CommandType ComType, List<DSSQLParam> lstDSSQLParam)
        {
            DataSet dsData = new DataSet();
            SqlDataAdapter sqlDa;

            objDbCommand.CommandType = ComType;
            objDbCommand.CommandText = TextOrSPName;
            objDbCommand.Parameters.Clear();
            if (lstDSSQLParam != null)
            {
                for (int iIndex = 0; iIndex < lstDSSQLParam.Count; iIndex++)
                {
                    string sInvariant = ConfigurationManager.AppSettings["provider-type"];
                    if (sInvariant == "System.Data.SqlClient")
                    {
                        DbParameter objDbParameter = new SqlParameter();
                        objDbParameter.ParameterName = CParameter.GetParameterName(lstDSSQLParam, iIndex);
                        objDbParameter.Value = CParameter.GetParameterValue(lstDSSQLParam, iIndex);
                        objDbParameter.Direction = CParameter.GetParameterDirection(lstDSSQLParam, iIndex);
                        objDbCommand.Parameters.Add(objDbParameter);
                    }
                }
            }
            sqlDa = new SqlDataAdapter(((SqlCommand)objDbCommand));
            sqlDa.Fill(dsData);

            return dsData;
        }
        public DataTable ExecuteDataTable(DbCommand objDbCommand, string TextOrSPName, CommandType ComType, List<DSSQLParam> lstDSSQLParam)
        {
            DataTable table = new DataTable();
            SqlDataAdapter sqlDa;

            objDbCommand.CommandType = ComType;
            objDbCommand.CommandText = TextOrSPName;
            objDbCommand.Parameters.Clear();
            if (lstDSSQLParam != null)
            {
                for (int iIndex = 0; iIndex < lstDSSQLParam.Count; iIndex++)
                {
                    string sInvariant = ConfigurationManager.AppSettings["provider-type"];
                    if (sInvariant == "System.Data.SqlClient")
                    {
                        DbParameter objDbParameter = new SqlParameter();
                        objDbParameter.ParameterName = CParameter.GetParameterName(lstDSSQLParam, iIndex);
                        objDbParameter.Value = CParameter.GetParameterValue(lstDSSQLParam, iIndex);
                        objDbParameter.Direction = CParameter.GetParameterDirection(lstDSSQLParam, iIndex);
                        objDbCommand.Parameters.Add(objDbParameter);
                    }
                }
            }
            sqlDa = new SqlDataAdapter(((SqlCommand)objDbCommand));
            sqlDa.Fill(table);

            return table;
        }
    }
}
