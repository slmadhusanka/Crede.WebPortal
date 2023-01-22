using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.deserialize
{
    public class AssistanceLogRowData
    {
        public AssistanceLogRowData()
        {
            Operators = new List<Operators>();
            SuggestedPriorities = new List<SuggestedPriority>();
            Locations = new List<Location>();
            Areas = new List<Area>();
            Modalities = new List<Modality>();
            ReferredTo = new List<ReferredTo>();
            Responses = new List<Responses>();
            PersonEnteringItem = new PersonEnteringItem();
        }

        public List<Operators> Operators { get; set; }
        public List<SuggestedPriority> SuggestedPriorities { get; set; }
        public PersonEnteringItem PersonEnteringItem { get; set; }
        public List<Location> Locations { get; set; }
        public List<Area> Areas { get; set; }
        public List<Modality> Modalities { get; set; }
        public List<ReferredTo> ReferredTo { get; set; }
        public List<Responses> Responses { get; set; }
    }

    public class PersonEnteringItem
    {
    }
}
