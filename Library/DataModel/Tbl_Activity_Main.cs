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
    
    public partial class Tbl_Activity_Main
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> ActDate { get; set; }
        public Nullable<int> Customer { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public string QBInvoice { get; set; }
        public bool Paid { get; set; }
        public string InvoiceNote { get; set; }
    }
}
