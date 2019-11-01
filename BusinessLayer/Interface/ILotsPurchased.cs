using BusinessLayer.Models;
using BusinessLayer.Models.LotsPurchasedModels;
using System;

namespace BusinessLayer.Interface
{
    public interface ILotsPurchased
    {
        ResponseBase Add(LotsPurchased_Models purchased);
        ResponseBase Update(LotsPurchased_Models purchased);
        ResponseBase Delete(int ID);
        Generic<LotsPurchased_Models> GetAll();
        Generic<LotsPurchased_Models> GetByID(int ID);
        Generic<LotsPurchased_Models> GetByLotSet(string LotSet);
        Generic<LotsPurchased_Models> GetByProvider(int Provider);
        Generic<LotsPurchased_Models> GetByComplete(bool IsComplete);
        Generic<LotsPurchased_Models> GetByCompleteByRange(DateTime StartTime, DateTime EndTime, bool IsComplete);
        Generic<LotsPurchased_Models> Search(int? ProviderID, string Lot_Set, DateTime? StartTime, DateTime? EndTime, bool? IsComplete);
    }
}
