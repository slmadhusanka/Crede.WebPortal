using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.deserialize
{
    public class SubAction
    {
        public int SubActionTableID { get; set; }
        public int FK_TempAuditHeader { get; set; }
        public int FK_TempActivityFollowActionID { get; set; }
        public int FK_ActivitySubActionID { get; set; }
        public int id { get; set; }
        public string description { get; set; }
        public bool IsOpportunity { get; set; }
    }
}
