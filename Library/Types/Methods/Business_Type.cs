using Library.DataModel;
using Library.Email.Methods;
using Library.ErrorLogging.Methods;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;

namespace Library.Types.Methods
{
    public class Business_Type
    {
        #region Injection
        private EmailMessage _emailMessage;
        private ApplicationError _applicationError;

        public Business_Type()
        {
            _emailMessage = new EmailMessage();
            _applicationError = new ApplicationError();
        }
        #endregion

        public ResponseBase Add(BusinessType businessType)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    var Added = ctx.BusinessTypes.Add(businessType);

                    if (Added.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = Added.ID;

                    }
                    else
                    {
                        response.ResponseMessage = "Unable to add Business Type: " + JsonConvert.SerializeObject(businessType);
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(businessType);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to add Business Type: " + JsonConvert.SerializeObject(businessType);
            }

            return response;
        }

        public ResponseBase Update(BusinessType businessType)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    ctx.Entry(businessType).State = EntityState.Modified;
                    var updated = ctx.SaveChanges();

                    if (updated > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = businessType.ID;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to update Business Type with " + JsonConvert.SerializeObject(businessType);
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(businessType);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to update Business Type with " + JsonConvert.SerializeObject(businessType);
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
                    var businessType = ctx.BusinessTypes.Where(s => s.ID == ID).FirstOrDefault();

                    if (businessType != null && businessType.ID > 0)
                    {
                        ctx.BusinessTypes.Remove(businessType);
                        var Deleted = ctx.SaveChanges();

                        if (Deleted > 0)
                        {
                            response.ResponseSuccess = true;
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to Delete Business Type ID " + businessType.ID;
                        }
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to find Type for Business Type ID " + businessType.ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Business Type ID: {ID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Delete Business Type ID " + ID.ToString();
            }

            return response;
        }

        public Generic<BusinessType> GetAll(bool IsActive)
        {
            Generic<BusinessType> response = new Generic<BusinessType>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.BusinessTypes.Where(s => s.IsActvie == IsActive).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Business Types";
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

                response.ResponseMessage = "Unable to get all Business Types";
            }

            return response;
        }
    }
}
