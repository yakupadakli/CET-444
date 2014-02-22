using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Semester
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "The year field cannot be empty!")]
        [Display(Name = "Year")]
        public int year { get; set; }
        [Required(ErrorMessage = "The semester field cannot be empty!")]
        [Display(Name = "Semester")]
        public int semester { get; set; }
        
        [Required(ErrorMessage = "The Course Name field cannot be empty!")]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }
        
        [Required(ErrorMessage = "The Section field cannot be empty!")]
        [Display(Name = "Section")]
        public int Section { get; set; }

        public virtual string semesterName
        {
            get
            {
                return year.ToString() + "-" + semester.ToString()+" "+CourseName+"."+Section.ToString();
            }
        }
        [Display(Name = "Register Code")]
        public string registerCode { get; set; }
        public string mentorRegisterCode{get; set;}

        public bool isActive { get; set; }

        [Required(ErrorMessage = "Register Date cannot be empty!")]
        [Display(Name = "Regiter Date")]
        public DateTime regDate { get; set; }

        [Required]
        public int regUserID { get; set; }


        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Scenario> Scenarios { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Reflection> Reflections { get; set;}
        public virtual ICollection<Resource> Resources { get; set; }

     }
}
