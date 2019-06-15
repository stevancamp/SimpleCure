namespace BusinessLayer.Models.ProductGroupModels
{
    public class ProductGroup_Models : ResponseBase
    {
        public int ID { get; set; }
        public string GroupName { get; set; }
        public bool IsActive { get; set; }
    }
}
