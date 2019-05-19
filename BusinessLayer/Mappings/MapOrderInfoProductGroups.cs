using BusinessLayer.Models.TypeModels;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapOrderInfoProductGroups
    {
        public OrderInfo_Product_Groups MapToLibrary(OrderInfoProductGroups_Model model)
        {
            OrderInfo_Product_Groups orderInfo_Product_Groups = new OrderInfo_Product_Groups();
            orderInfo_Product_Groups.ID = model.ID;
            orderInfo_Product_Groups.GroupName = model.GroupName;
            return orderInfo_Product_Groups;
        }

        public OrderInfoProductGroups_Model MapToUI(OrderInfo_Product_Groups model)
        {
            OrderInfoProductGroups_Model orderInfoProductGroups_Model = new OrderInfoProductGroups_Model();
            orderInfoProductGroups_Model.ID = model.ID;
            orderInfoProductGroups_Model.GroupName = model.GroupName;
            return orderInfoProductGroups_Model;
        }
    }
}
