using BusinessLayer.Models.Suggestions;

namespace SimpleCure.Models.SuggestionModels
{
    public class ViewSuggestion_ViewModel : ResponseBase
    {
        public Suggestions_Model Suggestion { get; set; }
    }
}