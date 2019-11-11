using BusinessLayer.Models.SharedModels;
using Library.Shared.Models;

namespace BusinessLayer.Mappings
{
    public class MapShared
    {
        public SharedModels MapToLibrary(Shared_Models model)
        {
            SharedModels returnModel = new SharedModels();
            returnModel.CompletedOrders = model.CompletedOrders;
            returnModel.NewOrders = model.NewOrders;
            returnModel.PendingOrders = model.PendingOrders;            
            return returnModel;
        }

        public Shared_Models MapToUI(SharedModels model)
        {
            Shared_Models returnModel = new Shared_Models();
            returnModel.CompletedOrders = model.CompletedOrders;
            returnModel.NewOrders = model.NewOrders;
            returnModel.PendingOrders = model.PendingOrders;
            return returnModel;
        }
    }
}
