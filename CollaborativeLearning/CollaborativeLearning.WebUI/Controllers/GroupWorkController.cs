using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollaborativeLearning.Entities;
using CollaborativeLearning.DataAccess;
using System.IO;
using System.IO.Compression;
namespace CollaborativeLearning.WebUI.Controllers
{
    public class GroupWorkController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _PartialSubmitWork(int id, int groupID)
        {
            unitOfWork = new UnitOfWork();
            WorkSemesterDueDate wsd = unitOfWork.WorkSemesterDueDateRepository.GetByID(id);
            GroupWork model = new GroupWork();


            if (wsd != null)
            {
                if (unitOfWork.GroupWorkRepository.Get(gw => gw.GroupID == groupID && gw.WorkId == wsd.WorkID).Count() > 0)
                {
                    model = unitOfWork.GroupWorkRepository.Get(gw => gw.GroupID == groupID && gw.WorkId == wsd.WorkID).FirstOrDefault();
                }
                else
                {
                    model.WorkId = wsd.WorkID;
                    model.Work = wsd.Work;
                    model.GroupID = groupID;
                    model.Groups = unitOfWork.GroupRepository.GetByID(groupID);
                }

            }
            return PartialView(model);

        }
        public ActionResult GroupWorkSubmitPost(GroupWork model,int id)
        {
            unitOfWork = new UnitOfWork();
            if (unitOfWork.GroupWorkRepository.Get(g => g.GroupID == model.GroupID && g.WorkId == model.WorkId).Count() > 0)
            {
                if (UpdateGroupWork(model))
                {
                    return JavaScript("GroupWorkSubmitSuccess()");
                }
                else
                {
                    return JavaScript("GroupWorkSubmitFail()");
                }
            }
            else
            {

                if (AddGroupWork(model))
                {
                    return JavaScript("GroupWorkSubmitSuccess()");
                }
                else
                {
                    return JavaScript("GroupWorkSubmitFail()");
                }

            }
        }
        public ActionResult GroupWorkSubmitFile(GroupWork modelInitial, HttpPostedFileBase file)
        {
            unitOfWork = new UnitOfWork();
            bool saveFile = false;
            if (unitOfWork.GroupWorkRepository.Get(g => g.GroupID == modelInitial.GroupID && g.WorkId == modelInitial.WorkId).Count() > 0)
            {
                saveFile = true;
            }
            else
            {
                saveFile = AddGroupWork(modelInitial);
            }
            if (saveFile)
            {
                unitOfWork = new UnitOfWork();
                GroupWork model = unitOfWork.GroupWorkRepository.Get(m => m.WorkId == modelInitial.WorkId && m.GroupID == modelInitial.GroupID).FirstOrDefault();
                string directoryPath = Path.Combine(Server.MapPath("~/GroupWorks/"), model.Groups.GroupName, model.Work.Name);
                string urlPath = Path.Combine(model.Groups.GroupName, model.Work.Name);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                var fileName = Path.GetFileName(file.FileName);
                var fileNameEncoded = model.Groups.GroupName + "_" + model.Work.Name + "_" + HttpUtility.HtmlEncode(fileName);
                var exten = Path.GetExtension(fileNameEncoded);
                if (HelperController.MimeOk(exten))
                {
                    var fullPath = Path.Combine(directoryPath, fileNameEncoded);
                    try
                    {
                        GroupWorkFile groupFile = new GroupWorkFile();
                        groupFile.FileName = fileNameEncoded;
                        groupFile.FileSize = file.ContentLength;
                        groupFile.FileType = file.ContentType;
                        groupFile.FileUrl = Path.Combine(urlPath, fileNameEncoded);
                        groupFile.regDate = DateTime.Now;
                        groupFile.regUserID = HelperController.GetCurrentUserId();
                        groupFile.GroupWorkID = model.Id;
                        unitOfWork = new UnitOfWork();
                        FileInfo fi = new FileInfo(fullPath);
                        if (fi.Exists)
                        {
                            fi.Delete();
                            var egwf = unitOfWork.GroupWorkFileRepository.Get(s => s.FileUrl == groupFile.FileUrl).FirstOrDefault();
                            egwf = groupFile;
                            unitOfWork.Save();
                        }
                        else { 
                            unitOfWork.GroupWorkFileRepository.Insert(groupFile);
                            unitOfWork.Save();
                        }
                        file.SaveAs(fullPath);
                        return JavaScript("GroupWorkFileUploadSubmit()");

                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex);
                        ViewBag.ErrorType = "GroupWorkFileUpload";
                        ViewBag.Message = "The system error. It can be database connection problem or anythin about server-side! Please try again. If this situation continue, Ask your system administrator."
                            + " Detail Error: " + ex.Message;
                        return JavaScript("GroupWorkFileUploadFail()");
                    }


                }
            }
            return JavaScript("GroupWorkFileUploadFail()");
        }

        public ActionResult DeleteAllFiles(int id)
        {
            unitOfWork = new UnitOfWork();
            GroupWork gw = unitOfWork.GroupWorkRepository.GetByID(id);
            User std = HelperController.GetCurrentUser();
            if (std.Groups.Where(g => g.Id == gw.GroupID).Count() > 0)
            {
                foreach (var item in gw.GroupWorkFiles)
                {
                    DeleteFile(item);
                }
               
            }
            if (gw.GroupWorkFiles ==null || gw.GroupWorkFiles.Count()==0)
            {
                if (gw.Content==null || gw.Content=="")
                {
                    unitOfWork.GroupWorkRepository.Delete(gw);
                    unitOfWork.Save();
                }
            }
            return JavaScript("ResetSuccess()");            

        }
        private bool AddGroupWork(GroupWork model)
        {
            unitOfWork = new UnitOfWork();
            model.regDate = DateTime.Now;
            model.SubmissionDate = DateTime.Now;
            model.regUserID = HelperController.GetCurrentUserId();

            try
            {
                unitOfWork.GroupWorkRepository.Insert(model);
                unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }

        }
        [ValidateInput(false)]
        private bool UpdateGroupWork(GroupWork model)
        {
            unitOfWork = new UnitOfWork();
            var m = unitOfWork.GroupWorkRepository.GetByID(model.Id);
            if (model.Content==null ||  model.Content=="")
            {
                if (m.GroupWorkFiles == null || m.GroupWorkFiles.Count() == 0)
                {
                    unitOfWork.GroupWorkRepository.Delete(unitOfWork.GroupWorkRepository.GetByID(model.Id));
                    unitOfWork.Save();
                    return true;
                }
            }
            if (ModelState.IsValid)
            {
                var gw = unitOfWork.GroupWorkRepository.GetByID(model.Id);
                gw.Content = model.Content;
                gw.SubmissionDate = DateTime.Now;
                gw.regUserID = HelperController.GetCurrentUserId();
                try
                {
                    unitOfWork.Save();
                    return true;
                }
                catch (Exception e)
                {

                    return false;
                }
            }
            return false;
        }

        private bool DeleteFile(GroupWorkFile model) {
            try
            {
                var directoryPath = Path.Combine(Server.MapPath("~/GroupWorks/"), model.FileUrl);
                FileInfo fi = new FileInfo(directoryPath);
                if (fi.Exists)
                {
                    fi.Delete();
                    unitOfWork = new UnitOfWork();
                    unitOfWork.GroupWorkFileRepository.Delete(model.Id);
                    unitOfWork.Save();
                    return true;
                }
                return false;
                
                
            }
            catch (Exception e)
            {

                return false;
            }
        }
    }
}
