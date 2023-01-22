using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.BaseEntity
{
    /// <summary>
    /// This will be holding basic  elements of table
    /// </summary>
    public class BaseEntityClass
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
