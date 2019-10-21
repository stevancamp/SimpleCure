using System;

namespace BusinessLayer.Models.Suggestions
{
    public class Suggestions_Model
    {
        public int ID { get; set; }
        public string SuggestionTitle { get; set; }
        public string SuggestionComments { get; set; }
        public string Status { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsActive { get; set; }
    }
}
