using BusinessLayer.Functions.Shared;
using SimpleCure.Models.HomeModels;
using System.Web.Mvc;

namespace SimpleCure.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Injection
        
        private SharedFunctions _sharedFunctions;

        public HomeController()
        {
            _sharedFunctions = new SharedFunctions();
        }
        #endregion

        public ActionResult Index()
        {
            var Numbers = _sharedFunctions.GetIndexNumbers();
            return View(new Index_ViewModels { CompletedOrders = Numbers.GenericClass.CompletedOrders, NewOrders = Numbers.GenericClass.NewOrders, PendingOrders = Numbers.GenericClass.PendingOrders });
        } 
    }
}