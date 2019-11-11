using BusinessLayer.Models.LotsPurchasedModels;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapLotsPurchased
    {
        public Tbl_Lots_Purchased MapToLibrary(LotsPurchased_Models model)
        {
            Tbl_Lots_Purchased returnModel = new Tbl_Lots_Purchased();
            returnModel.BudTrim = model.BudTrim;
            returnModel.BuyDate = model.BuyDate;
            returnModel.CBD = model.CBD;
            returnModel.Complete = model.Complete;
            returnModel.Cost = model.Cost;
            returnModel.EnterDate = model.EnterDate;
            returnModel.Grams = model.Grams;
            returnModel.ID = model.ID;
            returnModel.IndPackages = model.IndPackages;
            returnModel.Lot_Set = model.Lot_Set;
            returnModel.Notes = model.Notes;
            returnModel.Pounds = model.Pounds;
            returnModel.PricePerGram = model.PricePerGram;
            returnModel.PricePerPound = model.PricePerPound;
            returnModel.Provider = model.Provider;
            returnModel.SatPackages = model.SatPackages;
            returnModel.Strains = model.Strains;
            returnModel.TransportID = model.TransportID;
            returnModel.IsSimpleCure = model.IsSimpleCure;
            returnModel.CompletionDate = model.CompletionDate;
            returnModel.CompletedBy = model.CompletedBy;
            returnModel.To_From = model.To_From;
            returnModel.Split = model.Split;
            returnModel.SplitNotes = model.SplitNotes;
            returnModel.TransportLocationEnd = model.TransportLocationEnd;
            returnModel.TransportLocationStart = model.TransportLocationStart;
            return returnModel;
        }

        public LotsPurchased_Models MapToUI(Tbl_Lots_Purchased model)
        {
            LotsPurchased_Models returnModel = new LotsPurchased_Models();
            returnModel.BudTrim = model.BudTrim;
            returnModel.BuyDate = model.BuyDate;
            returnModel.CBD = model.CBD;
            returnModel.Complete = model.Complete;
            returnModel.Cost = model.Cost;
            returnModel.EnterDate = model.EnterDate;
            returnModel.Grams = model.Grams;
            returnModel.ID = model.ID;
            returnModel.IndPackages = model.IndPackages;
            returnModel.Lot_Set = model.Lot_Set;
            returnModel.Notes = model.Notes;
            returnModel.Pounds = model.Pounds;
            returnModel.PricePerGram = model.PricePerGram;
            returnModel.PricePerPound = model.PricePerPound;
            returnModel.Provider = model.Provider;
            returnModel.SatPackages = model.SatPackages;
            returnModel.Strains = model.Strains;
            returnModel.TransportID = model.TransportID;
            returnModel.IsSimpleCure = model.IsSimpleCure;
            returnModel.CompletionDate = model.CompletionDate;
            returnModel.CompletedBy = model.CompletedBy;
            returnModel.To_From = model.To_From;
            returnModel.Split = model.Split;
            returnModel.SplitNotes = model.SplitNotes;
            returnModel.TransportLocationEnd = model.TransportLocationEnd;
            returnModel.TransportLocationStart = model.TransportLocationStart;
            return returnModel;
        }
    }
}
