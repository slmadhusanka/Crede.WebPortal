using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Portal.DAL
{
    public static class CParameter
    {
        #region Methods
        /// <summary>
        /// Return the parameter Name 
        /// <param name="lstDSSQLParam">The List that holds parameter info</param>
        /// <param name="iIndex">Index of the parameter</param>
        /// <returns>The parameter name</returns>
        /// </summary>
        internal static string GetParameterName(List<DSSQLParam> lstDSSQLParam, int iIndex)
        {
            string sInvariant = ConfigurationManager.AppSettings["provider-type"];

            switch (sInvariant)
            {
                case "System.Data.SqlClient":   //If SQL server @ should be concateneted 	
                    return "@" + lstDSSQLParam[iIndex].sParamName;
                case "Oracle.DataAccess.Client":
                    return lstDSSQLParam[iIndex].sParamName;
                default:
                    return "@" + lstDSSQLParam[iIndex].sParamName;
            }
        }
        /// <summary>
        /// Return the parameter Name
        /// </summary>
        /// <param name="sParameterName">The parameter name</param>
        /// <returns>parameter name with prefix if necessary</returns>
        public static string GetParameterName(string sParameterName)
        {
            string sInvariant = ConfigurationManager.AppSettings["provider-type"];

            switch (sInvariant)
            {
                case "System.Data.SqlClient":   //If SQL server @ should be concateneted 	
                    return "@" + sParameterName;
                case "Oracle.DataAccess.Client":
                    return sParameterName;
                default:
                    return "@" + sParameterName;
            }
        }

        /// <summary>
        /// Return the parameter Value
        /// </summary>
        /// <param name="lstDSSQLParam">The List that holds parameter info</param>
        /// <param name="iIndex">Index of the parameter value</param>
        /// <returns>The parameter value</returns>
        internal static object GetParameterValue(List<DSSQLParam> lstDSSQLParam, int iIndex)
        {

            return (object)lstDSSQLParam[iIndex].objParamValue;
        }

        /// <summary>

        /// Purpose     : Return the parameter DBType
        /// </summary>
        /// <param name="lstDSSQLParam">The List that holds parameter info</param>
        /// <param name="iIndex">Index of the parameter value</param>
        /// <returns>The parameter value</returns>
        internal static DbType GetParameterDBType(List<DSSQLParam> lstDSSQLParam, int iIndex)
        {
            return (DbType)lstDSSQLParam[iIndex].dbParamDBType;
        }

        /// <summary>
        /// Get the direction of the parameter
        /// </summary>
        /// <param name="lstDSSQLParam">The List that holds parameter info</param>
        /// <param name="iIndex">Index of the parameter value</param>
        /// <returns>Direction of parameter as ParameterDirection </returns>
        internal static ParameterDirection GetParameterDirection(List<DSSQLParam> lstDSSQLParam, int iIndex)
        {
            return (ParameterDirection)lstDSSQLParam[iIndex].paramDirection;

        }

        /// <summary>

        /// Purpose     :Get the direction of the parameter for function.
        /// </summary>
        /// <param name="lstDSSQLParam">The List that holds parameter info</param>
        /// <param name="iIndex">Index of the parameter value</param>
        /// <returns>Direction of parameter as ParameterDirection </returns>
        internal static ParameterDirection GetParameterDirectionForFunction(List<DSSQLParam> lstDSSQLParam, int iIndex)
        {
            bool bDirection = lstDSSQLParam[iIndex].bIsOutputParam;
            if (bDirection)
                return ParameterDirection.ReturnValue;
            return ParameterDirection.Input;
        }

        /// <summary>
        /// Return the output parameter Name
        /// </summary>        
        /// <param name="sParamName">Name of output parameter</param>
        /// <returns>The output parameter name specific to provider</returns>
        public static string GetOutputParameterName(string sParamName)
        {
            string sInvariant = ConfigurationManager.AppSettings["provider-type"];

            switch (sInvariant)
            {
                case "System.Data.SqlClient":   //If SQL server @ should be concateneted 	
                    return "@" + sParamName;
                case "Oracle.DataAccess.Client":
                    return sParamName;
                default:
                    return "@" + sParamName;
            }
        }
        #endregion
    }
}
