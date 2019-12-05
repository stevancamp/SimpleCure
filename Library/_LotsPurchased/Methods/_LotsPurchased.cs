using Library.DataModel;
using Library.Email.Methods;
using Library.ErrorLogging.Methods;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;

namespace Library._LotsPurchased.Methods
{
    public class _LotsPurchased
    {
        #region Injection
        private EmailMessage _emailMessage;
        private ApplicationError _applicationError;

        public _LotsPurchased()
        {
            _emailMessage = new EmailMessage();
            _applicationError = new ApplicationError();
        }
        #endregion
       
        public ResponseBase Add(Tbl_Lots_Purchased purchased)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    purchased.Lot_Set = purchased.Lot_Set.ToUpper();                  
                    ctx.Tbl_Lots_Purchased.Add(purchased);
                    var Added = ctx.SaveChanges();

                    if (Added > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = purchased.ID;
                        response.responseTypes = ResponseTypes.Success;
                        response.ResponseMessage = "Successfully added Lot/Set Purchased";
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to add Lot/Set Purchased " + JsonConvert.SerializeObject(purchased);
                        response.responseTypes = ResponseTypes.Information;
                    }
                }
            }
            catch (Exception ex)
            {

                string obj = JsonConvert.SerializeObject(purchased);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException?.ToString() ?? ex.ToString();
                string ErrorMessage = $"There was an error at {TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"))} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to add Lot/Set Purchased: " + JsonConvert.SerializeObject(purchased);
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public ResponseBase Update(Tbl_Lots_Purchased purchased)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                 
                    purchased.Lot_Set = purchased.Lot_Set.ToUpper();
                    ctx.Entry(purchased).State = EntityState.Modified;
                    var updated = ctx.SaveChanges();

                    if (updated > 0)
                    {
                        response.ResponseSuccess = true;
                        response.ResponseInt = purchased.ID;
                        response.responseTypes = ResponseTypes.Success;
                        response.ResponseMessage = "Successfully updated Lot/Set Purchased";
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to update Lot/Set Purchased with " + JsonConvert.SerializeObject(purchased);
                        response.responseTypes = ResponseTypes.Information;
                    }

                }
            }
            catch (Exception ex)
            {
                string obj = JsonConvert.SerializeObject(purchased);
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException?.ToString() ?? ex.ToString();
                string ErrorMessage = $"There was an error at {TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"))} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Object: {obj}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to update Lot/Set Purchased with " + JsonConvert.SerializeObject(purchased);
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
                    var Purchased = ctx.Tbl_Lots_Purchased.Where(s => s.ID == ID).FirstOrDefault();

                    if (Purchased != null && Purchased.ID > 0)
                    {
                        ctx.Tbl_Lots_Purchased.Remove(Purchased);
                        var Deleted = ctx.SaveChanges();

                        if (Deleted > 0)
                        {
                            response.ResponseSuccess = true;
                            response.responseTypes = ResponseTypes.Success;
                            response.ResponseMessage = "Successfully deleted Lot/Set Purchased";
                        }
                        else
                        {
                            response.ResponseMessage = "Unable to Delete Lot/Set Purchased ID " + Purchased.ID;
                            response.responseTypes = ResponseTypes.Information;
                        }
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to find Type for Lot/Set Purchased ID " + Purchased.ID;
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
                string ErrorMessage = $"There was an error at {TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"))} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} Lot/Set Purchased ID: {ID.ToString()}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to Delete Lot/Set Purchased ID " + ID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }
 
        public Generic<Tbl_Lots_Purchased> GetAll()
        {
            Generic<Tbl_Lots_Purchased> response = new Generic<Tbl_Lots_Purchased>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.Tbl_Lots_Purchased.ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;

                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get all Lot/Set Purchased";
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
                string ErrorMessage = $"There was an error at {TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"))} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine}";
                _applicationError.Log(ErrorMessage, string.Empty);

                response.ResponseMessage = "Unable to get all Lot/Set Purchased";
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }
      
        public Generic<Tbl_Lots_Purchased> GetByID(int ID)
        {
            Generic<Tbl_Lots_Purchased> response = new Generic<Tbl_Lots_Purchased>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClass = ctx.Tbl_Lots_Purchased.Where(s => s.ID == ID).FirstOrDefault();

                    if (response.GenericClass != null && response.GenericClass.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Lot/Set Purchased for ID " + ID;
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
                string ErrorMessage = $"There was an error at {TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"))} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} For Lot/Set Purchased ID: {ID} {Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get Lot/Set Purchased for ID " + ID;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<Tbl_Lots_Purchased> GetByLotSet(string LotSet)
        {
            Generic<Tbl_Lots_Purchased> response = new Generic<Tbl_Lots_Purchased>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClass = ctx.Tbl_Lots_Purchased.Where(s => s.Lot_Set == LotSet).FirstOrDefault();

                    if (response.GenericClass != null && response.GenericClass.ID > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Lot/Set Purchased for LotSet " + LotSet;
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
                string ErrorMessage = $"There was an error at {TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"))} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} For Lot/Set Purchased LotSet: {LotSet} {Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get Lot/Set Purchased for LotSet " + LotSet;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }
       
        public Generic<Tbl_Lots_Purchased> GetByProvider(int Provider)
        {
            Generic<Tbl_Lots_Purchased> response = new Generic<Tbl_Lots_Purchased>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.Tbl_Lots_Purchased.Where(s => s.Provider == Provider).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Lot/Set Purchased for Provider " + Provider;
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
                string ErrorMessage = $"There was an error at {TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"))} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} For Lot/Set Purchased Provider: {Provider} {Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get Lot/Set Purchased for LotSet " + Provider;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }
        
        public Generic<Tbl_Lots_Purchased> GetByComplete(bool IsComplete)
        {
            Generic<Tbl_Lots_Purchased> response = new Generic<Tbl_Lots_Purchased>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.Tbl_Lots_Purchased.Where(s => s.Complete == IsComplete).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Lot/Set Purchased for Complete " + IsComplete;
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
                string ErrorMessage = $"There was an error at {TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"))} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} For Lot/Set Purchased Complete: {IsComplete} {Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get Lot/Set Purchased for Complete " + IsComplete;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<Tbl_Lots_Purchased> GetByCompleteByRange(DateTime StartTime, DateTime EndTime, bool IsComplete)
        {
            Generic<Tbl_Lots_Purchased> response = new Generic<Tbl_Lots_Purchased>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {
                    response.GenericClassList = ctx.Tbl_Lots_Purchased.Where(s => s.BuyDate >= StartTime && s.BuyDate <= EndTime && s.Complete == IsComplete).ToList();

                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Lot/Set Purchased for Complete " + IsComplete;
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
                string ErrorMessage = $"There was an error at {TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"))} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} For Lot/Set Purchased Complete: {IsComplete} {Environment.NewLine}";
                errors.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get Lot/Set Purchased for Complete " + IsComplete;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;
        }

        public Generic<Tbl_Lots_Purchased> Search(int? ProviderID, string Lot_Set, DateTime? StartTime, DateTime? EndTime, bool? IsComplete)
        {

            Generic<Tbl_Lots_Purchased> response = new Generic<Tbl_Lots_Purchased>();

            try
            {
                using (var ctx = new SimpleCureEntities())
                {

                    response.GenericClassList = ctx.Tbl_Lots_Purchased.ToList();

                    if (ProviderID != null && ProviderID > 0)
                    {
                        response.GenericClassList = response.GenericClassList.Where(s => s.Provider == ProviderID).ToList();
                    }

                    if (!string.IsNullOrEmpty(Lot_Set))
                    {
                        response.GenericClassList = response.GenericClassList.Where(s => s.Lot_Set.ToLower().Contains(Lot_Set.ToLower())).ToList();
                    }

                    if (StartTime != null)
                    {
                        response.GenericClassList = response.GenericClassList.Where(s => s.EnterDate >= s.BuyDate).ToList();
                    }

                    if (EndTime != null)
                    {
                        response.GenericClassList = response.GenericClassList.Where(s => s.EnterDate <= s.BuyDate).ToList();
                    }

                    if (IsComplete != null)
                    {
                        response.GenericClassList = response.GenericClassList.Where(s => s.Complete == IsComplete).ToList();
                    }
                
                    if (response.GenericClassList != null && response.GenericClassList.Count > 0)
                    {
                        response.ResponseSuccess = true;
                        response.responseTypes = ResponseTypes.Success;
                    }
                    else
                    {
                        response.ResponseMessage = "Unable to get Lot/Set by parameters provided";
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
                string ErrorMessage = $"There was an error at {TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"))} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} ProviderID {ProviderID ?? null} {Environment.NewLine} Lot_Set {Lot_Set ?? null} {Environment.NewLine} StartTime {StartTime ?? null} {Environment.NewLine} EndTime {EndTime ?? null} {Environment.NewLine} IsComplete {IsComplete ?? null} {Environment.NewLine} Error: {error}{Environment.NewLine}";
                _applicationError.Log(ErrorMessage, string.Empty);
                response.ResponseMessage = "Unable to get Lot/Set Purchased for Complete " + IsComplete;
                response.responseTypes = ResponseTypes.Failure;
            }

            return response;

        }
    }
}
