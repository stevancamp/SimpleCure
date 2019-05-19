using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.TypeModels;
using Library.DataModel;
using Library.Types.Methods;

namespace BusinessLayer.Functions.Types
{
    public class TypeFunctions : IType
    {
        #region Injection
        private Business_Type _business_Type;
        private Order_Activity_Type _order_Activity_Type;
        private Order_Info_Product_Type _order_Info_Product_Type;
        private OrderInfo_Product_Group _orderInfo_Product_Group;

        private MapBusinessTypes _mapBusinessTypes;
        private MapOrderActivityTypes _mapOrderActivityTypes;
        private MapOrderInfoProductTypes _mapOrderInfoProductTypes;
        private MapOrderInfoProductGroups _mapOrderInfoProductGroups;
        private MapResponseBase _mapResponseBase;

        public TypeFunctions()
        {
            _business_Type = new Business_Type();
            _order_Activity_Type = new Order_Activity_Type();
            _order_Info_Product_Type = new Order_Info_Product_Type();
            _orderInfo_Product_Group = new OrderInfo_Product_Group();

            _mapBusinessTypes = new MapBusinessTypes();
            _mapOrderActivityTypes = new MapOrderActivityTypes();
            _mapOrderInfoProductTypes = new MapOrderInfoProductTypes();
            _mapOrderInfoProductGroups = new MapOrderInfoProductGroups();
            _mapResponseBase = new MapResponseBase();
        }
        #endregion

        #region Business Types Functions
 
        public ResponseBase AddBusinessType(BusinessType_Model businessType)
        {
            return _mapResponseBase.MapToUI(_business_Type.Add(_mapBusinessTypes.MapToLibrary(businessType)));
        }
 
        public ResponseBase UpdateBusinessType(BusinessType_Model businessType)
        {
            return _mapResponseBase.MapToUI(_business_Type.Update(_mapBusinessTypes.MapToLibrary(businessType)));
        }
 
        public ResponseBase DeleteBusinessType(int ID)
        {
            return _mapResponseBase.MapToUI(_business_Type.Delete(ID));
        }

        public Generic<BusinessType_Model> GetBusinessTypeByID(int ID)
        {
            Generic<BusinessType_Model> model = new Generic<BusinessType_Model>();
            var BusinessTypes = _business_Type.GetByID(ID);
            model.ResponseInt = BusinessTypes.ResponseInt;
            model.ResponseListInt = BusinessTypes.ResponseListInt;
            model.ResponseListString = BusinessTypes.ResponseListString;
            model.ResponseMessage = BusinessTypes.ResponseMessage;
            model.ResponseString = BusinessTypes.ResponseString;
            model.ResponseSuccess = BusinessTypes.ResponseSuccess;
            model.GenericClass = _mapBusinessTypes.MapToUI(BusinessTypes.GenericClass);
            return model;
        }

        public Generic<BusinessType_Model> GetAllBusinessTypes(bool IsActive)
        {
            var BusinessTypes = _business_Type.GetAll(IsActive);
            Generic<BusinessType_Model> model = new Generic<BusinessType_Model>();
            model.ResponseInt = BusinessTypes.ResponseInt;
            model.ResponseListInt = BusinessTypes.ResponseListInt;
            model.ResponseListString = BusinessTypes.ResponseListString;
            model.ResponseMessage = BusinessTypes.ResponseMessage;
            model.ResponseString = BusinessTypes.ResponseString;
            model.ResponseSuccess = BusinessTypes.ResponseSuccess;
            foreach (var item in BusinessTypes.GenericClassList)
            {
                model.GenericClassList.Add(_mapBusinessTypes.MapToUI(item));
            }
            return model;
        }

        #endregion

        #region Order Activity Type

        public ResponseBase AddOrderActivityType(OrderActivityType_Model orderActivityType)
        {
            return _mapResponseBase.MapToUI(_order_Activity_Type.Add(_mapOrderActivityTypes.MapToLibrary(orderActivityType)));
        }

        public ResponseBase UpdateOrderActivityType(OrderActivityType_Model orderActivityType)
        {
            return _mapResponseBase.MapToUI(_order_Activity_Type.Update(_mapOrderActivityTypes.MapToLibrary(orderActivityType)));
        }

        public ResponseBase DeleteOrderActivityType(int ID)
        {
            return _mapResponseBase.MapToUI(_order_Activity_Type.Delete(ID));
        }

        public Generic<OrderActivityType_Model> GetOrderActivityTypeByID(int ID)
        {
            Generic<OrderActivityType_Model> model = new Generic<OrderActivityType_Model>();
            var OrderActivityType = _order_Activity_Type.GetByID(ID);
            model.ResponseInt = OrderActivityType.ResponseInt;
            model.ResponseListInt = OrderActivityType.ResponseListInt;
            model.ResponseListString = OrderActivityType.ResponseListString;
            model.ResponseMessage = OrderActivityType.ResponseMessage;
            model.ResponseString = OrderActivityType.ResponseString;
            model.ResponseSuccess = OrderActivityType.ResponseSuccess;
            model.GenericClass = _mapOrderActivityTypes.MapToUI(OrderActivityType.GenericClass);
            return model;
        }

        public Generic<OrderActivityType_Model> GetAllOrderActivityTypes(bool IsActive)
        {
            var OrderActivityTypes = _order_Activity_Type.GetAll(IsActive);
            Generic<OrderActivityType_Model> model = new Generic<OrderActivityType_Model>();
            model.ResponseInt = OrderActivityTypes.ResponseInt;
            model.ResponseListInt = OrderActivityTypes.ResponseListInt;
            model.ResponseListString = OrderActivityTypes.ResponseListString;
            model.ResponseMessage = OrderActivityTypes.ResponseMessage;
            model.ResponseString = OrderActivityTypes.ResponseString;
            model.ResponseSuccess = OrderActivityTypes.ResponseSuccess;
            foreach (var item in OrderActivityTypes.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderActivityTypes.MapToUI(item));
            }
            return model;
        }

