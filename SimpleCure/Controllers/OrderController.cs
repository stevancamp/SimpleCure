using BusinessLayer.Functions.Orders;
using BusinessLayer.Functions.Types;
using BusinessLayer.Models.OrderModels;
using SimpleCure.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleCure.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        #region Injection

        private TypeFunctions _typeFunctions;
        private OrderFunctions _orderFunctions;

        public OrderController()
        {
            _typeFunctions = new TypeFunctions();
            _orderFunctions = new OrderFunctions();
        }

        #endregion

        public ActionResult NewOrder()
        {
            NewOrder_ViewModel model = new NewOrder_ViewModel();

            model.OrderInfoProductTypesModel = _typeFunctions.GetAllOrderInfoProductTypesWithGroupName(true).GenericClassList;
            model.ListBusinessTypes = _typeFunctions.GetAllBusinessTypes(true).GenericClassList;

            return View(model);
        }

        [HttpPost]
        public int SubmitOrderInfo(string CompanyName, string ContactName, string OMMANumber, string EINNumber, string OBNDDNumber, string PhoneNumber, string EmailAddress, string StreetAddress, string BusinessTypes, string Notes)
        {
            int NewID = 0;
            try
            {
                Order_Model model = new Order_Model();
                model.BusinessType = string.Empty;
                model.CompanyName = CompanyName;
                model.Completed = false;
                model.CompletionNotes = string.Empty;
                model.ContactName = ContactName;
                model.EINNumber = EINNumber;
                model.EmailAddress = EmailAddress;
                model.Notes = Notes;
                model.OBNDDNumber = OBNDDNumber;
                model.OMMANumber = OMMANumber;
                model.OrderSubmissionDate = DateTime.Now;
                model.PhoneNumber = PhoneNumber;
                model.StreetAddress = StreetAddress;
                var Response = _orderFunctions.AddOrder(model);
                NewID = Response.ResponseInt;
            }
            catch (Exception ex)
            {

                throw;
            }

            return NewID;
        }
        
        [HttpPost]
        public bool SubmitProductInfo(int OrderInfoID, int Type, int Quantity)
        {
            var Success = false;
            OrderProduct_Model model = new OrderProduct_Model();
            model.OrderInfoID = OrderInfoID;
            model.Type = Type;
            model.Quantity = Quantity;
            try
            {
                Success = _orderFunctions.AddOrderProduct(model).ResponseSuccess;
            }
            catch (Exception ex)
            {

                throw;
            }

            return Success;
        }

        [HttpPost]
        public bool SubmitOrderHistory(int OrderID)
        {
            var Success = false;
            OrderActivityHistory_Model model = new OrderActivityHistory_Model();
            model.Notes = "New Order submitted";
            model.OrderActivityTypeID = 1;
            model.OrderID = OrderID;
            model.TimeStamp = DateTime.Now;
            try
            {
               Success = _orderFunctions.AddOrderHistory(model).ResponseSuccess;
            }
            catch (Exception ex)
            {

                throw;
            }
            return Success;
        }

        //[HttpPost]
        //public ActionResult SubmitOrder(NewOrder_ViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return View(); 

        //    }
        //}

        //Edit a Order

        //

        //view orders

        public ActionResult ViewOrders(string searchTerm = null)
        {
            ViewOrdersViewModel model = new ViewOrdersViewModel();


            try
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    model.ListOrders = _orderFunctions.GetOrdersBySearchParams(searchTerm, false).GenericClassList;
                }
                else
                {
                    model.ListOrders = _orderFunctions.GetAllOrders(false).GenericClassList;
                }
            }
            catch (Exception ex)
            {

                throw;
            }




            model.SearchTerm = string.Empty;
            return View(model);
        }

        //delete order

    }
}