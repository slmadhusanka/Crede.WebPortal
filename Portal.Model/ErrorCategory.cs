using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Model.BaseEntity;

namespace Portal.Model
{
    [Serializable()]
    public class ErrorCategory 
    {
        public string ID
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
       
        public string IsActive
        {
            get;
            set;
        }
    }
}
