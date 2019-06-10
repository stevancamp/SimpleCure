using Library.DataModel;
using Library.Email.Methods;
using Library.ErrorLogging.Methods;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;

namespace Library._Order.Methods
{
    public class _Order
    {
        #region Injection
        private EmailMessage _emailMessage;
        private ApplicationError _applicationError;

        public _Order()
        {
            _emailMessage = new EmailMessage();
            _applicationError = new ApplicationError();
        }
        #endregion

        public ResponseBase Add(Order Order)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {

                    ctx.Orders.Add(Order);
                    var Added = ctx.SaveChanges();

                    if (Added > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = Order.ID;
                        response.responseTypes = ResponseTypes.Success;
                        response.ResponseMessage = "Successfully added Order";

                    }
                    else
                    {
                        response.ResponseMessage = "Unable to add Order: " + JsonConvert.SerializeObject(Order);
                        response.responseTypes = ResponseTypes.Information;
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
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public ResponseBase Update(Order Order)
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
                        response.responseTypes = ResponseTypes.Success;
                        response.ResponseMessage = "Successfully updated Order";
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to update Order with " + JsonConvert.SerializeObject(Order);
                        response.responseTypes = ResponseTypes.Information;
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

                response.ResponseMessage = "Unable to update Order with " + JsonConvert.SerializeObject(Order);
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public ResponseBase Delete(int ID)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var Order = ctx.Orders.Where(s => s.ID == ID).FirstOrDefault();

                    if (Order != null && Order.ID > 0)
                    {
                        ctx.Orders.Remove(Order);
                        var Deleted = ctx.SaveChanges();

                        if (Deleted > 0)
                        {
                            response.ResponseSuccess = true;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully deleted Order";
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to Delete Order ID " + Order.ID;
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to find Type for Order ID " + Order.ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Order ID: {ID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Delete Order ID " + ID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<Order> GetAll()
        {
            Generic<Order> response = new Generic<Order>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.Orders.ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;

                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Orders";
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

                response.ResponseMessage = "Unable to get all Orders";
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<Order> GetAllByCompleted(bool Completed)
        {
            Generic<Order> response = new Generic<Order>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    if (Completed)
                    { response.GenericClassList = ctx.Orders.Where(s => s.CompletionDate != null).ToList(); }
                    else
                    { response.GenericClassList = ctx.Orders.Where(s => s.CompletionDate == null).ToList(); }
                   

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;

                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Orders";
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

                response.ResponseMessage = "Unable to get all Orders";
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<Order> GetByID(int ID)
        {
            Generic<Order> response = new Generic<Order>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClass = ctx.Orders.Where(s => s.ID == ID).FirstOrDefault();

                    if (response.GenericClass != null && response.GenericClass.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Order for ID " + ID;
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationError errors = new ApplicationError();
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} For Order ID: {ID} {Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get Order for ID " + ID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<Order> GetByStatus(string Status)
        {
            Generic<Order> response = new Generic<Order>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClass = ctx.Orders.Where(s => s.OrderStatus == Status).FirstOrDefault();

                    if (response.GenericClass != null && response.GenericClass.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Order for OrderStatus " + Status;
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationError errors = new ApplicationError();
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} For OrderStatus: {Status} {Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get Order for OrderStatus " + Status;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<Order> GetByCustomerID(int CustomerID)
        {
            Generic<Order> response = new Generic<Order>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClass = ctx.Orders.Where(s => s.Tbl_CustomerID == CustomerID).FirstOrDefault();

                    if (response.GenericClass != null && response.GenericClass.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Order for Tbl_Customer ID " + CustomerID;
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationError errors = new ApplicationError();
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} For Tbl_Customer ID: {CustomerID} {Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get Order for Tbl_Customer ID " + CustomerID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }
    }
}
