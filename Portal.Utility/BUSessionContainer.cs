using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Utility
{
    public class BUSessionContainer
    {
        public BUSessionContainer() { }

        #region Private Variables
        private string _ROLES_FOR_USER;
        private string _ROLE;
        private string _USER_ID;
        private string _FirstName;
        private string _LastName;
        private string _UserName;
        private string _FORCE_PASSWORD_CHANGED_FLAG;
        private string _IS_ADMIN_FLAG;
        private string _FUNCTION_ID;
        private string _LAST_SUCCESSFULL_LOGIN_DT;
        private object _OBJ_CLASS;
        private object _OBJ_CLASS1;
        private object _REPORT_DOCUMENT;
        private object _MENU;
        private object _DO_NULL;
        private string _LAST_LOGIN_DT;
        private string _LAST_LOGIN_TM;
        private string _REDIRECT_PATH;
        private string _Email;
        private string _LastPasswordChangedDate;
        private string _CreationDate;
        private bool _TwoWayAuthActivied;
        private string _PhoneNumber;
        private bool _IsTrustedBrowser;
        private string _CompanyName;
        private string _SessionTimeOut;
        #endregion

        #region Properties
        public string ROLES_FOR_USER
        {
            get { return _ROLES_FOR_USER; }
            set
            {
                _ROLES_FOR_USER = value;

                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public string ROLE
        {
            get { return _ROLE; }
            set
            {
                _ROLE = value;

                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public string USER_ID
        {
            get { return _USER_ID; }
            set
            {
                _USER_ID = value;
                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public string IS_ADMIN_FLAG
        {
            get { return _IS_ADMIN_FLAG; }
            set
            {
                _IS_ADMIN_FLAG = value;
                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public string FORCE_PASSWORD_CHANGED_FLAG
        {
            get { return _FORCE_PASSWORD_CHANGED_FLAG; }
            set
            {
                _FORCE_PASSWORD_CHANGED_FLAG = value;
                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public string FUNCTION_ID
        {
            get { return _FUNCTION_ID; }
            set
            {
                _FUNCTION_ID = value;

                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public object OBJ_CLASS
        {
            get { return _OBJ_CLASS; }
            set
            {
                _OBJ_CLASS = value;
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public object OBJ_CLASS1
        {
            get { return _OBJ_CLASS1; }
            set
            {
                _OBJ_CLASS1 = value;
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public object REPORT_DOCUMENT
        {
            get { return _REPORT_DOCUMENT; }
            set
            {
                _REPORT_DOCUMENT = value;
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public object MENU
        {
            get { return _MENU; }
            set
            {
                _MENU = value;
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public object DO_NULL
        {
            get { return _DO_NULL; }
            set
            {
                ///From any where when this variable is set then it will reset
                ///all session values.

                _ROLES_FOR_USER = string.Empty;
                _USER_ID = string.Empty;
                _FirstName = string.Empty;
                _LastName = string.Empty;
                _FORCE_PASSWORD_CHANGED_FLAG = string.Empty;
                _IS_ADMIN_FLAG = string.Empty;
                _FUNCTION_ID = string.Empty;
                _LAST_SUCCESSFULL_LOGIN_DT = string.Empty;
                _OBJ_CLASS = null;
                _OBJ_CLASS1 = null;
                _REPORT_DOCUMENT = null;
                _MENU = null;
                _LAST_LOGIN_DT = string.Empty;
                _LAST_LOGIN_TM = string.Empty;
                _REDIRECT_PATH = string.Empty;
                _Email = string.Empty;
                _LastPasswordChangedDate = string.Empty;
                _CreationDate = string.Empty;
                _DO_NULL = null;
                _PhoneNumber = string.Empty;
                _CompanyName = null;
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public string LAST_LOGIN_TM
        {
            get { return _LAST_LOGIN_TM; }
            set
            {
                _LAST_LOGIN_TM = value;

                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public string LAST_LOGIN_DT
        {
            get { return _LAST_LOGIN_DT; }
            set
            {
                _LAST_LOGIN_DT = value;

                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public string REDIRECT_PATH
        {
            get { return _REDIRECT_PATH; }
            set
            {
                _REDIRECT_PATH = value;

                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public string LastPasswordChangedDate
        {
            get { return _LastPasswordChangedDate; }
            set
            {
                _LastPasswordChangedDate = value;

                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public string CreationDate
        {
            get { return _CreationDate; }
            set
            {
                _CreationDate = value;

                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;

                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public bool TwoWayAuthActivied
        {
            get { return _TwoWayAuthActivied; }
            set
            {
                _TwoWayAuthActivied = value;

                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set
            {
                _PhoneNumber = value;

                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }

        public string CompanyName
        {
            get { return _CompanyName; }
            set
            {
                _CompanyName = value;

                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }

        public bool IsTrustedBrowser
        {
            get { return _IsTrustedBrowser; }
            set
            {
                _IsTrustedBrowser = value;

                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }
        public string SessionTimeOut
        {
            get { return _SessionTimeOut; }
            set
            {
                _SessionTimeOut = value;

                // call the set of SessionContainer Properties in SessionUtils Class
                BUSessionUtility.BUSessionContainer = this;
            }
        }


        #endregion
    }
}
