using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.OrderDiscountModels;
using Library._OrderDiscount.Methods;
using System;

namespace BusinessLayer.Functions.OrderDiscount
{
    public class OrderDiscountFunctions : IOrderDiscount
    {

        #region Injection
        private _OrderDiscount _orderDiscount;
        private MapOrderDiscount _mapOrderDiscount;
        private MapResponseBase _mapResponseBase;

        public OrderDiscountFunctions()
        {
            _orderDiscount = new _OrderDiscount();
            _mapOrderDiscount = new MapOrderDiscount();
            _mapResponseBase = new MapResponseBase();

        }
        #endregion


        public ResponseBase Add(OrderDiscount_Models OrderDiscount)
        {
            return _mapResponseBase.MapToUI(_orderDiscount.Add(_mapOrderDiscount.MapToLibrary(OrderDiscount)));
        }

        public ResponseBase Delete(int ID)
        {
            return _mapResponseBase.MapToUI(_orderDiscount.Delete(ID));
        }

        public Generic<OrderDiscount_Models> GetAll()
        {
            var OrderDiscount = _orderDiscount.GetAll();
            Generic<OrderDiscount_Models> model = new Generic<OrderDiscount_Models>();
            model.ResponseInt = OrderDiscount.ResponseInt;
            model.ResponseListInt = OrderDiscount.ResponseListInt;
            model.ResponseListString = OrderDiscount.ResponseListString;
            model.ResponseMessage = OrderDiscount.ResponseMessage;
            model.ResponseString = OrderDiscount.ResponseString;
            model.ResponseSuccess = OrderDiscount.ResponseSuccess;
            foreach (var item in OrderDiscount.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderDiscount.MapToUI(item));
            }
            return model;
        }

        public Generic<OrderDiscount_Models> GetAllByOrderID(int OrderID)
        {
            var OrderDiscount = _orderDiscount.GetAllByOrderID(OrderID);
            Generic<OrderDiscount_Models> model = new Generic<OrderDiscount_Models>();
            model.ResponseInt = OrderDiscount.ResponseInt;
            model.ResponseListInt = OrderDiscount.ResponseListInt;
            model.ResponseListString = OrderDiscount.ResponseListString;
            model.ResponseMessage = OrderDiscount.ResponseMessage;
            model.ResponseString = OrderDiscount.ResponseString;
            model.ResponseSuccess = OrderDiscount.ResponseSuccess;
            foreach (var item in OrderDiscount.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderDiscount.MapToUI(item));
            }
            return model;
        }

        public Generic<OrderDiscount_Models> GetByID(int ID)
        {
            var OrderDiscount = _orderDiscount.GetByID(ID);
            Generic<OrderDiscount_Models> model = new Generic<OrderDiscount_Models>();
            model.ResponseInt = OrderDiscount.ResponseInt;
            model.ResponseListInt = OrderDiscount.ResponseListInt;
            model.ResponseListString = OrderDiscount.ResponseListString;
            model.ResponseMessage = OrderDiscount.ResponseMessage;
            model.ResponseString = OrderDiscount.ResponseString;
            model.ResponseSuccess = OrderDiscount.ResponseSuccess;
            model.GenericClass = _mapOrderDiscount.MapToUI(OrderDiscount.GenericClass);
            return model;
        }

        public ResponseBase Update(OrderDiscount_Models OrderDiscount)
        {
            return _mapResponseBase.MapToUI(_orderDiscount.Update(_mapOrderDiscount.MapToLibrary(OrderDiscount)));
        }
    }
}
