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
        //
        // GET: /Semester/

        public ActionResult Index()
        {
            return View();
        }


        //

        public ActionResult _PartialGetSemesterGrid()
        {
            ViewBag.Message = "false";
            return PartialView();
        }


        //
        // GET: /Semester/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Semester/Create

        public ActionResult Create()
        {


            return View();
        }

        //
        // POST: /Semester/Create

        [HttpPost]
        public PartialViewResult _PartialCreate(Semester model)
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
                        ViewBag.Alert = "This Semester have beed added. You cannot dublicate.";
                        return PartialView("_PartialGetSemesterGrid");
                    }
                    else
                    {
                        model = null;
                        ViewBag.MessageType = "FailureAddSemester";
                        ViewBag.Alert = "This Semester have beed added. You cannot dublicate.";
                        return PartialView("_PartialGetSemesterGrid");

                    }
                    
                    
                }
                catch
                {
                    model = null;
                    ViewBag.MessageType = "FailureAddSemester";
                    ViewBag.Alert = "There is a porblem. Please try again.";
                    return PartialView("_PartialGetSemesterGrid");
                }
            }
            return PartialView();


        }
        [HttpGet]
        public ActionResult _PartialCreate() {
                                      
            return PartialView();
        }
        //
        // GET: /Semester/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Semester/Edit/5

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
        // GET: /Semester/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Semester/Delete/5

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


    }
}
