using System;

namespace BusinessLayer.Models.OrderModels
{
    public class OrderActivityHistory_Model
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int OrderActivityTypeID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Notes { get; set; }
    }
}