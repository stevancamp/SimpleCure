using BusinessLayer.Models.OrderStatusModels;
using System.Collections.Generic;

namespace SimpleCure.Models.OrderStatusModels
{
    public class OrderStatus_ViewModel : ResponseBase
    {
        public List<OrderStatus_Models> OrderStatus { get; set; }
    }
}