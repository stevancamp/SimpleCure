using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.ProductGroupModels;
using Library._ProductGroup.Methods;
using System;

namespace BusinessLayer.Functions.ProductGroup
{
    public class ProductGroupFunctions : IProductGroup
    {

        #region Injection
        private _ProductGroup _productGroup;
        private MapProductGroup _mapProductGroup;
        private MapResponseBase _mapResponseBase;

        public ProductGroupFunctions()
        {
            _productGroup = new _ProductGroup();
            _mapProductGroup = new MapProductGroup();
            _mapResponseBase = new MapResponseBase();

        }
        #endregion

        public ResponseBase Add(ProductGroup_Models ProductGroup)
        {
            return _mapResponseBase.MapToUI(_productGroup.Add(_mapProductGroup.MapToLibrary(ProductGroup)));
        }

        public ResponseBase Delete(int ID)
        {
            return _mapResponseBase.MapToUI(_productGroup.Delete(ID));
        }

        public Generic<ProductGroup_Models> GetAll()
        {
            var ProductGroups = _productGroup.GetAll();
            Generic<ProductGroup_Models> model = new Generic<ProductGroup_Models>();
            model.ResponseInt = ProductGroups.ResponseInt;
            model.ResponseListInt = ProductGroups.ResponseListInt;
            model.ResponseListString = ProductGroups.ResponseListString;
            model.ResponseMessage = ProductGroups.ResponseMessage;
            model.ResponseString = ProductGroups.ResponseString;
            model.ResponseSuccess = ProductGroups.ResponseSuccess;
            foreach (var item in ProductGroups.GenericClassList)
            {
                model.GenericClassList.Add(_mapProductGroup.MapToUI(item));
            }
            return model;
        }

        public Generic<ProductGroup_Models> GetAllByIsActive(bool IsActive)
        {
            var ProductGroups = _productGroup.GetAllByIsActive(IsActive);
            Generic<ProductGroup_Models> model = new Generic<ProductGroup_Models>();
            model.ResponseInt = ProductGroups.ResponseInt;
            model.ResponseListInt = ProductGroups.ResponseListInt;
            model.ResponseListString = ProductGroups.ResponseListString;
            model.ResponseMessage = ProductGroups.ResponseMessage;
            model.ResponseString = ProductGroups.ResponseString;
            model.ResponseSuccess = ProductGroups.ResponseSuccess;
            foreach (var item in ProductGroups.GenericClassList)
            {
                model.GenericClassList.Add(_mapProductGroup.MapToUI(item));
            }
            return model;
        }

        public Generic<ProductGroup_Models> GetByID(int ID)
        {
            var ProductGroups = _productGroup.GetByID(ID);
            Generic<ProductGroup_Models> model = new Generic<ProductGroup_Models>();
            model.ResponseInt = ProductGroups.ResponseInt;
            model.ResponseListInt = ProductGroups.ResponseListInt;
            model.ResponseListString = ProductGroups.ResponseListString;
            model.ResponseMessage = ProductGroups.ResponseMessage;
            model.ResponseString = ProductGroups.ResponseString;
            model.ResponseSuccess = ProductGroups.ResponseSuccess;
            model.GenericClass = _mapProductGroup.MapToUI(ProductGroups.GenericClass);
            return model;
        }

        public ResponseBase Update(ProductGroup_Models ProductGroup)
        {
            return _mapResponseBase.MapToUI(_productGroup.Update(_mapProductGroup.MapToLibrary(ProductGroup)));
        }
    }
}
