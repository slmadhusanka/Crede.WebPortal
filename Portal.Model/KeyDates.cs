using System;

namespace Portal.Model
{
    [Serializable]
    public class KeyDates
    {
        public string KeyDateID
        {
            get;
            set;
        }
        public string KeyDateStart
        {
            get;
            set;
        }
        public string KeyDateEnd
        {
            get;
            set;
        }
        public string DescriptionShort
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public string Reference
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