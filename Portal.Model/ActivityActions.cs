using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model
{
    public class ActivityActions
    {
        public int ActivityActionItemId { get; set; }
        public string Description { get; set; }
        public int? SortOrder { get; set; }
        public DateTime LastChangedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsPPEActivity { get; set; }
        public bool IsResultActivity { get; set; }
        public bool IsOpportunity { get; set; }
        public List<ActivitySubActions> SubActions { get; set; }
        public bool HasSubItems { get; set; }
        public int Moment { get; set; }
        public int ComplianceCondition { get; set; }
    }
}
