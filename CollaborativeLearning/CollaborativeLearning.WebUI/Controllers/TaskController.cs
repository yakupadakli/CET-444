using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            TempData["scenarioId"] = null;
            return View();
        }
        public ActionResult TaskAjaxHandler()
        {
            var results = from task in unitOfWork.TaskRepository.Get()
                          select new
                          {
                              Id = task.Id,
                              TaskName = task.TaskName,
                              Content = task.Id,
                              ScenariosCount = task.Scenarios.Count(),
                              Action = task.Id
                          };



            return Json(new
            {
                iTotalRecords = results.Count(),
                iTotalDisplayRecords = results.Count(),
                aaData = results
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult _PartialTaskShow(int id)
        {
            Task model = unitOfWork.TaskRepository.GetByID(id);
            return PartialView(model);
        }

        public ActionResult _PartialStudentTask(int id)
        {
            ViewBag.id = id;
            ViewBag.scenarioId = id;
            IEnumerable<Task> Tasks;
            Tasks = GetTask(id);

            return PartialView(Tasks);
        }
        private IEnumerable<Task> GetTask(int? id)
        {
            IEnumerable<CollaborativeLearning.Entities.Task> tasks;
            
            if (id.HasValue)
            {
                tasks = unitOfWork.ScenarioRepository.GetByID(id).Tasks.OrderBy(t => t.RegDateTime);
                
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
            {
                TempData["scenarioId"] = scenarioId;
                ViewBag.scenarioId = scenarioId;
            }
            return PartialView();
        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _PartialTaskCreate(Task task, int? scenarioId)
        {
            try
            {
                if (task != null)
                {
                    task.RegUserId = HelperController.GetCurrentUserId();
                    task.RegDateTime = DateTime.Now;
                    unitOfWork.TaskRepository.Insert(task);
                    unitOfWork.Save();

                    if (scenarioId != null)
                    {
                        unitOfWork = new UnitOfWork();
                        Scenario s = unitOfWork.ScenarioRepository.GetByID(scenarioId);
                        unitOfWork.TaskRepository.GetByID(task.Id).Scenarios.Add(s);
                        unitOfWork.Save();
                        return RedirectToAction("_PartialStudentTask", new { id = scenarioId });
                        
                    }
                    //else
                    return RedirectToAction("Index", "Task");
                }
                return RedirectToAction("Index", "Home");
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

        public ActionResult Delete(int id, int? scenarioId = null)
        {
            unitOfWork.TaskRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("_PartialStudentTask", new { id = scenarioId });
        }
        [HttpGet]
        public ActionResult _PartialSingleTaskUpdate(int id)
        {
            Task task = unitOfWork.TaskRepository.GetByID(id);
            return PartialView(task);
        }

        [HttpGet]
        public ActionResult _PartialTaskUpdate(int id, int? scenarioId = null)
        {
            if (id != null)
            {
                Task task = unitOfWork.TaskRepository.GetByID(id);
                if (scenarioId != null)
                    TempData["scenarioId"] = scenarioId;
                task.Content = WebUtility.HtmlDecode(task.Content);
                ViewBag.scenarioId = scenarioId;
                return PartialView(task);
            }
            return RedirectToAction("Index","Scenario");
        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _PartialTaskUpdate(Task task,int scenarioId)
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
                    return RedirectToAction("_PartialStudentTask", new { id = scenarioId });

                    //if (TempData["scenarioId"] != null)
                    //{
                    //    int id = (int)TempData["scenarioId"];
                    //    IEnumerable<Task> Tasks;
                    //    Tasks = GetTask(id);
                    //    //return Json(Tasks, JsonRequestBehavior.AllowGet);
                    //    return PartialView("_PartialTaskUpdate", task);
                    //    //return RedirectToAction("Index", "Scenario", new {id= id });
                    //}
                    //return PartialView("_PartialStudentTask");
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
