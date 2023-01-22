using System;

namespace Portal.Model
{
    [Serializable()]
    public class UnitType1
    {
        public string UnitType1Code
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
        public string IsActive
        {
            get;
            set;
        }
    }
}