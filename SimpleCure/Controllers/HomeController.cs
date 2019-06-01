using System.Web.Mvc;

namespace SimpleCure.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        } 
    }
}