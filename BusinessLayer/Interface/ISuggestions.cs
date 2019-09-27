using BusinessLayer.Models;
using BusinessLayer.Models.Suggestions;

namespace BusinessLayer.Interface
{
    public interface ISuggestions
    {
        ResponseBase Add(Suggestions_Model suggestion);
        ResponseBase Update(Suggestions_Model suggestion);
        ResponseBase Delete(int ID);
        Generic<Suggestions_Model> GetAll();
        Generic<Suggestions_Model> GetAllByIsActive(bool IsActive);
        Generic<Suggestions_Model> GetByID(int ID);       
    }
}
