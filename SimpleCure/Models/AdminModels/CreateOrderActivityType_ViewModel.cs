using System.ComponentModel.DataAnnotations;

namespace SimpleCure.Models.AdminModels
{
    public class CreateOrderActivityType_ViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public string OrderActivityType { get; set; }
        public bool IsActive { get; set; }
    }
}