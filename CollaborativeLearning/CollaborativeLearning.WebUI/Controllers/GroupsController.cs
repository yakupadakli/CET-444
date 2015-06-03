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
        public ActionResult Index(int semesterID, int? groupID)
        {
            int userID = HelperController.GetCurrentUserId();
            unitOfWork = new UnitOfWork();

            User user = unitOfWork.UserRepository.GetByID(userID);
            Semester semester = unitOfWork.SemesterRepository.GetByID(semesterID);
            Group group = new Group();
            if (user != null && semester != null && semester.isActive == true)
            {
                ViewData["semester"] = semester;
                ViewBag.SemesterID = semester.Id;
                if (user.Groups.Where(g => g.SemesterID == semesterID).Count() > 0)
                {
                    ViewBag.SelectedGroupID = group.Id;
                    if (groupID == null)
                    {
                        group = user.Groups.Where(g => g.SemesterID == semesterID).FirstOrDefault();
                    }
                    else
                    {
                        group = user.Groups.Where(g => g.SemesterID == semesterID && g.Id == groupID).FirstOrDefault();
                    }
                    
                    return View(group);
                }
                else
                {
                    ViewBag.Error = "True";
                    ViewBag.Message = "You are NOT assigned to any group yet! Please contact your instructor.";
                    ViewBag.SelectedGroupID = 0;
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

        public ActionResult Scenario(int id, int GroupID) {
            return View();
        }
        public ActionResult GetGroupListTable(int SemesterID)
        {
            unitOfWork = new UnitOfWork();
            ICollection<Group> model = unitOfWork.GroupRepository.Get(g => g.SemesterID == SemesterID).ToList();
            return PartialView(model);

        }
        public ActionResult GetScenarioListTable(int SemesterID)
        {
            unitOfWork = new UnitOfWork();
            Semester semester = unitOfWork.SemesterRepository.GetByID(SemesterID);
            List<Scenario> model = new List<Scenario>();
            if (semester != null)
            {
                model = semester.Scenarios.Where(Sc => Sc.isActive == true).ToList();
            }
            return PartialView(model);
        }
        public ActionResult GetCourseResourceListTable(int SemesterID)
        {
            unitOfWork = new UnitOfWork();
            Semester semester = unitOfWork.SemesterRepository.GetByID(SemesterID);
            List<Resource> model = new List<Resource>();
            if (semester != null)
            {
                model = semester.Resources.Where(Sc => Sc.isActive == true).ToList();
            }
            return PartialView(model);
        }
        public ActionResult GetMentorListTable(int SemesterID)
        {
            unitOfWork = new UnitOfWork();
            Semester semester = unitOfWork.SemesterRepository.GetByID(SemesterID);
            List<User> model = new List<User>();
            if (semester != null)
            {
                model = semester.Users.Where(Sc => Sc.RoleID==2).ToList();
            }
            return PartialView(model);
        }

        public ActionResult _PartialGetGroupScenarios(int SemesterID)
        {
            unitOfWork = new UnitOfWork();
            List<Scenario> groupScenarios = new List<Scenario>();
            User user = HelperController.GetCurrentUser();
            Group group = user.Groups.Where(s => s.SemesterID == SemesterID).FirstOrDefault();
            if (group != null)
            {
                groupScenarios = group.Scenarios.Where(s => s.isActive == true).ToList();
            }

            
            ViewBag.SelecteGroupID = group.Id;
            ViewBag.SemesterID = group.SemesterID;
            ViewData["semester"] = group.Semester;
       
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
            ViewBag.SemesterID = Semester.Id;
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
                ViewBag.GroupName = "You are NOT assigned to any group yet!";
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
