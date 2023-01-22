using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DAL
{
    public static class CDBConnection
    {
        #region public static GetCDBConnection
        /// <summary>
        /// Provides Connection String for Application DB 
        /// </summary>
        /// <param name="sCode">Connectstring name or Groupcode to search at Web.Config</param>
        /// <returns>Application DB Connection String</returns>
        public static string GetCDBConnection(string sCode)
        {
            string vConString;

            try
            {
                vConString = ConfigurationManager.ConnectionStrings[sCode].ConnectionString;
            }
            catch (System.Exception ex)
            {
                throw (new Exception("CDBConnection.GetCDBConnection() cannot find connectstring on:"
                                     + sCode + "---" + ex.Message + "---" + ex.StackTrace));
            }

            return vConString;
        }
        #endregion
    }
}
