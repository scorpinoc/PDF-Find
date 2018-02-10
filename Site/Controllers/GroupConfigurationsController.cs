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
    public class GroupConfigurationsController : Controller
    {
        private DataBase db = new DataBase("PdfFindConnection");

        // GET: GroupConfigurations
        public ActionResult Index()
        {
            var groups = db.GroupConfigurations.ToList();
            //var gr = groups.First().Reports.ToList();
            return View(groups);
        }

        // GET: GroupConfigurations/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupConfiguration groupConfiguration = db.GroupConfigurations.Find(id);
            if (groupConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(groupConfiguration);
        }

        // GET: GroupConfigurations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GroupConfigurations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GroupName,PrinterName,Duplex,PaperFormat")] GroupConfiguration groupConfiguration)
        {
            if (ModelState.IsValid)
            {
                
                db.GroupConfigurations.Add(groupConfiguration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(groupConfiguration);
        }

        // GET: GroupConfigurations/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupConfiguration groupConfiguration = db.GroupConfigurations.Find(id);
            if (groupConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(groupConfiguration);
        }

        // POST: GroupConfigurations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GroupName,PrinterName,Duplex,PaperFormat")] GroupConfiguration groupConfiguration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupConfiguration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groupConfiguration);
        }

        // GET: GroupConfigurations/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupConfiguration groupConfiguration = db.GroupConfigurations.Find(id);
            if (groupConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(groupConfiguration);
        }

        // POST: GroupConfigurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            GroupConfiguration groupConfiguration = db.GroupConfigurations.Find(id);
            db.GroupConfigurations.Remove(groupConfiguration);
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
