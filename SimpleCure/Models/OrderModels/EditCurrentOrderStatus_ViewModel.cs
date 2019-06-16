using BusinessLayer.Models.OrderStatusModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SimpleCure.Models.OrderModels
{
    public class EditCurrentOrderStatus_ViewModel
    {
        [Required]
        public string Status { get; set; }
        public int OrderID { get; set; }
        [AllowHtml]
        public string Notes { get; set; }
        public List<OrderStatus_Models> ListStatus { get; set; }
    }
}