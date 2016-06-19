using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Areas.Panel.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Panel/Profile
        public ActionResult Index()
        {
            return View();
        }
    }
}