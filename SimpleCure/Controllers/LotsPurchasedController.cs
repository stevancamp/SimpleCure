using AutoMapper;
using BusinessLayer.Functions.Customer;
using BusinessLayer.Functions.Email;
using BusinessLayer.Functions.ErrorLogging;
using BusinessLayer.Functions.LotsPurchased;
using SimpleCure.AutoMapper;
using SimpleCure.Models;
using SimpleCure.Models.LotPurchasedModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SimpleCure.Controllers
{
    //[Authorize]
    public class LotsPurchasedController : Controller
    {
        #region Injection
        private IMapper _IMapper;
        private LoggerFunctions _loggerFunctions;
        private EmailFunctions _emailFunctions;
        private LotsPurchasedFunctions _lotsPurchasedFunctions;
        private CustomerFunctions _customerFunctions;

        public LotsPurchasedController()
        {
            _IMapper = AutoMapperConfiguration.GetMapper();
            _loggerFunctions = new LoggerFunctions();
            _emailFunctions = new EmailFunctions();
            _lotsPurchasedFunctions = new LotsPurchasedFunctions();
            _customerFunctions = new CustomerFunctions();
        }
        #endregion

        public ActionResult NewLotPurchased()
        {
            var GenCustomers = _customerFunctions.GetCustomersList();
            if (GenCustomers.ResponseSuccess && GenCustomers.GenericClassList != null && GenCustomers.GenericClassList.Count > 0)
            {
                ViewBag.Customers = GenCustomers.GenericClassList;               
            }
            return View(new Generic<NewLotPurchased_ViewModel>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewLotPurchased(Generic<NewLotPurchased_ViewModel> model)
        {
            var GenCustomers = _customerFunctions.GetCustomersList();
            if (ModelState.IsValid)
            {
                var response = _lotsPurchasedFunctions.Add(new BusinessLayer.Models.LotsPurchasedModels.LotsPurchased_Models { BudTrim = model.GenericClass.BudTrim, BuyDate = model.GenericClass.BuyDate, CBD = model.GenericClass.CBD, Complete = true, Cost = model.GenericClass.Cost, EnterDate = DateTime.Now, Grams = model.GenericClass.Grams, IndPackages = model.GenericClass.IndPackages, Lot_Set = model.GenericClass.Lot_Set, Notes = model.GenericClass.Notes, Pounds = model.GenericClass.Pounds, PricePerGram = model.GenericClass.PricePerGram, PricePerPound = model.GenericClass.PricePerPound, Provider = model.GenericClass.Provider, SatPackages = model.GenericClass.SatPackages, Strains = model.GenericClass.Strains });
                if (response.ResponseSuccess)
                {
                    return RedirectToAction("LotsPurchased");
                }
                else
                {
                    model.ResponseSuccess = response.ResponseSuccess;
                    model.ResponseMessage = response.ResponseMessage;
                    model.responseTypes = ResponseTypes.Failure;
                    if (GenCustomers.ResponseSuccess && GenCustomers.GenericClassList != null && GenCustomers.GenericClassList.Count > 0)
                    {
                        ViewBag.Customers = GenCustomers.GenericClassList;
                    }
                    return View(model);
                }
            }
           
            if (GenCustomers.ResponseSuccess && GenCustomers.GenericClassList != null && GenCustomers.GenericClassList.Count > 0)
            {
                ViewBag.Customers = GenCustomers.GenericClassList;
            }
            model.ResponseSuccess = false;
            model.ResponseMessage = "There are required fields that have not been filled out.";
            model.responseTypes = ResponseTypes.Failure;
            return View(model);
        }

        public ActionResult LotsPurchased(DateTime? Start, DateTime? End, bool IsComplete = true)
        {
            Generic<LotsPurchased_ViewModel> response = new Generic<LotsPurchased_ViewModel>();

            if (Start.HasValue && End.HasValue)
            {
                response = _IMapper.Map<Generic<LotsPurchased_ViewModel>>(_lotsPurchasedFunctions.GetByCompleteByRange(Start ?? DateTime.Now, End ?? DateTime.Now, IsComplete));

                if (response.ResponseSuccess && response.GenericClassList != null && response.GenericClassList.Count > 0)
                {
                    foreach (var item in response.GenericClassList)
                    {

                        var customer = _customerFunctions.GetByUserID(item.Provider ?? 0);
                        
                        if (customer.ResponseSuccess)
                        {
                            item.Customer.CompanyName = customer.GenericClass.Company;
                            item.Customer.CustomerID = customer.GenericClass.ID;
                            item.Customer.CustomerName = customer.GenericClass.Customer;
                        }

                    }
                }
            }
            else
            {
                response.ResponseSuccess = true;
            }
            return View(response);
        }


        public ActionResult ViewLotPurchased(int ID)
        {
            Generic<ViewLotsPurchased_ViewModel> response = new Generic<ViewLotsPurchased_ViewModel>();
            var GenCustomers = _customerFunctions.GetCustomersList();

            var LotPurchased = _lotsPurchasedFunctions.GetByID(ID);

            if (LotPurchased.ResponseSuccess && LotPurchased.GenericClass != null)
            {
                response.ResponseSuccess = true;

                response.GenericClass = new ViewLotsPurchased_ViewModel { ID = LotPurchased.GenericClass.ID, BudTrim = LotPurchased.GenericClass.BudTrim, BuyDate = LotPurchased.GenericClass.BuyDate, CBD = LotPurchased.GenericClass.CBD, Complete = LotPurchased.GenericClass.Complete, Cost = LotPurchased.GenericClass.Cost, EnterDate = LotPurchased.GenericClass.EnterDate, Grams = LotPurchased.GenericClass.Grams, IndPackages = LotPurchased.GenericClass.IndPackages, Lot_Set = LotPurchased.GenericClass.Lot_Set, Notes = LotPurchased.GenericClass.Notes, Pounds = LotPurchased.GenericClass.Pounds, PricePerGram = LotPurchased.GenericClass.PricePerGram, PricePerPound = LotPurchased.GenericClass.PricePerPound, Provider = LotPurchased.GenericClass.Provider, SatPackages = LotPurchased.GenericClass.SatPackages, Strains = LotPurchased.GenericClass.Strains };
            }

            if (GenCustomers.ResponseSuccess && GenCustomers.GenericClassList != null && GenCustomers.GenericClassList.Count > 0)
            {
                ViewBag.Customers = GenCustomers.GenericClassList;
            }

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEditLotPurchased(Generic<ViewLotsPurchased_ViewModel> model)
        {                 
            var GenCustomers = _customerFunctions.GetCustomersList();
            var response = _lotsPurchasedFunctions.Update(new BusinessLayer.Models.LotsPurchasedModels.LotsPurchased_Models { BudTrim = model.GenericClass.BudTrim, BuyDate = model.GenericClass.BuyDate, CBD = model.GenericClass.CBD, Complete = true, Cost = model.GenericClass.Cost, EnterDate = DateTime.Now, Grams = model.GenericClass.Grams, IndPackages = model.GenericClass.IndPackages, Lot_Set = model.GenericClass.Lot_Set, Notes = model.GenericClass.Notes, Pounds = model.GenericClass.Pounds, PricePerGram = model.GenericClass.PricePerGram, PricePerPound = model.GenericClass.PricePerPound, Provider = model.GenericClass.Provider, SatPackages = model.GenericClass.SatPackages, Strains = model.GenericClass.Strains, ID = model.GenericClass.ID });

            if (response.ResponseSuccess)
            {
                return RedirectToAction("LotsPurchased", new Generic<LotsPurchased_ViewModel> { ResponseSuccess = true, responseTypes = ResponseTypes.Success, ResponseMessage = "Successfully updated " + model.GenericClass.Lot_Set });
            }
            else
            {
                return View("ViewLotPurchased", new { ID = model.GenericClass.ID });
            }
        }

        //public ActionResult EditLotPurchased()
        //{

        //    return RedirectToAction("ViewLotPurchased", new { ID = 1 });
        //}

        public ActionResult DeleteLotPurchased(int ID)
        {

            return RedirectToAction("LotsPurchased");

        }

    }
}