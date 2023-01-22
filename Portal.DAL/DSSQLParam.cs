using System;
using System.Data;
using System.Data.OracleClient;

namespace Portal.DAL
{
    [Serializable()]
    public struct DSSQLParam : IEquatable<DSSQLParam>
    {
        #region Constructors for Any DB Type
        /// <summary>
        /// Constructor for Any DB Type
        /// </summary>
        /// <param name="sParamName">Parameter Name</param>
        /// <param name="objParamValue">Parameter Value</param>		
        public DSSQLParam(string sParamName, object objParamValue)
        {
            this._sParamName = sParamName;
            this._objParamValue = objParamValue;
            this._bIsOutputParam = false;
            this._bDateTime = false;
            this._dbParamDBType = DbType.String;
            this._dbParamOracleDBType = OracleType.VarChar;
            this._dbParamSqlDBType = SqlDbType.VarChar;
            this._paramDirection = ParameterDirection.Input;
        }

        public DSSQLParam(string sParamName, object objParamValue, bool bIsOutputParam)
        {
            this._sParamName = sParamName;
            this._objParamValue = objParamValue;
            this._bIsOutputParam = bIsOutputParam;
            this._bDateTime = false;
            this._dbParamDBType = DbType.String;
            this._dbParamOracleDBType = OracleType.VarChar;
            this._dbParamSqlDBType = SqlDbType.VarChar;
            if (bIsOutputParam)
                this._paramDirection = ParameterDirection.Output;
            else
                this._paramDirection = ParameterDirection.Input;
        }
        public DSSQLParam(string sParamName, object objParamValue, ParameterDirection paramDirection)
        {
            this._sParamName = sParamName;
            this._objParamValue = objParamValue;
            this._bIsOutputParam = false;
            this._bDateTime = false;
            this._dbParamDBType = DbType.String;
            this._dbParamOracleDBType = OracleType.VarChar;
            this._dbParamSqlDBType = SqlDbType.VarChar;
            this._paramDirection = paramDirection;
        }

        public DSSQLParam(string sParamName, object objParamValue, bool bIsOutputParam, DbType dbParamDBType)
        {
            this._sParamName = sParamName;
            this._objParamValue = objParamValue;
            this._bIsOutputParam = bIsOutputParam;
            this._dbParamDBType = dbParamDBType;
            this._dbParamOracleDBType = OracleType.VarChar;
            this._dbParamSqlDBType = SqlDbType.VarChar;
            this._bDateTime = false;
            if (bIsOutputParam)
                this._paramDirection = ParameterDirection.Output;
            else
                this._paramDirection = ParameterDirection.Input;
        }
        public DSSQLParam(string sParamName, object objParamValue, ParameterDirection paramDirection, DbType dbParamDBType)
        {
            this._sParamName = sParamName;
            this._objParamValue = objParamValue;
            this._bIsOutputParam = false;
            this._dbParamDBType = dbParamDBType;
            this._dbParamOracleDBType = OracleType.VarChar;
            this._dbParamSqlDBType = SqlDbType.VarChar;
            this._bDateTime = false;
            this._paramDirection = paramDirection;
        }
        #endregion
        #region Constructors for Oracle DB Type 
        /// <param name="sParamName">Parameter Name</param>
        /// <param name="objParamValue">Parameter Value</param>	
        /// 
        public DSSQLParam(string sParamName, object objParamValue, bool bIsOutputParam, OracleType dbParamDBType)
        {
            this._sParamName = sParamName;
            this._objParamValue = objParamValue;
            this._bIsOutputParam = bIsOutputParam;
            this._dbParamDBType = DbType.String;
            this._dbParamOracleDBType = dbParamDBType;
            this._dbParamSqlDBType = SqlDbType.VarChar;
            this._bDateTime = false;
            if (bIsOutputParam)
                this._paramDirection = ParameterDirection.Output;
            else
                this._paramDirection = ParameterDirection.Input;
        }
        #endregion
        #region Constructors for SQL DB Type 
        /// <param name="sParamName">Parameter Name</param>
        /// <param name="objParamValue">Parameter Value</param>	
        /// 
        public DSSQLParam(string sParamName, object objParamValue, bool bIsOutputParam, SqlDbType dbParamDBType)
        {
            this._sParamName = sParamName;
            this._objParamValue = objParamValue;
            this._bIsOutputParam = bIsOutputParam;
            this._dbParamDBType = DbType.String;
            this._dbParamOracleDBType = OracleType.VarChar;
            this._dbParamSqlDBType = dbParamDBType;
            this._bDateTime = false;
            if (bIsOutputParam)
                this._paramDirection = ParameterDirection.Output;
            else
                this._paramDirection = ParameterDirection.Input;
        }

        #endregion
        #region Member Variables
        private string _sParamName;
        private object _objParamValue;
        private bool _bIsOutputParam;
        private bool _bDateTime;
        private DbType _dbParamDBType;
        private OracleType _dbParamOracleDBType;
        private SqlDbType _dbParamSqlDBType;
        private ParameterDirection _paramDirection;
        #endregion
        #region Properties
        /// <summary>
        /// Name of SP parameter
        /// <remarks>Do not use @ sign here if you are using SQL server as DB, but
        /// your SP parameters should be like @ParamName
        /// </remarks>
        /// </summary>        
        public string sParamName
        {
            get
            {
                return _sParamName;
            }
            set
            {
                _sParamName = value;
            }
        }
        /// <summary>
        /// Value of parameter
        /// </summary>
        public object objParamValue
        {
            get
            {
                return _objParamValue;
            }
            set
            {
                _objParamValue = value;
            }
        }
        /// <summary>
        /// Whether this parameter is an output parameter 
        /// true::output parameter and false::input parameter
        /// </summary>
        public bool bIsOutputParam
        {
            get
            {
                return _bIsOutputParam;
            }
            set
            {
                _bIsOutputParam = value;
            }
        }
        public bool bDateTime
        {
            get
            {
                return _bDateTime;
            }
            set
            {
                _bDateTime = value;
            }
        }
        public DbType dbParamDBType
        {
            get
            {
                return _dbParamDBType;
            }
            set
            {
                _dbParamDBType = value;
            }
        }
        public OracleType dbParamOracleDBType
        {
            get
            {
                return _dbParamOracleDBType;
            }
            set
            {
                _dbParamOracleDBType = value;
            }
        }
        public SqlDbType dbParamSqlDBType
        {
            get
            {
                return _dbParamSqlDBType;
            }
            set
            {
                _dbParamSqlDBType = value;
            }
        }

        public ParameterDirection paramDirection
        {
            get
            {
                return _paramDirection;
            }
            set
            {
                _paramDirection = value;
            }
        }
        #endregion

        #region need for performance
        /// <summary>

        /// Purpose:To improve performance(Override equals and operator equals on value types ) 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is DSSQLParam)) return false;

            return Equals((DSSQLParam)obj);
        }

        public bool Equals(DSSQLParam other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(DSSQLParam point1, DSSQLParam point2) { return point1.Equals(point2); }

        public static bool operator !=(DSSQLParam point1, DSSQLParam point2) { return !point1.Equals(point2); }

        #endregion need for performance
    }
}
