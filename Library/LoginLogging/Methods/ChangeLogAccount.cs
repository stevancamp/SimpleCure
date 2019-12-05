using Library.DataModel;
using Library.Email.Methods;
using Library.ErrorLogging.Methods;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;

namespace Library.LoginLogging.Methods
{
    public class ChangeLogAccount
    {
        #region Injection
        private EmailMessage _emailMessage;
        private ApplicationError _applicationError;

        public ChangeLogAccount()
        {
            _emailMessage = new EmailMessage();
            _applicationError = new ApplicationError();
        }
        #endregion

        public ResponseBase Add(AccountChangeLog model)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    ctx.AccountChangeLogs.Add(model);
                    var Added = ctx.SaveChanges();

                    if (Added > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = model.ID;
                        response.responseTypes = ResponseTypes.Success;
                        response.ResponseMessage = "Successfully added new Account Change Log";
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to add Account Change Log: " + JsonConvert.SerializeObject(model);
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {

                string obj = JsonConvert.SerializeObject(model);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException?.ToString() ?? ex.ToString();
                string ErrorMessage = $"There was an error at {TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"))} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to add Account Change Log: " + JsonConvert.SerializeObject(model);
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<AccountChangeLog> GetByUserIDByDate(string UserID, DateTime LoginDate)
        {
            Generic<AccountChangeLog> response = new Generic<AccountChangeLog>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.AccountChangeLogs.Where(s => s.UserIDChanged == UserID && DbFunctions.TruncateTime(s.ChangedDateTime) == DbFunctions.TruncateTime(LoginDate)).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Account Change Log for User " + UserID + " on Login Date " + LoginDate.ToShortDateString();
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
                string ErrorMessage = $"There was an error at {TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"))} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Login Date: {LoginDate.ToShortDateString()} {Environment.NewLine} User ID: {UserID}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to get Account Change Log for User " + UserID + " on Login Date " + LoginDate.ToShortDateString();
                response.responseTypes = ResponseTypes.Failure;
            }


            return response;
        }

    }
}
