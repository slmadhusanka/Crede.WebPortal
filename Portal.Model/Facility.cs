using System;

namespace Portal.Model
{
    [Serializable()]
    public class Facility
    {
        private string _Facility_Code;
        private string _Facility_Desc;
        private string _Facility_Type;
        private string _Facility_Type_desc;
        private string _Facility_Short_desc;
        private string _Facility_Short_desc_Facility_desc;
        private string _IsActive;

        public string Facility_Short_desc_Facility_desc
        {
            get { return _Facility_Short_desc_Facility_desc; }
            set { _Facility_Short_desc_Facility_desc = value; }
        }


        public string Facility_Short_desc
        {
            get { return _Facility_Short_desc; }
            set { _Facility_Short_desc = value; }
        }

        public string Facility_Type_desc
        {
            get { return _Facility_Type_desc; }
            set { _Facility_Type_desc = value; }
        }
        public string Facility_Type
        {
            get { return _Facility_Type; }
            set { _Facility_Type = value; }
        }
        public string Facility_Code
        {
            get { return _Facility_Code; }
            set { _Facility_Code = value; }
        }
        public string Facility_Desc
        {
            get { return _Facility_Desc; }
            set { _Facility_Desc = value; }
        }
        public string RegionCode
        {
            get;
            set;
        }
        public string IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
    }
}