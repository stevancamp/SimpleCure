using BusinessLayer.Models.SuggestionStatus;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapSuggestionStatus
    {
        public SuggestionStatu MapToLibrary(SuggestionStatus_Model model)
        {
            SuggestionStatu suggestion = new SuggestionStatu();
            suggestion.ID = model.ID;
            suggestion.Status = model.Status;

            return suggestion;
        }

        public SuggestionStatus_Model MapToUI(SuggestionStatu model)
        {
            SuggestionStatus_Model suggestion = new SuggestionStatus_Model();
            suggestion.ID = model.ID;
            suggestion.Status = model.Status;

            return suggestion;
        }
    }
}
