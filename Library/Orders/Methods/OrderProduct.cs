using Library.DataModel;
using Library.Email.Methods;
using Library.ErrorLogging.Methods;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;

namespace Library.Orders.Methods
{
    public class OrderProduct
    {
        #region Injection
        private EmailMessage _emailMessage;
        private ApplicationError _applicationError;

        public OrderProduct()
        {
            _emailMessage = new EmailMessage();
            _applicationError = new ApplicationError();
        }
        #endregion

        public ResponseBase Add(OrderInfo_Product Product)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var Added = ctx.OrderInfo_Product.Add(Product);

                    if (Added.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = Added.ID;
                        //confirm if this is needed or not
                        //_emailMessage.SendMessage(ConfigurationManager.AppSettings["EmailAddress"], "Product added to Order", "A product has been added to a order.");
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to add Products to Order with Product Info: " + Product;
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(Product);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to add Products to Order with Product Info: " + Product;
            }

            return response;
        }

        public ResponseBase Update(OrderInfo_Product Product)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    ctx.Entry(Product).State = System.Data.Entity.EntityState.Modified;
                    var updated = ctx.SaveChanges();

                    if (updated > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = Product.ID;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to update Product Order Info for Product: " + Product;
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(Product);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to update Product Order Info for Product: " + Product;
            }

            return response;
        }

        public ResponseBase Delete(OrderInfo_Product Product)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    ctx.OrderInfo_Product.Remove(Product);
                    var Deleted = ctx.SaveChanges();

                    if (Deleted > 0)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to Delete Product Order with ID " + Product.ID;
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(Product);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Delete Product Order with ID " + Product.ID;
            }

            return response;
        }

        public Generic<OrderInfo_Product> GetAll()
        {
            Generic<OrderInfo_Product> response = new Generic<OrderInfo_Product>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.OrderInfo_Product.ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Product Order Info";
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

                response.ResponseMessage = "Unable to get all Product Order Info";
            }

            return response;
        }

        public Generic<OrderInfo_Product> GetByID(int ID)
        {
            Generic<OrderInfo_Product> response = new Generic<OrderInfo_Product>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClass = ctx.OrderInfo_Product.Where(s => s.ID == ID).FirstOrDefault();

                    if (response.GenericClass != null && response.GenericClass.ID > 0)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Product Order Info for ID " + ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Order Product ID: {ID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get Product Order Info for ID " + ID;
            }

            return response;
        }

        public Generic<OrderInfo_Product> GetAllByOrderID(int OrderID)
        {
            Generic<OrderInfo_Product> response = new Generic<OrderInfo_Product>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.OrderInfo_Product.Where(s => s.OrderInfoID == OrderID).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Product Order Info by Order ID " + OrderID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Order ID: {OrderID}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to get Product Order Info by Order ID " + OrderID;
            }

            return response;
        }
    }
}
