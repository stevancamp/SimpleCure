using System;

namespace BusinessLayer.Models.SCSupplyModels
{
    public class SCSupply_Models
    {
        public int ID { get; set; }
        public int? Customer { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string Strain { get; set; }
        public double? Amount { get; set; }
        public string Unit { get; set; }
        public double? Cost { get; set; }
        public double? THC { get; set; }
        public double? THCA { get; set; }
        public double? D9THC { get; set; }
        public double? TotalPotTHC { get; set; }
        public double? CBD { get; set; }
        public double? CBG { get; set; }
        public double? CBN { get; set; }
        public double? CBC { get; set; }
        public DateTime? TestDate { get; set; }
        public string Tester { get; set; }
        public string ProductNotes { get; set; }
        public string ExtractionNotes { get; set; }
        public string ExtractionLog { get; set; }
        public string MainBatch { get; set; }
        public string Batch { get; set; }
        public double? Waste { get; set; }
        public int? CartsCreated { get; set; }
        public double? CartSize { get; set; }
        public DateTime? EnterDate { get; set; }
        public int? LotNumber { get; set; }
        public double? GramsCartableOil { get; set; }
        public double? RefinedTerps { get; set; }
        public double? GramsAfterDecarb { get; set; }
        public double? GramsAfterTerp { get; set; }
        public double? GramsTerpenesReintroduced { get; set; }
        public double? CentrifugeMin { get; set; }
        public bool Split { get; set; }
        public string SplitNote { get; set; }
        public double? CandyGrams { get; set; }
        public string CandyMaker { get; set; }
        public int SquareCreated { get; set; }
        public double SquareSize { get; set; }
    }
}
