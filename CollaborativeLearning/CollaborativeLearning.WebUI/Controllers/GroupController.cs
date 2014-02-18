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
    public class GroupController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /GroupBySemester

        public ActionResult _PartialGetGroupsBySemester(int id)
        {
            ViewBag.ID = id;

            var groupList = unitOfWork.GroupRepository.Get(s => s.semesterID == id);
            return PartialView(groupList);
        }

        public ActionResult _PartialAddGroup(int id)
        {
            ViewBag.ID = id;
            return PartialView();
        }
        public ActionResult AddGroupsToSemester(int Id, string groupName)
        {
            var semester = unitOfWork.SemesterRepository.GetByID(Id);

            Group group = new Group
            {
                groupName=groupName,
                regDate = DateTime.UtcNow,
                regUserID = HelperController.GetCurrentUserId(),
                semesterID = Id
            };
            
            unitOfWork.GroupRepository.Insert(group);

            unitOfWork.Save();
            ViewBag.ID = Id;
            ViewBag.AllGroups = unitOfWork.GroupRepository.Get();
            return RedirectToAction("_PartialGetGroupsBySemester", "Group", new { id = Id });
        }
        public ActionResult Delete(int id)
        {
            int semesterId = unitOfWork.GroupRepository.GetByID(id).semesterID;
            unitOfWork.GroupRepository.Delete(id);
            unitOfWork.Save();
            ViewBag.ID = semesterId;
            return RedirectToAction("_PartialGetGroupsBySemester", "Group", new { id = semesterId });
        }

        public ActionResult _PartialGetGroupsUsers(int id,int semesterId)
        {
            IEnumerable<User> users = unitOfWork.UserRepository.Get(u => u.RoleID == 3);
            List<User> userlist = new List<Entities.User>();
            foreach (var item in users)
            {
                if (item.Semesters.Contains(unitOfWork.SemesterRepository.GetByID(semesterId)))
                {
                    Group g = item.Groups.FirstOrDefault();
                    if (g == null)
                    {
                        userlist.Add(item);
                    }
                }
            }
            ViewBag.AllUsers = userlist;

            var groupUserList = unitOfWork.GroupRepository.Get(s => s.Id == id).FirstOrDefault().Users.Where(u => u.RoleID == 3).ToList();

            ViewBag.ID = id;
            ViewBag.groupId = id;
            return PartialView(groupUserList);
        }

        public ActionResult _PartialGetGroupsMentors(int id, int semesterId)
        {
            IEnumerable<User> users = unitOfWork.UserRepository.Get(u => u.RoleID == 2);
            List<User> mentorlist = new List<Entities.User>();

            foreach (var item in users)
            {
                if (item.Semesters.Contains(unitOfWork.SemesterRepository.GetByID(semesterId)))
                {
                    mentorlist.Add(item);
                }
            }
            ViewBag.AllMentors = mentorlist;

            var groupMentorList = unitOfWork.GroupRepository.Get(s => s.Id == id).FirstOrDefault().Users.Where(u=>u.RoleID==2).ToList();

            ViewBag.ID = id;
            ViewBag.groupId = id;
            return PartialView(groupMentorList);
        }

        [HttpPost]
        public ActionResult _UserToGroup(int id, string[] UserMultiSelect)
        {
            var group = unitOfWork.GroupRepository.GetByID(id);
            foreach (var item in UserMultiSelect)
            {
                var u = unitOfWork.UserRepository.GetByID(int.Parse(item));
                group.Users.Add(u);
            }
            unitOfWork.Save();
            ViewBag.AllGroups = unitOfWork.GroupRepository.Get();
            return RedirectToAction("_PartialGetGroupsBySemester", "Group", new { id = group.semesterID});
        }

        [HttpPost]
        public ActionResult DeleteUserFromGroup(int userId, string groupId)
        {
            Group group = unitOfWork.GroupRepository.GetByID(Convert.ToInt32(groupId));
            group.Users.Remove(unitOfWork.UserRepository.GetByID(userId));
            unitOfWork.Save();
            return RedirectToAction("_PartialGetGroupsBySemester", "Group", new { id = group.semesterID });
        }
        [HttpPost]
        public void UpdateGroupName(int id, string text)
        {
            Group group = unitOfWork.GroupRepository.GetByID(id);
            group.groupName = text;
            unitOfWork.Save();
            //return RedirectToAction("_PartialGetGroupsBySemester", "Group", new { id = group.semesterID });
        }

        [HttpPost]
        public ActionResult DeleteGroup(int groupId)
        {
            Group group = unitOfWork.GroupRepository.GetByID(groupId);
            int semesterID = group.semesterID;

            unitOfWork.GroupRepository.Delete(groupId);
            unitOfWork.Save();
            return RedirectToAction("_PartialGetGroupsBySemester", "Group", new { id = semesterID });
        }

    }
}
