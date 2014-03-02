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

        #region Admin Section
        public ActionResult _PartialGetScenarioGrid()
        {
            var allScenario = unitOfWork.ScenarioRepository.Get().ToList();

            return PartialView(allScenario);
        }
        public ActionResult _PartialGoToScenario()
        {
            return PartialView();
        }
        public ActionResult SelectScenario(Scenario m)
        {
            Scenario model = unitOfWork.ScenarioRepository.GetByID(m.Id);

            return RedirectToAction("Index", new { id = model.Id });
        }

        public ActionResult ChangeActiveStatus(int id, string ScenarioActive)
        {
            unitOfWork = new UnitOfWork();
            if (ScenarioActive == "True")
            {
                unitOfWork.ScenarioRepository.GetByID(id).isActive = false;

            }
            else
            {
                unitOfWork.ScenarioRepository.GetByID(id).isActive = true;

            }
            unitOfWork.Save();
            return RedirectToAction("_PartialGetScenarioGrid");
        }

        public ActionResult Index(int? id=null)
        {
            if (id == null)
                return RedirectToAction("Index","Home");
            Scenario scenario = unitOfWork.ScenarioRepository.GetByID(id);
            ViewBag.scenarioId = id;
            return View(scenario);
        }

        //
        
        public ActionResult _PartialCreate()
        {
            return PartialView();
        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        public ActionResult _PartialCreate(Scenario scenario)
        {
            try
            {
                if (scenario != null)
                {
                    scenario.RegUserID = HelperController.GetCurrentUserId();
                    scenario.RegDate = DateTime.Now;
                    unitOfWork.ScenarioRepository.Insert(scenario);
                    unitOfWork.Save();
                }
            }
            catch
            {
                return View();
            }
            return RedirectToAction("_PartialGetScenarioGrid");
        }

        public ActionResult _PartialSemesterScenarioCreate(int id)
        {
            ViewBag.semesterId = id;
            return PartialView();

        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        public ActionResult _PartialSemesterScenarioCreate(Scenario scenario, int semesterId)
        {
            try
            {
                if (scenario != null)
                {
                    scenario.RegUserID = HelperController.GetCurrentUserId();
                    scenario.RegDate = DateTime.Now;
                    unitOfWork.ScenarioRepository.Insert(scenario);
                    unitOfWork.Save();
                }
            }
            catch
            {
                return View();
            }
            return RedirectToAction("AddScenarioToSemester", "Semester", new { SemesterID = semesterId, scenarioId = scenario.Id });
        }


        //
        // GET: /Scenario/Edit/5

        public ActionResult _PartialEdit(int id)
        {
            unitOfWork = new UnitOfWork();
            Scenario model = unitOfWork.ScenarioRepository.GetByID(id);
            return PartialView("Edit", model);
        }


        public ActionResult Edit(int id)
        {
            Scenario model = unitOfWork.ScenarioRepository.GetByID(id);
            return PartialView(model);
        }


        [HttpPost]
        public ActionResult Edit(int id, Scenario scenario)
        {
            try
            {
                unitOfWork.ScenarioRepository.Update(scenario);
                unitOfWork.Save();

                return RedirectToAction("_PartialGetScenarioGrid");
            }
            catch
            {
                return View();
            }
        }

        //
        // POST: /Scenario/Edit/5

        [HttpPost]
        public ActionResult _PartialEdit(int id, Scenario scenario)
        {
            try
            {
                unitOfWork.ScenarioRepository.Update(scenario);
                unitOfWork.Save();

                return Json(new
                {
                    scenario
                }, JsonRequestBehavior.AllowGet);
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
            Scenario scenario = unitOfWork.ScenarioRepository.GetByID(id);
            if (scenario != null)
            {
                unitOfWork.ScenarioRepository.Delete(id);
                unitOfWork.Save();
            }
            return RedirectToAction("_PartialGetScenarioGrid", "Scenario");
        }

        public ActionResult _PartialActionPlan()
        {
            return PartialView();
        }
        public ActionResult _PartialResources(int id)
        {
            var resourceList = unitOfWork.ResourceRepository.Get(s => s.Scenarios.Where(se => se.Id == id).Count() > 0);
            ViewBag.scenarioId = id;
            return PartialView(resourceList);
        }
        public ActionResult _PartialSubmitedWorks()
        {
            return PartialView();
        }
        public ActionResult _PartialFeedbacks()
        {
            return PartialView();
        }
        public ActionResult _PartialGetScenariosBySemester(int id) {

            var scenarioList = unitOfWork.ScenarioRepository.Get(s => s.Semesters.Where(se=>se.Id==id).Count()>0);
            ViewBag.semesterId = id;
            return PartialView(scenarioList);
        }

        [HttpPost]
        public ActionResult DeleteScenario(int scenarioId, int semesterId)
        {
            Scenario s = unitOfWork.ScenarioRepository.GetByID(scenarioId);
            unitOfWork.SemesterRepository.GetByID(semesterId).Scenarios.Remove(s);

            unitOfWork.Save();
            return RedirectToAction("_PartialGetScenariosBySemester", new { id = semesterId });
        }


        [HttpPost]
        public ActionResult DeleteResourceFromScenario(int resourceId, int scenarioId)
        {
            Resource s = unitOfWork.ResourceRepository.GetByID(resourceId);
            unitOfWork.ScenarioRepository.GetByID(scenarioId).Resources.Remove(s);

            unitOfWork.Save();
            return RedirectToAction("_PartialResources", "Scenario", new { id = scenarioId });
        }


        public ActionResult _PartialSelectResourcesByScenario(int id)
        {
            ViewBag.ID = id;
            var ResourceScenarioList = unitOfWork.ScenarioRepository.GetByID(id).Resources.ToList();
            var AllList = unitOfWork.ResourceRepository.Get();
            bool t = false;
            List<Resource> ResourceList = new List<Resource>();
            foreach (var listItem in AllList)
            {
                t = false;
                foreach (var ss in ResourceScenarioList)
                {
                    if (listItem.Id == ss.Id)
                    {
                        t = true;
                    }
                }
                if (!t)
                {
                    ResourceList.Add(listItem);
                }
            }
            ViewBag.AllResources = ResourceList;
            return PartialView();
        }

        public ActionResult AddResourcesToScenario(int ScenarioID, string[] ResourceMultiSelect)
        {
            var scenario = unitOfWork.ScenarioRepository.GetByID(ScenarioID);
            foreach (var item in ResourceMultiSelect)
            {
                var s = unitOfWork.ResourceRepository.GetByID(int.Parse(item));
                scenario.Resources.Add(s);
            }
            unitOfWork.Save();
            ViewBag.ID = ScenarioID;
            ViewBag.AllResources = unitOfWork.ResourceRepository.Get();
            return RedirectToAction("_PartialResources", "Scenario", new { id = ScenarioID });
        }
        #endregion Admin

        #region Student
        public ActionResult Group(int id, int GroupID){
            unitOfWork = new UnitOfWork();
            Scenario model = new Scenario();
            Group group = unitOfWork.GroupRepository.GetByID(GroupID);
            if (group != null && group.Scenarios.Where(s => s.Id == id).Count() > 0)
            {
                model = unitOfWork.GroupRepository.GetByID(GroupID).Scenarios.Where(s => s.Id == id).FirstOrDefault();
                ViewBag.SelecteGroupID = group.Id;
                ViewBag.SemesterID = group.SemesterID;
                ViewData["semester"] = group.Semester;
                ViewData["group"] = group;
            }
            else {
                return RedirectToAction("Index", "Home", null);
            }

            
            return View(model);
        }
        #endregion

        #region Ortak
        public ActionResult Detail(int id, int semesterId)
        {
            unitOfWork = new UnitOfWork();
            Scenario model = new Scenario();
            Semester semester = unitOfWork.SemesterRepository.GetByID(semesterId);
            
            return View(model);
        }
        #endregion
    }
}
