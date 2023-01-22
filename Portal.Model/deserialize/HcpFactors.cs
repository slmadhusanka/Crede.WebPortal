using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.deserialize
{
    public class HcpFactors
    {
        [Required]
        public int id { get; set; }
        public int temp_id { get; set; }
        public int fk_temp_hcp_id { get; set; }
        public int index { get; set; }
        public string description { get; set; }
        public bool IsOpportunity { get; set; }
        public string time { get; set; }
        public long timestamp { get; set; }

        // these are coming from web api
        public bool isSelectedFromChild { get; set; }
        public string activityChildCode { get; set; }

        public HcpSubFactors hcp_factors { get; set; }
    }
}
