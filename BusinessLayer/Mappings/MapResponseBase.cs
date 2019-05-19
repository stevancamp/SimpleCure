using BusinessLayer.Models;

namespace BusinessLayer.Mappings
{
    public class MapResponseBase
    {
        public Library.ResponseBase MapToLibrary(Models.ResponseBase model)
        {
            Library.ResponseBase response = new Library.ResponseBase();
            response.ResponseInt = model.ResponseInt;
            response.ResponseListInt = model.ResponseListInt;
            response.ResponseListString = model.ResponseListString;
            response.ResponseMessage = model.ResponseMessage;
            response.ResponseString = model.ResponseString;
            response.ResponseSuccess = model.ResponseSuccess;

            switch (model.responseTypes)
            {
                case ResponseTypes.Success:
                    response.responseTypes = Library.ResponseTypes.Success;
                    break;
                case ResponseTypes.Failure:
                    response.responseTypes = Library.ResponseTypes.Failure;
                    break;
                case ResponseTypes.Information:
                    response.responseTypes = Library.ResponseTypes.Information;
                    break;
                default:
                    response.responseTypes = Library.ResponseTypes.Failure;
                    break;
            }

            return response; 
        }

        public ResponseBase MapToUI(Library.ResponseBase model)
        {
            ResponseBase response = new ResponseBase();
            response.ResponseInt = model.ResponseInt;
            response.ResponseListInt = model.ResponseListInt;
            response.ResponseListString = model.ResponseListString;
            response.ResponseMessage = model.ResponseMessage;
            response.ResponseString = model.ResponseString;
            switch (model.responseTypes)
            {
                case Library.ResponseTypes.Success:
                    response.responseTypes = ResponseTypes.Success;
                    break;
                case Library.ResponseTypes.Failure:
                    response.responseTypes = ResponseTypes.Failure;
                    break;
                case Library.ResponseTypes.Information:
                    response.responseTypes = ResponseTypes.Information;
                    break;
                default:
                    response.responseTypes = ResponseTypes.Failure;
                    break;
            }
            response.ResponseSuccess = model.ResponseSuccess;                   
            return response;
        }      
    }
}
