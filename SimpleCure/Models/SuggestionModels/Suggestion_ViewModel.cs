using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SimpleCure.Models.SuggestionModels
{
    public class Suggestion_ViewModel : ResponseBase
    {        
        [Required]
        [AllowHtml]
        public string SuggestionTitle { get; set; }
        [Required]
        [AllowHtml]
        public string SuggestionComments { get; set; }       
    }
}