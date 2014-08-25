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
        public ActionResult Index(string s)
        {
            if (HelperController.GetCurrentUser().RoleID == 1)
            {
                if (s==null)
                {
                    return View();
                }
                else
                {
                    if (s == "Courses")
                    {
                        return View("CoursePool");
                    }else
                    if (s== "Scenarios")
                    {
                        return View("ScenarioPool");
                    }
                    else
                    if (s=="Resources")
                    {
                        return View("ResourcePool");
                    }
                    else
                    if (s=="Mentors")
                    {
                        return View("MentorPool");

                    }
                    else
                    {
                        return View();
                    }
                }
                
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
