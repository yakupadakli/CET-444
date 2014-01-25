using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeLearning.Entities
{
    class SemesterWorkDueDate:Base
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
