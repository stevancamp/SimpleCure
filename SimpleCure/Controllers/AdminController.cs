using BusinessLayer.Functions.Customer;
using BusinessLayer.Functions.Discount;
using BusinessLayer.Functions.Email;
using BusinessLayer.Functions.ErrorLogging;
using BusinessLayer.Functions.LoginLogger;
using BusinessLayer.Functions.OrderStatus;
using BusinessLayer.Functions.Product;
using BusinessLayer.Functions.ProductGroup;
using BusinessLayer.Functions.Types;
using BusinessLayer.Models.CustomerModels;
using BusinessLayer.Models.DiscountModels;
using BusinessLayer.Models.LoginAttemptModels;
using BusinessLayer.Models.OrderStatusModels;
using BusinessLayer.Models.ProductGroupModels;
using BusinessLayer.Models.ProductModels;
using BusinessLayer.Models.TypeModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using SimpleCure.Helpers;
using SimpleCure.Models;
using SimpleCure.Models.AdminModels;
using SimpleCure.Models.DiscountModels;
using SimpleCure.Models.OrderStatusModels;
using SimpleCure.Models.ProductGroupsModels;
using SimpleCure.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SimpleCure.Controllers
{
    [Authorize(Roles = "WebAdmin,Owner")]
    public class AdminController : Controller
    {
        #region Injection
        private LoggerFunctions _loggerFunctions;
        private TypeFunctions _typeFunctions;
        private Helper _helper;
        private CustomerFunctions _customerFunctions;
        private ApplicationUserManager _userManager;
        private EmailFunctions _emailFunctions;
        private LoginAttemptFunctions _loginAttemptFunctions;
        private OrderStatusFunctions _orderStatusFucntions;
        private ProductFunctions _productFunctions;
        private ProductGroupFunctions _productGroupFunctions;
        private DiscountFunctions _discountFunctions;

        public AdminController()
        {
            _loggerFunctions = new LoggerFunctions();
            _typeFunctions = new TypeFunctions();
            _helper = new Helper();
            _customerFunctions = new CustomerFunctions();
            _emailFunctions = new EmailFunctions();
            _loginAttemptFunctions = new LoginAttemptFunctions();
            _orderStatusFucntions = new OrderStatusFunctions();
            _productFunctions = new ProductFunctions();
            _productGroupFunctions = new ProductGroupFunctions();
            _discountFunctions = new DiscountFunctions();
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion

        #region Business Types
        public ActionResult BusinessTypes(bool ActiveStatus = true, BusinessTypes_ViewModel model = null)
        {
            model.ActiveStatus = ActiveStatus;
            try
            {
                var BusinessTypes = _typeFunctions.GetAllBusinessTypes(ActiveStatus);
                if (string.IsNullOrEmpty(model.ResponseMessage))
                {
                    model.ResponseSuccess = BusinessTypes.ResponseSuccess;
                    model.ResponseMessage = BusinessTypes.ResponseMessage;
                    switch (BusinessTypes.responseTypes)
                    {
                        case BusinessLayer.Models.ResponseTypes.Success:
                            model.responseTypes = Models.ResponseTypes.Success;
                            break;
                        case BusinessLayer.Models.ResponseTypes.Failure:
                            model.responseTypes = Models.ResponseTypes.Failure;
                            break;
                        case BusinessLayer.Models.ResponseTypes.Information:
                            model.responseTypes = Models.ResponseTypes.Information;
                            break;
                        default:
                            model.responseTypes = Models.ResponseTypes.Failure;
                            break;
                    }
                }
                if (BusinessTypes.GenericClassList != null && BusinessTypes.GenericClassList.Count > 0)
                {
                    model.ListBusinessTypes = BusinessTypes.GenericClassList;
                }
            }
            catch (Exception ex)
            {
                _loggerFunctions.Log(ex.ToString(), _helper.GetIp());
                model.ResponseMessage = "There was an error while retrieving the Business Types.";
                model.ResponseSuccess = false;
            }

            return View(model);
        }
        public ActionResult CreateBusinessType()
        {
            return PartialView("_CreateBusinessType");
        }
        public ActionResult EditBusinessType(int ID)
        {
            EditBusinessType_ViewModel model = new EditBusinessType_ViewModel();

            try
            {
                var BusinessType = _typeFunctions.GetBusinessTypeByID(ID);
                if (BusinessType.ResponseSuccess)
                {
                    model.ID = BusinessType.GenericClass.ID;
                    model.EditType = BusinessType.GenericClass.Type;
                    model.IsActive = BusinessType.GenericClass.IsActive;
                }
                else
                {
                    BusinessTypes_ViewModel returnModel = new BusinessTypes_ViewModel();
                    returnModel.ResponseMessage = "Unable to retrieve Business Type by ID " + ID;
                    return View("BusinessTypes", model);
                }
            }
            catch (Exception ex)
            {
                _loggerFunctions.Log(ex.ToString(), _helper.GetIp());
                BusinessTypes_ViewModel returnModel = new BusinessTypes_ViewModel();
                returnModel.ResponseMessage = "There was an error while retrieving the Business Type by ID " + ID;
                returnModel.ResponseSuccess = false;
                return RedirectToAction("BusinessTypes", model);
            }

            return PartialView("_EditBusinessType", model);
        }
        [HttpPost]
        public ActionResult SaveBusinessType(string CreateType)
        {
            BusinessTypes_ViewModel model = new BusinessTypes_ViewModel();
            model.ActiveStatus = true;
            if (!string.IsNullOrEmpty(CreateType))
            {
                try
                {
                    BusinessType_Model businessType_Model = new BusinessType_Model();
                    businessType_Model.Type = CreateType;
                    businessType_Model.IsActive = true;
                    var Saved = _typeFunctions.AddBusinessType(businessType_Model);
                    if (Saved.ResponseSuccess)
                    {
                        model.ResponseSuccess = Saved.ResponseSuccess;
                        model.ResponseMessage = Saved.ResponseMessage;
                        model.responseTypes = Models.ResponseTypes.Success;
                    }
                    else
                    {
                        model.ResponseSuccess = Saved.ResponseSuccess;
                        model.ResponseMessage = Saved.ResponseMessage;
                        model.responseTypes = Models.ResponseTypes.Information;
                    }
                }
                catch (Exception ex)
                {
                    _loggerFunctions.Log(ex.ToString(), _helper.GetIp());
                    model.responseTypes = Models.ResponseTypes.Failure;
                    model.ResponseMessage = "There was an error while saving the Business Type " + CreateType;
                }
            }
            else
            {
                model.responseTypes = Models.ResponseTypes.Failure;
                model.ResponseMessage = "You must submit a Business Type Value";
            }

            return RedirectToAction("BusinessTypes", model);
        }

        [HttpPost]
        public ActionResult SaveBusinessTypeEdit(FormCollection formCollection)
        {
            BusinessTypes_ViewModel model = new BusinessTypes_ViewModel();
            model.ActiveStatus = true;
            if (formCollection != null && !string.IsNullOrEmpty(formCollection["EditType"]))
            {
                var BTid = Convert.ToInt32(formCollection["ID"]);
                var BTtype = formCollection["EditType"];
                var BTisactive = Convert.ToBoolean(formCollection["IsActive"].Split(',')[0]);

                try
                {
                    BusinessType_Model businessType_Model = new BusinessType_Model();
                    businessType_Model.ID = BTid;
                    businessType_Model.Type = BTtype;
                    businessType_Model.IsActive = BTisactive;
                    var Updated = _typeFunctions.UpdateBusinessType(businessType_Model);
                    if (Updated.ResponseSuccess)
                    {
                        model.ResponseSuccess = Updated.ResponseSuccess;
                        model.ResponseMessage = Updated.ResponseMessage;
                        model.responseTypes = Models.ResponseTypes.Success;
                    }
                    else
                    {
                        model.ResponseSuccess = Updated.ResponseSuccess;
                        model.ResponseMessage = Updated.ResponseMessage;
                        model.responseTypes = Models.ResponseTypes.Information;
                    }
                }
                catch (Exception ex)
                {
                    _loggerFunctions.Log(ex.ToString(), _helper.GetIp());
                    model.ResponseMessage = "There was an error while update the Business Type " + formCollection["EditType"];
                    model.responseTypes = Models.ResponseTypes.Failure;
                }
            }
            else
            {
                model.ResponseMessage = "You must submit a Business Type Value";
                model.responseTypes = Models.ResponseTypes.Failure;
            }
            return RedirectToAction("BusinessTypes", model);
        }
        [HttpPost]
        public ActionResult DeleteBusinessType(int ID)
        {
            BusinessTypes_ViewModel model = new BusinessTypes_ViewModel();
            model.ActiveStatus = true;
            if (ID > 0)
            {
                try
                {
                    var Deleted = _typeFunctions.DeleteBusinessType(ID);
                    if (Deleted.ResponseSuccess)
                    {
                        model.ResponseSuccess = Deleted.ResponseSuccess;
                        model.ResponseMessage = Deleted.ResponseMessage;
                        model.responseTypes = Models.ResponseTypes.Success;
                    }
                    else
                    {
                        model.ResponseSuccess = Deleted.ResponseSuccess;
                        model.ResponseMessage = Deleted.ResponseMessage;
                        model.responseTypes = Models.ResponseTypes.Information;
                    }
                }
                catch (Exception ex)
                {
                    _loggerFunctions.Log(ex.ToString(), _helper.GetIp());
                    model.ResponseMessage = "Unable to delete Business Type with ID " + ID;
                    model.responseTypes = Models.ResponseTypes.Failure;
                }
            }
            else
            {
                model.ResponseMessage = "You must submit a Business Type ID to delete";
                model.responseTypes = Models.ResponseTypes.Failure;
            }

            return RedirectToAction("BusinessTypes", model);
        }
        #endregion

        #region Admin User Maintenance

        public ActionResult UserMaintenance(string SearchTerm)
        {

            Generic<UserMaintenance_ViewModel> model = new Generic<UserMaintenance_ViewModel>();
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                var CustInfo = _customerFunctions.SearchCustomers(SearchTerm);

                if (CustInfo.GenericClassList != null && CustInfo.GenericClassList.Count > 0)
                {
                    foreach (var item in CustInfo.GenericClassList)
                    {
                        UserMaintenance_ViewModel user = new UserMaintenance_ViewModel();
                        user.Company = item.Company;
                        user.CustomerName = item.Customer;
                        user.Email = item.MainEmail;
                        user.MainPhoneNumber = item.MainPhone;
                        user.StreetAddress = item.Street1;
                        user.ID = item.ID;
                        if (!string.IsNullOrEmpty(item.AspNetUsersID))
                        {
                            user.HasLogin = true;
                        }
                        else
                        {
                            user.HasLogin = false;
                        }
                        model.GenericClassList.Add(user);
                    }
                }
                else
                {
                    model.GenericClassList = null;
                    model.ResponseMessage = CustInfo.ResponseMessage;
                    model.responseTypes = ResponseTypes.Information;
                }
            }
            else
            {
                var CustInfo = _customerFunctions.GetAll();

                if (CustInfo.GenericClassList != null && CustInfo.GenericClassList.Count > 0)
                {
                    foreach (var item in CustInfo.GenericClassList)
                    {
                        UserMaintenance_ViewModel user = new UserMaintenance_ViewModel();
                        user.Company = item.Company;
                        user.CustomerName = item.Customer;
                        user.Email = item.MainEmail;
                        user.MainPhoneNumber = item.MainPhone;
                        user.StreetAddress = item.Street1;
                        user.ID = item.ID;
                        if (!string.IsNullOrEmpty(item.AspNetUsersID))
                        {
                            user.HasLogin = true;
                        }
                        else
                        {
                            user.HasLogin = false;
                        }
                        model.GenericClassList.Add(user);
                    }
                }
                else
                {
                    model.GenericClassList = null;
                    model.ResponseMessage = CustInfo.ResponseMessage;
                    model.responseTypes = ResponseTypes.Information;
                }

            }

            return View(model);
        }

        public async Task<ActionResult> ViewUserInfo(int ID, string ResponseMessage = null, bool? ResponseSuccess = null, ResponseTypes? responseTypes = null, DateTime? Date = null)
        {
            ViewUserInfo_ViewModel model = new ViewUserInfo_ViewModel();
            if (!string.IsNullOrEmpty(ResponseMessage))
            {
                model.ResponseMessage = ResponseMessage;
                model.ResponseSuccess = ResponseSuccess ?? false;
                switch (responseTypes)
                {
                    case ResponseTypes.Success:
                        model.responseTypes = ResponseTypes.Success;
                        break;
                    case ResponseTypes.Failure:
                        model.responseTypes = ResponseTypes.Failure;
                        break;
                    case ResponseTypes.Information:
                        model.responseTypes = ResponseTypes.Information;
                        break;
                    default:
                        model.responseTypes = ResponseTypes.Information;
                        break;
                }
            }
            if (ID > 0)
            {
                model.CustInfoModel = _customerFunctions.GetByID(ID).GenericClass;
                model.AccountChangeList = _loginAttemptFunctions.GetAllAccountChangesByIDByLoginDate(model.CustInfoModel.AspNetUsersID, Date ?? DateTime.Now).GenericClassList;
                model.LoginAttempts = _loginAttemptFunctions.GetAllAttemptsByIDByLoginDate(model.CustInfoModel.ID.ToString(), Date ?? DateTime.Now).GenericClassList;
                if (!string.IsNullOrEmpty(model.CustInfoModel.AspNetUsersID))
                {
                    model.userInfoModel = await UserManager.FindByIdAsync(model.CustInfoModel.AspNetUsersID);
                }
            }
            else
            {
                model.ResponseSuccess = false;
                model.CustInfoModel = null;
                model.userInfoModel = null;
                model.AccountChangeList = null;
                model.LoginAttempts = null;
                model.ResponseMessage = "You must select a user";
                model.responseTypes = ResponseTypes.Failure;

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeAccountInformation(Customers_Model model)
        {
            //get the orginal value for the logs            
            var FromObj = JsonConvert.SerializeObject(_customerFunctions.GetByID(model.ID).GenericClass);
            var Updated = _customerFunctions.Update(model);
            if (Updated.ResponseSuccess)
            {
                AccountChangeLog_Model ChangeModel = new AccountChangeLog_Model();
                ChangeModel.ChangedBy = User.Identity.GetUserId();
                ChangeModel.ChangedDateTime = DateTime.Now;
                ChangeModel.ChangeFrom = FromObj;
                ChangeModel.ChangeTo = JsonConvert.SerializeObject(model);
                ChangeModel.UserIDChanged = model.ID.ToString();
                _loginAttemptFunctions.LogAccountChange(ChangeModel);
            }
            return RedirectToAction("ViewUserInfo", "Admin", new
            {
                ID = model.ID,
                ResponseMessage = Updated.ResponseMessage,
                ResponseSuccess = Updated.ResponseSuccess,
                responseTypes = Updated.responseTypes
            });
        }

        public ActionResult UserRoleManagement(int ID)
        {
            //get the roles of a user by the aspnetuserid which we will get from the tied account
            var user = _customerFunctions.GetByID(ID).GenericClass;
            List<string> userRoles = UserManager.GetRoles(user.AspNetUsersID).ToList();

            var allRoles = new ApplicationDbContext().Roles.OrderBy(r => r.Name).ToList();
            List<string> Roles = new List<string>();
            foreach (var item in allRoles)
            {
                if (item.Name != "WebAdmin")
                {
                    Roles.Add(item.Name.ToString());
                }
            }

            var model = new UserRoleManagement_ViewModel { allRoles = Roles, UserRoles = userRoles, ID = user.ID, ASPNetUserID = user.AspNetUsersID };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoleToUser(string Role, int ID, string ASPNetUserID)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                var Added = UserManager.AddToRole(ASPNetUserID, Role);

                if (Added.Succeeded)
                {
                    response.ResponseMessage = "Successfully added User to Role";
                    response.ResponseSuccess = true;
                    response.responseTypes = ResponseTypes.Success;
                }
                else
                {
                    response.ResponseMessage = "Unable to add User to Role";
                    response.ResponseSuccess = false;
                    response.responseTypes = ResponseTypes.Failure;
                }
            }
            catch (Exception ex)
            {
                _loggerFunctions.Log(ex.ToString(), _helper.GetIp());
                response.ResponseMessage = ex.ToString();
                response.ResponseSuccess = false;
                response.responseTypes = ResponseTypes.Failure;
            }


            return RedirectToAction("ViewUserInfo", "Admin", new
            {
                ID = ID,
                ResponseMessage = response.ResponseMessage,
                ResponseSuccess = response.ResponseSuccess,
                responseTypes = response.responseTypes
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveRoleFromUser(string Role, int ID, string ASPNetUserID)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                var Removed = UserManager.RemoveFromRole(ASPNetUserID, Role);

                if (Removed.Succeeded)
                {
                    response.ResponseMessage = "Successfully removed User from Role";
                    response.ResponseSuccess = true;
                    response.responseTypes = ResponseTypes.Success;
                }
                else
                {
                    response.ResponseMessage = "Unable to remove User from Role";
                    response.ResponseSuccess = false;
                    response.responseTypes = ResponseTypes.Failure;
                }
            }
            catch (Exception ex)
            {
                _loggerFunctions.Log(ex.ToString(), _helper.GetIp());
                response.ResponseMessage = ex.ToString();
                response.ResponseSuccess = false;
                response.responseTypes = ResponseTypes.Failure;
            }


            return RedirectToAction("ViewUserInfo", "Admin", new
            {
                ID = ID,
                ResponseMessage = response.ResponseMessage,
                ResponseSuccess = response.ResponseSuccess,
                responseTypes = response.responseTypes
            });
        }

        public ActionResult TieAccount(int ID, string ResponseMessage = null, bool? ResponseSuccess = null, ResponseTypes? responseTypes = null)
        {
            TieAccount_ViewModel model = new TieAccount_ViewModel();
            model.ID = ID;
            model.ResponseSuccess = ResponseSuccess ?? false;
            model.ResponseMessage = ResponseMessage;
            switch (responseTypes)
            {
                case ResponseTypes.Success:
                    model.responseTypes = ResponseTypes.Success;
                    break;
                case ResponseTypes.Failure:
                    model.responseTypes = ResponseTypes.Failure;
                    break;
                case ResponseTypes.Information:
                    model.responseTypes = ResponseTypes.Information;
                    break;
                default:
                    model.responseTypes = ResponseTypes.Information;
                    break;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTieAccount(string Login, int ID)
        {
            ResponseBase response = new ResponseBase();
            if (ModelState.IsValid)
            {
                var Customer = _customerFunctions.GetByID(ID);

                if (!string.IsNullOrEmpty(Login) && Customer.GenericClass != null && Customer.GenericClass.ID > 0)
                {
                    ApplicationUser user = new ApplicationUser();
                    using (var ctx = new ApplicationDbContext())
                    {
                        user = ctx.Users.FirstOrDefault(s => s.Email == Login);
                        if (user == null)
                        {
                            user = ctx.Users.FirstOrDefault(s => s.UserName == Login);
                        }
                    }

                    if (user != null)
                    {
                        Customer.GenericClass.AspNetUsersID = user.Id;
                        var Updated = _customerFunctions.Update(Customer.GenericClass);

                        switch (Updated.responseTypes)
                        {
                            case BusinessLayer.Models.ResponseTypes.Success:
                                response.responseTypes = ResponseTypes.Success;
                                break;
                            case BusinessLayer.Models.ResponseTypes.Failure:
                                response.responseTypes = ResponseTypes.Failure;
                                break;
                            case BusinessLayer.Models.ResponseTypes.Information:
                                response.responseTypes = ResponseTypes.Information;
                                break;
                            default:
                                response.responseTypes = ResponseTypes.Information;
                                break;
                        }
                        response.ResponseSuccess = Updated.ResponseSuccess;
                        response.ResponseMessage = Updated.ResponseMessage;
                    }
                    else
                    {
                        return RedirectToAction("TieAccount", "Admin", new
                        {
                            ID = ID,
                            ResponseMessage = "There is no Login with the information provided " + Login,
                            ResponseSuccess = false,
                            responseTypes = ResponseTypes.Failure
                        });
                    }
                }
                else
                {
                    response.responseTypes = ResponseTypes.Information;
                    response.ResponseMessage = "You must enter a Username or Email address";
                    response.ResponseSuccess = false;
                }

                return RedirectToAction("ViewUserInfo", "Admin", new
                {
                    ID = ID,
                    ResponseMessage = response.ResponseMessage,
                    ResponseSuccess = response.ResponseSuccess,
                    responseTypes = response.responseTypes
                });
            }
            else
            {
                return RedirectToAction("TieAccount", "Admin", new
                {
                    ID = ID,
                    ResponseMessage = "You must enter a User Name or Emaill address to tie to.",
                    ResponseSuccess = false,
                    responseTypes = ResponseTypes.Failure
                });
            }


        }

        public ActionResult SendForgotPasswordLink(int ID)
        {
            var user = UserManager.FindById(_customerFunctions.GetByID(ID).GenericClass.AspNetUsersID);
            string code = UserManager.GeneratePasswordResetToken(user.Id);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            _emailFunctions.SendMail(user.Email, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");

            ResponseBase response = new ResponseBase();

            return RedirectToAction("ViewUserInfo", "Admin", new
            {
                ID = ID,
                ResponseMessage = "Email sent to " + user.Email,
                ResponseSuccess = true,
                responseTypes = ResponseTypes.Success
            });
        }

        public ActionResult UnlockAccount(int ID)
        {
            var user = UserManager.FindById(_customerFunctions.GetByID(ID).GenericClass.AspNetUsersID);
            user.LockoutEndDateUtc = null;
            user.AccessFailedCount = 0;
            var Unlocked = UserManager.Update(user);
            if (Unlocked.Succeeded)
            {
                return RedirectToAction("ViewUserInfo", "Admin", new
                {
                    ID = ID,
                    ResponseMessage = "Un Locked account with email address " + user.Email,
                    ResponseSuccess = true,
                    responseTypes = ResponseTypes.Success
                });
            }
            else
            {
                return RedirectToAction("ViewUserInfo", "Admin", new
                {
                    ID = ID,
                    ResponseMessage = "Failed to Unloack account with email address " + user.Email,
                    ResponseSuccess = false,
                    responseTypes = ResponseTypes.Failure
                });
            }
        }

        public ActionResult LockAccount(int ID)
        {
            var user = UserManager.FindById(_customerFunctions.GetByID(ID).GenericClass.AspNetUsersID);
            user.LockoutEndDateUtc = DateTime.UtcNow.AddDays(365 * 200);
            user.AccessFailedCount = 5;
            var Locked = UserManager.Update(user);

            if (Locked.Succeeded)
            {
                return RedirectToAction("ViewUserInfo", "Admin", new
                {
                    ID = ID,
                    ResponseMessage = "Locked account with email address  " + user.Email,
                    ResponseSuccess = true,
                    responseTypes = ResponseTypes.Success
                });
            }
            else
            {
                return RedirectToAction("ViewUserInfo", "Admin", new
                {
                    ID = ID,
                    ResponseMessage = "Failed to Lock account with email address  " + user.Email,
                    ResponseSuccess = false,
                    responseTypes = ResponseTypes.Failure
                });
            }


        }

        public ActionResult RemoveAccount(int ID)
        {
            var model = new RemoveAccount_ViewModel { ID = ID };
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteAccount(int ID)
        {
            var user = UserManager.FindById(_customerFunctions.GetByID(ID).GenericClass.AspNetUsersID);
            //remove the login
            var Removed = UserManager.Delete(user);
            if (Removed.Succeeded)
            {
                var customerinfo = _customerFunctions.GetByID(ID).GenericClass;
                customerinfo.AspNetUsersID = null;
                var Updated = _customerFunctions.Update(customerinfo);

                return RedirectToAction("ViewUserInfo", "Admin", new
                {
                    ID = ID,
                    ResponseMessage = Updated.ResponseMessage,
                    ResponseSuccess = Updated.ResponseSuccess,
                    responseTypes = Updated.responseTypes
                });

            }
            else
            {
                return RedirectToAction("ViewUserInfo", "Admin", new
                {
                    ID = ID,
                    ResponseMessage = "Unable to remove account with email " + user.Email,
                    ResponseSuccess = false,
                    responseTypes = ResponseTypes.Failure
                });
            }

            //update the customer table aspnetuserid to null

        }

        public ActionResult CreateNewCustomerAccount()
        {
            //pass in the industry type list so that it can be used in a drop down
            var BusinessTypesModel = _typeFunctions.GetAllBusinessTypes(true);
            List<string> BusinessTypesList = new List<string>();
            foreach (var item in BusinessTypesModel.GenericClassList)
            {
                BusinessTypesList.Add(item.Type);
            }
            var model = new CreateNewCustomerAccount_ViewModel { IndustryTypes = BusinessTypesList, ResponseSuccess = true, responseTypes = ResponseTypes.Success, ResponseMessage = string.Empty };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewCustomerAccount(CreateNewCustomerAccount_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customers_Model cm = new Customers_Model();
                cm.AltEmail1 = model.AltEmail1;
                cm.AspNetUsersID = null;
                cm.City = model.City;
                cm.Company = model.Company;
                cm.Customer = model.Customer;
                cm.DocLink = null;
                cm.EIN = model.EIN;
                cm.EnterDate = DateTime.Now;
                cm.FEIN = model.FEIN;
                cm.IndustryType = model.IndustryType;
                cm.MainEmail = model.MainEmail;
                cm.MainPhone = model.MainPhone;
                cm.Mobile = model.Mobile;
                cm.OBN = model.OBN;
                cm.OMMALicense = model.OMMALicense;
                cm.State = model.State;
                cm.Street1 = model.Street1;
                cm.Zip = model.Zip;

                var Inserted = _customerFunctions.Add(cm);

                if (Inserted.ResponseSuccess)
                {
                    return RedirectToAction("ViewUserInfo", "Admin", new
                    {
                        ID = Inserted.ResponseInt,
                        ResponseMessage = Inserted.ResponseMessage,
                        ResponseSuccess = Inserted.ResponseSuccess,
                        responseTypes = Inserted.responseTypes
                    });
                }
                else
                {
                    model.ResponseSuccess = Inserted.ResponseSuccess;
                    model.ResponseMessage = Inserted.ResponseMessage;
                    switch (Inserted.responseTypes)
                    {
                        case BusinessLayer.Models.ResponseTypes.Success:
                            model.responseTypes = ResponseTypes.Success;
                            break;
                        case BusinessLayer.Models.ResponseTypes.Failure:
                            model.responseTypes = ResponseTypes.Failure;
                            break;
                        case BusinessLayer.Models.ResponseTypes.Information:
                            model.responseTypes = ResponseTypes.Information;
                            break;
                        default:
                            break;
                    }
                    return View(model);
                }
            }
            model.ResponseSuccess = false;
            model.ResponseMessage = "There was an error";
            model.responseTypes = ResponseTypes.Failure;
            //modelstate is not valid
            return View(model);
        }

        #endregion

        #region Order Status 
        public ActionResult OrderStatus(ResponseBase response = null)
        {
            var Status = _orderStatusFucntions.GetAll();

            OrderStatus_ViewModel model = new OrderStatus_ViewModel();
            model.OrderStatus = Status.GenericClassList;

            if (string.IsNullOrEmpty(response.ResponseMessage) && Status.ResponseSuccess)
            {
                model.ResponseSuccess = Status.ResponseSuccess;
                model.ResponseMessage = Status.ResponseMessage;
                switch (Status.responseTypes)
                {
                    case BusinessLayer.Models.ResponseTypes.Success:
                        model.responseTypes = ResponseTypes.Success;
                        break;
                    case BusinessLayer.Models.ResponseTypes.Failure:
                        model.responseTypes = ResponseTypes.Failure;
                        break;
                    case BusinessLayer.Models.ResponseTypes.Information:
                        model.responseTypes = ResponseTypes.Information;
                        break;
                    default:
                        model.responseTypes = ResponseTypes.Failure;
                        break;
                }
            }
            else
            {
                model.ResponseSuccess = response.ResponseSuccess;
                model.ResponseMessage = response.ResponseMessage;
                switch (response.responseTypes)
                {
                    case ResponseTypes.Success:
                        model.responseTypes = ResponseTypes.Success;
                        break;
                    case ResponseTypes.Failure:
                        model.responseTypes = ResponseTypes.Failure;
                        break;
                    case ResponseTypes.Information:
                        model.responseTypes = ResponseTypes.Information;
                        break;
                    default:
                        model.responseTypes = ResponseTypes.Failure;
                        break;
                }
            }

            return View(model);
        }
        public ActionResult CreateOrderStatus()
        {
            return PartialView("_CreateOrderStatus");
        }
        public ActionResult EditOrderStatus(int ID)
        {
            EditOrderStatus_ViewModel model = new EditOrderStatus_ViewModel();
            try
            {
                var OrderStatus = _orderStatusFucntions.GetByID(ID);
                if (OrderStatus.ResponseSuccess)
                {
                    model.ID = OrderStatus.GenericClass.ID;
                    model.Status = OrderStatus.GenericClass.Status;
                }
                else
                {
                    return RedirectToAction("OrderStatus", new ResponseBase { ResponseSuccess = OrderStatus.ResponseSuccess, responseTypes = ResponseTypes.Information, ResponseMessage = OrderStatus.ResponseMessage });
                }
            }
            catch (Exception ex)
            {
                _loggerFunctions.Log(ex.ToString(), _helper.GetIp());
                return RedirectToAction("OrderStatus", new ResponseBase { ResponseSuccess = false, responseTypes = ResponseTypes.Failure, ResponseMessage = ex.ToString() });
            }
            return PartialView("_EditOrderStatus", model);
        }
        [HttpPost]
        public ActionResult SaveOrderStatus(CreateOrderStatus_ViewModel model)
        {
            OrderStatus_Models toAdd = new OrderStatus_Models();
            toAdd.ID = model.ID;
            toAdd.Status = model.Status;

            var Saved = _orderStatusFucntions.Add(toAdd);

            ResponseBase response = new ResponseBase();
            response.ResponseInt = Saved.ResponseInt;
            response.ResponseListInt = Saved.ResponseListInt;
            response.ResponseListString = Saved.ResponseListString;
            response.ResponseMessage = Saved.ResponseMessage;
            response.ResponseString = Saved.ResponseString;
            response.ResponseSuccess = Saved.ResponseSuccess;
            switch (Saved.responseTypes)
            {
                case BusinessLayer.Models.ResponseTypes.Success:
                    response.responseTypes = ResponseTypes.Success;
                    break;
                case BusinessLayer.Models.ResponseTypes.Failure:
                    response.responseTypes = ResponseTypes.Failure;
                    break;
                case BusinessLayer.Models.ResponseTypes.Information:
                    response.responseTypes = ResponseTypes.Information;
                    break;
                default:
                    response.responseTypes = ResponseTypes.Failure;
                    break;
            }

            return RedirectToAction("OrderStatus", response);
        }
        [HttpPost]
        public ActionResult SaveOrderStatusEdit(EditOrderStatus_ViewModel model)
        {

            ResponseBase response = new ResponseBase();

            if (model.ID > 0)
            {
                OrderStatus_Models OrderStatus = new OrderStatus_Models();
                OrderStatus.ID = model.ID;
                OrderStatus.Status = model.Status;

                var Updated = _orderStatusFucntions.Update(OrderStatus);
                if (Updated.ResponseSuccess)
                {
                    response.ResponseSuccess = Updated.ResponseSuccess;
                    response.ResponseMessage = Updated.ResponseMessage;
                    response.responseTypes = ResponseTypes.Success;
                }
                else
                {
                    response.ResponseSuccess = Updated.ResponseSuccess;
                    response.ResponseMessage = Updated.ResponseMessage;
                    switch (Updated.responseTypes)
                    {
                        case BusinessLayer.Models.ResponseTypes.Success:
                            response.responseTypes = ResponseTypes.Success;
                            break;
                        case BusinessLayer.Models.ResponseTypes.Failure:
                            response.responseTypes = ResponseTypes.Failure;
                            break;
                        case BusinessLayer.Models.ResponseTypes.Information:
                            response.responseTypes = ResponseTypes.Information;
                            break;
                        default:
                            response.responseTypes = ResponseTypes.Failure;
                            break;
                    }
                }
            }
            else
            {
                response.ResponseSuccess = false;
                response.ResponseMessage = "You must provide a Order Status ID.";
                response.responseTypes = ResponseTypes.Failure;
            }

            return RedirectToAction("OrderStatus", response);
        }
        [HttpPost]
        public ActionResult DeleteOrderStatus(int ID)
        {
            ResponseBase response = new ResponseBase();

            if (ID > 0)
            {
                var Deleted = _orderStatusFucntions.Delete(ID);
                if (Deleted.ResponseSuccess)
                {
                    response.ResponseSuccess = Deleted.ResponseSuccess;
                    response.ResponseMessage = Deleted.ResponseMessage;
                    response.responseTypes = ResponseTypes.Success;
                }
                else
                {
                    response.ResponseSuccess = Deleted.ResponseSuccess;
                    response.ResponseMessage = Deleted.ResponseMessage;
                    switch (Deleted.responseTypes)
                    {
                        case BusinessLayer.Models.ResponseTypes.Success:
                            response.responseTypes = ResponseTypes.Success;
                            break;
                        case BusinessLayer.Models.ResponseTypes.Failure:
                            response.responseTypes = ResponseTypes.Failure;
                            break;
                        case BusinessLayer.Models.ResponseTypes.Information:
                            response.responseTypes = ResponseTypes.Information;
                            break;
                        default:
                            response.responseTypes = ResponseTypes.Failure;
                            break;
                    }
                }
            }
            else
            {
                response.ResponseSuccess = false;
                response.ResponseMessage = "You must provide a Order Status ID.";
                response.responseTypes = ResponseTypes.Failure;
            }

            return RedirectToAction("OrderStatus", response);

        }
        #endregion

        #region Product
        public ActionResult Product(ResponseBase response = null)
        {
            Product_ViewModel model = new Product_ViewModel();
            var Products = _productFunctions.GetAllByIsActive(true);
            model.ListProducts = Products.GenericClassList;

            if (string.IsNullOrEmpty(response.ResponseMessage) && Products.ResponseSuccess)
            {
                model.ResponseSuccess = Products.ResponseSuccess;
                model.ResponseMessage = Products.ResponseMessage;
                switch (Products.responseTypes)
                {
                    case BusinessLayer.Models.ResponseTypes.Success:
                        model.responseTypes = ResponseTypes.Success;
                        break;
                    case BusinessLayer.Models.ResponseTypes.Failure:
                        model.responseTypes = ResponseTypes.Failure;
                        break;
                    case BusinessLayer.Models.ResponseTypes.Information:
                        model.responseTypes = ResponseTypes.Information;
                        break;
                    default:
                        model.responseTypes = ResponseTypes.Failure;
                        break;
                }
            }
            else
            {
                model.ResponseSuccess = response.ResponseSuccess;
                model.ResponseMessage = response.ResponseMessage;
                switch (response.responseTypes)
                {
                    case ResponseTypes.Success:
                        model.responseTypes = ResponseTypes.Success;
                        break;
                    case ResponseTypes.Failure:
                        model.responseTypes = ResponseTypes.Failure;
                        break;
                    case ResponseTypes.Information:
                        model.responseTypes = ResponseTypes.Information;
                        break;
                    default:
                        model.responseTypes = ResponseTypes.Failure;
                        break;
                }
            }
            return View(model);
        }
        public ActionResult CreateProduct()
        {
            return PartialView("_CreateProduct", new CreateProduct_ViewModel { ProductGroupList = _productGroupFunctions.GetAllByIsActive(true).GenericClassList });
        }
        public ActionResult EditProduct(int ID)
        {
            EditProduct_ViewModel model = new EditProduct_ViewModel();

            var Product = _productFunctions.GetByID(ID);

            if (Product.ResponseSuccess)
            {
                model.CartGram = Product.GenericClass.CartGram;
                model.Description = Product.GenericClass.Description;
                model.Dominant = Product.GenericClass.Dominant;
                model.ID = Product.GenericClass.ID;
                model.IsActive = Product.GenericClass.IsActive;
                model.PricePerUnit = Product.GenericClass.PricePerUnit;
                model.ProductGroup = Product.GenericClass.ProductGroup;
                model.ProductImage = Product.GenericClass.ProductImage;
                model.Strain = Product.GenericClass.Strain;
                model.Type = Product.GenericClass.Type;
                model.ProductGroupList = _productGroupFunctions.GetAllByIsActive(true).GenericClassList;
            }
            else
            {
                return RedirectToAction("Product", new ResponseBase { ResponseSuccess = Product.ResponseSuccess, responseTypes = ResponseTypes.Information, ResponseMessage = Product.ResponseMessage });
            }

            return PartialView("_EditProduct", model);
        }
        [HttpPost]
        public ActionResult SaveNewProduct(CreateProduct_ViewModel model)
        {

            Product_Models product = new Product_Models();

            HttpPostedFileBase file = Request.Files["ProductImage"];

            product.CartGram = model.CartGram;
            product.Description = model.Description;
            product.Dominant = model.Dominant;
            product.IsActive = true;
            product.PricePerUnit = model.PricePerUnit;
            product.ProductGroup = "Group 1";
            product.ProductImage = ConvertToBytes(file);
            product.Strain = model.Strain;
            product.Type = model.Type;

            var Saved = _productFunctions.Add(product);

            ResponseBase response = new ResponseBase();
            response.ResponseInt = Saved.ResponseInt;
            response.ResponseListInt = Saved.ResponseListInt;
            response.ResponseListString = Saved.ResponseListString;
            response.ResponseMessage = Saved.ResponseMessage;
            response.ResponseString = Saved.ResponseString;
            response.ResponseSuccess = Saved.ResponseSuccess;
            switch (Saved.responseTypes)
            {
                case BusinessLayer.Models.ResponseTypes.Success:
                    response.responseTypes = ResponseTypes.Success;
                    break;
                case BusinessLayer.Models.ResponseTypes.Failure:
                    response.responseTypes = ResponseTypes.Failure;
                    break;
                case BusinessLayer.Models.ResponseTypes.Information:
                    response.responseTypes = ResponseTypes.Information;
                    break;
                default:
                    response.responseTypes = ResponseTypes.Failure;
                    break;
            }

            return RedirectToAction("Product", response);
        }
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
        [HttpPost]
        public ActionResult SaveEditProduct(EditProduct_ViewModel model)
        {
            ResponseBase response = new ResponseBase();

            if (model.ID > 0)
            {
                byte[] PI = null;
                HttpPostedFileBase file = Request.Files["NewProductImage"];
                if (file.ContentLength > 0)
                {
                    PI = ConvertToBytes(file);
                }
                else
                {
                    PI = _productFunctions.GetByID(model.ID).GenericClass.ProductImage;
                }

                var Updated = _productFunctions.Update(new Product_Models { CartGram = model.CartGram, Description = model.Description, Dominant = model.Dominant, ID = model.ID, IsActive = false, PricePerUnit = model.PricePerUnit, ProductGroup = model.ProductGroup, ProductImage = PI, Strain = model.Strain, Type = model.Type });
                if (Updated.ResponseSuccess)
                {
                    var Added = _productFunctions.Add(new Product_Models { CartGram = model.CartGram, Description = model.Description, Dominant = model.Dominant, IsActive = model.IsActive, PricePerUnit = model.PricePerUnit, ProductGroup = model.ProductGroup, ProductImage = PI, Strain = model.Strain, Type = model.Type });
                    if (Added.ResponseSuccess)
                    {
                        return RedirectToAction("Product", new ResponseBase { ResponseInt = Added.ResponseInt, ResponseMessage = Added.ResponseMessage, ResponseSuccess = Added.ResponseSuccess, responseTypes = ResponseTypes.Success });
                    }
                    else
                    {
                        return RedirectToAction("Product", new ResponseBase { ResponseInt = Added.ResponseInt, ResponseMessage = Added.ResponseMessage, ResponseSuccess = Added.ResponseSuccess, responseTypes = ResponseTypes.Failure });
                    }
                }
                else
                {
                    return RedirectToAction("Product", new ResponseBase { ResponseSuccess = Updated.ResponseSuccess, ResponseMessage = Updated.ResponseMessage, responseTypes = ResponseTypes.Failure });
                }
            }
            else
            {
                return RedirectToAction("Product", new ResponseBase { ResponseSuccess = false, ResponseMessage = "You must provide a Product ID.", responseTypes = ResponseTypes.Failure });
            }
        }
        [HttpPost]
        public ActionResult DeleteProduct(int ID)
        {
            var Product = _productFunctions.GetByID(ID);
            if (Product.ResponseSuccess)
            {
                var Updated = _productFunctions.Update(new Product_Models { CartGram = Product.GenericClass.CartGram, Description = Product.GenericClass.Description, Dominant = Product.GenericClass.Dominant, ID = Product.GenericClass.ID, IsActive = false, PricePerUnit = Product.GenericClass.PricePerUnit, ProductGroup = Product.GenericClass.ProductGroup, ProductImage = Product.GenericClass.ProductImage, Strain = Product.GenericClass.Strain, Type = Product.GenericClass.Type });
                if (Updated.ResponseSuccess)
                { return RedirectToAction("Product", new ResponseBase { ResponseMessage = Updated.ResponseMessage, ResponseSuccess = Updated.ResponseSuccess, responseTypes = ResponseTypes.Success }); }
                else
                { return RedirectToAction("Product", new ResponseBase { ResponseMessage = Updated.ResponseMessage, ResponseSuccess = Updated.ResponseSuccess, responseTypes = ResponseTypes.Failure }); }
            }
            else
            { return RedirectToAction("Product", new ResponseBase { ResponseMessage = Product.ResponseMessage, ResponseSuccess = Product.ResponseSuccess, responseTypes = ResponseTypes.Failure }); }

        }
        #endregion

        #region Product Groups 
        public ActionResult ProductGroup(ResponseBase response = null)
        {
            ProductGroup_ViewModel model = new ProductGroup_ViewModel();
            var ProductGroups = _productGroupFunctions.GetAllByIsActive(true);

            bool succes = false;
            string message = string.Empty;
            ResponseTypes responseTypes = ResponseTypes.Failure;
            if (response != null)
            {
                succes = response.ResponseSuccess;
                message = response.ResponseMessage;
                responseTypes = response.responseTypes;
            }
            if (ProductGroups.GenericClassList != null && ProductGroups.GenericClassList.Count > 0)
            { return View(new ProductGroup_ViewModel { ListProductGroups = ProductGroups.GenericClassList, responseTypes = ResponseTypes.Success, ResponseMessage = ProductGroups.ResponseMessage, ResponseSuccess = ProductGroups.ResponseSuccess }); }
            else
            { return View(new ProductGroup_ViewModel { ListProductGroups = ProductGroups.GenericClassList, responseTypes = ResponseTypes.Information, ResponseMessage = ProductGroups.ResponseMessage, ResponseSuccess = ProductGroups.ResponseSuccess }); }          
        }
        public ActionResult CreateProductGroup()
        {
            return PartialView("_CreateProductGroup");
        }
        public ActionResult EditProductGroup(int ID)
        {
            EditProductGroup_ViewModel model = null;

            var ProductGroup = _productGroupFunctions.GetByID(ID);

            if (ProductGroup.ResponseSuccess)
            {
                model = new EditProductGroup_ViewModel { GroupName = ProductGroup.GenericClass.GroupName, ID = ProductGroup.GenericClass.ID, IsActive = ProductGroup.GenericClass.IsActive };
            }
            else
            {
                return RedirectToAction("ProductGroup", new ResponseBase { ResponseSuccess = ProductGroup.ResponseSuccess, responseTypes = ResponseTypes.Information, ResponseMessage = ProductGroup.ResponseMessage });
            }
            return PartialView("_EditProductGroup", model);
        }

        [HttpPost]
        public ActionResult SaveNewProductGroup(CreateProductGroup_ViewModel model)
        {
            var Saved = _productGroupFunctions.Add(new ProductGroup_Models { GroupName = model.GroupName, IsActive = model.IsActive });
            return RedirectToAction("ProductGroup", new ResponseBase { ResponseInt = Saved.ResponseInt, ResponseMessage = Saved.ResponseMessage, ResponseSuccess = Saved.ResponseSuccess, responseTypes = ResponseTypes.Success });
        }
        [HttpPost]
        public ActionResult SaveEditProductGroup(EditProductGroup_ViewModel model)
        {
            if (model.ID > 0)
            {
                var Updated = _productGroupFunctions.Update(new ProductGroup_Models { GroupName = model.GroupName, IsActive = false, ID = model.ID });
                if (Updated.ResponseSuccess)
                {
                    var Added = _productGroupFunctions.Add(new ProductGroup_Models { GroupName = model.GroupName, IsActive = model.IsActive });
                    if (Added.ResponseSuccess)
                    {
                        return RedirectToAction("ProductGroup", new ResponseBase { ResponseInt = Added.ResponseInt, ResponseMessage = Added.ResponseMessage, ResponseSuccess = Added.ResponseSuccess, responseTypes = ResponseTypes.Success });
                    }
                    else
                    {
                        return RedirectToAction("ProductGroup", new ResponseBase { ResponseInt = Added.ResponseInt, ResponseMessage = Added.ResponseMessage, ResponseSuccess = Added.ResponseSuccess, responseTypes = ResponseTypes.Failure });
                    }

                }
                return RedirectToAction("ProductGroup", new ResponseBase { ResponseSuccess = Updated.ResponseSuccess, ResponseMessage = Updated.ResponseMessage, responseTypes = ResponseTypes.Failure });
            }
            else
            {
                return RedirectToAction("ProductGroup", new ResponseBase { ResponseSuccess = false, ResponseMessage = "You must provide a Product Group ID.", responseTypes = ResponseTypes.Failure });
            }
        }

        [HttpPost]
        public ActionResult DeleteProductGroup(int ID)
        {

            var ProductGroup = _productGroupFunctions.GetByID(ID);
            if (ProductGroup.ResponseSuccess)
            {
                var Updated = _productGroupFunctions.Update(new ProductGroup_Models { GroupName = ProductGroup.GenericClass.GroupName, IsActive = false, ID = ProductGroup.GenericClass.ID });
                if (Updated.ResponseSuccess)
                { return RedirectToAction("Product", new ResponseBase { ResponseMessage = Updated.ResponseMessage, ResponseSuccess = Updated.ResponseSuccess, responseTypes = ResponseTypes.Success }); }
                else
                { return RedirectToAction("Product", new ResponseBase { ResponseMessage = Updated.ResponseMessage, ResponseSuccess = Updated.ResponseSuccess, responseTypes = ResponseTypes.Failure }); }
            }
            else
            { return RedirectToAction("Product", new ResponseBase { ResponseMessage = ProductGroup.ResponseMessage, ResponseSuccess = ProductGroup.ResponseSuccess, responseTypes = ResponseTypes.Failure }); }
        }

        #endregion

        #region Discounts

        public ActionResult Discount(ResponseBase response = null)
        {
            Discount_ViewModel model = new Discount_ViewModel();
            var Discounts = _discountFunctions.GetAllByIsActive(true);
            model.ListDiscounts = Discounts.GenericClassList;

            if (string.IsNullOrEmpty(response.ResponseMessage) && Discounts.ResponseSuccess)
            {
                model.ResponseSuccess = Discounts.ResponseSuccess;
                model.ResponseMessage = Discounts.ResponseMessage;
                switch (Discounts.responseTypes)
                {
                    case BusinessLayer.Models.ResponseTypes.Success:
                        model.responseTypes = ResponseTypes.Success;
                        break;
                    case BusinessLayer.Models.ResponseTypes.Failure:
                        model.responseTypes = ResponseTypes.Failure;
                        break;
                    case BusinessLayer.Models.ResponseTypes.Information:
                        model.responseTypes = ResponseTypes.Information;
                        break;
                    default:
                        model.responseTypes = ResponseTypes.Failure;
                        break;
                }
            }
            else
            {
                model.ResponseSuccess = response.ResponseSuccess;
                model.ResponseMessage = response.ResponseMessage;
                switch (response.responseTypes)
                {
                    case ResponseTypes.Success:
                        model.responseTypes = ResponseTypes.Success;
                        break;
                    case ResponseTypes.Failure:
                        model.responseTypes = ResponseTypes.Failure;
                        break;
                    case ResponseTypes.Information:
                        model.responseTypes = ResponseTypes.Information;
                        break;
                    default:
                        model.responseTypes = ResponseTypes.Failure;
                        break;
                }
            }
            return View(model);
        }
        public ActionResult CreateDiscount()
        {
            return PartialView("_CreateDiscount");
        }
        public ActionResult EditDiscount(int ID)
        {
            EditDiscount_ViewModel model = new EditDiscount_ViewModel();

            var Discount = _discountFunctions.GetByID(ID);

            if (Discount.ResponseSuccess)
            {
                model.DiscountAmount = Discount.GenericClass.DiscountAmount;
                model.ID = Discount.GenericClass.ID;
                model.IsActive = Discount.GenericClass.IsActive;
                model.Type = Discount.GenericClass.Type;
            }
            else
            {
                return RedirectToAction("OrderStatus", new ResponseBase { ResponseSuccess = Discount.ResponseSuccess, responseTypes = ResponseTypes.Information, ResponseMessage = Discount.ResponseMessage });
            }

            return PartialView("_EditDiscount", model);
        }
        [HttpPost]
        public ActionResult SaveNewDiscount(CreateDiscount_ViewModel model)
        {

            Discount_Models Discount = new Discount_Models();
            Discount.DiscountAmount = model.DiscountAmount;
            Discount.ID = model.ID;
            Discount.IsActive = model.IsActive;
            Discount.Type = model.Type;

            var Saved = _discountFunctions.Add(Discount);

            ResponseBase response = new ResponseBase();
            response.ResponseInt = Saved.ResponseInt;
            response.ResponseListInt = Saved.ResponseListInt;
            response.ResponseListString = Saved.ResponseListString;
            response.ResponseMessage = Saved.ResponseMessage;
            response.ResponseString = Saved.ResponseString;
            response.ResponseSuccess = Saved.ResponseSuccess;
            switch (Saved.responseTypes)
            {
                case BusinessLayer.Models.ResponseTypes.Success:
                    response.responseTypes = ResponseTypes.Success;
                    break;
                case BusinessLayer.Models.ResponseTypes.Failure:
                    response.responseTypes = ResponseTypes.Failure;
                    break;
                case BusinessLayer.Models.ResponseTypes.Information:
                    response.responseTypes = ResponseTypes.Information;
                    break;
                default:
                    response.responseTypes = ResponseTypes.Failure;
                    break;
            }

            return RedirectToAction("Discount", response);
        }
        [HttpPost]
        public ActionResult SaveEditDiscount(EditDiscount_ViewModel model)
        {
            if (model.ID > 0)
            {
                var Updated = _discountFunctions.Update(new Discount_Models { DiscountAmount = model.DiscountAmount, ID = model.ID, IsActive = false, Type = model.Type });
                if (Updated.ResponseSuccess)
                {
                    var Added = _discountFunctions.Add(new Discount_Models { DiscountAmount = model.DiscountAmount, IsActive = model.IsActive, Type = model.Type });
                    if (Added.ResponseSuccess)
                    {
                        return RedirectToAction("Discount", new ResponseBase { ResponseInt = Added.ResponseInt, ResponseMessage = Added.ResponseMessage, ResponseSuccess = Added.ResponseSuccess, responseTypes = ResponseTypes.Success });
                    }
                    else
                    {
                        return RedirectToAction("Discount", new ResponseBase { ResponseInt = Added.ResponseInt, ResponseMessage = Added.ResponseMessage, ResponseSuccess = Added.ResponseSuccess, responseTypes = ResponseTypes.Failure });
                    }

                }
                return RedirectToAction("Discount", new ResponseBase { ResponseSuccess = Updated.ResponseSuccess, ResponseMessage = Updated.ResponseMessage, responseTypes = ResponseTypes.Failure });
            }
            else
            {
                return RedirectToAction("Discount", new ResponseBase { ResponseSuccess = false, ResponseMessage = "You must provide a Discount ID.", responseTypes = ResponseTypes.Failure });
            }
        }
        [HttpPost]
        public ActionResult DeleteDiscount(int ID)
        {
            var Discount = _discountFunctions.GetByID(ID);
            if (Discount.ResponseSuccess)
            {
                var Updated = _discountFunctions.Update(new Discount_Models { DiscountAmount = Discount.GenericClass.DiscountAmount, ID = Discount.GenericClass.ID, Type = Discount.GenericClass.Type, IsActive = false });
                if (Updated.ResponseSuccess)
                { return RedirectToAction("Discount", new ResponseBase { ResponseMessage = Updated.ResponseMessage, ResponseSuccess = Updated.ResponseSuccess, responseTypes = ResponseTypes.Success }); }
                else
                { return RedirectToAction("Discount", new ResponseBase { ResponseMessage = Updated.ResponseMessage, ResponseSuccess = Updated.ResponseSuccess, responseTypes = ResponseTypes.Failure }); }
            }
            else
            { return RedirectToAction("Discount", new ResponseBase { ResponseMessage = Discount.ResponseMessage, ResponseSuccess = Discount.ResponseSuccess, responseTypes = ResponseTypes.Failure }); }
        }
        #endregion

        [Authorize(Roles = "WebAdmin")]
        public ActionResult ApplicaitonLogs(DateTime? dateTime = null)
        {
            ApplicaitonLogs_ViewModel model = new ApplicaitonLogs_ViewModel();
            try
            {
                var Logs = _loggerFunctions.GetByDate(dateTime ?? DateTime.Now);
                model.ResponseSuccess = Logs.ResponseSuccess;
                model.ResponseMessage = Logs.ResponseMessage;

                if (Logs.GenericClassList != null && Logs.GenericClassList.Count > 0)
                {
                    model.ListLogs = Logs.GenericClassList;
                    model.responseTypes = Models.ResponseTypes.Success;
                }
                else
                {
                    model.responseTypes = Models.ResponseTypes.Information;
                }
            }
            catch (Exception ex)
            {
                _loggerFunctions.Log(ex.ToString(), _helper.GetIp());
                model.ResponseMessage = "There was an error while retrieving the logs.";
                model.ResponseSuccess = false;
                model.responseTypes = Models.ResponseTypes.Failure;
            }
            return View(model);
        }
    }
}