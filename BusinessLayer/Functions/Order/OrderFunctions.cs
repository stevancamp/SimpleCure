using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.OrderModels;
using Library._Order.Methods;
using System;

namespace BusinessLayer.Functions.Order
{
    public class OrderFunctions : IOrder
    {
        #region Injection
        private _Order _order;
        private MapOrder _mapOrder;
        private MapResponseBase _mapResponseBase;

        public OrderFunctions()
        {
            _order = new _Order();
            _mapOrder = new MapOrder();
            _mapResponseBase = new MapResponseBase();

        }
        #endregion
        public ResponseBase Add(Order_Models Order)
        {
            return _mapResponseBase.MapToUI(_order.Add(_mapOrder.MapToLibrary(Order)));
        }

        public ResponseBase Delete(int ID)
        {
            return _mapResponseBase.MapToUI(_order.Delete(ID));
        }

        public Generic<Order_Models> GetAll()
        {
            var Order = _order.GetAll();
            Generic<Order_Models> model = new Generic<Order_Models>();
            model.ResponseInt = Order.ResponseInt;
            model.ResponseListInt = Order.ResponseListInt;
            model.ResponseListString = Order.ResponseListString;
            model.ResponseMessage = Order.ResponseMessage;
            model.ResponseString = Order.ResponseString;
            model.ResponseSuccess = Order.ResponseSuccess;
            foreach (var item in Order.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrder.MapToUI(item));
            }
            return model;
        }

        public Generic<Order_Models> GetAllByCompleted(bool Completed)
        {
            var Order = _order.GetAllByCompleted(Completed);
            Generic<Order_Models> model = new Generic<Order_Models>();
            model.ResponseInt = Order.ResponseInt;
            model.ResponseListInt = Order.ResponseListInt;
            model.ResponseListString = Order.ResponseListString;
            model.ResponseMessage = Order.ResponseMessage;
            model.ResponseString = Order.ResponseString;
            model.ResponseSuccess = Order.ResponseSuccess;
            foreach (var item in Order.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrder.MapToUI(item));
            }
            return model;
        }

        public Generic<Order_Models> GetByCustomerID(int CustomerID)
        {
            var Order = _order.GetByCustomerID(CustomerID);
            Generic<Order_Models> model = new Generic<Order_Models>();
            model.ResponseInt = Order.ResponseInt;
            model.ResponseListInt = Order.ResponseListInt;
            model.ResponseListString = Order.ResponseListString;
            model.ResponseMessage = Order.ResponseMessage;
            model.ResponseString = Order.ResponseString;
            model.ResponseSuccess = Order.ResponseSuccess;
            foreach (var item in Order.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrder.MapToUI(item));
            }
            return model;
        }

        public Generic<Order_Models> GetByID(int ID)
        {
            var ProductGroups = _order.GetByID(ID);
            Generic<Order_Models> model = new Generic<Order_Models>();
            model.ResponseInt = ProductGroups.ResponseInt;
            model.ResponseListInt = ProductGroups.ResponseListInt;
            model.ResponseListString = ProductGroups.ResponseListString;
            model.ResponseMessage = ProductGroups.ResponseMessage;
            model.ResponseString = ProductGroups.ResponseString;
            model.ResponseSuccess = ProductGroups.ResponseSuccess;
            model.GenericClass = _mapOrder.MapToUI(ProductGroups.GenericClass);
            return model;
        }

        public Generic<Order_Models> GetByStatus(string Status)
        {
            var Order = _order.GetByStatus(Status);
            Generic<Order_Models> model = new Generic<Order_Models>();
            model.ResponseInt = Order.ResponseInt;
            model.ResponseListInt = Order.ResponseListInt;
            model.ResponseListString = Order.ResponseListString;
            model.ResponseMessage = Order.ResponseMessage;
            model.ResponseString = Order.ResponseString;
            model.ResponseSuccess = Order.ResponseSuccess;
            foreach (var item in Order.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrder.MapToUI(item));
            }
            return model;
        }

        public ResponseBase Update(Order_Models Order)
        {
            return _mapResponseBase.MapToUI(_order.Update(_mapOrder.MapToLibrary(Order)));
        }
    }
}
