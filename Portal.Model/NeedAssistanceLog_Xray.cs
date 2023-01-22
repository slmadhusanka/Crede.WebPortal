using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model
{
    public class NeedAssistanceLog_Xray
    {
        public int Id { get; set; }
        public DateTime DateTimeLog { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string PersonName { get; set; }
        public int SuggestedPriorityId { get; set; }
        public int LocationId { get; set; }
        public int AreaId { get; set; }
        public int ModalityId { get; set; }
        public string DescriptionOfIssue { get; set; }
        public int ReferredTo_ResponderID { get; set; }
        public int ResponseId { get; set; }
        public string Notes { get; set; }
        public int UserID_CreatedBy { get; set; }
        public int UserID_UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string Operators { get; set; }
        public string SuggestedPriority { get; set; }
        public string Location { get; set; }
        public string Area { get; set; }
        public string Modality { get; set; }
        public string ReferredTo { get; set; }
        public string Responses { get; set; }
    }
}
