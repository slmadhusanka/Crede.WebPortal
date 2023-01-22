using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.deserialize
{
    public class WebApiRequest
    {
        public List<CalculatedAuditHeaderDetails> calculatedObservation { get; set; }
        public AuditHeader activityFollow { get; set; }
    }
}
