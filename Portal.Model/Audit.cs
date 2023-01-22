using System;

namespace Portal.Model
{
    [Serializable]
    public class Audit
    {
        public string AuditId
        {
            get;
            set;
        }

        public string UserId
        {
            get;
            set;
        }

        public string FacilityCode
        {
            get;
            set;
        }

        public string UnitCode
        {
            get;
            set;
        }

        public string Facility
        {
            get;
            set;
        }

        public string Unit
        {
            get;
            set;
        }

        public string Auditor
        {
            get;
            set;
        }

        public string AuditDate
        {
            get;
            set;
        }

        public string AuditTime
        {
            get;
            set;
        }

        public DateTime AuditDateTime
        {
            get;
            set;
        }
        public DateTime UploadDateTime
        {
            get;
            set;
        }

        public int Observations
        {
            get;
            set;
        }

        public string ObservationID
        {
            get;
            set;
        }
        public string ObservationResultNumber
        {
            get;
            set;
        }

        public int HCWCode
        {
            get;
            set;
        }
        public string Moment1
        {
            get;
            set;
        }
        public string Moment2
        {
            get;
            set;
        }
        public string Moment3
        {
            get;
            set;
        }
        public string Moment4
        {
            get;
            set;
        }
        public string Moment5
        {
            get;
            set;
        }
        public string Result1
        {
            get;
            set;
        }
        public string Result2
        {
            get;
            set;
        }
        public string Result3
        {
            get;
            set;
        }
        public string Moment
        {
            get;
            set;
        }
        public string Result
        {
            get;
            set;
        }

        public string AuditTimeEnd
        {
            get;
            set;
        }

        public string RegionCode
        {
            get;
            set;
        }
        public string System
        {
            get;
            set;
        }

        public string GuideLine1 { get; set; }

        public string GuideLine2 { get; set; }

        public string GuideLine3 { get; set; }

        public string GuideLine4 { get; set; }

        public string GuideLine5 { get; set; }

        public string GuideLine { get; set; }

        public string Comment { get; set; }

        public int ID { get; set; }

        public bool EditEnable { get; set; }

        public bool DeleteEnable { get; set; }

        public string EditEnableMessage { get; set; }

        public string DeleteEnableMessage { get; set; }

        public string AuditType { get; set; }

        public string ReviewMethod { get; set; }
        public bool isDirectReview { get; set; }
        public bool IsFeedbackProvided { get; set; }
        public string AuditComment { get; set; }
    }
}
