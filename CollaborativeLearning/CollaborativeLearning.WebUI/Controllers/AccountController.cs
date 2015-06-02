using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CollaborativeLearning.Entities;
using CollaborativeLearning.DataAccess;
using CollaborativeLearning.WebUI.Filters;
using System.Text.RegularExpressions;
using CollaborativeLearning.WebUI.Membership;
using System.Web.Helpers;
using CollaborativeLearning.WebUI.Controllers;
using CollaborativeLearning.WebUI.Models;


[InitializeSimpleMembership]

public class AccountController : Controller
{
    private UnitOfWork unitOfWork = new UnitOfWork();
    //
    // GET: /Account/LogOn

    public ActionResult Login()
    {
        return View();
    }
    public ActionResult ProfileSegments()
    {
        return PartialView();
    }
    //
    // POST: /Account/LogOn

    [HttpPost]
    public ActionResult Login(CollaborativeLearning.WebUI.Models.AccountModels.LoginModel model, string returnUrl)
    {
        if (ModelState.IsValid)
        {
            unitOfWork = new UnitOfWork();
            User user2 = unitOfWork.UserRepository.Get(u => u.Username == model.UserName).FirstOrDefault();
            if (user2 != null)
            {
                if (user2.IsLockedOut)
                {
                    ViewBag.lockout = "lockout";
                    return View(model);
                }
                if (!user2.IsApproved)
                {
                    ViewBag.failed = "Your account is not approved. Please contact your course instructor.";
                    return View(model);
                }

            }

            if (Membership.ValidateUser(model.UserName, model.Password))
            {

                User user = unitOfWork.UserRepository.Get(u => u.Username == model.UserName).FirstOrDefault();

                bool activeSemester = false;
                if (user.RoleID == unitOfWork.RoleRepository.Get(r => r.RoleName == "Instructor").FirstOrDefault().RoleId)
                    activeSemester = true;
                else
                {
                    foreach (var semester in user.Semesters)
                    {
                        if (semester.isActive)
                            activeSemester = true;
                    }
                }

                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {

                    if (user.Role.RoleName == "Instructor")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (user.Role.RoleName == "Mentor")
                    {
                        return RedirectToAction("Index", "Mentor");

                    }
                    else
                    {
                        return RedirectToAction("Index", "User");

                    }
                }

            }
            else
            {
                ViewBag.failed = "The user name or password provided is invalid.";
            }

        }

        // If we got this far, something failed, redisplay form
        return View(model);
    }

    //
    // GET: /Account/LogOff

    public ActionResult LogOff()
    {
        int userId = HelperController.GetCurrentUserId();
        User u = unitOfWork.UserRepository.GetByID(userId);
        u.LastLockoutDate = DateTime.UtcNow;
        unitOfWork.Save();
        FormsAuthentication.SignOut();

        return RedirectToAction("Index", "Home");
    }

    //
    // GET: /Account/Register

    public ActionResult Register()
    {
        return View();
    }
    public ActionResult _Register()
    {
        ViewData["roleName"] = new SelectList(Roles.GetAllRoles(), "roleName");
        if (ViewBag.failed != null)
            ViewBag.failed = ViewBag.failed;
        if (ViewBag.success != null)
            ViewBag.success = ViewBag.success;
        return PartialView();
    }
    //
    // POST: /Account/Register

