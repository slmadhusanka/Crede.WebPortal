using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model
{
    [Serializable()]
    public class ActivityFollowConfigurations
    {

        public int ActivityFollowConfigurationID { get; set; }
        public string LastChangedDate { get; set; }
        public int AuditDuration { get; set; }
        public int AdditionalTime { get; set; }
        public int MinHCWObservation { get; set; }
        public bool EnableResultTimer { get; set; }
        public int ResultTimerDuration { get; set; }
       
    }
}
