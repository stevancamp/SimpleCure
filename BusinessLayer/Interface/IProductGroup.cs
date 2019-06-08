using BusinessLayer.Models;
using BusinessLayer.Models.ProductGroupModels;

namespace BusinessLayer.Interface
{
    public interface IProductGroup
    {
        ResponseBase Add(ProductGroup_Models ProductGroup);
        ResponseBase Update(ProductGroup_Models ProductGroup);
        ResponseBase Delete(int ID);
        Generic<ProductGroup_Models> GetAll();
        Generic<ProductGroup_Models> GetAllByIsActive(bool IsActive);
        Generic<ProductGroup_Models> GetByID(int ID);
    }
}
