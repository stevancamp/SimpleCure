using BusinessLayer.Models.TypeModels;
using System.Collections.Generic;

namespace SimpleCure.Models.AdminModels
{
    public class OrderInfoProductType_ViewModel : ResponeBase
    {
        public List<Order_Info_Product_Types_With_GroupName_Model> ListOrderInfoProductTypes { get; set; }
        public List<OrderInfoProductGroups_Model> ListOrderInfoProductGroups { get; set; }
        public bool ActiveStatus { get; set; }
    }
}