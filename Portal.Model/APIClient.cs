using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model
{
    public class APIClient
    {
        public string ClientId { get; set; }
        public string Name { get; set; }
        public string Scope { get; set; }
        public string ClientSecret { get; set; }
        public string IsActive { get; set; }
        public string CreatedAt { get; set; }
    }
}
