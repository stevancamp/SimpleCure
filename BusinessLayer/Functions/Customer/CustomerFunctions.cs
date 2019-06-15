using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.CustomerModels;
using Library.Customer.Methods;

namespace BusinessLayer.Functions.Customer
{
    public class CustomerFunctions : ICustomer
    {
        #region Injection
        private Customers _customers;
        private MapCustomers _mapCustomers;
        private MapResponseBase _mapResponseBase;

        public CustomerFunctions()
        {
            _customers = new Customers();
            _mapCustomers = new MapCustomers();
            _mapResponseBase = new MapResponseBase();

        }
        #endregion

        public ResponseBase Add(Customers_Model customer)
        {
            return _mapResponseBase.MapToUI(_customers.Add(_mapCustomers.MapToLibrary(customer)));
        }

        public ResponseBase Delete(int CustomerID)
        {
            return _mapResponseBase.MapToUI(_customers.Delete(CustomerID));
        }

        public Generic<Customers_Model> GetAll()
        {
            var Customers = _customers.GetAll();
            Generic<Customers_Model> model = new Generic<Customers_Model>();
            model.ResponseInt = Customers.ResponseInt;
            model.ResponseListInt = Customers.ResponseListInt;
            model.ResponseListString = Customers.ResponseListString;
            model.ResponseMessage = Customers.ResponseMessage;
            model.ResponseString = Customers.ResponseString;
            model.ResponseSuccess = Customers.ResponseSuccess;
            foreach (var item in Customers.GenericClassList)
            {
                model.GenericClassList.Add(_mapCustomers.MapToUI(item));
            }
            return model;
        }

        public Generic<Customers_Model> GetByID(int ID)
        {
            var Customers = _customers.GetByID(ID);
            Generic<Customers_Model> model = new Generic<Customers_Model>();
            model.ResponseInt = Customers.ResponseInt;
            model.ResponseListInt = Customers.ResponseListInt;
            model.ResponseListString = Customers.ResponseListString;
            model.ResponseMessage = Customers.ResponseMessage;
            model.ResponseString = Customers.ResponseString;
            model.ResponseSuccess = Customers.ResponseSuccess;
            model.GenericClass = _mapCustomers.MapToUI(Customers.GenericClass) ?? new Customers_Model();
            return model;
        }

        public Generic<Customers_Model> GetByIndustryType(string IndustryType)
        {
            var Customers = _customers.GetByIndustryType(IndustryType);
            Generic<Customers_Model> model = new Generic<Customers_Model>();
            model.ResponseInt = Customers.ResponseInt;
            model.ResponseListInt = Customers.ResponseListInt;
            model.ResponseListString = Customers.ResponseListString;
            model.ResponseMessage = Customers.ResponseMessage;
            model.ResponseString = Customers.ResponseString;
            model.ResponseSuccess = Customers.ResponseSuccess;
            foreach (var item in Customers.GenericClassList)
            {
                model.GenericClassList.Add(_mapCustomers.MapToUI(item));
            }
            return model;
        }

        public Generic<Customers_Model> GetByUserID(int UserID)
        {
            var Customers = _customers.GetByUserID(UserID);
            Generic<Customers_Model> model = new Generic<Customers_Model>();
            model.ResponseInt = Customers.ResponseInt;
            model.ResponseListInt = Customers.ResponseListInt;
            model.ResponseListString = Customers.ResponseListString;
            model.ResponseMessage = Customers.ResponseMessage;
            model.ResponseString = Customers.ResponseString;
            model.ResponseSuccess = Customers.ResponseSuccess;
            if (Customers.GenericClass != null)
            {
                model.GenericClass = _mapCustomers.MapToUI(Customers.GenericClass);
            }
            else
            {
                model.GenericClass = null;
            }          
            return model;
        }

        public Generic<Customers_Model> GetByAspNetUsersID(string AspNetUsersID)
        {
            var Customers = _customers.GetByAspNetUsersID(AspNetUsersID);
            Generic<Customers_Model> model = new Generic<Customers_Model>();
            model.ResponseInt = Customers.ResponseInt;
            model.ResponseListInt = Customers.ResponseListInt;
            model.ResponseListString = Customers.ResponseListString;
            model.ResponseMessage = Customers.ResponseMessage;
            model.ResponseString = Customers.ResponseString;
            model.ResponseSuccess = Customers.ResponseSuccess;
            if (Customers.GenericClass != null)
            {
                model.GenericClass = _mapCustomers.MapToUI(Customers.GenericClass);
            }
            else
            {
                model.GenericClass = null;
            }
            return model;
        }

        public Generic<CustomersLite_Model> GetCustomersList()
        {
            var Customers = _customers.GetCustomersList();
            Generic<CustomersLite_Model> model = new Generic<CustomersLite_Model>();
            model.ResponseInt = Customers.ResponseInt;
            model.ResponseListInt = Customers.ResponseListInt;
            model.ResponseListString = Customers.ResponseListString;
            model.ResponseMessage = Customers.ResponseMessage;
            model.ResponseString = Customers.ResponseString;
            model.ResponseSuccess = Customers.ResponseSuccess;
            foreach (var item in Customers.GenericClassList)
            {
                model.GenericClassList.Add(_mapCustomers.MapToUI(item));
            }
            return model;
        }

        public Generic<Customers_Model> SearchCustomers(string searchTerm)
        {
            var Customers = _customers.SearchCustomers(searchTerm);
            Generic<Customers_Model> model = new Generic<Customers_Model>();
            model.ResponseInt = Customers.ResponseInt;
            model.ResponseListInt = Customers.ResponseListInt;
            model.ResponseListString = Customers.ResponseListString;
            model.ResponseMessage = Customers.ResponseMessage;
            model.ResponseString = Customers.ResponseString;
            model.ResponseSuccess = Customers.ResponseSuccess;
            foreach (var item in Customers.GenericClassList)
            {
                model.GenericClassList.Add(_mapCustomers.MapToUI(item));
            }
            return model;
        }

        public ResponseBase Update(Customers_Model customer)
        {
            return _mapResponseBase.MapToUI(_customers.Update(_mapCustomers.MapToLibrary(customer)));
        }

        public bool IfLoginExists(string UserID)
        {
            return _customers.IfLoginExists(UserID);
        }
    }
}
