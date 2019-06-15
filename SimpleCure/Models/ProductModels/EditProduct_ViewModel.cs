using BusinessLayer.Models.ProductGroupModels;
using System.Collections.Generic;
using System.Web;

namespace SimpleCure.Models.ProductModels
{
    public class EditProduct_ViewModel
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Strain { get; set; }       
        public string Dominant { get; set; }
        public decimal CartGram { get; set; }
        public string ProductGroup { get; set; }
         public List<ProductGroup_Models> ProductGroupList { get; set; }
        public decimal PricePerUnit { get; set; }
        public byte[] ProductImage { get; set; }
        public HttpPostedFileBase NewProductImage { get; set; }
        public bool IsActive { get; set; }
    }
}