using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollaborativeLearning.DataAccess;

namespace CollaborativeLearning.WebUI.Controllers
{
    
    public class HomeController : Controller
    {

        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
