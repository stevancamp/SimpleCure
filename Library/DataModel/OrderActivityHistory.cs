//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Library.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderActivityHistory
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int OrderActivityTypeID { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public string Notes { get; set; }
    }
}
