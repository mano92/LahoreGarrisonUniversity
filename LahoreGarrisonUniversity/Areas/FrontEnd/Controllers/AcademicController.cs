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

        public async Task<ActionResult> UnderGraduate()
        {
            return View(await db.FrontEndCourses.Where(x => x.StudentLevel == "Undergraduate").ToListAsync());
        }

        public async Task<ActionResult> PostGraduate()
        {
            return View(await db.FrontEndCourses.Where(x => x.StudentLevel == "Postgraduate").ToListAsync());
        }
        
        public ActionResult AcademicCalendar()
        {
            return View();
        }

        public async Task<ActionResult> Courses()
        {
            return View(await db.FrontEndCourses.ToListAsync());
        }

        public ActionResult CourseDetails(int id)
        {
            return View(db.FrontEndCourses.Find(id));
        }
    }
}
