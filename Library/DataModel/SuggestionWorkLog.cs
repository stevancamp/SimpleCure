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
    
    public partial class SuggestionWorkLog
    {
        public int ID { get; set; }
        public int SuggestionID { get; set; }
        public string Comment { get; set; }
        public System.DateTime StartDateTime { get; set; }
        public System.DateTime EndDateTime { get; set; }
        public string EntryBy { get; set; }
        public System.DateTime EntryDateTime { get; set; }
    }
}
