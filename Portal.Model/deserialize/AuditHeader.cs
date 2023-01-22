using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.deserialize
{
    public class AuditHeader
    {
        [Required]
        public int auditTypeCode { get; set; }
        [Required]
        public long timestamp { get; set; }
        public DateTime auditDateTime { get; set; }
        [Required]
        public string tempAuditID { get; set; }
        [Required]
        public bool feedbackProvided { get; set; }
        [Required]
        public int regionCode { get; set; }
        [Required]
        public int facilityCode { get; set; }

        public string auditComment { get; set; }
        [Required]
        public int unitCode { get; set; }
        [Required]
        public string deviceID { get; set; }
        [Required]
        public int observerCode { get; set; }
        [Required]
        public int auditDuration { get; set; }
        [Required]
        public int userID { get; set; }

        [Required]
        public List<ActivityFollowHCP> hcpData { get; set; }

    }
}
