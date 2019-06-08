using BusinessLayer.Models;
using System.Collections.Generic;

namespace SimpleCure.Models.AdminModels
{
    public class ApplicaitonLogs_ViewModel : ResponseBase
    {
        public List<ErrorModel> ListLogs { get; set; }
    }
}