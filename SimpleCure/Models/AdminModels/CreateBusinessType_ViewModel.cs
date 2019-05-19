using System.ComponentModel.DataAnnotations;

namespace SimpleCure.Models.AdminModels
{
    public class CreateBusinessType_ViewModel
    {
        [Required(ErrorMessage = "Type is required")]
        public string CreateType { get; set; }      
    }
}