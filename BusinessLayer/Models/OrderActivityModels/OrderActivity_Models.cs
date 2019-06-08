using System;

namespace BusinessLayer.Models.OrderActivityModels
{
    public class OrderActivity_Models
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityBy { get; set; }
    }
}
