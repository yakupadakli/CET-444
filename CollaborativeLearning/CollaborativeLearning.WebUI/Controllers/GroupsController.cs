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
        public ActionResult Index(int semesterID)
        {
            int userID = HelperController.GetCurrentUserId();
            unitOfWork = new UnitOfWork();

            User user = unitOfWork.UserRepository.GetByID(userID);
            Semester semester = unitOfWork.SemesterRepository.GetByID(semesterID);
            StudentCourseRequest scr = unitOfWork.StudentCourseRequestRepository.Get(s => s.UserId == user.Id && s.SemesterId == semesterID).FirstOrDefault();
            Group group = new Group();
            if (user != null && semester != null && semester.isActive == true & scr.isApproved == true)
            {
                if (user.Groups.Count() > 0)
                {
                    group = user.Groups.FirstOrDefault();
                    ViewBag.SelecteGroupID = group.Id;
                    ViewBag.SemesterID = semester.Id;
                    ViewData["semester"] = semester;
                    return View(group);
                }
                else
                {
                    group = user.Groups.Where(g => g.SemesterID == semester.Id).FirstOrDefault();
                    ViewBag.Error = "True";
                    ViewBag.Message = "You don't assigned any group yet! Please contact your instructor.";
                    ViewBag.SelecteGroupID = 0;
                    return View();
                }
            }
            else {
                return RedirectToAction("Index", "User");
            }
                                           
           
        }
        public ActionResult Show(int GroupId)
        {

            unitOfWork = new UnitOfWork();
            User user = HelperController.GetCurrentUser();
            Group group = unitOfWork.GroupRepository.GetByID(GroupId);
            if (group != null)
            {
                ViewBag.SelecteGroupID = group.Id;
                ViewBag.SemesterID = group.SemesterID;
                ViewData["semester"] = group.Semester;
                return View(group);
            }
            else {

                return RedirectToAction("index","Home");
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
            List<Scenario> groupScenarios = new List<Scenario>();
            User user = HelperController.GetCurrentUser();
            if (user.Groups.FirstOrDefault() !=null)
            {
                if (user.Groups.FirstOrDefault().Scenarios!=null)
                {
                    groupScenarios = HelperController.GetCurrentUser().Groups.FirstOrDefault().Scenarios.OrderByDescending(s=>s.RegDate).ToList();
                }

            }
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
