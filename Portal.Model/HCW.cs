using System;

namespace Portal.Model
{
    [Serializable()]
    public class HCW
    {
        private string _HCW_Code;
        private string _HCW_Desc;
        private string _HCW_Cat_Code;
        private string _HCW_Cat_desc;
        private string _IsActive;

        public string HCW_Cat_desc
        {
            get { return _HCW_Cat_desc; }
            set { _HCW_Cat_desc = value; }
        }
        public string HCW_Cat_Code
        {
            get { return _HCW_Cat_Code; }
            set { _HCW_Cat_Code = value; }
        }
        public string HCW_Code
        {
            get { return _HCW_Code; }
            set { _HCW_Code = value; }
        }
        public string HCW_Desc
        {
            get { return _HCW_Desc; }
            set { _HCW_Desc = value; }
        }

        public string IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
    }
}