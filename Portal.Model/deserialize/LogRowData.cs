using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.deserialize
{
    public class LogRowData
    {
        public LogRowData()
        {
            SuggestedPriorities = new List<SuggestedPriority>();
            Locations = new List<Location>();
            Modalities = new List<Modality>();
            ErrorCategories = new List<ErrorCategory>();
            PossibleConsequences = new List<PossibleConsequence>();
            RemedialActionTakenMRPs = new List<RemedialActionTakenMRP>();
        }
        public List<SuggestedPriority> SuggestedPriorities { get; set; }
        public List<Location> Locations { get; set; }
        public List<Modality> Modalities { get; set; }
        public List<ErrorCategory> ErrorCategories { get; set; }
        public List<PossibleConsequence> PossibleConsequences { get; set; }
        public List<RemedialActionTakenMRP> RemedialActionTakenMRPs { get; set; }
    }
}
