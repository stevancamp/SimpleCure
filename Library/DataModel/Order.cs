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
    
    public partial class Order
    {
        public int ID { get; set; }
        public int Tbl_CustomerID { get; set; }
        public string Notes { get; set; }
        public System.DateTime SubmissionDate { get; set; }
        public string OrderStatus { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public string TransportID { get; set; }
        public bool IsSimpleCure { get; set; }
        public string TransportLocationStart { get; set; }
        public string TransportLocationEnd { get; set; }
        public string To_From { get; set; }
    }
}
