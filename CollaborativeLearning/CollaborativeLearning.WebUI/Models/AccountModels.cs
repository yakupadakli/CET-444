using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollaborativeLearning.WebUI.Models
{
    public class AccountModels
    {
        public class ChangePasswordModel
        {

            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Please enter current password.")]
            [Display(Name = "Current Password")]
            public string OldPassword { get; set; }


            [StringLength(100, ErrorMessage = "{0} must contain at least {2} characters.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Please enter new password.")]
            [Display(Name = "New Password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm New Password")]
            [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "New password and Confirm password are not same.")]
            public string ConfirmPassword { get; set; }

            public int? UserId { get; set; }
        }

        public class ChangeRoleModel
        {
            public int UserId { get; set; }
            [Required]
            [Display(Name = "Role")]
            public int RoleId { get; set; }
        }

        public class EditUserModel
        {
            public ChangePasswordModel ChangePasswordModel { get; set; }
            public ChangeRoleModel ChangeRoleModel { get; set; }
        }

        public class LoginModel
        {
            [Required]
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember me")]
            public bool RememberMe { get; set; }
        }

        public class RegisterModel
        {
            [Required]
            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Password must be {0} {2} characters.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password Confirmation")]
            [System.Web.Mvc.Compare("Password", ErrorMessage = "Passwords do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Name")]
            public string FirsName { get; set; }

            [Display(Name = "Student Number (Required for students!)")]
            public string StudentNumber { get; set; }

            [Required]
            [Display(Name = "Surname")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Gender")]
            public String Gender { get; set; }

            [Required]
            [Display(Name = "Phone")]
            public String PhoneNumber { get; set; }      

        }

        public class ProfileModel
        {

            [Required]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Email")]
            public string Email { get; set; }



            [Required]
            [Display(Name = "Name")]
            public string FirsName { get; set; }

            [Required]
            [Display(Name = "Surname")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Gender")]
            public String Gender { get; set; }

            [Required]
            [Display(Name = "Phone")]
            public String PhoneNumber { get; set; }

        }

    }
}
