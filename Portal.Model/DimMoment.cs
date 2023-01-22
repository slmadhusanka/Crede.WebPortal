using System;

namespace Portal.Model
{
    [Serializable()]
    public class DimMoment
    {
        private Int32 _MomentID;
        private string _moment1;
        private string _moment2;
        private string _moment3;
        private string _moment4;
        private string _moment5;
        private string _lastchangedate;

        public Int32 MomentID
        {
            get
            {
                return _MomentID;
            }
            set
            {
                _MomentID = value;
            }
        }

        public string moment1
        {
            get
            {
                return _moment1;
            }
            set
            {
                _moment1 = value;
            }
        }

        public string moment2
        {
            get
            {
                return _moment2;
            }
            set
            {
                _moment2 = value;
            }
        }

        public string moment3
        {
            get
            {
                return _moment3;
            }
            set
            {
                _moment3 = value;
            }
        }

        public string moment4
        {
            get
            {
                return _moment4;
            }
            set
            {
                _moment4 = value;
            }
        }

        public string moment5
        {
            get
            {
                return _moment5;
            }
            set
            {
                _moment5 = value;
            }
        }

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
