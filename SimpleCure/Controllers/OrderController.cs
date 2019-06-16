using BusinessLayer.Functions.Customer;
using BusinessLayer.Functions.Discount;
using BusinessLayer.Functions.ErrorLogging;
using BusinessLayer.Functions.Order;
using BusinessLayer.Functions.OrderActivity;
using BusinessLayer.Functions.OrderDiscount;
using BusinessLayer.Functions.OrderProducts;
using BusinessLayer.Functions.OrderStatus;
using BusinessLayer.Functions.Product;
using BusinessLayer.Models.OrderActivityModels;
using BusinessLayer.Models.OrderDiscountModels;
using BusinessLayer.Models.OrderModels;
using BusinessLayer.Models.OrderProductsModels;
using Microsoft.AspNet.Identity;
using SimpleCure.Models;
using SimpleCure.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public int SaveOrder(string Notes, string CustomerID, List<ProductsList> ListProductsToSubmit, List<DiscountIDList> ListDiscountIDs)
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
                    orderProducts_Models.BatchID = item.BatchID;
                    orderProducts_Models.EntryBy = user.Id;
                    orderProducts_Models.EntryDate = DateTime.Now;
                    orderProducts_Models.OrderID = OrderAdd.ResponseInt;
                    orderProducts_Models.ProductID = item.ProductID;
                    orderProducts_Models.Quantity = item.Quantity;
                    orderProducts_Models.Total = item.Quantity * Product.PricePerUnit;
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

            return OrderAdd.ResponseInt;
            
        }

        public ActionResult ViewOrder(int ID)
        {
            var OrderInfo = _orderFunctions.GetByID(ID).GenericClass;           
            var OrderProducts = _orderProductsFucntions.GetAllByByOrderID(ID);
            var OrderDiscounts = _orderDiscountFucntions.GetAllByOrderID(ID);
            var OrderStatus = _orderStatusFunctions.GetAll();

            List<OrderProduct_Prodcut_Model> ListOrderProductProducts = new List<OrderProduct_Prodcut_Model>();
            foreach (var item in OrderProducts.GenericClassList)
            {
                
                var Product = _productFucntions.GetByID(item.ProductID);
                ListOrderProductProducts.Add(new OrderProduct_Prodcut_Model { ProductID = Product.GenericClass.ID, BatchID = item.BatchID, CartGram = Product.GenericClass.CartGram, Description = Product.GenericClass.Description, Dominant = Product.GenericClass.Dominant, EntryBy = item.EntryBy, EntryDate = item.EntryDate, ForProductID = item.ProductID, IsActive = Product.GenericClass.IsActive, OrderID = item.OrderID, OrderProductID = item.ProductID, PricePerUnit = Product.GenericClass.PricePerUnit, ProductGroup = Product.GenericClass.ProductGroup, ProductImage = Product.GenericClass.ProductImage, Quantity = item.Quantity, Strain = Product.GenericClass.Strain, Total = item.Total, Type = Product.GenericClass.Type });
            }

            List<OrderDiscountDiscounts_Model> ListOrderDiscounts = new List<OrderDiscountDiscounts_Model>();
            foreach (var item in OrderDiscounts.GenericClassList)
            {
                var Discount = _discountFunctions.GetByID(item.DiscountID);
                ListOrderDiscounts.Add(new OrderDiscountDiscounts_Model { DiscountAmount = Discount.GenericClass.DiscountAmount, DiscountID = item.DiscountID, IsActive = Discount.GenericClass.IsActive, OrderDiscountDiscountID = item.DiscountID, OrderDiscountID = item.ID, OrderID = item.OrderID, Type = Discount.GenericClass.Type });
            }
               
 
            return View(new ViewOrder_ViewModel { ListProducts = ListOrderProductProducts, OrderInfo = OrderInfo,  ListOrderDiscounts = ListOrderDiscounts, ListStatus = OrderStatus.GenericClassList });
        }

        public ActionResult ViewOrderInfo(int OrderID)
        {
            var OrderInfo = _orderFunctions.GetByID(OrderID).GenericClass;
            var CustomerInfo = _customerFunctions.GetByUserID(OrderInfo.Tbl_CustomerID);
            var OrderActivity = _orderActivityFunctions.GetByOrderID(OrderID);

            return PartialView("_ViewOrderInfo", new ViewOrderInfo_ViewModel { CustomerInfo = CustomerInfo.GenericClass, OrderActivity = OrderActivity.GenericClassList, OrderInfo = OrderInfo });
        }

        public ActionResult EditCurrentOrderStatus(int OrderID)
        {
            EditCurrentOrderStatus_ViewModel model = new EditCurrentOrderStatus_ViewModel();
            model.ListStatus = _orderStatusFunctions.GetAll().GenericClassList;
            return PartialView("_EditCurrentOrderStatus", model);
        }
        [HttpPost]
        public bool SaveOrderActivityStatus(int OrderID, string OrderActivityStatus, string OrderActivityNotes)
        {
            var AddedActivity = _orderActivityFunctions.Add(new OrderActivity_Models { ActivityBy = User.Identity.GetUserId(), ActivityDate = DateTime.Now, Notes = OrderActivityNotes, OrderID = OrderID, Status = OrderActivityStatus });

            if (AddedActivity.ResponseSuccess)
            {
                var Order = _orderFunctions.GetByID(OrderID);
                var UpdatedOrderStatus = _orderFunctions.Update(new Order_Models { CompletionDate = Order.GenericClass.CompletionDate, ID = OrderID, Notes = Order.GenericClass.Notes, OrderStatus = OrderActivityStatus, SubmissionDate = Order.GenericClass.SubmissionDate, Tbl_CustomerID = Order.GenericClass.Tbl_CustomerID });
                if (UpdatedOrderStatus.ResponseSuccess)
                {
                    return true;
                }
            }
            return false;
        }


        [HttpPost]
        public ActionResult DeleteOrder(int ID)
        {
            throw new NotImplementedException();
        }

        public ActionResult Orders(string SearchTerm = null)
        {
            List<Orders_ViewModel> model = new List<Orders_ViewModel>();
            if (SearchTerm != null)
            {
                var Order = _orderFunctions.GetAllByCompleted(false);
                foreach (var item in Order.GenericClassList)
                {
                    var Customer = _customerFunctions.GetByID(item.Tbl_CustomerID);
                    var OrderActivity = _orderActivityFunctions.GetByOrderID(item.ID).GenericClassList.LastOrDefault();
                    if (item.OrderStatus.ToLower().Contains(SearchTerm.ToLower()) || Customer.GenericClass.Customer.ToLower().Contains(SearchTerm.ToLower()) || Customer.GenericClass.Company.ToLower().Contains(SearchTerm.ToLower()))
                    {
                        Orders_ViewModel OVM = new Orders_ViewModel();
                        OVM.CompanyName = Customer.GenericClass.Company;
                        OVM.CustomerName = Customer.GenericClass.Customer;
                        OVM.LastActionDate = OrderActivity.ActivityDate;
                        OVM.OrderDate = item.SubmissionDate;
                        OVM.OrderID = item.ID;
                        OVM.OrderStatus = item.OrderStatus;
                        model.Add(OVM);
                    }
                }
            }
            else
            {
                var Order = _orderFunctions.GetAllByCompleted(false);
                foreach (var item in Order.GenericClassList)
                {
                    var Customer = _customerFunctions.GetByID(item.Tbl_CustomerID);
                    var OrderActivity = _orderActivityFunctions.GetByOrderID(item.ID).GenericClassList.LastOrDefault();
                    Orders_ViewModel OVM = new Orders_ViewModel();
                    OVM.CompanyName = Customer.GenericClass.Company;
                    OVM.CustomerName = Customer.GenericClass.Customer;
                    OVM.LastActionDate = OrderActivity.ActivityDate;
                    OVM.OrderDate = item.SubmissionDate;
                    OVM.OrderID = item.ID;
                    OVM.OrderStatus = item.OrderStatus;
                    model.Add(OVM);
                }
            }
            return View(model);
        }

       
    }
}