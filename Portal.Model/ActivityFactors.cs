using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model
{
    public class ActivityFactors
    {
        public int FactorId { get; set; }
        public string Description { get; set; }
        public DateTime LastChangedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsOpportunity { get; set; }
        public List<ActivitySubFactors> SubFactors { get; set; }
        public bool HasSubItems { get; set; }
        public int SortOrder { get; set; }
    }
}
