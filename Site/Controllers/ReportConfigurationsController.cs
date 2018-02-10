using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;

namespace Site.Controllers
{
    public class ReportConfigurationsController : Controller
    {
        private DataBase db = new DataBase("PdfFindConnection");

        // GET: ReportConfigurations
        public ActionResult Index()
        {
            var reports = db.ReportConfigurations.ToList();
            return View(reports);
        }

        // GET: ReportConfigurations/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportConfiguration reportConfiguration = db.ReportConfigurations.Find(id);
            if (reportConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(reportConfiguration);
        }

        // GET: ReportConfigurations/Create
        public ActionResult Create()
        {
            ViewBag.Groups = db.GroupConfigurations.ToList();
            return View();
        }

        // POST: ReportConfigurations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReportName,PrinterName,Duplex,PaperFormat")] ReportConfiguration reportConfiguration, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                var groupId = Guid.Parse(formCollection["Group"]);
                var group = db.GroupConfigurations.First(g => g.Id == groupId);
                reportConfiguration.Id = Guid.NewGuid();
                reportConfiguration.Group = group;
                group.Reports.Add(reportConfiguration);
                db.ReportConfigurations.Add(reportConfiguration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reportConfiguration);
        }

        // GET: ReportConfigurations/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportConfiguration reportConfiguration = db.ReportConfigurations.Find(id);
            if (reportConfiguration == null)
            {
                return HttpNotFound();
            }
            ViewBag.Groups = db.GroupConfigurations.ToList();
            return View(reportConfiguration);
        }

        // POST: ReportConfigurations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ReportName,PrinterName,Duplex,PaperFormat")] ReportConfiguration reportConfiguration, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                var groupId = Guid.Parse(formCollection["Group"]);
                var group = db.GroupConfigurations.First(g => g.Id == groupId);
                reportConfiguration.Group = group;
                db.Entry(reportConfiguration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reportConfiguration);
        }

        // GET: ReportConfigurations/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportConfiguration reportConfiguration = db.ReportConfigurations.Find(id);
            if (reportConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(reportConfiguration);
        }

        // POST: ReportConfigurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ReportConfiguration reportConfiguration = db.ReportConfigurations.Find(id);
            db.ReportConfigurations.Remove(reportConfiguration);
            db.SaveChanges();
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
