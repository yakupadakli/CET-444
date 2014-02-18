﻿using System;
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
        public ActionResult Index()
        {



            return View();
        }
        public ActionResult _PartialGetResourceGrid()
        {
            return PartialView("Index");
        }
        public ActionResult AddResource(Resource model, string ResourceText, string ResourceUrl, HttpFileCollection[] ResourceFiles)
        {
            bool regStatus = false;
            if (model.Name != "" && model.type != "")
            {
                model.RegDate = DateTime.Now;
                model.RegUserID = HelperController.GetCurrentUserId();
                model.isActive = false;
                if (model.type == "Text")
                {
                    model.Description = ResourceText;
                    regStatus = true;
                }
                else if (model.type == "URL")
                {
                    model.Description = ResourceUrl;
                    regStatus = true;
                }
                else if (model.type.Substring(0, 4) == "File")
                {
                    UploadFile(ResourceFiles);
                    regStatus = true;
                }
                else
                {
                    regStatus = false;
                    ViewBag.ErrorType = "ResourceAdd";
                    ViewBag.Message = "Your Resource content is confused. Please try again.";
                }
            }
            else
            {
                regStatus = false;
                ViewBag.ErrorType = "ResourceAdd";
                ViewBag.Message = "The resource name and type can not be empty. There is an error, please try again.";
            }
            if (regStatus)
            {
                try
                {
                    unitOfWork = new UnitOfWork();
                    unitOfWork.ResourceListRepository.Insert(model);
                    unitOfWork.Save();
                    ViewBag.ErrorType = "ResourceAdd";
                    ViewBag.Message = "Resource is added succesfully. But! You have to switch it's active status to use";
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorType = "ResourceAdd";
                    ViewBag.Message = "The system error. It can be database connection problem or anythin about server-side! Please try again. If this situation continue, Ask your system administrator."
                        + " Detail Error: " + ex.Message;

                }
            }
            else
            {
                ViewBag.ErrorType = "ResourceAdd";
                ViewBag.Message = "The resource content cannot be added. Please try again.";

            }
            return PartialView("_PartialAddResource", model);
        }

        private void UploadFile(HttpFileCollection[] ResourceFiles)
        {

        }

        public ActionResult ChangeActiveStatus(int id, string Active)
        {
            unitOfWork = new UnitOfWork();
            if (Active == "True")
            {
                unitOfWork.ResourceListRepository.GetByID(id).isActive = false;

            }
            else
            {
                unitOfWork.ResourceListRepository.GetByID(id).isActive = true;

            }
            unitOfWork.Save();
            return RedirectToAction("_PartialResourceList");
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
