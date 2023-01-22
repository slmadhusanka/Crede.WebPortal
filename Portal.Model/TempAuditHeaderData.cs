using System;

namespace Portal.Model
{
    public class TempAuditHeaderData
    {
        public Int32 TempAuditHeaderId
        { get; set; }

        public Int32 RegionId
        { get; set; }

        public Int32 FacilityId
        { get; set; }

        public Int32 UnitId
        { get; set; }

        public Int32 AuditorId
        { get; set; }

        public string AuditId
        { get; set; }

        public Int32 UserId
        { get; set; }

        public string AuditDateTime
        { get; set; }

        public Int32 AuditType
        { get; set; }

        public bool IsHH { get; set; }

        public bool IsPPE { get; set; }

        public bool isDirectReview { get; set; }
        public string AuditComment { get; set; }
        public bool IsFeedbackProvided { get; set; }
        public int AuditDuration { get; set; }
    }
}