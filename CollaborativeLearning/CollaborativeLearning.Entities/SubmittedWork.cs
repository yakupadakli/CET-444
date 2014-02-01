using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class SubmittedWork:BaseEntity
    {
        public int groupsWorkID { get; set; }
        public virtual GroupWork GroupWorks { get; set; }
        [Required(ErrorMessage = "Teslim Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Teslim Tarihi")]
        public DateTime submissionDate { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}
