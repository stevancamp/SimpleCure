using BusinessLayer.Models;
using System;

namespace BusinessLayer.Interface
{
    public interface IApplicationErrors
    {
        ResponseBase Log(string ErrorMessage, string IPAddress);

        Generic<ErrorModel> GetAll();

        Generic<ErrorModel> GetByID(int ID);

        Generic<ErrorModel> GetByDate(DateTime date);
    }
}
