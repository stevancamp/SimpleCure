using BusinessLayer.Models.SuggestionWorkLog;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapSuggestionWorkLog
    {
        public SuggestionWorkLog MapToLibrary(SuggestionWorkLog_Model model)
        {
            SuggestionWorkLog suggestion = new SuggestionWorkLog();
            suggestion.Comment = model.Comment;
            suggestion.EndDateTime = model.EndDateTime;
            suggestion.EntryBy = model.EntryBy;
            suggestion.EntryDateTime = model.EntryDateTime;
            suggestion.ID = model.ID;
            suggestion.StartDateTime = model.StartDateTime;
            suggestion.SuggestionID = model.SuggestionID;
                        
            return suggestion;
        }

        public SuggestionWorkLog_Model MapToUI(SuggestionWorkLog model)
        {
            SuggestionWorkLog_Model suggestion = new SuggestionWorkLog_Model();
            suggestion.Comment = model.Comment;
            suggestion.EndDateTime = model.EndDateTime;
            suggestion.EntryBy = model.EntryBy;
            suggestion.EntryDateTime = model.EntryDateTime;
            suggestion.ID = model.ID;
            suggestion.StartDateTime = model.StartDateTime;
            suggestion.SuggestionID = model.SuggestionID;

            return suggestion;
        }
    }
}
