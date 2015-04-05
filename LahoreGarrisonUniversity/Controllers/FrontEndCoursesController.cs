using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using LahoreGarrisonUniversity.Models;

namespace LahoreGarrisonUniversity.Controllers
{
    public class FrontEndCoursesController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FrontEndCourses
        public async Task<ActionResult> Index()
        {
            return View(await db.FrontEndCourses.ToListAsync());
        }

        // GET: FrontEndCourses/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrontEndCourse frontEndCourse = await db.FrontEndCourses.FindAsync(id);
            if (frontEndCourse == null)
            {
                return HttpNotFound();
            }
            return View(frontEndCourse);
        }

        // GET: FrontEndCourses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FrontEndCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Flag,Semester,Duration,CreditHourTheory,CreditHourPractical,Level,StudentLevel,Description,CourseStructure,ImageUrl,RecommendedBooks,PrerequsiteCourse")] FrontEndCourse frontEndCourse)
        {
            if (ModelState.IsValid)
            {
                var photo = Request.Files[0];
                if (photo != null && photo.ContentLength > 0)
                {
                    var allowedFileExtensions = new string[] { ".jpg", ".gif", ".png" };
                    if (!allowedFileExtensions.Contains(photo.FileName.Substring(photo.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", allowedFileExtensions));
                    }
                    var fileName = Path.GetFileName(photo.FileName);
                    var fullPath = "~/Content/FrontEnd/images/news/" + fileName;
                    photo.SaveAs(Server.MapPath(fullPath));
                    frontEndCourse.ImageUrl = fullPath.Remove(0, 1);
                }
                frontEndCourse.Id = Guid.NewGuid();
                db.FrontEndCourses.Add(frontEndCourse);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(frontEndCourse);
        }

        // GET: FrontEndCourses/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrontEndCourse frontEndCourse = await db.FrontEndCourses.FindAsync(id);
            if (frontEndCourse == null)
            {
                return HttpNotFound();
            }
            return View(frontEndCourse);
        }

        // POST: FrontEndCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="frontEndCourse">The front end course.</param>
        /// <returns>Task{ActionResult}.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, [Bind(Include = "Id,Title,Flag,Semester,Duration,CreditHourTheory,CreditHourPractical,Level,StudentLevel,Description,CourseStructure,ImageUrl,RecommendedBooks,PrerequsiteCourse")] FrontEndCourse frontEndCourse)
        {
            if (ModelState.IsValid)
            {
                var photo = Request.Files[0];
                if (photo != null && photo.ContentLength > 0)
                {
                    var allowedFileExtensions = new string[] { ".jpg", ".gif", ".png" };
                    if (!allowedFileExtensions.Contains(photo.FileName.Substring(photo.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", allowedFileExtensions));
                    }
                    var fileName = Path.GetFileName(photo.FileName);
                    var fullPath = "~/Content/FrontEnd/images/news/" + fileName;
                    photo.SaveAs(Server.MapPath(fullPath));
                    frontEndCourse.ImageUrl = fullPath.Remove(0, 1);
                }
                frontEndCourse.Id = id;
                db.Entry(frontEndCourse).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(frontEndCourse);
        }

        // GET: FrontEndCourses/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrontEndCourse frontEndCourse = await db.FrontEndCourses.FindAsync(id);
            if (frontEndCourse == null)
            {
                return HttpNotFound();
            }
            return View(frontEndCourse);
        }

        // POST: FrontEndCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            FrontEndCourse frontEndCourse = await db.FrontEndCourses.FindAsync(id);
            db.FrontEndCourses.Remove(frontEndCourse);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Ajax Calls

        [HttpPost]
        public JsonResult GetCourses(string url)
        {
            var courses = db.FrontEndCourses.All(x => x.Level == "Undergraduate");
            return Json(new { courses }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
