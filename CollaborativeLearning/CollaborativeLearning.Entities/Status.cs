using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Status:BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Kayıt Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime regDate { get; set; }

        [Required]
        public int regUserID { get; set; }
        [Required(ErrorMessage = "Durum alanı boş bırakılamaz")]
        [Display(Name = "Durum")]
        [MaxLength(50, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string value { get; set; }

        public virtual ICollection<Work> Works { get; set; }
        public virtual ICollection<Semester> Semesters { get; set; }
        public virtual ICollection<GroupWork> GroupWorks { get; set; }
        public virtual ICollection<ActionPlanList> ActionPlanLists { get; set; }
    }
}
