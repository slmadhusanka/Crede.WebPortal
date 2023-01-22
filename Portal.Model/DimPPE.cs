using System;
using System.Collections.Generic;

namespace Portal.Model
{
    [Serializable()]
    public class DimPPE
    {
        private Int32 _PPECode;
        private string _PPE1;
        private string _PPE2;
        private string _PPE3;
        private string _PPE4;
        private string _PPE5;
        private string _Precautions1;
        private string _Precautions2;
        private string _Precautions3;
        private string _Precautions4;
        private string _lastchangedate;
        private string _EQP1;
        private string _EQP2;
        private string _EQP3;
        private string _EQP4;
        private string _EQP5;

        public Int32 PPECode
        {
            get
            {
                return _PPECode;
            }
            set
            {
                _PPECode = value;
            }
        }

        public string PPE1
        {
            get
            {
                return _PPE1;
            }
            set
            {
                _PPE1 = value;
            }
        }

        public string PPE2
        {
            get
            {
                return _PPE2;
            }
            set
            {
                _PPE2 = value;
            }
        }

        public string PPE3
        {
            get
            {
                return _PPE3;
            }
            set
            {
                _PPE3 = value;
            }
        }

        public string PPE4
        {
            get
            {
                return _PPE4;
            }
            set
            {
                _PPE4 = value;
            }
        }

        public string PPE5
        {
            get
            {
                return _PPE5;
            }
            set
            {
                _PPE5 = value;
            }
        }
        public string Precautions1
        {
            get
            {
                return _Precautions1;
            }
            set
            {
                _Precautions1 = value;
            }
        }

        public string Precautions2
        {
            get
            {
                return _Precautions2;
            }
            set
            {
                _Precautions2 = value;
            }
        }

        public string Precautions3
        {
            get
            {
                return _Precautions3;
            }
            set
            {
                _Precautions3 = value;
            }
        }

        public string Precautions4
        {
            get
            {
                return _Precautions4;
            }
            set
            {
                _Precautions4 = value;
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
        public string EQP1
        {
            get
            {
                return _EQP1;
            }
            set
            {
                _EQP1 = value;
            }
        }

        public string EQP2
        {
            get
            {
                return _EQP2;
            }
            set
            {
                _EQP2 = value;
            }
        }

        public string EQP3
        {
            get
            {
                return _EQP3;
            }
            set
            {
                _EQP3 = value;
            }
        }

        public string EQP4
        {
            get
            {
                return _EQP4;
            }
            set
            {
                _EQP4 = value;
            }
        }

        public string EQP5
        {
            get
            {
                return _EQP5;
            }
            set
            {
                _EQP5 = value;
            }
        }
        
        public List<DimSubPPE> SubPpes { get; set; }
    }
}
