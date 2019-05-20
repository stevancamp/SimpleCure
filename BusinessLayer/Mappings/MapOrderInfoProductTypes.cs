using BusinessLayer.Models.TypeModels;
using Library.DataModel;
 

namespace BusinessLayer.Mappings
{
    public class MapOrderInfoProductTypes
    {
        public OrderInfo_Product_Types MapToLibrary(OrderInfoProductTypes_Model model)
        {
            OrderInfo_Product_Types orderInfo_Product_Types = new OrderInfo_Product_Types();
            orderInfo_Product_Types.ID = model.ID;
            orderInfo_Product_Types.IsActive = model.IsActive;
            orderInfo_Product_Types.Type = model.Type;
            orderInfo_Product_Types.Price = model.Price;
            orderInfo_Product_Types.OrderInfo_Product_Group = model.OrderInfo_Product_Group;
            orderInfo_Product_Types.Type_SubHeader = model.Type_SubHeader;
            return orderInfo_Product_Types;
        }

        public OrderInfoProductTypes_Model MapToUI(OrderInfo_Product_Types model)
        {
            OrderInfoProductTypes_Model orderInfo_Product_TypesModel = new OrderInfoProductTypes_Model();
            orderInfo_Product_TypesModel.ID = model.ID;
            orderInfo_Product_TypesModel.IsActive = model.IsActive;
            orderInfo_Product_TypesModel.Type = model.Type;
            orderInfo_Product_TypesModel.Price = model.Price;
            orderInfo_Product_TypesModel.OrderInfo_Product_Group = model.OrderInfo_Product_Group;
            orderInfo_Product_TypesModel.Type_SubHeader = model.Type_SubHeader;
            return orderInfo_Product_TypesModel;
        }
    }
}
