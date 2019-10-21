using BusinessLayer.Models.Suggestions;
using System.Collections.Generic;

namespace SimpleCure.Models.SuggestionModels
{
    public class ViewAllSuggestions_ViewModel : ResponseBase
    {
        public List<Suggestions_Model> ListSuggestions { get; set; }
    }
}