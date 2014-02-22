using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CollaborativeLearning.DataAccess;
using CollaborativeLearning.WebUI.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using CollaborativeLearning.WebUI.Filters;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.WebUI.Controllers
{

    [InitializeSimpleMembership]
    public class HomeController : Controller
    {

        private UnitOfWork unitOfWork = new UnitOfWork();

        [Authorize(Roles=("Instructor")]
        public ActionResult Index()
        {
            return View();
        }
        

        
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
