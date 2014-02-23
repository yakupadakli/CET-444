﻿using System;
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
      

        public ActionResult Index()  //initial action for students
        {
            unitOfWork = new UnitOfWork();
            User model = unitOfWork.UserRepository.GetByID(HelperController.GetCurrentUserId());
            return View(model);
        }

        public ActionResult _PartialAddCourse() {
            unitOfWork = new UnitOfWork();
            
            var SemesterList = unitOfWork.SemesterRepository.Get(s => s.isActive == true).ToList();
            List<Semester> semesters = new List<Semester>();
            foreach (var semester in Semester)
            {
                foreach (var userSemester in HelperController.GetCurrentUser().Semesters)
                {
                    if (semester!=userSemester)
                    {
                        semesters.Add(semester);
                    }
                }
            }

            return PartialView(semesters);
        }

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
                return RedirectToAction("Index", "Home");
            }
            return View(profile);

        }


        #region Semester Operation
        public ActionResult _PartialGetStudentsBySemester(int id)
        {
            var studentsList = unitOfWork.UserRepository.Get(s => s.Semesters.Where(se => se.Id == id  && s.RoleID == 3).Count() > 0);
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
