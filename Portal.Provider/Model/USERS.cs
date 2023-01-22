using System;

namespace Portal.Provider.Model
{
    [Serializable()]
    public class USERS
    {
        private string _User_ID;
        private string _FirstName;
        private string _LastName;
        private string _Password;
        private string _CreationDate;
        private string _Mobile;
        private string _Phone;
        private string _Email;
        private string _FacilityCode;
        private string _FacilityDescription;
        private string _RegionCode;
        private string _Region;
        

        private string _ProgramCode;
        private string _ProgramDescription;

        private string _RoleCode;
        private string _RoleDescription;
        private string _Active_flag;
        private string _Reset_password;
        private string _LastLoginDate;
        private string _LastPasswordChangedDate;
        private string _Force_Password_Changed_Flag;

        private string _UnitCode;
        private string _IsLockedOut;
        private string _Unit;

        private string _LastChangedDate;
        private string _DisplayName;
        private string _IsAuditor;
        private string _IsActive;

        private string _PhoneNumber;
        private string _UserName;

        public string ProgramCode
        {
            get { return _ProgramCode; }
            set { _ProgramCode = value; }
        }
        public string ProgramDescription
        {
            get { return _ProgramDescription; }
            set { _ProgramDescription = value; }
        }
        public string Force_Password_Changed_Flag
        {
            get { return _Force_Password_Changed_Flag; }
            set { _Force_Password_Changed_Flag = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public string LastLoginDate
        {
            get { return _LastLoginDate; }
            set { _LastLoginDate = value; }
        }
        public string LastPasswordChangedDate
        {
            get { return _LastPasswordChangedDate; }
            set { _LastPasswordChangedDate = value; }
        }
        public string CreationDate
        {
            get { return _CreationDate; }
            set { _CreationDate = value; }
        }
        public string RoleCode
        {
            get { return _RoleCode; }
            set { _RoleCode = value; }
        }
        public string RoleDescription
        {
            get { return _RoleDescription; }
            set { _RoleDescription = value; }
        }
        public string RegionCode
        {
            get { return _RegionCode; }
            set { _RegionCode = value; }
        }
        public string Region
        {
            get { return _Region; }
            set { _Region = value; }
        }
        public string FacilityCode
        {
            get { return _FacilityCode; }
            set { _FacilityCode = value; }
        }
        public string FacilityDescription
        {
            get { return _FacilityDescription; }
            set { _FacilityDescription = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string Reset_password
        {
            get { return _Reset_password; }
            set { _Reset_password = value; }
        }
        public string Active_flag
        {
            get { return _Active_flag; }
            set { _Active_flag = value; }
        }
        public string User_ID
        {
            get { return _User_ID; }
            set { _User_ID = value; }
        }
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public string IsLockedOut
        {
            get { return _IsLockedOut; }
            set { _IsLockedOut = value; }
        }
        public string UnitCode
        {
            get { return _UnitCode; }
            set { _UnitCode = value; }
        }
        public string Unit
        {
            get { return _Unit; }
            set { _Unit = value; }
        }
        public string LastChangedDate
        {
            get { return _LastChangedDate; }
            set { _LastChangedDate = value; }
        }

        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; }
        }
        public string IsAuditor
        {
            get { return _IsAuditor; }
            set { _IsAuditor = value; }

        }
        public string IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }

        }
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }

        }
        public string Occupation { get; set; }
        public bool TwoWayAuthActivied { get; set; }

        public string Lab { get; set; }

    }
}
