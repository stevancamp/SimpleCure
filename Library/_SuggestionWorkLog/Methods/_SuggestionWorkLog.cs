using Library.DataModel;
using Library.Email.Methods;
using Library.ErrorLogging.Methods;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Library._SuggestionWorkLog.Methods
{
    public class _SuggestionWorkLog
    {
        #region Injection
        private EmailMessage _emailMessage;
        private ApplicationError _applicationError;

        public _SuggestionWorkLog()
        {
            _emailMessage = new EmailMessage();
            _applicationError = new ApplicationError();
        }
        #endregion

        public ResponseBase Add(SuggestionWorkLog suggestionWorkLog)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    ctx.SuggestionWorkLogs.Add(suggestionWorkLog);
                    var Added = ctx.SaveChanges();

                    if (Added > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = suggestionWorkLog.ID;
                        response.responseTypes = ResponseTypes.Success;
                        response.ResponseMessage = "Successfully added Suggestion Work Log";
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to add Suggestion Work Log: " + JsonConvert.SerializeObject(suggestionWorkLog);
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {

                string obj = JsonConvert.SerializeObject(suggestionWorkLog);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException?.ToString() ?? ex.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to add Suggestion Work Log: " + JsonConvert.SerializeObject(suggestionWorkLog);
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public ResponseBase Update(SuggestionWorkLog suggestionWorkLog)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    ctx.Entry(suggestionWorkLog).State = System.Data.Entity.EntityState.Modified;
                    var updated = ctx.SaveChanges();

                    if (updated > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = suggestionWorkLog.ID;
                        response.responseTypes = ResponseTypes.Success;
                        response.ResponseMessage = "Successfully updated Suggestion Work Log";
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to update Suggestion Work Log with " + JsonConvert.SerializeObject(suggestionWorkLog);
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(suggestionWorkLog);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException?.ToString() ?? ex.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to update Suggestion Work Log with " + JsonConvert.SerializeObject(suggestionWorkLog);
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
                    var suggestionWorkLog = ctx.SuggestionWorkLogs.Where(s => s.ID == ID).FirstOrDefault();
                    if (suggestionWorkLog != null && suggestionWorkLog.ID > 0)
                    {
                        ctx.SuggestionWorkLogs.Remove(suggestionWorkLog);
                        var Deleted = ctx.SaveChanges();

                        if (Deleted > 0)
                        {
                            response.ResponseSuccess = true;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully deleted Suggestion Work Log";
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to Delete Suggestion Work Log ID " + suggestionWorkLog.ID;
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to find Suggestion Work Log for Suggestion Work Log ID " + suggestionWorkLog.ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Suggestion Work Log ID: {ID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Delete Suggestion Work Log ID " + ID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<SuggestionWorkLog> GetAll()
        {
            Generic<SuggestionWorkLog> response = new Generic<SuggestionWorkLog>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.SuggestionWorkLogs.ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Suggestion Work Logs";
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

                response.ResponseMessage = "Unable to get all Suggestion Work Logs";
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<SuggestionWorkLog> GetAllBySuggestionID(int SuggestionID)
        {
            Generic<SuggestionWorkLog> response = new Generic<SuggestionWorkLog>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.SuggestionWorkLogs.Where(s => s.SuggestionID == SuggestionID).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Suggestion Work Logs by Suggestion ID" + SuggestionID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Suggestion ID: {SuggestionID}{Environment.NewLine} Error: {error}{Environment.NewLine}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to get all Suggestion Work Logs by Suggestion ID" + SuggestionID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<SuggestionWorkLog> GetByID(int ID)
        {
            Generic<SuggestionWorkLog> response = new Generic<SuggestionWorkLog>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClass = ctx.SuggestionWorkLogs.Where(s => s.ID == ID).FirstOrDefault();

                    if (response.GenericClass != null && response.GenericClass.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Suggestion Work Log for ID " + ID;
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
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} For Suggestion ID: {ID} {Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get Suggestion Work Log for ID " + ID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }
    }
}
