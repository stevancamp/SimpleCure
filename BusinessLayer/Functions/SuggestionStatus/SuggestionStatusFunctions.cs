using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.SuggestionStatus;
using Library._SuggestionStatus.Methods;

namespace BusinessLayer.Functions.SuggestionStatus
{
    public class SuggestionStatusFunctions : ISuggestionStatus
    {
        #region Injection
        private _SuggestionStatus _suggestionStatus;
        private MapSuggestionStatus _mapSuggestionStatus;
        private MapResponseBase _mapResponseBase;

        public SuggestionStatusFunctions()
        {
            _suggestionStatus = new _SuggestionStatus();
            _mapSuggestionStatus = new MapSuggestionStatus();
            _mapResponseBase = new MapResponseBase();

        }
        #endregion

        public Generic<SuggestionStatus_Model> GetAll()
        {
            var SuggestionStatus = _suggestionStatus.GetAll();
            Generic<SuggestionStatus_Model> model = new Generic<SuggestionStatus_Model>();
            model.ResponseInt = SuggestionStatus.ResponseInt;
            model.ResponseListInt = SuggestionStatus.ResponseListInt;
            model.ResponseListString = SuggestionStatus.ResponseListString;
            model.ResponseMessage = SuggestionStatus.ResponseMessage;
            model.ResponseString = SuggestionStatus.ResponseString;
            model.ResponseSuccess = SuggestionStatus.ResponseSuccess;
            foreach (var item in SuggestionStatus.GenericClassList)
            {
                model.GenericClassList.Add(_mapSuggestionStatus.MapToUI(item));
            }
            return model;
        }
    }
}
