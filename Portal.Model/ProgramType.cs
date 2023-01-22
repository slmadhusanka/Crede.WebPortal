using System;

namespace Portal.Model
{
    [Serializable()]
    public class ProgramType
    {
        public string ProgramTypeCode
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