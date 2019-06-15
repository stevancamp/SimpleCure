namespace SimpleCure.Models.OrderModels
{
    public class ViewOrderProductList
    {
        public int BatchID { get; set; }
        public int ProductQuantity { get; set; }
        public decimal Total { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Strain { get; set; }
        public string Dominant { get; set; }
        public decimal CartGram { get; set; }
        public string ProductGroup { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}