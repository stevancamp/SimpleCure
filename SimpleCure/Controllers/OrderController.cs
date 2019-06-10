using BusinessLayer.Functions.Customer;
using BusinessLayer.Functions.Discount;
using BusinessLayer.Functions.ErrorLogging;
using BusinessLayer.Functions.Order;
using BusinessLayer.Functions.OrderActivity;
using BusinessLayer.Functions.OrderDiscount;
using BusinessLayer.Functions.OrderProducts;
using BusinessLayer.Functions.OrderStatus;
using BusinessLayer.Functions.Product;
using BusinessLayer.Models.DiscountModels;
using BusinessLayer.Models.OrderActivityModels;
using BusinessLayer.Models.OrderDiscountModels;
using BusinessLayer.Models.OrderModels;
using BusinessLayer.Models.OrderProductsModels;
using BusinessLayer.Models.ProductModels;
using SimpleCure.Models;
using SimpleCure.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SimpleCure.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        #region Injection
        private LoggerFunctions _loggerFunctions;

        private OrderFunctions _orderFunctions;
        private OrderActivityFunctions _orderActivityFunctions;
        private OrderProductsFunctions _orderProductsFucntions;
        private OrderStatusFunctions _orderStatusFunctions;
        private OrderDiscountFunctions _orderDiscountFucntions;
        private DiscountFunctions _discountFunctions;
        private CustomerFunctions _customerFunctions;
        private ProductFunctions _productFucntions;
 
        public OrderController()
        {
            _loggerFunctions = new LoggerFunctions();

            _orderFunctions = new OrderFunctions();
            _orderActivityFunctions = new OrderActivityFunctions();
            _orderProductsFucntions = new OrderProductsFunctions();
            _orderStatusFunctions = new OrderStatusFunctions();
            _orderDiscountFucntions = new OrderDiscountFunctions();
            _discountFunctions = new DiscountFunctions();
            _customerFunctions = new CustomerFunctions();
            _productFucntions = new ProductFunctions();
 
        }
        #endregion

        //create order
        public ActionResult CreateOrder(CreateOrder_ViewModel model)
        {
            
            var Products = _productFucntions.GetAllByIsActive(true);
          
            var Discounts = _discountFunctions.GetAllByIsActive(true);
          
            var Customers = _customerFunctions.GetCustomersList();
 
            model.ListCustomers = Customers.GenericClassList;
            model.ListDiscounts = Discounts.GenericClassList;
            model.ListProducts = Products.GenericClassList;

            return View(model);

        }
 
        [HttpPost]
        public ActionResult SaveOrder(string Notes, string CustomerID, List<ProductsList> ListProductsToSubmit, List<DiscountIDList> ListDiscountIDs)
        //public ActionResult SaveOrder(CreateOrder_ViewModel Data)
        {
            ApplicationUser user = new ApplicationUser();
 
            Order_Models order_Models = new Order_Models();
            order_Models.Notes = Notes;
            order_Models.OrderStatus = "Created";
            order_Models.SubmissionDate = DateTime.Now;
            order_Models.Tbl_CustomerID = Convert.ToInt32(CustomerID);
            order_Models.CompletionDate = null;
            var OrderAdd = _orderFunctions.Add(order_Models);
 
            OrderActivity_Models orderActivity_Models = new OrderActivity_Models();
            orderActivity_Models.ActivityBy = user.Id;
            orderActivity_Models.ActivityDate = DateTime.Now;
            orderActivity_Models.Notes = Notes;
            orderActivity_Models.OrderID = OrderAdd.ResponseInt;
            orderActivity_Models.Status = "Created";
            var ActivityAdd = _orderActivityFunctions.Add(orderActivity_Models);
 
            if (ListProductsToSubmit != null && ListProductsToSubmit.Count > 0)
            {

                foreach (var item in ListProductsToSubmit)
                {
 
                    var Product = _productFucntions.GetByID(item.ProductID).GenericClass;

                    OrderProducts_Models orderProducts_Models = new OrderProducts_Models();
                    orderProducts_Models.BatchID = Product.BatchID;
                    orderProducts_Models.EntryBy = user.Id;
                    orderProducts_Models.EntryDate = DateTime.Now;
                    orderProducts_Models.OrderID = OrderAdd.ResponseInt;
                    orderProducts_Models.ProductID = item.ProductID;
                    orderProducts_Models.Quantity = item.Quantity;
                    orderProducts_Models.Total = item.Quantity * Product.PricePerGram;
                    var OrderProductAdd = _orderProductsFucntions.Add(orderProducts_Models);

                }
            }
 
            if (ListDiscountIDs != null && ListDiscountIDs.Count > 0)
            {
                foreach (var item in ListDiscountIDs)
                {
                    
                    OrderDiscount_Models orderDiscount_Models = new OrderDiscount_Models();
                    orderDiscount_Models.DiscountID = item.DiscountID;
                    orderDiscount_Models.OrderID = OrderAdd.ResponseInt;                   
                    var OrderDiscountsAdd = _orderDiscountFucntions.Add(orderDiscount_Models);

                }
               
            }

            return RedirectToAction("CreateOrder", new CreateOrder_ViewModel { ResponseMessage = "Order Created successfully.", ResponseSuccess = true, responseTypes = ResponseTypes.Success});
           
        }

        //edit order
        public ActionResult EditOrder(int ID)
        {
            EditOrder_ViewModel model = new EditOrder_ViewModel();



            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder(EditOrder_ViewModel model)
        {
            throw new NotImplementedException();
        }
        //view order
        public ActionResult ViewOrder(int ID)
        {
            ViewOrder_ViewModel model = new ViewOrder_ViewModel();
            return View(model);
        }

        //count of orders by status

        //delete order
        [HttpPost]
        public ActionResult DeleteOrder(int ID)
        {
            throw new NotImplementedException();
        }
         
        //list all orders by customer with search option parameters

        public ActionResult Orders(string SearchTerm = null, string Status = null)
        {
            Orders_ViewModel model = new Orders_ViewModel();
               

            return View(model);
        }
        



    }
}