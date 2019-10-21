using System;

namespace SimpleCure.Models.SuggestionModels
{
    public class EnterSuggestionWorkLog_ViewModel
    {       
        public int SuggestionID { get; set; }
        public string Comment { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }      
        public string SuggestionStatus { get; set; }
        public bool IsComplete { get; set; }
    }
}