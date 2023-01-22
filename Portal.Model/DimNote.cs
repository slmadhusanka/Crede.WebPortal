using System;

namespace Portal.Model
{
    [Serializable()]
    public class DimNote
    {
        public Int32 NoteCode { get; set; }
        public Int32? OrderID { get; set; }
        public string Description { get; set; }
        public string Definition { get; set; }
        public string LastChangedDate { get; set; }
        public string IsActive { get; set; }
}
}
