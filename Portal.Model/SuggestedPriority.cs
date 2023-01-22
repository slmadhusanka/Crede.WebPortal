using System;


namespace Portal.Model
{
    [Serializable()]
    public class SuggestedPriority
    {
        //private string _Id;
        //private string _Description;
        //private string _IsActive;
        public string ID
        {
            //get { return _Id; }
            //set { _Id = value; }
            get;
            set;
        }

        public string Description
        {
            // get { return _Description; }
            // set { _Description = value; }
            get;
            set;
        }

        public string IsActive
        {
            //  get { return _IsActive; }
            //  set { _IsActive = value; }
            get;
            set;
        }
    }

}
