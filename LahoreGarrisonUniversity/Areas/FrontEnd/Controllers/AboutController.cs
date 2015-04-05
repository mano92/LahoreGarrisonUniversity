using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using LahoreGarrisonUniversity.Models;

namespace LahoreGarrisonUniversity.Areas.FrontEnd.Controllers
{
    public class AboutController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /About/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LGULeadership()
        {
            return View();
        }

        public ActionResult VisionAndMission()
        {
            return View();
        }

        public ActionResult HonorsAndAwards()
        {
            return View();
        }

        public ActionResult LGUCompuses()
        {
            return View();
        }

        public ActionResult LGUAtGlance()
        {
            return View();
        }

        public ActionResult Jobs()
        {
            return View();
        }

        public async Task<ActionResult> JobDetails(int id)
        {
            var item = await db.Job.FindAsync(id);
            return View(item);
        }

    }
}
