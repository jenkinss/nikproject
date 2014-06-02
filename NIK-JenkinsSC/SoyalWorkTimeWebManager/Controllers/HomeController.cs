using System.Web.Mvc;

namespace SoyalWorkTimeWebManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Error = "Fejlesztés alatt.. :)";

            return View();
        }

        public ActionResult Admin()
        {
            ViewBag.Message = "Administration here...";

            return View();
        }
       
        
    }
}
