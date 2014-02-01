using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class SubmittedWorkStatus:BaseEntity
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

        public Boolean isSeen { get; set; }

        [Required(ErrorMessage = " Son Görülme Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Son Görülme Tarihi")]
        public DateTime lastSeenDate { get; set; }
    }
}
