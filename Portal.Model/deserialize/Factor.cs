using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.deserialize
{
    public class Factor
    {
        public int FactorAuditTableID { get; set; }
        public int FK_TempActivityFollowContentID { get; set; }
        public int FK_TempAuditHeader { get; set; }
        public int FK_ActivityFactorID { get; set; }
        public int id { get; set; }
        public string description { get; set; }
        public bool IsOpportunity { get; set; }
        public string time { get; set; }
        public long timestamp { get; set; }
        public DateTime TimestampDateTimeUTC { get; set; }
        public SubFactor sub_factor { get; set; }
    }
}
