using BusinessLayer.Models;
using BusinessLayer.Models.OrderActivityModels;

namespace BusinessLayer.Interface
{
    public interface IOrderActivity
    {
        ResponseBase Add(OrderActivity_Models OrderActivity);
        ResponseBase Update(OrderActivity_Models OrderActivity);
        ResponseBase Delete(int ID);
        Generic<OrderActivity_Models> GetAll();
        Generic<OrderActivity_Models> GetByID(int ID);
        Generic<OrderActivity_Models> GetByOrderID(int OrderID);
    }
}
