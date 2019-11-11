using System;

namespace SimpleCure.Models.OrderModels
{
    public class OrderProduct_Prodcut_Model
    {
        public int ProductID { get; set; }
        public string Type { get; set; }
        public string ProductDescription { get; set; }
        public string Strain { get; set; }
        public string Dominant { get; set; }
        public decimal CartGram { get; set; }
        public string ProductGroup { get; set; }
        public decimal PricePerUnit { get; set; }
        public byte[] ProductImage { get; set; }
        public bool IsActive { get; set; }
        public int OrderProductID { get; set; }
        public int OrderID { get; set; }
        public int ForProductID { get; set; }
        public string BatchID { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string Status { get; set; }
        public string OrderProductDescription { get; set; }
    }
}