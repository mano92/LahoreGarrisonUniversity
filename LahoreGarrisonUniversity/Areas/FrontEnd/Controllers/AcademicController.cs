using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LahoreGarrisonUniversity.Models;

namespace LahoreGarrisonUniversity.Areas.FrontEnd.Controllers
{
    public class AcademicController : Controller
    {
        //
        // GET: /Academic/
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UnderGraduate()
        {
            //var courses = new object();
            return View();
        }

        public ActionResult PostGraduate()
        {
            return View();
        }
        
        public ActionResult AcademicCalendar()
        {
            return View();
        }

        public async Task<ActionResult> Courses()
        {
            return View(await db.FrontEndCourses.ToListAsync());
        }

    }
}
