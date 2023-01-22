using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BIZ.HelperModel
{
    public class LogAction
    {
        private LogAction(string value) { Value = value; }

        public string Value { get; private set; }

        public static LogAction Add { get { return new LogAction("ADD"); } }
        public static LogAction Edit { get { return new LogAction("EDIT"); } }
        public static LogAction Delete { get { return new LogAction("DELETE"); } }
        public static LogAction Assign { get { return new LogAction("ASSIGN"); } }
        public static LogAction UnAssign { get { return new LogAction("UNASSIGNED"); } }
        public static LogAction EmailSend { get { return new LogAction("EMAIL_SEND"); } }
        public static LogAction Error { get { return new LogAction("Error"); } }
    }
}
