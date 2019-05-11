using Library.DataModel;
using Library.Email.Methods;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;

namespace Library.ErrorLogging.Methods
{
    public class ApplicationError
    {
        #region Injection
        private EmailMessage _emailMessage;

        public ApplicationError()
        {
            _emailMessage = new EmailMessage();
        }
        #endregion

        public ResponseBase Log(string ErrorMessage, string IPAddress)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    AppError error = new AppError();

                    error.ErrorMessage = ErrorMessage;
                    error.ErrorTime = DateTime.Now;
                    error.IP_Address = IPAddress;

                    var Logged = ctx.AppErrors.Add(error);

                    if (Logged.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = Logged.ID;
                    }
                    else
                    {                        
                        _emailMessage.SendMessage(ConfigurationManager.AppSettings["DeveloperEmail"], "Simple Cure Failure To Log", $"At {DateTime.Now} the Simple Cure application was unable to log error message {ErrorMessage} for IPAddress {IPAddress}");
                        response.ResponseMessage = "Failed to log error message!";
                    }
                }
            }
            catch (Exception ex)
            {               
                _emailMessage.SendMessage(ConfigurationManager.AppSettings["DeveloperEmail"], "Simple Cure Failure To Log", $"At {DateTime.Now} the Simple Cure application was unable to log error message {ErrorMessage} for IPAddress {IPAddress} Catch exception is {ex.ToString()}");

                response.ResponseMessage = ex.ToString();
                throw;
            }

            return response;
        }

        public Generic<AppError> GetAll()
        {
            Generic<AppError> response = new Generic<AppError>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.AppErrors.ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to Get All Application Errors";
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Get All Application Errors";
            }

            return response;
        }

        public Generic<AppError> GetByID(int ID)
        {
            Generic<AppError> response = new Generic<AppError>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClass = ctx.AppErrors.Where(s => s.ID == ID).FirstOrDefault();

                    if (response.GenericClass != null && response.GenericClass.ID > 0)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Application Error Information for ID " + ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} For Error ID: {ID} {Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get Application Error Information for ID " + ID;
            }

            return response;
        }

        //look at this for the getting by date
        public Generic<AppError> GetByDate(DateTime Date)
        {
            Generic<AppError> response = new Generic<AppError>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.AppErrors.Where(s => DbFunctions.TruncateTime(s.ErrorTime) == Date).ToList();

                    
                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to Get Application Errors for Date " + Date;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} For Error Date: {Date} {Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);
                throw;
            }

            return response;
        }

    }
}
