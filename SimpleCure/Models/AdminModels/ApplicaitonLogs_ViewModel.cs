using BusinessLayer.Models;
using System.Collections.Generic;

namespace SimpleCure.Models.AdminModels
{
    public class ApplicaitonLogs_ViewModel : ResponeBase
    {
        public List<ErrorModel> ListLogs { get; set; }
    }
}