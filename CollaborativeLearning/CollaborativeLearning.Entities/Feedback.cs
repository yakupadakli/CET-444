using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Feedback:BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Kayıt Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime regDate { get; set; }

        [Required]
        public int regUserID { get; set; }

        public int submittedWorkID { get; set; }
        public virtual SubmittedWork SubmittedWork { get; set; }

        [Required(ErrorMessage = "Dönüt alanı boş bırakılamaz")]
        [Display(Name = " Dönüt")]
        [MaxLength(500, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string value { get; set; }


        [Required(ErrorMessage = "Tarih alanı boş bırakılamaz")]
        [Display(Name = "Tarih")]
        public DateTime dueDate { get; set; }

        public int statusID { get; set; }
        public virtual Status status { get; set; }

        public int parentID { get; set; }

        public virtual Feedback parent { get; set; }
    }
}
