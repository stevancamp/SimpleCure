using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleCure.Models.AdminModels
{
    public class CreateNewCustomerAccount_ViewModel : ResponseBase
    {
        [Required]
        public string Company { get; set; }
        [Required]
        public string Customer { get; set; }
        [Required]
        [Phone]
        public string MainPhone { get; set; }
        [Required]
        [Phone]
        public string Mobile { get; set; }
        [Required]
        [EmailAddress]
        public string MainEmail { get; set; }
        [Required]
        [EmailAddress]
        public string AltEmail1 { get; set; }
        [Required]
        public string Street1 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public string IndustryType { get; set; }
        [Required]
        public string OMMALicense { get; set; }
        [Required]
        public string EIN { get; set; }
        [Required]
        public string FEIN { get; set; }
        [Required]
        public string OBN { get; set; }

        public List<string> IndustryTypes { get; set; }
    }
}