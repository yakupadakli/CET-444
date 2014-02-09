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
        public ActionResult Create(Semester collection)
        {
            Semester semester = new Semester();
            
            if (ModelState.IsValid)
            {
                try
                {
                    Semester semesterItem = new Semester();
                    semesterItem.year = collection.year;
                    semesterItem.semester = collection.semester;

                    string semesterCode = "cet" + collection.year.ToString();
                    semesterItem.registerCode = semesterCode;

                    semesterItem.regUserID = unitOfWork.UserRepository.Get(u => u.Username == WebSecurity.User.Identity.Name).First().Id;
                    semesterItem.regDate = DateTime.Today;
            

                    return View("SemesterSummaryPartial", semester);
                }
                catch
                {
                    return View();
                }
            }
            return View(collection);

           
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



        public ActionResult Semesters_Read([DataSourceRequest]DataSourceRequest request)
        {
            var results = from Ord in unitOfWork.SemesterRepository.Get()
                          select new
                          {
                              Id = Ord.Id,
                              semester = Ord.semester,
                              year = Ord.year,
                              semesterName = Ord.semesterName,
                              StudentCount = Ord.Users.Where(u => u.Role.RoleName == "Student").Count(),
                              MentorCount = Ord.Users.Where(u => u.Role.RoleName == "Mentor").Count(),
                          };


            DataSourceResult result = results.ToList().ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Semester_Create([DataSourceRequest] DataSourceRequest request, Semester semesterModel)
        {
            if (semesterModel.year != null && semesterModel.semester != null)
            {
                try
                {

                    Semester semesterItem = new Semester();
                    semesterItem.year = semesterModel.year;
                    semesterItem.semester = semesterModel.semester;

                    //string semesterCode = "cet" + semesterModel.year.ToString() + HelperController.GetRandomString(4);
                    string semesterCode = "cet" + semesterModel.year.ToString();
                    semesterItem.registerCode = semesterCode;

                    //semesterItem.regUserID = HelperController.GetCurrentUserId();
                    semesterItem.regUserID = 1;

                    semesterItem.regDate = DateTime.Today;


                    unitOfWork.SemesterRepository.Insert(semesterItem);
                    unitOfWork.Save();

                    unitOfWork = new UnitOfWork();
                    var u = unitOfWork.UserRepository.GetByID(10);
                    var s = unitOfWork.SemesterRepository.GetByID(semesterItem.Id);
                    s.Users.Add(u);
                    unitOfWork.Save();

                    ViewData["Succes"] = true;

                    semesterModel = s;
                    return Semesters_Read(request);
                }
                catch
                {
                    ViewData["Succes"] = false;
                    return View(semesterModel);
                }
            }
            ViewData["Succes"] = false;
            return Semesters_Read(request);
        }


        public ActionResult Semester_Update([DataSourceRequest] DataSourceRequest request, Semester semesterModel)
        {
            if (semesterModel.year != null && semesterModel.semester != null)
            {
                var semester = unitOfWork.SemesterRepository.GetByID(semesterModel.Id);
                semester.year = semesterModel.year;
                semester.semester = semesterModel.semester;
                unitOfWork.SemesterRepository.Update(semester);
                unitOfWork.Save();
            }
            return Semesters_Read(request);
        }


        public ActionResult Semester_Destroy([DataSourceRequest] DataSourceRequest request, Semester semesterModel)
        {
            if (semesterModel != null)
            {
                unitOfWork.SemesterRepository.Delete(semesterModel.Id);
            }
            return Semesters_Read(request);
        }

    }
}
