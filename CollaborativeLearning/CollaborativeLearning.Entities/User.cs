using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        [Display(Name = "No")]
        public virtual int UserId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public virtual String Username { get; set; }

        [Display(Name = "Email")]
        public virtual String Email { get; set; }

        [Display(Name = "Şifre")]
        [Required, DataType(DataType.Password)]
        public virtual String Password { get; set; }

        [Display(Name = "Adı")]
        public virtual String FirstName { get; set; }

        [Display(Name = "Soyadı")]
        public virtual String LastName { get; set; }

        [Display(Name = "Adı Soyadı")]
        public virtual string FullName { get { return FirstName + " " + LastName; } }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Yorum")]
        public virtual String Comment { get; set; }

        [Display(Name = "Onay Durumu")]
        public virtual Boolean IsApproved { get; set; }

        public virtual int PasswordFailuresSinceLastSuccess { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime? LastPasswordFailureDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime? LastActivityDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime? LastLockoutDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime? LastLoginDate { get; set; }

        public virtual String ConfirmationToken { get; set; }


        [Display(Name = "Kayıt Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime? CreateDate { get; set; }

        [Display(Name = "Kayıt Tarihi")]
        public virtual string ShortCreateDate
        {
            get
            {
                if (CreateDate != null)
                {
                    DateTime d = Convert.ToDateTime(CreateDate);
                    return d.ToShortDateString();
                }
                return DateTime.Now.ToShortDateString();
            }
        }

        public virtual Boolean IsLockedOut { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime? LastPasswordChangedDate { get; set; }
        public virtual String PasswordVerificationToken { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime? PasswordVerificationTokenExpirationDate { get; set; }
        public virtual string LastLoginIP { get; set; }


        [Display(Name = "Roller")]
        public virtual ICollection<Role> Roles { get; set; }
    }

    //public class LocalPasswordModel
    //{
    //    [Required]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Current password")]
    //    public string OldPassword { get; set; }

    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "New password")]
    //    public string NewPassword { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm new password")]
    //    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    //    public string ConfirmPassword { get; set; }
    //}

    //public class LoginModel
    //{
    //    [Required]
    //    [Display(Name = "User name")]
    //    public string UserName { get; set; }

    //    [Required]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Password")]
    //    public string Password { get; set; }

    //    [Display(Name = "Remember me?")]
    //    public bool RememberMe { get; set; }
    //}

    //public class RegisterModel
    //{
    //    [Required]
    //    [Display(Name = "User name")]
    //    public string UserName { get; set; }

    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Password")]
    //    public string Password { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm password")]
    //    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    //    public string ConfirmPassword { get; set; }
    //}
}
