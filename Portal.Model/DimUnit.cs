using System;

namespace Portal.Model
{
    [Serializable()]
    public class DimUnit
    {
        private int _UnitCode;
        private int _OrderID;
        private int _FacilityCode;
        private int _ProgramCode;
        private int _UnitType1Code;
        private int _UnitType2Code;
        private string _Description;
        private string _DescriptionShort;
        private string _DescriptionLong;
        private string _LastChangedDate;

        private string _FacilityCodeDesc;
        private string _ProgramCodeDesc;
        private string _UnitType1CodeDesc;
        private string _UnitType2CodeDesc;
        private string _Dim_Unit_Desc_long;

        private int _ManagerID;
        private string _ManagerName;
        private string _IsActive;
        private string _Beds;
        //private int _UserId;

        public int UnitCode
        { get { return _UnitCode; } set { _UnitCode = value; } }

        public int OrderID
        { get { return _OrderID; } set { _OrderID = value; } }

        public int FacilityCode
        { get { return _FacilityCode; } set { _FacilityCode = value; } }

        public int ProgramCode
        { get { return _ProgramCode; } set { _ProgramCode = value; } }

        public int UnitType1Code
        { get { return _UnitType1Code; } set { _UnitType1Code = value; } }

        public int UnitType2Code
        { get { return _UnitType2Code; } set { _UnitType2Code = value; } }


        public string Description
        { get { return _Description; } set { _Description = value; } }

        public string DescriptionShort
        { get { return _DescriptionShort; } set { _DescriptionShort = value; } }

        public string DescriptionLong
        { get { return _DescriptionLong; } set { _DescriptionLong = value; } }

        public string LastChangedDate
        { get { return _LastChangedDate; } set { _LastChangedDate = value; } }


        public string FacilityCodeDesc
        { get { return _FacilityCodeDesc; } set { _FacilityCodeDesc = value; } }

        public string ProgramCodeDesc
        { get { return _ProgramCodeDesc; } set { _ProgramCodeDesc = value; } }


        public string UnitType1CodeDesc
        { get { return _UnitType1CodeDesc; } set { _UnitType1CodeDesc = value; } }

        public string UnitType2CodeDesc
        { get { return _UnitType2CodeDesc; } set { _UnitType2CodeDesc = value; } }


        public string Dim_Unit_Desc_long
        { get { return _Dim_Unit_Desc_long; } set { _Dim_Unit_Desc_long = value; } }

        public int ManagerID
        { get { return _ManagerID; } set { _ManagerID = value; } }

        //public string UserName
        //{ get { return _UserName; } set { _UserName = value; } }

        public string IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        //public int UserID
        //{ get { return _UserId; } set { _UserId = value; } }

        public string ManagerName
        { get { return _ManagerName; } set { _ManagerName = value; } }

        public string Beds
        { get { return _Beds; } set { _Beds = value; } }
    }
}
