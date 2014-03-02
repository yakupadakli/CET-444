using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CollaborativeLearning.Entities;
using CollaborativeLearning.DataAccess;
using CollaborativeLearning.WebUI.Filters;
using System.Text.RegularExpressions;
using CollaborativeLearning.WebUI.Models;

namespace CollaborativeLearning.WebUI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        #region StudentsIndexOperation
        public ActionResult Index()  //initial action for students
        {
            unitOfWork = new UnitOfWork();
            User model = unitOfWork.UserRepository.GetByID(HelperController.GetCurrentUserId());
            if (model.RoleID == 2)
            {
                return RedirectToAction("Index", "Mentor");
            }
            return View(model);
        }
    
        public ActionResult _PartialAddCourse()
        {
            unitOfWork = new UnitOfWork();

            var SemesterList = unitOfWork.SemesterRepository.Get(s => s.isActive == true).ToList();
            User user = unitOfWork.UserRepository.GetByID(HelperController.GetCurrentUserId());
            List<Semester> semesters = new List<Semester>();

            if (user.Semesters.Where(s=>s.isActive==true).Count() > 0)
            {
                foreach (var semester in SemesterList)
                {
                    if (user.Semesters.Where(s=>s.isActive==true && s.Id==semester.Id).Count()==0)
                    {
                        semesters.Add(semester);
                    }
                }

            }
            else {
                semesters = SemesterList;
            }

            return PartialView(semesters);
        }
        public ActionResult _PartialStudentsCourseList(int id)
        {
            unitOfWork = new UnitOfWork();
            User user = unitOfWork.UserRepository.GetByID(id);
            
            return PartialView(user.Semesters.Where(s=>s.isActive==true).ToList());
        }

        public ActionResult AddCourse(int UserID, string CourseID)
        {
            unitOfWork = new UnitOfWork();
            User user = unitOfWork.UserRepository.GetByID(UserID);
            
                var semester = unitOfWork.SemesterRepository.Get(s=>s.registerCode==CourseID).FirstOrDefault();
                if (semester!=null && semester.isActive==true )
                {
                        user.Semesters.Add(semester);
                        unitOfWork.Save();
                        ViewBag.AddCourseSuccess = "true";
                }else{
                    ViewBag.AddCourseSuccess = "false";
                }
            
            return PartialView("_PartialUserCourse");
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
            return PartialView("_PartialUserCourse");
        }
        public ActionResult CancelConcent(int id)
        {
            unitOfWork = new UnitOfWork();
            StudentCourseRequest scr = unitOfWork.StudentCourseRequestRepository.GetByID(id);
            User user = unitOfWork.UserRepository.GetByID(scr.UserId);
            Semester semester = unitOfWork.SemesterRepository.GetByID(scr.SemesterId);
            if (scr!=null && scr.UserId == HelperController.GetCurrentUserId())
            {
                user.Semesters.Remove(semester);
                unitOfWork.StudentCourseRequestRepository.Delete(scr.Id);
                unitOfWork.Save();
            }
            return PartialView("_PartialUserCourse");

        }
        #endregion
        public ActionResult Profile()
        {
            int userId = HelperController.GetCurrentUserId();
            User user = unitOfWork.UserRepository.GetByID(userId);

            CollaborativeLearning.WebUI.Models.AccountModels.ProfileModel profile = new AccountModels.ProfileModel();
            profile.Email = user.Email;
            profile.FirsName = user.FirstName;
            profile.Gender = user.Gender;
            profile.LastName = user.LastName;
            profile.PhoneNumber = user.PhoneNumber;
            return View(profile);
        }

        // POST: /Account/Register
        [HttpPost]
        public ActionResult Profile(CollaborativeLearning.WebUI.Models.AccountModels.ProfileModel profile)
        {

            if (ModelState.IsValid)
            {
                int userId = HelperController.GetCurrentUserId();
                User user = unitOfWork.UserRepository.GetByID(userId);

                user.Email = profile.Email;
                user.FirstName = profile.FirsName;
                user.Gender = profile.Gender;
                user.LastName = profile.LastName;
                user.PhoneNumber = profile.PhoneNumber;

                unitOfWork.UserRepository.Update(user);
                unitOfWork.Save();
                return View(profile);
                //return RedirectToAction("Index", "Home");
            }
            return View(profile);

        }


        #region Semester Operation
        public ActionResult _PartialGetStudentsBySemester(int id)
        {
            var studentsList = unitOfWork.UserRepository.Get(s => s.Semesters.Where(se => se.Id == id && s.RoleID == 3).Count() > 0);
            ViewBag.semesterId = id;
            return PartialView(studentsList);
        }

        [HttpPost]
        public ActionResult RemoveUserSemester(int userId, int semesterId)
        {
            User user = unitOfWork.UserRepository.GetByID(userId);
            unitOfWork.SemesterRepository.GetByID(semesterId).Users.Remove(user);

            unitOfWork.Save();
            return RedirectToAction("_PartialGetStudentsBySemester", new { id = semesterId });
        }
        #endregion


        public ActionResult _PartialSelectStudents(int id)
        {
            ViewBag.ID = id;
            var AllList = unitOfWork.UserRepository.Get().Where(m => m.RoleID == 3).ToList();

            var StudentSemesterList = unitOfWork.SemesterRepository.GetByID(id).Users.ToList();

            bool t = false;
            List<User> UserList = new List<User>();
            foreach (var listItem in AllList)
            {
                t = false;
                foreach (var ss in StudentSemesterList)
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



            ViewBag.AllStudents = UserList;
            return PartialView();
        }
        public ActionResult _PartialSelectUsers()
        {
            var AllStudents = unitOfWork.UserRepository.Get().Where(m => m.RoleID == 3).ToList();

            ViewBag.Students = AllStudents;

            return PartialView();
        }


    }
}
