using BusinessLayer.Models.TypeModels;
using System.Collections.Generic;

namespace SimpleCure.Models.AdminModels
{
    public class OrderInfoProductType_ViewModel : ResponeBase
    {
        public List<OrderInfoProductTypes_Model> ListOrderInfoProductTypes { get; set; }
        public bool ActiveStatus { get; set; }
    }
}