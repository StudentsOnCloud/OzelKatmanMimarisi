using CustomProject.Entity;
using CustomProject.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomProject.MvcUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            
            List<Categories> kategoriler = CategoriesORM.Current.Select(); ;
            return View(kategoriler);
        }
    }
}