using BusinessLayer.Models;
using BusinessLayer.Models.OrderProductsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IOrderProducts
    {
        ResponseBase Add(OrderProducts_Models OrderProducts);
        ResponseBase Update(OrderProducts_Models OrderProducts);
        ResponseBase Delete(int ID);
        Generic<OrderProducts_Models> GetAll();
        Generic<OrderProducts_Models> GetAllByByOrderID(int OrderID);
        Generic<OrderProducts_Models> GetByID(int ID);
    }
}
