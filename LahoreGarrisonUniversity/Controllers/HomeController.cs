using System.Web.Mvc;
using System.Web.Routing;

namespace LahoreGarrisonUniversity.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult FrontEnd()
        {
            return RedirectToAction("Index", "Home", new { area = "FrontEnd" });
        }
    }
}
