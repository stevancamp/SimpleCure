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
    
    public partial class OrderInfo
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string OMMANumber { get; set; }
        public string EINNumber { get; set; }
        public string OBNDDNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string StreetAddress { get; set; }
        public string Notes { get; set; }
        public string BusinessType { get; set; }
        public System.DateTime OrderSubmissionDate { get; set; }
        public bool Completed { get; set; }
        public string CompletionNotes { get; set; }
    }
}