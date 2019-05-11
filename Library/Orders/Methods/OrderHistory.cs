using Library.DataModel;
using Library.Email.Methods;
using Library.ErrorLogging.Methods;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;

namespace Library.Orders.Methods
{
    public class OrderHistory
    {
        #region Injection

        private ApplicationError _applicationErrors;
        private EmailMessage _emailMessage;

        public OrderHistory()
        {
            _applicationErrors = new ApplicationError();
            _emailMessage = new EmailMessage();
        }
        #endregion

        public ResponseBase Add(OrderActivityHistory history)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var Added = ctx.OrderActivityHistories.Add(history);

                    if (Added.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = Added.ID;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to Add Order History with " + JsonConvert.SerializeObject(history);
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(history);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationErrors.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Add Order History with " + JsonConvert.SerializeObject(history);
            }

            return response;
        }

        public ResponseBase Update(OrderActivityHistory history)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    ctx.Entry(history).State = EntityState.Modified;
                    var updated = ctx.SaveChanges();

                    if (updated > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = history.ID;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to update Order Activity History with: " + JsonConvert.SerializeObject(history);
                    }

                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(history);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationErrors.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to update Order Activity History with: " + JsonConvert.SerializeObject(history);
            }

            return response;
        }

        public ResponseBase Delete(int HistoryID)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var History = ctx.OrderActivityHistories.Where(s => s.ID == HistoryID).FirstOrDefault();

                    if (History != null && History.ID > 0)
                    {
                        ctx.OrderActivityHistories.Remove(History);
                        var Deleted = ctx.SaveChanges();

                        if (Deleted > 0)
                        {
                            response.ResponseSuccess = true;
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to Delete Order Activity History ID " + History.ID;
                        }
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to find History Info for Order Activity History ID " + History.ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Order Activity History ID: {HistoryID.ToString()}";
                _applicationErrors.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Delete Order Activity History ID " + HistoryID.ToString();
            }

            return response;
        }

        public Generic<OrderActivityHistory> GetAll()
        {
            Generic<OrderActivityHistory> response = new Generic<OrderActivityHistory>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.OrderActivityHistories.ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Order Activity History Info";
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
                _applicationErrors.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get all Order Activity History Info";
            }

            return response;
        }

        public Generic<OrderActivityHistory> GetAllByOrderID(int OrderID)
        {
            Generic<OrderActivityHistory> response = new Generic<OrderActivityHistory>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.OrderActivityHistories.Where(s => s.OrderID == OrderID).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to Get Order Activity History by Order ID: " + OrderID;
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
                _applicationErrors.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Get Order Activity History by Order ID: " + OrderID;
            }

            return response;
        }
    }
}
