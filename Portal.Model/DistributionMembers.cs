using System;

namespace Portal.Model
{
    [Serializable]
    public class DistributionMembers
    {
        public string DistributionListMemberID
        {
            get;
            set;
        }
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
        public string UserID
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
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