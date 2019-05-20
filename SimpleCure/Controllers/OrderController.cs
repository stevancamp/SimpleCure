using BusinessLayer.Functions.Types;
using SimpleCure.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleCure.Controllers
{
    public class OrderController : Controller
    {
        #region Injection

        private TypeFunctions _typeFunctions;

        public OrderController()
        {
            _typeFunctions = new TypeFunctions();
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
        public ActionResult SubmitOrder(FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            else
            {
                return View(); 
                    
            }
        }

        //Edit a Order

        //

        //view orders

        //delete order
       
    }
}