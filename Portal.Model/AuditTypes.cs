using System;

namespace Portal.Model
{
    [Serializable()]
    public class AuditTypes
    {
        private Int32 _AuditTypeID;
        private string _AuditType;
        private Int32 _SortOrder;
        private bool _IsActive;
        private string _Active;

        private string _lastchangedate;

        public Int32 AuditTypeID
        {
            get
            {
                return _AuditTypeID;
            }
            set
            {
                _AuditTypeID = value;
            }
        }

        public string AuditType
        {
            get
            {
                return _AuditType;
            }
            set
            {
                _AuditType = value;
            }
        }

        public Int32 SortOrder
        {
            get
            {
                return _SortOrder;
            }
            set
            {
                _SortOrder = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                _IsActive = value;
            }
        }

        public string Active
        {
            get
            {
                return _Active;
            }
            set
            {
                _Active = value;
            }
        }
    }
}