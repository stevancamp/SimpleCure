using BusinessLayer.Models.OrderModels;
using BusinessLayer.Models.OrderStatusModels;
using System.Collections.Generic;

namespace SimpleCure.Models.OrderModels
{
    public class ViewOrder_ViewModel : ResponseBase
    {
        public Order_Models OrderInfo { get; set; }
        public List<ViewOrderProductList> ProductList { get; set; }
        public List<OrderProduct_Prodcut_Model> ListProducts { get; set; }
        public List<OrderStatus_Models> ListStatus { get; set; }
        public List<OrderDiscountDiscounts_Model> ListOrderDiscounts { get; set; }
        public string Status { get; set; }       
        public string Notes { get; set; }
        public decimal OrderTotal { get; set; }
    }  
}