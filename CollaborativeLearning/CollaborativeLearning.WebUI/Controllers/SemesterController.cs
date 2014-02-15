using System;
using System.Linq;
using System.Web.Mvc;
using CollaborativeLearning.DataAccess;
using CollaborativeLearning.Entities;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;


namespace CollaborativeLearning.WebUI.Controllers
{
    public class SemesterController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
      
        [Authorize]
        public ActionResult Index(Semester m)
        {
            if (m.Id==0)
            {
                return View("Dashboard");
            }
            Semester model = unitOfWork.SemesterRepository.GetByID(m.Id);
            return View(model);
        }
    
        public ActionResult _PartialGetSemesterGrid()
        {
            ViewData["SemesterList"] = unitOfWork.SemesterRepository.Get().ToList();
            ViewBag.Message = "false";
            return PartialView();
        }
        public ActionResult _PartialGoToSemester()
        {
            return PartialView();   
        }
        public ActionResult SelectSemester(Semester m)
        {
            Semester model = unitOfWork.SemesterRepository.GetByID(m.Id);
            
           return RedirectToAction("Index", model);
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

                        semesterItem.regUserID = HelperController.GetCurrentUserId();
                        semesterItem.regDate = DateTime.Today;
                       
                        semesterItem.Users.Add(unitOfWork.UserRepository.GetByID(HelperController.GetCurrentUserId()));
                        unitOfWork.SemesterRepository.Insert(semesterItem);
                        unitOfWork.Save();


                        ViewBag.MessageType = "SuccessAddSemester";
                        ViewBag.Alert = "This Semester is added.";
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        model = null;
                        ViewBag.MessageType = "FailureAddSemester";
                        ViewBag.Alert = "This Semester have beed added. You cannot dublicate.";
                        return RedirectToAction("Index","Home");

                    }
                    
                    
                }
                catch
                {
                    model = null;
                    ViewBag.MessageType = "FailureAddSemester";
                    ViewBag.Alert = "There is a porblem. Please try again.";
                    return RedirectToAction("Index", "Home");
                }
            }
            return PartialView();


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
            return View();
        }

  
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
                                   Group = q.Groups.Count(),
                                   Students = q.Users.Where(s => s.RoleID == 3).Count(),
                                   Mentors = q.Users.Where(s => s.RoleID == 2).Count(),
                                   Active = q.isActive,
                                   Action = ""
                               });

            return Json(new
            {
                iTotalRecords = allSemester.Count(),
                iTotalDisplayRecords = semesterlist.Count(),
                aaData = semesterlist
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _PartialSelectScenarios(int id) {
            ViewBag.ID = id;
            var s = unitOfWork.ScenarioRepository.Get();
            ViewBag.AllScenarios = s;
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
            return PartialView();
        }

    }
}
