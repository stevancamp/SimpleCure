using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCure.Models.LotPurchasedModels
{
    public class ViewLotsPurchased_ViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Lot_Set { get; set; }
        [Required]
        public DateTime? BuyDate { get; set; }
        [Required]
        public int? Provider { get; set; }
        [Required]
        public decimal? Cost { get; set; }
        [Required]
        public double? Pounds { get; set; }
        [Required]
        public double? Grams { get; set; }
        public DateTime? EnterDate { get; set; }
        [Required]
        public string Strains { get; set; }
        public string Notes { get; set; }
        [Required]
        public string BudTrim { get; set; }
        public int? SatPackages { get; set; }
        public int? IndPackages { get; set; }
        [Required]
        public decimal? PricePerPound { get; set; }
        [Required]
        public decimal? PricePerGram { get; set; }
        public bool Complete { get; set; }
        public bool CBD { get; set; }
    }
}