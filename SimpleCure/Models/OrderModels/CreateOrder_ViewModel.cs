using BusinessLayer.Models.CustomerModels;
using BusinessLayer.Models.DiscountModels;
using BusinessLayer.Models.ProductModels;
using System.Collections.Generic;

namespace SimpleCure.Models.OrderModels
{
    public class CreateOrder_ViewModel : ResponseBase
    {
        public List<Product_Models> ListProducts { get; set; }
        public List<Discount_Models> ListDiscounts { get; set; }
        public List<CustomersLite_Model> ListCustomers { get; set; }
        public List<CartOrder_ViewModel> ListCartItems { get; set; }
        public List<ProductsList> ListProductsToSubmit { get; set; }
        public List<DiscountIDList> ListDiscountIDs { get; set; }
        public string OrderNotes { get; set; }
        public string CustomerID { get; set; }
    }

    public class ProductsList
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
    public class DiscountIDList
    {
        public int DiscountID { get; set; }
    }
}