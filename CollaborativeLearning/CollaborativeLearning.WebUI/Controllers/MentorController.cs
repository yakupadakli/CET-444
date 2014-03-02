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
    public class MentorController : Controller
    {

        #region Mentor Side
        private UnitOfWork unitOfWork = new UnitOfWork();

        [Authorize(Roles="Mentor")]
        public ActionResult Index()
        {
            unitOfWork = new UnitOfWork();
            User model = unitOfWork.UserRepository.GetByID(HelperController.GetCurrentUserId());
            return View(model);
        }

        public ActionResult _PartialAddCourse()
        {
            unitOfWork = new UnitOfWork();

            var SemesterList = unitOfWork.SemesterRepository.Get().ToList();
            User user = unitOfWork.UserRepository.GetByID(HelperController.GetCurrentUserId());
            List<Semester> semesters = new List<Semester>();

            if (user.Semesters.Count() > 0)
            {
                foreach (var semester in SemesterList)
                {
                    if (user.Semesters.Where(s => s.Id == semester.Id).Count() == 0)
                    {
                        semesters.Add(semester);
                    }
                }

            }
            else
            {
                semesters = SemesterList;
            }

            return PartialView(semesters);
        }
        public ActionResult _PartialMentorsCourseList(int id)
        {
            unitOfWork = new UnitOfWork();
            User user = unitOfWork.UserRepository.GetByID(id);

            return PartialView(user.Semesters.ToList());
        }
        public ActionResult AddCourse(int UserID, string CourseID)
        {
            unitOfWork = new UnitOfWork();
            User user = unitOfWork.UserRepository.GetByID(UserID);

            var semester = unitOfWork.SemesterRepository.Get(s => s.mentorRegisterCode == CourseID).FirstOrDefault();
            if (semester != null )
            {
                user.Semesters.Add(semester);
                unitOfWork.Save();
                ViewBag.AddCourseSuccess = "true";
            }
            else
            {
                ViewBag.AddCourseSuccess = "false";
            }

            return PartialView("_PartialMentorCourse");
        }
        public ActionResult DropCourse(int id)
        {
            unitOfWork = new UnitOfWork();
            Semester semester = unitOfWork.SemesterRepository.GetByID(id);
            User user = unitOfWork.UserRepository.GetByID(HelperController.GetCurrentUserId());
            if (user.Semesters.Where(sem => sem.Id == semester.Id).Count() > 0)
            {
                try
                {
                    user.Semesters.Remove(semester);
                    unitOfWork.Save();
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            return PartialView("_PartialMentorCourse");
        }
        public ActionResult Semester(int id)
        {
            ViewBag.SemesterID = id;
            Semester semester = unitOfWork.SemesterRepository.GetByID(id);
            User user = HelperController.GetCurrentUser();
            List<Group> groups = new List<Group>();
            foreach (var group in semester.Groups)
            {
                foreach (var u in group.Users)
                {
                    if(u.Id == user.Id)
                    {
                        groups.Add(group);
                    }
                }
            }

            ViewData["groups"] = groups.ToList();

            return View(semester);
        }

        public ActionResult _PartialGetMentorScenarios(int SemesterID)
        {
            unitOfWork = new UnitOfWork();
            List<Scenario> groupScenarios = new List<Scenario>();
            User user = HelperController.GetCurrentUser();
            ICollection<Group> groups = user.Groups.Where(s => s.SemesterID == SemesterID).ToList();
            
            ViewBag.SemesterID = SemesterID;

            return PartialView(groups);
        }

        public ActionResult _PartialHeaderSemeterInfo(int semesterId)
        {
            unitOfWork = new UnitOfWork();
            User user = HelperController.GetCurrentUser();
            ViewBag.CurrentSemester = unitOfWork.SemesterRepository.GetByID(semesterId).semesterName;
            return PartialView();
        }

        public ActionResult _PartialGetOtherGroupsForMenu(int SemesterID)
        {
            unitOfWork = new UnitOfWork();
            Semester Semester = unitOfWork.SemesterRepository.GetByID(SemesterID);
            List<Group> groupLists = Semester.Groups.ToList();
            User CurrentUser = HelperController.GetCurrentUser();
            List<Group> groups = new List<Group>();
            if (CurrentUser.Groups.Count > 0)
            {
              
                    foreach (var item2 in CurrentUser.Groups.Where(g => g.SemesterID == SemesterID).ToList())
                    {
                        groupLists.Remove(groupLists.Where(s=>s.Id == item2.Id).FirstOrDefault());
                    }
               
            }
            else
            {
                groups = groupLists;
            }

            return PartialView(groupLists);

        }
        //
        // GET: /Mentor/Create

        public ActionResult Create()
        {
            return View();
        }

        #endregion

        #region Admin Side

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Mentor/Edit/5

        public ActionResult Edit(int id)
        {
            User model = unitOfWork.UserRepository.GetByID(id);
            return PartialView(model);
        }

        //
        // POST: /Mentor/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            try
            {
                unitOfWork = new UnitOfWork();
                User u = unitOfWork.UserRepository.GetByID(user.Id);
                u.Username = user.Username;
                u.Email = user.Email;
                u.FirstName = user.FirstName;
                u.LastName = user.LastName;
                u.IsLockedOut = user.IsLockedOut;
                u.RoleID = user.RoleID;
                unitOfWork.UserRepository.Update(u);
                unitOfWork.Save();


                //return RedirectToAction("Index","Home");
                return RedirectToAction("_PartialGetMentorGrid");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Mentor/Delete/5

        public ActionResult Delete(int id)
        {
            User user = unitOfWork.UserRepository.GetByID(id);
            if (user != null)
            {
                unitOfWork.UserRepository.Delete(id);
                unitOfWork.Save();
            }
            return RedirectToAction("_PartialGetMentorGrid", "Mentor");
        }
        [HttpGet]
        public ActionResult _SemesterToMentor(int id)
        {
            unitOfWork = new UnitOfWork();
            IEnumerable<Semester> semesters = unitOfWork.SemesterRepository.Get();
            List<Semester> semesterList = new List<Entities.Semester>();

            User mentor = unitOfWork.UserRepository.GetByID(id);
            if (mentor != null)
            {
                foreach (var item in semesters)
                {
                    if (!item.Users.Contains(unitOfWork.UserRepository.GetByID(mentor.Id)))
                    {
                        semesterList.Add(item);
                    }
                }
            
            ViewBag.AllSemesters = semesterList;
            
             var mentorSemesterList = unitOfWork.UserRepository.Get(s => s.Id == id).FirstOrDefault().Semesters.ToList();

            ViewBag.ID = id;
            ViewBag.MentorName = mentor.FullName;
            return PartialView(mentorSemesterList);
            }
            return PartialView(null);
        }


        [HttpPost]
        public ActionResult _SemesterToMentor(int id, string[] SemesterMultiSelect)
        {
            var mentor = unitOfWork.UserRepository.GetByID(id);
            foreach (var item in SemesterMultiSelect)
            {
                var u = unitOfWork.SemesterRepository.GetByID(int.Parse(item));
                mentor.Semesters.Add(u);
            }
            unitOfWork.Save();
            ViewBag.AllMentors = unitOfWork.UserRepository.Get();
            return RedirectToAction("_SemesterToMentor", "Mentor", new { id = id });
        }

        [HttpPost]
        public ActionResult DeleteSemesterFromMentor(int userId, int semesterId)
        {
            Semester semester = unitOfWork.SemesterRepository.GetByID(Convert.ToInt32(semesterId));
            semester.Users.Remove(unitOfWork.UserRepository.GetByID(userId));
            unitOfWork.Save();
            return RedirectToAction("_SemesterToMentor", "Mentor", new { id = userId });
        }


        public ActionResult _PartialGetMentorGrid()
        {
            var allMentor = unitOfWork.UserRepository.Get().Where(m => m.RoleID == 2).ToList();

            return PartialView(allMentor);
        }
        public ActionResult _PartialEdit(int id) {
            unitOfWork = new UnitOfWork();
            User model = unitOfWork.UserRepository.GetByID(id);

            return PartialView("Edit",model);
        }

        public ActionResult _PartialGetMentorsBySemester(int id)
        {
            var mentorsList = unitOfWork.UserRepository.Get(s => s.Semesters.Where(se => se.Id == id && s.RoleID == 2).Count() > 0);
            ViewBag.semesterId = id;
            return PartialView(mentorsList);
        }

        [HttpPost]
        public ActionResult RemoveMentorSemester(int userId, int semesterId)
        {
            User user = unitOfWork.UserRepository.GetByID(userId);
            unitOfWork.SemesterRepository.GetByID(semesterId).Users.Remove(user);

            unitOfWork.Save();
            return RedirectToAction("_PartialGetMentorsBySemester", new { id = semesterId });
        }

        public ActionResult _PartialSelectMentors(int id)
        {
            ViewBag.ID = id;
            var AllList = unitOfWork.UserRepository.Get().Where(m => m.RoleID == 2).ToList();

            var MentorSemesterList = unitOfWork.SemesterRepository.GetByID(id).Users.ToList();

            bool t = false;
            List<User> UserList = new List<User>();
            foreach (var listItem in AllList)
            {
                t = false;
                foreach (var ss in MentorSemesterList)
                {
                    if (listItem.Id == ss.Id)
                    {
                        t = true;
                    }
                }
                if (!t)
                {
                    UserList.Add(listItem);
                }
            }



            ViewBag.AllMentors = UserList;
            return PartialView();
        }


        [HttpPost]
        public ActionResult AssignAsMentor(string[] UserMultiSelect)
        {
            foreach (var item in UserMultiSelect)
            {
                var s = unitOfWork.UserRepository.GetByID(int.Parse(item));
                s.RoleID = 2;
                unitOfWork.UserRepository.Update(s);
                unitOfWork.Save();
            }
            return RedirectToAction("_PartialGetMentorGrid", "Mentor");
        }


        [HttpPost]
        public ActionResult _MakeMentorStudent(int id)
        {

                var s = unitOfWork.UserRepository.GetByID(id);
                s.RoleID = 3;
                unitOfWork.UserRepository.Update(s);
                unitOfWork.Save();

            return RedirectToAction("_PartialGetMentorGrid", "Mentor");
        }

        [HttpPost]
        public ActionResult _MakeMentorAdmin(int id)
        {

            var s = unitOfWork.UserRepository.GetByID(id);
            s.RoleID = 1;
            unitOfWork.UserRepository.Update(s);
            unitOfWork.Save();

            return RedirectToAction("_PartialGetMentorGrid", "Mentor");
        }
        #endregion


    }
}
