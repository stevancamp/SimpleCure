namespace SimpleCure.Models.OrderModels
{
    public class OrderDiscountDiscounts_Model
    {
        public int DiscountID { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public decimal DiscountAmount { get; set; }
        public int OrderDiscountID { get; set; }
        public int OrderID { get; set; }
        public int OrderDiscountDiscountID { get; set; }
    }
}