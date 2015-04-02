using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using LahoreGarrisonUniversity.Areas.FrontEnd.Models;
using LahoreGarrisonUniversity.Models;

namespace LahoreGarrisonUniversity.Areas.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //
        // GET: /Home/
        public ActionResult Index()
        {
            var viewModel = new HomePageViewModel();
            //viewModel.Events = db.Event.Take(4).OrderByDescending(item => item.StartDate);
            //viewModel.News = db.News.Take(6).OrderByDescending(item => item.CreatedAt);
            //viewModel.Testimonial =
            //    db.Testimonial.Take(4).Where(item => item.IsApproved == 1).OrderByDescending(item => item.CreatedDate);
            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetEvents()
        {
            var items = new List<Event>();
            items = db.Event.Take(4).OrderByDescending(item => item.StartDate).ToList();
            return Json( items, JsonRequestBehavior.AllowGet );
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetNews()
        {
            var items = new List<News>();
            items = db.News.Take(6).OrderByDescending(item => item.CreatedAt).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetTestimonial()
        {
            var items = new List<Testimonial>();
            items = db.Testimonial.Take(4).Where(item => item.IsApproved == 1).OrderByDescending(item => item.CreatedDate).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public async Task<ActionResult> NewsDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }
    }
}
