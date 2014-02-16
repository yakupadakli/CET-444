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

        
        public ActionResult _PartialGetScenarioGrid()
        {
            return PartialView();
        }

        public ActionResult ScenarioAjaxHandler()
        {
            var results = from scenario in unitOfWork.ScenarioRepository.Get()
                          select new
                          {
                              Id = scenario.Id,
                              Name = scenario.Name,
                              ShortDescription = scenario.ShortDescription,
                              isActive = scenario.isActive,
                              ResourcesCount = scenario.Resources.Count(),
                              SemestersCount = scenario.Semesters.Count(),
                              Action = scenario.Id
                          };



            return Json(new {
                iTotalRecords = results.Count(),
                iTotalDisplayRecords = results.Count(),
                aaData = results
            }, JsonRequestBehavior.AllowGet);

        }

        //
        // GET: /Scenario/Details/5

        public ActionResult Index(int id)
        {
            ViewBag.scenarioId = id;
            return View();
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
                    scenario.regUserID = HelperController.GetCurrentUserId();
                    scenario.regDate = DateTime.Now;
                    unitOfWork.ScenarioRepository.Insert(scenario);
                    unitOfWork.Save();

                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
            return PartialView(scenario);
        }
        //
        // GET: /Scenario/Edit/5

        public ActionResult _PartialEdit(int id)
        {
            Scenario model = unitOfWork.ScenarioRepository.GetByID(id);
            return PartialView(model);
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
            return RedirectToAction("Index", "Home");
        }

        public ActionResult _PartialActionPlan()
        {
            return PartialView();
        }
        public ActionResult _PartialResources()
        {
            return PartialView();
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
            
            return PartialView(scenarioList);
        }
    }
}
