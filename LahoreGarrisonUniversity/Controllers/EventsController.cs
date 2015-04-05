﻿using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using LahoreGarrisonUniversity.Models;

namespace LahoreGarrisonUniversity.Controllers
{
    public class EventsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Events/
        public async Task<ActionResult> Index()
        {
            return View(await db.Event.ToListAsync());
        }

        // GET: /Events/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = await db.Event.FindAsync(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: /Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Event @event)
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
                    var fullPath = "~/Content/FrontEnd/images/events/" + fileName;
                    photo.SaveAs(Server.MapPath(fullPath));
                    @event.MediaUrl = fullPath.Remove(0, 1);
                }
                @event.CreatedAt = DateTime.Now;
                @event.UserName = User.Identity.Name;
                db.Event.Add(@event);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: /Events/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = await db.Event.FindAsync(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: /Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Event @event)
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
                    var fullPath = "~/Content/FrontEnd/images/events/" + fileName;
                    photo.SaveAs(Server.MapPath(fullPath));
                    @event.MediaUrl = fullPath.Remove(0, 1);
                }
                @event.CreatedAt = DateTime.Now;
                @event.UserName = User.Identity.Name;
                db.Entry(@event).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: /Events/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = await db.Event.FindAsync(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: /Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Event @event = await db.Event.FindAsync(id);
            db.Event.Remove(@event);
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
