using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Utility.Model
{
    public class BINDCOMBO
    {
        public BINDCOMBO()
        {
        }
        private string _Select_Statement;
        private string _Data_Text;
        private string _Data_Value;

        public string Data_text
        {
            get { return _Data_Text; }
            set { _Data_Text = value; }
        }
        public string Data_Value
        {
            get { return _Data_Value; }
            set { _Data_Value = value; }
        }
        public string Select_Statement
        {
            get { return _Select_Statement; }
            set { _Select_Statement = value; }
        }
    }
}
