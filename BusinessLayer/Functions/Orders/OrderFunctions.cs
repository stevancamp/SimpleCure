using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.OrderModels;
using Library.Orders.Methods;
using System;

namespace BusinessLayer.Functions.Orders
{
    public class OrderFunctions : IOrder
    {
        #region Injection

        private Order _order;
        private OrderProduct _orderProduct;
        private OrderHistory _orderHistory;

        private MapOrderInfo _mapOrderInfo;
        private MapOrderInfoProduct _mapOrderInfoProduct;
        private MapOrderActivityHistory _mapOrderActivityHistory;
        private MapResponseBase _mapResponseBase;

        public OrderFunctions()
        {
            _order = new Order();
            _orderProduct = new OrderProduct();
            _orderHistory = new OrderHistory();

            _mapOrderInfo = new MapOrderInfo();
            _mapOrderInfoProduct = new MapOrderInfoProduct();
            _mapOrderActivityHistory = new MapOrderActivityHistory();
            _mapResponseBase = new MapResponseBase();

        }
        #endregion

        #region Order

        public ResponseBase AddOrder(Order_Model orderInfo)
        {
            return _mapResponseBase.MapToUI(_order.Add(_mapOrderInfo.MapToLibrary(orderInfo)));
        }

        public ResponseBase UpdateOrder(Order_Model orderInfo)
        {
            return _mapResponseBase.MapToUI(_order.Update(_mapOrderInfo.MapToLibrary(orderInfo)));
        }

        public ResponseBase DeleteOrder(int OrderID)
        {
            return _mapResponseBase.MapToUI(_order.Delete(OrderID));
        }

        public Generic<Order_Model> GetAllOrders(bool IsComplete)
        {
            var Orders = _order.GetAll(IsComplete);
            Generic<Order_Model> model = new Generic<Order_Model>();
            model.ResponseInt = Orders.ResponseInt;
            model.ResponseListInt = Orders.ResponseListInt;
            model.ResponseListString = Orders.ResponseListString;
            model.ResponseMessage = Orders.ResponseMessage;
            model.ResponseString = Orders.ResponseString;
            model.ResponseSuccess = Orders.ResponseSuccess;
            foreach (var item in Orders.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderInfo.MapToUI(item));
            }
            return model;
        }

        public Generic<Order_Model> GetOrderByID(int ID)
        {
            Generic<Order_Model> response = new Generic<Order_Model>();
            var order = _order.GetByID(ID);
            response.ResponseInt = order.ResponseInt;
            response.ResponseListInt = order.ResponseListInt;
            response.ResponseListString = order.ResponseListString;
            response.ResponseMessage = order.ResponseMessage;
            response.ResponseString = order.ResponseString;
            response.ResponseSuccess = order.ResponseSuccess;
            response.GenericClass = _mapOrderInfo.MapToUI(order.GenericClass);
            return response;
        }

