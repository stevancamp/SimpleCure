using System.ComponentModel.DataAnnotations;

namespace SimpleCure.Models.OrderStatusModels
{
    public class CreateOrderStatus_ViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Status { get; set; }
    }
}