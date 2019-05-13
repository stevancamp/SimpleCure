using BusinessLayer.Mappings;
using Library;
using Library.Orders.Methods;
using SimpleCure.Models.OrderModels;
using System;

namespace BusinessLayer.Functions.Orders
{
    public class Functions
    {
        #region Injection

        private Order _order;
        private OrderProduct _orderProduct;
        private OrderHistory _orderHistory;

        private MapOrderInfo _mapOrderInfo;
        private MapOrderInfoProduct _mapOrderInfoProduct;
        private MapOrderActivityHistory _mapOrderActivityHistory;

        public Functions()
        {
            _order = new Order();
            _orderProduct = new OrderProduct();
            _orderHistory = new OrderHistory();

            _mapOrderInfo = new MapOrderInfo();
            _mapOrderInfoProduct = new MapOrderInfoProduct();
            _mapOrderActivityHistory = new MapOrderActivityHistory();

        }
        #endregion

        #region Order

        public ResponseBase AddOrder(Order_Model orderInfo)
        {
            return _order.Add(_mapOrderInfo.MapToLibrary(orderInfo));
        }

        public ResponseBase UpdateOrder(Order_Model orderInfo)
        {
            return _order.Update(_mapOrderInfo.MapToLibrary(orderInfo));
        }

        public ResponseBase DeleteOrder(int OrderID)
        {
            return _order.Delete(OrderID);
        }

        public Generic<Order_Model> GetAllOrders(bool IsComplete)
        {
            var Orders = _order.GetAll(IsComplete);
            Generic<Order_Model> model = new Generic<Order_Model>();
            foreach (var item in Orders.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderInfo.MapToUI(item));
            }
            return model;
        }

        public Generic<Order_Model> GetOrderByID(int ID)
        {
            Generic<Order_Model> response = new Generic<Order_Model>();
            response.GenericClass = _mapOrderInfo.MapToUI(_order.GetByID(ID).GenericClass);
            return response;
        }

        public Generic<Order_Model> GetOrderByCompanyName(string CompanyName, bool IsComplete)
        {
            var Orders = _order.GetByCompanyName(CompanyName, IsComplete);
            Generic<Order_Model> model = new Generic<Order_Model>();
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
            return _orderProduct.Add(_mapOrderInfoProduct.MapToLibrary(orderProduct_Model));
        }

        public ResponseBase UpdateOrderProduct(OrderProduct_Model orderProduct_Model)
        {
            return _orderProduct.Update(_mapOrderInfoProduct.MapToLibrary(orderProduct_Model));
        }

        public ResponseBase DeleteOrderProduct(int OrderProductID)
        {
            return _orderProduct.Delete(OrderProductID);
        }

        public Generic<OrderProduct_Model> GetAllOrderProducts()
        {
            var OrderProducts = _orderProduct.GetAll();
            Generic<OrderProduct_Model> model = new Generic<OrderProduct_Model>();
            foreach (var item in OrderProducts.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderInfoProduct.MapToUI(item));
            }
            return model;
        }

        public Generic<OrderProduct_Model> GetOrderProductByID(int ID)
        {
            Generic<OrderProduct_Model> response = new Generic<OrderProduct_Model>();
            response.GenericClass = _mapOrderInfoProduct.MapToUI(_orderProduct.GetByID(ID).GenericClass);
            return response;
        }

        public Generic<OrderProduct_Model> GetOrderProductsByOrderID(int OrderID)
        {
            var OrderProducts = _orderProduct.GetAllByOrderID(OrderID);
            Generic<OrderProduct_Model> model = new Generic<OrderProduct_Model>();
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
            return _orderHistory.Add(_mapOrderActivityHistory.MapToLibrary(orderActivityHistory_Model));
        }

        public ResponseBase UpdateOrderHistory(OrderActivityHistory_Model orderActivityHistory_Model)
        {
            return _orderHistory.Update(_mapOrderActivityHistory.MapToLibrary(orderActivityHistory_Model));
        }

        public ResponseBase DeleteOrderHistory(int HistoryID)
        {
            return _orderHistory.Delete(HistoryID);
        }

        public Generic<OrderActivityHistory_Model> GetAllOrderHistory()
        {
            var OrderHistory = _orderHistory.GetAll();
            Generic<OrderActivityHistory_Model> model = new Generic<OrderActivityHistory_Model>();
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
            foreach (var item in OrderHistory.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderActivityHistory.MapToUI(item));
            }
            return model;
        }
        #endregion
    }
}
