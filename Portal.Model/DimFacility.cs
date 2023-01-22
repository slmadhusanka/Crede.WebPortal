using System;

namespace Portal.Model
{
    [Serializable()]
    public class DimFacility
    {
        private int _FacilityCode;
        private string _Description;
        private int _FacilityTypeCode;
        private string _LastChangedDate;
        private string _FacilityTypeDesc;
        private int _RegionCode;
        private string _RegionCodeDesc;
        private string _DescriptionLong;
        private string _DescriptionShort;
        private string _IsActive;



        public int RegionCode
        {
            get { return _RegionCode; }
            set { _RegionCode = value; }
        }


        public int FacilityCode
        {
            get { return _FacilityCode; }
            set { _FacilityCode = value; }
        }

        public int FacilityTypeCode
        {
            get { return _FacilityTypeCode; }
            set { _FacilityTypeCode = value; }
        }

        public string RegionCodeDesc
        {
            get { return _RegionCodeDesc; }
            set { _RegionCodeDesc = value; }
        }


        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public string LastChangedDate
        {
            get { return _LastChangedDate; }
            set { _LastChangedDate = value; }
        }

        public string FacilityTypeDesc
        {
            get { return _FacilityTypeDesc; }
            set { _FacilityTypeDesc = value; }
        }

        public string DescriptionLong
        {
            get { return _DescriptionLong; }
            set { _DescriptionLong = value; }
        }

        public string DescriptionShort
        {
            get { return _DescriptionShort; }
            set { _DescriptionShort = value; }
        }
        public string IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
    }
}
