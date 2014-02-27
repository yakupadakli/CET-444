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
                tasks = unitOfWork.ScenarioRepository.GetByID(id).Tasks.OrderBy(t => t.OrderID);
                
            }
            else
            {
                tasks = unitOfWork.TaskRepository.Get().OrderBy(t => t.OrderID);
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
                    int orderId = 1;
                    if (scenarioId != null)
                    {
                        Task t = unitOfWork.ScenarioRepository.GetByID(scenarioId).Tasks.OrderByDescending(ta=>ta.OrderID).FirstOrDefault();
                        if(t != null)
                            orderId = t.OrderID + 1;
                    }
                    task.RegUserId = HelperController.GetCurrentUserId();
                    task.RegDateTime = DateTime.Now;
                    task.OrderID = orderId;
                    unitOfWork.TaskRepository.Insert(task);
                    unitOfWork.Save();

                    if (scenarioId != null)
                    {
                        unitOfWork = new UnitOfWork();
                        Scenario s = unitOfWork.ScenarioRepository.GetByID(scenarioId);
                        unitOfWork.TaskRepository.GetByID(task.Id).Scenarios.Add(s);
                        unitOfWork.Save();
                        unitOfWork = new UnitOfWork();
                        
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
            if (scenarioId != null)
            {
                Task task = unitOfWork.TaskRepository.GetByID(id);

                IEnumerable<Task> nextTasks = unitOfWork.ScenarioRepository.GetByID(scenarioId).Tasks.Where(t => t.OrderID > task.OrderID);
                foreach (var item in nextTasks)
                {
                    item.OrderID--;
                }

                unitOfWork.Save();
            }
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

                }
            }
            catch
            {
                return PartialView(task);
            }
            return PartialView(task);
        }

        public ActionResult TaskDown(int id, int? scenarioId = null)
        {
            if (scenarioId != null)
            {
                Task lastTask = unitOfWork.ScenarioRepository.GetByID(scenarioId).Tasks.OrderByDescending(ta => ta.OrderID).FirstOrDefault();
                int lastOrderId = lastTask.OrderID;

                Task t1 = unitOfWork.TaskRepository.GetByID(id);
                int oldId = t1.OrderID;
                if (oldId < lastOrderId)
                {
                    int newId = oldId + 1;
                    Task t2 = unitOfWork.ScenarioRepository.GetByID(scenarioId).Tasks.Where(tt => tt.OrderID == newId).FirstOrDefault();
                    t1.OrderID = newId;
                    t2.OrderID = oldId;
                    unitOfWork.Save();
                }
            }
            return RedirectToAction("_PartialStudentTask", new { id = scenarioId });
        }

        public ActionResult TaskUp(int id, int? scenarioId = null)
        {
            if (scenarioId != null)
            {
                Task firstTask = unitOfWork.ScenarioRepository.GetByID(scenarioId).Tasks.OrderBy(ta => ta.OrderID).FirstOrDefault();
                int firstOrderId = firstTask.OrderID;

                Task t1 = unitOfWork.TaskRepository.GetByID(id);
                int oldId = t1.OrderID;
                if (oldId > firstOrderId)
                {
                    int newId = oldId - 1;
                    Task t2 = unitOfWork.ScenarioRepository.GetByID(scenarioId).Tasks.Where(tt => tt.OrderID == newId).FirstOrDefault();
                    t1.OrderID = newId;
                    t2.OrderID = oldId;
                    unitOfWork.Save();
                }
            }
            return RedirectToAction("_PartialStudentTask", new { id = scenarioId });
        }
    }
}
