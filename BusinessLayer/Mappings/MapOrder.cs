using BusinessLayer.Models.OrderModels;
using Library._Order.Model;
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
            returnModel.TransportID = model.TransportID;
            returnModel.IsSimpleCure = model.IsSimpleCure;
            returnModel.TransportID = model.TransportID;
            returnModel.TransportLocationEnd = model.TransportLocationEnd;
            returnModel.TransportLocationStart = model.TransportLocationStart;
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
            returnModel.TransportID = model.TransportID;
            returnModel.IsSimpleCure = model.IsSimpleCure;
            returnModel.TransportID = model.TransportID;
            returnModel.TransportLocationEnd = model.TransportLocationEnd;
            returnModel.TransportLocationStart = model.TransportLocationStart;
            return returnModel;
        }

        public SearchPaidOrder MapPaidToLibrary(PaidOrders_Models model)
        {
            SearchPaidOrder returnModel = new SearchPaidOrder();
            returnModel.Company = model.Company;
            returnModel.Customer = model.Customer;
            returnModel.OrderDate = model.OrderDate;
            returnModel.OrderID = model.OrderID;
            returnModel.OrderStatus = model.OrderStatus;
            returnModel.PaidDate = model.PaidDate;
            return returnModel;
        }

        public PaidOrders_Models MapPaidToUI(SearchPaidOrder model)
        {
            PaidOrders_Models returnModel = new PaidOrders_Models();
            returnModel.Company = model.Company;
            returnModel.Customer = model.Customer;
            returnModel.OrderDate = model.OrderDate;
            returnModel.OrderID = model.OrderID;
            returnModel.OrderStatus = model.OrderStatus;
            returnModel.PaidDate = model.PaidDate;
            return returnModel;
        }



        public SearchPaidOrder MapPaidToLibrary(PaidOrders_Models model)
        {
            SearchPaidOrder returnModel = new SearchPaidOrder();
            returnModel.Company = model.Company;
            returnModel.Customer = model.Customer;
            returnModel.OrderDate = model.OrderDate;
            returnModel.OrderID = model.OrderID;
            returnModel.OrderStatus = model.OrderStatus;
            returnModel.PaidDate = model.PaidDate;
            return returnModel;
        }

        public PaidOrders_Models MapPaidToUI(SearchPaidOrder model)
        {
            PaidOrders_Models returnModel = new PaidOrders_Models();
            returnModel.Company = model.Company;
            returnModel.Customer = model.Customer;
            returnModel.OrderDate = model.OrderDate;
            returnModel.OrderID = model.OrderID;
            returnModel.OrderStatus = model.OrderStatus;
            returnModel.PaidDate = model.PaidDate;
            return returnModel;
        }
    }
}
