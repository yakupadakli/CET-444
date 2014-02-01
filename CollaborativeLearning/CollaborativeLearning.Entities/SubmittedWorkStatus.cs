using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class SubmittedWorkStatus:BaseEntity
    {
        public int submittedWorkID { get; set; }
        public virtual SubmittedWork SubmittedWork { get; set; }

        public Boolean isSeen { get; set; }

        [Required(ErrorMessage = " Son Görülme Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Son Görülme Tarihi")]
        public DateTime lastSeenDate { get; set; }
    }
}
