using BusinessLayer.Functions.ErrorLogging;
using BusinessLayer.Functions.Types;
using BusinessLayer.Models.TypeModels;
using SimpleCure.Helpers;
using SimpleCure.Models.AdminModels;
using System;
using System.Web.Mvc;

namespace SimpleCure.Controllers
{
    //have to be at least site admin for these functions
    public class AdminController : Controller
    {
        #region Injection
        private LoggerFunctions _loggerFunctions;
        private TypeFunctions _typeFunctions;
        private Helper _helper;

        public AdminController()
        {
            _loggerFunctions = new LoggerFunctions();
            _typeFunctions = new TypeFunctions();
            _helper = new Helper();
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

        #region Order Activity Types
        public ActionResult OrderActivityTypes(bool ActiveStatus = true, OrderActivityType_ViewModel model = null)
        {
            model.ActiveStatus = ActiveStatus;
            try
            {
                var OrderActivityTypes = _typeFunctions.GetAllOrderActivityTypes(ActiveStatus);
                if (string.IsNullOrEmpty(model.ResponseMessage))
                {
                    model.ResponseSuccess = OrderActivityTypes.ResponseSuccess;
                    model.ResponseMessage = OrderActivityTypes.ResponseMessage;
                    switch (OrderActivityTypes.responseTypes)
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
                if (OrderActivityTypes.GenericClassList != null && OrderActivityTypes.GenericClassList.Count > 0)
                {
                    model.ListOrderActivityTypes = OrderActivityTypes.GenericClassList;
                }
            }
            catch (Exception ex)
            {
                _loggerFunctions.Log(ex.ToString(), _helper.GetIp());
                model.ResponseMessage = "There was an error while retrieving the Order Activity Types.";
                model.ResponseSuccess = false;
            }

            return View(model);
        }
        public ActionResult CreateOrderActivityType()
        {
            return PartialView("_CreateOrderActivityType");
        }
        [HttpPost]
        public ActionResult SaveOrderActivityType(string OrderActivityType)
        {
            OrderActivityType_ViewModel model = new OrderActivityType_ViewModel();
            model.ActiveStatus = true;
            if (!string.IsNullOrEmpty(OrderActivityType))
            {
                try
                {
                    OrderActivityType_Model OrderActivityType_Model = new OrderActivityType_Model();
                    OrderActivityType_Model.Type = OrderActivityType;
                    OrderActivityType_Model.IsActive = true;
                    var Saved = _typeFunctions.AddOrderActivityType(OrderActivityType_Model);
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
                    model.ResponseMessage = "There was an error while saving the Order Activity Type " + OrderActivityType;
                }
            }
            else
            {
                model.responseTypes = Models.ResponseTypes.Failure;
                model.ResponseMessage = "You must submit a Order Activity Type Value";
            }

            return RedirectToAction("OrderActivityTypes", model);
        }
        public ActionResult EditOrderActivityType(int ID)
        {
            EditOrderActivityType_ViewModel model = new EditOrderActivityType_ViewModel();

            try
            {
                var OrderActivityType = _typeFunctions.GetOrderActivityTypeByID(ID);
                if (OrderActivityType.ResponseSuccess)
                {
                    model.ID = OrderActivityType.GenericClass.ID;
                    model.OrderActivityType = OrderActivityType.GenericClass.Type;
                    model.IsActive = OrderActivityType.GenericClass.IsActive;
                }
                else
                {
                    OrderActivityType_ViewModel returnModel = new OrderActivityType_ViewModel();
                    returnModel.ResponseMessage = "Unable to retrieve Order Activity Type by ID " + ID;
                    return View("OrderActivityTypes", model);
                }
            }
            catch (Exception ex)
            {
                _loggerFunctions.Log(ex.ToString(), _helper.GetIp());
                OrderActivityType_ViewModel returnModel = new OrderActivityType_ViewModel();
                returnModel.ResponseMessage = "There was an error while retrieving the Order Activity Type by ID " + ID;
                returnModel.ResponseSuccess = false;
                return RedirectToAction("OrderActivityTypes", model);
            }

            return PartialView("_EditOrderActivityType", model);
        }
        [HttpPost]
        public ActionResult SaveOrderActivityTypeEdit(FormCollection formCollection)
        {
            OrderActivityType_ViewModel model = new OrderActivityType_ViewModel();
            model.ActiveStatus = true;
            if (formCollection != null && !string.IsNullOrEmpty(formCollection["OrderActivityType"]))
            {
                var BTid = Convert.ToInt32(formCollection["ID"]);
                var BTtype = formCollection["OrderActivityType"];
                var BTisactive = Convert.ToBoolean(formCollection["IsActive"].Split(',')[0]);

                try
                {
                    OrderActivityType_Model OrderActivityType_Model = new OrderActivityType_Model();
                    OrderActivityType_Model.ID = BTid;
                    OrderActivityType_Model.Type = BTtype;
                    OrderActivityType_Model.IsActive = BTisactive;
                    var Updated = _typeFunctions.UpdateOrderActivityType(OrderActivityType_Model);
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
                    model.ResponseMessage = "There was an error while update the Order Activity Type " + formCollection["OrderActivityType"];
                    model.responseTypes = Models.ResponseTypes.Failure;
                }
            }
            else
            {
                model.ResponseMessage = "You must submit a Order Activity Type Value";
                model.responseTypes = Models.ResponseTypes.Failure;
            }
            return RedirectToAction("OrderActivityTypes", model);
        }
        [HttpPost]
        public ActionResult DeleteOrderActivityType(int ID)
        {
            OrderActivityType_ViewModel model = new OrderActivityType_ViewModel();
            model.ActiveStatus = true;
            if (ID > 0)
            {
                try
                {
                    var Deleted = _typeFunctions.DeleteOrderActivityType(ID);
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
                    model.ResponseMessage = "Unable to delete Order Activity Type with ID " + ID;
                    model.responseTypes = Models.ResponseTypes.Failure;
                }
            }
            else
            {
                model.ResponseMessage = "You must submit a Order Activity Type ID to delete";
                model.responseTypes = Models.ResponseTypes.Failure;
            }

            return RedirectToAction("OrderActivityTypes", model);
        }
        #endregion

        #region Order Info Product Group
        public ActionResult OrderInfoProductGroups(OrderInfoProductGroups_ViewModel model = null)
        {
            try
            {
                var OrderInfoProductGroups = _typeFunctions.GetAllOrderInfoProductGroups();
                if (string.IsNullOrEmpty(model.ResponseMessage))
                {
                    model.ResponseSuccess = OrderInfoProductGroups.ResponseSuccess;
                    model.ResponseMessage = OrderInfoProductGroups.ResponseMessage;
                    switch (OrderInfoProductGroups.responseTypes)
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
                if (OrderInfoProductGroups.GenericClassList != null && OrderInfoProductGroups.GenericClassList.Count > 0)
                {
                    model.ListOrderInfoProductGroups = OrderInfoProductGroups.GenericClassList;
                }
            }
            catch (Exception ex)
            {
                _loggerFunctions.Log(ex.ToString(), _helper.GetIp());
                model.ResponseMessage = "There was an error while retrieving the Order Info Product Group.";
                model.ResponseSuccess = false;
            }

            return View(model);
        }
        public ActionResult CreateOrderInfoProductGroup()
        {
            return PartialView("_CreateOrderInfoProductGroup");
        }
        [HttpPost]
        public ActionResult SaveOrderInfoProductGroup(string GroupName)
        {
            OrderInfoProductGroups_ViewModel model = new OrderInfoProductGroups_ViewModel();

            if (!string.IsNullOrEmpty(GroupName))
            {
                try
                {
                    OrderInfoProductGroups_Model OrderInfoProductGroup_Model = new OrderInfoProductGroups_Model();
                    OrderInfoProductGroup_Model.GroupName = GroupName;
                    var Saved = _typeFunctions.AddOrderInfoProductGroup(OrderInfoProductGroup_Model);
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
                    model.ResponseMessage = "There was an error while saving the Order Info Product Group " + GroupName;
                }
            }
            else
            {
                model.responseTypes = Models.ResponseTypes.Failure;
                model.ResponseMessage = "You must submit a Order Info Product Group Value";
            }

            return RedirectToAction("OrderInfoProductGroups", model);
        }
        public ActionResult EditOrderInfoProductGroup(int ID)
        {
            EditOrderInfoProductGroup_ViewModel model = new EditOrderInfoProductGroup_ViewModel();

            try
            {
                var OrderInfoProductGroup = _typeFunctions.GetByIDOrderInfoProductGroup(ID);
                if (OrderInfoProductGroup.ResponseSuccess)
                {
                    model.ID = OrderInfoProductGroup.GenericClass.ID;
                    model.GroupName = OrderInfoProductGroup.GenericClass.GroupName;
                }
                else
                {
                    OrderInfoProductGroups_ViewModel returnModel = new OrderInfoProductGroups_ViewModel();
                    returnModel.ResponseMessage = "Unable to retrieve Order Info Product Group by ID " + ID;
                    return View("OrderInfoProductGroups", model);
                }
            }
            catch (Exception ex)
            {
                _loggerFunctions.Log(ex.ToString(), _helper.GetIp());
                OrderInfoProductGroups_ViewModel returnModel = new OrderInfoProductGroups_ViewModel();
                returnModel.ResponseMessage = "There was an error while retrieving the Order Info Product Group by ID " + ID;
                returnModel.ResponseSuccess = false;
                return RedirectToAction("OrderInfoProductGroups", model);
            }

            return PartialView("_EditOrderInfoProductGroup", model);
        }
        [HttpPost]
        public ActionResult SaveOrderInfoProductGroupEdit(FormCollection formCollection)
        {
            OrderInfoProductGroups_ViewModel model = new OrderInfoProductGroups_ViewModel();

            if (formCollection != null && !string.IsNullOrEmpty(formCollection["GroupName"]))
            {
                var BTid = Convert.ToInt32(formCollection["ID"]);
                var BTGroupName = formCollection["GroupName"];


                try
                {
                    OrderInfoProductGroups_Model OrderInfoProductGroup_Model = new OrderInfoProductGroups_Model();
                    OrderInfoProductGroup_Model.ID = BTid;
                    OrderInfoProductGroup_Model.GroupName = BTGroupName;

                    var Updated = _typeFunctions.UpdateOrderInfoProductGroup(OrderInfoProductGroup_Model);
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
                    model.ResponseMessage = "There was an error while update the Order Info Product Group " + formCollection["GroupName"];
                    model.responseTypes = Models.ResponseTypes.Failure;
                }
            }
            else
            {
                model.ResponseMessage = "You must submit a Order Info Product Group Value";
                model.responseTypes = Models.ResponseTypes.Failure;
            }
            return RedirectToAction("OrderInfoProductGroups", model);
        }
        [HttpPost]
        public ActionResult DeleteOrderInfoProductGroup(int ID)
        {
            OrderInfoProductGroups_ViewModel model = new OrderInfoProductGroups_ViewModel();

            if (ID > 0)
            {
                try
                {
                    var Deleted = _typeFunctions.DeleteOrderInfoProductGroup(ID);
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
                    model.ResponseMessage = "Unable to delete Order Info Product Group with ID " + ID;
                    model.responseTypes = Models.ResponseTypes.Failure;
                }
            }
            else
            {
                model.ResponseMessage = "You must submit a Order Info Product Group ID to delete";
                model.responseTypes = Models.ResponseTypes.Failure;
            }

            return RedirectToAction("OrderInfoProductGroups", model);
        }
        #endregion

        #region Order Info Product Types
        public ActionResult OrderInfoProductTypes(bool ActiveStatus = true, OrderInfoProductType_ViewModel model = null)
        {
            model.ActiveStatus = ActiveStatus;
            try
            {
                var OrderInfoProductTypes = _typeFunctions.GetAllOrderInfoProductTypes(ActiveStatus);
                if (string.IsNullOrEmpty(model.ResponseMessage))
                {
                    model.ResponseSuccess = OrderInfoProductTypes.ResponseSuccess;
                    model.ResponseMessage = OrderInfoProductTypes.ResponseMessage;
                    switch (OrderInfoProductTypes.responseTypes)
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
                if (OrderInfoProductTypes.GenericClassList != null && OrderInfoProductTypes.GenericClassList.Count > 0)
                {
                    model.ListOrderInfoProductTypes = OrderInfoProductTypes.GenericClassList;
                }
            }
            catch (Exception ex)
            {
                _loggerFunctions.Log(ex.ToString(), _helper.GetIp());
                model.ResponseMessage = "There was an error while retrieving the Order Info Product Types.";
                model.ResponseSuccess = false;
            }

            return View(model);
        }
        public ActionResult CreateOrderInfoProductType()
        {
            return PartialView("_CreateOrderInfoProductType");
        }
        [HttpPost]
        public ActionResult SaveOrderInfoProductType(string OrderInfoProductType, decimal Price)
        {
            OrderInfoProductType_ViewModel model = new OrderInfoProductType_ViewModel();
            model.ActiveStatus = true;
            if (!string.IsNullOrEmpty(OrderInfoProductType))
            {
                try
                {
                    OrderInfoProductTypes_Model OrderInfoProductType_Model = new OrderInfoProductTypes_Model();
                    OrderInfoProductType_Model.Type = OrderInfoProductType;
                    OrderInfoProductType_Model.IsActive = true;
                    OrderInfoProductType_Model.Price = Price;
                    var Saved = _typeFunctions.AddOrderInfoProductType(OrderInfoProductType_Model);
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
                    model.ResponseMessage = "There was an error while saving the Order Info Product Type " + OrderInfoProductType;
                }
            }
            else
            {
                model.responseTypes = Models.ResponseTypes.Failure;
                model.ResponseMessage = "You must submit a Order Info Product Type Value";
            }

            return RedirectToAction("OrderInfoProductTypes", model);
        }
        public ActionResult EditOrderInfoProductType(int ID)
        {
            EditOrderInfoProductType_ViewModel model = new EditOrderInfoProductType_ViewModel();

            try
            {
                var OrderInfoProductType = _typeFunctions.GetOrderInfoProductTypeByID(ID);
                if (OrderInfoProductType.ResponseSuccess)
                {
                    model.ID = OrderInfoProductType.GenericClass.ID;
                    model.OrderInfoProductType = OrderInfoProductType.GenericClass.Type;
                    model.IsActive = OrderInfoProductType.GenericClass.IsActive;
                    model.Price = Math.Round(OrderInfoProductType.GenericClass.Price, 2); 
                }
                else
                {
                    OrderInfoProductType_ViewModel returnModel = new OrderInfoProductType_ViewModel();
                    returnModel.ResponseMessage = "Unable to retrieve Order Info Product Type by ID " + ID;
                    return View("OrderInfoProductTypes", model);
                }
            }
            catch (Exception ex)
            {
                _loggerFunctions.Log(ex.ToString(), _helper.GetIp());
                OrderInfoProductType_ViewModel returnModel = new OrderInfoProductType_ViewModel();
                returnModel.ResponseMessage = "There was an error while retrieving the Order Info Product Type by ID " + ID;
                returnModel.ResponseSuccess = false;
                return RedirectToAction("OrderInfoProductTypes", model);
            }

            return PartialView("_EditOrderInfoProductType", model);
        }
        [HttpPost]
        public ActionResult SaveOrderInfoProductTypeEdit(FormCollection formCollection)
        {
            OrderInfoProductType_ViewModel model = new OrderInfoProductType_ViewModel();
            model.ActiveStatus = true;
            if (formCollection != null && !string.IsNullOrEmpty(formCollection["OrderInfoProductType"]))
            {
                var BTid = Convert.ToInt32(formCollection["ID"]);
                var BTtype = formCollection["OrderInfoProductType"];
                var BTisactive = Convert.ToBoolean(formCollection["IsActive"].Split(',')[0]);
                var BTPrice = Convert.ToDecimal(formCollection["Price"]);

                try
                {
                    OrderInfoProductTypes_Model OrderInfoProductType_Model = new OrderInfoProductTypes_Model();
                    OrderInfoProductType_Model.ID = BTid;
                    OrderInfoProductType_Model.Type = BTtype;
                    OrderInfoProductType_Model.IsActive = BTisactive;
                    OrderInfoProductType_Model.Price = BTPrice;
                    var Updated = _typeFunctions.UpdateOrderInfoProductType(OrderInfoProductType_Model);
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
                    model.ResponseMessage = "There was an error while update the Order Info Product Type " + formCollection["OrderInfoProductType"];
                    model.responseTypes = Models.ResponseTypes.Failure;
                }
            }
            else
            {
                model.ResponseMessage = "You must submit a Order Info Product Type Value";
                model.responseTypes = Models.ResponseTypes.Failure;
            }
            return RedirectToAction("OrderInfoProductTypes", model);
        }
        [HttpPost]
        public ActionResult DeleteOrderInfoProductType(int ID)
        {
            OrderInfoProductType_ViewModel model = new OrderInfoProductType_ViewModel();
            model.ActiveStatus = true;
            if (ID > 0)
            {
                try
                {
                    var Deleted = _typeFunctions.DeleteOrderInfoProductType(ID);
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
                    model.ResponseMessage = "Unable to delete Order Info Product Type with ID " + ID;
                    model.responseTypes = Models.ResponseTypes.Failure;
                }
            }
            else
            {
                model.ResponseMessage = "You must submit a Order Info Product Type ID to delete";
                model.responseTypes = Models.ResponseTypes.Failure;
            }

            return RedirectToAction("OrderInfoProductTypes", model);
        }
        #endregion
        
     


        //if role is web admin
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