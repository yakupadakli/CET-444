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
    public class FeedbackController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Feedback/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Feedback/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Feedback/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Feedback/Create

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
        // GET: /Feedback/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Feedback/Edit/5

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
        // GET: /Feedback/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Feedback/Delete/5

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
        public ActionResult _PartialGetGroupWorkFeedbacks(int id)
        {
            GroupWork work = unitOfWork.GroupWorkRepository.GetByID(id);
            return PartialView(work);
        }

        [HttpPost]
        public ActionResult _PartialGetGroupWorkFeedbacks(int id, int? parentId = null)
        {


                //id= groupwork id 



            GroupWork work = unitOfWork.GroupWorkRepository.GetByID(id);
            return PartialView(work);
        }

        public ActionResult _PartialGetGroupsBySemesterForFeedback(int id)
        {
            ViewBag.ID = id;

            ICollection<Group> groupList = unitOfWork.GroupRepository.Get(s => s.SemesterID == id).OrderByDescending(c => c.RegDate).ToList();
            return PartialView(groupList);
        }

        public ActionResult _PartialGetFeedbacksByGroupWorkId(int id)
        {
            Feedback feedback = unitOfWork.FeedbackRepository.GetByID(id);

            return PartialView(feedback.Feedbacks);
        }

        [HttpPost]
        public ActionResult AddFeedback(int id)
        {

                //id = feedback parentid   -----  groupwork id parent'ın groupwork id'si ile aynı

            return RedirectToAction("_PartialGetFeedbacksByGroupWorkId", "Feedback", new { id = id });
        }

    }
}
