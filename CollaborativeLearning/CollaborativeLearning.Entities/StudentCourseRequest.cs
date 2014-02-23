using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class StudentCourseRequest
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int SemesterId { get; set; }
        public virtual Semester Semester { get; set; }

        public bool isApproved { get; set; }

        [Display(Name = "Request Date")]
        public DateTime reqDate { get; set; }

        [Display(Name = "Register Date")]
        public DateTime regDate { get; set; }

        [Required]
        public int regUserID { get; set; }

    }
}
