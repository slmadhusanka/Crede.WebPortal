using System;

namespace Portal.Model
{
    [Serializable]
    public class DistributionList
    {
        public string DistributionListID
        {
            get;
            set;
        }
        public string DistributionListName
        {
            get;
            set;
        }
        public string Notes
        {
            get;
            set;
        }
        public string IsActive
        {
            get;
            set;
        }
    }
}