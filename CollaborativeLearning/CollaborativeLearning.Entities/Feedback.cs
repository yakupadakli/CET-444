using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeLearning.Entities
{
    class Feedback:Base
    {
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
