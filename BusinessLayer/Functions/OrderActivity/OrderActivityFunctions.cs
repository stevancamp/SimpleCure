using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.OrderActivityModels;
using Library._OrderActivity.Methods;
using System;

namespace BusinessLayer.Functions.OrderActivity
{
    public class OrderActivityFunctions : IOrderActivity
    {

        #region Injection
        private _OrderActivity _orderActivity;
        private MapOrderActivity _mapOrderActivity;
        private MapResponseBase _mapResponseBase;

        public OrderActivityFunctions()
        {
            _orderActivity = new _OrderActivity();
            _mapOrderActivity = new MapOrderActivity();
            _mapResponseBase = new MapResponseBase();

        }
        #endregion


        public ResponseBase Add(OrderActivity_Models OrderActivity)
        {
            return _mapResponseBase.MapToUI(_orderActivity.Add(_mapOrderActivity.MapToLibrary(OrderActivity)));
        }

        public ResponseBase Delete(int ID)
        {
            return _mapResponseBase.MapToUI(_orderActivity.Delete(ID));
        }

        public Generic<OrderActivity_Models> GetAll()
        {
            var OrderActivity = _orderActivity.GetAll();
            Generic<OrderActivity_Models> model = new Generic<OrderActivity_Models>();
            model.ResponseInt = OrderActivity.ResponseInt;
            model.ResponseListInt = OrderActivity.ResponseListInt;
            model.ResponseListString = OrderActivity.ResponseListString;
            model.ResponseMessage = OrderActivity.ResponseMessage;
            model.ResponseString = OrderActivity.ResponseString;
            model.ResponseSuccess = OrderActivity.ResponseSuccess;
            foreach (var item in OrderActivity.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderActivity.MapToUI(item));
            }
            return model;
        }

        public Generic<OrderActivity_Models> GetByID(int ID)
        {
            var OrderActivity = _orderActivity.GetByID(ID);
            Generic<OrderActivity_Models> model = new Generic<OrderActivity_Models>();
            model.ResponseInt = OrderActivity.ResponseInt;
            model.ResponseListInt = OrderActivity.ResponseListInt;
            model.ResponseListString = OrderActivity.ResponseListString;
            model.ResponseMessage = OrderActivity.ResponseMessage;
            model.ResponseString = OrderActivity.ResponseString;
            model.ResponseSuccess = OrderActivity.ResponseSuccess;
            model.GenericClass = _mapOrderActivity.MapToUI(OrderActivity.GenericClass);
            return model;
        }

        public Generic<OrderActivity_Models> GetByOrderID(int OrderID)
        {
            var OrderActivity = _orderActivity.GetByOrderID(OrderID);
            Generic<OrderActivity_Models> model = new Generic<OrderActivity_Models>();
            model.ResponseInt = OrderActivity.ResponseInt;
            model.ResponseListInt = OrderActivity.ResponseListInt;
            model.ResponseListString = OrderActivity.ResponseListString;
            model.ResponseMessage = OrderActivity.ResponseMessage;
            model.ResponseString = OrderActivity.ResponseString;
            model.ResponseSuccess = OrderActivity.ResponseSuccess;
            foreach (var item in OrderActivity.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderActivity.MapToUI(item));
            }
            return model;
        }

        public ResponseBase Update(OrderActivity_Models OrderActivity)
        {
            return _mapResponseBase.MapToUI(_orderActivity.Update(_mapOrderActivity.MapToLibrary(OrderActivity)));
        }
    }
}
