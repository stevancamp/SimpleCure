using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.Suggestions;
using Library._Suggestions.Methods;

namespace BusinessLayer.Functions.Suggestions
{
    public class SuggestionFunctions : ISuggestions
    {
        #region Injection
        private _Suggestions _suggestions;
        private MapSuggestions _mapSuggestions;
        private MapResponseBase _mapResponseBase;

        public SuggestionFunctions()
        {
            _suggestions = new _Suggestions();
            _mapSuggestions = new MapSuggestions();
            _mapResponseBase = new MapResponseBase();

        }
        #endregion

        public ResponseBase Add(Suggestions_Model suggestion)
        {
            return _mapResponseBase.MapToUI(_suggestions.Add(_mapSuggestions.MapToLibrary(suggestion)));
        }

        public ResponseBase Delete(int ID)
        {
            return _mapResponseBase.MapToUI(_suggestions.Delete(ID));           
        }

        public Generic<Suggestions_Model> GetAll()
        {
            var Suggestions = _suggestions.GetAll();
            Generic<Suggestions_Model> model = new Generic<Suggestions_Model>();
            model.ResponseInt = Suggestions.ResponseInt;
            model.ResponseListInt = Suggestions.ResponseListInt;
            model.ResponseListString = Suggestions.ResponseListString;
            model.ResponseMessage = Suggestions.ResponseMessage;
            model.ResponseString = Suggestions.ResponseString;
            model.ResponseSuccess = Suggestions.ResponseSuccess;
            foreach (var item in Suggestions.GenericClassList)
            {
                model.GenericClassList.Add(_mapSuggestions.MapToUI(item));
            }
            return model;
        }

        public Generic<Suggestions_Model> GetAllByIsActive(bool IsActive)
        {
            var Suggestions = _suggestions.GetAllByIsActive(IsActive);
            Generic<Suggestions_Model> model = new Generic<Suggestions_Model>();
            model.ResponseInt = Suggestions.ResponseInt;
            model.ResponseListInt = Suggestions.ResponseListInt;
            model.ResponseListString = Suggestions.ResponseListString;
            model.ResponseMessage = Suggestions.ResponseMessage;
            model.ResponseString = Suggestions.ResponseString;
            model.ResponseSuccess = Suggestions.ResponseSuccess;
            foreach (var item in Suggestions.GenericClassList)
            {
                model.GenericClassList.Add(_mapSuggestions.MapToUI(item));
            }
            return model;
        }

        public Generic<Suggestions_Model> GetByID(int ID)
        {
            var Suggestion = _suggestions.GetByID(ID);
            Generic<Suggestions_Model> model = new Generic<Suggestions_Model>();
            model.ResponseInt = Suggestion.ResponseInt;
            model.ResponseListInt = Suggestion.ResponseListInt;
            model.ResponseListString = Suggestion.ResponseListString;
            model.ResponseMessage = Suggestion.ResponseMessage;
            model.ResponseString = Suggestion.ResponseString;
            model.ResponseSuccess = Suggestion.ResponseSuccess;
            model.GenericClass = _mapSuggestions.MapToUI(Suggestion.GenericClass);
            return model;
        }

        public ResponseBase Update(Suggestions_Model suggestion)
        {
            return _mapResponseBase.MapToUI(_suggestions.Update(_mapSuggestions.MapToLibrary(suggestion)));
        }
    }
}
