using Library.DataModel;
using Library.Email.Methods;
using Library.ErrorLogging.Methods;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;

namespace Library.Types.Methods
{
    public class OrderInfo_Product_Group
    {
        #region Injection
        private EmailMessage _emailMessage;
        private ApplicationError _applicationError;

        public OrderInfo_Product_Group()
        {
            _emailMessage = new EmailMessage();
            _applicationError = new ApplicationError();
        }
        #endregion

        public ResponseBase Add(OrderInfo_Product_Groups orderInfo_Product_Groups)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var Exist = ctx.OrderInfo_Product_Groups.Where(s => s.GroupName == orderInfo_Product_Groups.GroupName).FirstOrDefault();
                    if (Exist == null)
                    {
                        ctx.OrderInfo_Product_Groups.Add(orderInfo_Product_Groups);
                        var Added = ctx.SaveChanges();

                        if (Added > 0)
                        {
                            response.ResponseSuccess = true;
                            response.ResponseInt = orderInfo_Product_Groups.ID;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully added Order Product Group Type";

                        }
                        else
                        {
                            response.ResponseMessage = "Unable to add Order Product Group Type: " + JsonConvert.SerializeObject(orderInfo_Product_Groups);
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseSuccess = false;
                        response.ResponseMessage = "The Order Product Group Type " + orderInfo_Product_Groups.GroupName + " already exists, please enter another.";
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(orderInfo_Product_Groups);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to add Order Product Group Type: " + JsonConvert.SerializeObject(orderInfo_Product_Groups);
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }
        public ResponseBase Update(OrderInfo_Product_Groups orderInfo_Product_Groups)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var Exist = ctx.OrderInfo_Product_Groups.Where(s => s.GroupName == orderInfo_Product_Groups.GroupName && s.ID != orderInfo_Product_Groups.ID).FirstOrDefault();
                    if (Exist == null)
                    {
                        ctx.Entry(orderInfo_Product_Groups).State = EntityState.Modified;
                        var updated = ctx.SaveChanges();

                        if (updated > 0)
                        {
                            response.ResponseSuccess = true;
                            response.ResponseInt = orderInfo_Product_Groups.ID;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully updated Order Product Group Type";
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to update Order Product Group Type with " + JsonConvert.SerializeObject(orderInfo_Product_Groups);
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseSuccess = false;
                        response.ResponseMessage = "The Order Product Group Type " + orderInfo_Product_Groups.GroupName + " already exists, please enter another.";
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(orderInfo_Product_Groups);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to update Order Product Group Type with " + JsonConvert.SerializeObject(orderInfo_Product_Groups);
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
                    var orderInfo_Product_Types = ctx.OrderInfo_Product_Groups.Where(s => s.ID == ID).FirstOrDefault();

                    if (orderInfo_Product_Types != null && orderInfo_Product_Types.ID > 0)
                    {
                        ctx.OrderInfo_Product_Groups.Remove(orderInfo_Product_Types);
                        var Deleted = ctx.SaveChanges();

                        if (Deleted > 0)
                        {
                            response.ResponseSuccess = true;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully deleted Order Product Group Type";
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to Delete Order Product Group Type ID " + orderInfo_Product_Types.ID;
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to find Type for Order Product Group Type ID " + orderInfo_Product_Types.ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Order Product Group Type ID: {ID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Delete Order Product Group Type ID " + ID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }     
        public Generic<OrderInfo_Product_Groups> GetAll()
        {
            Generic<OrderInfo_Product_Groups> response = new Generic<OrderInfo_Product_Groups>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.OrderInfo_Product_Groups.ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Order Product Group Types";
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

                response.ResponseMessage = "Unable to get all Order Product Group Types";
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }       
        public Generic<OrderInfo_Product_Groups> GetByID(int ID)
        {
            Generic<OrderInfo_Product_Groups> response = new Generic<OrderInfo_Product_Groups>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClass = ctx.OrderInfo_Product_Groups.Where(s => s.ID == ID).FirstOrDefault();

                    if (response.GenericClass != null && response.GenericClass.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Order Product Group Type for ID " + ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} For Order Product Group Type ID: {ID} {Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get Order Product Group Type for ID " + ID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }
    }
}
