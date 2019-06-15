﻿using BusinessLayer.Models.ProductModels;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapProduct
    {
        public Product MapToLibrary(Product_Models model)
        {
            Product returnModel = new Product();           
            returnModel.CartGram = model.CartGram;
            returnModel.Description = model.Description;
            returnModel.Dominant = model.Dominant;
            returnModel.ID = model.ID;
            returnModel.IsActive = model.IsActive;
            returnModel.PricePerUnit = model.PricePerUnit;
            returnModel.ProductGroup = model.ProductGroup;
            returnModel.ProductImage = model.ProductImage;
            returnModel.Strain = model.Strain;
            returnModel.Type = model.Type;
            return returnModel;
        }

        public Product_Models MapToUI(Product model)
        {
            Product_Models returnModel = new Product_Models();           
            returnModel.CartGram = model.CartGram;
            returnModel.Description = model.Description;
            returnModel.Dominant = model.Dominant;
            returnModel.ID = model.ID;
            returnModel.IsActive = model.IsActive;
            returnModel.PricePerUnit = model.PricePerUnit;
            returnModel.ProductGroup = model.ProductGroup;
            returnModel.ProductImage = model.ProductImage;
            returnModel.Strain = model.Strain;
            returnModel.Type = model.Type;
            return returnModel;
        }
    }
}
