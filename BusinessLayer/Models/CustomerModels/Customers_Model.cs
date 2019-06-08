using System;

namespace BusinessLayer.Models.CustomerModels
{
    public class Customers_Model
    {
        public int ID { get; set; }
        public string Company { get; set; }
        public string Customer { get; set; }
        public string MainPhone { get; set; }
        public string Mobile { get; set; }
        public string MainEmail { get; set; }
        public string AltEmail1 { get; set; }
        public string Street1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string IndustryType { get; set; }
        public string OMMALicense { get; set; }
        public string EIN { get; set; }
        public string DocLink { get; set; }
        public DateTime? EnterDate { get; set; }
        public string FEIN { get; set; }
        public string OBN { get; set; }
        public string AspNetUsersID { get; set; }
        public string JenBillTo { get; set; }
        public string JenEmail { get; set; }
        public string JenFirst { get; set; }
        public string JenLast { get; set; }
        public string JenEIN { get; set; }
        public string JenPhone { get; set; }
    }
}
