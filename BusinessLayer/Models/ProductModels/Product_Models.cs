﻿namespace BusinessLayer.Models.ProductModels
{
    public class Product_Models
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Strain { get; set; }        
        public string Dominant { get; set; }
        public decimal CartGram { get; set; }
        public string ProductGroup { get; set; }
        public decimal PricePerUnit { get; set; }
        public byte[] ProductImage { get; set; }
        public bool IsActive { get; set; }
    }
}
