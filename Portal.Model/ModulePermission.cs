namespace Portal.Model
{
    public class ModulePermission
    {
        private int _ModuleId;
        private string _Name;
        private string _AccessDescription;
        private string _ModuleKey;
        private int _ParentModuleId;

        private int _PermissionId;
        private int _FKRoleCode;
        private bool _IsActive;


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

        public int PermissionId
        {
            get { return _PermissionId; }
            set { _PermissionId = value; }
        }

        public int FKRoleCode
        {
            get { return _FKRoleCode; }
            set { _FKRoleCode = value; }
        }

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

    }
}