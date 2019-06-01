using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.LoginAttemptModels;
using Library.LoginLogging.Methods;
using System;

namespace BusinessLayer.Functions.LoginLogger
{
    public class LoginAttemptFunctions : ILoginAttempts
    {
        #region Injection

        private UserLoginAttempts _userLoginAttempts;
        private ChangeLogAccount _changeLogAccount;
        private MapLoginAttempts _mapLoginAttempts;
        private MapChangeLogAccount _mapChangeLogAccount;
        private MapResponseBase _mapResponseBase;

        public LoginAttemptFunctions()
        {
            _userLoginAttempts = new UserLoginAttempts();
            _changeLogAccount = new ChangeLogAccount();
            _mapLoginAttempts = new MapLoginAttempts();
            _mapChangeLogAccount = new MapChangeLogAccount();
            _mapResponseBase = new MapResponseBase();           
        }
        #endregion
        public Generic<AspNetUsersLoginAttempt_Model> GetAllAttemptsByIDByLoginDate(string UserID, DateTime dateTime)
        {
            var Attempts = _userLoginAttempts.GetByUserIDByDate(UserID, dateTime);
            Generic<AspNetUsersLoginAttempt_Model> model = new Generic<AspNetUsersLoginAttempt_Model>();
            model.ResponseInt = Attempts.ResponseInt;
            model.ResponseListInt = Attempts.ResponseListInt;
            model.ResponseListString = Attempts.ResponseListString;
            model.ResponseMessage = Attempts.ResponseMessage;
            model.ResponseString = Attempts.ResponseString;
            model.ResponseSuccess = Attempts.ResponseSuccess;
            foreach (var item in Attempts.GenericClassList)
            {
                model.GenericClassList.Add(_mapLoginAttempts.MapToUI(item));
            }
            return model;
        }
        public ResponseBase LogAttempt(AspNetUsersLoginAttempt_Model aspNetUsersLoginAttempt_Model)
        {
            return _mapResponseBase.MapToUI(_userLoginAttempts.Add(_mapLoginAttempts.MapToLibrary(aspNetUsersLoginAttempt_Model)));
        }
        public ResponseBase LogAccountChange(AccountChangeLog_Model accountChangeLog_Model)
        {
            return _mapResponseBase.MapToUI(_changeLogAccount.Add(_mapChangeLogAccount.MapToLibrary(accountChangeLog_Model)));
        }
        public Generic<AccountChangeLog_Model> GetAllAccountChangesByIDByLoginDate(string UserID, DateTime dateTime)
        {
            var Attempts = _changeLogAccount.GetByUserIDByDate(UserID, dateTime);
            Generic<AccountChangeLog_Model> model = new Generic<AccountChangeLog_Model>();
            model.ResponseInt = Attempts.ResponseInt;
            model.ResponseListInt = Attempts.ResponseListInt;
            model.ResponseListString = Attempts.ResponseListString;
            model.ResponseMessage = Attempts.ResponseMessage;
            model.ResponseString = Attempts.ResponseString;
            model.ResponseSuccess = Attempts.ResponseSuccess;
            foreach (var item in Attempts.GenericClassList)
            {
                model.GenericClassList.Add(_mapChangeLogAccount.MapToUI(item));
            }
            return model;
        }           
    }
}
