using System;

namespace Portal.Model
{
    [Serializable()]
    public class Region
    {

        public string RegionCode
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
        public string LastChangedDate
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