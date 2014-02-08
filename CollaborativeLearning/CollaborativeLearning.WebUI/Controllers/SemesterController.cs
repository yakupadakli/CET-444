using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollaborativeLearning.Entities;
using CollaborativeLearning.DataAccess;
using CollaborativeLearning.WebUI.Filters;
using CollaborativeLearning.Entities;
using CollaborativeLearning.DataAccess;
using CollaborativeLearning.WebUI.Filters;
namespace CollaborativeLearning.WebUI.Controllers
{
    public class SemesterController : Controller
    {
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
        public ActionResult Create(Scenario collection)
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
        // GET: /Semester/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Semester/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Scenario collection)
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
        public ActionResult Delete(int id, Semester collection)
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
