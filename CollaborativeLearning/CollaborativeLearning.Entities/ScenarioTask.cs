using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class ScenarioTask
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Kayıt Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime regDate { get; set; }

        [Required]
        public int regUserID { get; set; }
        public int SemesterID { get; set; }
        public virtual Semester Semester { get; set; }

        [Required(ErrorMessage = "Görev Tanımı alanı boş bırakılamaz")]
        [Display(Name = " Görev Tanımı")]
        [MaxLength(500, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string description { get; set; }
    }
}
