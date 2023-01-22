using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BIZ.HelperModel
{
    public class AuditTrailDataModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int UserRoleID { get; set; }
        public string UserRole { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public int ModuleID { get; set; }
        public string TableName { get; set; }
        public Exception Exception { get; set; }
    }
}
