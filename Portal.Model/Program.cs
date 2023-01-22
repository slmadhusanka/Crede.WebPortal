using System;

namespace Portal.Model
{
    [Serializable()]
    public class Program
    {
        private string _Program_Code;
        private string _Program_Desc;
        private string _Program_Type_Code;
        private string _Program_Type_Desc;
        private string _IsActive;
        private string _DescriptionShort;

        public string Program_Type_Code
        {
            get { return _Program_Type_Code; }
            set { _Program_Type_Code = value; }
        }
        public string Program_Type_Desc
        {
            get { return _Program_Type_Desc; }
            set { _Program_Type_Desc = value; }
        }
        public string Program_Code
        {
            get { return _Program_Code; }
            set { _Program_Code = value; }
        }
        public string Program_Desc
        {
            get { return _Program_Desc; }
            set { _Program_Desc = value; }
        }
        public string IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        public string DescriptionShort
        {
            get { return _DescriptionShort; }
            set { _DescriptionShort = value; }
        }
    }
}