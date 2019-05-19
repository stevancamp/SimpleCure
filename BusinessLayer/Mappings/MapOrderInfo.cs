using BusinessLayer.Models.OrderModels;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapOrderInfo
    {
        public OrderInfo MapToLibrary(Order_Model model)
        {
            OrderInfo orderInfo = new OrderInfo();
            orderInfo.BusinessType = model.BusinessType;
            orderInfo.CompanyName = model.CompanyName;
            orderInfo.Completed = model.Completed;
            orderInfo.CompletionNotes = model.CompletionNotes;
            orderInfo.ContactName = model.ContactName;
            orderInfo.EINNumber = model.EINNumber;
            orderInfo.EmailAddress = model.EmailAddress;
            orderInfo.ID = model.ID;
            orderInfo.Notes = model.Notes;
            orderInfo.OBNDDNumber = model.OBNDDNumber;
            orderInfo.OMMANumber = model.OMMANumber;
            orderInfo.OrderSubmissionDate = model.OrderSubmissionDate;
            orderInfo.PhoneNumber = model.PhoneNumber;
            orderInfo.StreetAddress = model.StreetAddress;

            return orderInfo;
        }

        public Order_Model MapToUI(OrderInfo model)
        {
            Order_Model orderInfo = new Order_Model();
            orderInfo.BusinessType = model.BusinessType;
            orderInfo.CompanyName = model.CompanyName;
            orderInfo.Completed = model.Completed;
            orderInfo.CompletionNotes = model.CompletionNotes;
            orderInfo.ContactName = model.ContactName;
            orderInfo.EINNumber = model.EINNumber;
            orderInfo.EmailAddress = model.EmailAddress;
            orderInfo.ID = model.ID;
            orderInfo.Notes = model.Notes;
            orderInfo.OBNDDNumber = model.OBNDDNumber;
            orderInfo.OMMANumber = model.OMMANumber;
            orderInfo.OrderSubmissionDate = model.OrderSubmissionDate;
            orderInfo.PhoneNumber = model.PhoneNumber;
            orderInfo.StreetAddress = model.StreetAddress;

            return orderInfo;
        }
    }
}
