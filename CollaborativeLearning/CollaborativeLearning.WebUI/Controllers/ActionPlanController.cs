using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollaborativeLearning.Entities;
using CollaborativeLearning.DataAccess;
using CollaborativeLearning.WebUI.Filters;
using System.Net;

namespace CollaborativeLearning.WebUI.Controllers
{
    public class ActionPlanController : Controller
    {

        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult _PartialActionPlan(int id)
        {
            ViewBag.id = id;
            ViewBag.scenarioId = id;
            IEnumerable<ActionPlan> ActionPlans;
            ActionPlans = GetActionPlan(id);

            return PartialView(ActionPlans);
        }
        private IEnumerable<ActionPlan> GetActionPlan(int? id)
        {
            IEnumerable<CollaborativeLearning.Entities.ActionPlan> actionPlans;

            if (id.HasValue)
            {
                actionPlans = unitOfWork.ScenarioRepository.GetByID(id).ActionPlans.OrderBy(t => t.OrderID);
            }
            else
            {
                actionPlans = unitOfWork.ActionPlanRepository.Get().OrderBy(t => t.OrderID);
            }

            return actionPlans;
        }
        public ActionResult _PartialActionPlanCreate(int? scenarioId)
        {
            if (scenarioId != null)
            {
                ViewBag.scenarioId = scenarioId;
                TempData["scenarioId"] = scenarioId;
            }
            return PartialView();
        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _PartialActionPlanCreate(ActionPlan ActionPlan, int? scenarioId)
        {
            try
            {
                if (ActionPlan != null)
                {
                    int orderId = 1;
                    if (scenarioId != null)
                    {
                        ActionPlan t = unitOfWork.ScenarioRepository.GetByID(scenarioId).ActionPlans.OrderByDescending(ta => ta.OrderID).FirstOrDefault();
                        if (t != null)
                            orderId = t.OrderID + 1;
                    }
                    ActionPlan.RegUserId = HelperController.GetCurrentUserId();
                    ActionPlan.RegDate = DateTime.Now;
                    ActionPlan.OrderID = orderId;
                    unitOfWork.ActionPlanRepository.Insert(ActionPlan);
                    unitOfWork.Save();

                    if (scenarioId != null)
                    {
                        unitOfWork = new UnitOfWork();
                        Scenario s = unitOfWork.ScenarioRepository.GetByID(TempData["scenarioId"]);
                        unitOfWork.ActionPlanRepository.GetByID(ActionPlan.Id).Scenarios.Add(s);
                        unitOfWork.Save();
                        return RedirectToAction("_PartialActionPlan", new { id = scenarioId });
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

        public ActionResult Delete(int id, int scenarioId)
        {
            if (scenarioId != null)
            {
                ActionPlan AP= unitOfWork.ActionPlanRepository.GetByID(id);

                IEnumerable<ActionPlan> nextAPs = unitOfWork.ScenarioRepository.GetByID(scenarioId).ActionPlans.Where(t => t.OrderID > AP.OrderID);
                foreach (var item in nextAPs)
                {
                    item.OrderID--;
                }

                unitOfWork.Save();
            }
            unitOfWork.ActionPlanRepository.Delete(id);
            unitOfWork.Save();

            if (scenarioId != null)
                return RedirectToAction("_PartialActionPlan", new { id = scenarioId });
            return RedirectToAction("Index", "Scenario");
        }


        [HttpGet]
        public ActionResult _PartialActionPlanUpdate(int id, int scenarioId)
        {
            if (id != null)
            {
                ActionPlan ActionPlan = unitOfWork.ActionPlanRepository.GetByID(id);
                if (scenarioId != null)
                {
                    ViewBag.scenarioId = scenarioId;
                    TempData["scenarioId"] = scenarioId;
                }
                ActionPlan.Content = WebUtility.HtmlDecode(ActionPlan.Content);
                return PartialView(ActionPlan);
            }
            return RedirectToAction("Index", "Scenario");
        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _PartialActionPlanUpdate(ActionPlan ActionPlan,int scenarioId)
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
                    if (scenarioId != null)
                        return RedirectToAction("_PartialActionPlan", new { id = scenarioId });
                    return RedirectToAction("Index", "Scenario");
                }
            }
            catch
            {
                return PartialView(ActionPlan);
            }
            return PartialView(ActionPlan);
        }

        public ActionResult ChangeActiveStatus(int id, string Active, int scenarioId)
        {
            unitOfWork = new UnitOfWork();
            if (Active == "True")
            {
                unitOfWork.ActionPlanRepository.GetByID(id).isActive = false;

            }
            else
            {
                unitOfWork.ActionPlanRepository.GetByID(id).isActive = true;

            }
            unitOfWork.Save();
            if (scenarioId != null)
                return RedirectToAction("_PartialActionPlan", new { id = scenarioId });
            else
                return RedirectToAction("Index", "Scenario");
        }

        public ActionResult ActionPlanDown(int id, int? scenarioId = null)
        {
            if (scenarioId != null)
            {
                ActionPlan lastAP = unitOfWork.ScenarioRepository.GetByID(scenarioId).ActionPlans.OrderByDescending(ta => ta.OrderID).FirstOrDefault();
                int lastOrderId = lastAP.OrderID;

                ActionPlan t1 = unitOfWork.ActionPlanRepository.GetByID(id);
                int oldId = t1.OrderID;
                if (oldId < lastOrderId)
                {
                    int newId = oldId + 1;
                    ActionPlan t2 = unitOfWork.ScenarioRepository.GetByID(scenarioId).ActionPlans.Where(tt => tt.OrderID == newId).FirstOrDefault();
                    t1.OrderID = newId;
                    t2.OrderID = oldId;
                    unitOfWork.Save();
                }
            }
            return RedirectToAction("_PartialActionPlan", new { id = scenarioId });
        }

        public ActionResult ActionPlanUp(int id, int? scenarioId = null)
        {
            if (scenarioId != null)
            {
                ActionPlan firstAC = unitOfWork.ScenarioRepository.GetByID(scenarioId).ActionPlans.OrderBy(ta => ta.OrderID).FirstOrDefault();
                int firstOrderId = firstAC.OrderID;

                ActionPlan t1 = unitOfWork.ActionPlanRepository.GetByID(id);
                int oldId = t1.OrderID;
                if (oldId > firstOrderId)
                {
                    int newId = oldId - 1;
                    ActionPlan t2 = unitOfWork.ScenarioRepository.GetByID(scenarioId).ActionPlans.Where(tt => tt.OrderID == newId).FirstOrDefault();
                    t1.OrderID = newId;
                    t2.OrderID = oldId;
                    unitOfWork.Save();
                }
            }
            return RedirectToAction("_PartialActionPlan", new { id = scenarioId });
        }
    }
}
