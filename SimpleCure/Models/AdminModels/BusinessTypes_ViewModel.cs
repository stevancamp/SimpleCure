using BusinessLayer.Models.TypeModels;
using System.Collections.Generic;

namespace SimpleCure.Models.AdminModels
{
    public class BusinessTypes_ViewModel : ResponseBase
    {
        public List<BusinessType_Model> ListBusinessTypes { get; set; }
        public bool ActiveStatus { get; set; }
    }
}