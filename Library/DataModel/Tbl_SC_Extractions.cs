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
    
    public partial class Tbl_SC_Extractions
    {
        public int ID { get; set; }
        public Nullable<int> SID { get; set; }
        public Nullable<int> SupplyID { get; set; }
        public Nullable<int> RunOrder { get; set; }
        public string RunType { get; set; }
        public Nullable<System.DateTime> RunDate { get; set; }
        public string Operators { get; set; }
        public Nullable<System.DateTime> RunBeginTime { get; set; }
        public Nullable<System.DateTime> RunEndTime { get; set; }
        public Nullable<int> RunMinutes { get; set; }
        public Nullable<int> GramsYield { get; set; }
        public string RunNotes { get; set; }
        public string ExtPSI { get; set; }
        public string ExtTemp { get; set; }
        public string SepPSI { get; set; }
        public string SepTemp { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<int> GramsIn { get; set; }
    }
}
