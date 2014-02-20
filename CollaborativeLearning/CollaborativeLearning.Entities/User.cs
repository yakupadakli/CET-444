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
        public virtual int Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public virtual String Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        public virtual String Email { get; set; }

        [Display(Name = "Pasword")]
        [Required, DataType(DataType.Password)]
        public virtual String Password { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public virtual String FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public virtual String LastName { get; set; }

        [Display(Name = "Full Name")]
        public virtual string FullName { get { return FirstName + " " + LastName; } }

        [Required]
        [Display(Name = "Gender")]
        public String Gender { get; set; }
        
        [Required]
        [Display(Name = "Phone")]
        public String PhoneNumber{get; set;}      
  
        [Display(Name = "Is Approved")]
        public virtual Boolean IsApproved { get; set; }

        [Display(Name = "Is Active")]
        public virtual Boolean IsLockedOut { get; set; }

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


        [Display(Name = "Register Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime? CreateDate { get; set; }

        [Display(Name = "Register Date")]
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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime? LastPasswordChangedDate { get; set; }

        public virtual String PasswordVerificationToken { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime? PasswordVerificationTokenExpirationDate { get; set; }

        public virtual string LastLoginIP { get; set; }

        public int RoleID { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Reflection> Reflections { get; set; }


    }

}
