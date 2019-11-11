using Library.DataModel;
using Library.ErrorLogging.Methods;
using Library.Shared.Models;
using System;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace Library.Shared.Methods
{
    public class Shared
    {
        #region Injection

        private ApplicationError _applicationError;

        public Shared()
        {
            _applicationError = new ApplicationError();
        }
        #endregion
        public Generic<SharedModels> GetIndexNumbers()
        {
            Generic<SharedModels> response = new Generic<SharedModels>();
            response.GenericClass = new SharedModels();
            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var sevendays = DateTime.Now.AddDays(-7);

                    var CompletedOrders = (from s in ctx.Orders
                                                             where EntityFunctions.TruncateTime(s.CompletionDate) >= EntityFunctions.TruncateTime(sevendays)
                                                             && s.OrderStatus == "Paid"
                                                             select s).Count();
                    response.GenericClass.CompletedOrders = CompletedOrders;
                    var NewOrders = (from s in ctx.Orders
                                                       where EntityFunctions.TruncateTime(s.CompletionDate) >= EntityFunctions.TruncateTime(sevendays)
                                                       && s.OrderStatus == "Created"
                                                       select s).Count();
                    response.GenericClass.NewOrders = NewOrders;

                    var PendingOrders = (from s in ctx.Orders
                                                           where EntityFunctions.TruncateTime(s.CompletionDate) >= EntityFunctions.TruncateTime(sevendays)
                                                            && s.OrderStatus != "Paid" && s.OrderStatus != "Created"
                                                           select s).Count();
                    response.GenericClass.PendingOrders = PendingOrders;

                    if (response.GenericClass != null)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get the Index view information";
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
                string error = ex.InnerException?.ToString() ?? ex.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to get the Index view information";
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }
    }
}
