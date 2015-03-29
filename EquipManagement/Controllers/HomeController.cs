using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using EquipManagement.Models;
namespace EquipManagement.Controllers
{
    public class HomeController : Controller
    {
    
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EquipSearch(EquipmentType Type,string Name,string UserId) { 
            return View();

        }
    }
}