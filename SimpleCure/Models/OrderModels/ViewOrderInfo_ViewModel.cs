using BusinessLayer.Models.CustomerModels;
using BusinessLayer.Models.OrderActivityModels;
using BusinessLayer.Models.OrderModels;
using System;
using System.Collections.Generic;

namespace SimpleCure.Models.OrderModels
{
    public class ViewOrderInfo_ViewModel
    {        
        //public string CustomerName { get; set; }
        //public string CompanyName { get; set; }
        //public DateTime OrderDate { get; set; }
        //public string OrderStatus { get; set; }
        public Customers_Model CustomerInfo { get; set; }
        public List<OrderActivity_Models> OrderActivity { get; set; }
        public Order_Models OrderInfo { get; set; }
    }
}