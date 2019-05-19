using BusinessLayer.Models.OrderModels;
using BusinessLayer.Models.TypeModels;
using System.Collections.Generic;

namespace SimpleCure.Models.OrderModels
{
    public class NewOrder_ViewModel : ResponeBase
    {
        public Order_Model OrderModel { get; set; }
        public List<OrderInfoProductTypes_Model> OrderInfoProductTypesModel { get; set; }
    }
}