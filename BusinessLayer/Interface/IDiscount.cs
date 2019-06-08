using BusinessLayer.Models;
using BusinessLayer.Models.DiscountModels;

namespace BusinessLayer.Interface
{
    public interface IDiscount
    {
        ResponseBase Add(Discount_Models discount);
        ResponseBase Update(Discount_Models discount);
        ResponseBase Delete(int ID);
        Generic<Discount_Models> GetAll();
        Generic<Discount_Models> GetAllByIsActive(bool IsActive);
        Generic<Discount_Models> GetByID(int ID);
    }
}
