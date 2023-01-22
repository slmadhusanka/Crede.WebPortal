using System;

namespace Portal.Model
{
    [Serializable()]
    public class Comment
    {
        private int _CommentId;
        private string _CommentTxt;
        private string _ReportLink;
        private string _StartDate;
        private string _EndDate;
        private string _IsActive;

        public int CommentId
        {
            get { return _CommentId; }
            set { _CommentId = value; }
        }

        public string CommentTxt
        {
            get { return _CommentTxt; }
            set { _CommentTxt = value; }
        }

        public string ReportLink
        {
            get { return _ReportLink; }
            set { _ReportLink = value; }
        }

        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        public string IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
    }
}
