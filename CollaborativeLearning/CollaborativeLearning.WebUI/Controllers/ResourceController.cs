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
    public class ResourceController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index() {
           
           

            return View();
        }
        public ActionResult AddResource(Resource model)
        {
            if (model.Name!="" && model.type!="")
            {
                model.RegDate = DateTime.Now;
                model.RegUserID = HelperController.GetCurrentUserId();
                model.isActive = false;
                if (ModelState.IsValid)
                {
                    unitOfWork = new UnitOfWork();
                    unitOfWork.ResourceListRepository.Insert(model);
                    unitOfWork.Save();
                    return PartialView("_PartialAddResource", model);
                }
                else {

                    return PartialView("_PartialAddResource", model);
                }
            }
            return PartialView("_PartialAddResource",model);
        }
      
        public bool Create()
        {
            return true;
        }

        public ActionResult _PartialResourceList()
        { 
            ICollection<Resource> Model = unitOfWork.ResourceListRepository.Get().ToList();

            return PartialView(Model);
        }
    }
}
