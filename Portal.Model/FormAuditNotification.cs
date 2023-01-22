using System;

namespace Portal.Model
{
    [Serializable()]
    class FormAuditNotification
    {
        private string _ID;
        private string _Notification;
        private string _Subject;
        private string _Message;
        private string _AdditionalEmail;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Notification
        {
            get { return _Notification; }
            set { _Notification = value; }
        }
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public string AdditionalEmail
        {
            get { return _AdditionalEmail; }
            set { _AdditionalEmail = value; }
        }

    }
}