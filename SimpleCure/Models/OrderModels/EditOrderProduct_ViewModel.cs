using BusinessLayer.Models.OrderProductsModels;
using BusinessLayer.Models.ProductModels;
using System.Collections.Generic;

namespace SimpleCure.Models.OrderModels
{
    public class EditOrderProduct_ViewModel : ResponseBase
    {
        public List<Product_Models> ListProducts { get; set; }
        public OrderProducts_Models OrderProductModel { get; set; }
        public Product_Models ProductModel { get; set; }
    }
}