using Library.DataModel;
using Library.Email.Methods;
using Library.ErrorLogging.Methods;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;

namespace Library._OrderProducts.Methods
{
    public class _OrderProducts
    {
        #region Injection
        private EmailMessage _emailMessage;
        private ApplicationError _applicationError;

        public _OrderProducts()
        {
            _emailMessage = new EmailMessage();
            _applicationError = new ApplicationError();
        }
        #endregion

        public ResponseBase Add(OrderProduct OrderProducts)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                  
                        ctx.OrderProducts.Add(OrderProducts);
                        var Added = ctx.SaveChanges();

                        if (Added > 0)
                        {
                            response.ResponseSuccess = true;
                            response.ResponseInt = OrderProducts.ID;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully added OrderProducts";

                        }
                        else
                        {
                            response.ResponseMessage = "Unable to add OrderProducts: " + JsonConvert.SerializeObject(OrderProducts);
                            response.responseTypes = ResponseTypes.Information;
                        }
                   
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(OrderProducts);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to add OrderProducts: " + JsonConvert.SerializeObject(OrderProducts);
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public ResponseBase Update(OrderProduct OrderProducts)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                  
                        ctx.Entry(OrderProducts).State = EntityState.Modified;
                        var updated = ctx.SaveChanges();

                        if (updated > 0)
                        {
                            response.ResponseSuccess = true;
                            response.ResponseInt = OrderProducts.ID;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully updated OrderProducts";
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to update OrderProducts with " + JsonConvert.SerializeObject(OrderProducts);
                            response.responseTypes = ResponseTypes.Information;
                        }
                   
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(OrderProducts);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to update OrderProducts with " + JsonConvert.SerializeObject(OrderProducts);
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
                    var OrderProducts = ctx.OrderProducts.Where(s => s.ID == ID).FirstOrDefault();

                    if (OrderProducts != null && OrderProducts.ID > 0)
                    {
                        ctx.OrderProducts.Remove(OrderProducts);
                        var Deleted = ctx.SaveChanges();

                        if (Deleted > 0)
                        {
                            response.ResponseSuccess = true;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully deleted OrderProducts";
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to Delete OrderProducts ID " + OrderProducts.ID;
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to find Type for OrderProducts ID " + OrderProducts.ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} OrderProducts ID: {ID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Delete OrderProducts ID " + ID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<OrderProduct> GetAll()
        {
            Generic<OrderProduct> response = new Generic<OrderProduct>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.OrderProducts.ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;

                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all OrderProductss";
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

                response.ResponseMessage = "Unable to get all OrderProductss";
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<OrderProduct> GetAllByByOrderID(int OrderID)
        {
            Generic<OrderProduct> response = new Generic<OrderProduct>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.OrderProducts.Where(s => s.OrderID == OrderID).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;

                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all OrderProductss by OrderID " + OrderID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} OrderID: {OrderID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to get all OrderProductss by OrderID " + OrderID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<OrderProduct> GetByID(int ID)
        {
            Generic<OrderProduct> response = new Generic<OrderProduct>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClass = ctx.OrderProducts.Where(s => s.ID == ID).FirstOrDefault();

                    if (response.GenericClass != null && response.GenericClass.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get OrderProducts for ID " + ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} For OrderProducts ID: {ID} {Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get OrderProducts for ID " + ID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }
    }
}
