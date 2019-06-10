namespace SimpleCure.Models.DiscountModels
{
    public class EditDiscount_ViewModel
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}