using BusinessLayer.Models.OrderModels;
using Library.DataModel;
 
namespace BusinessLayer.Mappings
{
    public class MapOrderInfoProduct
    {
        public OrderInfo_Product MapToLibrary(OrderProduct_Model model)
        {
            OrderInfo_Product orderInfo_Product = new OrderInfo_Product();
            orderInfo_Product.ID = model.ID;
            orderInfo_Product.OrderInfoID = model.OrderInfoID;
            orderInfo_Product.Quantity = model.Quantity;
            orderInfo_Product.Type = model.Type;

            return orderInfo_Product;
        }

        public OrderProduct_Model MapToUI(OrderInfo_Product model)
        {
            OrderProduct_Model orderInfo_Product = new OrderProduct_Model();
            orderInfo_Product.ID = model.ID;
            orderInfo_Product.OrderInfoID = model.OrderInfoID;
            orderInfo_Product.Quantity = model.Quantity;
            orderInfo_Product.Type = model.Type;

            return orderInfo_Product;
        }
    }
}
