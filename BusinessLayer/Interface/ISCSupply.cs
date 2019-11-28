using BusinessLayer.Models;
using BusinessLayer.Models.SCSupplyModels;
using System;

namespace BusinessLayer.Interface
{
    public interface ISCSupply
    {

        ResponseBase Add(SCSupply_Models sc_Supply);
        ResponseBase Update(SCSupply_Models sc_Supply);
        ResponseBase Delete(int ID);
        Generic<SCSupply_Models> GetAll();
        Generic<SCSupply_Models> GetByID(int ID);
        Generic<SCSupply_Models> GetAllByRange(DateTime StartDate, DateTime EndDate);
    }
}
