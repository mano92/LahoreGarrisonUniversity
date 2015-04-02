using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using LahoreGarrisonUniversity.Models;
using Microsoft.AspNet.Identity;

namespace LahoreGarrisonUniversity.Controllers
{
    public class TestimonialController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Testimonial/
        public async Task<ActionResult> Index()
        {
            if (User.IsInRole("Student") || User.IsInRole("Teacher"))
            {
                var user = User.Identity.GetUserName();
                var testimonial = db.Testimonial.Where(x => x.Name == user).OrderByDescending(d => d.CreatedDate);
                return View(testimonial.ToList());
            }
            else
            {
                return View(await db.Testimonial.ToListAsync());
            }
        }

        // GET: /Testimonial/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial testimonial = await db.Testimonial.FindAsync(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // GET: /Testimonial/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /Testimonial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Review,MediaUrl")] Testimonial testimonial)
        {
            var userId = User.Identity.GetUserId();
            var currentUserId = db.Users.FirstOrDefault(x => x.UserName == userId);
            testimonial.CreatedDate = DateTime.Now;
            if (currentUserId != null)
                testimonial.Name = currentUserId.FirstName + " " + currentUserId.MiddleName;
            testimonial.IsApproved = 0;
            if (testimonial.MediaUrl == null)
            {
                testimonial.MediaUrl = "No Data Image Provided";
            }
            if (ModelState.IsValid)
            {
                db.Testimonial.Add(testimonial);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(testimonial);
        }

        // GET: /Testimonial/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial testimonial = await db.Testimonial.FindAsync(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // POST: /Testimonial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Review,Name,CreatedDate,IsApproved,MediaUrl")] Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testimonial).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(testimonial);
        }

        // GET: /Testimonial/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial testimonial = await db.Testimonial.FindAsync(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // POST: /Testimonial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Testimonial testimonial = await db.Testimonial.FindAsync(id);
            db.Testimonial.Remove(testimonial);
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
