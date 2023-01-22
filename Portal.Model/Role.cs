using System;

namespace Portal.Model
{
    [Serializable()]
    public class Role
    {
        private int _RoleCode;
        private string _Description;
        private bool _IsActive;
        private string _Active;

        public int RoleCode
        {
            get { return _RoleCode; }
            set { _RoleCode = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        public string Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

    }
}