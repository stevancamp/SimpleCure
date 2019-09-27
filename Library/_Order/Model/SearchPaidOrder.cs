using System;

namespace Library._Order.Model
{
    public class SearchPaidOrder
    {
        public int OrderID { get; set; }
        public string Company { get; set; }
        public string Customer { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime PaidDate { get; set; }
    }
}
