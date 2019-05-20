using BusinessLayer.Models.TypeModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleCure.Models.AdminModels
{
    public class EditOrderInfoProductType_ViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public string OrderInfoProductType { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        public int OrderInfo_Product_Group { get; set; }
        public string OrderInfoProductSubType { get; set; }
        public List<OrderInfoProductGroups_Model> ListOrderInfoProductGroups { get; set; }
    }
}