﻿using BusinessLayer.Functions.Customer;
using BusinessLayer.Functions.Discount;
using BusinessLayer.Functions.Email;
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
using Rotativa;
using SimpleCure.Models;
using SimpleCure.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
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
        private EmailFunctions _emailFunctions;

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
            _emailFunctions = new EmailFunctions();

        }
        #endregion

        public ActionResult CreateOrder(CreateOrder_ViewModel model)
        {
            var Products = _productFucntions.GetAllByIsActive(true);
            var Discounts = _discountFunctions.GetAllByIsActive(true);
            var Customers = _customerFunctions.GetCustomersList();
            model.ListCustomers = Customers.GenericClassList.OrderBy(s => s.CompanyName).ToList();
            model.ListDiscounts = Discounts.GenericClassList;
            model.ListProducts = Products.GenericClassList;
            return View(model);
        }
        [HttpPost]
        public int SaveOrder(string Notes, string CustomerID, List<ProductsList> ListProductsToSubmit, List<DiscountIDList> ListDiscountIDs)
        {
            ApplicationUser user = new ApplicationUser();
            var Customer = _customerFunctions.GetByID(Convert.ToInt32(CustomerID));
            Order_Models order_Models = new Order_Models();
            order_Models.Notes = Notes;
            order_Models.OrderStatus = "Created";
            order_Models.SubmissionDate = DateTime.Now;
            order_Models.Tbl_CustomerID = Convert.ToInt32(CustomerID);
            order_Models.CompletionDate = null;
            order_Models.Street = Customer.GenericClass.Street1;
            order_Models.City = Customer.GenericClass.City;
            order_Models.State = Customer.GenericClass.State;
            order_Models.ZIP = Customer.GenericClass.Zip;
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
                    orderProducts_Models.status = "New";
                    orderProducts_Models.Description = item.Description;
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
                    orderDiscount_Models.CustomAmount = item.CustomAmount;
                    orderDiscount_Models.CustomDiscountType = item.CustomeDiscountType;
                    var OrderDiscountsAdd = _orderDiscountFucntions.Add(orderDiscount_Models);

                }

            }

            return OrderAdd.ResponseInt;

        }
        public ActionResult ViewOrder(int ID)
        {
            var OrderInfo = _orderFunctions.GetByID(ID).GenericClass;
            var OrderStatus = _orderStatusFunctions.GetAll();
            var Customers = _customerFunctions.GetCustomersList();

            return View(new ViewOrder_ViewModel { OrderInfo = OrderInfo, ListStatus = OrderStatus.GenericClassList, ListCustomers = Customers.GenericClassList.OrderBy(s => s.CompanyName).ToList() });
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

            var Order = _orderFunctions.GetByID(OrderID);
            if (Order.ResponseSuccess && Order.GenericClass != null)
            {
                model.TransportID = Order.GenericClass.TransportID;
                model.TransportLocationEnd = Order.GenericClass.TransportLocationEnd;
                model.TransportLocationStart = Order.GenericClass.TransportLocationStart;
                model.Notes = Order.GenericClass.Notes;
                model.Status = Order.GenericClass.OrderStatus;
                model.To_From = Order.GenericClass.To_From;
                model.OrderID = Order.GenericClass.ID;
                model.ListStatus = _orderStatusFunctions.GetAll().GenericClassList;
            }
            return PartialView("_EditCurrentOrderStatus", model);
        }
        [HttpPost]
        public bool SaveOrderActivityStatus(int OrderID, string OrderActivityStatus, string OrderActivityNotes)
        {
            var AddedActivity = _orderActivityFunctions.Add(new OrderActivity_Models { ActivityBy = User.Identity.GetUserId(), ActivityDate = DateTime.Now, Notes = OrderActivityNotes, OrderID = OrderID, Status = OrderActivityStatus });

            if (AddedActivity.ResponseSuccess)
            {
                var Order = _orderFunctions.GetByID(OrderID);
                var UpdatedOrderStatus = _orderFunctions.Update(new Order_Models { CompletionDate = Order.GenericClass.CompletionDate, ID = OrderID, Notes = OrderActivityNotes, OrderStatus = OrderActivityStatus, SubmissionDate = Order.GenericClass.SubmissionDate, Tbl_CustomerID = Order.GenericClass.Tbl_CustomerID, Street = Order.GenericClass.Street, City = Order.GenericClass.City, State = Order.GenericClass.State, IsSimpleCure = Order.GenericClass.IsSimpleCure, To_From = Order.GenericClass.To_From, TransportID = Order.GenericClass.TransportID, TransportLocationEnd = Order.GenericClass.TransportLocationEnd, TransportLocationStart = Order.GenericClass.TransportLocationStart, ZIP = Order.GenericClass.ZIP });
                if (UpdatedOrderStatus.ResponseSuccess)
                {
                    return true;
                }
            }
            return false;
        }
        [HttpPost]
        public bool SaveCustomerInfoForOrder(int OrderID, int CustomerID)
        {
            var Order = _orderFunctions.GetByID(OrderID);
            if (Order.ResponseSuccess)
            {
                var Updated = _orderFunctions.Update(new Order_Models { CompletionDate = Order.GenericClass.CompletionDate, ID = OrderID, Notes = Order.GenericClass.Notes, OrderStatus = Order.GenericClass.OrderStatus, SubmissionDate = Order.GenericClass.SubmissionDate, Tbl_CustomerID = CustomerID, Street = Order.GenericClass.Street, City = Order.GenericClass.City, State = Order.GenericClass.State, IsSimpleCure = Order.GenericClass.IsSimpleCure, To_From = Order.GenericClass.To_From, TransportID = Order.GenericClass.TransportID, TransportLocationEnd = Order.GenericClass.TransportLocationEnd, TransportLocationStart = Order.GenericClass.TransportLocationStart, ZIP = Order.GenericClass.ZIP });
                if (Updated.ResponseSuccess)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public ActionResult ViewOrderProducts(int OrderID)
        {
            var OrderProducts = _orderProductsFucntions.GetAllByByOrderID(OrderID);
            var OrderDiscounts = _orderDiscountFucntions.GetAllByOrderID(OrderID);
            List<OrderProduct_Prodcut_Model> ListOrderProductProducts = new List<OrderProduct_Prodcut_Model>();
            foreach (var item in OrderProducts.GenericClassList)
            {
                var Product = _productFucntions.GetByID(item.ProductID);
                ListOrderProductProducts.Add(new OrderProduct_Prodcut_Model { ProductID = Product.GenericClass.ID, BatchID = item.BatchID, CartGram = Product.GenericClass.CartGram, ProductDescription = Product.GenericClass.Description, Dominant = Product.GenericClass.Dominant, EntryBy = item.EntryBy, EntryDate = item.EntryDate, ForProductID = item.ProductID, IsActive = Product.GenericClass.IsActive, OrderID = item.OrderID, OrderProductID = item.ID, PricePerUnit = Product.GenericClass.PricePerUnit, ProductGroup = Product.GenericClass.ProductGroup, ProductImage = Product.GenericClass.ProductImage, Quantity = item.Quantity, Strain = Product.GenericClass.Strain, Total = item.Total, Type = Product.GenericClass.Type, Status = item.status, OrderProductDescription = item.Description });
            }
            List<OrderDiscountDiscounts_Model> ListOrderDiscounts = new List<OrderDiscountDiscounts_Model>();
            foreach (var item in OrderDiscounts.GenericClassList)
            {
                var Discount = _discountFunctions.GetByID(item.DiscountID);
                if (Discount.ResponseSuccess)
                {
                    decimal discountAmount = 0.00m;
                    if (item.CustomAmount != null)
                    { discountAmount = item.CustomAmount ?? 0.00m; }
                    else
                    { discountAmount = Discount.GenericClass.DiscountAmount; }
                    ListOrderDiscounts.Add(new OrderDiscountDiscounts_Model { DiscountAmount = discountAmount, DiscountID = item.DiscountID, IsActive = Discount.GenericClass.IsActive, OrderDiscountDiscountID = item.DiscountID, OrderDiscountID = item.ID, OrderID = item.OrderID, Type = Discount.GenericClass.Type });
                }
            }
            return PartialView("_ViewOrderProducts", new ViewOrderProducts_ViewModel { ListProducts = ListOrderProductProducts, ListOrderDiscounts = ListOrderDiscounts });
        }
        public ActionResult ViewProductInfo(int ProductID)
        {
            var Product = _productFucntions.GetByID(ProductID);

            return PartialView("_ViewProductInfo", new ViewProductInfo_ViewModel { CartGram = Product.GenericClass.CartGram, Description = Product.GenericClass.Description, Dominant = Product.GenericClass.Dominant, ID = Product.GenericClass.ID, IsActive = Product.GenericClass.IsActive, PricePerUnit = Product.GenericClass.PricePerUnit, ProductGroup = Product.GenericClass.ProductGroup, ProductImage = Product.GenericClass.ProductImage, Strain = Product.GenericClass.Strain, Type = Product.GenericClass.Type });
        }
        public ActionResult ViewAddOrderProduct()
        {
            var Products = _productFucntions.GetAllByIsActive(true);
            return PartialView("_ViewAddOrderProduct", new ViewAddOrderProduct_ViewModel { ListProducts = Products.GenericClassList });
        }
        [HttpPost]
        public bool AddOrderProdcut(int OrderID, string BatchID, int Quantity, int ProductID, string Description)
        {
            ApplicationUser user = new ApplicationUser();
            var Added = _orderProductsFucntions.Add(new OrderProducts_Models { BatchID = BatchID, EntryBy = user.Id, EntryDate = DateTime.Now, OrderID = OrderID, ProductID = ProductID, Quantity = Quantity, Total = _productFucntions.GetByID(ProductID).GenericClass.PricePerUnit * Quantity, Description = Description });
            if (Added.ResponseSuccess)
            { return true; }
            else
            { return false; }
        }
        [HttpPost]
        public bool RemoveOrderProduct(int OrderProductID)
        {
            var Removed = _orderProductsFucntions.Delete(OrderProductID);
            if (Removed.ResponseSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ActionResult ViewOrderDiscounts(int OrderID)
        {
            var OrderDiscounts = _orderDiscountFucntions.GetAllByOrderID(OrderID);
            List<OrderDiscountDiscounts_Model> ListOrderDiscounts = new List<OrderDiscountDiscounts_Model>();
            foreach (var item in OrderDiscounts.GenericClassList)
            {
                //var Discount = _discountFunctions.GetByID(item.DiscountID);
                //if (Discount.ResponseSuccess)
                //{
                //decimal discountAmount = 0.00m;
                //string DiscountType = string.Empty;
                //if (item.CustomAmount != null)
                //{
                //    discountAmount = item.CustomAmount ?? 0.00m;
                //    DiscountType = "Custom";
                //}
                //else
                //{
                //    discountAmount = Discount.GenericClass.DiscountAmount;
                //    DiscountType = Discount.GenericClass.Type;
                //}
                ListOrderDiscounts.Add(new OrderDiscountDiscounts_Model { DiscountAmount = item.CustomAmount ?? 0.00M, DiscountID = item.DiscountID, IsActive = true, OrderDiscountDiscountID = item.DiscountID, OrderDiscountID = item.ID, OrderID = item.OrderID, Type = item.CustomDiscountType });
                //}
            }
            return PartialView("_ViewOrderDiscounts", new ViewOrderDiscounts_ViewModel { ListOrderDiscounts = ListOrderDiscounts });
        }
        public ActionResult ViewAddOrderDiscount()
        {
            var Discounts = _discountFunctions.GetAllByIsActive(true);
            return PartialView("_ViewAddOrderDiscount", new ViewAddOrderDiscount_ViewModel { ListDiscounts = Discounts.GenericClassList });
        }
        [HttpPost]
        public bool AddOrderDiscount(int DiscountID, int OrderID, decimal? CustomAmount, string CustomType)
        {
            var Added = _orderDiscountFucntions.Add(new OrderDiscount_Models { DiscountID = DiscountID, OrderID = OrderID, CustomAmount = CustomAmount, CustomDiscountType = CustomType });
            if (Added.ResponseSuccess)
            { return true; }
            else
            { return false; }
        }
        [HttpPost]
        public bool RemoveOrderDiscount(int ID)
        {
            var Removed = _orderDiscountFucntions.Delete(ID);
            if (Removed.ResponseSuccess)
            { return true; }
            else
            { return false; }
        }
        [HttpPost]
        public bool DeleteOrder(int ID)
        {
            var OrderProducts = _orderProductsFucntions.GetAllByByOrderID(ID);
            if (OrderProducts.ResponseSuccess && (OrderProducts.GenericClassList != null && OrderProducts.GenericClassList.Count > 0))
            {
                foreach (var item in OrderProducts.GenericClassList)
                {
                    _orderProductsFucntions.Delete(item.ID);
                }
            }
            var OrderDiscounts = _orderDiscountFucntions.GetAllByOrderID(ID);
            if (OrderDiscounts.ResponseSuccess && (OrderDiscounts.GenericClassList != null && OrderDiscounts.GenericClassList.Count > 0))
            {
                foreach (var item in OrderDiscounts.GenericClassList)
                {
                    _orderDiscountFucntions.Delete(item.ID);
                }
            }
            var OrderActivity = _orderActivityFunctions.GetByOrderID(ID);
            if (OrderActivity.ResponseSuccess && (OrderActivity.GenericClassList != null && OrderActivity.GenericClassList.Count > 0))
            {
                foreach (var item in OrderActivity.GenericClassList)
                {
                    _orderActivityFunctions.Delete(item.ID);
                }
            }
            var Deleted = _orderFunctions.Delete(ID);
            if (Deleted.ResponseSuccess)
            { return true; }
            else
            { return false; }
        }
        [HttpPost]
        public bool OrderPaid(int ID, string CompletionNotes, string To_From, string TransportID, string TransportLocationStart, string TransportLocationEnd)
        {
            ApplicationUser user = new ApplicationUser();
            var Order = _orderFunctions.GetByID(ID);
            if (Order.ResponseSuccess)
            {
                var AddActivity = _orderActivityFunctions.Add(new OrderActivity_Models { ActivityBy = user.Id, ActivityDate = DateTime.Now, Notes = CompletionNotes, OrderID = ID, Status = "Paid" });
                if (AddActivity.ResponseSuccess)
                {
                    var Paid = _orderFunctions.Update(new Order_Models { CompletionDate = DateTime.Now, ID = Order.GenericClass.ID, Notes = CompletionNotes, OrderStatus = Order.GenericClass.OrderStatus, SubmissionDate = Order.GenericClass.SubmissionDate, Tbl_CustomerID = Order.GenericClass.Tbl_CustomerID, Street = Order.GenericClass.Street, City = Order.GenericClass.City, State = Order.GenericClass.State, IsSimpleCure = Order.GenericClass.IsSimpleCure, To_From = To_From, TransportID = TransportID, TransportLocationEnd = TransportLocationEnd, TransportLocationStart = TransportLocationStart, ZIP = Order.GenericClass.ZIP });
                    if (Paid.ResponseSuccess)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
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
        public ActionResult PaidOrders(string SearchTerm = null)
        {
            List<PaidOrders_ViewModel> model = new List<PaidOrders_ViewModel>();
            if (SearchTerm != null)
            {
                var PaidOrders = _orderFunctions.GetPaidOrdersBySearchTerm(SearchTerm);
                if (PaidOrders.ResponseSuccess && PaidOrders.GenericClassList != null && PaidOrders.GenericClassList.Count > 0)
                {
                    foreach (var item in PaidOrders.GenericClassList)
                    {
                        model.Add(new PaidOrders_ViewModel { Company = item.Company, Customer = item.Customer, OrderDate = item.OrderDate, OrderID = item.OrderID, OrderStatus = item.OrderStatus, PaidDate = item.PaidDate });
                    }
                }
            }
            return View(model);
        }
        public ActionResult OrderInvoice(int ID)
        {
            var OrderInfo = _orderFunctions.GetByID(ID).GenericClass;
            var CustomerInfo = _customerFunctions.GetByUserID(OrderInfo.Tbl_CustomerID).GenericClass;
            var OrderProducts = _orderProductsFucntions.GetAllByByOrderID(ID);
            var OrderDiscounts = _orderDiscountFucntions.GetAllByOrderID(ID);
            List<OrderProduct_Prodcut_Model> ListOrderProductProducts = new List<OrderProduct_Prodcut_Model>();
            foreach (var item in OrderProducts.GenericClassList)
            {
                var Product = _productFucntions.GetByID(item.ProductID);
                ListOrderProductProducts.Add(new OrderProduct_Prodcut_Model { ProductID = Product.GenericClass.ID, BatchID = item.BatchID, CartGram = Product.GenericClass.CartGram, ProductDescription = Product.GenericClass.Description, Dominant = Product.GenericClass.Dominant, EntryBy = item.EntryBy, EntryDate = item.EntryDate, ForProductID = item.ProductID, IsActive = Product.GenericClass.IsActive, OrderID = item.OrderID, OrderProductID = item.ID, PricePerUnit = Product.GenericClass.PricePerUnit, ProductGroup = Product.GenericClass.ProductGroup, ProductImage = Product.GenericClass.ProductImage, Quantity = item.Quantity, Strain = Product.GenericClass.Strain, Total = item.Total, Type = Product.GenericClass.Type, OrderProductDescription = item.Description });
            }
            List<OrderDiscountDiscounts_Model> ListOrderDiscounts = new List<OrderDiscountDiscounts_Model>();
            foreach (var item in OrderDiscounts.GenericClassList)
            {
                var Discount = _discountFunctions.GetByID(item.DiscountID);
                decimal discountAmount = 0.00m;
                if (item.CustomAmount != null)
                { discountAmount = item.CustomAmount ?? 0.00m; }
                else
                { discountAmount = Discount.GenericClass.DiscountAmount; }
                ListOrderDiscounts.Add(new OrderDiscountDiscounts_Model { DiscountAmount = discountAmount, DiscountID = item.DiscountID, IsActive = Discount.GenericClass.IsActive, OrderDiscountDiscountID = item.DiscountID, OrderDiscountID = item.ID, OrderID = item.OrderID, Type = Discount.GenericClass.Type });
            }

            var model = new OrderInvoice_ViewModel { CustomerInfo = CustomerInfo, OrderInfo = OrderInfo, ListDiscounts = ListOrderDiscounts, ListProducts = ListOrderProductProducts };

            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }
            var abc = new ViewAsPdf(model)
            {
                FileName = "OrderInvoice.pdf",
                Cookies = cookieCollection,
                FormsAuthenticationCookieName = System.Web.Security.FormsAuthentication.FormsCookieName
            };

            return abc;
        }
        public ActionResult ViewEditOrderProduct(int OrderID, int OrderProductID)
        {
            var OrderProduct = _orderProductsFucntions.GetByID(OrderProductID);
            if (OrderProduct.ResponseSuccess)
            {
                var Product = _productFucntions.GetByID(OrderProduct.GenericClass.ProductID);
                var ProductList = _productFucntions.GetAllByIsActive(true);

                return PartialView("_EditOrderProduct", new EditOrderProduct_ViewModel { ListProducts = ProductList.GenericClassList, OrderProductModel = OrderProduct.GenericClass, ProductModel = Product.GenericClass });
            }

            return View("ViewOrder", new { ID = OrderID });
        }
        [HttpPost]
        public JsonResult SaveEditOrderProduct(int OrderProductID, int OrderID, int ProductID, string BatchID, int Quantity, string Status, string Description)
        {
            var Product = _productFucntions.GetByID(ProductID).GenericClass;
            var Updated = _orderProductsFucntions.Update(new OrderProducts_Models { BatchID = BatchID, EntryBy = User.Identity.ToString(), EntryDate = DateTime.Now, ID = OrderProductID, OrderID = OrderID, ProductID = ProductID, Quantity = Quantity, status = Status, Total = Quantity * Product.PricePerUnit, Description = Description });
            return Json(Updated, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EmailInvoice(string EmailAddress, int ID, string EmailMessage)
        {

            bool MailSent = false;

            var OrderInfo = _orderFunctions.GetByID(ID).GenericClass;
            var CustomerInfo = _customerFunctions.GetByUserID(OrderInfo.Tbl_CustomerID).GenericClass;
            var OrderProducts = _orderProductsFucntions.GetAllByByOrderID(ID);
            var OrderDiscounts = _orderDiscountFucntions.GetAllByOrderID(ID);
            List<OrderProduct_Prodcut_Model> ListOrderProductProducts = new List<OrderProduct_Prodcut_Model>();
            foreach (var item in OrderProducts.GenericClassList)
            {
                var Product = _productFucntions.GetByID(item.ProductID);
                ListOrderProductProducts.Add(new OrderProduct_Prodcut_Model { ProductID = Product.GenericClass.ID, BatchID = item.BatchID, CartGram = Product.GenericClass.CartGram, ProductDescription = Product.GenericClass.Description, Dominant = Product.GenericClass.Dominant, EntryBy = item.EntryBy, EntryDate = item.EntryDate, ForProductID = item.ProductID, IsActive = Product.GenericClass.IsActive, OrderID = item.OrderID, OrderProductID = item.ID, PricePerUnit = Product.GenericClass.PricePerUnit, ProductGroup = Product.GenericClass.ProductGroup, ProductImage = Product.GenericClass.ProductImage, Quantity = item.Quantity, Strain = Product.GenericClass.Strain, Total = item.Total, Type = Product.GenericClass.Type, OrderProductDescription = item.Description });
            }
            List<OrderDiscountDiscounts_Model> ListOrderDiscounts = new List<OrderDiscountDiscounts_Model>();
            foreach (var item in OrderDiscounts.GenericClassList)
            {
                var Discount = _discountFunctions.GetByID(item.DiscountID);
                decimal discountAmount = 0.00m;
                if (item.CustomAmount != null)
                { discountAmount = item.CustomAmount ?? 0.00m; }
                else
                { discountAmount = Discount.GenericClass.DiscountAmount; }
                ListOrderDiscounts.Add(new OrderDiscountDiscounts_Model { DiscountAmount = discountAmount, DiscountID = item.DiscountID, IsActive = Discount.GenericClass.IsActive, OrderDiscountDiscountID = item.DiscountID, OrderDiscountID = item.ID, OrderID = item.OrderID, Type = Discount.GenericClass.Type });
            }

            var model = new OrderInvoice_ViewModel { CustomerInfo = CustomerInfo, OrderInfo = OrderInfo, ListDiscounts = ListOrderDiscounts, ListProducts = ListOrderProductProducts };

            var pdfResult = new ViewAsPdf("OrderInvoice", model) { PageOrientation = Rotativa.Options.Orientation.Portrait };
            var binary = pdfResult.BuildFile(this.ControllerContext);
            byte[] bytes = binary;
            ContentType ct2 = new ContentType()
            {
                MediaType = "application/pdf",
                Name = "SimpleCure Order Invoice.pdf"
            };
            Attachment att1 = new Attachment(new MemoryStream(bytes), ct2);
            var success = _emailFunctions.SendMailWithAttachment(EmailAddress, "SimpleCure Invoice", $"Attached is your invoice. {Environment.NewLine}{Environment.NewLine} Simple Cure Message{Environment.NewLine}{EmailMessage}", att1);
            MailSent = success.ResponseSuccess;
            return Json(MailSent, JsonRequestBehavior.AllowGet);

        }
        public Byte[] InvoicePDFforMail(int ID)
        {
            try
            {
                var mailpdft = new ActionAsPdf("OrderInvoice/" + ID)
                {
                    FileName = "Simple Cure Invoice",
                    PageSize = Rotativa.Options.Size.A4,
                    PageWidth = 210,
                    PageHeight = 297
                };
                Byte[] PdfData = mailpdft.BuildFile(ControllerContext);
                return PdfData;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException?.ToString() ?? ex.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Order ID: {ID}{Environment.NewLine} Error: {error}";
                _loggerFunctions.Log(ErrorMessage, string.Empty);
                return null;
            }

            //return new Byte[];

        }

        [HttpPost]
        public bool SaveEditBillToAddress(int ID, string Street, string City, string State, string ZIP)
        {
            ApplicationUser user = new ApplicationUser();
            var Order = _orderFunctions.GetByID(ID);
            if (Order.ResponseSuccess)
            {
                var Update = _orderFunctions.Update(new Order_Models { CompletionDate = Order.GenericClass.CompletionDate, ID = Order.GenericClass.ID, Notes = Order.GenericClass.Notes, OrderStatus = Order.GenericClass.OrderStatus, SubmissionDate = Order.GenericClass.SubmissionDate, Tbl_CustomerID = Order.GenericClass.Tbl_CustomerID, Street = Street, City = City, State = State, IsSimpleCure = Order.GenericClass.IsSimpleCure, To_From = Order.GenericClass.To_From, TransportID = Order.GenericClass.TransportID, TransportLocationEnd = Order.GenericClass.TransportLocationEnd, TransportLocationStart = Order.GenericClass.TransportLocationStart, ZIP = ZIP });
                if (Update.ResponseSuccess)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}