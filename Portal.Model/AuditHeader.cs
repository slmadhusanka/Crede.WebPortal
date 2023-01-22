using System;

namespace Portal.Model
{
    [Serializable]
    public class AuditHeader
    {
        public string AuditId { get; set; }
        public int Observations { get; set; }
        public DateTime AuditDateTime { get; set; }
        public int RegionCode { get; set; }
        public int FacilityCode { get; set; }
        public int UnitCode { get; set; }
        public int UserId { get; set; }
        public bool isDirectReview { get; set; }
        public string AuditComment { get; set; }
        public bool IsFeedbackProvided { get; set; }
        public int AuditDuration { get; set; }
    }
}
