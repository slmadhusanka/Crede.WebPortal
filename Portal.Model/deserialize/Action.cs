using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.deserialize
{
    public class Action
    {
        public int AuditActionTableID { get; set; }
        public int FK_TempActivityFollowContentID { get; set; }
        public int FK_TempAuditHeader { get; set; }
        public int FK_ActivityActionID { get; set; }
        public int id { get; set; }
        public string description { get; set; }
        public bool IsOpportunity { get; set; }
        public bool IsPPE { get; set; }
        public bool IsResult { get; set; }
        public string time { get; set; }
        public long timestamp { get; set; }
        public DateTime TimestampDateTimeUTC { get; set; }
        public SubAction sub_action { get; set; }
    }
}
