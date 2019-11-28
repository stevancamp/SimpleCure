using BusinessLayer.Models.SCSupplyModels;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapSCSuppy
    {
        public Tbl_SC_Supply MapToLibrary(SCSupply_Models model)
        {
            Tbl_SC_Supply returnModel = new Tbl_SC_Supply();
            returnModel.Amount = model.Amount;
            returnModel.Batch = model.Batch;
            returnModel.CandyGrams = model.CandyGrams;
            returnModel.CandyMaker = model.CandyMaker;
            returnModel.CartsCreated = model.CartsCreated;
            returnModel.CartSize = model.CartSize;
            returnModel.CBC = model.CBC;
            returnModel.CBD = model.CBD;
            returnModel.CBG = model.CBG;
            returnModel.CBN = model.CBN;
            returnModel.CentrifugeMin = model.CentrifugeMin;
            returnModel.Cost = model.Cost;
            returnModel.Customer = model.Customer;
            returnModel.D9THC = model.D9THC;
            returnModel.EnterDate = model.EnterDate;
            returnModel.ExtractionLog = model.ExtractionLog;
            returnModel.ExtractionNotes = model.ExtractionNotes;
            returnModel.GramsAfterDecarb = model.GramsAfterDecarb;
            returnModel.GramsAfterTerp = model.GramsAfterTerp;
            returnModel.GramsCartableOil = model.GramsCartableOil;
            returnModel.GramsTerpenesReintroduced = model.GramsTerpenesReintroduced;
            returnModel.ID = model.ID;
            returnModel.LotNumber = model.LotNumber;
            returnModel.MainBatch = model.MainBatch;
            returnModel.ProcessDate = model.ProcessDate;
            returnModel.ProductNotes = model.ProductNotes;
            returnModel.RefinedTerps = model.RefinedTerps;
            returnModel.Split = model.Split;
            returnModel.SplitNote = model.SplitNote;
            returnModel.SquareCreated = model.SquareCreated;
            returnModel.SquareSize = model.SquareSize;
            returnModel.Strain = model.Strain;
            returnModel.TestDate = model.TestDate;
            returnModel.Tester = model.Tester;
            returnModel.THC = model.THC;
            returnModel.THCA = model.THCA;
            returnModel.TotalPotTHC = model.TotalPotTHC;
            returnModel.Unit = model.Unit;
            returnModel.Waste = model.Waste;
            return returnModel;
        }

        public SCSupply_Models MapToUI(Tbl_SC_Supply model)
        {
            SCSupply_Models returnModel = new SCSupply_Models();
            returnModel.Amount = model.Amount;
            returnModel.Batch = model.Batch;
            returnModel.CandyGrams = model.CandyGrams;
            returnModel.CandyMaker = model.CandyMaker;
            returnModel.CartsCreated = model.CartsCreated;
            returnModel.CartSize = model.CartSize;
            returnModel.CBC = model.CBC;
            returnModel.CBD = model.CBD;
            returnModel.CBG = model.CBG;
            returnModel.CBN = model.CBN;
            returnModel.CentrifugeMin = model.CentrifugeMin;
            returnModel.Cost = model.Cost;
            returnModel.Customer = model.Customer;
            returnModel.D9THC = model.D9THC;
            returnModel.EnterDate = model.EnterDate;
            returnModel.ExtractionLog = model.ExtractionLog;
            returnModel.ExtractionNotes = model.ExtractionNotes;
            returnModel.GramsAfterDecarb = model.GramsAfterDecarb;
            returnModel.GramsAfterTerp = model.GramsAfterTerp;
            returnModel.GramsCartableOil = model.GramsCartableOil;
            returnModel.GramsTerpenesReintroduced = model.GramsTerpenesReintroduced;
            returnModel.ID = model.ID;
            returnModel.LotNumber = model.LotNumber;
            returnModel.MainBatch = model.MainBatch;
            returnModel.ProcessDate = model.ProcessDate;
            returnModel.ProductNotes = model.ProductNotes;
            returnModel.RefinedTerps = model.RefinedTerps;
            returnModel.Split = model.Split;
            returnModel.SplitNote = model.SplitNote;
            returnModel.SquareCreated = model.SquareCreated;
            returnModel.SquareSize = model.SquareSize;
            returnModel.Strain = model.Strain;
            returnModel.TestDate = model.TestDate;
            returnModel.Tester = model.Tester;
            returnModel.THC = model.THC;
            returnModel.THCA = model.THCA;
            returnModel.TotalPotTHC = model.TotalPotTHC;
            returnModel.Unit = model.Unit;
            returnModel.Waste = model.Waste;
            return returnModel;
        }
    }
}
