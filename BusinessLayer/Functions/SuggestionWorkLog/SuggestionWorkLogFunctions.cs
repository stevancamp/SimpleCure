using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.SuggestionWorkLog;
using Library._SuggestionWorkLog.Methods;

namespace BusinessLayer.Functions.SuggestionWorkLog
{
    public class SuggestionWorkLogFunctions : ISuggestionWorkLog
    {
        #region Injection
        private _SuggestionWorkLog _suggestionWorkLog;
        private MapSuggestionWorkLog _mapSuggestionWorkLog;
        private MapResponseBase _mapResponseBase;

        public SuggestionWorkLogFunctions()
        {
            _suggestionWorkLog = new _SuggestionWorkLog();
            _mapSuggestionWorkLog = new MapSuggestionWorkLog();
            _mapResponseBase = new MapResponseBase();

        }
        #endregion

        public ResponseBase Add(SuggestionWorkLog_Model suggestionWorkLog)
        {
            return _mapResponseBase.MapToUI(_suggestionWorkLog.Add(_mapSuggestionWorkLog.MapToLibrary(suggestionWorkLog)));
        }

        public ResponseBase Delete(int ID)
        {
            return _mapResponseBase.MapToUI(_suggestionWorkLog.Delete(ID));
        }

        public Generic<SuggestionWorkLog_Model> GetAll()
        {
            var SuggestionWorkLogs = _suggestionWorkLog.GetAll();
            Generic<SuggestionWorkLog_Model> model = new Generic<SuggestionWorkLog_Model>();
            model.ResponseInt = SuggestionWorkLogs.ResponseInt;
            model.ResponseListInt = SuggestionWorkLogs.ResponseListInt;
            model.ResponseListString = SuggestionWorkLogs.ResponseListString;
            model.ResponseMessage = SuggestionWorkLogs.ResponseMessage;
            model.ResponseString = SuggestionWorkLogs.ResponseString;
            model.ResponseSuccess = SuggestionWorkLogs.ResponseSuccess;
            foreach (var item in SuggestionWorkLogs.GenericClassList)
            {
                model.GenericClassList.Add(_mapSuggestionWorkLog.MapToUI(item));
            }
            return model;
        }

        public Generic<SuggestionWorkLog_Model> GetAllBySuggestionID(int SuggestionID)
        {
            var SuggestionWorkLogs = _suggestionWorkLog.GetAllBySuggestionID(SuggestionID);
            Generic<SuggestionWorkLog_Model> model = new Generic<SuggestionWorkLog_Model>();
            model.ResponseInt = SuggestionWorkLogs.ResponseInt;
            model.ResponseListInt = SuggestionWorkLogs.ResponseListInt;
            model.ResponseListString = SuggestionWorkLogs.ResponseListString;
            model.ResponseMessage = SuggestionWorkLogs.ResponseMessage;
            model.ResponseString = SuggestionWorkLogs.ResponseString;
            model.ResponseSuccess = SuggestionWorkLogs.ResponseSuccess;
            foreach (var item in SuggestionWorkLogs.GenericClassList)
            {
                model.GenericClassList.Add(_mapSuggestionWorkLog.MapToUI(item));
            }
            return model;
        }

        public Generic<SuggestionWorkLog_Model> GetByID(int ID)
        {
            var SuggestionWorkLog = _suggestionWorkLog.GetByID(ID);
            Generic<SuggestionWorkLog_Model> model = new Generic<SuggestionWorkLog_Model>();
            model.ResponseInt = SuggestionWorkLog.ResponseInt;
            model.ResponseListInt = SuggestionWorkLog.ResponseListInt;
            model.ResponseListString = SuggestionWorkLog.ResponseListString;
            model.ResponseMessage = SuggestionWorkLog.ResponseMessage;
            model.ResponseString = SuggestionWorkLog.ResponseString;
            model.ResponseSuccess = SuggestionWorkLog.ResponseSuccess;
            model.GenericClass = _mapSuggestionWorkLog.MapToUI(SuggestionWorkLog.GenericClass);
            return model;
        }

        public ResponseBase Update(SuggestionWorkLog_Model suggestionWorkLog)
        {
            return _mapResponseBase.MapToUI(_suggestionWorkLog.Update(_mapSuggestionWorkLog.MapToLibrary(suggestionWorkLog)));
        }
    }
}
