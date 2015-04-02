using System.Web.Mvc;

namespace LahoreGarrisonUniversity.Areas.FrontEnd.Controllers
{
    public class DefaultController : Controller
    {
        // GET: FrontEnd/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}