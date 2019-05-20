using BusinessLayer.Models.TypeModels;
using Library.Types.Models;

namespace BusinessLayer.Mappings
{
    public class MapOrderInfoProductTypesWithGroupName
    {
        public Order_Info_Product_Types_With_GroupName MapToLibrary(Order_Info_Product_Types_With_GroupName_Model model)
        {
            Order_Info_Product_Types_With_GroupName order_Info_Product_Types_With_GroupName = new Order_Info_Product_Types_With_GroupName();
            order_Info_Product_Types_With_GroupName.GroupName = model.GroupName;
            order_Info_Product_Types_With_GroupName.ID = model.ID;
            order_Info_Product_Types_With_GroupName.IsActive = model.IsActive;
            order_Info_Product_Types_With_GroupName.OrderInfo_Product_Group = model.OrderInfo_Product_Group;
            order_Info_Product_Types_With_GroupName.Price = model.Price;
            order_Info_Product_Types_With_GroupName.Type = model.Type;
            order_Info_Product_Types_With_GroupName.Type_SubHeader = model.Type_SubHeader;

            return order_Info_Product_Types_With_GroupName;
        }

        public Order_Info_Product_Types_With_GroupName_Model MapToUI(Order_Info_Product_Types_With_GroupName model)
        {
            Order_Info_Product_Types_With_GroupName_Model order_Info_Product_Types_With_GroupName_Model = new Order_Info_Product_Types_With_GroupName_Model();
            order_Info_Product_Types_With_GroupName_Model.GroupName = model.GroupName;
            order_Info_Product_Types_With_GroupName_Model.ID = model.ID;
            order_Info_Product_Types_With_GroupName_Model.IsActive = model.IsActive;
            order_Info_Product_Types_With_GroupName_Model.OrderInfo_Product_Group = model.OrderInfo_Product_Group;
            order_Info_Product_Types_With_GroupName_Model.Price = model.Price;
            order_Info_Product_Types_With_GroupName_Model.Type = model.Type;
            order_Info_Product_Types_With_GroupName_Model.Type_SubHeader = model.Type_SubHeader;

            return order_Info_Product_Types_With_GroupName_Model;
        }
    }
}
