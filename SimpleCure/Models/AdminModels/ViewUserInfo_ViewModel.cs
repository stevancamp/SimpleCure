using BusinessLayer.Models.CustomerModels;
using BusinessLayer.Models.LoginAttemptModels;
using System.Collections.Generic;

namespace SimpleCure.Models.AdminModels
{
    public class ViewUserInfo_ViewModel : ResponseBase
    {
        public Customers_Model CustInfoModel { get; set; }
        public ApplicationUser userInfoModel { get; set; }
        public List<AccountChangeLog_Model> AccountChangeList { get; set; }
        public List<AspNetUsersLoginAttempt_Model> LoginAttempts { get; set; }
    }
}