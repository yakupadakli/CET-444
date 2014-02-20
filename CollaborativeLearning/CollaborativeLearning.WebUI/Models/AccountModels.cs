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
            [Required(ErrorMessage = "Şu anki şifrenizi giriniz.")]
            [Display(Name = "Şu anki şifre")]
            public string OldPassword { get; set; }


            [StringLength(100, ErrorMessage = "{0} en az {2} karatkterden oluşmalı.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Yeni şifrenizi giriniz.")]
            [Display(Name = "Yeni şifre")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Yeni şifre tekrar")]
            [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "Şifreniz ve onaylama şifreniz birbiriyle uyuşmuyor.")]
            public string ConfirmPassword { get; set; }
        }

        public class LoginModel
        {
            [Required]
            [Display(Name = "Kullanıcı Adı")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Şifre")]
            public string Password { get; set; }

            [Display(Name = "Beni hatırla?")]
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
