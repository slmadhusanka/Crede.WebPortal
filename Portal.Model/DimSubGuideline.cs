using System;

namespace Portal.Model
{
    [Serializable()]
    public class DimSubGuideline
    {
        private Int32 _SubGuidelineCode;
        private string _Description;
        private string _Guideline;
        private string _LastChangedDate;
        private bool _IsActive;

        public Int32 SubGuidelineCode
        {
            get { return _SubGuidelineCode; }
            set { _SubGuidelineCode = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public string Guideline
        {
            get { return _Guideline; }
            set { _Guideline = value; }
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