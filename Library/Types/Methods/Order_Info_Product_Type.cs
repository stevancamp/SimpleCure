using Library.DataModel;
using Library.Email.Methods;
using Library.ErrorLogging.Methods;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;

namespace Library.Types.Methods
{
    public class Order_Info_Product_Type
    {
        #region Injection
        private EmailMessage _emailMessage;
        private ApplicationError _applicationError;

        public Order_Info_Product_Type()
        {
            _emailMessage = new EmailMessage();
            _applicationError = new ApplicationError();
        }
        #endregion

        public ResponseBase Add(OrderInfo_Product_Types orderInfo_Product_Types)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var Added = ctx.OrderInfo_Product_Types.Add(orderInfo_Product_Types);

                    if (Added.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = Added.ID;

                    }
                    else
                    {
                        response.ResponseMessage = "Unable to add Order Info Product Type: " + JsonConvert.SerializeObject(orderInfo_Product_Types);
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(orderInfo_Product_Types);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to add Order Info Product Type: " + JsonConvert.SerializeObject(orderInfo_Product_Types);
            }

            return response;
        }

        public ResponseBase Update(OrderInfo_Product_Types orderInfo_Product_Types)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    ctx.Entry(orderInfo_Product_Types).State = EntityState.Modified;
                    var updated = ctx.SaveChanges();

                    if (updated > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = orderInfo_Product_Types.ID;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to update Order Info Producy Type with " + JsonConvert.SerializeObject(orderInfo_Product_Types);
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(orderInfo_Product_Types);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to update Order Info Producy Type with " + JsonConvert.SerializeObject(orderInfo_Product_Types);
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
                    var orderInfo_Product_Types = ctx.OrderInfo_Product_Types.Where(s => s.ID == ID).FirstOrDefault();

                    if (orderInfo_Product_Types != null && orderInfo_Product_Types.ID > 0)
                    {
                        ctx.OrderInfo_Product_Types.Remove(orderInfo_Product_Types);
                        var Deleted = ctx.SaveChanges();

                        if (Deleted > 0)
                        {
                            response.ResponseSuccess = true;
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to Delete Order Info Product Type ID " + orderInfo_Product_Types.ID;
                        }
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to find Type for Order Info Product Type ID " + orderInfo_Product_Types.ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Order Info Product Type ID: {ID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Delete Order Info Product Type ID " + ID;
            }

            return response;
        }

        public Generic<OrderInfo_Product_Types> GetAll(bool IsActive)
        {
            Generic<OrderInfo_Product_Types> response = new Generic<OrderInfo_Product_Types>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.OrderInfo_Product_Types.Where(s => s.IsActive == IsActive).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Order Info Product Types";
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

                response.ResponseMessage = "Unable to get all Order Info Product Types";
            }

            return response;
        }
    }
}
