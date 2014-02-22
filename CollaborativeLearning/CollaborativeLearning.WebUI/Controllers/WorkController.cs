using CollaborativeLearning.DataAccess;
using CollaborativeLearning.Entities;
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

        public ActionResult _PartialWork(int id)
        {
            ViewBag.id = id;
            ViewBag.scenarioId = id;
            IEnumerable<Work> Works;
            Works = GetWork(id);
            //return Json(Works, JsonRequestBehavior.AllowGet);

            return PartialView(Works);
        }
        private IEnumerable<Work> GetWork(int? id)
        {
            IEnumerable<CollaborativeLearning.Entities.Work> Works;

            if (id.HasValue)
            {
                Works = unitOfWork.ScenarioRepository.GetByID(id).Works.OrderByDescending(t => t.RegDate);
            }
            else
            {
                Works = unitOfWork.WorkRepository.Get().OrderByDescending(t => t.RegDate);
            }

            return Works;
        }
        public ActionResult _PartialWorkCreate(int? scenarioId)
        {
            if (scenarioId != null)
                ViewBag.scenarioId = scenarioId;
            return PartialView();
        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _PartialWorkCreate(Work Work, int? scenarioId)
        {
            try
            {
                if (Work != null)
                {
                    Work.RegUserID = HelperController.GetCurrentUserId();
                    Work.RegDate = DateTime.Now;
                    unitOfWork.WorkRepository.Insert(Work);
                    unitOfWork.Save();

                    if (scenarioId != null)
                    {
                        unitOfWork = new UnitOfWork();
                        Scenario s = unitOfWork.ScenarioRepository.GetByID(TempData["scenarioId"]);
                        unitOfWork.WorkRepository.GetByID(Work.Id).Scenarios.Add(s);
                        unitOfWork.Save();
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
            unitOfWork.WorkRepository.Delete(id);
            unitOfWork.Save();
            if (scenarioId != null)
                return RedirectToAction("_PartialWork", new { id = scenarioId });
            else
                return RedirectToAction("Index", "Scenario");
        }


        [HttpGet]
        public ActionResult _PartialWorkUpdate(int id, int scenarioId)
        {
            if (id != null)
            {
                Work Work = unitOfWork.WorkRepository.GetByID(id);
                if (scenarioId != null)
                    ViewBag.scenarioId = scenarioId;
                Work.Description = WebUtility.HtmlDecode(Work.Description);
                return PartialView(Work);
            }
            return RedirectToAction("Index", "Scenario");
        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _PartialWorkUpdate(Work Work,int scenarioId)
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
                        return RedirectToAction("_PartialWork", new { id = scenarioId });
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

    }
}
