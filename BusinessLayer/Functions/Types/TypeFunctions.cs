using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.TypeModels;
using Library.Types.Methods;

namespace BusinessLayer.Functions.Types
{
    public class TypeFunctions : IType
    {
        #region Injection
        private Business_Type _business_Type;       
        private MapBusinessTypes _mapBusinessTypes;         
        private MapResponseBase _mapResponseBase;

        public TypeFunctions()
        {
            _business_Type = new Business_Type();           
            _mapBusinessTypes = new MapBusinessTypes();           
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
        
    }
}
