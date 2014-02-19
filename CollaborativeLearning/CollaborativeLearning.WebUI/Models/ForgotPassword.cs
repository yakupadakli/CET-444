using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CollaborativeLearning.WebUI.Models
{
    public class ForgotPassword
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ResetPasswordModel
    {
        [StringLength(100, ErrorMessage = "{0} must contain at least {2} character(s)", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter new password.")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "The confirmation password and password are not same.")]
        public string ConfirmPassword { get; set; }
    }
}