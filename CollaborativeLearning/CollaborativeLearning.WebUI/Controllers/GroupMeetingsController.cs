using CollaborativeLearning.DataAccess;
using CollaborativeLearning.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollaborativeLearning.WebUI.Controllers
{
    public class GroupMeetingsController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();       
        public ActionResult Index(int SemesterID, int? groupId)
        {
            if (SemesterID != null)
                ViewBag.SemesterID = SemesterID;
            else
                ViewBag.SemesterID = 0;

            if (groupId != null)
                ViewBag.groupId = groupId;
            else
                ViewBag.groupId = 0;

            return View();
        }

        public ActionResult _AddShowMeetingNotes(int SemesterID, int groupId)
        {
            try
            {
                if (SemesterID != 0)
                {
                    Group group = null;
                    if (groupId == 0)
                    {
                        group = HelperController.GetCurrentUser().Groups.Where(g => g.SemesterID == SemesterID).FirstOrDefault();
                    }
                    else
                    {
                        group = unitOfWork.GroupRepository.GetByID(groupId);
                    }

                    if (group != null)
                    {
                        return PartialView(group);
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // Return add meeting note partial
        public ActionResult _AddNewMeeting(int groupId)
        {
            if (groupId != 0)
            {
                ViewBag.groupId = groupId;
                return PartialView();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _AddNewMeeting(MeetingNote model, int groupId, HttpPostedFileBase filePicture, HttpPostedFileBase fileRar)
        {
            Group group = unitOfWork.GroupRepository.GetByID(groupId);
            model.GroupID = groupId;
            model.regDate = DateTime.Now;
            model.regUserID = HelperController.GetCurrentUser().Id;

            if (ModelState.IsValid)
            {
                unitOfWork.MeetingNoteRepository.Insert(model);
                unitOfWork.Save();

                if (filePicture != null)
                {
                    string Folder = "MeetingPhotos";
                    SaveFile(model, filePicture, Folder);
                }

                if (fileRar != null)
                {
                    string Folder = "MeetingAttachment";
                    SaveFile(model, fileRar, Folder);
                }

                int roleId = HelperController.GetCurrentUser().RoleID;
                if (roleId == 3)
                {
                    groupId = 0;
                    return RedirectToAction("Index", "GroupMeetings", new { group.SemesterID });
                }
                else
                    return RedirectToAction("Index", "GroupMeetings", new { group.SemesterID, groupId });


            }
            return PartialView(model);
        }

        private void SaveFile(MeetingNote model, HttpPostedFileBase savedFile, string Folder)
        {
            unitOfWork = new UnitOfWork();
            MeetingNote meetingNote = unitOfWork.MeetingNoteRepository.GetByID(model.Id);
            string directoryPath = Path.Combine(Server.MapPath("~/"), "GroupMeetingsFiles", Folder, meetingNote.Group.Semester.semesterName);
            //string directoryPath = Path.Combine(Server.MapPath("~/Resources"));

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string[] file = savedFile.FileName.Split('.');
            string uzanti = file[1];
            var fileName = meetingNote.Id.ToString() + "." + uzanti;
            var fileNameEncoded = fileName.ToString();
            if (fileName != null)
            {
                var fullPath = Path.Combine(directoryPath, fileNameEncoded);
                try
                {

                    MeetingNoteFile meetingNoteFile = new MeetingNoteFile();
                    meetingNoteFile.MeetingNoteID = meetingNote.Id;
                    meetingNoteFile.FileName = fileNameEncoded;
                    meetingNoteFile.FileSize = savedFile.ContentLength;
                    meetingNoteFile.FileType = savedFile.ContentType;
                    meetingNoteFile.FileUrl = Path.Combine(Folder, meetingNote.Group.Semester.semesterName,fileNameEncoded);
                    meetingNoteFile.regDate = DateTime.Now;
                    meetingNoteFile.regUserID = HelperController.GetCurrentUserId();


                    unitOfWork = new UnitOfWork();
                    MeetingNoteFile mf = unitOfWork.MeetingNoteFileRepository.Get(m => m.MeetingNoteID == meetingNote.Id && m.FileName == fileNameEncoded).FirstOrDefault();
                    if (mf != null)
                        unitOfWork.MeetingNoteFileRepository.Delete(mf);
                    unitOfWork.MeetingNoteFileRepository.Insert(meetingNoteFile);
                    unitOfWork.Save();
                    savedFile.SaveAs(fullPath);


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

        public ActionResult _MeetingNoteUpdate(int id)
        {
            if (id != 0)
            {
                MeetingNote meetingNote = unitOfWork.MeetingNoteRepository.GetByID(id);
                return PartialView(meetingNote);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _MeetingNoteUpdate(MeetingNote model, HttpPostedFileBase filePictureUpdate, HttpPostedFileBase fileRarUpdate)
        {
            try
            {
                if (model != null)
                {
                    int roleId = HelperController.GetCurrentUser().RoleID;
                    if (HelperController.IsMemberOfTheGroup(model.GroupID) && roleId == 3)
                    {
                        Group group = unitOfWork.GroupRepository.GetByID(model.GroupID);
                        MeetingNote content = unitOfWork.MeetingNoteRepository.GetByID(model.Id);
                        content.regDate = DateTime.Now;
                        content.regUserID = HelperController.GetCurrentUser().Id;
                        content.Description = model.Description;
                        content.Name = model.Name;

                        if (ModelState.IsValid)
                        {
                            unitOfWork.MeetingNoteRepository.Update(content);
                            unitOfWork.Save();

                            if (filePictureUpdate != null)
                            {
                                string Folder = "MeetingPhotos";
                                SaveFile(model, filePictureUpdate, Folder);
                            }

                            if (fileRarUpdate != null)
                            {
                                string Folder = "MeetingAttachment";
                                SaveFile(model, fileRarUpdate, Folder);
                            }

                            return RedirectToAction("Index", "GroupMeetings", new { group.SemesterID });


                        }
                    }
                }
            }
            catch
            {

            }
            return PartialView(model);
        }

    }
}
