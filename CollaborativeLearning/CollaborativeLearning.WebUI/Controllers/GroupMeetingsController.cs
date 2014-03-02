using CollaborativeLearning.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollaborativeLearning.WebUI.Controllers
{
    public class GroupMeetingsController : Controller
    {
        //
        // GET: /GroupMeetings/

        public ActionResult Index()
        {
            Group currentGroup = null;
            return View();
        }

    }
}
