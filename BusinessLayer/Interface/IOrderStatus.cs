using BusinessLayer.Models;
using BusinessLayer.Models.OrderStatusModels;

namespace BusinessLayer.Interface
{
    public interface IOrderStatus
    {
        ResponseBase Add(OrderStatus_Models orderStatus);
        ResponseBase Update(OrderStatus_Models orderStatus);
        ResponseBase Delete(int ID);
        Generic<OrderStatus_Models> GetAll();
        Generic<OrderStatus_Models> GetByID(int ID);
    }
}
