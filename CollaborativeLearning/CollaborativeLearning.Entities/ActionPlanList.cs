using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class ActionPlanList:BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int regUserID { get; set; }
        [Required(ErrorMessage = "Tanım alanı boş bırakılamaz")]
        [Display(Name = "Tanım")]
        [MaxLength(500, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string value { get; set; }


        [Required(ErrorMessage = "Kayıt Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime regDate { get; set; }

        public int statusID { get; set; }
        public virtual Status Status { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; }
    }
}
