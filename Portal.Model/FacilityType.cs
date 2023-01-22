using System;

namespace Portal.Model
{
    [Serializable()]
    public class FacilityType
    {
        public string FacilityTypeCode
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public string DescriptionShort
        {
            get;
            set;
        }
        public string DescriptionLong
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