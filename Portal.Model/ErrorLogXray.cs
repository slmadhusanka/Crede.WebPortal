using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model
{
    public class ErrorLogXray
    {
        public int Id { get; set; }
        public DateTime DateOfError { get; set; }
        public string Operator { get; set; }
        public int SuggestedPriorityID { get; set; }
        public int LocationID { get; set; }
        public int ModalityID { get; set; }
        public int ErrorCategoryID { get; set; }
        public string DescriptionOfError { get; set; }
        public int PossibleConsequenceID { get; set; }
        public int RemedialActionTakenMRPID { get; set; }
        public string RemedialAction { get; set; }
        public string Notes { get; set; }
        public int UserID_CreatedBy { get; set; }
        public int UserID_UpdatedBy { get; set; }
        public string SuggestedPriority { get; set; }
        public string Location { get; set; }
        public string Modality { get; set; }
        public string ErrorCategory { get; set; }
        public string PossibleConsequence { get; set; }
        public string RemedialActionTakenMRP { get; set; }
        public string PersonEnteringItem { get; set; }
    }
}
