using System;

namespace SimpleCure.Models.OrderModels
{
    public class PaidOrders_ViewModel
    {
        public int OrderID { get; set; }
        public string Company { get; set; }
        public string Customer { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime PaidDate { get; set; }
    }
}