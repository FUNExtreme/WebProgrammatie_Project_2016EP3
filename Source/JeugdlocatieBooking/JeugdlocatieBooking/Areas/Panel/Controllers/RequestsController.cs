﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Areas.Panel.Controllers
{
    public class RequestsController : Controller
    {
        // GET: Panel/Requests
        public ActionResult Index()
        {
            return View();
        }
    }
}