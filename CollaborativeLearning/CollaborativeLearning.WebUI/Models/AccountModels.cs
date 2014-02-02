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
            [Compare("NewPassword", ErrorMessage = "Şifreniz ve onaylama şifreniz birbiriyle uyuşmuyor.")]
            public string ConfirmPassword { get; set; }
        }

        public class LogOnModel
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
            [Display(Name = "Kullanıcı Adı")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Email Adresi")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "{0} en az {2} karatkterden oluşmalı.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Şifre")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Şifre Tekrar")]
            [Compare("Password", ErrorMessage = "Şifreniz ve onaylama şifreniz birbiriyle uyuşmuyor.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Adınız")]
            public string FirsName { get; set; }

            [Required]
            [Display(Name = "Soyadınız")]
            public string LastName { get; set; }

        }

    }
}
