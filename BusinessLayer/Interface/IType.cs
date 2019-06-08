using BusinessLayer.Models;
using BusinessLayer.Models.TypeModels;

namespace BusinessLayer.Interface
{
    public interface IType
    {
        ResponseBase AddBusinessType(BusinessType_Model businessType);
        ResponseBase UpdateBusinessType(BusinessType_Model businessType);
        ResponseBase DeleteBusinessType(int ID);
        Generic<BusinessType_Model> GetBusinessTypeByID(int ID);        
        Generic<BusinessType_Model> GetAllBusinessTypes(bool IsActive);       
    }
}
