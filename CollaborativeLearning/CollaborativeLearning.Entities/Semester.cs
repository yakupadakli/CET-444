using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Semester:BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Kayıt Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime regDate { get; set; }

        [Required]
        public int regUserID { get; set; }
        [Required(ErrorMessage = "Yıl alanı boş bırakılamaz")]
        [Display(Name = "Yıl")]
        public DateTime year { get; set; }


        [Required(ErrorMessage = "Dönem alanı boş bırakılamaz")]
        [Display(Name = "Dönem")]
        public int semester { get; set; }

        [Display(Name = "Özel Kod")]
        public string specialCode { get; set; }

        public int statusID { get; set; }
        public virtual Status Status { get; set; }

        public virtual ICollection<Scenario> Scenarios { get; set; }
    }
}
