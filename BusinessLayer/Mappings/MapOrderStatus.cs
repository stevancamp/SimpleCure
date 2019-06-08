using BusinessLayer.Models.OrderStatusModels;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapOrderStatus
    {
        public OrderStatu MapToLibrary(OrderStatus_Models model)
        {
            OrderStatu returnModel = new OrderStatu();
            returnModel.ID = model.ID;
            returnModel.Status = model.Status;
            return returnModel;
        }

        public OrderStatus_Models MapToUI(OrderStatu model)
        {
            OrderStatus_Models returnModel = new OrderStatus_Models();
            returnModel.ID = model.ID;
            returnModel.Status = model.Status;
            return returnModel;
        }
    }
}
