using System.ComponentModel.DataAnnotations;

namespace SimpleCure.Models.AdminModels
{
    public class EditOrderInfoProductType_ViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public string OrderInfoProductType { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
    }
}