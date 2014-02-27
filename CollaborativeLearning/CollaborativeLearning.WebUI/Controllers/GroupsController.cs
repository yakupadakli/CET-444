﻿using System;
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
            Group group = new Group();
            if (user != null && semester != null && semester.isActive == true)
            {
                ViewData["semester"] = semester;
                ViewBag.SemesterID = semester.Id;
                if (user.Groups.Count() > 0)
                {
                    ViewBag.SelectedGroupID = group.Id;
                    group = user.Groups.FirstOrDefault();
                    return View(group);
                }
                else
                {
                    ViewBag.Error = "True";
                    ViewBag.Message = "You don't assigned any group yet! Please contact your instructor.";
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
            ICollection<Group> model = unitOfWork.GroupRepository.Get(g => g.SemesterID == SemesterID).ToList();
            return PartialView(model);

        }
        public ActionResult GetScenarioListTable(int SemesterID)
        {
            Semester semester = unitOfWork.SemesterRepository.GetByID(SemesterID);
            List<Scenario> model = new List<Scenario>();
            if (semester != null)
            {
                model = semester.Scenarios.Where(Sc => Sc.isActive == true).ToList();
            }
            return PartialView(model);
        }
        public ActionResult _PartialGetGroupScenarios(int SemesterID)
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
            ViewBag.SemesterID = SemesterID;
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
