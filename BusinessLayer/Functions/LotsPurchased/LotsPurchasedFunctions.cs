using System;
using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.LotsPurchasedModels;
using Library._LotsPurchased.Methods;

namespace BusinessLayer.Functions.LotsPurchased
{
    public class LotsPurchasedFunctions : ILotsPurchased
    {
        #region Injection
        private _LotsPurchased _lotsPurchased;
        private MapLotsPurchased _mapLotsPurchased;
        private MapResponseBase _mapResponseBase;

        public LotsPurchasedFunctions()
        {
            _lotsPurchased = new _LotsPurchased();
            _mapLotsPurchased = new MapLotsPurchased();
            _mapResponseBase = new MapResponseBase();

        }
        #endregion

        public ResponseBase Add(LotsPurchased_Models purchased)
        {           
            return _mapResponseBase.MapToUI(_lotsPurchased.Add(_mapLotsPurchased.MapToLibrary(purchased)));
        }

        public ResponseBase Delete(int ID)
        {
            return _mapResponseBase.MapToUI(_lotsPurchased.Delete(ID));
        }

        public Generic<LotsPurchased_Models> GetAll()
        {
            var Customers = _lotsPurchased.GetAll();
            Generic<LotsPurchased_Models> model = new Generic<LotsPurchased_Models>();
            model.ResponseInt = Customers.ResponseInt;
            model.ResponseListInt = Customers.ResponseListInt;
            model.ResponseListString = Customers.ResponseListString;
            model.ResponseMessage = Customers.ResponseMessage;
            model.ResponseString = Customers.ResponseString;
            model.ResponseSuccess = Customers.ResponseSuccess;
            foreach (var item in Customers.GenericClassList)
            {
                model.GenericClassList.Add(_mapLotsPurchased.MapToUI(item));
            }
            return model;
        }

        public Generic<LotsPurchased_Models> GetByComplete(bool IsComplete)
        {
            var Customers = _lotsPurchased.GetByComplete(IsComplete);
            Generic<LotsPurchased_Models> model = new Generic<LotsPurchased_Models>();
            model.ResponseInt = Customers.ResponseInt;
            model.ResponseListInt = Customers.ResponseListInt;
            model.ResponseListString = Customers.ResponseListString;
            model.ResponseMessage = Customers.ResponseMessage;
            model.ResponseString = Customers.ResponseString;
            model.ResponseSuccess = Customers.ResponseSuccess;
            foreach (var item in Customers.GenericClassList)
            {
                model.GenericClassList.Add(_mapLotsPurchased.MapToUI(item));
            }
            return model;
        }

        public Generic<LotsPurchased_Models> GetByCompleteByRange(DateTime StartTime, DateTime EndTime, bool IsComplete)
        {
            var Customers = _lotsPurchased.GetByCompleteByRange(StartTime, EndTime, IsComplete);
            Generic<LotsPurchased_Models> model = new Generic<LotsPurchased_Models>();
            model.ResponseInt = Customers.ResponseInt;
            model.ResponseListInt = Customers.ResponseListInt;
            model.ResponseListString = Customers.ResponseListString;
            model.ResponseMessage = Customers.ResponseMessage;
            model.ResponseString = Customers.ResponseString;
            model.ResponseSuccess = Customers.ResponseSuccess;
            foreach (var item in Customers.GenericClassList)
            {
                model.GenericClassList.Add(_mapLotsPurchased.MapToUI(item));
            }
            return model;
        }

        public Generic<LotsPurchased_Models> GetByID(int ID)
        {
            var lotsPurchased = _lotsPurchased.GetByID(ID);
            Generic<LotsPurchased_Models> model = new Generic<LotsPurchased_Models>();
            model.ResponseInt = lotsPurchased.ResponseInt;
            model.ResponseListInt = lotsPurchased.ResponseListInt;
            model.ResponseListString = lotsPurchased.ResponseListString;
            model.ResponseMessage = lotsPurchased.ResponseMessage;
            model.ResponseString = lotsPurchased.ResponseString;
            model.ResponseSuccess = lotsPurchased.ResponseSuccess;
            model.GenericClass = _mapLotsPurchased.MapToUI(lotsPurchased.GenericClass) ?? new LotsPurchased_Models();
            return model;
        }

        public Generic<LotsPurchased_Models> GetByLotSet(string LotSet)
        {
            var Customers = _lotsPurchased.GetByLotSet(LotSet);
            Generic<LotsPurchased_Models> model = new Generic<LotsPurchased_Models>();
            model.ResponseInt = Customers.ResponseInt;
            model.ResponseListInt = Customers.ResponseListInt;
            model.ResponseListString = Customers.ResponseListString;
            model.ResponseMessage = Customers.ResponseMessage;
            model.ResponseString = Customers.ResponseString;
            model.ResponseSuccess = Customers.ResponseSuccess;
            foreach (var item in Customers.GenericClassList)
            {
                model.GenericClassList.Add(_mapLotsPurchased.MapToUI(item));
            }
            return model;
        }

        public Generic<LotsPurchased_Models> GetByProvider(int Provider)
        {
            var Customers = _lotsPurchased.GetByProvider(Provider);
            Generic<LotsPurchased_Models> model = new Generic<LotsPurchased_Models>();
            model.ResponseInt = Customers.ResponseInt;
            model.ResponseListInt = Customers.ResponseListInt;
            model.ResponseListString = Customers.ResponseListString;
            model.ResponseMessage = Customers.ResponseMessage;
            model.ResponseString = Customers.ResponseString;
            model.ResponseSuccess = Customers.ResponseSuccess;
            foreach (var item in Customers.GenericClassList)
            {
                model.GenericClassList.Add(_mapLotsPurchased.MapToUI(item));
            }
            return model;
        }

        public ResponseBase Update(LotsPurchased_Models purchased)
        {
            return _mapResponseBase.MapToUI(_lotsPurchased.Update(_mapLotsPurchased.MapToLibrary(purchased)));
        }
    }
}
