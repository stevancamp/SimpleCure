using BusinessLayer.Models.ProductGroupModels;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapProductGroup
    {
        public ProductGroup MapToLibrary(ProductGroup_Models model)
        {
            ProductGroup returnModel = new ProductGroup();
            returnModel.GroupName = model.GroupName;
            returnModel.ID = model.ID;
            return returnModel;
        }

        public ProductGroup_Models MapToUI(ProductGroup model)
        {
            ProductGroup_Models returnModel = new ProductGroup_Models();
            returnModel.GroupName = model.GroupName;
            returnModel.ID = model.ID;
            return returnModel;
        }
    }
}
