using BusinessLayer.Functions.Email;
using BusinessLayer.Functions.ErrorLogging;
using BusinessLayer.Functions.Suggestions;
using BusinessLayer.Functions.SuggestionStatus;
using BusinessLayer.Functions.SuggestionWorkLog;
using SimpleCure.Models;
using SimpleCure.Models.SuggestionModels;
using System;
using System.Web.Mvc;

namespace SimpleCure.Controllers
{
    [Authorize(Roles = "WebAdmin,Owner")]
    public class SuggestionsController : Controller
    {
        #region Injection
        private LoggerFunctions _loggerFunctions;
        private EmailFunctions _emailFunctions;
        private SuggestionFunctions _suggestionFunctions;
        private SuggestionStatusFunctions _suggestionStatusFunctions;
        private SuggestionWorkLogFunctions _suggestionWorkLogFunctions;

        public SuggestionsController()
        {
            _loggerFunctions = new LoggerFunctions();
            _emailFunctions = new EmailFunctions();
            _suggestionFunctions = new SuggestionFunctions();
            _suggestionStatusFunctions = new SuggestionStatusFunctions();
            _suggestionWorkLogFunctions = new SuggestionWorkLogFunctions();
        }
        #endregion

        public ActionResult EnterSuggestion()
        {
            return View(new Suggestion_ViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNewSuggestion(Suggestion_ViewModel model)
        {
            ResponseBase response = new ResponseBase();
            if (ModelState.IsValid)
            {

                var Added = _suggestionFunctions.Add(new BusinessLayer.Models.Suggestions.Suggestions_Model { EntryBy = User.Identity.Name, EntryDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")), ID = 0, IsActive = true, Status = "Initial Entry", SuggestionComments = model.SuggestionComments, SuggestionTitle = model.SuggestionTitle });
                response.ResponseInt = Added.ResponseInt;
                response.ResponseListInt = Added.ResponseListInt;
                response.ResponseListString = Added.ResponseListString;
                response.ResponseMessage = Added.ResponseMessage;
                response.ResponseString = Added.ResponseString;
                response.ResponseSuccess = Added.ResponseSuccess;
                switch (Added.responseTypes)
                {
                    case BusinessLayer.Models.ResponseTypes.Success:
                        response.responseTypes = ResponseTypes.Success;
                        break;
                    case BusinessLayer.Models.ResponseTypes.Failure:
                        response.responseTypes = ResponseTypes.Failure;
                        break;
                    case BusinessLayer.Models.ResponseTypes.Information:
                        response.responseTypes = ResponseTypes.Information;
                        break;
                    default:
                        response.responseTypes = ResponseTypes.Failure;
                        break;
                }
                if (Added.ResponseSuccess)
                {
                    return RedirectToAction("ViewAllSuggestions");
                }
            }
            return View("EnterSuggestion", model);
        }

        public ActionResult ViewAllSuggestions(bool IsActive = true)
        {
            var Suggestions = _suggestionFunctions.GetAllByIsActive(IsActive);
            var suggestionResponseTypes = new ResponseTypes();
            switch (Suggestions.responseTypes)
            {
                case BusinessLayer.Models.ResponseTypes.Success:
                    suggestionResponseTypes = ResponseTypes.Success;
                    break;
                case BusinessLayer.Models.ResponseTypes.Failure:
                    suggestionResponseTypes = ResponseTypes.Failure;
                    break;
                case BusinessLayer.Models.ResponseTypes.Information:
                    suggestionResponseTypes = ResponseTypes.Information;
                    break;
                default:
                    break;
            }
            return View(new ViewAllSuggestions_ViewModel { ListSuggestions = Suggestions?.GenericClassList, ResponseInt = Suggestions.ResponseInt, ResponseListInt = Suggestions.ResponseListInt, ResponseListString = Suggestions.ResponseListString, ResponseMessage = Suggestions.ResponseMessage, ResponseString = Suggestions.ResponseString, ResponseSuccess = Suggestions.ResponseSuccess, responseTypes = suggestionResponseTypes });
        }

        [HttpPost]
        public JsonResult DeleteSuggestion(int ID)
        {
            var Deleted = _suggestionFunctions.Delete(ID);
            var DeletedSuggestionResponseType = new ResponseTypes();
            switch (Deleted.responseTypes)
            {
                case BusinessLayer.Models.ResponseTypes.Success:
                    DeletedSuggestionResponseType = ResponseTypes.Success;
                    break;
                case BusinessLayer.Models.ResponseTypes.Failure:
                    DeletedSuggestionResponseType = ResponseTypes.Failure;
                    break;
                case BusinessLayer.Models.ResponseTypes.Information:
                    DeletedSuggestionResponseType = ResponseTypes.Information;
                    break;
                default:
                    break;
            }
            return Json(new ResponseBase { ResponseInt = Deleted.ResponseInt, ResponseListInt = Deleted.ResponseListInt, ResponseListString = Deleted.ResponseListString, ResponseMessage = Deleted.ResponseMessage, ResponseString = Deleted.ResponseString, ResponseSuccess = Deleted.ResponseSuccess, responseTypes = DeletedSuggestionResponseType }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SuggestionsWorkLog(int ID)
        {
            return View(new SuggestionWorkLog_ViewModel { SuggestionID = ID });
        }

        public ActionResult ViewSuggestionWorkLogsBySuggestionID(int SuggestionID)
        {
            var ListStatus = _suggestionStatusFunctions.GetAll();
            var SuggestionWorkLog = _suggestionWorkLogFunctions.GetAllBySuggestionID(SuggestionID);
            var SuggestionWorkLogResponseTypes = new ResponseTypes();
            switch (SuggestionWorkLog.responseTypes)
            {
                case BusinessLayer.Models.ResponseTypes.Success:
                    SuggestionWorkLogResponseTypes = ResponseTypes.Success;
                    break;
                case BusinessLayer.Models.ResponseTypes.Failure:
                    SuggestionWorkLogResponseTypes = ResponseTypes.Failure;
                    break;
                case BusinessLayer.Models.ResponseTypes.Information:
                    SuggestionWorkLogResponseTypes = ResponseTypes.Information;
                    break;
                default:
                    break;
            }

            return PartialView(new ViewSuggestionWorkLogsBySuggestionID_ViewModel { ListSuggestionWorkLog = SuggestionWorkLog?.GenericClassList, ListStatus = ListStatus?.GenericClassList, ResponseInt = SuggestionWorkLog.ResponseInt, ResponseListInt = SuggestionWorkLog.ResponseListInt, ResponseListString = SuggestionWorkLog.ResponseListString, ResponseMessage = SuggestionWorkLog.ResponseMessage, ResponseString = SuggestionWorkLog.ResponseString, ResponseSuccess = SuggestionWorkLog.ResponseSuccess, responseTypes = SuggestionWorkLogResponseTypes });
        }

        [HttpPost]
        public JsonResult SaveSuggestionWorkLog(EnterSuggestionWorkLog_ViewModel model)
        {
            ResponseBase response = new ResponseBase();
            var SavedWorkLogItem = _suggestionWorkLogFunctions.Add(new BusinessLayer.Models.SuggestionWorkLog.SuggestionWorkLog_Model { Comment = model.Comment, EndDateTime = model.EndDateTime, EntryBy = User.Identity.Name, EntryDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")), StartDateTime = model.StartDateTime, SuggestionID = model.SuggestionID, ID = 0 });
            if (SavedWorkLogItem.ResponseSuccess)
            {
                var suggestion = _suggestionFunctions.GetByID(model.SuggestionID);
                if (suggestion.ResponseSuccess)
                {
                    bool SuggestionIsComplete = new bool();
                    if (model.IsComplete)
                    {
                        SuggestionIsComplete = false;
                    }
                    var UpdatedSuggestion = _suggestionFunctions.Update(new BusinessLayer.Models.Suggestions.Suggestions_Model { EntryBy = suggestion.GenericClass.EntryBy, EntryDate = suggestion.GenericClass.EntryDate, ID = suggestion.GenericClass.ID, IsActive = SuggestionIsComplete, Status = model.SuggestionStatus, SuggestionComments = suggestion.GenericClass.SuggestionComments, SuggestionTitle = suggestion.GenericClass.SuggestionTitle });

                    if (UpdatedSuggestion.ResponseSuccess)
                    {
                        response.ResponseSuccess = true;
                    }
                    else
                    {
                        response.ResponseMessage = UpdatedSuggestion.ResponseMessage;
                    }
                }
                else
                {
                    response.ResponseMessage = suggestion.ResponseMessage;
                }
            }
            else
            {
                response.ResponseMessage = SavedWorkLogItem.ResponseMessage;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ResponseBase DeleteSuggestionWorkLog(int ID)
        {
            var Deleted = _suggestionWorkLogFunctions.Delete(ID);
            var DeletedSuggestionWorkLogResponseType = new ResponseTypes();
            switch (Deleted.responseTypes)
            {
                case BusinessLayer.Models.ResponseTypes.Success:
                    DeletedSuggestionWorkLogResponseType = ResponseTypes.Success;
                    break;
                case BusinessLayer.Models.ResponseTypes.Failure:
                    DeletedSuggestionWorkLogResponseType = ResponseTypes.Failure;
                    break;
                case BusinessLayer.Models.ResponseTypes.Information:
                    DeletedSuggestionWorkLogResponseType = ResponseTypes.Information;
                    break;
                default:
                    break;
            }
            return new ResponseBase { ResponseInt = Deleted.ResponseInt, ResponseListInt = Deleted.ResponseListInt, ResponseListString = Deleted.ResponseListString, ResponseMessage = Deleted.ResponseMessage, ResponseString = Deleted.ResponseString, ResponseSuccess = Deleted.ResponseSuccess, responseTypes = DeletedSuggestionWorkLogResponseType };
        }

    }
}