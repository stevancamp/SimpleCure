using System;

namespace BusinessLayer.Models.OrderModels
{
    public class Order_Models
    {
        public int ID { get; set; }
        public int Tbl_CustomerID { get; set; }
        public string Notes { get; set; }
        public DateTime SubmissionDate { get; set; }
        public int OrderStatus { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
