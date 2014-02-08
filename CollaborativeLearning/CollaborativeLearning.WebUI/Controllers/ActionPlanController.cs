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
    public class ActionPlanController : Controller
    {
        //
        // GET: /ActionPlan/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /ActionPlan/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ActionPlan/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ActionPlan/Create

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
        // GET: /ActionPlan/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ActionPlan/Edit/5

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
        // GET: /ActionPlan/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ActionPlan/Delete/5

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
