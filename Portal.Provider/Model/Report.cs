using System;

namespace Portal.Provider.Model
{
    [Serializable()]
    public class Report
    {
        private int _ReportID;
        private string _ReportCode;
        private string _ReportName;
        private string _ReportDescription;
        private string _ReportServerLocation;
        private string _ReportCategory;
        private string _ReportSubCategory;
        private bool _IsActive;
        private int _SortOrder;
        private bool _IsSendEnable;
        private string _ReportKey;
        private string _Active;

        //private bool _IsPublicRoleVisible;
        //private bool _IsAuthenticatedRoleVisible;
        //private bool _IsAdminRoleVisible;
        //private bool _IsManagerRoleVisible;

        public int ReportID
        {
            get { return _ReportID; }
            set { _ReportID = value; }
        }

        public string ReportCode
        {
            get { return _ReportCode; }
            set { _ReportCode = value; }
        }

        public string ReportName
        {
            get { return _ReportName; }
            set { _ReportName = value; }
        }

        public string ReportDescription
        {
            get { return _ReportDescription; }
            set { _ReportDescription = value; }
        }

        public string ReportServerLocation
        {
            get { return _ReportServerLocation; }
            set { _ReportServerLocation = value; }
        }

        public string ReportCategory
        {
            get { return _ReportCategory; }
            set { _ReportCategory = value; }
        }

        public string ReportSubCategory
        {
            get { return _ReportSubCategory; }
            set { _ReportSubCategory = value; }
        }

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        public int SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }

        public bool IsSendEnable
        {
            get { return _IsSendEnable; }
            set { _IsSendEnable = value; }
        }

        public string ReportKey
        {
            get { return _ReportKey; }
            set { _ReportKey = value; }
        }

        public string Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        //public bool IsPublicRoleVisible
        //{
        //    get { return _IsPublicRoleVisible; }
        //    set { _IsPublicRoleVisible = value; }
        //}

        //public bool IsAuthenticatedRoleVisible
        //{
        //    get { return _IsAuthenticatedRoleVisible; }
        //    set { _IsAuthenticatedRoleVisible = value; }
        //}

        //public bool IsAdminRoleVisible
        //{
        //    get { return _IsAdminRoleVisible; }
        //    set { _IsAdminRoleVisible = value; }
        //}

        //public bool IsManagerRoleVisible
        //{
        //    get { return _IsManagerRoleVisible; }
        //    set { _IsManagerRoleVisible = value; }
        //}      
    }
}
