using BusinessLayer.Models.DiscountModels;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapDiscount
    {
        public Discount MapToLibrary(Discount_Models model)
        {
            Discount returnModel = new Discount();
            returnModel.DiscountAmount = model.DiscountAmount;
            returnModel.ID = model.ID;
            returnModel.IsActive = model.IsActive;
            returnModel.Type = model.Type;
            return returnModel;
        }

        public Discount_Models MapToUI(Discount model)
        {
            Discount_Models returnModel = new Discount_Models();
            returnModel.DiscountAmount = model.DiscountAmount;
            returnModel.ID = model.ID;
            returnModel.IsActive = model.IsActive;
            returnModel.Type = model.Type;
            return returnModel;
        }
    }
}
