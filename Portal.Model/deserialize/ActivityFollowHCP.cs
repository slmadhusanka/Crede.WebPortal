using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.deserialize
{
    public class ActivityFollowHCP
    {
        [Required]
        public int hcp_id { get; set; }
        public int temp_hcp_id { get; set; }
        public string description { get; set; }
        public int index { get; set; }
        public bool IsLocked { get; set; }
        public long timestamp { get; set; }
        public DateTime AddedDateTime { get; set; }
        public List<AuditHcpActions> hcp_actions { get; set; }
        public List<HcpFactors> hcp_factors { get; set; }
    }
}
