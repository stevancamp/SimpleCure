using Library.DataModel;
using SimpleCure.Models.TypeModels;

namespace BusinessLayer.Mappings
{
    public class MapBusinessTypes
    {
        public BusinessType MapToLibrary(BusinessTypeModel model)
        {
            BusinessType businessType = new BusinessType();
            businessType.ID = model.ID;
            businessType.IsActvie = model.IsActvie;
            businessType.Type = model.Type;

            return businessType; 
        }

        public BusinessTypeModel MapToUI(BusinessType model)
        {
            BusinessTypeModel businessTypeModel = new BusinessTypeModel();
            businessTypeModel.ID = model.ID;
            businessTypeModel.IsActvie = model.IsActvie;
            businessTypeModel.Type = model.Type;

            return businessTypeModel;
        }
    }
}
