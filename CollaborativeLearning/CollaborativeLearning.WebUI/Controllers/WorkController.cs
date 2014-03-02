using CollaborativeLearning.DataAccess;
using CollaborativeLearning.Entities;
using CollaborativeLearning.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CollaborativeLearning.WebUI.Controllers
{
    public class WorkController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: /Work/
        #region Admin
        public ActionResult _PartialWork(int id, int? semesterId)
        {
            ViewBag.id = id;
            ViewBag.scenarioId = id;
            if (semesterId != null)
                ViewBag.semesterId = semesterId;
            IEnumerable<Work> Works;
            Works = GetWork(id);
            //return Json(Works, JsonRequestBehavior.AllowGet);

            return PartialView(Works);
        }
        public ActionResult _PartialWorkWithDueDate(int scenarioId , int semesterId)
        {
            ViewBag.id = scenarioId;
            ViewBag.scenarioId = scenarioId;
            ViewBag.semesterId = semesterId;
            IEnumerable<Work> Works;
            Works = GetWork(scenarioId);
            List<WorkWithDueDate> worksWithDueDate = new List<WorkWithDueDate>();
            foreach (var work in Works)
            {
                WorkWithDueDate workWithDueDate = new WorkWithDueDate
                {
                    Id = work.Id,
                    SemesterID = semesterId,
                    WorkID = work.Id,
                    ScenarioID = scenarioId,
                    DueDate = DateTime.MinValue
                };
                worksWithDueDate.Add(workWithDueDate);

            }
            //return Json(Works, JsonRequestBehavior.AllowGet);

            return PartialView(worksWithDueDate);
        }
        private IEnumerable<Work> GetWork(int? id)
        {
            IEnumerable<CollaborativeLearning.Entities.Work> Works;

            if (id.HasValue)
            {
                Works = unitOfWork.ScenarioRepository.GetByID(id).Works.OrderBy(t => t.OrderID);
            }
            else
            {
                Works = unitOfWork.WorkRepository.Get().OrderBy(t => t.OrderID);
            }

            return Works;
        }
        public ActionResult _PartialWorkCreate(int? scenarioId, int? semesterId)
        {
            if (scenarioId != null)
                ViewBag.scenarioId = scenarioId;

            if (semesterId != null || semesterId != 0)
                ViewBag.semesterId = semesterId;
            else
                ViewBag.semesterId = 0;

            return PartialView();
        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _PartialWorkCreate(Work Work, int? scenarioId, int? semesterId)
        {
            try
            {
                if (Work.Name != null && Work.Description != null )
                {
                    int orderId = 1;
                    if (scenarioId != null)
                    {
                        Work t = unitOfWork.ScenarioRepository.GetByID(scenarioId).Works.OrderByDescending(ta => ta.OrderID).FirstOrDefault();
                        if (t != null)
                            orderId = t.OrderID + 1;
                    }
                    Work.RegUserID = HelperController.GetCurrentUserId();
                    Work.RegDate = DateTime.Now;
                    Work.OrderID = orderId;
                    unitOfWork.WorkRepository.Insert(Work);
                    unitOfWork.Save();

                    if (scenarioId != null)
                    {
                        unitOfWork = new UnitOfWork();
                        Scenario s = unitOfWork.ScenarioRepository.GetByID(TempData["scenarioId"]);
                        unitOfWork.WorkRepository.GetByID(Work.Id).Scenarios.Add(s);
                        unitOfWork.Save();
                        if (semesterId.HasValue)
                            return RedirectToAction("_PartialWork", new { id = scenarioId, semesterId = semesterId });
                        else
                            return RedirectToAction("_PartialWork", new { id = scenarioId });
                    }
                    else
                        return RedirectToAction("Index", "Scenario");
                }
            }
            catch
            {
                return View();
            }
            return PartialView(Work);
        }

        //
        // GET: /Work/Delete/5

        public ActionResult Delete(int id, int scenarioId)
        {
            if (scenarioId != null)
            {
                Work Work = unitOfWork.WorkRepository.GetByID(id);

                IEnumerable<Work> nextWorks = unitOfWork.ScenarioRepository.GetByID(scenarioId).Works.Where(t => t.OrderID > Work.OrderID);
                foreach (var item in nextWorks)
                {
                    item.OrderID--;
                }

                unitOfWork.Save();
            }
            unitOfWork.WorkRepository.Delete(id);
            unitOfWork.Save();
            if (scenarioId != null)
                return RedirectToAction("_PartialWork", new { id = scenarioId });
            else
                return RedirectToAction("Index", "Scenario");
        }


        [HttpGet]
        public ActionResult _PartialWorkUpdate(int id, int scenarioId, int? semesterId)
        {
            if (id != null)
            {
                Work Work = unitOfWork.WorkRepository.GetByID(id);
                if (scenarioId != null)
                    ViewBag.scenarioId = scenarioId;
                if (semesterId.HasValue)
                    ViewBag.semesterId = semesterId;
                else
                    ViewBag.semesterId = 0;
                Work.Description = WebUtility.HtmlDecode(Work.Description);
                return PartialView(Work);
            }
            return RedirectToAction("Index", "Scenario");
        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _PartialWorkUpdate(Work Work,int scenarioId, int semesterId)
        {
            try
            {
                if (Work != null)
                {
                    Work t = unitOfWork.WorkRepository.GetByID(Work.Id);

                    t.Name = Work.Name;
                    t.Description = Work.Description;

                    unitOfWork.WorkRepository.Update(t);
                    unitOfWork.Save();
                    if (scenarioId != null)
                    {
                        if (semesterId != 0)
                            return RedirectToAction("_PartialWork", new { id = scenarioId, semesterId = semesterId });
                        else
                            return RedirectToAction("_PartialWork", new { id = scenarioId });
                    }
                    else
                        return RedirectToAction("Index", "Scenario");
                }
            }
            catch
            {
                return PartialView(Work);
            }
            return PartialView(Work);
        }

        [HttpGet]
        public ActionResult _PartialWorkWithDueDateUpdate(int id, int scenarioId,int semesterId)
        {
            if (id != null)
            {
                Work work = unitOfWork.WorkRepository.GetByID(id);
                if (scenarioId != null)
                    ViewBag.scenarioId = scenarioId;
                WorkSemesterDueDate workSemester = unitOfWork.WorkSemesterDueDateRepository.Get(w => w.WorkID == id).FirstOrDefault();
                DateTime DueDate = DateTime.Now;
                if (workSemester != null)
                {
                    DueDate = workSemester.DueDate;
                    ViewBag.NoDueDate = false;
                }
                else
                {
                    ViewBag.NoDueDate = true;
                }
                        
                WorkWithDueDate workWithDueDate = new WorkWithDueDate
                {
                    Id = work.Id,
                    SemesterID = semesterId,
                    WorkID = id,
                    DueDate = DueDate
                    

                };

                return PartialView(workWithDueDate);
            }
            return RedirectToAction("Index", "Scenario");
        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _PartialWorkWithDueDateUpdate(WorkWithDueDate Work, int scenarioId, string DueDate_Date,string DueDate_Time)
        {
            try
            {
                if (Work != null)
                {
                    string dueDateAll = DueDate_Date + " " + DueDate_Time;
                    DateTime dueDateDateTime = Convert.ToDateTime(dueDateAll);

                    WorkSemesterDueDate workSemester = unitOfWork.WorkSemesterDueDateRepository.Get(w => w.WorkID == Work.WorkID).FirstOrDefault();
                    if (workSemester == null)
                    {
                        workSemester = new WorkSemesterDueDate
                        {
                            WorkID = Work.WorkID,
                            DueDate = dueDateDateTime,
                            RegDate = DateTime.Now,
                            RegUserID = HelperController.GetCurrentUserId(),
                            SemesterID = Work.SemesterID
                        };
                        unitOfWork.WorkSemesterDueDateRepository.Insert(workSemester);
                    }
                    else
                    {
                        workSemester.DueDate = dueDateDateTime;
                        unitOfWork.WorkSemesterDueDateRepository.Update(workSemester);

                    }
                    
                    unitOfWork.Save();
                    if (scenarioId != null)
                        return RedirectToAction("_PartialWork", new { id = scenarioId, semesterId=Work.SemesterID });
                    else
                        return RedirectToAction("Index", "Scenario");
                }
            }
            catch
            {
                return PartialView(Work);
            }
            return PartialView(Work);
        }

        public ActionResult ChangeActiveStatus(int id, string Active,int scenarioId, int semesterId)
        {
            unitOfWork = new UnitOfWork();
            if (Active == "True")
            {
                unitOfWork.WorkRepository.GetByID(id).isActive = false;

            }
            else
            {
                unitOfWork.WorkRepository.GetByID(id).isActive = true;

            }
            unitOfWork.Save();
            if (scenarioId != null && semesterId != null)
                return RedirectToAction("_PartialWork", new { id = scenarioId, semesterId = semesterId });
            else if (scenarioId != null)
                return RedirectToAction("_PartialWork", new { id = scenarioId});
            else
                return RedirectToAction("Index", "Scenario");
        }
        #endregion
        #region Change OrderID
        public ActionResult WorkDown(int id, int? scenarioId = null,int semesterId=0)
        {
            if (scenarioId != null)
            {
                Work lastWork = unitOfWork.ScenarioRepository.GetByID(scenarioId).Works.OrderByDescending(ta => ta.OrderID).FirstOrDefault();
                int lastOrderId = lastWork.OrderID;

                Work t1 = unitOfWork.WorkRepository.GetByID(id);
                int oldId = t1.OrderID;
                if (oldId < lastOrderId)
                {
                    int newId = oldId + 1;
                    Work t2 = unitOfWork.ScenarioRepository.GetByID(scenarioId).Works.Where(tt => tt.OrderID == newId).FirstOrDefault();
                    t1.OrderID = newId;
                    t2.OrderID = oldId;
                    unitOfWork.Save();
                }
            }
            if (scenarioId != null)
            {
                if (semesterId != 0)
                    return RedirectToAction("_PartialWork", new { id = scenarioId, semesterId = semesterId });
                else
                    return RedirectToAction("_PartialWork", new { id = scenarioId });
            }
            else
                return RedirectToAction("Index", "Scenario");
        }

        public ActionResult WorkUp(int id, int? scenarioId = null, int semesterId=0)
        {
            if (scenarioId != null)
            {
                Work firstWork = unitOfWork.ScenarioRepository.GetByID(scenarioId).Works.OrderBy(ta => ta.OrderID).FirstOrDefault();
                int firstOrderId = firstWork.OrderID;

                Work t1 = unitOfWork.WorkRepository.GetByID(id);
                int oldId = t1.OrderID;
                if (oldId > firstOrderId)
                {
                    int newId = oldId - 1;
                    Work t2 = unitOfWork.ScenarioRepository.GetByID(scenarioId).Works.Where(tt => tt.OrderID == newId).FirstOrDefault();
                    t1.OrderID = newId;
                    t2.OrderID = oldId;
                    unitOfWork.Save();
                }
            }
            if (scenarioId != null)
            {
                if (semesterId != 0)
                    return RedirectToAction("_PartialWork", new { id = scenarioId, semesterId = semesterId });
                else
                    return RedirectToAction("_PartialWork", new { id = scenarioId });
            }
            else
                return RedirectToAction("Index", "Scenario");
        }
        #endregion

        #region StudentArea
        public ActionResult _PartialGetWorkForGroup(int GroupID,int ScenarioID) {
            unitOfWork = new UnitOfWork();
           List<GroupScenarioWork> model = new List<GroupScenarioWork>();
            
            Group group = unitOfWork.GroupRepository.GetByID(GroupID);
            ICollection<Work> worklist = unitOfWork.ScenarioRepository.GetByID(ScenarioID).Works.ToList();
            List<WorkSemesterDueDate> scenarioWork = new List<WorkSemesterDueDate>();
            
            foreach (var item in worklist.Where(w=>w.isActive==true))
            {
                var wsd = unitOfWork.WorkSemesterDueDateRepository.Get(s => s.WorkID == item.Id).FirstOrDefault();
                if (wsd!=null)
                {
                    scenarioWork.Add(wsd);
                }
            }

            foreach (var item in scenarioWork)
            {         
                var groupWork = unitOfWork.GroupWorkRepository.Get(g=>g.GroupID==GroupID && g.WorkId==item.WorkID).LastOrDefault();
                GroupScenarioWork gsw = new GroupScenarioWork
                {
                    GroupID = group.Id,
                    Group = group,
                    WorkSemesterDueDateID = item.Id,
                    WorkSemesterDueDate = item
                };
                if (groupWork !=null)
                {
                    gsw.GroupWorkID = groupWork.Id;
                    gsw.GroupWork = groupWork;
                }

                model.Add(gsw);
            }
            ViewBag.GroupID = GroupID;
            ViewBag.ScenarioID = ScenarioID;
            return PartialView(model.OrderBy(s=>s.WorkSemesterDueDate.DueDate).ToList());
            
        }
        #endregion
    }
}
