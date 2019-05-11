using Library.DataModel;
using Library.Email.Methods;
using Library.ErrorLogging.Methods;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;

namespace Library.Orders.Methods
{
    public class Order
    {
        #region Injection
        private EmailMessage _emailMessage;
        private ApplicationError _applicationError;

        public Order()
        {
            _emailMessage = new EmailMessage();
            _applicationError = new ApplicationError();
        }
        #endregion

        public ResponseBase Add(OrderInfo Order)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var Added = ctx.OrderInfoes.Add(Order);

                    if (Added.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = Added.ID;
                        //confirm if they want this or not and if so change it to a PDF attachement that is sent which means we will have to conver the Order just received into a pdf then stream it to the email message piece as a attachment
                        _emailMessage.SendMessage(ConfigurationManager.AppSettings["EmailAddress"], "New Order Created", "A new order has been created.");
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to add Order: " + JsonConvert.SerializeObject(Order);
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(Order);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to add Order: " + JsonConvert.SerializeObject(Order);
            }

            return response;
        }

        public ResponseBase Update(OrderInfo Order)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    ctx.Entry(Order).State = EntityState.Modified;
                    var updated = ctx.SaveChanges();

                    if (updated > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = Order.ID;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to update Order Info for Order: " + JsonConvert.SerializeObject(Order);
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(Order);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to update Order Info for Order: " + JsonConvert.SerializeObject(Order);
            }

            return response;
        }

        public ResponseBase Delete(int OrderID)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var Order = ctx.OrderInfoes.Where(s => s.ID == OrderID).FirstOrDefault();

                    if (Order != null && Order.ID > 0)
                    {
                        ctx.OrderInfoes.Remove(Order);
                        var Deleted = ctx.SaveChanges();

                        if (Deleted > 0)
                        {
                            response.ResponseSuccess = true;
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to Delete Order ID " + Order.ID;
                        }
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to find Order Info for Order ID " + Order.ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Order ID: {OrderID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Delete Order ID " + OrderID.ToString();
            }

            return response;
        }

        public Generic<OrderInfo> GetAll(bool IsComplete)
        {
            Generic<OrderInfo> response = new Generic<OrderInfo>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.OrderInfoes.Where(s => s.Completed == IsComplete).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Order Info";
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} IsComplete: {IsComplete.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get all Order Info";
            }

            return response;
        }

        public Generic<OrderInfo> GetByID(int ID)
        {
            Generic<OrderInfo> response = new Generic<OrderInfo>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClass = ctx.OrderInfoes.Where(s => s.ID == ID).FirstOrDefault();

                    if (response.GenericClass != null && response.GenericClass.ID > 0)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Order Info for ID " + ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Order ID: {ID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to get Order Info for ID " + ID;
            }

            return response;
        }

        public Generic<OrderInfo> GetByCompanyName(string CompanyName, bool IsComplete)
        {
            Generic<OrderInfo> response = new Generic<OrderInfo>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.OrderInfoes.Where(s => s.CompanyName.Contains(CompanyName) && s.Completed ==IsComplete).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Order Info by Company Name " + CompanyName;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Company Name: {CompanyName} {Environment.NewLine} IsComplete: {IsComplete.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to get Order Info by Company Name " + CompanyName;
            }

            return response;
        }

        //check on this date search piece
        public Generic<OrderInfo> GetByDate(DateTime OrderDate, bool IsComplete)
        {
            Generic<OrderInfo> response = new Generic<OrderInfo>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {                   
                    response.GenericClassList = ctx.OrderInfoes.Where(s => DbFunctions.TruncateTime(s.OrderSubmissionDate) == OrderDate && s.Completed == IsComplete).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Order Info by Order Date " + OrderDate;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Order Date: {OrderDate} {Environment.NewLine} IsCompleted: {IsComplete.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to get Order Info by Order Date " + OrderDate;
            }

            return response;
        }

        public Generic<OrderInfo> SearchOrderInfo(string SearchTerm, bool IsCompleted)
        {
            Generic<OrderInfo> response = new Generic<OrderInfo>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = (from s in ctx.OrderInfoes
                                                 where
                                                 s.CompanyName.ToLower().Contains(SearchTerm.ToLower()) ||
                                                 s.ContactName.ToLower().Contains(SearchTerm.ToLower()) ||
                                                 s.OMMANumber.ToLower().Contains(SearchTerm.ToLower()) ||
                                                 s.EINNumber.ToLower().Contains(SearchTerm.ToLower()) ||
                                                 s.OBNDDNumber.ToLower().Contains(SearchTerm.ToLower()) ||
                                                 s.PhoneNumber.ToLower().Contains(SearchTerm.ToLower()) ||
                                                 s.EmailAddress.ToLower().Contains(SearchTerm.ToLower()) ||
                                                 s.StreetAddress.ToLower().Contains(SearchTerm.ToLower())
                                                 && s.Completed == IsCompleted
                                                 select s).ToList();
                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to find any Orders with the search term " + SearchTerm + " where is completed is " + IsCompleted.ToString();
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Search Term: {SearchTerm} {Environment.NewLine} IsCompleted {IsCompleted.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to find any Orders with the search term " + SearchTerm + " where is complted = " + IsCompleted.ToString();
            }

            return response;
        }      
    }
}
