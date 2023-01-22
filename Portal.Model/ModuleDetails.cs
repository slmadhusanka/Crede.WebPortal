namespace Portal.Model
{
    public class ModuleDetails
    {
        private int _ModuleId;
        private string _Name;
        private string _AccessDescription;
        private string _ModuleKey;
        private int _ParentModuleId;



        public int ModuleId
        {
            get { return _ModuleId; }
            set { _ModuleId = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string AccessDescription
        {
            get { return _AccessDescription; }
            set { _AccessDescription = value; }
        }

        public string ModuleKey
        {
            get { return _ModuleKey; }
            set { _ModuleKey = value; }
        }

        public int ParentModuleId
        {
            get { return _ParentModuleId; }
            set { _ParentModuleId = value; }
        }


    }
}