        #endregion

        #region Order Info Product Type
        public ResponseBase AddOrderInfoProductType(OrderInfoProductTypes_Model orderInfoProductType)
        {
            return _mapResponseBase.MapToUI(_order_Info_Product_Type.Add(_mapOrderInfoProductTypes.MapToLibrary(orderInfoProductType)));
        }

        public ResponseBase UpdateOrderInfoProductType(OrderInfoProductTypes_Model OrderInfoProductType)
        {
            return _mapResponseBase.MapToUI(_order_Info_Product_Type.Update(_mapOrderInfoProductTypes.MapToLibrary(OrderInfoProductType)));
        }

        public ResponseBase DeleteOrderInfoProductType(int ID)
        {
            return _mapResponseBase.MapToUI(_order_Info_Product_Type.Delete(ID));
        }

        public Generic<OrderInfoProductTypes_Model> GetOrderInfoProductTypeByID(int ID)
        {
            Generic<OrderInfoProductTypes_Model> model = new Generic<OrderInfoProductTypes_Model>();
            var OrderInfoProductType = _order_Info_Product_Type.GetByID(ID);
            model.ResponseInt = OrderInfoProductType.ResponseInt;
            model.ResponseListInt = OrderInfoProductType.ResponseListInt;
            model.ResponseListString = OrderInfoProductType.ResponseListString;
            model.ResponseMessage = OrderInfoProductType.ResponseMessage;
            model.ResponseString = OrderInfoProductType.ResponseString;
            model.ResponseSuccess = OrderInfoProductType.ResponseSuccess;
            model.GenericClass = _mapOrderInfoProductTypes.MapToUI(OrderInfoProductType.GenericClass);
            return model;
        }

        public Generic<OrderInfoProductTypes_Model> GetAllOrderInfoProductTypes(bool IsActive)
        {
            var OrderInfoProductTypes = _order_Info_Product_Type.GetAll(IsActive);
            Generic<OrderInfoProductTypes_Model> model = new Generic<OrderInfoProductTypes_Model>();
            model.ResponseInt = OrderInfoProductTypes.ResponseInt;
            model.ResponseListInt = OrderInfoProductTypes.ResponseListInt;
            model.ResponseListString = OrderInfoProductTypes.ResponseListString;
            model.ResponseMessage = OrderInfoProductTypes.ResponseMessage;
            model.ResponseString = OrderInfoProductTypes.ResponseString;
            model.ResponseSuccess = OrderInfoProductTypes.ResponseSuccess;
            foreach (var item in OrderInfoProductTypes.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderInfoProductTypes.MapToUI(item));
            }
            return model;
        }
        #endregion

        #region Order Product Group
        public ResponseBase AddOrderInfoProductGroup(OrderInfoProductGroups_Model orderInfo_Product_Groups)
        {
            return _mapResponseBase.MapToUI(_orderInfo_Product_Group.Add(_mapOrderInfoProductGroups.MapToLibrary(orderInfo_Product_Groups)));
        }

        public ResponseBase UpdateOrderInfoProductGroup(OrderInfoProductGroups_Model orderInfo_Product_Groups)
        {
            return _mapResponseBase.MapToUI(_orderInfo_Product_Group.Update(_mapOrderInfoProductGroups.MapToLibrary(orderInfo_Product_Groups)));
        }

        public ResponseBase DeleteOrderInfoProductGroup(int ID)
        {
            return _mapResponseBase.MapToUI(_orderInfo_Product_Group.Delete(ID));
        }

        public Generic<OrderInfoProductGroups_Model> GetAllOrderInfoProductGroups()
        {
            var OrderProductGrouopTypes = _orderInfo_Product_Group.GetAll();
            Generic<OrderInfoProductGroups_Model> model = new Generic<OrderInfoProductGroups_Model>();
            model.ResponseInt = OrderProductGrouopTypes.ResponseInt;
            model.ResponseListInt = OrderProductGrouopTypes.ResponseListInt;
            model.ResponseListString = OrderProductGrouopTypes.ResponseListString;
            model.ResponseMessage = OrderProductGrouopTypes.ResponseMessage;
            model.ResponseString = OrderProductGrouopTypes.ResponseString;
            model.ResponseSuccess = OrderProductGrouopTypes.ResponseSuccess;
            foreach (var item in OrderProductGrouopTypes.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderInfoProductGroups.MapToUI(item));
            }
            return model;
        }

        public Generic<OrderInfoProductGroups_Model> GetByIDOrderInfoProductGroup(int ID)
        {           
            Generic<OrderInfoProductGroups_Model> model = new Generic<OrderInfoProductGroups_Model>();
            var OrderProductGroupType = _orderInfo_Product_Group.GetByID(ID);
            model.ResponseInt = OrderProductGroupType.ResponseInt;
            model.ResponseListInt = OrderProductGroupType.ResponseListInt;
            model.ResponseListString = OrderProductGroupType.ResponseListString;
            model.ResponseMessage = OrderProductGroupType.ResponseMessage;
            model.ResponseString = OrderProductGroupType.ResponseString;
            model.ResponseSuccess = OrderProductGroupType.ResponseSuccess;
            model.GenericClass = _mapOrderInfoProductGroups.MapToUI(OrderProductGroupType.GenericClass);
            return model;           
        }
        #endregion

    }
}
