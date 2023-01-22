using System;

namespace Portal.Model
{
    public class ReprocessingLog
    {
        public string ReprocessingLogID { get; set; }
        public DateTime Date { get; set; }
        public string Transducer { get; set; }
        public string VisitNumber { get; set; }
        public DateTime TimeHLDInitiated { get; set; }
        public DateTime TimeHLDCompleted { get; set; }
    }
}