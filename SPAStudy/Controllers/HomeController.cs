using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SPAStudy.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "SPAStudy";
            return View();
        }
    }
}
