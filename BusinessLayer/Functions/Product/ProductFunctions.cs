using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.ProductModels;
using Library._Product.Methods;

namespace BusinessLayer.Functions.Product
{
    public class ProductFunctions : IProduct
    {
        #region Injection
        private _Product _product;
        private MapProduct _mapProduct;
        private MapResponseBase _mapResponseBase;

        public ProductFunctions()
        {
            _product = new _Product();
            _mapProduct = new MapProduct();
            _mapResponseBase = new MapResponseBase();

        }
        #endregion


        public ResponseBase Add(Product_Models Product)
        {
            return _mapResponseBase.MapToUI(_product.Add(_mapProduct.MapToLibrary(Product)));
        }

        public ResponseBase Delete(int ID)
        {
            return _mapResponseBase.MapToUI(_product.Delete(ID));
        }

        public Generic<Product_Models> GetAll()
        {
            var Product = _product.GetAll();
            Generic<Product_Models> model = new Generic<Product_Models>();
            model.ResponseInt = Product.ResponseInt;
            model.ResponseListInt = Product.ResponseListInt;
            model.ResponseListString = Product.ResponseListString;
            model.ResponseMessage = Product.ResponseMessage;
            model.ResponseString = Product.ResponseString;
            model.ResponseSuccess = Product.ResponseSuccess;
            foreach (var item in Product.GenericClassList)
            {
                model.GenericClassList.Add(_mapProduct.MapToUI(item));
            }
            return model;
        }

        public Generic<Product_Models> GetAllByIsActive(bool IsActive)
        {
            var Product = _product.GetAllByIsActive(IsActive);
            Generic<Product_Models> model = new Generic<Product_Models>();
            model.ResponseInt = Product.ResponseInt;
            model.ResponseListInt = Product.ResponseListInt;
            model.ResponseListString = Product.ResponseListString;
            model.ResponseMessage = Product.ResponseMessage;
            model.ResponseString = Product.ResponseString;
            model.ResponseSuccess = Product.ResponseSuccess;
            foreach (var item in Product.GenericClassList)
            {
                model.GenericClassList.Add(_mapProduct.MapToUI(item));
            }
            return model;
        }

        public Generic<Product_Models> GetByID(int ID)
        {
            var Product = _product.GetByID(ID);
            Generic<Product_Models> model = new Generic<Product_Models>();
            model.ResponseInt = Product.ResponseInt;
            model.ResponseListInt = Product.ResponseListInt;
            model.ResponseListString = Product.ResponseListString;
            model.ResponseMessage = Product.ResponseMessage;
            model.ResponseString = Product.ResponseString;
            model.ResponseSuccess = Product.ResponseSuccess;
            model.GenericClass = _mapProduct.MapToUI(Product.GenericClass);
            return model;
        }

        public ResponseBase Update(Product_Models Product)
        {
            return _mapResponseBase.MapToUI(_product.Update(_mapProduct.MapToLibrary(Product)));
        }
    }
}
