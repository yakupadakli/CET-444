using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollaborativeLearning.Entities;
using CollaborativeLearning.DataAccess;

namespace CollaborativeLearning.WebUI.Controllers
{
    public class GroupWorkController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _PartialSubmitWork(int id,int groupID) {
            unitOfWork = new UnitOfWork();
            WorkSemesterDueDate wsd = unitOfWork.WorkSemesterDueDateRepository.GetByID(id);
            GroupWork model = new GroupWork();
            if (wsd !=null)
            {
                model.WorkId = wsd.WorkID;
                model.GroupID = groupID;
            }
            return PartialView(model);
           
        }
    }
}
