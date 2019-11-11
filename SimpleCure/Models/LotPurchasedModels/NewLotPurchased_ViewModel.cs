using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCure.Models.LotPurchasedModels
{
    public class NewLotPurchased_ViewModel
    {
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
        public bool Split { get; set; }
        public string SplitNotes { get; set; }

        public string TransportID { get; set; }
        public string TransportLocationStart { get; set; }
        public string TransportLocationEnd { get; set; }
        public bool IsSimpleCure { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string CompletedBy { get; set; }
        public string To_From { get; set; }
    }
} 