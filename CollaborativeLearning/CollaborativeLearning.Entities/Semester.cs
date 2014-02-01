using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Semester:BaseEntity
    {
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
