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
    
    public partial class OrderProduct
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string BatchID { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string EntryBy { get; set; }
        public System.DateTime EntryDate { get; set; }
        public string status { get; set; }
        public string Description { get; set; }
    }
}
