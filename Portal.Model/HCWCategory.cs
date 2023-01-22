using System;

namespace Portal.Model
{
    [Serializable()]
    public class HCWCategory
    {
        public string HCWCategoryCode
        {
            get;
            set;
        }
        public string Description
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