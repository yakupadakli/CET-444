using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeLearning.Entities
{
    class SubmittedWork:Base
    {

        public int groupsWorkID { get; set; }
        public virtual GroupWork GroupWorks { get; set; }
        [Required(ErrorMessage = "Teslim Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Teslim Tarihi")]
        public DateTime submissionDate { get; set; }

        public virtual ICollection<Files> Files { get; set; }

    }
}
