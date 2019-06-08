using System;

namespace BusinessLayer.Models.OrderProductsModels
{
    public class OrderProducts_Models
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int? BatchID { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
