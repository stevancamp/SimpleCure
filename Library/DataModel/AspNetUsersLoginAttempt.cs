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
    
    public partial class AspNetUsersLoginAttempt
    {
        public int ID { get; set; }
        public string ASPNetUserID { get; set; }
        public string IP_Address { get; set; }
        public System.DateTime LoginDatetime { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
