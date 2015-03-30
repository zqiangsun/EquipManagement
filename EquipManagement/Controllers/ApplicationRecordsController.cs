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
    [Authorize(Roles="Teacher")]
    public class ApplicationRecordsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public bool Authorize(int? id)
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).First();
            var equipment = db.Equipments.Find(id);
            if (equipment.Owner.Id != user.Id)
            {
                return false;
            }
            return true;
        }

        // GET: ApplicationRecords

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!Authorize(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            var applicationRecords = db.ApplicationRecords.Where(a => a.EquipmentId == id).Include(a => a.Equipment);
            return View(applicationRecords.ToList());
        }

        // GET: ApplicationRecords/Details/5
        [AllowAnonymous]
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

        // GET: ApplicationRecords/Create/5
        [AllowAnonymous]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.EquipmentId = new SelectList(db.Equipments.Where(e => e.Id == id), "Id", "Name");
            return View();
        }

        // POST: ApplicationRecords/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "Id,EquipmentId,Applicant,Contact,StudentInfo,ApplicationDate")] ApplicationRecord applicationRecord)
        {
            if (ModelState.IsValid)
            {
                applicationRecord.ReturnDate = applicationRecord.ApplicationDate;
                applicationRecord.IsApprove = false;
                db.ApplicationRecords.Add(applicationRecord);
                db.SaveChanges();
                return RedirectToAction("Details", new { id=applicationRecord.Id});
            }

            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name", applicationRecord.EquipmentId);
            return View(applicationRecord);
        }        
        public ActionResult Approve(int? id)
        {
            return View();
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
