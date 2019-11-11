using BusinessLayer.Models;
using BusinessLayer.Models.SharedModels;

namespace BusinessLayer.Interface
{
    public interface ISharedFunctions
    {
        Generic<Shared_Models> GetIndexNumbers();
    }
}
