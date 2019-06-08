using BusinessLayer.Models.OrderModels;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapOrder
    {
        public Order MapToLibrary(Order_Models model)
        {
            Order returnModel = new Order();
            returnModel.CompletionDate = model.CompletionDate;
            returnModel.ID = model.ID;
            returnModel.Notes = model.Notes;
            returnModel.OrderStatus = model.OrderStatus;
            returnModel.SubmissionDate = model.SubmissionDate;
            returnModel.Tbl_CustomerID = model.Tbl_CustomerID;
            return returnModel;
        }

        public Order_Models MapToUI(Order model)
        {
            Order_Models returnModel = new Order_Models();
            returnModel.CompletionDate = model.CompletionDate;
            returnModel.ID = model.ID;
            returnModel.Notes = model.Notes;
            returnModel.OrderStatus = model.OrderStatus;
            returnModel.SubmissionDate = model.SubmissionDate;
            returnModel.Tbl_CustomerID = model.Tbl_CustomerID;
            return returnModel;
        }
    }
}
