using Library.DataModel;
using SimpleCure.Models.TypeModels;

namespace BusinessLayer.Mappings
{
    public class MapOrderInfoProductTypes
    {
        public OrderInfo_Product_Types MapToLibrary(OrderInfo_Product_TypesModel model)
        {
            OrderInfo_Product_Types orderInfo_Product_Types = new OrderInfo_Product_Types();
            orderInfo_Product_Types.ID = model.ID;
            orderInfo_Product_Types.IsActive = model.IsActive;
            orderInfo_Product_Types.Type = model.Type;
                 
            return orderInfo_Product_Types;
        }

        public OrderInfo_Product_TypesModel MapToUI(OrderInfo_Product_Types model)
        {
            OrderInfo_Product_TypesModel orderInfo_Product_TypesModel = new OrderInfo_Product_TypesModel();
            orderInfo_Product_TypesModel.ID = model.ID;
            orderInfo_Product_TypesModel.IsActive = model.IsActive;
            orderInfo_Product_TypesModel.Type = model.Type;

            return orderInfo_Product_TypesModel;
        }
    }
}
