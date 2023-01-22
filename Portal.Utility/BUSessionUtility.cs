using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Portal.Utility
{
    public class BUSessionUtility
    {
        private const string SESSION_KEY_PREFIX = "__BU__";
        public static BUSessionContainer BUSessionContainer
        {
            set
            {
                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session[SESSION_KEY_PREFIX + "BUSessionContainer"] = value;
                }
            }
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session[SESSION_KEY_PREFIX + "BUSessionContainer"] != null)
                    {
                        return (BUSessionContainer)HttpContext.Current.Session[SESSION_KEY_PREFIX + "BUSessionContainer"];
                    }
                    else
                    {
                        return new BUSessionContainer();
                    }
                }
                else
                    return new BUSessionContainer();
            }

            /*get
            {
                BUSessionContainer session =
                  (BUSessionContainer)HttpContext.Current.Session[SESSION_KEY_PREFIX + "BUSessionContainer"];
                if (session == null)
                {
                    session = new BUSessionContainer(); 
                    HttpContext.Current.Session[SESSION_KEY_PREFIX + "BUSessionContainer"] = session;
                }
                return session;
            }
            set {
                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session[SESSION_KEY_PREFIX + "BUSessionContainer"] = value;
                }
            }*/

        }
    }
}
