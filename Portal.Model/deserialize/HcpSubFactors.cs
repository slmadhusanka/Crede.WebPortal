using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.deserialize
{
    public class HcpSubFactors
    {
        public int id { get; set; }
        public int temp_id { get; set; }
        public string description { get; set; }
        public bool IsOpportunity { get; set; }
        public int index { get; set; }
        public DateTime addedDateTime { get; set; }
    }
}
