﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollaborativeLearning.DataAccess;
using CollaborativeLearning.Entities;
using CollaborativeLearning.WebUI.Filters;

namespace CollaborativeLearning.WebUI.Controllers
{

    public class HomeController : Controller
    {

        private UnitOfWork unitOfWork = new UnitOfWork();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
