using System;
using System.Collections.Generic;

namespace Portal.Model
{
    [Serializable()]
    public class DimGuideline
    {
        private Int32 _GuidlineID;
        private string _Guideline1;
        private string _Guideline2;
        private string _Guideline3;
        private string _Guideline4;
        private string _Guideline5;
        private string _lastchangedate;

        public Int32 GuidlineID
        {
            get
            {
                return _GuidlineID;
            }
            set
            {
                _GuidlineID = value;
            }
        }

        public string Guideline1
        {
            get
            {
                return _Guideline1;
            }
            set
            {
                _Guideline1 = value;
            }
        }

        public string Guideline2
        {
            get
            {
                return _Guideline2;
            }
            set
            {
                _Guideline2 = value;
            }
        }

        public string Guideline3
        {
            get
            {
                return _Guideline3;
            }
            set
            {
                _Guideline3 = value;
            }
        }

        public string Guideline4
        {
            get
            {
                return _Guideline4;
            }
            set
            {
                _Guideline4 = value;
            }
        }

        public string Guideline5
        {
            get
            {
                return _Guideline5;
            }
            set
            {
                _Guideline5 = value;
            }
        }

        public List<DimSubGuideline> SubGuidelines { get; set; }

        public string lastchangedate
        {
            get
            {
                return _lastchangedate;
            }
            set
            {
                _lastchangedate = value;
            }
        }
    }
}
