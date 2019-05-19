using BusinessLayer.Models.TypeModels;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapOrderActivityTypes
    {
        public OrderActivityType MapToLibrary(OrderActivityType_Model model)
        {
            OrderActivityType orderActivityType = new OrderActivityType();
            orderActivityType.ID = model.ID;
            orderActivityType.IsActive = model.IsActive;
            orderActivityType.Type = model.Type;

            return orderActivityType;
        }

        public OrderActivityType_Model MapToUI(OrderActivityType model)
        {
            OrderActivityType_Model orderActivityType = new OrderActivityType_Model();
            orderActivityType.ID = model.ID;
            orderActivityType.IsActive = model.IsActive;
            orderActivityType.Type = model.Type;

            return orderActivityType;
        }
    }
}
