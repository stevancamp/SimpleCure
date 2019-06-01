using BusinessLayer.Models.OrderModels;
using System.Collections.Generic;

namespace SimpleCure.Models.OrderModels
{
    public class ViewOrdersViewModel : ResponeBase
    {
        public List<Order_Model> ListOrders { get; set; }
        public string SearchTerm { get; set; }
    }
}