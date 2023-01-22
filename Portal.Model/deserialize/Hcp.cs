using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.deserialize
{
    public class Hcp
    {
        public int hcp_id { get; set; }
        public int AuditTableHcpID { get; set; }
        public int FK_TempAuditHeader { get; set; }
        public int FK_HCPID { get; set; }
        public string description { get; set; }
        public List<Action> hcp_actions { get; set; }
        public List<Factor> hcp_factors { get; set; }
    }
}
