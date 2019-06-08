using BusinessLayer.Functions.Customer;
using BusinessLayer.Functions.Email;
using BusinessLayer.Functions.ErrorLogging;
using BusinessLayer.Functions.LoginLogger;
using BusinessLayer.Functions.Types;
using BusinessLayer.Models.CustomerModels;
using BusinessLayer.Models.LoginAttemptModels;
using BusinessLayer.Models.TypeModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using SimpleCure.Helpers;
using SimpleCure.Models;
using SimpleCure.Models.AdminModels;
using System;
using System.Collections.Generic;
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

        public AdminController()
        {
            _loggerFunctions = new LoggerFunctions();
            _typeFunctions = new TypeFunctions();
            _helper = new Helper();
            _customerFunctions = new CustomerFunctions();
            _emailFunctions = new EmailFunctions();
            _loginAttemptFunctions = new LoginAttemptFunctions();          
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
        //search option here
        public ActionResult UserMaintenance(string SearchTerm)
        {
            //List<UserMaintenance_ViewModel> model = new List<UserMaintenance_ViewModel>();
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
            var model = new CreateNewCustomerAccount_ViewModel { IndustryTypes = BusinessTypesList, ResponseSuccess = true, responseTypes = ResponseTypes.Success, ResponseMessage = string.Empty};
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