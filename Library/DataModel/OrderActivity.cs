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
    
    public partial class OrderActivity
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public System.DateTime ActivityDate { get; set; }
        public string ActivityBy { get; set; }
    }
}
