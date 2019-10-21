using BusinessLayer.Models;
using BusinessLayer.Models.SuggestionStatus;

namespace BusinessLayer.Interface
{
    public interface ISuggestionStatus
    {      
        Generic<SuggestionStatus_Model> GetAll();        
    }
}
