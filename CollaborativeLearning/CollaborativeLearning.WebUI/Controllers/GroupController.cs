using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollaborativeLearning.Entities;
using CollaborativeLearning.DataAccess;
using CollaborativeLearning.WebUI.Filters;
namespace CollaborativeLearning.WebUI.Controllers
{
    public class GroupController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /GroupBySemester

        public ActionResult _PartialGetGroupsBySemester(int id)
        {
            var groupList = unitOfWork.GroupRepository.Get(s => s.semesterID==id);
            ViewBag.ID = id;
            return PartialView(groupList);
        }

        public ActionResult _PartialAddGroup(int id)
        {
            ViewBag.ID = id;
            return PartialView();
        }
        public ActionResult AddGroupsToSemester(int Id, string groupName)
        {
            var semester = unitOfWork.SemesterRepository.GetByID(Id);

            Group group = new Group
            {
                groupName=groupName,
                regDate = DateTime.UtcNow,
                regUserID = HelperController.GetCurrentUserId(),
                semesterID = Id
            };
            
            unitOfWork.GroupRepository.Insert(group);

            unitOfWork.Save();
            ViewBag.ID = Id;
            ViewBag.AllGroups = unitOfWork.GroupRepository.Get();
            return RedirectToAction("_PartialGetGroupsBySemester", "Group", new { id = Id });
        }
        public ActionResult Delete(int id)
        {
            int semesterId = unitOfWork.GroupRepository.GetByID(id).semesterID;
            unitOfWork.GroupRepository.Delete(id);
            unitOfWork.Save();
            ViewBag.ID = semesterId;
            return RedirectToAction("_PartialGetGroupsBySemester", "Group", new { id = semesterId });
        }
    }
}
