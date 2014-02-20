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
        //
        // GET: /Mentor/

        private UnitOfWork unitOfWork = new UnitOfWork();


        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Mentor/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Mentor/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Mentor/Create

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
            return RedirectToAction("Index", "Home");
        }

        

        public ActionResult _SemesterToMentor(int id)
        {
            IEnumerable<Semester> semesters = unitOfWork.SemesterRepository.Get(u => u.isActive == true);
            List<Semester> semesterList = new List<Entities.Semester>();

            User mentor = unitOfWork.UserRepository.GetByID(id);

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
            return PartialView(mentorSemesterList);
        }


        [HttpPost]
        public ActionResult _SemesterToMentor(int id, string[] UserMultiSelect)
        {
            var mentor = unitOfWork.UserRepository.GetByID(id);
            foreach (var item in UserMultiSelect)
            {
                var u = unitOfWork.SemesterRepository.GetByID(int.Parse(item));
                mentor.Semesters.Add(u);
            }
            unitOfWork.Save();
            ViewBag.AllMentors = unitOfWork.UserRepository.Get();
            return RedirectToAction("_PartialGetGroupsBySemester", "Group");
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
        //public ActionResult MentorAjaxHandler()
        //{
        //    //var allMentor = unitOfWork.UserRepository.Get();
        //    var allMentor = unitOfWork.UserRepository.Get().Where(m => m.RoleID == 2).ToList();
        //    var mentorList = from q in allMentor
        //                       select (new

        //                       {
        //                           Id = q.Id,
        //                           MentorName = q.FullName,
        //                           UserName = q.Username,
        //                           eMail = q.Email,
        //                           GroupCount = q.Groups.Count(),
        //                           Action = ""
        //                       });

        //    return Json(new
        //    {
        //        iTotalRecords = allMentor.Count(),
        //        iTotalDisplayRecords = mentorList.Count(),
        //        aaData = mentorList
        //    }, JsonRequestBehavior.AllowGet);

        //}

    }
}
