using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Portal.BIZ
{
    public class secPrincipal : IPrincipal
    {
        secIdentity _IDENTITY;
        string[] _ROLES;

        #region IPrincipal
        /// <summary>
        /// Implements the Identity property defined by IPrincipal.
        /// </summary>
        IIdentity IPrincipal.Identity
        {
            get
            {
                return _IDENTITY;
            }
        }
        /// <summary>
        /// Implements the IsInRole property defined by IPrincipal.
        /// </summary>
        bool IPrincipal.IsInRole(string role)
        {
            return _IDENTITY.IsInRole(role);
        }
        #endregion

        #region Login process
        public static void PortalPrincipal(secIdentity psecIdentity, string[] pROLES)
        {
            new secPrincipal(psecIdentity, pROLES);
        }
        private secPrincipal(secIdentity psecIdentity, string[] pROLES)
        {
            this._IDENTITY = psecIdentity;
            this._ROLES = pROLES;

            AppDomain currentdomain = Thread.GetDomain();
            currentdomain.SetPrincipalPolicy(PrincipalPolicy.UnauthenticatedPrincipal);
            //GenericPrincipal principal = new GenericPrincipal(psecIdentity, pROLES);
            IPrincipal oldPrincipal = Thread.CurrentPrincipal;
            HttpContext.Current.User = this;
            try
            {
                if (!(oldPrincipal.GetType() == typeof(secPrincipal)))
                {
                    try
                    {
                        currentdomain.SetThreadPrincipal(this);
                    }
                    catch (PolicyException ex)
                    {

                    }
                }
            }
            catch
            {
                // failed, but we don't care because there's nothing
                // we can do in this case
            }
        }
        #endregion
    }
}
