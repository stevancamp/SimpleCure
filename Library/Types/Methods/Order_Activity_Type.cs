using Library.DataModel;
using Library.Email.Methods;
using Library.ErrorLogging.Methods;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;

namespace Library.Types.Methods
{
    public class Order_Activity_Type
    {
        #region Injection
        private EmailMessage _emailMessage;
        private ApplicationError _applicationError;

        public Order_Activity_Type()
        {
            _emailMessage = new EmailMessage();
            _applicationError = new ApplicationError();
        }
        #endregion

        public ResponseBase Add(OrderActivityType orderActivityType)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var Exist = ctx.OrderActivityTypes.Where(s => s.Type == orderActivityType.Type).FirstOrDefault();
                    if (Exist == null)
                    {

                        ctx.OrderActivityTypes.Add(orderActivityType);
                        var Added = ctx.SaveChanges();

                        if (orderActivityType.ID > 0)
                        {
                            response.ResponseSuccess = true;
                            response.ResponseInt = orderActivityType.ID;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully added Order Activity Type";

                        }
                        else
                        {
                            response.ResponseMessage = "Unable to add Order Activity Type: " + JsonConvert.SerializeObject(orderActivityType);
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseSuccess = false;
                        response.ResponseMessage = "The Order Activity Type " + orderActivityType.Type + " already exists, please enter another.";
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(orderActivityType);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to add Order Activity Type: " + JsonConvert.SerializeObject(orderActivityType);
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public ResponseBase Update(OrderActivityType orderActivityType)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var Exist = ctx.OrderActivityTypes.Where(s => s.Type == orderActivityType.Type && s.ID != orderActivityType.ID).FirstOrDefault();
                    if (Exist == null)
                    {
                        ctx.Entry(orderActivityType).State = EntityState.Modified;
                        var updated = ctx.SaveChanges();

                        if (updated > 0)
                        {
                            response.ResponseSuccess = true;
                            response.ResponseInt = orderActivityType.ID;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully udpated Order Activity Type";
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to update Order Activity Type with " + JsonConvert.SerializeObject(orderActivityType);
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseSuccess = false;
                        response.ResponseMessage = "The Order Activity Type " + orderActivityType.Type + " already exists, please enter another.";
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(orderActivityType);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to update Order Activity Type with " + JsonConvert.SerializeObject(orderActivityType);
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
                    var orderActivityType = ctx.OrderActivityTypes.Where(s => s.ID == ID).FirstOrDefault();

                    if (orderActivityType != null && orderActivityType.ID > 0)
                    {
                        ctx.OrderActivityTypes.Remove(orderActivityType);
                        var Deleted = ctx.SaveChanges();

                        if (Deleted > 0)
                        {
                            response.ResponseSuccess = true;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully deleted Order Activity Type";
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to Delete Order Activity Type ID " + orderActivityType.ID;
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to find Type for Order Activity Type ID " + orderActivityType.ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Order Activity Type ID: {ID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Delete Order Activity Type ID " + ID.ToString();
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<OrderActivityType> GetAll(bool IsActive)
        {
            Generic<OrderActivityType> response = new Generic<OrderActivityType>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.OrderActivityTypes.Where(s => s.IsActive == IsActive).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Order Activity Types";
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} IsActive: {IsActive.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to get all Order Activity Types";
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<OrderActivityType> GetByID(int ID)
        {
            Generic<OrderActivityType> response = new Generic<OrderActivityType>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClass = ctx.OrderActivityTypes.Where(s => s.ID == ID).FirstOrDefault();

                    if (response.GenericClass != null && response.GenericClass.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Order Activity Type for ID " + ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} For Order Activity Type ID: {ID} {Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get Order Activity Type for ID " + ID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

    }
}
