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
    
    public partial class OrderProductAndDiscount
    {
        public int id { get; set; }
        public int orderid { get; set; }
        public int productid { get; set; }
        public string batchid { get; set; }
        public int quantity { get; set; }
        public Nullable<decimal> total { get; set; }
        public string entryby { get; set; }
        public Nullable<System.DateTime> entrydate { get; set; }
        public string status { get; set; }
        public Nullable<decimal> PricePerUnit { get; set; }
    }
}