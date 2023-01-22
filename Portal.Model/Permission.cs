namespace Portal.Model
{
    public class Permission
    {
        private int _PermissionId;
        private int _FKModuleId;
        private string _ModuleKey;
        private int _FKRoleCode;
        private bool _IsActive;

        public int PermissionId
        {
            get { return _PermissionId; }
            set { _PermissionId = value; }
        }

        public int FKModuleId
        {
            get { return _FKModuleId; }
            set { _FKModuleId = value; }
        }

        public string ModuleKey
        {
            get { return _ModuleKey; }
            set { _ModuleKey = value; }
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