using System;

namespace Portal.Model
{
    [Serializable()]
    public class SystemConfiguration
    {
        public int ConfigurationID { get; set; }
        public string LastChangedDate { get; set; }
        public int AuditDuration { get; set; }
        public string AdditionalTime { get; set; }
        public int MinHCWObservation { get; set; }
        public int MinObservationPerHCW { get; set; }
        public int MaxObservationPerHCW { get; set; }
        public bool EnableResultTimer { get; set; }
        public string ResultTimerDuration { get; set; }
        public int MinObservation { get; set; }
        public int? AuditLimit { get; set; }
        public bool IsAuditLimitActive { get; set; }
        public bool EnablePPE { get; set; }
        public bool EnablePrecautions { get; set; }
    }
}