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
        ResponseBase AddOrderActivityType(OrderActivityType_Model orderActivityType);
        ResponseBase UpdateOrderActivityType(OrderActivityType_Model orderActivityType);
        ResponseBase DeleteOrderActivityType(int ID);
        Generic<OrderActivityType_Model> GetOrderActivityTypeByID(int ID);        
        Generic<OrderActivityType_Model> GetAllOrderActivityTypes(bool IsActive);
        ResponseBase AddOrderInfoProductType(OrderInfoProductTypes_Model orderInfoProductType);
        ResponseBase UpdateOrderInfoProductType(OrderInfoProductTypes_Model OrderInfoProductType);
        ResponseBase DeleteOrderInfoProductType(int ID);
        Generic<OrderInfoProductTypes_Model> GetOrderInfoProductTypeByID(int ID);
        Generic<OrderInfoProductTypes_Model> GetAllOrderInfoProductTypes(bool IsActive);
        ResponseBase AddOrderInfoProductGroup(OrderInfoProductGroups_Model orderInfo_Product_Groups);
        ResponseBase UpdateOrderInfoProductGroup(OrderInfoProductGroups_Model orderInfo_Product_Groups);
        ResponseBase DeleteOrderInfoProductGroup(int ID);
        Generic<OrderInfoProductGroups_Model> GetAllOrderInfoProductGroups();
        Generic<OrderInfoProductGroups_Model> GetByIDOrderInfoProductGroup(int ID);
    }
}
