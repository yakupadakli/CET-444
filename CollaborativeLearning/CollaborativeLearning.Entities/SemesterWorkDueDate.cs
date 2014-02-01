using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class SemesterWorkDueDate:BaseEntity
    {
        public int semesterID { get; set; }
        public virtual Semester Semester { get; set; }

        public int workID { get; set; }
        public virtual Work Works { get; set; }

        [Required(ErrorMessage = "Bitiş Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Bitiş Tarihi")]
        public DateTime dueDate { get; set; }
    }
}
