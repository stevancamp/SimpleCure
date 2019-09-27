using BusinessLayer.Models;
using BusinessLayer.Models.SuggestionWorkLog;

namespace BusinessLayer.Interface
{
    public interface ISuggestionWorkLog
    {
        ResponseBase Add(SuggestionWorkLog_Model suggestionWorkLog);
        ResponseBase Update(SuggestionWorkLog_Model suggestionWorkLog);
        ResponseBase Delete(int ID);
        Generic<SuggestionWorkLog_Model> GetAll();
        Generic<SuggestionWorkLog_Model> GetAllBySuggestionID(int SuggestionID);
        Generic<SuggestionWorkLog_Model> GetByID(int ID);             
    }
}
