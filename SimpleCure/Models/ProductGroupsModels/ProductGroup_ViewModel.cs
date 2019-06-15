using BusinessLayer.Models.ProductGroupModels;
using System.Collections.Generic;

namespace SimpleCure.Models.ProductGroupsModels
{
    public class ProductGroup_ViewModel :ResponseBase
    {
        public List<ProductGroup_Models> ListProductGroups { get; set; }
    }
}