using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EquipManagement.Models;

namespace EquipManagement.Controllers
{   
    public class ApplicationRecordsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationRecords
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var applicationRecords = db.ApplicationRecords.Where(a=>a.EquipmentId==id).Include(a => a.Equipment);
            return View(applicationRecords.ToList());
        }

        // GET: ApplicationRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRecord applicationRecord = db.ApplicationRecords.Find(id);
            if (applicationRecord == null)
            {
                return HttpNotFound();
            }
            return View(applicationRecord);
        }

        // GET: ApplicationRecords/Create
        public ActionResult Create()
        {
            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name");
            return View();
        }

        // POST: ApplicationRecords/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EquipmentId,ApplicantName,Contact,StudentInfo,ApplicationDate,ReturnDate,IsApprove,Conclusion")] ApplicationRecord applicationRecord)
        {
            if (ModelState.IsValid)
            {
                db.ApplicationRecords.Add(applicationRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name", applicationRecord.EquipmentId);
            return View(applicationRecord);
        }

        // GET: ApplicationRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRecord applicationRecord = db.ApplicationRecords.Find(id);
            if (applicationRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name", applicationRecord.EquipmentId);
            return View(applicationRecord);
        }

        // POST: ApplicationRecords/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EquipmentId,ApplicantName,Contact,StudentInfo,ApplicationDate,ReturnDate,IsApprove,Conclusion")] ApplicationRecord applicationRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name", applicationRecord.EquipmentId);
            return View(applicationRecord);
        }

        // GET: ApplicationRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRecord applicationRecord = db.ApplicationRecords.Find(id);
            if (applicationRecord == null)
            {
                return HttpNotFound();
            }
            return View(applicationRecord);
        }

        // POST: ApplicationRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplicationRecord applicationRecord = db.ApplicationRecords.Find(id);
            db.ApplicationRecords.Remove(applicationRecord);
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
