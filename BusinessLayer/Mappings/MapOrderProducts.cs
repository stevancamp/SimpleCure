using BusinessLayer.Models.OrderProductsModels;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapOrderProducts
    {
        public OrderProduct MapToLibrary(OrderProducts_Models model)
        {
            OrderProduct returnModel = new OrderProduct();
            returnModel.BatchID = model.BatchID;
            returnModel.EntryBy = model.EntryBy;
            returnModel.EntryDate = model.EntryDate;
            returnModel.ID = model.ID;
            returnModel.OrderID = model.OrderID;
            returnModel.ProductID = model.ProductID;
            returnModel.Quantity = model.Quantity;
            returnModel.Total = model.Total;
            returnModel.status = model.status;
            returnModel.Description = model.Description;
            return returnModel;
        }

        public OrderProducts_Models MapToUI(OrderProduct model)
        {
            OrderProducts_Models returnModel = new OrderProducts_Models();
            returnModel.BatchID = model.BatchID;
            returnModel.EntryBy = model.EntryBy;
            returnModel.EntryDate = model.EntryDate;
            returnModel.ID = model.ID;
            returnModel.OrderID = model.OrderID;
            returnModel.ProductID = model.ProductID;
            returnModel.Quantity = model.Quantity;
            returnModel.Total = model.Total;
            returnModel.status = model.status;
            returnModel.Description = model.Description;
            return returnModel;
        }
    }
}
