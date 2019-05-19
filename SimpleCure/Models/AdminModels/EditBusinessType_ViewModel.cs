using System.ComponentModel.DataAnnotations;

namespace SimpleCure.Models.AdminModels
{
    public class EditBusinessType_ViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public string EditType { get; set; }
        public bool IsActive { get; set; }
    }
}