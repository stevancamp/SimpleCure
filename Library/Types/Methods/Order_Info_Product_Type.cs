using Library.DataModel;
using Library.Email.Methods;
using Library.ErrorLogging.Methods;
using Library.Types.Models;
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
                    var Exist = ctx.OrderInfo_Product_Types.Where(s => s.Type == orderInfo_Product_Types.Type).FirstOrDefault();
                    if (Exist == null)
                    {
                        ctx.OrderInfo_Product_Types.Add(orderInfo_Product_Types);
                        var Added = ctx.SaveChanges();

                        if (Added > 0)
                        {
                            response.ResponseSuccess = true;
                            response.ResponseInt = orderInfo_Product_Types.ID;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully added Order Product Info Type";

                        }
                        else
                        {
                            response.ResponseMessage = "Unable to add Order Info Product Type: " + JsonConvert.SerializeObject(orderInfo_Product_Types);
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseSuccess = false;
                        response.ResponseMessage = "The Order Product Info Type " + orderInfo_Product_Types.Type + " already exists, please enter another.";
                        response.responseTypes = ResponseTypes.Information;
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
                response.responseTypes = ResponseTypes.Failure;
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
                    var Exist = ctx.OrderInfo_Product_Types.Where(s => s.Type == orderInfo_Product_Types.Type && s.ID != orderInfo_Product_Types.ID).FirstOrDefault();
                    if (Exist == null)
                    {
                        ctx.Entry(orderInfo_Product_Types).State = EntityState.Modified;
                        var updated = ctx.SaveChanges();

                        if (updated > 0)
                        {
                            response.ResponseSuccess = true;
                            response.ResponseInt = orderInfo_Product_Types.ID;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully updated Order Product Info Type";
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to update Order Info Producy Type with " + JsonConvert.SerializeObject(orderInfo_Product_Types);
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseSuccess = false;
                        response.ResponseMessage = "The Order Product Info Type " + orderInfo_Product_Types.Type + " already exists, please enter another.";
                        response.responseTypes = ResponseTypes.Information;
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
                    var orderInfo_Product_Types = ctx.OrderInfo_Product_Types.Where(s => s.ID == ID).FirstOrDefault();

                    if (orderInfo_Product_Types != null && orderInfo_Product_Types.ID > 0)
                    {
                        ctx.OrderInfo_Product_Types.Remove(orderInfo_Product_Types);
                        var Deleted = ctx.SaveChanges();

                        if (Deleted > 0)
                        {
                            response.ResponseSuccess = true;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully deleted Order Product Info Type";
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to Delete Order Info Product Type ID " + orderInfo_Product_Types.ID;
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to find Type for Order Info Product Type ID " + orderInfo_Product_Types.ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Order Info Product Type ID: {ID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Delete Order Info Product Type ID " + ID;
                response.responseTypes = ResponseTypes.Failure;
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
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Order Info Product Types";
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

                response.ResponseMessage = "Unable to get all Order Info Product Types";
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<OrderInfo_Product_Types> GetByID(int ID)
        {
            Generic<OrderInfo_Product_Types> response = new Generic<OrderInfo_Product_Types>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClass = ctx.OrderInfo_Product_Types.Where(s => s.ID == ID).FirstOrDefault();

                    if (response.GenericClass != null && response.GenericClass.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Order Info Product Type for ID " + ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} For Order Info Product Type ID: {ID} {Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get Order Info Product Type for ID " + ID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<Order_Info_Product_Types_With_GroupName> GetAllWithGroupName(bool IsActive)
        {
            Generic<Order_Info_Product_Types_With_GroupName> response = new Generic<Order_Info_Product_Types_With_GroupName>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                   
                    var GetObj = (from s in ctx.OrderInfo_Product_Types
                                  join d in ctx.OrderInfo_Product_Groups on s.OrderInfo_Product_Group equals d.ID
                                  select new
                                  {
                                      s.ID,
                                      s.IsActive,
                                      s.OrderInfo_Product_Group,
                                      s.Price,
                                      s.Type,
                                      s.Type_SubHeader,
                                      d.GroupName
                                  }).ToList();

                    if (GetObj != null && GetObj.Count > 0)
                    {
                      
                        foreach (var item in GetObj)
                        {                           
                            Order_Info_Product_Types_With_GroupName obj = new Order_Info_Product_Types_With_GroupName();
                            obj.GroupName = item.GroupName;
                            obj.ID = item.ID;
                            obj.IsActive = item.IsActive;
                            obj.OrderInfo_Product_Group = item.OrderInfo_Product_Group;
                            obj.Price = item.Price;
                            obj.Type = item.Type;
                            obj.Type_SubHeader = item.Type_SubHeader;
                            response.GenericClassList.Add(obj);
                        }
 
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Order Info Product Types With Group Name";
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

                response.ResponseMessage = "Unable to get all Order Info Product Types With Group Name";
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }
    }
}
