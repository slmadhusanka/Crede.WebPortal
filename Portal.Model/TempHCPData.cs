using System;
using System.Collections.Generic;

namespace Portal.Model
{
    public class TempHCPData
    {
        public Int32 TempHCPId
        { get; set; }

        public Int32 Id
        { get; set; }

        public string HCP
        { get; set; }

        public Int32 HCPNo
        { get; set; }

        public string HCPCode
        { get; set; }

        public Int32 TempAuditHeaderId
        { get; set; }

        public Int32 UserId
        { get; set; }

        public List<TempObsData> lstTempObsData
        { get; set; }

        public bool IsHH { get; set; }

        public bool IsPPE { get; set; }
    }
}