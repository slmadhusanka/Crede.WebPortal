using System;

namespace Portal.Model
{
    [Serializable()]
    public class DimSubPPE
    {
        private Int32 _SubPPECode;
        private string _Description;
        private string _PPE;
        private string _LastChangedDate;
        private bool _IsActive;

        public Int32 SubPPECode
        {
            get { return _SubPPECode; }
            set { _SubPPECode = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public string PPE
        {
            get { return _PPE; }
            set { _PPE = value; }
        }

        public string LastChangedDate
        {
            get { return _LastChangedDate; }
            set { _LastChangedDate = value; }
        }

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
    }
}