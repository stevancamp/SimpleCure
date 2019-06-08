using BusinessLayer.Models;
using BusinessLayer.Models.OrderModels;

namespace BusinessLayer.Interface
{
    public interface IOrder
    {
        ResponseBase Add(Order_Models Order);
        ResponseBase Update(Order_Models Order);
        ResponseBase Delete(int ID);
        Generic<Order_Models> GetAll();
        Generic<Order_Models> GetAllByCompleted(bool Completed);
        Generic<Order_Models> GetByID(int ID);
        Generic<Order_Models> GetByStatus(int StatusID);
        Generic<Order_Models> GetByCustomerID(int CustomerID);
    }
}
