using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Scenario:BaseEntity
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }


        [Required]
        public int regUserID { get; set; }

        [Required(ErrorMessage = "Senaryo Adı alanı boş bırakılamaz")]
        [Display(Name = "Senaryo Adı")]
        [MaxLength(50, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string ScenarioName { get; set; }


        [Required(ErrorMessage = "Kısa Tanım alanı boş bırakılamaz")]
        [Display(Name = "Kısa Tanım")]
        [MaxLength(500, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string ShortDescription { get; set; }

        public int statusID { get; set; }
        public virtual Status Status { get; set; }


        [Required(ErrorMessage = "Kayıt Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime regDate { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; }
        public virtual ICollection<ActionPlanList> ActionPlanLists { get; set; }
        public virtual ICollection<Work> Works { get; set; }
    }
}
