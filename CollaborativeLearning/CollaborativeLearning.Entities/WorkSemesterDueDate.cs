using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class WorkSemesterDueDate
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
             
        public int SemesterID { get; set; }
        public virtual Semester Semester { get; set; }

        public int WorkID { get; set; }
        public virtual Work Work { get; set; }

        [Required(ErrorMessage = "Bitiş Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Bitiş Tarihi")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Kayıt Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime RegDate { get; set; }

        [Required]
        public int RegUserID { get; set; }
    }
}
