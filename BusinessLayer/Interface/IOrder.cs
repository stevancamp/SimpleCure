using BusinessLayer.Models;
using BusinessLayer.Models.OrderModels;
using System;

namespace BusinessLayer.Interface
{
    public interface IOrder
    {
        ResponseBase AddOrder(Order_Model orderInfo);
        ResponseBase UpdateOrder(Order_Model orderInfo);
        ResponseBase DeleteOrder(int OrderID);
        Generic<Order_Model> GetAllOrders(bool IsComplete);
        Generic<Order_Model> GetOrderByID(int ID);
        Generic<Order_Model> GetOrderByCompanyName(string CompanyName, bool IsComplete);
        Generic<Order_Model> GetOrderbyDate(DateTime OrderDate, bool IsComplete);
        Generic<Order_Model> GetOrdersBySearchParams(string SearchTerm, bool IsComplete);
        ResponseBase AddOrderProduct(OrderProduct_Model orderProduct_Model);
        ResponseBase UpdateOrderProduct(OrderProduct_Model orderProduct_Model);
        ResponseBase DeleteOrderProduct(int OrderProductID);
        Generic<OrderProduct_Model> GetAllOrderProducts();
        Generic<OrderProduct_Model> GetOrderProductByID(int ID);
        Generic<OrderProduct_Model> GetOrderProductsByOrderID(int OrderID);
        ResponseBase AddOrderHistory(OrderActivityHistory_Model orderActivityHistory_Model);
        ResponseBase UpdateOrderHistory(OrderActivityHistory_Model orderActivityHistory_Model);
        ResponseBase DeleteOrderHistory(int HistoryID);
        Generic<OrderActivityHistory_Model> GetAllOrderHistory();
        Generic<OrderActivityHistory_Model> GetAllOrderHistoryByOrderID(int OrderID);
    }
}
