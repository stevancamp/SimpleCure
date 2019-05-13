using BusinessLayer.Mappings;
using Library;
using Library.ErrorLogging.Methods;
using SimpleCure.Models;
using System;

namespace BusinessLayer.Functions.ErrorLogging
{
    public class Functions
    {
        #region Injection
        private ApplicationError _applicationError;
        private MapAppErrors _mapAppErrors;
        public Functions()
        {
            _applicationError = new ApplicationError();
            _mapAppErrors = new MapAppErrors();
        }
        #endregion

        public ResponseBase Log(string ErrorMessage, string IPAddress)
        {
            return _applicationError.Log(ErrorMessage, IPAddress);
        }

        public Generic<ErrorModel> GetAll()
        {
            var Logs = _applicationError.GetAll();
            Generic<ErrorModel> model = new Generic<ErrorModel>();
            foreach (var item in Logs.GenericClassList)
            {
                model.GenericClassList.Add(_mapAppErrors.MapToUI(item));
            }
            return model;
        }

        public Generic<ErrorModel> GetByID(int ID)
        {            
            Generic<ErrorModel> model = new Generic<ErrorModel>();
            model.GenericClass = _mapAppErrors.MapToUI(_applicationError.GetByID(ID).GenericClass);
            return model;
        }

        public Generic<ErrorModel> GetByDate(DateTime Date)
        {
            var Logs = _applicationError.GetByDate(Date);
            Generic<ErrorModel> model = new Generic<ErrorModel>();
            foreach (var item in Logs.GenericClassList)
            {
                model.GenericClassList.Add(_mapAppErrors.MapToUI(item));
            }

            return model;
        }
    }
}
