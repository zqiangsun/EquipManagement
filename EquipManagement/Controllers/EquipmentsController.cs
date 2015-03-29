using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EquipManagement.Models;
using System.IO;

namespace EquipManagement.Controllers
{
    [Authorize(Roles="Teacher")]
    public class EquipmentsController : Controller
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
        public FileContentResult GetImage(int id)
        {
            var equipment = db.Equipments.Find(id);
            if (equipment != null&&equipment.Image!=null)
            {
                return File(equipment.Image,"image/png");
            }
            else
            {
                return null;
            }
        }
        // GET: Equipments
        public ActionResult Index()
        {
            var user=db.Users.Where(u => u.UserName == User.Identity.Name).First();
            var equipments = db.Equipments.Where(e => e.OwnerId == user.Id).Include(e => e.Type);
            return View(equipments.ToList());
        }

        // GET: Equipments/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
            var n = equipment.Records.Count();
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // GET: Equipments/Create
        public ActionResult Create()
        {
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Name");
            ViewBag.TypeId = new SelectList(db.EquipmentTypes, "Id", "Name");
            return View();
        }

        // POST: Equipments/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,TypeId,PurchaseDate,OwnerId")] Equipment equipment,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                equipment.Status = EquipmentStatus.Usable;

                var user = db.Users.Where(u => u.UserName == User.Identity.Name).First();
                equipment.Owner = user;
                MemoryStream stream = new MemoryStream();
                Image.InputStream.CopyTo(stream);
                equipment.Image = stream.ToArray();
                db.Equipments.Add(equipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Name", equipment.OwnerId);
            ViewBag.TypeId = new SelectList(db.EquipmentTypes, "Id", "Name", equipment.TypeId);
            return View(equipment);
        }

        // GET: Equipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
             
            if (equipment == null)
            {
                return HttpNotFound();
            }
            else if(!Authorize(id)){
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Name", equipment.OwnerId);
            ViewBag.TypeId = new SelectList(db.EquipmentTypes, "Id", "Name", equipment.TypeId);
            return View(equipment);
        }

        // POST: Equipments/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,TypeId,PurchaseDate")] Equipment equipment,HttpPostedFileBase Image)
        {
           
            if (!Authorize(equipment.Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            else if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    MemoryStream stream = new MemoryStream();
                    Image.InputStream.CopyTo(stream);
                    equipment.Image = stream.ToArray();
                }
                db.Entry(equipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Name", equipment.OwnerId);
            ViewBag.TypeId = new SelectList(db.EquipmentTypes, "Id", "Name", equipment.TypeId);
            return View(equipment);
        }

        // GET: Equipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }else if(!Authorize(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Authorize(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            Equipment equipment = db.Equipments.Find(id);
            db.Equipments.Remove(equipment);
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
