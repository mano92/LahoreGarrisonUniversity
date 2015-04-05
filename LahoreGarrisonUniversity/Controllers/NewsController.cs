using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LahoreGarrisonUniversity.Models;

namespace LahoreGarrisonUniversity.Controllers
{
    public class NewsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /News/
        public async Task<ActionResult> Index()
        {
            return View(await db.News.ToListAsync());
        }

        // GET: /News/Details/5
        public async Task<ActionResult> Details(int? id)
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

        // GET: /News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(News news)
        {
            if (ModelState.IsValid)
            {
                var photo = Request.Files[0];
                if (photo != null && photo.ContentLength > 0)
                {
                    var allowedFileExtensions = new string[] { ".jpg", ".gif", ".png"};
                    if (!allowedFileExtensions.Contains(photo.FileName.Substring(photo.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", allowedFileExtensions));
                    }
                    var fileName = Path.GetFileName(photo.FileName);
                    var fullPath = "/Content/news/" + fileName;
                    photo.SaveAs(Server.MapPath(fullPath));
                    news.MediaUrl = fullPath;
                }
                news.CreatedAt = DateTime.Now;
                news.UserName = User.Identity.Name;
                db.News.Add(news);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: /News/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

        // POST: /News/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, News news)
        {
            if (ModelState.IsValid)
            {
                var photo = Request.Files[0];
                if (photo != null && photo.ContentLength > 0)
                {
                    var allowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };
                    if (!allowedFileExtensions.Contains(photo.FileName.Substring(photo.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", allowedFileExtensions));
                    }
                    var fileName = Path.GetFileName(photo.FileName);
                    var fullPath = "/Content/FrontEnd/images/news/" + fileName;
                    photo.SaveAs(Server.MapPath(fullPath));
                    news.MediaUrl = fullPath;
                }
                news.CreatedAt = DateTime.Now;
                news.UserName = User.Identity.Name;
                news.Id = id;
                db.Entry(news).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: /News/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: /News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            News news = await db.News.FindAsync(id);
            db.News.Remove(news);
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
