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
    public class TaskController : Controller
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: /Task/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _PartialStudentTask(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }
        public ActionResult _PartialTaskCreate(int? scenarioId)
        {
            if (scenarioId != null)
                ViewBag.scenarioId = scenarioId;
            return PartialView();
        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        public ActionResult _PartialTaskCreate(Task task)
        {
            try
            {
                //if (task != null)
                //{
                //    task.RegUserId = HelperController.GetCurrentUserId();
                //    task.RegDateTime = DateTime.Now;
                //    unitOfWork.TaskRepository.Insert(task);
                //    unitOfWork.Save();

                //    if(ViewBag.scenarioId != null)
                //    {
                //        unitOfWork = new UnitOfWork();
                //        Scenario s = unitOfWork.ScenarioRepository.GetByID(ViewBag.scenarioId);
                //        unitOfWork.TaskRepository.GetByID(task.Id).Scenarios.Add(s);
                //        unitOfWork.Save();
                //        return RedirectToAction("Details", "Scenario",ViewBag.scenarioId);
                //    }
                //    else
                //        return RedirectToAction("Details", "Scenario");
                //}
            }
            catch
            {
                return View();
            }
            return PartialView(task);
        }
        //
        // GET: /Task/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Task/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Task/Create

        [HttpPost]
        public ActionResult Create(TaskController collection)
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
        // GET: /Task/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, TaskController collection)
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
        // GET: /Task/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Task/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, TaskController collection)
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
