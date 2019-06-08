using Library.Customer.Models;
using Library.DataModel;
using Library.Email.Methods;
using Library.ErrorLogging.Methods;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Library.Customer.Methods
{
    public class Customers
    {
        #region Injection
        private EmailMessage _emailMessage;
        private ApplicationError _applicationError;

        public Customers()
        {
            _emailMessage = new EmailMessage();
            _applicationError = new ApplicationError();
        }
        #endregion     
        public ResponseBase Add(Tbl_Customers customer)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    ctx.Tbl_Customers.Add(customer);
                    var Added = ctx.SaveChanges();

                    if (Added > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = customer.ID;
                        response.responseTypes = ResponseTypes.Success;
                        response.ResponseMessage = "Successfully added new Customer";                            
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to add customer: " + JsonConvert.SerializeObject(customer);
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(customer);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to add customer: " + JsonConvert.SerializeObject(customer);
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }
        public ResponseBase Update(Tbl_Customers customer)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    //ctx.Tbl_Customers.                                                                    
                    ctx.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                    //ctx.Tbl_Customers.Add(customer);
                    var updated = ctx.SaveChanges();

                    if (updated > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = customer.ID;
                        response.responseTypes = ResponseTypes.Success;
                        response.ResponseMessage = "Successfully updated Customer";
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to update Customer Info for Customer: " + JsonConvert.SerializeObject(customer);
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(customer);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to update Customer Info for Customer: " + JsonConvert.SerializeObject(customer);
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }      
        public ResponseBase Delete(int CustomerID)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var Customer = ctx.Tbl_Customers.Where(s => s.ID == CustomerID).FirstOrDefault();

                    if (Customer != null && Customer.ID > 0)
                    {
                        ctx.Tbl_Customers.Remove(Customer);
                        var Deleted = ctx.SaveChanges();

                        if (Deleted > 0)
                        {
                            response.ResponseSuccess = true;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Customer " + CustomerID + " has been successfully deleted";
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to Delete Customer ID " + CustomerID;
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to find Customer Info for Customer ID " + CustomerID;
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Customer ID: {CustomerID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Delete Customer ID " + CustomerID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }    
        public Generic<Tbl_Customers> GetAll()
        {
            Generic<Tbl_Customers> response = new Generic<Tbl_Customers>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.Tbl_Customers.ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Customer Info";
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine}";
                _applicationError.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get all Customer Info";
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }     
        public Generic<Tbl_Customers> GetByID(int ID)
        {
            Generic<Tbl_Customers> response = new Generic<Tbl_Customers>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClass = ctx.Tbl_Customers.Where(s => s.ID == ID).FirstOrDefault();

                    if (response.GenericClass != null && response.GenericClass.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Customer Info for ID " + ID;
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Customer ID: {ID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to get Customer Info for ID " + ID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }
        public Generic<Tbl_Customers> GetByUserID(string UserID)
        {
            Generic<Tbl_Customers> response = new Generic<Tbl_Customers>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClass = ctx.Tbl_Customers.Where(s => s.AspNetUsersID == UserID).FirstOrDefault();

                    if (response.GenericClass != null && response.GenericClass.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Customer Info for User ID " + UserID;
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} User ID: {UserID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to get Customer Info for User ID " + UserID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }
        public Generic<Tbl_Customers> GetByIndustryType(string IndustryType)
        {
            Generic<Tbl_Customers> response = new Generic<Tbl_Customers>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.Tbl_Customers.Where(s => s.IndustryType == IndustryType).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Customer Info for IndustryType " + IndustryType;
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Industry Type: {IndustryType}";
                _applicationError.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get all Customer Info for IndustryType " + IndustryType;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }      
        public Generic<Tbl_Customers> SearchCustomers(string searchTerm)
        {
            Generic<Tbl_Customers> response = new Generic<Tbl_Customers>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    //response.GenericClassList = ctx.Tbl_Customers.Where(s => s.IndustryType == IndustryType).ToList();
                    response.GenericClassList = (from s in ctx.Tbl_Customers
                                                 where
                                                 s.Company.ToLower().Contains(searchTerm.ToLower()) ||
                                                 s.Customer.ToLower().Contains(searchTerm.ToLower()) ||
                                                 s.MainEmail.ToLower().Contains(searchTerm.ToLower()) ||
                                                 s.Street1.ToLower().Contains(searchTerm.ToLower())
                                                 select s
                                                 ).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Customer Info for search term " + searchTerm;
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Search Term: {searchTerm}";
                _applicationError.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get all Customer Info for search term " + searchTerm;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }
        public Generic<CustomersLite> GetCustomersList()
        {
            Generic<CustomersLite> response = new Generic<CustomersLite>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var Customers = (from s in ctx.Tbl_Customers
                                     select new
                                     {
                                         s.ID,
                                         s.Customer
                                     }).ToList();
                    if (Customers != null && Customers.Count > 0)
                    {
                        foreach (var item in Customers)
                        {
                            CustomersLite customer = new CustomersLite();
                            customer.CustomerID = item.ID;
                            customer.CustomerName = item.Customer;

                            response.GenericClassList.Add(customer);                               
                        }
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Customer Lite Info";
                        response.responseTypes = ResponseTypes.Information;
                    }

                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to get all Customer Lite Info";
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public bool IfLoginExists(string UserID)
        {
            bool Exists = false;

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var CustomerInfo = ctx.Tbl_Customers.Where(s => s.AspNetUsersID == UserID).FirstOrDefault();

                    if (CustomerInfo != null && CustomerInfo.ID > 0)
                    {
                        Exists = true;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} User ID: {UserID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

               
            }

            return Exists;
        }
    }
}
