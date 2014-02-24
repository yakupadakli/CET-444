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

        [Authorize]
        public ActionResult Index()
        {
            if (HelperController.GetCurrentUser().RoleID == 1)
            {
                return View();
            }
            else if (HelperController.GetCurrentUser().RoleID == 2)
            {
                return RedirectToAction("Index", "Mentor");
            }
            else if (HelperController.GetCurrentUser().RoleID == 3)
            {
                return RedirectToAction("Index", "User");
            }
            else {
                return RedirectToAction("Login", "Account");
            }
            
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
