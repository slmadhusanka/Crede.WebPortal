using System;

namespace Portal.Model
{
    [Serializable()]
    public class DimResult
    {
        private Int32 _ResultCode;
        private string _Result1;
        private string _Result2;
        private string _Result3;
        private string _Result4;
        private string _LastChangedDate;

        public Int32 ResultCode
        {
            get { return _ResultCode; }
            set { _ResultCode = value; }
        }

        public string Result1
        {
            get { return _Result1; }
            set { _Result1 = value; }
        }

        public string Result2
        {
            get { return _Result2; }
            set { _Result2 = value; }
        }

        public string Result3
        {
            get { return _Result3; }
            set { _Result3 = value; }
        }

        public string Result4
        {
            get { return _Result4; }
            set { _Result4 = value; }
        }

        public string LastChangedDate
        {
            get { return _LastChangedDate; }
            set { _LastChangedDate = value; }
        }


    }
}