using System;

namespace BusinessLayer.Models.OrderModels
{
    public class Order_Models
    {
        public int ID { get; set; }
        public int Tbl_CustomerID { get; set; }
        public string Notes { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string TransportID { get; set; }
        public bool IsSimpleCure { get; set; }
        public string TransportLocationStart { get; set; }
        public string TransportLocationEnd { get; set; }
        public string To_From { get; set; }
    }
}
