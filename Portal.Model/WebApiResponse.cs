using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model
{
    public class WebApiResponse
    {
        public int AuditID { get; set; }
        public bool Success { get; set; }
        public object ErrorMessage { get; set; }
        public object ErrorTrace { get; set; }
    }
}
