using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollaborativeLearning.Entities;
using CollaborativeLearning.DataAccess;
using CollaborativeLearning.WebUI.Filters;
using System.IO;
namespace CollaborativeLearning.WebUI.Controllers
{
    public class ResourceController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _PartialAddResource()
        {
            return PartialView("_PartialAddResource");
        }
        public ActionResult _PartialGetResourceGrid()
        {
            return PartialView("Index");
        }
        public ActionResult AddResource(Resource model, string ResourceText, string ResourceUrl, String FileUpload)
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
                else if (model.type.Contains("File"))
                {
                    model.Description = "Files";
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
                    model.RegDate = DateTime.Now;
                    model.RegUserID = HelperController.GetCurrentUserId();
                    unitOfWork = new UnitOfWork();
                    unitOfWork.ResourceRepository.Insert(model);
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
            if (model.type.Contains("File") && FileUpload =="Now")
            {
                ViewBag.ResourceID = model.Id;
                return RedirectToAction("_PartialEditResource", new {id= model.Id });

            }
            else
            {
                return RedirectToAction("_PartialAddResource");
            }
            
        }

      

        public ActionResult Delete(int id)
        {
            Resource re = unitOfWork.ResourceRepository.GetByID(id);

            if (re != null)
            {
                try
                {
                    unitOfWork = new UnitOfWork();
                    unitOfWork.ResourceRepository.Delete(id);
                    unitOfWork.Save();
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", ex);
                    ViewBag.ErrorType = "ResourceAdd";
                    ViewBag.Message = "The system error. It can be database connection problem or anythin about server-side! Please try again. If this situation continue, Ask your system administrator."
                        + " Detail Error: " + ex.Message;
                };
            }
            else
            {
                ModelState.AddModelError("", "The Resource cannot find to delete");
                ViewBag.ErrorType = "ResourceAdd";
                ViewBag.Message = "The Resource cannot find to delete";
            }
            return RedirectToAction("_PartialResourceList");
        }
        public ActionResult ChangeActiveStatus(int id, string Active)
        {
            unitOfWork = new UnitOfWork();
            if (Active == "True")
            {
                unitOfWork.ResourceRepository.GetByID(id).isActive = false;

            }
            else
            {
                unitOfWork.ResourceRepository.GetByID(id).isActive = true;

            }
            unitOfWork.Save();
            return RedirectToAction("_PartialResourceList");
        }

        public ActionResult _PartialResourceList()
        {
            var Model = unitOfWork.ResourceRepository.Get().OrderBy(m => m.Name).ToList();

            return PartialView(Model);
        }
        public ActionResult _PartialEditResource(int id)
        {
            Resource model = new Resource();
            Resource m = unitOfWork.ResourceRepository.GetByID(id);
            if (m != null)
            {
                model = m;
                if (model.type.Contains("File"))
                {
                    string directoryPath = Path.Combine(Server.MapPath("~/Resources/"), model.Id.ToString() + "-" + model.Name + "-" + DateTime.Today.ToShortDateString());
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                }
                
                List<SelectListItem> ResourceCategory = GetResourceTypesSelectList();
                ViewBag.ResourceCategory = ResourceCategory;
                return PartialView("_PartialEditResource", model);

            }
            else
            {
                return null;
            }
        }

        private static List<SelectListItem> GetResourceTypesSelectList()
        {
            List<SelectListItem> ResourceCategory = new List<SelectListItem>();
            ResourceCategory.Add(new SelectListItem { Text = "Select ResourceType", Value = "" });
            ResourceCategory.Add(new SelectListItem { Text = "Text", Value = "Text" });
            ResourceCategory.Add(new SelectListItem { Text = "Link", Value = "URL" });
            ResourceCategory.Add(new SelectListItem { Text = "File/PDF", Value = "File/PDF" });
            ResourceCategory.Add(new SelectListItem { Text = "File/Zip", Value = "File/Zip" });
            ResourceCategory.Add(new SelectListItem { Text = "File/Document", Value = "File/Document" });
            ResourceCategory.Add(new SelectListItem { Text = "File/Other", Value = "File/Other" });
            ResourceCategory.Add(new SelectListItem { Text = "Other", Value = "Other" });
            return ResourceCategory;
        }

        public ActionResult Edit(Resource model)
        {
            unitOfWork = new UnitOfWork();
            Resource resource = unitOfWork.ResourceRepository.GetByID(model.Id);
            if (resource != null)
            {

                resource.Name = model.Name;
                resource.Description = model.Description;
                if (model.type.Contains("File"))
                {
                    resource.Description = model.type;
                }
                
                try
                {
                    unitOfWork.ResourceRepository.Update(resource);
                    unitOfWork.Save();
                    ViewBag.ErrorType = "ResourceEdit";
                    ViewBag.Message = "Resource is updated succesfully. But! You have to switch it's active status to use";
                }
                catch (Exception ex)
                {

                    ViewBag.ErrorType = "ResourceEdit";
                    ViewBag.Message = "The system error. It can be database connection problem or anythin about server-side! Please try again. If this situation continue, Ask your system administrator."
                        + " Detail Error: " + ex.Message;
                }
            }
            else {
                ModelState.AddModelError("", "The Resource cannot find to update");
                ViewBag.ErrorType = "ResourceEdit";
                ViewBag.Message = "The Resource cannot find to update";
            }
            return RedirectToAction("_PartialEditResource", new { id = model.Id });

        }

        public ActionResult _PartialFileList(int id) { 
            unitOfWork = new UnitOfWork();
            ICollection<ResourceFile> model = unitOfWork.ResourceRepository.GetByID(id).ResourceFiles.ToList();
            if (model!=null)
            {
                return PartialView(model);
            }
            return null;
        }

        #region ResourceUpload
        public ActionResult _PartialFileUploadToResource(int id)
        {
            ViewBag.ResourceID = id;
            ViewBag.ResourceName = "Resoruce";
            return View();
        }
        public ActionResult UploadHandler(HttpPostedFileBase file, int ResourceID)
        {
            if (file!=null)
            {
                unitOfWork = new UnitOfWork();
                Resource Resourcemodel = unitOfWork.ResourceRepository.GetByID(ResourceID);
                string directoryPath = Path.Combine(Server.MapPath("~/Resources/"), Resourcemodel.Id.ToString() + "-" + Resourcemodel.Name + "-" + DateTime.Today.ToShortDateString());
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                var fileName = Path.GetFileName(file.FileName);
                var fileNameEncoded = HttpUtility.HtmlEncode(fileName);
                var exten = Path.GetExtension(fileNameEncoded);
                if (HelperController.MimeOk(exten))
                {
                    var fullPath = Path.Combine(directoryPath, fileNameEncoded);
                    try
                    {
                        ResourceFile resourceFile = new ResourceFile();
                        resourceFile.FileName = fileName;
                        resourceFile.FileSize = file.ContentLength;
                        resourceFile.FileType = exten;
                        resourceFile.FileUrl = fullPath;
                        resourceFile.regDate = DateTime.Now;
                        resourceFile.regUserID = HelperController.GetCurrentUserId();
                        resourceFile.ResourceID = ResourceID;

                        unitOfWork = new UnitOfWork();
                        unitOfWork.ResourceFileRepository.Insert(resourceFile);
                        unitOfWork.Save();
                        file.SaveAs(fullPath);


                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            return null;
        }

       
        #endregion

    }
}
