using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model
{
    public class ActivitySubActions
    {
        public int ActivitySubActionItemId { get; set; }
        public string Description { get; set; }
        public int ActivityActionId { get; set; }
        public int? SortOrder { get; set; }
        public DateTime LastChangedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsOpportunity { get; set; }
    }
}
