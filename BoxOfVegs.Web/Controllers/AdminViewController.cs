using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoxOfVegs.Web.Controllers
{
    public class AdminViewController : Controller
    {
        // GET: AdminView
        public ActionResult Index()
        {
            return View();
        }
    }
}