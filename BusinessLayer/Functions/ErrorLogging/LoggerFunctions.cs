using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using Library.ErrorLogging.Methods;
using System;

namespace BusinessLayer.Functions.ErrorLogging
{
    public class LoggerFunctions : IApplicationErrors
    {
        #region Injection
        private ApplicationError _applicationError;
        private MapAppErrors _mapAppErrors;
        private MapResponseBase _mapResponseBase;

        public LoggerFunctions()
        {
            _applicationError = new ApplicationError();
            _mapAppErrors = new MapAppErrors();
            _mapResponseBase = new MapResponseBase();
        }
        #endregion

        public ResponseBase Log(string ErrorMessage, string IPAddress)
        {
            return _mapResponseBase.MapToUI(_applicationError.Log(ErrorMessage, IPAddress));
        }

        public Generic<ErrorModel> GetAll()
        {
            var Logs = _applicationError.GetAll();
            Generic<ErrorModel> model = new Generic<ErrorModel>();
            model.ResponseInt = Logs.ResponseInt;
            model.ResponseListInt = Logs.ResponseListInt;
            model.ResponseListString = Logs.ResponseListString;
            model.ResponseMessage = Logs.ResponseMessage;
            model.ResponseString = Logs.ResponseString;
            model.ResponseSuccess = Logs.ResponseSuccess;
            foreach (var item in Logs.GenericClassList)
            {
                model.GenericClassList.Add(_mapAppErrors.MapToUI(item));
            }
            return model;
        }

        public Generic<ErrorModel> GetByID(int ID)
        {
            Generic<ErrorModel> model = new Generic<ErrorModel>();
            var Log = _applicationError.GetByID(ID);
            model.ResponseInt = Log.ResponseInt;
            model.ResponseListInt = Log.ResponseListInt;
            model.ResponseListString = Log.ResponseListString;
            model.ResponseMessage = Log.ResponseMessage;
            model.ResponseString = Log.ResponseString;
            model.ResponseSuccess = Log.ResponseSuccess;
            model.GenericClass = _mapAppErrors.MapToUI(Log.GenericClass);
            return model;
        }

        public Generic<ErrorModel> GetByDate(DateTime Date)
        {
            var Logs = _applicationError.GetByDate(Date);            
            Generic<ErrorModel> model = new Generic<ErrorModel>();
            model.ResponseInt = Logs.ResponseInt;
            model.ResponseListInt = Logs.ResponseListInt;
            model.ResponseListString = Logs.ResponseListString;
            model.ResponseMessage = Logs.ResponseMessage;
            model.ResponseString = Logs.ResponseString;
            model.ResponseSuccess = Logs.ResponseSuccess;
            foreach (var item in Logs.GenericClassList)
            {
                model.GenericClassList.Add(_mapAppErrors.MapToUI(item));
            }

            return model;
        }
    }
}
