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

        //
        // GET: /Scenario/

        public ActionResult Index()
        {
            return View();
        }
        
        //

        public ActionResult _PartialGetScenarioGrid()
        {
            return PartialView();
        }


        //
        // GET: /Scenario/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Scenario/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Scenario/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
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
        // GET: /Scenario/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Scenario/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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
        // GET: /Scenario/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Scenario/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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



        public ActionResult Scenarios_Read([DataSourceRequest]DataSourceRequest request)
        {
            var results = from Ord in unitOfWork.ScenarioRepository.Get()
                          select new
                          {
                              Id = Ord.Id,
                              ShortDescription = Ord.ShortDescription,
                              Name = Ord.Name,
                              isActive = Ord.isActive,
                              //StudentCount = Ord.Semesters.Where(u => u.Role.RoleName == "Student").Count(),
                              //MentorCount = Ord.Users.Where(u => u.Role.RoleName == "Mentor").Count(),
                          };


            DataSourceResult result = results.ToList().ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Scenario_Create([DataSourceRequest] DataSourceRequest request, Scenario semesterModel)
        {
            
            return Scenarios_Read(request);
        }


        public ActionResult Scenario_Update([DataSourceRequest] DataSourceRequest request, Scenario semesterModel)
        {
           
            return Scenarios_Read(request);
        }


        public ActionResult Scenario_Destroy([DataSourceRequest] DataSourceRequest request, Scenario semesterModel)
        {
           
            return Scenarios_Read(request);
        }

    }
}
