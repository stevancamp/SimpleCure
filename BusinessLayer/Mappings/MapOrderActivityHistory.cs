using Library.DataModel;
using SimpleCure.Models.OrderModels;

namespace BusinessLayer.Mappings
{
    public class MapOrderActivityHistory
    {
        public OrderActivityHistory MapToLibrary(OrderActivityHistory_Model model)
        {
            OrderActivityHistory orderActivityHistory = new OrderActivityHistory();
            orderActivityHistory.ID = model.ID;
            orderActivityHistory.Notes = model.Notes;
            orderActivityHistory.OrderActivityTypeID = model.OrderActivityTypeID;
            orderActivityHistory.OrderID = model.OrderID;
            orderActivityHistory.TimeStamp = model.TimeStamp;

            return orderActivityHistory;
        }

        public OrderActivityHistory_Model MapToUI(OrderActivityHistory model)
        {
            OrderActivityHistory_Model orderActivityHistory = new OrderActivityHistory_Model();
            orderActivityHistory.ID = model.ID;
            orderActivityHistory.Notes = model.Notes;
            orderActivityHistory.OrderActivityTypeID = model.OrderActivityTypeID;
            orderActivityHistory.OrderID = model.OrderID;
            orderActivityHistory.TimeStamp = model.TimeStamp;

            return orderActivityHistory;


        }
    }
}
