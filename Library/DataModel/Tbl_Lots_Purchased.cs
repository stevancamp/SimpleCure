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
    
    public partial class Tbl_Lots_Purchased
    {
        public int ID { get; set; }
        public string Lot_Set { get; set; }
        public Nullable<System.DateTime> BuyDate { get; set; }
        public Nullable<int> Provider { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<double> Pounds { get; set; }
        public Nullable<double> Grams { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public string Strains { get; set; }
        public string Notes { get; set; }
        public string BudTrim { get; set; }
        public Nullable<int> SatPackages { get; set; }
        public Nullable<int> IndPackages { get; set; }
        public Nullable<decimal> PricePerPound { get; set; }
        public Nullable<decimal> PricePerGram { get; set; }
        public bool Complete { get; set; }
        public bool CBD { get; set; }
    }
}