        public Generic<Order_Model> GetOrderByCompanyName(string CompanyName, bool IsComplete)
        {
            var Orders = _order.GetByCompanyName(CompanyName, IsComplete);
            Generic<Order_Model> model = new Generic<Order_Model>();
            model.ResponseInt = Orders.ResponseInt;
            model.ResponseListInt = Orders.ResponseListInt;
            model.ResponseListString = Orders.ResponseListString;
            model.ResponseMessage = Orders.ResponseMessage;
            model.ResponseString = Orders.ResponseString;
            model.ResponseSuccess = Orders.ResponseSuccess;
            foreach (var item in Orders.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderInfo.MapToUI(item));
            }
            return model;
        }

        public Generic<Order_Model> GetOrderbyDate(DateTime OrderDate, bool IsComplete)
        {
            var Orders = _order.GetByDate(OrderDate, IsComplete);
            Generic<Order_Model> model = new Generic<Order_Model>();
            model.ResponseInt = Orders.ResponseInt;
            model.ResponseListInt = Orders.ResponseListInt;
            model.ResponseListString = Orders.ResponseListString;
            model.ResponseMessage = Orders.ResponseMessage;
            model.ResponseString = Orders.ResponseString;
            model.ResponseSuccess = Orders.ResponseSuccess;
            foreach (var item in Orders.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderInfo.MapToUI(item));
            }
            return model;
        }

        public Generic<Order_Model> GetOrdersBySearchParams(string SearchTerm, bool IsComplete)
        {
            var Orders = _order.SearchOrderInfo(SearchTerm, IsComplete);
            Generic<Order_Model> model = new Generic<Order_Model>();
            model.ResponseInt = Orders.ResponseInt;
            model.ResponseListInt = Orders.ResponseListInt;
            model.ResponseListString = Orders.ResponseListString;
            model.ResponseMessage = Orders.ResponseMessage;
            model.ResponseString = Orders.ResponseString;
            model.ResponseSuccess = Orders.ResponseSuccess;
            foreach (var item in Orders.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderInfo.MapToUI(item));
            }
            return model;
        }

        #endregion

        #region OrderProduct

        public ResponseBase AddOrderProduct(OrderProduct_Model orderProduct_Model)
        {
            return _mapResponseBase.MapToUI(_orderProduct.Add(_mapOrderInfoProduct.MapToLibrary(orderProduct_Model)));
        }

        public ResponseBase UpdateOrderProduct(OrderProduct_Model orderProduct_Model)
        {
            return _mapResponseBase.MapToUI(_orderProduct.Update(_mapOrderInfoProduct.MapToLibrary(orderProduct_Model)));
        }

        public ResponseBase DeleteOrderProduct(int OrderProductID)
        {
            return _mapResponseBase.MapToUI(_orderProduct.Delete(OrderProductID));
        }

        public Generic<OrderProduct_Model> GetAllOrderProducts()
        {
            var OrderProducts = _orderProduct.GetAll();
            Generic<OrderProduct_Model> model = new Generic<OrderProduct_Model>();
            model.ResponseInt = OrderProducts.ResponseInt;
            model.ResponseListInt = OrderProducts.ResponseListInt;
            model.ResponseListString = OrderProducts.ResponseListString;
            model.ResponseMessage = OrderProducts.ResponseMessage;
            model.ResponseString = OrderProducts.ResponseString;
            model.ResponseSuccess = OrderProducts.ResponseSuccess;
            foreach (var item in OrderProducts.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderInfoProduct.MapToUI(item));
            }
            return model;
        }

        public Generic<OrderProduct_Model> GetOrderProductByID(int ID)
        {
            Generic<OrderProduct_Model> response = new Generic<OrderProduct_Model>();
            var OrderProduct = _orderProduct.GetByID(ID);
            response.ResponseInt = OrderProduct.ResponseInt;
            response.ResponseListInt = OrderProduct.ResponseListInt;
            response.ResponseListString = OrderProduct.ResponseListString;
            response.ResponseMessage = OrderProduct.ResponseMessage;
            response.ResponseString = OrderProduct.ResponseString;
            response.ResponseSuccess = OrderProduct.ResponseSuccess;
            response.GenericClass = _mapOrderInfoProduct.MapToUI(OrderProduct.GenericClass);
            return response;
        }

        public Generic<OrderProduct_Model> GetOrderProductsByOrderID(int OrderID)
        {
            var OrderProducts = _orderProduct.GetAllByOrderID(OrderID);
            Generic<OrderProduct_Model> model = new Generic<OrderProduct_Model>();
            model.ResponseInt = OrderProducts.ResponseInt;
            model.ResponseListInt = OrderProducts.ResponseListInt;
            model.ResponseListString = OrderProducts.ResponseListString;
            model.ResponseMessage = OrderProducts.ResponseMessage;
            model.ResponseString = OrderProducts.ResponseString;
            model.ResponseSuccess = OrderProducts.ResponseSuccess;
            foreach (var item in OrderProducts.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderInfoProduct.MapToUI(item));
            }
            return model;
        }

        #endregion

        #region OrderHistory

        public ResponseBase AddOrderHistory(OrderActivityHistory_Model orderActivityHistory_Model)
        {
            return _mapResponseBase.MapToUI(_orderHistory.Add(_mapOrderActivityHistory.MapToLibrary(orderActivityHistory_Model)));
        }

        public ResponseBase UpdateOrderHistory(OrderActivityHistory_Model orderActivityHistory_Model)
        {
            return _mapResponseBase.MapToUI(_orderHistory.Update(_mapOrderActivityHistory.MapToLibrary(orderActivityHistory_Model)));
        }

        public ResponseBase DeleteOrderHistory(int HistoryID)
        {
            return _mapResponseBase.MapToUI(_orderHistory.Delete(HistoryID));
        }

        public Generic<OrderActivityHistory_Model> GetAllOrderHistory()
        {
            var OrderHistory = _orderHistory.GetAll();
            Generic<OrderActivityHistory_Model> model = new Generic<OrderActivityHistory_Model>();
            model.ResponseInt = OrderHistory.ResponseInt;
            model.ResponseListInt = OrderHistory.ResponseListInt;
            model.ResponseListString = OrderHistory.ResponseListString;
            model.ResponseMessage = OrderHistory.ResponseMessage;
            model.ResponseString = OrderHistory.ResponseString;
            model.ResponseSuccess = OrderHistory.ResponseSuccess;
            foreach (var item in OrderHistory.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderActivityHistory.MapToUI(item));
            }
            return model;
        }

        public Generic<OrderActivityHistory_Model> GetAllOrderHistoryByOrderID(int OrderID)
        {
            var OrderHistory = _orderHistory.GetAllByOrderID(OrderID);
            Generic<OrderActivityHistory_Model> model = new Generic<OrderActivityHistory_Model>();
            model.ResponseInt = OrderHistory.ResponseInt;
            model.ResponseListInt = OrderHistory.ResponseListInt;
            model.ResponseListString = OrderHistory.ResponseListString;
            model.ResponseMessage = OrderHistory.ResponseMessage;
            model.ResponseString = OrderHistory.ResponseString;
            model.ResponseSuccess = OrderHistory.ResponseSuccess;
            foreach (var item in OrderHistory.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderActivityHistory.MapToUI(item));
            }
            return model;
        }
        #endregion
    }
}
