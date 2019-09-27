using BusinessLayer.Models.Suggestions;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapSuggestions
    {
        public Suggestion MapToLibrary(Suggestions_Model model)
        {
            Suggestion suggestion = new Suggestion();
            suggestion.EntryBy = model.EntryBy;
            suggestion.EntryDate = model.EntryDate;
            suggestion.ID = model.ID;
            suggestion.IsActive = model.IsActive;
            suggestion.Status = model.Status;
            suggestion.SuggestionComments = model.SuggestionComments;
            suggestion.SuggestionTitle = model.SuggestionTitle;
            
            return suggestion;
        }

        public Suggestions_Model MapToUI(Suggestion model)
        {
            Suggestions_Model suggestion = new Suggestions_Model();
            suggestion.EntryBy = model.EntryBy;
            suggestion.EntryDate = model.EntryDate;
            suggestion.ID = model.ID;
            suggestion.IsActive = model.IsActive;
            suggestion.Status = model.Status;
            suggestion.SuggestionComments = model.SuggestionComments;
            suggestion.SuggestionTitle = model.SuggestionTitle;

            return suggestion;
        }
    }
}
