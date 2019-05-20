using BusinessLayer.Models.OrderModels;
using BusinessLayer.Models.TypeModels;
using System.Collections.Generic;

namespace SimpleCure.Models.OrderModels
{
    public class NewOrder_ViewModel : ResponeBase
    {
        public Order_Model OrderModel { get; set; }
        public List<Order_Info_Product_Types_With_GroupName_Model> OrderInfoProductTypesModel { get; set; }
        public List<BusinessType_Model> ListBusinessTypes { get; set; }
    }
}