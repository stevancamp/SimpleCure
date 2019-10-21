using Library.DataModel;
using Library.Email.Methods;
using Library.ErrorLogging.Methods;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;

namespace Library._ProductGroup.Methods
{
    public class _ProductGroup
    {
        #region Injection
        private EmailMessage _emailMessage;
        private ApplicationError _applicationError;

        public _ProductGroup()
        {
            _emailMessage = new EmailMessage();
            _applicationError = new ApplicationError();
        }
        #endregion

        public ResponseBase Add(ProductGroup ProductGroup)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var Exist = ctx.ProductGroups.Where(s => s.GroupName == ProductGroup.GroupName).FirstOrDefault();
                    if (Exist == null)
                    {
                        ctx.ProductGroups.Add(ProductGroup);
                        var Added = ctx.SaveChanges();

                        if (Added > 0)
                        {
                            response.ResponseSuccess = true;
                            response.ResponseInt = ProductGroup.ID;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully added ProductGroup";

                        }
                        else
                        {
                            response.ResponseMessage = "Unable to add ProductGroup: " + JsonConvert.SerializeObject(ProductGroup);
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseSuccess = false;
                        response.ResponseMessage = "The ProductGroup " + ProductGroup.GroupName + " already exists, please enter another.";
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(ProductGroup);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException?.ToString() ?? ex.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to add ProductGroup: " + JsonConvert.SerializeObject(ProductGroup);
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public ResponseBase Update(ProductGroup ProductGroup)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var Exist = ctx.ProductGroups.Where(s => s.GroupName == ProductGroup.GroupName && s.ID != ProductGroup.ID).FirstOrDefault();
                    if (Exist == null)
                    {
                        ctx.Entry(ProductGroup).State = EntityState.Modified;
                        var updated = ctx.SaveChanges();

                        if (updated > 0)
                        {
                            response.ResponseSuccess = true;
                            response.ResponseInt = ProductGroup.ID;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully updated ProductGroup";
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to update ProductGroup with " + JsonConvert.SerializeObject(ProductGroup);
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseSuccess = false;
                        response.ResponseMessage = "The ProductGroup " + ProductGroup.GroupName + " already exists, please enter another.";
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(ProductGroup);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException?.ToString() ?? ex.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to update ProductGroup with " + JsonConvert.SerializeObject(ProductGroup);
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
                    var ProductGroup = ctx.ProductGroups.Where(s => s.ID == ID).FirstOrDefault();

                    if (ProductGroup != null && ProductGroup.ID > 0)
                    {
                        ctx.ProductGroups.Remove(ProductGroup);
                        var Deleted = ctx.SaveChanges();

                        if (Deleted > 0)
                        {
                            response.ResponseSuccess = true;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully deleted ProductGroup";
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to Delete ProductGroup ID " + ProductGroup.ID;
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to find Type for ProductGroup ID " + ProductGroup.ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} ProductGroup ID: {ID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Delete ProductGroup ID " + ID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<ProductGroup> GetAll()
        {
            Generic<ProductGroup> response = new Generic<ProductGroup>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.ProductGroups.ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;

                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all ProductGroups";
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

                response.ResponseMessage = "Unable to get all ProductGroups";
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<ProductGroup> GetAllByIsActive(bool IsActive)
        {
            Generic<ProductGroup> response = new Generic<ProductGroup>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.ProductGroups.Where(s => s.IsActive == IsActive).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;

                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all ProductGroups";
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} IsActive: {IsActive.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to get all ProductGroups";
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<ProductGroup> GetByID(int ID)
        {
            Generic<ProductGroup> response = new Generic<ProductGroup>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClass = ctx.ProductGroups.Where(s => s.ID == ID).FirstOrDefault();

                    if (response.GenericClass != null && response.GenericClass.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get ProductGroup for ID " + ID;
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
                string error = ex.InnerException?.ToString() ?? ex.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} For ProductGroup ID: {ID} {Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get ProductGroup for ID " + ID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }
    }
}
