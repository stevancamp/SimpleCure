using BusinessLayer.Models;
using BusinessLayer.Models.LoginAttemptModels;
using System;

namespace BusinessLayer.Interface
{
    public interface ILoginAttempts
    {
        ResponseBase LogAttempt(AspNetUsersLoginAttempt_Model aspNetUsersLoginAttempt_Model);
        Generic<AspNetUsersLoginAttempt_Model> GetAllAttemptsByIDByLoginDate(string UserID, DateTime dateTime);
        ResponseBase LogAccountChange(AccountChangeLog_Model aspNetUsersLoginAttempt_Model);
        Generic<AccountChangeLog_Model> GetAllAccountChangesByIDByLoginDate(string UserID, DateTime dateTime);
    }
}