    [HttpPost]   // Bu register kullanılıyor.
    public ActionResult Register(CollaborativeLearning.WebUI.Models.AccountModels.RegisterModel model, string registr_code)
    {
        if (ModelState.IsValid)
        {
            // Attempt to register the user
            try
            {
                Semester semester = null;
                int roleId = 0;
                if (registr_code != null || registr_code != "")
                {
                    semester = unitOfWork.SemesterRepository.Get(s => s.registerCode == registr_code).FirstOrDefault();
                    if (semester != null)
                    {
                        if (semester.isActive == false)
                        {
                            ViewBag.failed = "The semester you are trying to register is not active.";
                            return View(model);
                        }
                        else
                        {

                            if (model.StudentNumber == null || model.StudentNumber == "")
                            {
                                ViewBag.failed = "Please enter your student number.";
                                return View(model);
                            }
                            else
                                roleId = unitOfWork.RoleRepository.Get(r => r.RoleName == "Student").FirstOrDefault().RoleId;
                        }
                    }
                    else
                    {
                        semester = unitOfWork.SemesterRepository.Get(s => s.mentorRegisterCode == registr_code).FirstOrDefault();
                        if (semester != null)
                        {
                            roleId = unitOfWork.RoleRepository.Get(r => r.RoleName == "Mentor").FirstOrDefault().RoleId;
                        }
                        else
                        {
                            ViewBag.failed = "The registration code is not valid.";
                            return View(model);
                        }

                    }
                }

                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, model.FirsName, model.LastName, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    try
                    {
                        unitOfWork = new UnitOfWork();
                        Semester s = unitOfWork.SemesterRepository.GetByID(semester.Id);
                        User user = unitOfWork.UserRepository.Get(u => u.Email == model.Email & u.Username == model.UserName).FirstOrDefault();
                        string number = (Regex.Replace(model.PhoneNumber, "[^0-9]", ""));
                        user.PhoneNumber = "0" + number.Substring(2, number.Length - 2);
                        user.RoleID = roleId;
                        user.Gender = model.Gender;
                        user.StudentNo = model.StudentNumber;

                        user.Semesters.Add(s);
                        StudentCourseRequest scr = new StudentCourseRequest();
                        scr.SemesterId = s.Id;
                        scr.UserId = user.Id;
                        scr.isApproved = true;
                        scr.regDate = DateTime.Now;
                        scr.regUserID = user.Id;
                        scr.reqDate = DateTime.Now;
                        unitOfWork.StudentCourseRequestRepository.Insert(scr);
                        unitOfWork.Save();

                    }
                    catch
                    {
                        unitOfWork = new UnitOfWork();
                        User user = unitOfWork.UserRepository.Get(u => u.Email == model.Email & u.Username == model.UserName).FirstOrDefault();
                        if (user != null)
                        {
                            unitOfWork.UserRepository.Delete(user);
                            unitOfWork.Save();
                        }
                        ModelState.AddModelError("", ErrorCodeToString(MembershipCreateStatus.UserRejected));
                        return View(model);
                    }

                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }


            }
            catch (MembershipCreateUserException e)
            {
                ModelState.AddModelError("", ErrorCodeToString(MembershipCreateStatus.UserRejected));
            }
        }
        // If we got this far, something failed, redisplay form
        return View(model);

    }
    [HttpPost]
    public ActionResult _Register(CollaborativeLearning.WebUI.Models.AccountModels.RegisterModel model, string roleName)
    {
        if (ModelState.IsValid)
        {
            // Attempt to register the user
            try
            {
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    Roles.AddUserToRole(model.UserName, Request.Form["roleName"]);

                    User user = new User();
                    var u = unitOfWork.UserRepository.Get();
                    foreach (var item in u)
                    {
                        if (item.Email == model.Email)
                            user = item;
                    }

                    user.FirstName = model.FirsName;
                    user.LastName = model.LastName;
                    user.IsApproved = true;
                    unitOfWork.Save();

                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Setting", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }


            }
            catch (MembershipCreateUserException e)
            {
                ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
            }
        }
        // If we got this far, something failed, redisplay form
        return View(model);

        //if (ModelState.IsValid)
        //{
        //    // Attempt to register the user
        //    MembershipCreateStatus createStatus;
        //    Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

        //    if (createStatus == MembershipCreateStatus.Success)
        //    {
        //        FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
        //        return PartialView("Users","Admin");
        //        //return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", ErrorCodeToString(createStatus));
        //    }
        //}

        //// If we got this far, something failed, redisplay form
        //return PartialView(model);
    }
    //
    // GET: /Account/ChangePassword

    [Authorize]
    public ActionResult ChangePassword()
    {
        return View();
    }

    //
    // POST: /Account/ChangePassword

    [Authorize]
    [HttpPost]
    public ActionResult ChangePassword(CollaborativeLearning.WebUI.Models.AccountModels.ChangePasswordModel model)
    {
        if (ModelState.IsValid)
        {

            // ChangePassword will throw an exception rather
            // than return false in certain failure scenarios.
            bool changePasswordSucceeded;
            try
            {
                MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
            }
            catch (Exception)
            {
                changePasswordSucceeded = false;
            }

            if (changePasswordSucceeded)
            {
                return RedirectToAction("ChangePasswordSuccess");
            }
            else
            {
                ModelState.AddModelError("", "The current password provided is invalid.");
            }
        }

        // If we got this far, something failed, redisplay form
        return View(model);
    }
    [Authorize]
    [HttpPost]
    public ActionResult _ChangePassword(CollaborativeLearning.WebUI.Models.AccountModels.ChangePasswordModel model)
    {
        if (ModelState.IsValid)
        {

            // ChangePassword will throw an exception rather
            // than return false in certain failure scenarios.
            bool changePasswordSucceeded;
            try
            {
                MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
            }
            catch (Exception)
            {
                changePasswordSucceeded = false;
            }

            if (changePasswordSucceeded)
            {
                //return PartialView(model);
                //return View("Setting", "Home");
            }
            else
            {
                ModelState.AddModelError("", "The current password provided is invalid");
            }
        }

        // If we got this far, something failed, redisplay form
        return PartialView(model);
    }
    //
    // GET: /Account/ChangePasswordSuccess
    public ActionResult _ChangePassword()
    {
        return PartialView();
    }

    public ActionResult ChangePasswordSuccess()
    {
        return View();
    }

    public ActionResult ForgetPassword()
    {
        return View();
    }
    [HttpPost]
    public ActionResult ForgetPassword(CollaborativeLearning.WebUI.Models.ForgotPassword model)
    {
        if (ModelState.IsValid)
        {
            User user = unitOfWork.UserRepository.Get(u => u.Email == model.Email).FirstOrDefault();
            if (user != null)
                SendPasswordViaMail(user);
            else
            {
                TempData["failed"] = "The email address you entered does not belong to any account.";
                TempData["success"] = "";
            }
        }
        return View();
    }

    [Authorize]
    public ActionResult SendPasswordViaMail(User user)
    {
        if (user != null)
        {
            var encrypted = CollaborativeLearning.WebUI.Membership.Crypto.Generate128Characters();

            string baseUrl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);


            var passwordLink = baseUrl + "/Account/ResetPassword?digest=" + HttpUtility.UrlEncode(encrypted);
            var helpLink = baseUrl + "/Home/Contact";

            Boolean IsAuthenticated = WebSecurity.IsAuthenticated;

            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "cetyool@gmail.com";
                WebMail.Password = "Cl2Kb0Boun08!Ayoo";
                WebMail.SmtpPort = 587;

                string mesaj = "Recovering the password";

                mesaj += "<p> Hello " + user.FullName + " Somebody recently asked to reset your account password.</p>";
                mesaj += "<p><a href='" + passwordLink + "'> Click here to change your password.</a></p>";
                mesaj += "<p> If you didn't request a new password, let us know immediately.</p>";

                mesaj += "<p>CET YOOL</p>";
                mesaj += "<p><a href='" + helpLink + "'> Help</a></p>";

                user.PasswordVerificationToken = encrypted;
                user.PasswordVerificationTokenExpirationDate = DateTime.Now.AddHours(1);
                unitOfWork.Save();

                WebMail.Send(
                        user.Email,
                        "Somebody requested a new password for your CET YOOL account‏",
                        mesaj,
                        "cetyool@gmail.com"
                    );

                TempData["success"] = "A link which is valid for next one hour to reset your password has been sent.";

                TempData["failed"] = "";
                return RedirectToAction("Login", "Account");
            }
            catch (Exception e)
            {
                TempData["failed"] = "There was a problem sending the password reset link. Please try again.";
                TempData["success"] = "";

                return RedirectToAction("ForgetPassword", "Account");
            }
        }
        TempData["failed"] = "There was a problem sending the password reset link. Please try again.";
        TempData["success"] = "";

        return RedirectToAction("ForgetPassword", "Account");
    }

    [HttpGet]
    public ActionResult ResetPassword(string digest)
    {

        User u = unitOfWork.UserRepository.Get(user => user.PasswordVerificationToken == digest).FirstOrDefault();
        if (u != null)
        {
            if (u.PasswordVerificationTokenExpirationDate >= DateTime.Now)
            {
                ViewBag.Digest = digest;
            }
            return View();
        }
        return View();
    }

    [HttpPost]
    public ActionResult ResetPassword(CollaborativeLearning.WebUI.Models.ResetPasswordModel model, string digest)
    {
        if (model.NewPassword == null)
        {
            TempData["failed"] = "Please enter a new password.";
            return ResetPassword(digest);
        }

        if (model.ConfirmPassword == null)
        {
            TempData["failed"] = "Please confirm your new password.";
            return ResetPassword(digest);
        }
        try
        {
            ViewBag.Digest = digest;

            User u = unitOfWork.UserRepository.Get(user => user.PasswordVerificationToken == digest && user.PasswordVerificationTokenExpirationDate >= DateTime.Now).FirstOrDefault();
            if (u != null)
            {
                if (true)
                {
                    u.Password = CollaborativeLearning.WebUI.Membership.Crypto.HashPassword(model.NewPassword);
                    u.IsLockedOut = false;
                    u.PasswordVerificationToken = null;
                    u.PasswordVerificationTokenExpirationDate = null;
                    u.PasswordFailuresSinceLastSuccess = 0;
                    unitOfWork.UserRepository.Update(u);
                    unitOfWork.Save();

                    if (WebSecurity.IsAuthenticated)
                    {
                        FormsAuthentication.SignOut();
                    }

                    TempData["success"] = "Your password has been changed successfully.";

                    TempData["failed"] = "";

                    return RedirectToAction("Login");
                }
            }
            else
            {
                TempData["failed"] = "The password reset link is not valid or expired.";
                TempData["success"] = "";
                return ResetPassword(digest);
            }
        }
        catch
        {
        }

        TempData["failed"] = "There is an error while changing the password.";
        TempData["success"] = "";
        return ResetPassword(digest);
    }


    public ActionResult _PartialAllUsers()
    {
        var allUsers = unitOfWork.UserRepository.Get().ToList();

        return PartialView(allUsers);
    }

   
    public ActionResult _PartialEditUser(int userId)
    {
        ViewBag.Roles = unitOfWork.RoleRepository.Get().ToList();

        AccountModels.EditUserModel editUserModel = new AccountModels.EditUserModel()
        {
            ChangePasswordModel = new AccountModels.ChangePasswordModel()
            {
                UserId = userId,
                OldPassword = "justToMakeModelValid"
            },
            ChangeRoleModel = new AccountModels.ChangeRoleModel()
            {
                UserId = userId
            }
        };

        return PartialView(editUserModel);
    }

    [HttpPost]
    public ActionResult _PartialEditUser(AccountModels.EditUserModel editUserModel, string changePasswordSubmit)
    {
        bool editUserSucceeded = false;

        if (!String.IsNullOrEmpty(changePasswordSubmit))
        {
            if (ChangePasswordOfUser(editUserModel.ChangePasswordModel))
            {
                var allUsers = unitOfWork.UserRepository.Get().ToList();
                return PartialView("_PartialAllUsers", allUsers);
            }
        }
        else
        {
            if (ChangeRoleOfUser(editUserModel.ChangeRoleModel))
            {

                var allUsers = unitOfWork.UserRepository.Get().ToList();
                return PartialView("_PartialAllUsers", allUsers);
            }
        }


        ViewBag.Roles = unitOfWork.RoleRepository.Get().ToList();

        return PartialView(editUserModel);
    }

    public bool ChangePasswordOfUser(AccountModels.ChangePasswordModel changePasswordModel)
    {
        try
        {
            var user = unitOfWork.UserRepository.Get().SingleOrDefault(m => m.Id == changePasswordModel.UserId);
            user.Password = CollaborativeLearning.WebUI.Membership.Crypto.HashPassword(changePasswordModel.NewPassword);
            unitOfWork.UserRepository.Update(user);
            unitOfWork.Save();

            return true;
        }
        catch (Exception)
        {
        }

        return false;
    }

    public bool ChangeRoleOfUser(AccountModels.ChangeRoleModel changeRoleModel)
    {
        try
        {
            var user = unitOfWork.UserRepository.Get().SingleOrDefault(m => m.Id == changeRoleModel.UserId);
            user.RoleID = changeRoleModel.RoleId;
            unitOfWork.UserRepository.Update(user);
            unitOfWork.Save();

            return true;
        }
        catch (Exception)
        {
        }

        return false;
    }


    #region Status Codes
    private static string ErrorCodeToString(MembershipCreateStatus createStatus)
    {
        // See http://go.microsoft.com/fwlink/?LinkID=177550 for
        // a full list of status codes.
        switch (createStatus)
        {
            case MembershipCreateStatus.DuplicateUserName:
                return "User name already exists. Please enter a different user name.";

            case MembershipCreateStatus.DuplicateEmail:
                return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

            case MembershipCreateStatus.InvalidPassword:
                return "The password provided is invalid. Please enter a valid password value.";

            case MembershipCreateStatus.InvalidEmail:
                return "The e-mail address provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidAnswer:
                return "The password retrieval answer provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidQuestion:
                return "The password retrieval question provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidUserName:
                return "The user name provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.ProviderError:
                return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

            case MembershipCreateStatus.UserRejected:
                return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

            default:
                return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
        }
    }
    #endregion


}
