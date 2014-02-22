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
        public ActionResult Index(int id = 0)
        {  
            int groupID=id;
            if (id == 0 ){
                 groupID = HelperController.GetCurrentUser().Groups.FirstOrDefault().Id;
            }   
            unitOfWork = new UnitOfWork();
            Group group = new Group();
            group = unitOfWork.GroupRepository.GetByID(groupID);
           
            if (group == null)
            {   ViewBag.Error = "True";
                ViewBag.Message = "You don't assigned any group yet! Please contact your instructor.";
                ViewBag.SelecteGroupID = 0;
                 return View();
            }
            else 
            {
                if (group.Id == HelperController.GetCurrentUser().Groups.FirstOrDefault().Id)
                {
                    ViewBag.SelecteGroupID = group.Id;
                    return View(group);

                }
                else {
                    return RedirectToAction("Show", group);
                }
            }
        }
        public ActionResult Show(Group group)
        {

            unitOfWork = new UnitOfWork();
            if (group != null)
            {
                ViewBag.SelecteGroupID = group.Id;
                return View(group);
            }
            else {
                return RedirectToAction("index", new { id = 0 });
            }

        }

        public ActionResult GetGroupListTable(int SemesterID)
        {
            ICollection<Group> model = unitOfWork.GroupRepository.Get(g => g.SemesterID == SemesterID).ToList();
            return PartialView(model);

        }
        public ActionResult _PartialGetGroupScenarios()
        {
            unitOfWork = new UnitOfWork();
            List<Scenario> groupScenarios = HelperController.GetCurrentUser().Groups.FirstOrDefault().Scenarios.ToList();
            return PartialView(groupScenarios);
        }
        public ActionResult _PartialGetOtherGroupsForMenu()
        {
            unitOfWork = new UnitOfWork();
            Semester Semester = HelperController.GetUserActiveSemester(HelperController.GetCurrentUserId());
            List<Group> groupLists = Semester.Groups.ToList();
            User CurrentUser = HelperController.GetCurrentUser();
            List<Group> groups = new List<Group>();
            if (CurrentUser.Groups.Count > 0)
            {

                foreach (var item in groupLists)
                {
                    if (item.Id != CurrentUser.Groups.FirstOrDefault().Id)
                    {
                        groups.Add(item);
                    }
                }
            }
            else
            {
                groups = groupLists;
            }

            return PartialView(groups);

        }
        public ActionResult _PartialGetScenariosForMenu()
        {
            unitOfWork = new UnitOfWork();
            Semester Semester = HelperController.GetUserActiveSemester(HelperController.GetCurrentUserId());
            ICollection<Scenario> scenarios = Semester.Scenarios.ToList();
            return PartialView(scenarios);

        }
        public ActionResult _PartialHeaderSemeterInfo()
        {
            unitOfWork = new UnitOfWork();
            User user = HelperController.GetCurrentUser();
            ViewBag.CurrentSemester = HelperController.GetUserActiveSemester(user.Id).semesterName;
            if (user.Groups.Count > 0)
            {
                ViewBag.GroupName = user.Groups.FirstOrDefault().GroupName;
            }
            else
            {
                ViewBag.GroupName = "You don't assigned any group yet!";
            }
            return PartialView();
        }
        public ActionResult _ChangeGroupName(Group model) {
            unitOfWork = new UnitOfWork();
            if (model.Id == HelperController.GetCurrentUser().Groups.FirstOrDefault().Id)
            {
                Group grouoItem = unitOfWork.GroupRepository.GetByID(model.Id);
                grouoItem.GroupName = model.GroupName;
                unitOfWork.Save();
            }
            return JavaScript("OnSuccess()");
        }
    }
}
