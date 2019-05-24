namespace BusinessLayer.Models.TypeModels
{
    public class Order_Info_Product_Types_With_GroupName_Model
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public int OrderInfo_Product_Group { get; set; }
        public string GroupName { get; set; }
        public string Type_SubHeader { get; set; }
        public byte[] ProductImage { get; set; }
    }
}
