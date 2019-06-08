using BusinessLayer.Models;
using BusinessLayer.Models.ProductModels;

namespace BusinessLayer.Interface
{
    public interface IProduct
    {
        ResponseBase Add(Product_Models Product);
        ResponseBase Update(Product_Models Product);
        ResponseBase Delete(int ID);
        Generic<Product_Models> GetAll();
        Generic<Product_Models> GetAllByIsActive(bool IsActive);
        Generic<Product_Models> GetByID(int ID);
    }
}
