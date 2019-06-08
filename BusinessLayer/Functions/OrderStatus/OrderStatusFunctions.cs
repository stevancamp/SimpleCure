using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.OrderStatusModels;
using Library._OrderStatus.Methods;
using System;

namespace BusinessLayer.Functions.OrderStatus
{
    public class OrderStatusFunctions : IOrderStatus
    {

        #region Injection
        private _OrderStatus _orderStatus;
        private MapOrderStatus _mapOrderStatus;
        private MapResponseBase _mapResponseBase;

        public OrderStatusFunctions()
        {
            _orderStatus = new _OrderStatus();
            _mapOrderStatus = new MapOrderStatus();
            _mapResponseBase = new MapResponseBase();

        }
        #endregion


        public ResponseBase Add(OrderStatus_Models orderStatus)
        {
            return _mapResponseBase.MapToUI(_orderStatus.Add(_mapOrderStatus.MapToLibrary(orderStatus)));
        }

        public ResponseBase Delete(int ID)
        {
            return _mapResponseBase.MapToUI(_orderStatus.Delete(ID));
        }

        public Generic<OrderStatus_Models> GetAll()
        {
            var orderStatus = _orderStatus.GetAll();
            Generic<OrderStatus_Models> model = new Generic<OrderStatus_Models>();
            model.ResponseInt = orderStatus.ResponseInt;
            model.ResponseListInt = orderStatus.ResponseListInt;
            model.ResponseListString = orderStatus.ResponseListString;
            model.ResponseMessage = orderStatus.ResponseMessage;
            model.ResponseString = orderStatus.ResponseString;
            model.ResponseSuccess = orderStatus.ResponseSuccess;
            foreach (var item in orderStatus.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderStatus.MapToUI(item));
            }
            return model;
        }

        public Generic<OrderStatus_Models> GetByID(int ID)
        {
            var orderStatus = _orderStatus.GetByID(ID);
            Generic<OrderStatus_Models> model = new Generic<OrderStatus_Models>();
            model.ResponseInt = orderStatus.ResponseInt;
            model.ResponseListInt = orderStatus.ResponseListInt;
            model.ResponseListString = orderStatus.ResponseListString;
            model.ResponseMessage = orderStatus.ResponseMessage;
            model.ResponseString = orderStatus.ResponseString;
            model.ResponseSuccess = orderStatus.ResponseSuccess;
            model.GenericClass = _mapOrderStatus.MapToUI(orderStatus.GenericClass);
            return model;
        }

        public ResponseBase Update(OrderStatus_Models orderStatus)
        {
            return _mapResponseBase.MapToUI(_orderStatus.Update(_mapOrderStatus.MapToLibrary(orderStatus)));
        }
    }
}
