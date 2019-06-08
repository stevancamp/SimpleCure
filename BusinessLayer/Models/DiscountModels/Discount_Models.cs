namespace BusinessLayer.Models.DiscountModels
{
    public class Discount_Models
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
