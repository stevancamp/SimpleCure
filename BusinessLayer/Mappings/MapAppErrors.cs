using Library.DataModel;
using SimpleCure.Models;

namespace BusinessLayer.Mappings
{
    public class MapAppErrors
    {
        public AppError MapToLibrary(ErrorModel model)
        {
            AppError appError = new AppError();
            appError.ErrorMessage = model.ErrorMessage;
            appError.ErrorTime = model.ErrorTime;
            appError.ID = model.ID;
            appError.IP_Address = model.IP_Address;

            return appError;
        }

        public ErrorModel MapToUI(AppError model)
        {
            ErrorModel errorModel = new ErrorModel();
            errorModel.ErrorMessage = model.ErrorMessage;
            errorModel.ErrorTime = model.ErrorTime;
            errorModel.ID = model.ID;
            errorModel.IP_Address = model.IP_Address;

            return errorModel;
        }
    }
}
