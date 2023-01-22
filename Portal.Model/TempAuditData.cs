using System.Collections.Generic;

namespace Portal.Model
{
    public class TempAuditData
    {
        public TempAuditHeaderData header { get; set; }
        public List<TempHCPData> body { get; set; }
    }
}