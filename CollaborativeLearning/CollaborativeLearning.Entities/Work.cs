using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Work:BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Kayıt Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime regDate { get; set; }

        [Required]
        public int regUserID { get; set; }
        [Required(ErrorMessage = "İş Adı alanı boş bırakılamaz")]
        [Display(Name = "İş Adı")]
        [MaxLength(50, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string groupName { get; set; }

        public int scenarioID { get; set; }
        public virtual Scenario Scenario { get; set; }

        [ForeignKey("Status")]
        public int statusID { get; set; }
        
        public virtual Status Status { get; set; }
    }
}
