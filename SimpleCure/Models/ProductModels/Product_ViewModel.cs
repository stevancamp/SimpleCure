using BusinessLayer.Models.ProductModels;
using System.Collections.Generic;

namespace SimpleCure.Models.ProductModels
{
    public class Product_ViewModel : ResponseBase
    {
        public List<Product_Models> ListProducts { get; set; }
    }
}