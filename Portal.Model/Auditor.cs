using System;

namespace Portal.Model
{
    [Serializable]
    public class Auditor
    {
        public string UserId
        {
            get;
            set;
        }

        public string AuditorName
        {
            get;
            set;
        }
    }
}
