using System;

namespace SimpleCure.Models.OrderModels
{
    public class Orders_ViewModel
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime LastActionDate { get; set; }
    }
}