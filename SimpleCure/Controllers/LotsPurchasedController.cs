using AutoMapper;
using BusinessLayer.Functions.Customer;
using BusinessLayer.Functions.Email;
using BusinessLayer.Functions.ErrorLogging;
using BusinessLayer.Functions.LotsPurchased;
using Microsoft.AspNet.Identity;
using SimpleCure.AutoMapper;
using SimpleCure.Models;
using SimpleCure.Models.LotPurchasedModels;
using System;
using BusinessLayer.Models.LotsPurchasedModels;
using System.Web.Mvc;

namespace SimpleCure.Controllers
{
    [Authorize]
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
                var response = _lotsPurchasedFunctions.Add(new BusinessLayer.Models.LotsPurchasedModels.LotsPurchased_Models { BudTrim = model.GenericClass.BudTrim, BuyDate = model.GenericClass.BuyDate, CBD = model.GenericClass.CBD, Complete = false, Cost = model.GenericClass.Cost, EnterDate = DateTime.Now, Grams = model.GenericClass.Grams, IndPackages = model.GenericClass.IndPackages, Lot_Set = model.GenericClass.Lot_Set, Notes = model.GenericClass.Notes, Pounds = model.GenericClass.Pounds, PricePerGram = model.GenericClass.PricePerGram, PricePerPound = model.GenericClass.PricePerPound, Provider = model.GenericClass.Provider, SatPackages = model.GenericClass.SatPackages, Strains = model.GenericClass.Strains, IsSimpleCure = false, TransportID = string.Empty, CompletedBy = string.Empty, CompletionDate = null });
                if (response.ResponseSuccess)
                {
                    return RedirectToAction("LotsPurchased", new { ResponseMessage = response.ResponseMessage, ResponseSuccess = response.ResponseSuccess });
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

        public ActionResult LotsPurchased(DateTime? Start, DateTime? End, bool? IsComplete, int Provider = 0, string Lot_Set = "", string ResponseMessage = "", bool ResponseSuccess = false)
        {
            Generic<LotsPurchased_ViewModel> model = new Generic<LotsPurchased_ViewModel>();

            var GenCustomers = _customerFunctions.GetCustomersList();

            if (GenCustomers.ResponseSuccess && GenCustomers.GenericClassList != null && GenCustomers.GenericClassList.Count > 0)
            {
                ViewBag.Customers = GenCustomers.GenericClassList;
            }
            if (Start.HasValue || End.HasValue || Provider > 0 || !string.IsNullOrEmpty(Lot_Set) || IsComplete != null)
            {

                model = _IMapper.Map<Generic<LotsPurchased_ViewModel>>(_lotsPurchasedFunctions.Search(Provider, Lot_Set, Start, End, IsComplete));

                if (model.ResponseSuccess && model.GenericClassList != null && model.GenericClassList.Count > 0)
                {
                    foreach (var item in model.GenericClassList)
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
            else if (model != null)
            {
                model.ResponseSuccess = true;
            }
            return View(model);
        }

        public ActionResult ViewLotPurchased(int ID, Generic<ViewLotsPurchased_ViewModel> model = null)
        {
            var GenCustomers = _customerFunctions.GetCustomersList();
            var LotPurchased = _lotsPurchasedFunctions.GetByID(ID);
            if (LotPurchased.ResponseSuccess && LotPurchased.GenericClass != null)
            {
                if (model == null)
                {
                    model.ResponseSuccess = true;
                }
                model.GenericClass = new ViewLotsPurchased_ViewModel { ID = LotPurchased.GenericClass.ID, BudTrim = LotPurchased.GenericClass.BudTrim, BuyDate = LotPurchased.GenericClass.BuyDate, CBD = LotPurchased.GenericClass.CBD, Complete = LotPurchased.GenericClass.Complete, Cost = LotPurchased.GenericClass.Cost, EnterDate = LotPurchased.GenericClass.EnterDate, Grams = LotPurchased.GenericClass.Grams, IndPackages = LotPurchased.GenericClass.IndPackages, Lot_Set = LotPurchased.GenericClass.Lot_Set, Notes = LotPurchased.GenericClass.Notes, Pounds = LotPurchased.GenericClass.Pounds, PricePerGram = LotPurchased.GenericClass.PricePerGram, PricePerPound = LotPurchased.GenericClass.PricePerPound, Provider = LotPurchased.GenericClass.Provider, SatPackages = LotPurchased.GenericClass.SatPackages, Strains = LotPurchased.GenericClass.Strains, Lot_Set_Orginal = LotPurchased.GenericClass.Lot_Set, CompletedBy = LotPurchased.GenericClass.CompletedBy, CompletionDate = LotPurchased.GenericClass.CompletionDate, IsSimpleCure = LotPurchased.GenericClass.IsSimpleCure, TransportID = LotPurchased.GenericClass.TransportID, To_From = LotPurchased.GenericClass.To_From, Split = LotPurchased.GenericClass.Split, SplitNotes = LotPurchased.GenericClass.SplitNotes, TransportLocationEnd = LotPurchased.GenericClass.TransportLocationEnd, TransportLocationStart = LotPurchased.GenericClass.TransportLocationStart };
            }

            if (GenCustomers.ResponseSuccess && GenCustomers.GenericClassList != null && GenCustomers.GenericClassList.Count > 0)
            {
                ViewBag.Customers = GenCustomers.GenericClassList;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEditLotPurchased(Generic<ViewLotsPurchased_ViewModel> model)
        {
            var lotset = string.Empty;
            if (model.GenericClass.Lot_Set_Orginal.ToLower().Contains(model.GenericClass.Lot_Set.ToLower()))
            {
                lotset = model.GenericClass.Lot_Set_Orginal;
            }
            else
            {
                lotset = model.GenericClass.Lot_Set;
            }

            if (model.GenericClass.Complete)
            {
                model.GenericClass.CompletedBy = User.Identity.GetUserId();
                model.GenericClass.CompletionDate = DateTime.Now;
            }
            else
            {
                model.GenericClass.CompletedBy = null;
                model.GenericClass.CompletionDate = null;
            }

            var response = _lotsPurchasedFunctions.Update(new LotsPurchased_Models { BudTrim = model.GenericClass.BudTrim, BuyDate = model.GenericClass.BuyDate, CBD = model.GenericClass.CBD, Complete = model.GenericClass.Complete, Cost = model.GenericClass.Cost, EnterDate = DateTime.Now, Grams = model.GenericClass.Grams, IndPackages = model.GenericClass.IndPackages, Lot_Set = lotset, Notes = model.GenericClass.Notes, Pounds = model.GenericClass.Pounds, PricePerGram = model.GenericClass.PricePerGram, PricePerPound = model.GenericClass.PricePerPound, Provider = model.GenericClass.Provider, SatPackages = model.GenericClass.SatPackages, Strains = model.GenericClass.Strains, ID = model.GenericClass.ID, TransportID = model.GenericClass.TransportID, TransportLocationEnd = model.GenericClass.TransportLocationEnd, TransportLocationStart = model.GenericClass.TransportLocationEnd, Split = model.GenericClass.Split, SplitNotes = model.GenericClass.SplitNotes, To_From = model.GenericClass.To_From });

            if (response.ResponseSuccess)
            {
                return RedirectToAction("LotsPurchased", new { ResponseMessage = response.ResponseMessage, ResponseSuccess = response.ResponseSuccess });
            }
            else
            {
                return View("ViewLotPurchased", new { model.GenericClass.ID, model = new Generic<ViewLotsPurchased_ViewModel> { ResponseSuccess = response.ResponseSuccess, ResponseMessage = response.ResponseMessage, responseTypes = ResponseTypes.Failure } });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLotPurchased(int LotPurchasedID)
        {

            var response = _lotsPurchasedFunctions.Delete(LotPurchasedID);
            if (response.ResponseSuccess)
            {
                return RedirectToAction("LotsPurchased", new Generic<LotsPurchased_ViewModel> { ResponseSuccess = true, responseTypes = ResponseTypes.Success, ResponseMessage = "Successfully delete Lot Purchased with ID: " + LotPurchasedID });
            }
            else
            {
                return RedirectToAction("ViewLotPurchased", new { ID = LotPurchasedID, model = new Generic<ViewLotsPurchased_ViewModel> { ResponseSuccess = response.ResponseSuccess, responseTypes = ResponseTypes.Failure, ResponseMessage = response.ResponseMessage } });
            }
        }
    }
}