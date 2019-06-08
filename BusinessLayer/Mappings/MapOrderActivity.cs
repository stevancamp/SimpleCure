using BusinessLayer.Models.OrderActivityModels;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapOrderActivity
    {
        public OrderActivity MapToLibrary(OrderActivity_Models model)
        {
            OrderActivity returnModel = new OrderActivity();
            returnModel.ActivityBy = model.ActivityBy;
            returnModel.ActivityDate = model.ActivityDate;
            returnModel.ID = model.ID;
            returnModel.Notes = model.Notes;
            returnModel.OrderID = model.OrderID;
            returnModel.Status = model.Status;
            return returnModel;
        }

        public OrderActivity_Models MapToUI(OrderActivity model)
        {
            OrderActivity_Models returnModel = new OrderActivity_Models();
            returnModel.ActivityBy = model.ActivityBy;
            returnModel.ActivityDate = model.ActivityDate;
            returnModel.ID = model.ID;
            returnModel.Notes = model.Notes;
            returnModel.OrderID = model.OrderID;
            returnModel.Status = model.Status;
            return returnModel;
        }
    }
}
