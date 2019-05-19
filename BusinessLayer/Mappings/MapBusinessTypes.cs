using BusinessLayer.Models.TypeModels;
using Library.DataModel;
 
namespace BusinessLayer.Mappings
{
    public class MapBusinessTypes
    {
        public BusinessType MapToLibrary(BusinessType_Model model)
        {
            BusinessType businessType = new BusinessType();
            businessType.ID = model.ID;
            businessType.IsActive = model.IsActive;
            businessType.Type = model.Type;

            return businessType; 
        }

        public BusinessType_Model MapToUI(BusinessType model)
        {
            BusinessType_Model businessTypeModel = new BusinessType_Model();
            businessTypeModel.ID = model.ID;
            businessTypeModel.IsActive = model.IsActive;
            businessTypeModel.Type = model.Type;

            return businessTypeModel;
        }
    }
}
