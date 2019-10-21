using BusinessLayer.Models.SuggestionStatus;
using BusinessLayer.Models.SuggestionWorkLog;
using System.Collections.Generic;

namespace SimpleCure.Models.SuggestionModels
{
    public class ViewSuggestionWorkLogsBySuggestionID_ViewModel : ResponseBase
    {
        public List<SuggestionWorkLog_Model> ListSuggestionWorkLog { get; set; }
        public List<SuggestionStatus_Model> ListStatus { get; set; }
    }
}