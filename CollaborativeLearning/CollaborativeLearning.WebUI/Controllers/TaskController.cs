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

        public ActionResult _PartialStudentTask(int id)
        {
            ViewBag.id = id;
            IEnumerable<Task> Tasks;
            Tasks = GetTask(id);
            //return Json(Tasks, JsonRequestBehavior.AllowGet);

            return PartialView(Tasks);
        }
        private IEnumerable<Task> GetTask(int? id)
        {
            IEnumerable<CollaborativeLearning.Entities.Task> tasks;
            
            if (id.HasValue)
            {
                tasks = unitOfWork.ScenarioRepository.GetByID(id).Tasks.OrderByDescending(t => t.RegDateTime);
            }
            else
            {
                tasks = unitOfWork.TaskRepository.Get().OrderByDescending(t => t.RegDateTime);
            }

            return tasks;
        }
        public ActionResult _PartialTaskCreate(int? scenarioId)
        {
            if (scenarioId != null)
                TempData["scenarioId"] = scenarioId;
            return PartialView();
        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        public JsonResult _PartialTaskCreate(Task task, int? scenarioId)
        {
            try
            {
                if (task != null)
                {
                    task.RegUserId = HelperController.GetCurrentUserId();
                    task.RegDateTime = DateTime.Now;
                    unitOfWork.TaskRepository.Insert(task);
                    unitOfWork.Save();

                    if (TempData["scenarioId"] != null )
                    {
                        unitOfWork = new UnitOfWork();
                        Scenario s = unitOfWork.ScenarioRepository.GetByID(TempData["scenarioId"]);
                        unitOfWork.TaskRepository.GetByID(task.Id).Scenarios.Add(s);
                        unitOfWork.Save();
                        //return RedirectToAction("Index", "Scenario", new { id = TempData["scenarioId"] });
                        IEnumerable<Task> Tasks;
                        Tasks = GetTask((int)TempData["scenarioId"]);
                        return Json(unitOfWork.TaskRepository.Get().Select(a=>a.TaskName), JsonRequestBehavior.AllowGet);
                    }
                    //else
                    //    return RedirectToAction("Index", "Scenario");
                }
            }
            catch
            {
                //return View();
            }

            IEnumerable<Task> Taskdd = GetTask((int)TempData["scenarioId"]);
            return Json(Taskdd, JsonRequestBehavior.AllowGet);
        }
        
        //
        // GET: /Task/Delete/5

        public ActionResult Delete(int id)
        {
            unitOfWork.TaskRepository.Delete(id);
            unitOfWork.Save();
            if (TempData["scenarioId"] != null)
                return RedirectToAction("Index", "Scenario", new { id = TempData["scenarioId"] });
            return RedirectToAction("Index","Scenario");
        }


        [HttpGet]
        public ActionResult _PartialTaskUpdate(int id, int scenarioId)
        {
            if (id != null)
            {
                Task task = unitOfWork.TaskRepository.GetByID(id);
                if (scenarioId != null)
                    TempData["scenarioId"] = scenarioId;
                return PartialView(task);
            }
            return RedirectToAction("Index","Scenario");
        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        public ActionResult _PartialTaskUpdate(Task task)
        {
            try
            {
                if (task != null)
                {
                    Task t = unitOfWork.TaskRepository.GetByID(task.Id);

                    t.TaskName = task.TaskName;
                    t.Content = task.Content;

                    unitOfWork.TaskRepository.Update(t);
                    unitOfWork.Save();
                    if (TempData["scenarioId"] != null)
                    {
                        int id = (int)TempData["scenarioId"];
                        IEnumerable<Task> Tasks;
                        Tasks = GetTask(id);
                        //return Json(Tasks, JsonRequestBehavior.AllowGet);
                        return PartialView();
                    }
                    return RedirectToAction("Index", "Scenario");
                }
            }
            catch
            {
                return PartialView(task);
            }
            return PartialView(task);
        }
    }
}
