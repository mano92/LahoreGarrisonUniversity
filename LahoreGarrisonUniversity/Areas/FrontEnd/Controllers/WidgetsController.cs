﻿using System.Web.Mvc;

namespace LahoreGarrisonUniversity.Areas.FrontEnd.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: FrontEnd/Widgets
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DownloadProspectus()
        {
            return File("~/Content/documents/Route.pdf", "application/pdf", "Prospectus.pdf");
        }

        public ActionResult DownloadRoute()
        {
            return File("~/Content/documents/Route.pdf", "application/pdf", "Route.pdf");
        }

        public ActionResult DownloadImpNotice()
        {
            return File("~/Content/documents/Imp-notice.pdf", "application/pdf", "Imp-notice.pdf");
        }

        public ActionResult DownloadFeeNotice()
        {
            return File("~/Content/documents/Fee-notice.pdf", "application/pdf", "Fee-notice.pdf");
        }

        public ActionResult DownloadFeeSchedule()
        {
            return File("~/Content/documents/Fee-Schedule-1-1.pdf", "application/pdf", "Fee-Schedule-1-1.pdf");
        }

        public ActionResult DownloadScholarship()
        {
            return File("~/Content/documents/Scholarship.pdf", "application/pdf", "Scholarship.pdf");
        }
        public ActionResult DownloadScholarshipDescription()
        {
            return File("~/Content/documents/Scholarship-Description.pdf", "application/pdf", "Scholarship-Description.pdf");
        }
    }
}