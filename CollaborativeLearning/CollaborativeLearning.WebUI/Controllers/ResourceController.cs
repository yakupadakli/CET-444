using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CollaborativeLearning.Entities;
using CollaborativeLearning.DataAccess;
using CollaborativeLearning.WebUI.Filters;
using System.IO;
using System.IO.Compression;
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
        [ValidateInput(false)]
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

        public ActionResult DeleteFile(int id)
        {
            ResourceFile resourceFile = unitOfWork.ResourceFileRepository.GetByID(id);
            if (resourceFile != null)
            {
                var directoryPath = Path.Combine(Server.MapPath("~/Resources/"), resourceFile.FileUrl);
                FileInfo fi = new FileInfo(directoryPath);
                if (fi.Exists)
                {
                    fi.Delete();
                }
                unitOfWork = new UnitOfWork();
                unitOfWork.ResourceFileRepository.Delete(id);
                unitOfWork.Save();
            }
            return RedirectToAction("_PartialFileList", new { id = resourceFile.ResourceID });
        }

        public ActionResult Delete(int id)
        {
            try
            {

                unitOfWork = new UnitOfWork();
                Resource resource = unitOfWork.ResourceRepository.GetByID(id);
                if (resource.type != "Text" && resource.type != "Link")
                {
                    if (resource != null)
                    {
                        IEnumerable<ResourceFile> resourceFile = unitOfWork.ResourceFileRepository.Get(r => r.ResourceID == resource.Id);

                        if (resourceFile.Count() > 0)
                        {
                            string[] fileUrl = resourceFile.FirstOrDefault().FileUrl.Split('\\');
                            var directoryPath = Path.Combine(Server.MapPath("~/Resources/"), fileUrl[0]);
                            DirectoryInfo di = new DirectoryInfo(directoryPath);
                            if (di != null)
                            {
                                Directory.Delete(directoryPath, true);
                            }
                            unitOfWork = new UnitOfWork();
                            unitOfWork.ResourceRepository.Delete(id);
                            unitOfWork.Save();
                        }
                        else
                        {
                            var directoryPath = Path.Combine(Server.MapPath("~/Resources/"), resource.Id +"-"+resource.Name);
                            DirectoryInfo di = new DirectoryInfo(directoryPath);
                            if (di != null)
                            {
                                Directory.Delete(directoryPath, true);
                            }
                            unitOfWork = new UnitOfWork();
                            unitOfWork.ResourceRepository.Delete(id);
                            unitOfWork.Save();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The Resource cannot find to delete");
                        ViewBag.ErrorType = "ResourceAdd";
                        ViewBag.Message = "The Resource cannot find to delete";
                    }
                }
                else
                {
                    if (resource != null)
                    {
                        unitOfWork = new UnitOfWork();
                        unitOfWork.ResourceRepository.Delete(id);
                        unitOfWork.Save();
                    }

                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex);
                ViewBag.ErrorType = "ResourceAdd";
                ViewBag.Message = "The system error. It can be database connection problem or anythin about server-side! Please try again. If this situation continue, Ask your system administrator."
                    + " Detail Error: " + ex.Message;
            }

            return RedirectToAction("_PartialAddResource");
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
            m.Description= WebUtility.HtmlDecode(m.Description);
            if (m != null)
            {
                model = m;
                if (model.type.Contains("File"))
                {
                    string directoryPath = Path.Combine(Server.MapPath("~/Resources/"), model.Id.ToString() + "-" + model.Name);
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
                string urlPath = Path.Combine(Resourcemodel.Id.ToString() + "-" + Resourcemodel.Name);

                string directoryPath = Path.Combine(Server.MapPath("~/Resources/"), Resourcemodel.Id.ToString() + "-" + Resourcemodel.Name);
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
                        resourceFile.FileUrl = Path.Combine(urlPath, fileNameEncoded);
                        resourceFile.regDate = DateTime.Now;
                        resourceFile.regUserID = HelperController.GetCurrentUserId();
                        resourceFile.ResourceID = ResourceID;

                        unitOfWork = new UnitOfWork();
                        unitOfWork.ResourceFileRepository.Insert(resourceFile);
                        unitOfWork.Save();
                        file.SaveAs(fullPath);


                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex);
                        ViewBag.ErrorType = "ResourceAdd";
                        ViewBag.Message = "The system error. It can be database connection problem or anythin about server-side! Please try again. If this situation continue, Ask your system administrator."
                            + " Detail Error: " + ex.Message;
                    }
                }
            }
            return null;
        }

       
        #endregion

        #region Download Files
        public ActionResult DownloadAllResourceFiles(int id)
        {
            Resource resource = unitOfWork.ResourceRepository.GetByID(id);
            if (resource != null)
            {
                ResourceFile resourceFile = unitOfWork.ResourceFileRepository.Get(r => r.ResourceID == resource.Id).FirstOrDefault();
                if (resourceFile != null)
                {
                    string[] file = resourceFile.FileUrl.Split('\\');
                    var directoryPath = Path.Combine(Server.MapPath("~/Resources/"), file[0]);
                    var archivePath = Path.Combine(Server.MapPath("~/Resources/"), "Archive", resource.Name + ".zip");
                    FileInfo fi = new FileInfo(archivePath);
                    if (fi.Exists)
                    {
                        fi.Delete();
                    }
                    if (Directory.Exists(directoryPath))
                    {
                        ZipFile.CreateFromDirectory(directoryPath, archivePath);

                        return File(archivePath, "Application/x-zip-compressed", resource.Name + ".zip");
                    }
                    else
                    {
                        return null;
                    }


                }
            }
            return View();
        }

        #endregion

    }
}
