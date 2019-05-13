using Library.DataModel;
using SimpleCure.Models.TypeModels;

namespace BusinessLayer.Mappings
{
    public class MapOrderActivityTypes
    {
        public OrderActivityType MapToLibrary(OrderActivityTypeModel model)
        {
            OrderActivityType orderActivityType = new OrderActivityType();
            orderActivityType.ID = model.ID;
            orderActivityType.IsActive = model.IsActive;
            orderActivityType.Type = model.Type;

            return orderActivityType;
        }

        public OrderActivityTypeModel MapToUI(OrderActivityType model)
        {
            OrderActivityTypeModel orderActivityType = new OrderActivityTypeModel();
            orderActivityType.ID = model.ID;
            orderActivityType.IsActive = model.IsActive;
            orderActivityType.Type = model.Type;

            return orderActivityType;
        }
    }
}
