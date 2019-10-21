using System;

namespace BusinessLayer.Models.LotsPurchasedModels
{
    public class LotsPurchased_Models
    {
        public int ID { get; set; }
        public string Lot_Set { get; set; }
        public DateTime? BuyDate { get; set; }
        public int? Provider { get; set; }
        public decimal? Cost { get; set; }
        public double? Pounds { get; set; }
        public double? Grams { get; set; }
        public DateTime? EnterDate { get; set; }
        public string Strains { get; set; }
        public string Notes { get; set; }
        public string BudTrim { get; set; }
        public int? SatPackages { get; set; }
        public int? IndPackages { get; set; }
        public decimal? PricePerPound { get; set; }
        public decimal? PricePerGram { get; set; }
        public bool Complete { get; set; }
        public bool CBD { get; set; }
    }
}
