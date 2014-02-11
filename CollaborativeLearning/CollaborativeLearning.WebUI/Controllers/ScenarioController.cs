using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollaborativeLearning.Entities;
using CollaborativeLearning.DataAccess;
using CollaborativeLearning.WebUI.Filters;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace CollaborativeLearning.WebUI.Controllers
{
    public class ScenarioController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Scenario/

        public ActionResult Index()
        {
            return View();
        }
        
        //

        public ActionResult _PartialGetScenarioGrid()
        {
            return PartialView();
        }

        public ActionResult ScenarioAjaxHandler()
        {
            var results = from Ord in unitOfWork.SemesterRepository.Get()
                          select new
                          {
                              Id = Ord.Id,
                              semester = Ord.semester,
                              year = Ord.year,
                              semesterName = Ord.semesterName,
                              StudentCount = Ord.Users.Where(u => u.Role.RoleName == "Student").Count(),
                              MentorCount = Ord.Users.Where(u => u.Role.RoleName == "Mentor").Count(),
                          };



            return Json(new {
                iTotalRecords = results.Count(),
                iTotalDisplayRecords = results.Count(),
                aaData = results
            }, JsonRequestBehavior.AllowGet);

        }

        //
        // GET: /Scenario/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Scenario/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Scenario/Create

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
        // GET: /Scenario/Edit/5

        public ActionResult Edit(int id)
        {
            Semester model = unitOfWork.SemesterRepository.GetByID(id);
            return PartialView(model);
        }

        //
        // POST: /Scenario/Edit/5

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
        // GET: /Scenario/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Scenario/Delete/5

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
