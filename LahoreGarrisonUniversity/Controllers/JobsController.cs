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
    public class JobsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Jobs/
        public async Task<ActionResult> Index()
        {
            return View(await db.Job.ToListAsync());
        }

        // GET: /Jobs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Job.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: /Jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Jobs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Job job)
        {
            if (ModelState.IsValid)
            {
                var photo = Request.Files[0];
                if (photo != null && photo.ContentLength > 0)
                {
                    var allowedFileExtensions = new string[] { ".doc", ".docx" };
                    if (!allowedFileExtensions.Contains(photo.FileName.Substring(photo.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", allowedFileExtensions));
                    }
                    var fileName = Path.GetFileName(photo.FileName);
                    var fullPath = "/Content/applications/" + fileName;
                    photo.SaveAs(Server.MapPath(fullPath));
                    job.ApplicationForm = fullPath;
                }
                job.CreatedAt = DateTime.Now;

                db.Job.Add(job);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(job);
        }

        // GET: /Jobs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Job.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: /Jobs/Edit/5
        /// <summary>
        /// Edits the specified job.
        /// </summary>
        /// <param name="job">The job.</param>
        /// <returns>Task{ActionResult}.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Job job)
        {
            if (ModelState.IsValid)
            {
                var photo = Request.Files[0];
                if (photo != null && photo.ContentLength > 0)
                {
                    var allowedFileExtensions = new string[] {".doc", ".docx" };
                    if (!allowedFileExtensions.Contains(photo.FileName.Substring(photo.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", allowedFileExtensions));
                    }
                    var fileName = Path.GetFileName(photo.FileName);
                    var fullPath = "/Content/applications/" + fileName;
                    photo.SaveAs(Server.MapPath(fullPath));
                    job.ApplicationForm = fullPath;
                }
                job.CreatedAt = DateTime.Now;
                job.Id = id;
                db.Entry(job).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        // GET: /Jobs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Job.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: /Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Job job = await db.Job.FindAsync(id);
            db.Job.Remove(job);
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
    }
}
