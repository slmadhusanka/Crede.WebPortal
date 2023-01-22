using System;

namespace Portal.Model
{
    [Serializable()]
    public class DimHCW
    {
        private int _HCWCode;
        private int _OrderID;
        private int _HCWCategoryCode;
        private string _HCWCategoryDesc;
        private string _Description;
        private string _LastChangedDate;
        private string _IsActive;


        public int HCWCode
        { get { return _HCWCode; } set { _HCWCode = value; } }

        public int OrderID
        { get { return _OrderID; } set { _OrderID = value; } }

        public int HCWCategoryCode
        { get { return _HCWCategoryCode; } set { _HCWCategoryCode = value; } }

        public string Description
        { get { return _Description; } set { _Description = value; } }

        public string LastChangedDate
        { get { return _LastChangedDate; } set { _LastChangedDate = value; } }

        public string HCWCategoryDesc
        { get { return _HCWCategoryDesc; } set { _HCWCategoryDesc = value; } }
        public string IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
    }
}
