using BusinessLayer.Models.DiscountModels;
using System.Collections.Generic;

namespace SimpleCure.Models.DiscountModels
{
    public class Discount_ViewModel : ResponseBase
    {
        public List<Discount_Models> ListDiscounts { get; set; }
    }
}