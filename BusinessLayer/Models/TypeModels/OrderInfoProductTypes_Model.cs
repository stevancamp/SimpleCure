namespace BusinessLayer.Models.TypeModels
{
    public class OrderInfoProductTypes_Model
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public int OrderInfo_Product_Group { get; set; }
    }
}