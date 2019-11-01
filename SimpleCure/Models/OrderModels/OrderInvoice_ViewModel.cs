using BusinessLayer.Models.CustomerModels;
using BusinessLayer.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleCure.Models.OrderModels
{
    public class OrderInvoice_ViewModel
    {
        public Order_Models OrderInfo { get; set; }
        public Customers_Model CustomerInfo { get; set; }
        public List<OrderProduct_Prodcut_Model> ListProducts { get; set; }
        public List<OrderDiscountDiscounts_Model> ListDiscounts { get; set; }
    }
}