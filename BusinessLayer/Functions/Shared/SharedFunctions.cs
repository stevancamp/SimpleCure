using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.SharedModels;

namespace BusinessLayer.Functions.Shared
{
    public class SharedFunctions : ISharedFunctions
    {

        #region Injection
        private Library.Shared.Methods.Shared _Shared;
        private MapShared _mapShared;
        private MapResponseBase _mapResponseBase;

        public SharedFunctions()
        {
            _Shared = new Library.Shared.Methods.Shared();
            _mapShared = new MapShared();
            _mapResponseBase = new MapResponseBase();

        }
        #endregion
        public Generic<Shared_Models> GetIndexNumbers()
        {
            var Index = _Shared.GetIndexNumbers();
            Generic<Shared_Models> model = new Generic<Shared_Models>();
            model.ResponseInt = Index.ResponseInt;
            model.ResponseListInt = Index.ResponseListInt;
            model.ResponseListString = Index.ResponseListString;
            model.ResponseMessage = Index.ResponseMessage;
            model.ResponseString = Index.ResponseString;
            model.ResponseSuccess = Index.ResponseSuccess;
            model.GenericClass = _mapShared.MapToUI(Index.GenericClass);
            return model;
        }
    }
}
