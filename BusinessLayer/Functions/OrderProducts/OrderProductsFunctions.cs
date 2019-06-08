using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.OrderProductsModels;
using Library._OrderProducts.Methods;

namespace BusinessLayer.Functions.OrderProducts
{
    public class OrderProductsFunctions : IOrderProducts
    {

        #region Injection
        private _OrderProducts _orderProducts;
        private MapOrderProducts _mapOrderProducts;
        private MapResponseBase _mapResponseBase;

        public OrderProductsFunctions()
        {
            _orderProducts = new _OrderProducts();
            _mapOrderProducts = new MapOrderProducts();
            _mapResponseBase = new MapResponseBase();

        }
        #endregion


        public ResponseBase Add(OrderProducts_Models OrderProducts)
        {
            return _mapResponseBase.MapToUI(_orderProducts.Add(_mapOrderProducts.MapToLibrary(OrderProducts)));
        }

        public ResponseBase Delete(int ID)
        {
            return _mapResponseBase.MapToUI(_orderProducts.Delete(ID));
        }

        public Generic<OrderProducts_Models> GetAll()
        {
            var OrderProducts = _orderProducts.GetAll();
            Generic<OrderProducts_Models> model = new Generic<OrderProducts_Models>();
            model.ResponseInt = OrderProducts.ResponseInt;
            model.ResponseListInt = OrderProducts.ResponseListInt;
            model.ResponseListString = OrderProducts.ResponseListString;
            model.ResponseMessage = OrderProducts.ResponseMessage;
            model.ResponseString = OrderProducts.ResponseString;
            model.ResponseSuccess = OrderProducts.ResponseSuccess;
            foreach (var item in OrderProducts.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderProducts.MapToUI(item));
            }
            return model;
        }

        public Generic<OrderProducts_Models> GetAllByByOrderID(int OrderID)
        {
            var OrderProducts = _orderProducts.GetAllByByOrderID(OrderID);
            Generic<OrderProducts_Models> model = new Generic<OrderProducts_Models>();
            model.ResponseInt = OrderProducts.ResponseInt;
            model.ResponseListInt = OrderProducts.ResponseListInt;
            model.ResponseListString = OrderProducts.ResponseListString;
            model.ResponseMessage = OrderProducts.ResponseMessage;
            model.ResponseString = OrderProducts.ResponseString;
            model.ResponseSuccess = OrderProducts.ResponseSuccess;
            foreach (var item in OrderProducts.GenericClassList)
            {
                model.GenericClassList.Add(_mapOrderProducts.MapToUI(item));
            }
            return model;
        }

        public Generic<OrderProducts_Models> GetByID(int ID)
        {
            var OrderProducts = _orderProducts.GetByID(ID);
            Generic<OrderProducts_Models> model = new Generic<OrderProducts_Models>();
            model.ResponseInt = OrderProducts.ResponseInt;
            model.ResponseListInt = OrderProducts.ResponseListInt;
            model.ResponseListString = OrderProducts.ResponseListString;
            model.ResponseMessage = OrderProducts.ResponseMessage;
            model.ResponseString = OrderProducts.ResponseString;
            model.ResponseSuccess = OrderProducts.ResponseSuccess;
            model.GenericClass = _mapOrderProducts.MapToUI(OrderProducts.GenericClass);
            return model;
        }

        public ResponseBase Update(OrderProducts_Models OrderProducts)
        {
            return _mapResponseBase.MapToUI(_orderProducts.Update(_mapOrderProducts.MapToLibrary(OrderProducts)));
        }
    }
}
