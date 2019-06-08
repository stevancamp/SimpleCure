using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.DiscountModels;
using Library._Discount.Methods;
using System;

namespace BusinessLayer.Functions.Discount
{
    public class DiscountFunctions : IDiscount
    {

        #region Injection
        private _Discount _discount;
        private MapDiscount _mapDiscount;
        private MapResponseBase _mapResponseBase;

        public DiscountFunctions()
        {
            _discount = new _Discount();
            _mapDiscount = new MapDiscount();
            _mapResponseBase = new MapResponseBase();

        }
        #endregion

        public ResponseBase Add(Discount_Models discount)
        {
            return _mapResponseBase.MapToUI(_discount.Add(_mapDiscount.MapToLibrary(discount)));
        }

        public ResponseBase Delete(int ID)
        {
            return _mapResponseBase.MapToUI(_discount.Delete(ID));
        }

        public Generic<Discount_Models> GetAll()
        {
            var discount = _discount.GetAll();
            Generic<Discount_Models> model = new Generic<Discount_Models>();
            model.ResponseInt = discount.ResponseInt;
            model.ResponseListInt = discount.ResponseListInt;
            model.ResponseListString = discount.ResponseListString;
            model.ResponseMessage = discount.ResponseMessage;
            model.ResponseString = discount.ResponseString;
            model.ResponseSuccess = discount.ResponseSuccess;
            foreach (var item in discount.GenericClassList)
            {
                model.GenericClassList.Add(_mapDiscount.MapToUI(item));
            }
            return model;
        }

        public Generic<Discount_Models> GetAllByIsActive(bool IsActive)
        {
            var discount = _discount.GetAllByIsActive(IsActive);
            Generic<Discount_Models> model = new Generic<Discount_Models>();
            model.ResponseInt = discount.ResponseInt;
            model.ResponseListInt = discount.ResponseListInt;
            model.ResponseListString = discount.ResponseListString;
            model.ResponseMessage = discount.ResponseMessage;
            model.ResponseString = discount.ResponseString;
            model.ResponseSuccess = discount.ResponseSuccess;
            foreach (var item in discount.GenericClassList)
            {
                model.GenericClassList.Add(_mapDiscount.MapToUI(item));
            }
            return model;
        }

        public Generic<Discount_Models> GetByID(int ID)
        {
            var discount = _discount.GetByID(ID);
            Generic<Discount_Models> model = new Generic<Discount_Models>();
            model.ResponseInt = discount.ResponseInt;
            model.ResponseListInt = discount.ResponseListInt;
            model.ResponseListString = discount.ResponseListString;
            model.ResponseMessage = discount.ResponseMessage;
            model.ResponseString = discount.ResponseString;
            model.ResponseSuccess = discount.ResponseSuccess;
            model.GenericClass = _mapDiscount.MapToUI(discount.GenericClass);
            return model;
        }

        public ResponseBase Update(Discount_Models discount)
        {
            return _mapResponseBase.MapToUI(_discount.Update(_mapDiscount.MapToLibrary(discount)));
        }
    }
}
