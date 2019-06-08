using BusinessLayer.Models;
using BusinessLayer.Models.OrderDiscountModels;

namespace BusinessLayer.Interface
{
    public interface IOrderDiscount
    {
        ResponseBase Add(OrderDiscount_Models OrderDiscount);
        ResponseBase Update(OrderDiscount_Models OrderDiscount);
        ResponseBase Delete(int ID);
        Generic<OrderDiscount_Models> GetAll();
        Generic<OrderDiscount_Models> GetAllByOrderID(int OrderID);
        Generic<OrderDiscount_Models> GetByID(int ID);
    }
}
