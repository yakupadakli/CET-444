using System;
using System.Linq;
using System.Web.Mvc;
using CollaborativeLearning.DataAccess;
using CollaborativeLearning.Entities;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Collections.Generic;


namespace CollaborativeLearning.WebUI.Controllers
{
    public class SemesterController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
      
        [Authorize]
        public ActionResult Index(int id)
        {
            if (id==0)
            {
                return RedirectToAction("Index", "Home");
            }
            Semester model = unitOfWork.SemesterRepository.GetByID(id);
            return View(model);
        }
    
        public ActionResult _PartialGetSemesterGrid()
        {

            ViewData["SemesterList"] = unitOfWork.SemesterRepository.Get().ToList();
            ViewBag.Message = "false";

            var Model = unitOfWork.SemesterRepository.Get().OrderBy(m => m.semesterName).ToList();

            return PartialView(Model);
        }
        public ActionResult _PartialGoToSemester()
        {
            return PartialView();   
        }
        public ActionResult SelectSemester(Semester m)
        {
            Semester model = unitOfWork.SemesterRepository.GetByID(m.Id);

            return RedirectToAction("Index", new {id=model.Id });
        }
              
        public ActionResult Details(int id)
        {
            return View();
        }
              

        public ActionResult Create()
        {


            return View();
        }

       [HttpPost]
        public ActionResult Create(Semester model)
        {
            ViewBag.Message = "true";
            Semester semester = new Semester();

            if (ModelState.IsValid)
            {
                try
                {
                    

                    if (unitOfWork.SemesterRepository.Get().Where(q => q.semester == model.semester && q.year == model.year).Count() == 0)
                    {
                        Semester semesterItem = new Semester();
                        semesterItem.year = model.year;
                        semesterItem.semester = model.semester;

                        string semesterCode = "cet" + semesterItem.year.ToString() + HelperController.GetRandomString(2);
                        semesterItem.registerCode = semesterCode;
                        semesterItem.mentorRegisterCode = "cet" + semesterItem.year.ToString() + "M"+HelperController.GetRandomString(2);
                        semesterItem.regUserID = HelperController.GetCurrentUserId();
                        semesterItem.regDate = DateTime.Today;
                        semesterItem.isActive = true;
                        unitOfWork.SemesterRepository.Insert(semesterItem);
                        unitOfWork.Save();
                        unitOfWork = new UnitOfWork();
                        unitOfWork.SemesterRepository.GetByID(semesterItem.Id).Users.Add(unitOfWork.UserRepository.GetByID(HelperController.GetCurrentUserId()));
                                   
                        unitOfWork.Save();

                        ViewBag.Message = "True";
                        ViewBag.MessageType = "SuccessAddSemester";
                        ViewBag.Alert = "This Semester is added.";
                       
                    }
                    else
                    {
                        model = null;
                        ViewBag.Message = "True";
                        ViewBag.MessageType = "FailureAddSemester";
                        ViewBag.Alert = "This Semester have beed added. You cannot dublicate.";
                      
                    }
                    
                    
                }
                catch
                {
                    model = null;
                    ViewBag.Message = "True";
                    ViewBag.MessageType = "FailureAddSemester";
                    ViewBag.Alert = "There is a porblem. Please try again.";
                    return PartialView("_PartialSemesterDataTable");

                }
            }
            return RedirectToAction("_PartialGetSemesterGrid");


        }

       
        public ActionResult _PartialCreate() {
                                      
            return PartialView();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

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

  
        public ActionResult Delete(int id)
        {
            Semester semester = unitOfWork.SemesterRepository.GetByID(id);
            if (semester != null)
            {
                unitOfWork.SemesterRepository.Delete(id);
                unitOfWork.Save();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SemesterAjaxHandler()
        {
            var allSemester = unitOfWork.SemesterRepository.Get();
            var semesterlist = from q in allSemester
                               select (new

                               {
                                   Id = q.Id,
                                   SemesterName = q.semesterName,
                                   Year = q.year,
                                   Semester = q.semester,
                                   Registration = q.registerCode,
                                   MentorCode = q.mentorRegisterCode,
                                   Group = q.Groups.Count(),
                                   Students = q.Users.Where(s => s.RoleID == 3).Count(),
                                   Mentors = q.Users.Where(s => s.RoleID == 2).Count(),
                                   Active = q.isActive,
                                   Action = ""
                               });

            var semesterlistOrdered = semesterlist.OrderBy(q => q.Year).ToList();
            return Json(new
            {
                iTotalRecords = allSemester.Count(),
                iTotalDisplayRecords = semesterlistOrdered.Count(),
                aaData = semesterlistOrdered
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _PartialSelectScenarios(int id) {
            ViewBag.ID = id;
            var ScenarioSemesterList = unitOfWork.SemesterRepository.GetByID(id).Scenarios.ToList();
            var AllList = unitOfWork.ScenarioRepository.Get();
            bool t = false;
            List<Scenario> ScenarioList = new List<Scenario>();
            foreach (var listItem in AllList)
            {
                t = false;
                foreach (var ss in ScenarioSemesterList)
                {
                    if (listItem.Id == ss.Id)
                    {
                        t = true;
                    }
                }
                if (!t)
                {
                    ScenarioList.Add(listItem);
                }
            }



            ViewBag.AllScenarios = ScenarioList;
            return PartialView();
        }
        public ActionResult AddSceneriosToSemester(int SemesterID, string[] ScenarioMultiSelect)
        {
            var semester = unitOfWork.SemesterRepository.GetByID(SemesterID);
            foreach (var item in ScenarioMultiSelect)
            {
                var s = unitOfWork.ScenarioRepository.GetByID(int.Parse(item));
                semester.Scenarios.Add(s);
            }
            unitOfWork.Save();
            ViewBag.ID = SemesterID;
            ViewBag.AllScenarios = unitOfWork.ScenarioRepository.Get();
            return RedirectToAction("_PartialGetScenariosBySemester", "Scenario", new { id = SemesterID });
        }

    }
}
