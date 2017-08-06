using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPAStudy.Controllers
{
    public class StockTickerController : Controller
    {
        // GET: StockTicker
        public ActionResult Index()
        {
            ViewBag.Title = "StockTicker";
            return View();
        }
    }
}