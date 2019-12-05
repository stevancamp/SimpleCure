using AutoMapper;
using BusinessLayer.Functions.Customer;
using BusinessLayer.Functions.Email;
using BusinessLayer.Functions.ErrorLogging;
using BusinessLayer.Functions.SCSupply;
using SimpleCure.AutoMapper;
using SimpleCure.Models;
using SimpleCure.Models.SCSupplyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleCure.Controllers
{
    public class SCSupplyController : Controller
    {
        #region Injection
        private IMapper _IMapper;
        private LoggerFunctions _loggerFunctions;
        private EmailFunctions _emailFunctions;
        private SCSupplyFunctions _scsupplyFunctions;
        private CustomerFunctions _customerFunctions;

        public SCSupplyController()
        {
            _IMapper = AutoMapperConfiguration.GetMapper();
            _loggerFunctions = new LoggerFunctions();
            _emailFunctions = new EmailFunctions();
            _scsupplyFunctions = new SCSupplyFunctions();
            _customerFunctions = new CustomerFunctions();
        }
        #endregion
        //view all from SC Supply
        public ActionResult Index(DateTime? StartDate, DateTime? EndDate)
        {
            Generic<Index_ViewModel> model = new Generic<Index_ViewModel>();
            if (StartDate != null && EndDate != null)
            {
                var RangeData = _scsupplyFunctions.GetAllByRange(StartDate ?? TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")).AddDays(-31), EndDate ?? DateTime.Now);

                if (RangeData.ResponseSuccess && RangeData.GenericClassList != null && RangeData.GenericClassList.Count > 0)
                {
                    foreach (var item in RangeData.GenericClassList)
                    {
                        model.GenericClassList.Add(new Index_ViewModel { Model = item });
                    }
                }
            }
            else
            {
                var allData = _scsupplyFunctions.GetAll();
                if (allData.ResponseSuccess && allData.GenericClassList != null && allData.GenericClassList.Count > 0)
                {
                    foreach (var item in allData.GenericClassList)
                    {
                        model.GenericClassList.Add(new Index_ViewModel { Model = item });
                    }
                }
            }
            return View(model);
        }

        //view/edit a single SC Supply by SC Supply ID
        public ActionResult ViewSCSupply(int ID)
        {
            return View(_scsupplyFunctions.GetByID(ID));
        }

        //Create new SC Supply View      
        public ActionResult CreateSCSupply()
        {
            return View(new CreateSCSupply_ViewModel());
        }

        //Create SC Supply HttpPost
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(CreateSCSupply_ViewModel model)
        //{

        //}

        ////Edit SC Supply HttpPost
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit()
        //{

        //}

        ////Delete SC Supply HttpPost
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete()
        //{

        //}
    }
}