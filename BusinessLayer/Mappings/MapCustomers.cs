using BusinessLayer.Models.CustomerModels;
using Library.Customer.Models;
using Library.DataModel;
using System;

namespace BusinessLayer.Mappings
{
    public class MapCustomers
    {
        public Tbl_Customers MapToLibrary(Customers_Model model)
        {
            Tbl_Customers Customer = new Tbl_Customers();
            Customer.AltEmail1 = model.AltEmail1;
            Customer.AspNetUsersID = model.AspNetUsersID;
            Customer.City = model.City;
            Customer.Company = model.Company;
            Customer.Customer = model.Customer;
            Customer.DocLink = model.DocLink;
            Customer.EIN = model.EIN;
            Customer.EnterDate = model.EnterDate;
            Customer.FEIN = model.FEIN;
            Customer.ID = model.ID;
            Customer.IndustryType = model.IndustryType;
            Customer.MainEmail = model.MainEmail;
            Customer.MainPhone = model.MainPhone;
            Customer.Mobile = model.Mobile;
            Customer.OBN = model.OBN;
            Customer.OMMALicense = model.OMMALicense;
            Customer.State = model.State;
            Customer.Street1 = model.Street1;
            Customer.Zip = model.Zip;
             
            Customer.JenBillTo = model.JenBillTo;
            Customer.JenEIN = model.JenEmail;
            Customer.JenEmail = model.JenEmail;
            Customer.JenFirst = model.JenFirst;
            Customer.JenLast = model.JenLast;
            Customer.JenPhone = model.JenPhone;

            Customer.IsActive = model.IsActive;
             
            return Customer;
        }

        public Customers_Model MapToUI(Tbl_Customers model)
        {
            Customers_Model Customer = new Customers_Model();
            Customer.AltEmail1 = model.AltEmail1 ?? string.Empty;
            Customer.AspNetUsersID = model.AspNetUsersID ?? string.Empty;
            Customer.City = model.City ?? string.Empty;
            Customer.Company = model.Company ?? string.Empty;
            Customer.Customer = model.Customer ?? string.Empty;
            Customer.DocLink = model.DocLink ?? string.Empty;
            Customer.EIN = model.EIN ?? string.Empty;
            Customer.EnterDate = model.EnterDate ?? DateTime.Now;
            Customer.FEIN = model.FEIN ?? string.Empty;
            Customer.ID = model.ID;
            Customer.IndustryType = model.IndustryType ?? string.Empty;
            Customer.MainEmail = model.MainEmail ?? string.Empty;
            Customer.MainPhone = model.MainPhone ?? string.Empty;
            Customer.Mobile = model.Mobile ?? string.Empty;
            Customer.OBN = model.OBN ?? string.Empty;
            Customer.OMMALicense = model.OMMALicense ?? string.Empty;
            Customer.State = model.State ?? string.Empty;
            Customer.Street1 = model.Street1 ?? string.Empty;
            Customer.Zip = model.Zip ?? string.Empty;

            Customer.JenBillTo = model.JenBillTo ?? string.Empty;
            Customer.JenEIN = model.JenEmail ?? string.Empty;
            Customer.JenEmail = model.JenEmail ?? string.Empty;
            Customer.JenFirst = model.JenFirst ?? string.Empty;
            Customer.JenLast = model.JenLast ?? string.Empty;
            Customer.JenPhone = model.JenPhone ?? string.Empty;

            Customer.IsActive = model.IsActive;

            return Customer;
        }

        public CustomersLite MapToLibrary(CustomersLite_Model model)
        {
            CustomersLite Customer = new CustomersLite();
            Customer.CustomerID = model.CustomerID;
            Customer.CustomerName = model.CustomerName;
            Customer.CompanyName = model.CompanyName;
            return Customer;
        }

        public CustomersLite_Model MapToUI(CustomersLite model)
        {
            CustomersLite_Model Customer = new CustomersLite_Model();
            Customer.CustomerID = model.CustomerID;
            Customer.CustomerName = model.CustomerName;
            Customer.CompanyName = model.CompanyName;
            return Customer;
        }
    }
}
