using BusinessLayer.Models;
using BusinessLayer.Models.CustomerModels;

namespace BusinessLayer.Interface
{
    public interface ICustomer
    {
        ResponseBase Add(Customers_Model customer);
        ResponseBase Update(Customers_Model customer);
        ResponseBase Delete(int CustomerID);
        Generic<Customers_Model> GetAll();
        Generic<Customers_Model> GetByID(int ID);
        Generic<Customers_Model> GetByUserID(int UserID);
        Generic<Customers_Model> GetByAspNetUsersID(string AspNetUsersID);
        Generic<Customers_Model> GetByIndustryType(string IndustryType);
        Generic<Customers_Model> SearchCustomers(string searchTerm);
        Generic<CustomersLite_Model> GetCustomersList();
        bool IfLoginExists(string UserID);
    }
}
