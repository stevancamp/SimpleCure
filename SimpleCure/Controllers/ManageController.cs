using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Functions.Customer;
using BusinessLayer.Functions.LoginLogger;
using BusinessLayer.Mappings;
using BusinessLayer.Models.CustomerModels;
using BusinessLayer.Models.LoginAttemptModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using SimpleCure.Models;

namespace SimpleCure.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        #region Injection        
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private LoginAttemptFunctions _loginAttemptFunctions;
        private CustomerFunctions _customerFunctions;
        private MapCustomers _mapCustomers;

        public ManageController()
        {
            _loginAttemptFunctions = new LoginAttemptFunctions();
            _customerFunctions = new CustomerFunctions();
            _mapCustomers = new MapCustomers();
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : message == ManageMessageId.ChangeEmailSuccess ? "Your Email address was updated"
                : message == ManageMessageId.ChangeEmailFailure ? "Your Email address was not updated"
                : message == ManageMessageId.ChangeUserNameSuccess ? "Your User Name was updated"
                : message == ManageMessageId.ChangeUserNameFailure ? "Your User Name was not updated"
                : message == ManageMessageId.ChangeAccountInformationSuccess ? "Your account information has been changed."
                : "";

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var customerinfo = _customerFunctions.GetByUserID(user.Id);
            CustomerInfoViewModel vm = new CustomerInfoViewModel();
            if (customerinfo.responseTypes == BusinessLayer.Models.ResponseTypes.Success && customerinfo.GenericClass != null)
            {
                vm.AltEmail1 = customerinfo.GenericClass.AltEmail1;
                vm.AspNetUsersID = customerinfo.GenericClass.AspNetUsersID;
                vm.City = customerinfo.GenericClass.City;
                vm.Company = customerinfo.GenericClass.Company;
                vm.Customer = customerinfo.GenericClass.Customer;
                vm.DocLink = customerinfo.GenericClass.DocLink;
                vm.EIN = customerinfo.GenericClass.EIN;
                vm.EnterDate = customerinfo.GenericClass.EnterDate;
                vm.FEIN = customerinfo.GenericClass.FEIN;
                vm.ID = customerinfo.GenericClass.ID;
                vm.IndustryType = customerinfo.GenericClass.IndustryType;
                vm.MainEmail = customerinfo.GenericClass.MainEmail;
                vm.MainPhone = customerinfo.GenericClass.MainPhone;
                vm.Mobile = customerinfo.GenericClass.Mobile;
                vm.OBN = customerinfo.GenericClass.OBN;
                vm.OMMALicense = customerinfo.GenericClass.OMMALicense;
                vm.State = customerinfo.GenericClass.State;
                vm.Street1 = customerinfo.GenericClass.Street1;
                vm.Zip = customerinfo.GenericClass.Zip;

                vm.JenBillTo = customerinfo.GenericClass.JenBillTo;
                vm.JenEIN = customerinfo.GenericClass.JenEmail;
                vm.JenEmail = customerinfo.GenericClass.JenEmail;
                vm.JenFirst = customerinfo.GenericClass.JenFirst;
                vm.JenLast = customerinfo.GenericClass.JenLast;
                vm.JenPhone = customerinfo.GenericClass.JenPhone;
            }
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(user.Id),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(user.Id),
                Logins = await UserManager.GetLoginsAsync(user.Id),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(user.Id),
                UserInfo = user,
                CustomerInfo = vm
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        public ActionResult ChangeEmailAddress(ManageMessageId? message)
        {
            var MessageError =
                 message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                 : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                 : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                 : message == ManageMessageId.Error ? "An error has occurred."
                 : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                 : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                 : message == ManageMessageId.ChangeEmailSuccess ? "Your Email address was updated"
                 : message == ManageMessageId.ChangeEmailFailure ? "Your Email address was not updated"
                 : string.Empty;

            var model = new ChangeEmailAddressViewModel
            {
                ErrorMessage = MessageError
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeEmailAddress(ChangeEmailAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                //check if email entered is the same email address of the user that is signed in currently

                var emailcheckuser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (emailcheckuser.Email.ToLower() != model.EmailAddress.ToLower() && emailcheckuser.UserName.ToLower() != model.EmailAddress.ToLower())
                {
                    //check if the email address they are trying to change to is already used as a email address or a username
                    var email = string.Empty;
                    ApplicationUser user = new ApplicationUser();
                    using (var ctx = new ApplicationDbContext())
                    {
                        user = ctx.Users.FirstOrDefault(s => s.Email == model.EmailAddress);
                        if (string.IsNullOrEmpty(email))
                        {
                            user = ctx.Users.FirstOrDefault(s => s.UserName == model.EmailAddress);
                        }
                    }
                    //we could not find anything with this email address so we are good to change to it
                    if (user == null)
                    {
                        //get the user information 
                        user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        var FromObj = JsonConvert.SerializeObject(user);
                        if (user != null)
                        {
                            user.Email = model.EmailAddress;

                            var EmailUpdateSuccess = await UserManager.UpdateAsync(user);

                            if (EmailUpdateSuccess.Succeeded)
                            {
                                AccountChangeLog_Model ChangeModel = new AccountChangeLog_Model();
                                ChangeModel.ChangedBy = User.Identity.GetUserId();
                                ChangeModel.ChangedDateTime = DateTime.Now;
                                ChangeModel.ChangeFrom = FromObj;
                                ChangeModel.ChangeTo = JsonConvert.SerializeObject(user);
                                ChangeModel.UserIDChanged = user.Id;
                                _loginAttemptFunctions.LogAccountChange(ChangeModel);

                                return RedirectToAction("Index", new { Message = ManageMessageId.ChangeEmailSuccess });
                            }
                            else
                            {
                                model.ErrorMessage = "Email update failed";
                                return View(model);
                            }
                        }
                    }
                    else
                    {
                        model.ErrorMessage = "Email/Username already in use";
                        return View(model);
                    }
                }
                else
                {
                    ApplicationUser user = new ApplicationUser();
                    user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    var FromObj = JsonConvert.SerializeObject(user);
                    emailcheckuser.Email = model.EmailAddress;
                    var EmailUpdateSuccess = await UserManager.UpdateAsync(emailcheckuser);
                    if (EmailUpdateSuccess.Succeeded)
                    {
                        AccountChangeLog_Model ChangeModel = new AccountChangeLog_Model();
                        ChangeModel.ChangedBy = User.Identity.GetUserId();
                        ChangeModel.ChangedDateTime = DateTime.Now;
                        ChangeModel.ChangeFrom = FromObj;
                        ChangeModel.ChangeTo = JsonConvert.SerializeObject(user);
                        ChangeModel.UserIDChanged = user.Id;
                        _loginAttemptFunctions.LogAccountChange(ChangeModel);

                        return RedirectToAction("Index", new { Message = ManageMessageId.ChangeEmailSuccess });
                    }
                    else
                    {
                        model.ErrorMessage = "Email update failed";
                        return View(model);
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult ChangeUserName(ManageMessageId? message)
        {
            var MessageError =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : message == ManageMessageId.ChangeEmailSuccess ? "Your Email address was updated"
                : message == ManageMessageId.ChangeEmailFailure ? "Your Email address was not updated"
                : message == ManageMessageId.ChangeUserNameSuccess ? "Your User Name was updated"
                : message == ManageMessageId.ChangeUserNameFailure ? "Your User Name was not updated"
                : string.Empty;

            var model = new ChangeUserNameViewModel
            {
                ErrorMessage = MessageError
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeUserName(ChangeUserNameViewModel model)
        {
            if (ModelState.IsValid)
            {
                //check if email entered is the same email address of the user that is signed in currently

                var emailcheckuser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (emailcheckuser.Email.ToLower() != model.UserName.ToLower() && emailcheckuser.UserName.ToLower() != model.UserName.ToLower())
                {
                    //check if the email address they are trying to change to is already used as a email address or a username
                    var email = string.Empty;
                    ApplicationUser user = new ApplicationUser();
                    using (var ctx = new ApplicationDbContext())
                    {
                        user = ctx.Users.FirstOrDefault(s => s.Email == model.UserName);
                        if (string.IsNullOrEmpty(email))
                        {
                            user = ctx.Users.FirstOrDefault(s => s.UserName == model.UserName);
                        }
                    }
                    //we could not find anything with this email address so we are good to change to it
                    if (user == null)
                    {
                        //get the user information 
                        user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        var FromObj = JsonConvert.SerializeObject(user);
                        if (user != null)
                        {
                            user.UserName = model.UserName;

                            var EmailUpdateSuccess = await UserManager.UpdateAsync(user);

                            if (EmailUpdateSuccess.Succeeded)
                            {
                                AccountChangeLog_Model ChangeModel = new AccountChangeLog_Model();
                                ChangeModel.ChangedBy = User.Identity.GetUserId();
                                ChangeModel.ChangedDateTime = DateTime.Now;
                                ChangeModel.ChangeFrom = FromObj;
                                ChangeModel.ChangeTo = JsonConvert.SerializeObject(user);
                                ChangeModel.UserIDChanged = user.Id;
                                _loginAttemptFunctions.LogAccountChange(ChangeModel);

                                return RedirectToAction("Index", new { Message = ManageMessageId.ChangeUserNameSuccess });
                            }
                            else
                            {
                                model.ErrorMessage = "User Name update failed";
                                return View(model);
                            }
                        }
                    }
                    else
                    {
                        model.ErrorMessage = "Email/Username already in use";
                        return View(model);
                    }
                }
                else
                {
                    ApplicationUser user = new ApplicationUser();
                    user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    var FromObj = JsonConvert.SerializeObject(user);
                    emailcheckuser.UserName = model.UserName;
                    var EmailUpdateSuccess = await UserManager.UpdateAsync(emailcheckuser);
                    if (EmailUpdateSuccess.Succeeded)
                    {
                        AccountChangeLog_Model ChangeModel = new AccountChangeLog_Model();
                        ChangeModel.ChangedBy = User.Identity.GetUserId();
                        ChangeModel.ChangedDateTime = DateTime.Now;
                        ChangeModel.ChangeFrom = FromObj;
                        ChangeModel.ChangeTo = JsonConvert.SerializeObject(user);
                        ChangeModel.UserIDChanged = user.Id;
                        _loginAttemptFunctions.LogAccountChange(ChangeModel);

                        return RedirectToAction("Index", new { Message = ManageMessageId.ChangeUserNameSuccess });
                    }
                    else
                    {
                        model.ErrorMessage = "User Name update failed";
                        return View(model);
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public ActionResult DeleteAccount(ManageMessageId? message)
        {
            var MessageError =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : message == ManageMessageId.ChangeEmailSuccess ? "Your Email address was updated"
                : message == ManageMessageId.ChangeEmailFailure ? "Your Email address was not updated"
                : string.Empty;

            var model = new DeleteAccountViewModel
            {
                ErrorMessage = MessageError
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfimredDeleteAccount()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var IsInCustTbl = _customerFunctions.GetByUserID(user.Id);
            if (IsInCustTbl.ResponseSuccess && IsInCustTbl.GenericClass != null && IsInCustTbl.responseTypes == BusinessLayer.Models.ResponseTypes.Success)
            {
                Customers_Model cm = new Customers_Model();
                cm.AltEmail1 = IsInCustTbl.GenericClass.AltEmail1;
                cm.AspNetUsersID = null;
                cm.City = IsInCustTbl.GenericClass.City;
                cm.Company = IsInCustTbl.GenericClass.Company;
                cm.Customer = IsInCustTbl.GenericClass.Customer;
                cm.DocLink = IsInCustTbl.GenericClass.DocLink;
                cm.EIN = IsInCustTbl.GenericClass.EIN;
                cm.EnterDate = IsInCustTbl.GenericClass.EnterDate;
                cm.FEIN = IsInCustTbl.GenericClass.FEIN;
                cm.ID = IsInCustTbl.GenericClass.ID;
                cm.IndustryType = IsInCustTbl.GenericClass.IndustryType;
                cm.MainEmail = IsInCustTbl.GenericClass.MainEmail;
                cm.MainPhone = IsInCustTbl.GenericClass.MainPhone;
                cm.Mobile = IsInCustTbl.GenericClass.Mobile;
                cm.OBN = IsInCustTbl.GenericClass.OBN;
                cm.OMMALicense = IsInCustTbl.GenericClass.OMMALicense;
                cm.State = IsInCustTbl.GenericClass.State;
                cm.Street1 = IsInCustTbl.GenericClass.Street1;
                cm.Zip = IsInCustTbl.GenericClass.Zip;

                cm.JenBillTo = IsInCustTbl.GenericClass.JenBillTo;
                cm.JenEIN = IsInCustTbl.GenericClass.JenEmail;
                cm.JenEmail = IsInCustTbl.GenericClass.JenEmail;
                cm.JenFirst = IsInCustTbl.GenericClass.JenFirst;
                cm.JenLast = IsInCustTbl.GenericClass.JenLast;
                cm.JenPhone = IsInCustTbl.GenericClass.JenPhone;

                _customerFunctions.Update(cm);
            }

            var Deleted = await UserManager.DeleteAsync(user);
            if (Deleted.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return RedirectToAction("DeleteAccount", new { Message = ManageMessageId.Error });
            }
        }


        public ActionResult ChangeAccountInformation(ManageMessageId? message)
        {
            var MessageError =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : message == ManageMessageId.ChangeEmailSuccess ? "Your Email address was updated"
                : message == ManageMessageId.ChangeEmailFailure ? "Your Email address was not updated"
                : message == ManageMessageId.ChangeUserNameSuccess ? "Your User Name was updated"
                : message == ManageMessageId.ChangeUserNameFailure ? "Your User Name was not updated"
                : string.Empty;
            var user = UserManager.FindById(User.Identity.GetUserId());
            var UserInfo = _customerFunctions.GetByUserID(user.Id);
            if (UserInfo.ResponseSuccess && UserInfo.GenericClass != null && UserInfo.responseTypes == BusinessLayer.Models.ResponseTypes.Success)
            {
                var model = new CustomerInfoViewModel
                {
                    AltEmail1 = UserInfo.GenericClass.AltEmail1 ?? string.Empty,
                    AspNetUsersID = user.Id,
                    City = UserInfo.GenericClass.City,
                    Company = UserInfo.GenericClass.Company,
                    Customer = UserInfo.GenericClass.Customer,
                    DocLink = UserInfo.GenericClass.DocLink,
                    EIN = UserInfo.GenericClass.EIN,
                    EnterDate = UserInfo.GenericClass.EnterDate,
                    FEIN = UserInfo.GenericClass.FEIN,
                    ID = UserInfo.GenericClass.ID,
                    IndustryType = UserInfo.GenericClass.IndustryType,
                    MainEmail = UserInfo.GenericClass.MainEmail,
                    MainPhone = UserInfo.GenericClass.MainPhone,
                    Mobile = UserInfo.GenericClass.Mobile,
                    OBN = UserInfo.GenericClass.OBN,
                    OMMALicense = UserInfo.GenericClass.OMMALicense,
                    State = UserInfo.GenericClass.State,
                    Street1 = UserInfo.GenericClass.Street1,
                    Zip = UserInfo.GenericClass.Zip,
                    ErrorMessage = MessageError
                };
                return View(model);
            }
            else
            {
                var model = new CustomerInfoViewModel
                {                  
                    ErrorMessage = MessageError
                };
                return View(model);
            }
              
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeAccountInformation(CustomerInfoViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = UserManager.FindById(User.Identity.GetUserId());
                var IsInCustTbl = _customerFunctions.GetByUserID(user.Id);
                    //.ResponseSuccess;
                Customers_Model cm = new Customers_Model();
                cm.AltEmail1 = model.AltEmail1;
                cm.AspNetUsersID = user.Id;
                cm.City = model.City;
                cm.Company = model.Company;
                cm.Customer = model.Customer;
                cm.DocLink = model.DocLink;
                cm.EIN = model.EIN;
                cm.EnterDate = model.EnterDate;
                cm.FEIN = model.FEIN;
                cm.ID = model.ID;
                cm.IndustryType = model.IndustryType;
                cm.MainEmail = model.MainEmail;
                cm.MainPhone = model.MainPhone;
                cm.Mobile = model.Mobile;
                cm.OBN = model.OBN;
                cm.OMMALicense = model.OMMALicense;
                cm.State = model.State;
                cm.Street1 = model.Street1;
                cm.Zip = model.Zip;

                if (IsInCustTbl.ResponseSuccess && IsInCustTbl.GenericClass != null && IsInCustTbl.responseTypes == BusinessLayer.Models.ResponseTypes.Success)
                //if (IsInCustTbl)
                {
                    var Updated = _customerFunctions.Update(cm);

                    if (Updated.ResponseSuccess)
                    {
                        return RedirectToAction("Index", new { Message = ManageMessageId.ChangeAccountInformationSuccess });
                    }
                    else
                    {
                        model.ErrorMessage = "Customer Information update failed";
                        return View(model);
                    }
                }
                else
                {
                    var Inserted = _customerFunctions.Add(cm);
                    if (Inserted.ResponseSuccess)
                    {
                        return RedirectToAction("Index", new { Message = ManageMessageId.ChangeAccountInformationSuccess });
                    }
                    else
                    {
                        model.ErrorMessage = "Customer Information update failed";
                        return View(model);
                    }
                }             
            }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error,
            ChangeEmailSuccess,
            ChangeEmailFailure,
            ChangeUserNameSuccess,
            ChangeUserNameFailure,
            ChangeAccountInformationSuccess
        }

        #endregion
    }
}