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
using Microsoft.AspNet.Identity.EntityFramework;

namespace EquipManagement.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                ViewBag.UserCount = db.Users.Count() - 1;
                ViewBag.EquipmentTypeCount=db.EquipmentTypes.Count();
            }
            return View();
        }
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult EquipSearch(int? EquipmentTypeId, string Name, string OwnerId, string Status)
        {

            var equipments = db.Equipments.Include(e => e.Type).Include(e => e.Owner);
            if (Name != "" && Name != null)
            {
                equipments = equipments.Where(e => e.Name.Contains(Name));
            }
            if (EquipmentTypeId != null)
            {
                equipments = equipments.Where(e => e.Type.Id == EquipmentTypeId);
            }
            if (OwnerId != "" && OwnerId != null)
            {
                equipments = equipments.Where(e => e.Owner.Id == OwnerId);
            }
            if (Status != null)
            {
                equipments = equipments.Where(e => e.Status == !(Status == "on"));
            }
            var teacherRole = db.Roles.Where(r => r.Name == "Teacher").First(); ;
            var owner = from user in db.Users where user.Roles.FirstOrDefault().RoleId == teacherRole.Id select user;


            //var a = from user in db.Users where user.Roles.Contains() select user;            
            ViewBag.OwnerId = new SelectList(owner, "Id", "Name", db.Users.Find(OwnerId));
            ViewBag.EquipmentTypeId = new SelectList(db.EquipmentTypes, "Id", "Name", db.EquipmentTypes.Find(EquipmentTypeId));
            return View(equipments.ToList());

        }
    }
}