using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class ScenarioTask:BaseEntity
    {
        public int SemesterID { get; set; }
        public virtual Semester Semester { get; set; }

        [Required(ErrorMessage = "Görev Tanımı alanı boş bırakılamaz")]
        [Display(Name = " Görev Tanımı")]
        [MaxLength(500, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string description { get; set; }
    }
}
