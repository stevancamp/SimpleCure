using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace SimpleCure.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        //model for ASpnetUserInformation information
        public ApplicationUser UserInfo { get; set; }
        //model for Customer Info
        public CustomerInfoViewModel CustomerInfo { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }

    public class ChangeEmailAddressViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ChangeUserNameViewModel
    {
        [Required]        
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class DeleteAccountViewModel
    {
        public string ErrorMessage { get; set; }
    }

    public class CustomerInfoViewModel
    {
        public int ID { get; set; }
        public string Company { get; set; }
        public string Customer { get; set; }
        [Phone]
        public string MainPhone { get; set; }
        [Phone]
        public string Mobile { get; set; }
        [EmailAddress]
        public string MainEmail { get; set; }
        [EmailAddress]
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
        public string JenBillTo { get; set; }
        public string JenEmail { get; set; }
        public string JenFirst { get; set; }
        public string JenLast { get; set; }
        public string JenEIN { get; set; }
        public string JenPhone { get; set; }
        public string AspNetUsersID { get; set; }
        public string ErrorMessage { get; set; }
    }
}