using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Portal.BIZ
{
    public class secIdentity : IIdentity
    {
        private string _USER_NM = string.Empty;
        private ArrayList _ROLES = new ArrayList();
        private FormsAuthenticationTicket _FTK;

        #region Constructor
        private secIdentity(FormsAuthenticationTicket pFTK)// prevent direct creation
        {
            this._FTK = pFTK;
            this._USER_NM = pFTK.Name;
        }
        #endregion
        internal bool IsInRole(string pROLE)
        {
            return _ROLES.Contains(pROLE);
        }
        #region IIdentity

        /// <summary>
        /// Implements the IsAuthenticated property defined by IIdentity.
        /// </summary>
        bool IIdentity.IsAuthenticated
        {
            get
            {
                return (_USER_NM.Length > 0);
            }
        }

        /// <summary>
        /// Implements the AuthenticationType property defined by IIdentity.
        /// </summary>
        string IIdentity.AuthenticationType
        {
            get
            {
                return "PORTAL";
            }
        }
        /// <summary>
        /// Implements the Name property defined by IIdentity.
        /// </summary>
        string IIdentity.Name
        {
            get
            {
                return _USER_NM;
            }
        }
        #endregion
        public static secIdentity PortalIdentity(FormsAuthenticationTicket pFTK)
        {
            return new secIdentity(pFTK);
        }
    }
}
