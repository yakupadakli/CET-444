using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollaborativeLearning.Entities;
using CollaborativeLearning.DataAccess;
namespace CollaborativeLearning.WebUI.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index(Group group)
        {

            unitOfWork = new UnitOfWork();

            if (group == null)
            {

                ViewBag.Error = "True";
                ViewBag.Message = "You don't assigned any group yet! Please contact your instructor.";
                return View();
            }
            return View(group);
        }

       


        ///
        public ActionResult GetGroupListTable(int SemesterID) { 
            unitOfWork = new UnitOfWork();
            ICollection<Group> model = unitOfWork.GroupRepository.Get(g => g.SemesterID == SemesterID).ToList();
            return PartialView(model);
        }
    }
}
