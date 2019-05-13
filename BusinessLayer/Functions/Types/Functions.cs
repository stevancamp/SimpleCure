using BusinessLayer.Mappings;
using Library;
using Library.Types.Methods;
using SimpleCure.Models.TypeModels;

namespace BusinessLayer.Functions.Types
{
    public class Functions
    {
        #region Injection
        private Business_Type _business_Type;
        private Order_Activity_Type _order_Activity_Type;
        private Order_Info_Product_Type _order_Info_Product_Type;

        private MapBusinessTypes _mapBusinessTypes;
        private MapOrderActivityTypes _mapOrderActivityTypes;
        private MapOrderInfoProductTypes _mapOrderInfoProductTypes;

        public Functions()
        {
            _business_Type = new Business_Type();
            _order_Activity_Type = new Order_Activity_Type();
            _order_Info_Product_Type = new Order_Info_Product_Type();

            _mapBusinessTypes = new MapBusinessTypes();
            _mapOrderActivityTypes = new MapOrderActivityTypes();
            _mapOrderInfoProductTypes = new MapOrderInfoProductTypes();
        }
        #endregion

        #region Business Types Functions
 
        public ResponseBase AddBusinessType(BusinessTypeModel businessType)
        {
            return _business_Type.Add(_mapBusinessTypes.MapToLibrary(businessType));
        }
 
        public ResponseBase UpdateBusinessType(BusinessTypeModel businessType)
        {
            return _business_Type.Update(_mapBusinessTypes.MapToLibrary(businessType));
        }
 
        public ResponseBase DeleteBusinessType(int ID)
        {
            return _business_Type.Delete(ID);
        }
 
        public Generic<BusinessTypeModel> GetAllBusinessTypes(bool IsActive)
        {
            var Logs = _business_Type.GetAll(IsActive);
            Generic<BusinessTypeModel> model = new Generic<BusinessTypeModel>();
            foreach (var item in Logs.GenericClassList)
            {
                model.GenericClassList.Add(_mapBusinessTypes.MapToUI(item));
            }
            return model;
        }

        #endregion

        #region Order Activity Type

        public ResponseBase AddOrderActivityType(OrderActivityTypeModel orderActivityType)
        {
            return _order_Activity_Type.Add(_mapOrderActivityTypes.MapToLibrary(orderActivityType));
        }

        public ResponseBase UpdateOrderActivityType(OrderActivityTypeModel orderActivityType)
        {
            return _order_Activity_Type.Update(_mapOrderActivityTypes.MapToLibrary(orderActivityType));
        }

        public ResponseBase DeleteOrderActivityType(int ID)
        {
            return _order_Activity_Type.Delete(ID);
        }

        public Generic<OrderActivityTypeModel> GetAllOrderActivityTypes(bool IsActive)
        {
            var Logs = _order_Activity_Type.GetAll(IsActive);
            Generic<OrderActivityTypeModel> model = new Generic<OrderActivityTypeModel>();
            foreach (var item in Logs.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderActivityTypes.MapToUI(item));
            }
            return model;
        }

        #endregion

        #region Order Info Product Type
        public ResponseBase AddOrderInfoProductType(OrderInfo_Product_TypesModel orderInfoProductType)
        {
            return _order_Info_Product_Type.Add(_mapOrderInfoProductTypes.MapToLibrary(orderInfoProductType));
        }

        public ResponseBase UpdateOrderInfoProductType(OrderInfo_Product_TypesModel OrderInfoProductType)
        {
            return _order_Info_Product_Type.Update(_mapOrderInfoProductTypes.MapToLibrary(OrderInfoProductType));
        }

        public ResponseBase DeleteOrderInfoProductType(int ID)
        {
            return _order_Info_Product_Type.Delete(ID);
        }

        public Generic<OrderInfo_Product_TypesModel> GetAllOrderInfoProductTypes(bool IsActive)
        {
            var Logs = _order_Info_Product_Type.GetAll(IsActive);
            Generic<OrderInfo_Product_TypesModel> model = new Generic<OrderInfo_Product_TypesModel>();
            foreach (var item in Logs.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderInfoProductTypes.MapToUI(item));
            }
            return model;
        }
        #endregion

    }
}
