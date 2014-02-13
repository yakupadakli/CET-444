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
                unitOfWork.UserRepository.Update(user);
                unitOfWork.Save();


                return RedirectToAction("Index","Home");
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

        ////
        //// POST: /Mentor/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public ActionResult _PartialGetMentorGrid()
        {
            return PartialView();
        }

        public ActionResult MentorAjaxHandler()
        {
            //var allMentor = unitOfWork.UserRepository.Get();
            var allMentor = unitOfWork.UserRepository.Get().Where(m => m.RoleID == 2).ToList();
            var mentorList = from q in allMentor
                               select (new

                               {
                                   Id = q.Id,
                                   MentorName = q.FullName,
                                   UserName = q.Username,
                                   eMail = q.Email,
                                   GroupCount = q.Groups.Count(),
                                   Action = ""
                               });

            return Json(new
            {
                iTotalRecords = allMentor.Count(),
                iTotalDisplayRecords = mentorList.Count(),
                aaData = mentorList
            }, JsonRequestBehavior.AllowGet);

        }

    }
}
