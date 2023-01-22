using System;

namespace Portal.Model
{
    [Serializable()]
    public class SendMail
    {
        private string _MailId;
        private string _DistributionListID;
        private string _Subject;
        private string _Message;
        private string _LastSendDate;
        private string _DistributionListName;

        public string MailId
        {
            get { return _MailId; }
            set { _MailId = value; }
        }


        public string DistributionListID
        {
            get { return _DistributionListID; }
            set { _DistributionListID = value; }
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
        public string LastSendDate
        {
            get { return _LastSendDate; }
            set { _LastSendDate = value; }
        }
        public string DistributionListName
        {
            get { return _DistributionListName; }
            set { _DistributionListName = value; }
        }


    }
}