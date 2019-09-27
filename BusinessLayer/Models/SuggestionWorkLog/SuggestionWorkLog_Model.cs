using System;

namespace BusinessLayer.Models.SuggestionWorkLog
{
    public class SuggestionWorkLog_Model
    {
        public int ID { get; set; }
        public int SuggestionID { get; set; }
        public string Comment { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDateTime { get; set; }
    }
}
