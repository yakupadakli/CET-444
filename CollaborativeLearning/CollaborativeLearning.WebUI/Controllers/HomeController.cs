using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CollaborativeLearning.DataAccess;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using CollaborativeLearning.WebUI.Filters;

namespace CollaborativeLearning.WebUI.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {

        private UnitOfWork unitOfWork = new UnitOfWork();

        [Authorize]
        public ActionResult Index()
        {
            IEnumerable<CollaborativeLearning.Entities.User> users;
            users = unitOfWork.UserRepository.Get().OrderBy(t => t.FirstName);
            return View(users);
        }
        public ActionResult Customers_Read([DataSourceRequest]DataSourceRequest request)
        {
            var results = from Ord in unitOfWork.UserRepository.Get()
                          select new
                          {
                              UserId = Ord.UserId,
                              FirstName = Ord.FirstName,
                              LastName = Ord.LastName
                          };

            DataSourceResult result = results.ToList().ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
