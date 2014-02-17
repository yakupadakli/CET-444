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

        public ActionResult ForgetPassword()
        {
            return View();
        }
        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult Login(CollaborativeLearning.WebUI.Models.AccountModels.LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
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
            return PartialView();
        }
        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(CollaborativeLearning.WebUI.Models.AccountModels.RegisterModel model,string registr_code)
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
                        semester = unitOfWork.SemesterRepository.Get(s => s.registerCode == registr_code && s.isActive == true).FirstOrDefault();
                        if (semester == null)
                        {
                            if (registr_code.IndexOf("!MN") != -1)
                            {
                                if (registr_code.Length > 2)
                                {
                                    registr_code = registr_code.Substring(0, registr_code.Length - 3);

                                    semester = unitOfWork.SemesterRepository.Get(s => s.registerCode == registr_code && s.isActive == true).FirstOrDefault();
                                    if(semester != null)
                                        roleId = unitOfWork.RoleRepository.Get(r => r.RoleName == "Mentor").FirstOrDefault().RoleId;
                                }

                            }
                        }
                        else
                            roleId = unitOfWork.RoleRepository.Get(r => r.RoleName == "Student").FirstOrDefault().RoleId;
                        
                        //Registration code girilmemişse geri gönderiyoruz. User Rejected Hata mesajı verilcek.
                        if (semester == null || roleId == 0)
                        {
                            ModelState.AddModelError("", ErrorCodeToString(MembershipCreateStatus.UserRejected));
                            return View(model);
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

                            user.Semesters.Add(s);

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
                    Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true,null,out createStatus);
                   
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
