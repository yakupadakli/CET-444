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

        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult _PartialActionPlan(int id)
        {
            ViewBag.id = id;
            IEnumerable<ActionPlan> ActionPlans;
            ActionPlans = GetActionPlan(id);

            return PartialView(ActionPlans);
        }
        private IEnumerable<ActionPlan> GetActionPlan(int? id)
        {
            IEnumerable<CollaborativeLearning.Entities.ActionPlan> actionPlans;

            if (id.HasValue)
            {
                actionPlans = unitOfWork.ScenarioRepository.GetByID(id).ActionPlans.OrderByDescending(t => t.RegDate);
            }
            else
            {
                actionPlans = unitOfWork.ActionPlanRepository.Get().OrderByDescending(t => t.RegDate);
            }

            return actionPlans;
        }
        public ActionResult _PartialActionPlanCreate(int? scenarioId)
        {
            if (scenarioId != null)
                TempData["scenarioId"] = scenarioId;
            return PartialView();
        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        public ActionResult _PartialActionPlanCreate(ActionPlan ActionPlan, int? scenarioId)
        {
            try
            {
                if (ActionPlan != null)
                {
                    ActionPlan.RegUserId = HelperController.GetCurrentUserId();
                    ActionPlan.RegDate = DateTime.Now;
                    unitOfWork.ActionPlanRepository.Insert(ActionPlan);
                    unitOfWork.Save();

                    if (TempData["scenarioId"] != null)
                    {
                        unitOfWork = new UnitOfWork();
                        Scenario s = unitOfWork.ScenarioRepository.GetByID(TempData["scenarioId"]);
                        unitOfWork.ActionPlanRepository.GetByID(ActionPlan.Id).Scenarios.Add(s);
                        unitOfWork.Save();
                        return RedirectToAction("Index", "Scenario", new { id = TempData["scenarioId"] });
                    }
                    else
                        return RedirectToAction("Index", "Scenario");
                }
            }
            catch
            {
                return View();
            }
            return PartialView(ActionPlan);
        }

        //
        // GET: /ActionPlan/Delete/5

        public ActionResult Delete(int id)
        {
            unitOfWork.ActionPlanRepository.Delete(id);
            unitOfWork.Save();
            if (TempData["scenarioId"] != null)
                return RedirectToAction("Index", "Scenario", new { id = TempData["scenarioId"] });
            return RedirectToAction("Index", "Scenario");
        }


        [HttpGet]
        public ActionResult _PartialActionPlanUpdate(int id, int scenarioId)
        {
            if (id != null)
            {
                ActionPlan ActionPlan = unitOfWork.ActionPlanRepository.GetByID(id);
                if (scenarioId != null)
                    TempData["scenarioId"] = scenarioId;
                return PartialView(ActionPlan);
            }
            return RedirectToAction("Index", "Scenario");
        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        public ActionResult _PartialActionPlanUpdate(ActionPlan ActionPlan)
        {
            try
            {
                if (ActionPlan != null)
                {
                    ActionPlan t = unitOfWork.ActionPlanRepository.GetByID(ActionPlan.Id);

                    t.isActive = ActionPlan.isActive;
                    t.Content = ActionPlan.Content;

                    unitOfWork.ActionPlanRepository.Update(t);
                    unitOfWork.Save();
                    if (TempData["scenarioId"] != null)
                        return RedirectToAction("Index", "Scenario", new { id = TempData["scenarioId"] });
                    return RedirectToAction("Index", "Scenario");
                }
            }
            catch
            {
                return PartialView(ActionPlan);
            }
            return PartialView(ActionPlan);
        }
    }
}
