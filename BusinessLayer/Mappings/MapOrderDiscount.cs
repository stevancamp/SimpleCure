using BusinessLayer.Models.OrderDiscountModels;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapOrderDiscount
    {
        public OrderDiscount MapToLibrary(OrderDiscount_Models model)
        {
            OrderDiscount returnModel = new OrderDiscount();
            returnModel.DiscountID = model.DiscountID;
            returnModel.ID = model.ID;
            returnModel.OrderID = model.OrderID;
            returnModel.CustomDiscountAmount = model.CustomAmount;
            returnModel.CustomDiscountType = model.CustomDiscountType;
            return returnModel;
        }

        public OrderDiscount_Models MapToUI(OrderDiscount model)
        {
            OrderDiscount_Models returnModel = new OrderDiscount_Models();
            returnModel.DiscountID = model.DiscountID;
            returnModel.ID = model.ID;
            returnModel.OrderID = model.OrderID;
            returnModel.CustomAmount = model.CustomDiscountAmount;
            returnModel.CustomDiscountType = model.CustomDiscountType;
            return returnModel;
        }
    }
}
