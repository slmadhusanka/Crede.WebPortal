using System;

namespace Portal.Model
{
    [Serializable()]
    public class AllowedDevice
    {
        private string _ID;
        private string _UDID;
        private string _ServerName;
        private string _ServerURL;
        private string _ContactName;
        private string _ContactEmail;
        private string _ContactPhone;
        private string _LastChageDate;
        private string _VerificationCode;
        private string _IsActive;
        private string _IsRegistered;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string UDID
        {
            get { return _UDID; }
            set { _UDID = value; }
        }


        public string ServerName
        {
            get { return _ServerName; }
            set { _ServerName = value; }
        }


        public string ServerURL
        {
            get { return _ServerURL; }
            set { _ServerURL = value; }
        }

        public string ContactName
        {
            get { return _ContactName; }
            set { _ContactName = value; }
        }
        public string ContactEmail
        {
            get { return _ContactEmail; }
            set { _ContactEmail = value; }
        }
        public string ContactPhone
        {
            get { return _ContactPhone; }
            set { _ContactPhone = value; }
        }
        public string LastChageDate
        {
            get { return _LastChageDate; }
            set { _LastChageDate = value; }
        }

        public string VerificationCode
        {
            get { return _VerificationCode; }
            set { _VerificationCode = value; }
        }

        public string IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        public string IsRegistered
        {
            get { return _IsRegistered; }
            set { _IsRegistered = value; }
        }
    }

}
