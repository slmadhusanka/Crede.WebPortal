using System;

namespace Portal.Model
{
    [Serializable()]
    public class Unit
    {
        private string _Unit_ID;
        private string _Unit_Name;
        private string _Facility_Code;
        private string _Facility_Desc;
        private string _Program_Code;
        private string _Program_Desc;
        private string _UserID;
        private string _DescriptionLong;
        private string _Unit_Name_DescriptionLong;
        private string _IsActive;


        public string Unit_Name_DescriptionLong
        {
            get { return _Unit_Name_DescriptionLong; }
            set { _Unit_Name_DescriptionLong = value; }

        }

        public string DescriptionLong
        {
            get { return _DescriptionLong; }
            set { _DescriptionLong = value; }

        }
        public string Unit_ID
        {
            get { return _Unit_ID; }
            set { _Unit_ID = value; }
        }
        public string Unit_Name
        {
            get { return _Unit_Name; }
            set { _Unit_Name = value; }
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
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string RegionDescription
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
