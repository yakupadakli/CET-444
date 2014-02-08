using CollaborativeLearning.Entities;
using CollaborativeLearning.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollaborativeLearning.WebUI.Controllers
{
    public class SemesterController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Semester/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Semester/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Semester/Create

        public ActionResult Create()
        {           
            
            
            return View();
        }

        //
        // POST: /Semester/Create

        [HttpPost]
        public ActionResult Create(Semester collection)
        {
            Semester semester = new Semester();
            
            if (ModelState.IsValid)
            {
                try
                {
                    Semester semesterItem = new Semester();
                    semesterItem.year = collection.year;
                    semesterItem.semester = collection.semester;

                    string semesterCode = "cet" + collection.year.ToString();
                    semesterItem.specialCode = semesterCode;

                    semesterItem.regUserID = unitOfWork.UserRepository.Get(u => u.Username == WebSecurity.User.Identity.Name).First().UserId;
                    semesterItem.regDate = DateTime.Today;
            

                    return View("SemesterSummaryPartial", semester);
                }
                catch
                {
                    return View();
                }
            }
            return View(collection);

           
        }

        //
        // GET: /Semester/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Semester/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Semester/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Semester/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
