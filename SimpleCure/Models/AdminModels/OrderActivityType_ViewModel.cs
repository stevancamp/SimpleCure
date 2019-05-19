using BusinessLayer.Models.TypeModels;
using System.Collections.Generic;

namespace SimpleCure.Models.AdminModels
{
    public class OrderActivityType_ViewModel : ResponeBase
    {
        public List<OrderActivityType_Model> ListOrderActivityTypes { get; set; }
        public bool ActiveStatus { get; set; }
    }
}